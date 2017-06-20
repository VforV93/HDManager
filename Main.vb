Imports System.Data.SqlClient
Imports System.IO
Imports WbemScripting
Public Class Main
    Public Cnndb As New ADODB.Connection
    Dim Rstdb, Rstdb2, Rstdb3 As New ADODB.Recordset

    Dim Strconnect As String = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" & My.Application.Info.DirectoryPath & "\DatiGestioneHd.mdb;Persist Security Info=True;Jet OLEDB:Database Password=" & db_pass & ";"

    'Dim Strconnect As String = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=\\Serverhda\gestionehd\database\gestionehd.mdb;Persist Security Info=True;Jet OLEDB:Database Password=" & db_pass & ";"
    'Dim Strconnect As String = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=\\noemanas\CustomerService\gestionehd\database\DatiGestionehd.mdb;Persist Security Info=True;Jet OLEDB:Database Password=" & db_pass & ";"
    Dim strsql As String
    Dim IdImpianto As Long
    Dim IdRiferimento As Long
    Dim NomeImpianto As String
    Dim Lavora, LavoraFile As System.Threading.Thread
    'Dim CheckTicket As Boolean = False
    Dim WithEvents Up As New Net.WebClient
    Dim vpnconnectionfactory As VPNConnectionFactory = New VPNConnectionFactory()
    Dim vpnconnection As IVPNConnection



   

    Private Sub Main_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MsgBox("closed")
        Lavora = Nothing
        LavoraFile = Nothing
    End Sub
    Private Sub Main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MsgBox("cloasing")
        Cnndb.Close()
        'TODO: RIVEDERE... MAGARI METTERE AL CLOSED?
        Try
            If My.Computer.Name.ToUpper.StartsWith("VMHD") Then
                If File.Exists("\\noemanas\CustomerService\GestioneHD\VM\" & My.Computer.Name & ".txt") Then
                    File.Delete("\\noemanas\CustomerService\GestioneHD\VM\" & My.Computer.Name & ".txt")
                End If
            End If

            If Lavora Is Nothing = False Then
                Try
                    If Lavora.IsAlive Then
                        Lavora.Abort(Lavora)
                        Lavora = Nothing
                    End If
                    Lavora = Nothing
                    GC.WaitForPendingFinalizers()
                    GC.Collect()
                Catch ex As Exception

                End Try
            End If
            If LavoraFile Is Nothing = False Then
                Try
                    If LavoraFile.IsAlive Then
                        LavoraFile.Abort(Lavora)
                        LavoraFile = Nothing
                    End If
                    LavoraFile = Nothing
                    GC.WaitForPendingFinalizers()
                    GC.Collect()
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Up_DownloadFileCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles Up.DownloadFileCompleted
        Me.Close()
        System.Diagnostics.Process.Start(Application.StartupPath & "\Update.exe")
    End Sub
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With My.Application.Info.Version
            Me.Text = "HdManager " & .Major & "." & .Minor & "." & .Build & "." & .Revision
        End With
        Try
            Application.DoEvents()
            'Vado con l'aggiornamento se raggiungo i server Noemalife:
            If My.Computer.Network.IsAvailable = True Then
                If My.Computer.Network.Ping("noemanas") = True Then
                    'TODO: Per ora lo tolgo (e da rivedere poi) perchè in debug perdo un sacco di tempo per il check
                    Dim Version As String = ""
                    Using LeggiVer As New StreamReader("\\noemanas\CustomerService\GestioneHD\Update\Last")
                        Version = LeggiVer.ReadLine()

                        LeggiVer.Close()
                        LeggiVer.Dispose()
                    End Using
                    'Aggiorno l'exe se c'è nuova versione:                    
                    If My.Application.Info.Version.ToString < Version Then
                        If File.Exists("\\noemanas\CustomerService\GestioneHD\Update\Update.exe") Then
                            If MsgBox("E' disponibile un aggiornamento di HdManager. Installarlo?", MsgBoxStyle.YesNo, "Aggiornamento Disponibile") = MsgBoxResult.Yes Then
                                'L'update lo copio sempre perchè può essere utile cambiare anche quello:                    
                                Dim Cetiri As New Uri("\\noemanas\CustomerService\GestioneHD\Update\Update.exe")

                                Up.DownloadFileAsync(Cetiri, Application.StartupPath & "\Update.exe")
                            End If
                        End If
                    Else
                        'PARTE DI AGGIORNAMENTO DEL DB:
                        'TODO: VEDERE COME SI COMPORTA LA COPIA DEL DB QUANDO FA UPDATE DELL'EXE CHE CHIUDE IL PROG 
                        If Debugger.IsAttached = False Then
                            Aggiorna.Focus()
                            Aggiorna.ShowDialog(Me)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Information)
        End Try

        My.Settings.gestioneHDConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & _
        "\DatiGestioneHD.mdb;Persist Security Info=True;Jet OLEDB:Database Password=" & db_pass & ";"

        Cnndb.Open(Strconnect)
        strsql = "select * from impianti where attivoimpianto='1' order by nomeimpianto asc"
        Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        If Rstdb.RecordCount <> 0 Then
            CmbImpianti.Items.Clear()
            While Rstdb.EOF <> True
                CmbImpianti.Items.Add(Rstdb("NomeImpianto").Value & "    -    " & Rstdb("IdImpianto").Value)
                Rstdb.MoveNext()
            End While
        End If

        Rstdb.Close()

        CmbImpianti.Select()
    End Sub
    Private Sub Indietro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Indietro.Click
        If Web.CanGoBack Then
            Web.GoBack()
        End If
        'Indietro è enabled solo se si può andare indietro
        Indietro.Enabled = Web.CanGoBack()
    End Sub
    Private Sub Web_CanGoBackChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Web.CanGoBackChanged
        Indietro.Enabled = Web.CanGoBack()
        Indietro.Visible = Web.CanGoBack
    End Sub
    Private Sub CmbImpianti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbImpianti.Click
        CmbImpianti.SelectAll()
    End Sub



    Private Sub CmbImpianti_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbImpianti.SelectedValueChanged
        If CmdDisconnectVpn.Visible = True Then
            CmdDisconnectVpn_Click(CmbImpianti, e)
            If CmdDisconnectVpn.Visible Then 'simulando il click dovrebbe già essere nascosto
                CmdDisconnectVpn.Hide()
            End If
        End If

        NomeVPN1.Checked = False
        NomeVPN2.Checked = False
        NomeVPN3.Checked = False
        NomeVPN4.Checked = False
        NomeVPN5.Checked = False
        NomeVPN6.Checked = False

        Dim Posizione As Short = 0
        Dim StringaAppoggio As String
        Dim Note As String

        Try
            Posizione = InStr(CmbImpianti.Text, "    -    ")
            NomeImpianto = Mid(CmbImpianti.Text, 1, (Posizione - 1))
            StringaAppoggio = Mid(CmbImpianti.Text, Posizione, Len(CmbImpianti.Text))
            IdImpianto = LTrim(Mid(StringaAppoggio, InStr(StringaAppoggio, "-") + 1))
            'MsgBox("Posizione:" & Posizione)
            'MsgBox("NomeImpianto:" & NomeImpianto)
            'MsgBox("StringaAppoggio:" & StringaAppoggio)
            'MsgBox("IdImpianto:" & IdImpianto)

            strsql = "select * from impianti where idimpianto=" & IdImpianto
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            IdRiferimento = Rstdb("idriferimento").Value
            Note = Rstdb("noteconnessione").Value.ToString

            Rstdb.Close()
            'Se c'è skynet mostro il pulsante per collegarsi:            
            strsql = "select * from prodotti where id_riferimento=" & IdRiferimento & " and nome='Sistema di Monitoraggio'"
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            If Rstdb.RecordCount <> 0 Then
                CmdSkynet.Show()
            Else
                CmdSkynet.Hide()
            End If

            Rstdb.Close()
            'Metto le password di vnc:
            LstVnc.Items.Clear()
            strsql = "select distinct(PwdVnc) from pwdvnc where IdRiferimento=" & IdRiferimento
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            If Rstdb.RecordCount <> 0 Then
                While Rstdb.EOF <> True
                    LstVnc.Items.Add(Rstdb("PwdVnc").Value)

                    Rstdb.MoveNext()
                End While
            End If

            Rstdb.Close()

            strsql = "select * from impianti_connessioni where idimpianto=" & IdImpianto
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            If Rstdb.RecordCount = 0 Then
                MsgBox("Nessuna connessione associata all'impianto", MsgBoxStyle.Information)
                Rstdb.Close()
                Exit Sub
            End If

            Rstdb.Close()

            strsql = "select * from impianti_connessioni where idimpianto=" & IdImpianto & " and tipoconnessione<>'Internet VPN'"
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            Tab.Show()
            'Ciclo nelle vpn cisco e open e flaggo le checkbox:
            Dim VpnFound As Boolean = False
            Dim x As Byte
            If Rstdb.RecordCount <> 0 Then
                For x = 0 To Rstdb.RecordCount - 1
                    VpnFound = True
                    GroupInternet.Hide()
                    TabVpn.Hide()
                    Tab.SelectTab("TabServer")
                    GroupVpn.Show()

                    If Rstdb("Tipoconnessione").Value = "Logmein" Then
                        TxtUser.Text = Rstdb("Utenteconnessione").Value
                        TxtPwd.Text = Rstdb("Passwordconnessione").Value
                    End If

                    If x = 0 Then
                        NomeVPN1.Text = Rstdb("nomeconnessione").Value.ToString
                        If Rstdb.RecordCount = 1 Then NomeVPN1.Checked = True
                        NomeVPN2.Hide()
                        NomeVPN3.Hide()
                        NomeVPN4.Hide()
                        NomeVPN5.Hide()
                        NomeVPN6.Hide()
                    ElseIf x = 1 Then
                        NomeVPN2.Text = Rstdb("nomeconnessione").Value.ToString
                        NomeVPN2.Show()
                    ElseIf x = 2 Then
                        NomeVPN3.Text = Rstdb("nomeconnessione").Value.ToString
                        NomeVPN3.Show()
                    ElseIf x = 3 Then
                        NomeVPN4.Text = Rstdb("nomeconnessione").Value.ToString
                        NomeVPN4.Show()
                    ElseIf x = 4 Then
                        NomeVPN5.Text = Rstdb("nomeconnessione").Value.ToString
                        NomeVPN5.Show()
                    ElseIf x = 5 Then
                        NomeVPN6.Text = Rstdb("nomeconnessione").Value.ToString
                        NomeVPN6.Show()
                    End If
                    Rstdb.MoveNext()
                Next
            End If

            Rstdb.Close()
            'Vedo se c'è vpn internet:
            strsql = "select * from impianti_connessioni where idimpianto=" & IdImpianto & " and tipoconnessione='Internet VPN'"
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            If Rstdb.RecordCount <> 0 Then
                TxtUser.Text = Rstdb("Utenteconnessione").Value.ToString
                TxtPwd.Text = Rstdb("Passwordconnessione").Value.ToString
                GroupInternet.Show()

                If VpnFound = False Then
                    GroupVpn.Hide()
                    GroupInternet.Location = GroupVpn.Location
                Else
                    GroupInternet.Location = New Point(GroupInternet.Location.X, GroupVpn.Height + GroupVpn.Location.Y + 25)
                End If

                Tab.SelectTab("TabVpn")
                TabVpn.Show()
                Dim Link As New System.Uri(Rstdb("connessionestringa").Value)
                Web.Url = Link
                Web.Focus()
                Web.Show()
            End If

            Rstdb.Close()

            'FILLO LA TAB SERVER:
            Me.ServerTableAdapter.Fill(Me.GestioneHDDataSet.Server, IdRiferimento)
            'FILLO LA TAB INTEGRAZIONI:
            Me.IntegrazioniTableAdapter.Fill(Me.GestioneHDDataSet.Integrazioni, IdRiferimento)
            'FILLO LA TAB PRODOTTI:
            Me.ProdottiTableAdapter.Fill(Me.GestioneHDDataSet.Prodotti, IdRiferimento)
            'FILLO LA TAB RUBRICA:
            Me.RubricaTableAdapter.Fill(Me.GestioneHDDataSet.Rubrica, IdRiferimento)
            'Metto a grigio le righe relative ai server di test/backup:
            With GrigliaServer
                For x = 0 To .RowCount - 1
                    If .Rows(x).Cells("BackupCol").Value = True OrElse .Rows(x).Cells("TestCol").Value = True Then
                        .Rows(x).DefaultCellStyle.BackColor = Color.Gainsboro
                    End If
                Next
            End With
            'Il testo in LEGGIMI:
            TxtLeggimi.Text = Note

            strsql = "select * from leggimi where id_impianto=" & IdImpianto
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            If Rstdb.RecordCount <> 0 Then
                TxtLeggimi.Text += vbCrLf & vbCrLf & vbCrLf & Rstdb("Testo").Value
            End If

            Rstdb.Close()

            GrigliaTicket.Hide()
            LblTicket.Show()
            LblTicket.Text = "Caricamento Ticket in corso ..."
            PrgTicket.Show()

            If Lavora Is Nothing = False Then
                If Lavora.ThreadState = Threading.ThreadState.Running Then
                    Exit Sub
                End If
                If Lavora.IsAlive Then
                    Lavora.Abort(Lavora)
                    Lavora = Nothing
                End If
                Lavora = Nothing
            End If

            Lavora = New System.Threading.Thread(AddressOf CaricaTicket)
            Lavora.Priority = Threading.ThreadPriority.Normal
            Lavora.Start()

            'Scrivo l'impianto nel file sul server:
            If LavoraFile Is Nothing = False Then
                If LavoraFile.ThreadState = Threading.ThreadState.Running Then
                    Exit Sub
                End If
                If LavoraFile.IsAlive Then
                    LavoraFile.Abort(LavoraFile)
                    LavoraFile = Nothing
                End If
                LavoraFile = Nothing
            End If

            LavoraFile = New System.Threading.Thread(AddressOf SalvaImpianto)
            LavoraFile.Priority = Threading.ThreadPriority.Normal
            LavoraFile.Start()
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            If Rstdb.State = 1 Then Rstdb.Close()
        End Try
    End Sub
    Private Sub CmdImportaTutti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdImportaTutti.Click
        Dim objServiceManager, objCoreReflection, objDesktop, myDoc, allSheets, mySheet, mySheet2 As Object

        objServiceManager = CreateObject("com.sun.star.ServiceManager")
        'Create the CoreReflection service that is later used to create structs
        objCoreReflection = objServiceManager.createInstance("com.sun.star.reflection.CoreReflection")
        'Create the Desktop
        objDesktop = objServiceManager.createInstance("com.sun.star.frame.Desktop")

        strsql = "delete from server"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from integrazioni"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from prodotti"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from leggimi"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from PwdVnc"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from Rubrica"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "select * from impianti where attivoimpianto='1' order by nomeimpianto"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        Try
            While Rstdb3.EOF <> True
                'CI METTO L'IDRIFERIMENTO VISTO CHE LE SCHEDE CLIENTI SONO COMUNI PER ASL:
                IdImpianto = Rstdb3("idimpianto").Value
                IdRiferimento = Rstdb3("idriferimento").Value
                strsql = "select nomeriferimento from riferimento_impianti where idriferimento=" & Rstdb3("idriferimento").Value
                Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                Dim Impianto As String = Rstdb("nomeriferimento").Value
                'If IdImpianto = 102 OrElse IdImpianto = 383 Then
                '    Dim cetiri As String = "prova"
                '    Dim pet As Long = 123
                'End If

                Rstdb.Close()

                Dim Path As String = ""

                If Directory.Exists("c:\SCHEDECLIENTI_BCK\" & Impianto & "\") Then
                    Dim SchedaDir As New DirectoryInfo("c:\SCHEDECLIENTI_BCK\" & Impianto & "\")
                    Dim FileList As New ArrayList(SchedaDir.GetFiles())
                    Dim b As Short
                    For b = 0 To FileList.Count - 1
                        If FileList.Item(b).ToString().Contains("Scheda") AndAlso FileList.Item(b).ToString().Contains("Old") = False AndAlso FileList.Item(b).ToString().EndsWith("xls") Then
                            Path = SchedaDir.FullName & FileList.Item(b).ToString()
                        End If
                        Exit For
                    Next
                Else
                    'LOGGO QUELLE DI CUI NON TROVO LA SCHEDA:
                    Using sw As StreamWriter = File.AppendText(My.Application.Info.DirectoryPath & "\Manca_Scheda " & Replace(CStr(Today.Date), "/", "-") & ".txt")

                        sw.WriteLine(vbCrLf & Impianto)

                        sw.Close()
                    End Using
                End If
                Try
                    If Path <> "" Then
                        'SE CON QUELL'IDRIFERIMENTO C'E' SCRITTO QUALCOSA IN SERVER ALLORA SUPPONGO CI SIANO GIA'
                        'I DATI INSERITI PER L'IMPIANTO E QUINDI NON RISCRIVO SERVER, PRODOTTI, ECC.:
                        strsql = "select * from server where id_riferimento=" & IdRiferimento
                        Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                        If Rstdb.RecordCount = 0 Then
                            Rstdb.Close()

                            Dim args(-1) As Object
                            myDoc = objDesktop.loadComponentFromURL("file:///" & Path, "_blank", 0, args)

                            allSheets = myDoc.Sheets
                            mySheet = allSheets.getByName("Cosa e Dove")
                            Dim ServerFound As Boolean = False

                            'IMPORTO I SERVER
                            strsql = "select * from server"
                            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                            Dim x, y, z As Short
                            Dim Backup As Boolean = False
                            Dim Test As Boolean = False
                            For x = 4 To 150
                                Dim Cell As String = mySheet.getCellRangeByName("C" & x).String
                                'MI DEVO PRENDERE DOVE VENGONO SCRITTI I PRODOTTI
                                If Cell.ToLower = "backup" Then
                                    Backup = True
                                    Test = False
                                End If
                                If Cell.ToLower = "test" Then
                                    Backup = False
                                    Test = True
                                End If

                                If Cell <> "" AndAlso Char.IsLetter(Cell, 0) <> True Then
                                    'Scrivo sul DB:
                                    With Rstdb
                                        ServerFound = True

                                        .AddNew()
                                        .Fields("id_impianto").Value = IdImpianto
                                        .Fields("id_riferimento").Value = IdRiferimento
                                        .Fields("Ip").Value = Cell
                                        .Fields("Backup").Value = Backup
                                        .Fields("Test").Value = Test

                                        'Mi prendo i prodotti dalle colonne:
                                        Dim Prodotti As String = ""
                                        Dim ColY As Byte
                                        'In alcuni fogli son scritti nella 4 in altri nella 2, quindi devo mettere una variabile:

                                        If mySheet.getCellByPosition(4, 1).String <> "" Then
                                            ColY = 1
                                        ElseIf mySheet.getCellByPosition(4, 2).String <> "" Then
                                            ColY = 2
                                        ElseIf mySheet.getCellByPosition(4, 3).String <> "" Then
                                            ColY = 3
                                        End If

                                        For y = 4 To 50
                                            Dim Col As String = mySheet.getCellByPosition(y, x - 1).String
                                            If Col.ToLower = "x" Then Prodotti += mySheet.getCellByPosition(y, ColY).String & ", "
                                        Next

                                        'If Prodotti.EndsWith(",") Then Prodotti = Prodotti.Remove(3, 5)

                                        .Fields("Prodotti").Value = Prodotti

                                        'VADO A PRENDERMI I DATI DAL FOGLIO MACCHINE:
                                        mySheet2 = allSheets.getByName("Macchine")

                                        For z = 4 To 150
                                            If Cell = mySheet2.getCellRangeByName("C" & z).String Then
                                                Rstdb("Alias").Value = mySheet2.getCellRangeByName("D" & z).String
                                                If mySheet2.getCellRangeByName("G1").String.ToString.Contains("Mac") OrElse mySheet2.getCellRangeByName("G2").String.ToString.Contains("Mac") Then
                                                    'Controllo se c'è la colonna macchina virtuale per il numero di col:
                                                    Rstdb("SO").Value = mySheet2.getCellRangeByName("H" & z).String
                                                    Rstdb("Dominio").Value = mySheet2.getCellRangeByName("I" & z).String
                                                    Rstdb("Utente").Value = mySheet2.getCellRangeByName("J" & z).String
                                                    Rstdb("Pwd").Value = mySheet2.getCellRangeByName("K" & z).String

                                                    If mySheet2.getCellRangeByName("L" & z).String.ToString.ToLower = "x" Then
                                                        Rstdb("Rdp").Value = True
                                                    Else
                                                        Rstdb("Rdp").Value = False
                                                    End If
                                                    If mySheet2.getCellRangeByName("M" & z).String.ToString.ToLower = "x" Then
                                                        Rstdb("Putty").Value = True
                                                    Else
                                                        Rstdb("Putty").Value = False
                                                    End If
                                                    Rstdb("Note").Value = mySheet2.getCellRangeByName("R" & z).String
                                                Else
                                                    Rstdb("SO").Value = mySheet2.getCellRangeByName("G" & z).String
                                                    Rstdb("Dominio").Value = mySheet2.getCellRangeByName("H" & z).String
                                                    Rstdb("Utente").Value = mySheet2.getCellRangeByName("I" & z).String
                                                    Rstdb("Pwd").Value = mySheet2.getCellRangeByName("J" & z).String

                                                    If mySheet2.getCellRangeByName("K" & z).String.ToString.ToLower = "x" Then
                                                        Rstdb("Rdp").Value = True
                                                    Else
                                                        Rstdb("Rdp").Value = False
                                                    End If
                                                    If mySheet2.getCellRangeByName("L" & z).String.ToString.ToLower = "x" Then
                                                        Rstdb("Putty").Value = True
                                                    Else
                                                        Rstdb("Putty").Value = False
                                                    End If
                                                    Rstdb("Note").Value = mySheet2.getCellRangeByName("Q" & z).String
                                                End If
                                                'PROVO A PRENDERE SU LE PWD DI VNC:
                                                Dim ColVnc As String = ""
                                                Dim Cetiri As String = mySheet2.getCellRangeByName("O1").String.ToString
                                                If mySheet2.getCellRangeByName("O1").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("O2").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("O3").String.ToString.ToLower = "vnc" Then
                                                    ColVnc = "O"
                                                ElseIf mySheet2.getCellRangeByName("P1").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("P2").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("P3").String.ToString.ToLower = "vnc" Then
                                                    ColVnc = "P"
                                                ElseIf mySheet2.getCellRangeByName("N1").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("N2").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("N3").String.ToString.ToLower = "vnc" Then
                                                    ColVnc = "N"
                                                ElseIf mySheet2.getCellRangeByName("Q1").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("Q2").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("Q3").String.ToString.ToLower = "vnc" Then
                                                    ColVnc = "Q"
                                                End If
                                                If ColVnc <> "" Then
                                                    If mySheet2.getCellRangeByName(ColVnc & z).String.ToString <> "" Then
                                                        Dim RstdbVnc As New ADODB.Recordset
                                                        Dim StrVnc As String = "select * from PwdVnc"
                                                        RstdbVnc.Open(StrVnc, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                                                        RstdbVnc.AddNew()
                                                        RstdbVnc("IdImpianto").Value = IdImpianto
                                                        RstdbVnc("IdRiferimento").Value = IdRiferimento
                                                        RstdbVnc("PwdVnc").Value = mySheet2.getCellRangeByName(ColVnc & z).String.ToString
                                                        RstdbVnc.Update()

                                                        RstdbVnc.Close()
                                                        'SPUNTO QUI CHE C'E' VNC:
                                                        Rstdb("Vnc").Value = True
                                                        'TODO: RIVEDERE! SCRIVO LA PWDVNC NELLA RIGA DEL SERVER PERCHE' MAGARI RELATIVA A LUI:
                                                        Rstdb("PwdVnc").Value = mySheet2.getCellRangeByName(ColVnc & z).String.ToString
                                                    End If
                                                End If
                                            End If
                                        Next

                                        Rstdb.Update()
                                    End With
                                End If
                                'QUESTO LO USO PER USCIRE PERCHè SE TROVO TRE RIGHE VUOTE ALLORA NON CI SONO PIU' SERVER... SPERO!
                                With mySheet
                                    If Cell = "" AndAlso mySheet.getCellRangeByName("C" & x + 1).String = "" AndAlso mySheet.getCellRangeByName("C" & x + 2).String = "" Then
                                        Exit For
                                    End If
                                End With
                            Next

                            Rstdb.Close()

                            'VADO A PRENDERE I DATI DAL FOGLIO LEGGIMI:
                            strsql = "select * from leggimi"
                            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                            mySheet = allSheets.getByName("Leggimi")

                            Dim Testo As String = ""
                            Dim b As Byte
                            For b = 1 To 250
                                Dim Cell As String = mySheet.getCellRangeByName("B" & b).String
                                If Cell <> "" Then
                                    Testo += Cell
                                    Testo += " " & mySheet.getCellRangeByName("C" & b).String & " " & mySheet.getCellRangeByName("D" & b).String & " " & _
                                    mySheet.getCellRangeByName("E" & b).String & vbCrLf
                                Else
                                    Testo += vbCrLf
                                End If
                            Next

                            Rstdb.AddNew()
                            Rstdb("Id_Impianto").Value = IdImpianto
                            Rstdb("Id_Riferimento").Value = IdRiferimento
                            Rstdb("Testo").Value = Testo
                            Rstdb.Update()

                            Rstdb.Close()

                            'VADO A PRENDERE I DATI DAL FOGLIO INTEGRAZIONI:
                            strsql = "select * from integrazioni"
                            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                            If IdImpianto = 109 OrElse IdImpianto = 108 Then
                                mySheet = allSheets.getByName("Integrazioni Rimini")
                            ElseIf IdImpianto = 106 OrElse IdImpianto = 51 OrElse IdImpianto = 75 Then
                                mySheet = allSheets.getByName("Integrazioni RAFALU")
                            ElseIf IdImpianto = 160 Then
                                mySheet = allSheets.getByName("Integrazioni FORLI")
                            ElseIf IdImpianto = 251 OrElse IdImpianto = 246 Then
                                mySheet = allSheets.getByName("Integrazioni CESENA")
                            Else
                                mySheet = allSheets.getByName("Integrazioni")
                            End If

                            For b = 4 To 250
                                Dim Cell As String = mySheet.getCellRangeByName("B" & b).String
                                If Cell <> "" Then
                                    Rstdb.AddNew()
                                    Rstdb("Id_Impianto").Value = IdImpianto
                                    Rstdb("Id_Riferimento").Value = IdRiferimento
                                    Rstdb("Nome").Value = Cell
                                    Rstdb("Modo").Value = mySheet.getCellRangeByName("C" & b).String
                                    Rstdb("Documentazione").Value = mySheet.getCellRangeByName("D" & b).String
                                    Rstdb.Update()
                                End If
                            Next

                            Rstdb.Close()

                            'VADO A PRENDERE I DATI DAL FOGLIO PRODOTTI:
                            Dim Rstdb4 As New ADODB.Recordset
                            strsql = "select * from prodotti"
                            Rstdb4.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                            mySheet = allSheets.getByName("Prodotti")
                            Dim Prodotto As Boolean = False
                            Try 'Metto in un try i prodotti perchè in alcune schede user e pwd sono scritte in G e H invece che F e G
                                For b = 4 To 250
                                    Dim Cell As String = mySheet.getCellRangeByName("C" & b).String
                                    If Cell.ToLower = "prodotto" Then Prodotto = True
                                    If Cell.ToLower <> "database" AndAlso Cell.ToLower <> "prodotto" AndAlso Cell.ToLower <> "backup" AndAlso Cell <> "" Then
                                        If Cell.ToLower = "backup" Then Exit For
                                        If Prodotto = False Then
                                            Dim c As Byte
                                            Dim ArrayUtente As New ArrayList
                                            Dim ArrayPwd As New ArrayList
                                            Dim Utente As String = ""
                                            Dim Pwd As String = ""
                                            For c = 0 To mySheet.getCellRangeByName("F" & b).String.ToString.Length - 1
                                                Dim car As Char = mySheet.getCellRangeByName("F" & b).String.Chars(c)
                                                If Char.IsWhiteSpace(car) = False Then
                                                    Utente += car
                                                Else
                                                    ArrayUtente.Add(Utente)
                                                    Utente = ""
                                                End If
                                                If c = mySheet.getCellRangeByName("F" & b).String.ToString.Length - 1 Then ArrayUtente.Add(Utente)
                                            Next
                                            For c = 0 To mySheet.getCellRangeByName("G" & b).String.ToString.Length - 1
                                                Dim car As Char = mySheet.getCellRangeByName("G" & b).String.Chars(c)
                                                If Char.IsWhiteSpace(car) = False Then
                                                    Pwd += car
                                                Else
                                                    ArrayPwd.Add(Pwd)
                                                    Pwd = ""
                                                End If
                                                If c = mySheet.getCellRangeByName("G" & b).String.ToString.Length - 1 Then ArrayPwd.Add(Pwd)
                                            Next

                                            'Inserisco tante righe quante sono le utenze:
                                            For c = 0 To ArrayUtente.Count - 1
                                                Rstdb4.AddNew()
                                                Rstdb4("Id_Impianto").Value = IdImpianto
                                                Rstdb4("Id_Riferimento").Value = IdRiferimento
                                                Rstdb4("Nome").Value = Cell
                                                Rstdb4("Versione").Value = mySheet.getCellRangeByName("D" & b).String
                                                Rstdb4("SidPorta").Value = mySheet.getCellRangeByName("E" & b).String
                                                Rstdb4("Utente").Value = ArrayUtente(c).ToString
                                                'La pwd la metto in un try perchè a volte il nr. di utente non corrisponde:
                                                Try
                                                    Rstdb4("Pwd").Value = ArrayPwd(c).ToString
                                                Catch ex As Exception

                                                End Try
                                                Rstdb4("Path").Value = mySheet.getCellRangeByName("H" & b).String
                                                Rstdb4("Url").Value = mySheet.getCellRangeByName("I" & b).String
                                                Rstdb4("Comandi").Value = mySheet.getCellRangeByName("J" & b).String
                                                Rstdb4("Note").Value = mySheet.getCellRangeByName("K" & b).String
                                                Rstdb4.Update()
                                            Next
                                        Else 'SONO SOTTO PRODOTTO E NON CICLO PER USER E PWD:
                                            Rstdb4.AddNew()
                                            Rstdb4("Id_Impianto").Value = IdImpianto
                                            Rstdb4("Id_Riferimento").Value = IdRiferimento
                                            Rstdb4("Nome").Value = Cell
                                            Rstdb4("Versione").Value = mySheet.getCellRangeByName("D" & b).String
                                            Rstdb4("SidPorta").Value = mySheet.getCellRangeByName("E" & b).String
                                            Rstdb4("Utente").Value = mySheet.getCellRangeByName("F" & b).String
                                            Rstdb4("Pwd").Value = mySheet.getCellRangeByName("G" & b).String
                                            Rstdb4("Path").Value = mySheet.getCellRangeByName("H" & b).String
                                            Rstdb4("Url").Value = mySheet.getCellRangeByName("I" & b).String
                                            Rstdb4("Comandi").Value = mySheet.getCellRangeByName("J" & b).String
                                            Rstdb4("Note").Value = mySheet.getCellRangeByName("K" & b).String
                                            Rstdb4.Update()
                                        End If
                                    End If
                                Next
                                Rstdb4.Close()

                                'VADO A PRENDERE I DATI DAL FOGLIO RUBRICA:
                                strsql = "select * from rubrica"
                                Rstdb4.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                                mySheet = allSheets.getByName("Rubrica")

                                Dim Tipo As String = ""
                                For b = 4 To 250
                                    Dim Cell As String = mySheet.getCellRangeByName("C" & b).String
                                    If Cell <> "" Then
                                        'Bold è 150, normale è 100:
                                        If mySheet.getCellRangeByName("C" & b).CharWeight = 150 Then
                                            Tipo = mySheet.getCellRangeByName("C" & b).String
                                        Else
                                            Rstdb4.AddNew()
                                            Rstdb4("Id_Impianto").Value = IdImpianto
                                            Rstdb4("Id_Riferimento").Value = IdRiferimento
                                            Rstdb4("Tipo").Value = Tipo
                                            Rstdb4("Nome").Value = Cell
                                            Rstdb4("Sede").Value = mySheet.getCellRangeByName("E" & b).String
                                            Rstdb4("Fax").Value = mySheet.getCellRangeByName("F" & b).String
                                            Rstdb4("Telefono").Value = mySheet.getCellRangeByName("G" & b).String
                                            Rstdb4("Cellulare").Value = mySheet.getCellRangeByName("H" & b).String
                                            Rstdb4("Email").Value = mySheet.getCellRangeByName("I" & b).String
                                            Rstdb4("Competenze").Value = mySheet.getCellRangeByName("J" & b).String
                                            Rstdb4("Note").Value = mySheet.getCellRangeByName("K" & b).String

                                            Rstdb4.Update()
                                        End If
                                    End If
                                Next

                                Rstdb4.Close()

                            Catch ex As Exception
                                Rstdb4 = Nothing
                            End Try
                            myDoc.close(True)
                        Else
                            Rstdb.Close() 'CHIUDO IL RECORDSET CON CUI HO CONTROLLATO SE GIA' SCRITTO IN TAB QUELL'IDRIFERIMENTO
                        End If
                    End If
                Catch ex As Exception
                    'LOGGO L'ERRORE:
                    Using sw As StreamWriter = File.AppendText(My.Application.Info.DirectoryPath & "\XlsError " & Replace(CStr(Today.Date), "/", "-") & ".txt")

                        sw.WriteLine("Id Impianto: " & IdImpianto & " " & ex.Message.ToString)

                        sw.Close()
                    End Using
                    'CHIUDO L'RSTDB SE NO MI SLATA LE SCHEDE DOPO!
                    If Rstdb.State = 1 Then Rstdb.Close()
                End Try

                Rstdb3.MoveNext()
            End While
        Catch ex As Exception
            'LOGGO QUELLE DI CUI NON TROVO LA SCHEDA:
            Using sw As StreamWriter = File.AppendText(My.Application.Info.DirectoryPath & "\Importa_Error " & Replace(CStr(Today.Date), "/", "-") & ".txt")

                sw.WriteLine("Id Impianto: " & IdImpianto & " " & ex.Message.ToString)

                sw.Close()
            End Using
        End Try

        Rstdb3.Close()
        MsgBox("Finito", MsgBoxStyle.Information)

    End Sub
    Private Sub CmdPlSql_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdPlSql.Click
        If GrigliaProd.SelectedRows.Count = 0 Then
            MsgBox("Selezionare la riga relativa al DB a cui ci si vuole collegare", MsgBoxStyle.Information)
            Exit Sub
        End If

        'Creo il tnsnames:
        Try
            TnsNames(IdImpianto)

            Dim x As Short

            With GrigliaProd.SelectedRows
                For x = 0 To .Count - 1
                    Dim c As Byte
                    Dim Sid As String = ""
                    Dim NomeTns As String = ""
                    For c = 0 To .Item(x).Cells("SidPortaCol").Value.ToString.Length - 1
                        Dim car As Char = .Item(x).Cells("SidPortaCol").Value.ToString.Chars(c)
                        If Char.IsControl(car) OrElse Char.IsWhiteSpace(car) Then Exit For                        

                        Sid += car                        
                    Next

                    'Col sid trovato vado sul db nella tabella tnsnames in cui c'è sperando che siano uguali....
                    strsql = "select * from tnsnames where idimpianto =" & IdImpianto & " and sidtns='" & Sid & "'"
                    Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                    If Rstdb.RecordCount <> 0 Then
                        NomeTns = Rstdb("NomeTns").Value
                    End If

                    Rstdb.Close()

                    Dim p As New ProcessStartInfo
                    Dim Param As String
                    'Se è selezionato uno script lo lancio con quello:
                    If LstSqlScript.SelectedItems.Count <> 0 Then
                        Param = "userid=" & .Item(x).Cells("PlUtente").Value.ToString & "/" & .Item(x).Cells("PlPwd").Value.ToString & "@" & NomeTns & " " & """" & Application.StartupPath & "\SqlScript\" & LstSqlScript.SelectedItems(0) & """"
                    Else
                        'Altrimenti lo lancio con blank.sql in cui c'è solo select * from :
                        If Not File.Exists(Application.StartupPath & "\Blank.sql") Then
                            Using BlankSql As New StreamWriter(Application.StartupPath & "\Blank.sql")
                                BlankSql.Write("select * from ")

                                BlankSql.Close()
                                BlankSql.Dispose()
                            End Using
                        End If

                        Param = "userid=" & .Item(x).Cells("PlUtente").Value.ToString & "/" & .Item(x).Cells("PlPwd").Value.ToString & "@" & NomeTns & " " & """" & Application.StartupPath & "\Blank.sql" & """"
                    End If

                    ' Specify the location of the binary
                    p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\PlSql Developer\plsqldev.exe"

                    ' Use these arguments for the process
                    p.Arguments = Param

                    ' Start the process
                    Process.Start(p)
                Next
            End With
            'Deseleziono l'eventuale script cliccato:
            If LstSqlScript.SelectedItems.Count <> 0 Then
                LstSqlScript.SelectedItems.Remove(LstSqlScript.SelectedItems.Item(0))
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub CmdConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdConnect.Click
        MsgBox("NUOVO!!!")
        'Dim vpnconnection As IVPNConnection
        Dim Vpn As String = ""
        Dim dbmanager As DbManager = DbManager.Instance

        If NomeVPN1.Checked = True Then
            Vpn = NomeVPN1.Text
        ElseIf NomeVPN2.Checked = True Then
            Vpn = NomeVPN2.Text
        ElseIf NomeVPN3.Checked = True Then
            Vpn = NomeVPN3.Text
        ElseIf NomeVPN4.Checked = True Then
            Vpn = NomeVPN4.Text
        ElseIf NomeVPN5.Checked = True Then
            Vpn = NomeVPN5.Text
        ElseIf NomeVPN6.Checked = True Then
            Vpn = NomeVPN6.Text
        Else
            MsgBox("Selezionare la VPN a cui connettersi", MsgBoxStyle.Information)
            Exit Sub
        End If

        strsql = "select * from impianti_connessioni where idimpianto=" & IdImpianto & " and nomeconnessione='" & Vpn & "'"
        Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        If Rstdb.RecordCount = 0 Then
            MsgBox("Nessuna connessione trovata sul DB, aggiornare le informazioni per collegarsi", MsgBoxStyle.Information)
            Rstdb.Close()
            Exit Sub
        End If

        vpnconnection = vpnconnectionfactory.getIVPNConnection(IdImpianto, Rstdb)
        vpnconnection.connect()

        Rstdb.Close()
    End Sub
    Sub SalvaImpianto()
        'Salvo l'impianto a cui sono connesso se sono in una VM:
        Try
            If My.Computer.Name.ToUpper.StartsWith("VM") Then
                If My.Computer.Network.Ping("noemanas") Then
                    Using sw As StreamWriter = File.CreateText("\\noemanas\CustomerService\GestioneHD\VM\" & My.Computer.Name & ".txt")
                        sw.WriteLine(My.User.Name)
                        sw.WriteLine(NomeImpianto)

                        sw.Close()
                        sw.Dispose()
                    End Using
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CmdRdp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRdp.Click
        'Controllo se è spuntato l'RDP manuale:
        If ChkRdp.Checked = True Then
            Dim p As New ProcessStartInfo

            p.FileName = "mstsc.exe"

            Process.Start(p)

            ChkRdp.Checked = False
            Exit Sub
        End If

        If GrigliaServer.SelectedRows.Count = 0 Then
            MsgBox("Selezionare le righe relative ai server a cui ci si vuole collegare", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim x As Byte

        For x = 0 To GrigliaServer.SelectedRows.Count - 1
            With GrigliaServer.SelectedRows(x)
                'USER E PWD NON INSERITI:
                If .Cells("UsernameCol").Value.ToString = "" OrElse .Cells("PasswordCol").Value.ToString = "" Then
                    MsgBox("Uno o più server non hanno User e Password salvati sul DB, aggiornarli per connettersi automaticamente." & vbCrLf & _
                    "Per questi verrà aperta la connessione con la richiesta di credenziali", MsgBoxStyle.Information)
                    Exit For
                End If
            End With
        Next

        For x = 0 To GrigliaServer.SelectedRows.Count - 1
            With GrigliaServer.SelectedRows(x)
                'USER E PWD INSERITI:
                If .Cells("UsernameCol").Value.ToString <> "" AndAlso .Cells("PasswordCol").Value.ToString <> "" Then
                    Dim Utente As String = ""
                    Dim Pwd As String = ""
                    Dim MultiUser As Boolean = False
                    For c = 0 To .Cells("UsernameCol").Value.ToString.Length - 1
                        Dim car As Char = .Cells("UsernameCol").Value.ToString.Chars(c)
                        If Char.IsControl(car) OrElse car.ToString = "," OrElse car.ToString = " " Then
                            MultiUser = True
                            RdpUser.ArrayUtente.Add(Utente)
                            Utente = ""
                        Else
                            Utente += car
                            If c = .Cells("UsernameCol").Value.ToString.Length - 1 AndAlso MultiUser = True Then
                                RdpUser.ArrayUtente.Add(Utente)
                            End If
                        End If
                    Next
                    If MultiUser = True Then
                        For c = 0 To .Cells("PasswordCol").Value.ToString.Length - 1
                            Dim car As Char = .Cells("PasswordCol").Value.ToString.Chars(c)
                            If Char.IsControl(car) OrElse car.ToString = "," OrElse car.ToString = " " Then
                                RdpUser.ArrayPwd.Add(Pwd)
                                Pwd = ""
                            Else
                                Pwd += car
                                If c = .Cells("PasswordCol").Value.ToString.Length - 1 Then
                                    RdpUser.ArrayPwd.Add(Pwd)
                                End If
                            End If
                        Next

                        RdpUser.LblTitolo.Text = "Seleziona utenza con cui collegarsi a" & vbCrLf & .Cells("ProdottiCol").Value.ToString
                        RdpUser.NrRiga = x
                        RdpUser.ShowDialog(Me)
                    End If

                    Dim Terminal As String
                    If x = 0 Then
                        Terminal = "Terminal.rdp"
                    Else
                        Terminal = ("Terminal" & x & ".rdp")
                    End If

                    File.Delete(Application.StartupPath & "\" & Terminal)

                    RDP.CreaBatxRdp(.Cells("UsernameCol").Value.ToString, .Cells("PasswordCol").Value.ToString, .Cells("IpCol").Value.ToString, .Cells("DominioCol").Value.ToString, Terminal)

                    If File.Exists(Application.StartupPath & "\" & Terminal) Then
                        'Se ho creato correttamente l'rdp con salvate user e pwd apro quello: 
                        System.Diagnostics.Process.Start(Application.StartupPath & "\" & Terminal)
                    Else
                        'Altrimenti qualcosa è andato storto e apro comunque il terminal col prompt della password:
                        Dim p As New ProcessStartInfo

                        p.FileName = "mstsc.exe"

                        p.Arguments = "/v:" & """" & .Cells("IpCol").Value.ToString & """"

                        Process.Start(p)
                    End If
                Else
                    Dim p As New ProcessStartInfo

                    p.FileName = "mstsc.exe"

                    p.Arguments = "/v:" & """" & .Cells("IpCol").Value.ToString & """"

                    Process.Start(p)
                End If
            End With
        Next
    End Sub
    Private Sub CmdPutty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdPutty.Click
        If File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Putty\Putty.exe") = False Then
            MsgBox("Putty non presente nel sistema, installarlo per connettersi", MsgBoxStyle.Information)
            Exit Sub
        End If
        'Controllo se è spuntato Putty Manuale:
        If ChkPutty.Checked = True Then
            Dim p As New ProcessStartInfo
            p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Putty\Putty.exe"
            Process.Start(p)
            ChkPutty.Checked = False
            Exit Sub
        End If

        If GrigliaServer.SelectedRows.Count = 0 Then
            MsgBox("Selezionare le righe relative ai server a cui ci si vuole collegare", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim x As Byte

        For x = 0 To GrigliaServer.SelectedRows.Count - 1
            With GrigliaServer.SelectedRows(x)
                'USER E PWD NON INSERITI:
                If .Cells("UsernameCol").Value.ToString = "" OrElse .Cells("PasswordCol").Value.ToString = "" Then
                    MsgBox("Uno o più server non hanno User e Password salvati sul DB, aggiornarli per connettersi automaticamente." & vbCrLf & _
                    "Per questi verrà aperta la connessione con la richiesta di credenziali", MsgBoxStyle.Information)
                    Exit For
                End If
            End With
        Next

        For x = 0 To GrigliaServer.SelectedRows.Count - 1
            With GrigliaServer.SelectedRows(x)
                Dim p As New ProcessStartInfo

                p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Putty\Putty.exe"
                'USER E PWD INSERITI:
                If .Cells("UsernameCol").Value.ToString <> "" AndAlso .Cells("PasswordCol").Value.ToString <> "" Then
                    Dim Utente As String = ""
                    Dim Pwd As String = ""
                    Dim MultiUser As Boolean = False
                    For c = 0 To .Cells("UsernameCol").Value.ToString.Length - 1
                        Dim car As Char = .Cells("UsernameCol").Value.ToString.Chars(c)
                        If Char.IsControl(car) OrElse car.ToString = "," OrElse car.ToString = " " Then
                            MultiUser = True
                            RdpUser.ArrayUtente.Add(Utente)
                            Utente = ""
                        Else
                            Utente += car
                            If c = .Cells("UsernameCol").Value.ToString.Length - 1 AndAlso MultiUser = True Then
                                RdpUser.ArrayUtente.Add(Utente)
                            End If
                        End If
                    Next
                    If MultiUser = True Then
                        For c = 0 To .Cells("PasswordCol").Value.ToString.Length - 1
                            Dim car As Char = .Cells("PasswordCol").Value.ToString.Chars(c)
                            If Char.IsControl(car) OrElse car.ToString = "," OrElse car.ToString = " " Then
                                RdpUser.ArrayPwd.Add(Pwd)
                                Pwd = ""
                            Else
                                Pwd += car
                                If c = .Cells("PasswordCol").Value.ToString.Length - 1 Then
                                    RdpUser.ArrayPwd.Add(Pwd)
                                End If
                            End If
                        Next

                        RdpUser.LblTitolo.Text = "Seleziona utenza con cui collegarsi a" & vbCrLf & .Cells("ProdottiCol").Value.ToString
                        RdpUser.NrRiga = x
                        RdpUser.ShowDialog(Me)
                    End If

                    p.Arguments = .Cells("UsernameCol").Value.ToString & "@" & .Cells("IpCol").Value.ToString & " -pw " & .Cells("PasswordCol").Value.ToString
                Else
                    p.Arguments = .Cells("IpCol").Value.ToString
                End If

                Process.Start(p)
            End With
        Next
    End Sub
    Private Sub CmdWinScp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdWinScp.Click
        'Controllo se è spuntato WinScp Manuale:
        If ChkWinScp.Checked = True Then
            Dim p As New ProcessStartInfo
            p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\WinSCP\WinSCP.exe"
            Process.Start(p)
            ChkWinScp.Checked = False
            Exit Sub
        End If

        If GrigliaServer.SelectedRows.Count = 0 Then
            MsgBox("Selezionare le righe relative ai server a cui ci si vuole collegare", MsgBoxStyle.Information)
            Exit Sub
        End If
        If File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\WinScp\WinScp.exe") = False Then
            MsgBox("WinScp non presente nel sistema, installarlo per connettersi", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim x As Byte

        For x = 0 To GrigliaServer.SelectedRows.Count - 1
            With GrigliaServer.SelectedRows(x)
                'USER E PWD INSERITI:
                If .Cells("UsernameCol").Value.ToString = "" OrElse .Cells("PasswordCol").Value.ToString = "" Then
                    MsgBox("Uno o più server non hanno User e Password salvati sul DB, aggiornarli per connettersi automaticamente." & vbCrLf & _
                    "Per questi verrà aperta la connessione con la richiesta di credenziali", MsgBoxStyle.Information)
                    Exit For
                End If
            End With
        Next

        For x = 0 To GrigliaServer.SelectedRows.Count - 1
            With GrigliaServer.SelectedRows(x)
                Dim p As New ProcessStartInfo

                p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\WinScp\WinScp.exe"
                'USER E PWD INSERITI:
                If .Cells("UsernameCol").Value.ToString <> "" AndAlso .Cells("PasswordCol").Value.ToString <> "" Then
                    p.Arguments = .Cells("UsernameCol").Value.ToString & ":" & .Cells("PasswordCol").Value.ToString & "@" & .Cells("IpCol").Value.ToString
                Else
                    p.Arguments = .Cells("IpCol").Value.ToString
                End If

                Process.Start(p)
            End With
        Next
    End Sub
    Sub TnsNames(ByVal Id As Long)
        'Distinguo per trovare la home oracle corretta:
        Dim OraHome As String = ""
        If Directory.Exists("C:\oracle\product\11.2.0\client_1\network\admin") Then
            OraHome = "C:\oracle\product\11.2.0\client_1"
        ElseIf Directory.Exists("C:\ora81") Then
            OraHome = "C:\ora81"
        End If

        strsql = "select * from tnsnames where idimpianto=" & Id
        Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If Rstdb.RecordCount <> 0 Then
            Using sw As StreamWriter = File.CreateText(OraHome & "\Network\Admin\tnsnames.ora")
                While Rstdb.EOF <> True
                    If Rstdb("Rac").Value.ToString = "" Then
                        'NON E' UN RAC:
                        sw.WriteLine(vbCrLf & Rstdb("nometns").Value.ToString & " =" & vbCrLf & _
                        "  (DESCRIPTION =" & vbCrLf & _
                         "    (ADDRESS_LIST =" & vbCrLf & _
                         "      (ADDRESS = (PROTOCOL = TCP)(HOST = " & Rstdb("iptns").Value.ToString & ")(PORT = " & Rstdb("portatns").Value.ToString & "))" & vbCrLf & _
                         "    )" & vbCrLf & _
                         "    (CONNECT_DATA =" & vbCrLf & _
                         "      (SID = " & Rstdb("sidtns").Value.ToString & ")" & vbCrLf & _
                         "    )" & vbCrLf & _
                         "  )" & vbCrLf & vbCrLf & _
                         "")
                    Else
                        'E' UN RAC:
                        sw.WriteLine(vbCrLf & Rstdb("nometns").Value.ToString & " =" & vbCrLf & _
                         "  (DESCRIPTION =" & vbCrLf & _
                         "      (ADDRESS = (PROTOCOL = TCP)(HOST = " & Rstdb("IpRac1").Value.ToString & ")(PORT = " & Rstdb("portatns").Value.ToString & "))" & vbCrLf & _
                         "      (ADDRESS = (PROTOCOL = TCP)(HOST = " & Rstdb("IpRac2").Value.ToString & ")(PORT = " & Rstdb("portatns").Value.ToString & "))")

                        If Rstdb("Failover").Value.ToString <> "" Then
                            sw.WriteLine("      (FAILOVER = " & Rstdb("Failover").Value.ToString & ")" & vbCrLf)
                        End If

                        sw.WriteLine("      (LOAD_BALANCE = " & Rstdb("Load_Balance").Value.ToString & ")" & vbCrLf & _
                         "    (CONNECT_DATA =" & vbCrLf & _
                         "      (SERVER = " & Rstdb("RacServer").Value.ToString & ")" & vbCrLf & _
                         "      (SERVICE_NAME = " & Rstdb("Service_Name").Value.ToString & ")" & vbCrLf & _
                         "      (FAILOVER_MODE =  " & vbCrLf & _
                         "          (TYPE = " & Rstdb("Failover_Type").Value.ToString & ")" & vbCrLf & _
                         "          (METHOD = " & Rstdb("Failover_Method").Value.ToString & ")" & vbCrLf & _
                         "          (RETRIES = " & Rstdb("Failover_Retries").Value.ToString & ")" & vbCrLf & _
                         "          (DELAY = " & Rstdb("Failover_Delay").Value.ToString & ")" & vbCrLf & _
                         "    )" & vbCrLf & _
                         "   )" & vbCrLf & _
                         "  )" & vbCrLf & vbCrLf & _
                         "")
                    End If
                    Rstdb.MoveNext()
                End While

                sw.Close()
                sw.Dispose()
            End Using
        End If
        Rstdb.Close()
    End Sub
    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'Non ci sono user e pwd:
        If TxtUser.Text = "" AndAlso TxtPwd.Text = "" Then Exit Sub

        If TxtUser.Text <> "" Then
            SendKeys.Send(TxtUser.Text & "{TAB}" & TxtPwd.Text & "{TAB}{ENTER}")
        Else
            SendKeys.Send(TxtPwd.Text & "{TAB}{ENTER}")
        End If

        Timer2.Enabled = False
        Timer1.Enabled = False
    End Sub
    Private Sub TimerSkynet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerSkynet.Tick
        Dim Rstdb3 As New ADODB.Recordset
        Dim User, Pwd As String

        strsql = "select * from prodotti where id_riferimento=" & IdRiferimento & " and nome='Sistema di Monitoraggio'"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        User = Rstdb3("Utente").Value
        Pwd = Rstdb3("Pwd").Value

        Rstdb3.Close()

        Threading.Thread.Sleep(3000)

        If Control.IsKeyLocked(Keys.CapsLock) Then
            SendKeys.Send("+(" & User & "){TAB}+(" & Pwd & "){TAB}{ENTER}")
        Else
            SendKeys.Send(User & "{TAB}" & Pwd & "{TAB}{ENTER}")
        End If
        'SendKeys.Send("12345") ad esempio per la OPEN...
        TimerSkynet.Enabled = False
        Timer1.Enabled = False
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Process.Start(Web.Url.ToString)
    End Sub
    Private Sub CmdTicket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdTicket.Click
        GrigliaTicket.Hide()
        LblTicket.Show()
        LblTicket.Text = "Caricamento Ticket in corso ..."
        PrgTicket.Show()

        Try
            If Lavora Is Nothing = False Then
                If Lavora.ThreadState = Threading.ThreadState.Running Then
                    Exit Sub
                End If
                If Lavora.IsAlive Then
                    Lavora.Abort(Lavora)
                    Lavora = Nothing
                End If
                Lavora = Nothing
            End If

            Lavora = New System.Threading.Thread(AddressOf CaricaTicket)
            Lavora.Priority = Threading.ThreadPriority.Normal
            Lavora.Start()
        Catch ex As Exception

        End Try
    End Sub
    Sub CaricaTicket()
        Try
            'Se non c'è connessione di rete esco:
            If My.Computer.Network.IsAvailable = False Then Exit Sub

            Dim Rstdb3 As New ADODB.Recordset

            'Cancello subito i ticket se no se fallisce la connessione ad hda vedo quelli di prima:
            strsql = "delete from ticket"
            Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            'Se non raggiungo Hda avviso ed esco:
            If My.Computer.Network.Ping("noemanas") = False Then
                LblTicket.Invoke(Frm1DelegataXCambio, "NoHda")
                Exit Sub
            End If

            strsql = "select * from trasc where idimpianto=" & IdImpianto
            Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            Dim IdHda As String = ""
            If Rstdb3.RecordCount <> 0 Then
                IdHda = Rstdb3("IdHda").Value.ToString
            End If

            Rstdb3.Close()

            'Carico solo se c'è la transcodifica di HDA:
            If IdHda = "" Then
                LblTicket.Invoke(Frm1DelegataXCambio, "NoTrans")
                Exit Sub
            End If

            Dim Conn As SqlConnection = New SqlConnection("server=hda.noemalife.loc;uid=helpdesk;pwd=noema_hd;database=HDA")
            Dim myCommand As SqlCommand
            Dim SqlDr As SqlDataReader

            'SELECT PER GLI ALLEGATI:
            '      Select a.IDProtocollo
            '    ,a.FileName
            '    ,a.RealName      
            '    ,c.IDAllegato
            '    ,c.IDChiamata
            'FROM TABAllegati a, TABChiamateAllegati c
            'where a.IDProtocollo=c.IDAllegato
            'and c.IDChiamata='219031D';

            Conn.Open()
            myCommand = New SqlCommand("select idprotocollo,idforncli,riferimento,domanda,rispostadettaglio,data,destinatario,idstato " & _
                                       "from tabchiamate where idforncli='" & IdHda & "' and idstato not in ('S5') order by data desc", Conn)
            SqlDr = myCommand.ExecuteReader

            strsql = "select * from ticket"
            Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)

            Dim NrTicket As Short
            While SqlDr.Read
                Rstdb3.AddNew()
                Rstdb3("idProtocollo").Value = SqlDr("idProtocollo")
                Rstdb3("idforncli").Value = SqlDr("idforncli")
                Rstdb3("riferimento").Value = SqlDr("riferimento")
                Rstdb3("domanda").Value = SqlDr("domanda")
                Rstdb3("rispostadettaglio").Value = SqlDr("rispostadettaglio")
                Rstdb3("data").Value = SqlDr("data")
                Rstdb3("destinatario").Value = SqlDr("destinatario")

                If SqlDr("idstato") = "S7D" Then
                    Rstdb3("idstato").Value = "Risolto"
                ElseIf SqlDr("idstato") = "S1D" Then
                    Rstdb3("idstato").Value = "Inserito"
                ElseIf SqlDr("idstato") = "S2" Then
                    Rstdb3("idstato").Value = "In corso"
                ElseIf SqlDr("idstato") = "S4D" Then
                    Rstdb3("idstato").Value = "Sospeso"
                End If

                Rstdb3.Update()
                NrTicket += 1
            End While

            Rstdb3.Requery()
            SqlDr.Close()
            Conn.Close()
            Rstdb3.Close()

            LblTicket.Invoke(Frm1DelegataXCambio, "OkHda")
            Tab.TabPages("TabTicket").Invoke(Frm1DelegataXCambio, CType(NrTicket, String))

        Catch ex As Exception
            LblTicket.Invoke(Frm1DelegataXCambio, "NoHda")
        End Try
    End Sub
    Delegate Function PerCambioTextBox(ByVal S As String) As String 'Dichiaro un tipo di funziuone delegata che vuole un parametro stringa e 
    'restituisce un parametro stringa (i due tipi sono uguali in quest'esempio,ma non è assolutamente necessario che lo siano)
    Public Frm1DelegataXCambio As New PerCambioTextBox(AddressOf LeggiECambiaTextBox1) 'Istanzio una funzione delegata del tipo appena dichiarato
    ' e che ha come compito quello di eseguire la funzione LeggiECambiaTextBox1. E' obbliagtorio che la procedura a cui si punta, abbia uguale firma
    ' rispetto al tipo di delegata che si usa.    

    Public Function LeggiECambiaTextBox1(ByVal NuovoTesto As String) As String
        'Memorizza il valore attuale della proprietà TEXT di Textbox1 e lo restituisce al ritorno dalla funzione.
        'Prima di tornare, assegna il nuovo valore a textbox1
        If NuovoTesto = "NoHda" Then
            LblTicket.Text = "Impossibile raggiungere Hda per il recupero dei ticket"
            PrgTicket.Hide()
        ElseIf NuovoTesto = "OkHda" Then
            LblTicket.Hide()
            PrgTicket.Hide()
            Me.TicketTableAdapter.Fill(Me.GestioneHDDataSet.Ticket)
            GrigliaTicket.Show()
        ElseIf NuovoTesto = "NoTrans" Then
            LblTicket.Text = "Non esiste la Transcodifica di Hda per l'impianto selezionato"
            PrgTicket.Hide()
        Else
            Tab.TabPages("TabTicket").Text = "Ticket HDA " & "(" & NuovoTesto & ")"
            PrgTicket.Hide()
        End If

        Return NuovoTesto 'Torna restituendo il testo che era contenuto in textbox1
    End Function
    Private Sub TabTicket_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabTicket.Enter
        'PROVO A FAR CARICARE I TICKET DIRETTAMENTE DALLA CMB DEGLI IMPIANTI QUANDO VIENE SCELTO
        'PERCHE' SE NO SE SI PERDE LA RETE DOPO NON SI CARICANO PIU', QUINDI COMMENTO QUESTA PARTE:

        'Lo faccio solo se si è cambiato impianto se no sono nello stesso e non devo più cercare i ticket:
        'If CheckTicket = True Then Exit Sub

        'GrigliaTicket.Hide()
        'LblTicket.Show()
        'LblTicket.Text = "Caricamento Ticket in corso ..."
        'PrgTicket.Show()

        'If Lavora Is Nothing = False Then
        '    If Lavora.ThreadState = Threading.ThreadState.Running Then
        '        Exit Sub
        '    End If
        '    If Lavora.IsAlive Then
        '        Lavora.Abort(Lavora)
        '        Lavora = Nothing
        '    End If
        '    Lavora = Nothing
        'End If

        'Lavora = New System.Threading.Thread(AddressOf CaricaTicket)
        'Lavora.Priority = Threading.ThreadPriority.Normal
        'Lavora.Start()

        'CheckTicket = True
    End Sub
    Private Sub AggiornaMdbDaHda(ByVal sender As System.Object, ByVal e As System.EventArgs)
        strsql = "select * from trasc"
        Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockPessimistic)

        Dim Conn As SqlConnection = New SqlConnection("server=serverhda.noemalife.loc;uid=helpdesk;pwd=noema_hd;database=HDA")
        Dim myCommand As SqlCommand
        Dim SqlDr As SqlDataReader

        Conn.Open()

        While Rstdb.EOF <> True
            myCommand = New SqlCommand("select count(*) from tabclienti where active=1 and cognome='" & Replace(Rstdb("Nomeimpianto").Value, "'", "''") & "'", Conn)
            Dim NrRec As Byte = myCommand.ExecuteScalar

            If NrRec > 0 Then
                myCommand = New SqlCommand("select idcliente,cognome from tabclienti where active=1 and cognome='" & Replace(Rstdb("Nomeimpianto").Value, "'", "''") & "'", Conn)
                SqlDr = myCommand.ExecuteReader

                SqlDr.Read()
                Rstdb("IdHda").Value = SqlDr("IdCliente")
                Rstdb("NomeHda").Value = SqlDr("Cognome")

                Rstdb.Update()
                SqlDr.Close()
            End If

            Rstdb.MoveNext()
        End While
    End Sub
    Private Sub CmdImportaSingolo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdImportaSingolo.Click
        If IdRiferimento = 0 Then
            MsgBox("Selezionare l'impianto da cui importare la scheda clienti", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim PathSchede As String = "\\noemanas\SchedeClienti\"

        If My.Computer.Network.Ping("Noemanas") = False Then
            MsgBox("Impossibile raggiungere Noemanas per l'importazione della scheda cliente", MsgBoxStyle.Information)
            Exit Sub
        End If
        'IMPORTO DIRETTAMENTE SUL DB SUL SERVER:
        Dim CnndbHDA As New ADODB.Connection
        Dim StrconnectHDA As String = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=\\noemanas\CustomerService\gestionehd\database\DatiGestionehd.mdb;Persist Security Info=True;Jet OLEDB:Database Password=" & db_pass & ";"

        CnndbHDA.Open(StrconnectHDA)

        Dim objServiceManager, objCoreReflection, objDesktop, myDoc, allSheets, mySheet, mySheet2 As Object

        objServiceManager = CreateObject("com.sun.star.ServiceManager")
        'Create the CoreReflection service that is later used to create structs
        objCoreReflection = objServiceManager.createInstance("com.sun.star.reflection.CoreReflection")
        'Create the Desktop
        objDesktop = objServiceManager.createInstance("com.sun.star.frame.Desktop")

        strsql = "delete from server where Id_Riferimento=" & IdRiferimento
        Rstdb3.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from integrazioni where Id_Riferimento=" & IdRiferimento
        Rstdb3.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from prodotti where Id_Riferimento=" & IdRiferimento
        Rstdb3.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from leggimi where Id_Riferimento=" & IdRiferimento
        Rstdb3.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from PwdVnc where IdRiferimento=" & IdRiferimento
        Rstdb3.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "delete from Rubrica where Id_Riferimento=" & IdRiferimento
        Rstdb3.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        strsql = "select * from impianti where IdRiferimento=" & IdRiferimento
        Rstdb3.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        Try
            'CI METTO L'IDRIFERIMENTO VISTO CHE LE SCHEDE CLIENTI SONO COMUNI PER ASL:            
            IdRiferimento = Rstdb3("idriferimento").Value
            strsql = "select nomeriferimento from riferimento_impianti where IdRiferimento=" & Rstdb3("idriferimento").Value
            Rstdb.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            Dim Impianto As String = Rstdb("nomeriferimento").Value
            
            Rstdb.Close()

            Dim Path As String = ""

            If Directory.Exists(PathSchede & Impianto & "\") Then
                Dim SchedaDir As New DirectoryInfo(PathSchede & Impianto & "\")
                Dim FileList As New ArrayList(SchedaDir.GetFiles())
                Dim b As Short
                For b = 0 To FileList.Count - 1
                    'CASO DEL LUM  E DEL S.ORSOLA:
                    If IdImpianto = 17 OrElse IdImpianto = 25 Then
                        If IdImpianto = 17 Then
                            If FileList.Item(b).ToString().Contains("AUSL") AndAlso FileList.Item(b).ToString().Contains("Scheda") AndAlso FileList.Item(b).ToString().Contains("Old") = False AndAlso FileList.Item(b).ToString().EndsWith("xls") Then
                                Path = SchedaDir.FullName & FileList.Item(b).ToString()
                                Exit For
                            End If
                        Else
                            If FileList.Item(b).ToString().Contains("AOSP") AndAlso FileList.Item(b).ToString().Contains("Scheda") AndAlso FileList.Item(b).ToString().Contains("Old") = False AndAlso FileList.Item(b).ToString().EndsWith("xls") Then
                                Path = SchedaDir.FullName & FileList.Item(b).ToString()
                                Exit For
                            End If
                        End If
                    Else
                        If FileList.Item(b).ToString().Contains("Scheda") AndAlso FileList.Item(b).ToString().Contains("Old") = False AndAlso FileList.Item(b).ToString().EndsWith("xls") Then
                            Path = SchedaDir.FullName & FileList.Item(b).ToString()
                            Exit For
                        End If
                    End If
                Next
            Else
                'LOGGO QUELLE DI CUI NON TROVO LA SCHEDA:
                Using sw As StreamWriter = File.AppendText(My.Application.Info.DirectoryPath & "\Importa_Error " & Replace(CStr(Today.Date), "/", "-") & ".txt")

                    sw.WriteLine(vbCrLf & Impianto)

                    sw.Close()
                End Using
            End If
            Try
                If Path <> "" Then
                    'SE CON QUELL'IDRIFERIMENTO C'E' SCRITTO QUALCOSA IN SERVER ALLORA SUPPONGO CI SIANO GIA'
                    'I DATI INSERITI PER L'IMPIANTO E QUINDI NON RISCRIVO SERVER, PRODOTTI, ECC.:
                    strsql = "select * from server where id_riferimento=" & IdRiferimento
                    Rstdb.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                    If Rstdb.RecordCount = 0 Then
                        Rstdb.Close()

                        Dim args(-1) As Object
                        myDoc = objDesktop.loadComponentFromURL("file:///" & Path, "_blank", 0, args)

                        allSheets = myDoc.Sheets
                        mySheet = allSheets.getByName("Cosa e Dove")

                        'IMPORTO I SERVER
                        strsql = "select * from server"
                        Rstdb.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                        Dim x, y, z As Short
                        Dim Backup As Boolean = False
                        Dim Test As Boolean = False
                        For x = 4 To 150
                            Dim Cell As String = mySheet.getCellRangeByName("C" & x).String
                            'MI DEVO PRENDERE DOVE VENGONO SCRITTI I PRODOTTI
                            If Cell.ToLower = "backup" Then
                                Backup = True
                                Test = False
                            End If
                            If Cell.ToLower = "test" Then
                                Backup = False
                                Test = True
                            End If

                            If Cell <> "" AndAlso Char.IsLetter(Cell, 0) <> True Then
                                'Scrivo sul DB:
                                With Rstdb
                                    .AddNew()
                                    .Fields("id_impianto").Value = IdImpianto
                                    .Fields("id_riferimento").Value = IdRiferimento
                                    .Fields("Ip").Value = Cell
                                    .Fields("Backup").Value = Backup
                                    .Fields("Test").Value = Test

                                    'Mi prendo i prodotti dalle colonne:
                                    Dim Prodotti As String = ""
                                    Dim ColY As Byte
                                    'In alcuni fogli son scritti nella 4 in altri nella 2, quindi devo mettere una variabile:

                                    If mySheet.getCellByPosition(4, 1).String <> "" Then
                                        ColY = 1
                                    ElseIf mySheet.getCellByPosition(4, 2).String <> "" Then
                                        ColY = 2
                                    ElseIf mySheet.getCellByPosition(4, 3).String <> "" Then
                                        ColY = 3
                                    End If

                                    For y = 4 To 50
                                        Dim Col As String = mySheet.getCellByPosition(y, x - 1).String
                                        If Col.ToLower = "x" Then Prodotti += mySheet.getCellByPosition(y, ColY).String & ", "
                                    Next

                                    'If Prodotti.EndsWith(",") Then Prodotti = Prodotti.Remove(3, 5)

                                    .Fields("Prodotti").Value = Prodotti

                                    'VADO A PRENDERMI I DATI DAL FOGLIO MACCHINE:
                                    mySheet2 = allSheets.getByName("Macchine")

                                    For z = 4 To 150
                                        If Cell = mySheet2.getCellRangeByName("C" & z).String Then
                                            Rstdb("Alias").Value = mySheet2.getCellRangeByName("D" & z).String
                                            If mySheet2.getCellRangeByName("G1").String.ToString.Contains("Mac") OrElse mySheet2.getCellRangeByName("G2").String.ToString.Contains("Mac") Then
                                                'Controllo se c'è la colonna macchina virtuale per il numero di col:
                                                Rstdb("SO").Value = mySheet2.getCellRangeByName("H" & z).String
                                                Rstdb("Dominio").Value = mySheet2.getCellRangeByName("I" & z).String
                                                Rstdb("Utente").Value = mySheet2.getCellRangeByName("J" & z).String
                                                Rstdb("Pwd").Value = mySheet2.getCellRangeByName("K" & z).String

                                                If mySheet2.getCellRangeByName("L" & z).String.ToString.ToLower = "x" Then
                                                    Rstdb("Rdp").Value = True
                                                Else
                                                    Rstdb("Rdp").Value = False
                                                End If
                                                If mySheet2.getCellRangeByName("M" & z).String.ToString.ToLower = "x" Then
                                                    Rstdb("Putty").Value = True
                                                Else
                                                    Rstdb("Putty").Value = False
                                                End If
                                                Rstdb("Note").Value = mySheet2.getCellRangeByName("R" & z).String
                                            Else
                                                Rstdb("SO").Value = mySheet2.getCellRangeByName("G" & z).String
                                                Rstdb("Dominio").Value = mySheet2.getCellRangeByName("H" & z).String
                                                Rstdb("Utente").Value = mySheet2.getCellRangeByName("I" & z).String
                                                Rstdb("Pwd").Value = mySheet2.getCellRangeByName("J" & z).String

                                                If mySheet2.getCellRangeByName("K" & z).String.ToString.ToLower = "x" Then
                                                    Rstdb("Rdp").Value = True
                                                Else
                                                    Rstdb("Rdp").Value = False
                                                End If
                                                If mySheet2.getCellRangeByName("L" & z).String.ToString.ToLower = "x" Then
                                                    Rstdb("Putty").Value = True
                                                Else
                                                    Rstdb("Putty").Value = False
                                                End If
                                                Rstdb("Note").Value = mySheet2.getCellRangeByName("Q" & z).String
                                            End If
                                            'PROVO A PRENDERE SU LE PWD DI VNC:
                                            Dim ColVnc As String = ""
                                            If mySheet2.getCellRangeByName("O1").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("O2").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("O3").String.ToString.ToLower = "vnc" Then
                                                ColVnc = "O"
                                            ElseIf mySheet2.getCellRangeByName("P1").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("P2").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("P3").String.ToString.ToLower = "vnc" Then
                                                ColVnc = "P"
                                            ElseIf mySheet2.getCellRangeByName("N1").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("N2").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("N3").String.ToString.ToLower = "vnc" Then
                                                ColVnc = "N"
                                            ElseIf mySheet2.getCellRangeByName("Q1").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("Q2").String.ToString.ToLower = "vnc" OrElse mySheet2.getCellRangeByName("Q3").String.ToString.ToLower = "vnc" Then
                                                ColVnc = "Q"
                                            End If
                                            If ColVnc <> "" Then
                                                If mySheet2.getCellRangeByName(ColVnc & z).String.ToString <> "" Then
                                                    Dim RstdbVnc As New ADODB.Recordset
                                                    Dim StrVnc As String = "select * from PwdVnc"
                                                    RstdbVnc.Open(StrVnc, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                                                    RstdbVnc.AddNew()
                                                    RstdbVnc("IdImpianto").Value = IdImpianto
                                                    RstdbVnc("IdRiferimento").Value = IdRiferimento
                                                    RstdbVnc("PwdVnc").Value = mySheet2.getCellRangeByName(ColVnc & z).String.ToString
                                                    RstdbVnc.Update()

                                                    RstdbVnc.Close()
                                                    'SPUNTO QUI CHE C'E' VNC:
                                                    Rstdb("Vnc").Value = True
                                                    'TODO: RIVEDERE! SCRIVO LA PWDVNC NELLA RIGA DEL SERVER PERCHE' MAGARI RELATIVA A LUI:
                                                    Rstdb("PwdVnc").Value = mySheet2.getCellRangeByName(ColVnc & z).String.ToString
                                                End If
                                            End If
                                        End If
                                    Next

                                    Rstdb.Update()
                                End With
                            End If
                            'QUESTO LO USO PER USCIRE PERCHè SE TROVO TRE RIGHE VUOTE ALLORA NON CI SONO PIU' SERVER... SPERO!
                            With mySheet
                                If Cell = "" AndAlso mySheet.getCellRangeByName("C" & x + 1).String = "" AndAlso mySheet.getCellRangeByName("C" & x + 2).String = "" Then
                                    Exit For
                                End If
                            End With
                        Next

                        Rstdb.Close()

                        'VADO A PRENDERE I DATI DAL FOGLIO LEGGIMI:
                        strsql = "select * from leggimi"
                        Rstdb.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                        mySheet = allSheets.getByName("Leggimi")

                        Dim Testo As String = ""
                        Dim b As Byte
                        For b = 1 To 250
                            Dim Cell As String = mySheet.getCellRangeByName("B" & b).String
                            If Cell <> "" Then
                                Testo += Cell
                                Testo += " " & mySheet.getCellRangeByName("C" & b).String & " " & mySheet.getCellRangeByName("D" & b).String & " " & _
                                mySheet.getCellRangeByName("E" & b).String & vbCrLf
                            Else
                                Testo += vbCrLf
                            End If
                        Next

                        Rstdb.AddNew()
                        Rstdb("Id_Impianto").Value = IdImpianto
                        Rstdb("Id_Riferimento").Value = IdRiferimento
                        Rstdb("Testo").Value = Testo
                        Rstdb.Update()

                        Rstdb.Close()

                        'VADO A PRENDERE I DATI DAL FOGLIO INTEGRAZIONI:
                        strsql = "select * from integrazioni"
                        Rstdb.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                        If IdImpianto = 109 OrElse IdImpianto = 108 Then
                            mySheet = allSheets.getByName("Integrazioni Rimini")
                        ElseIf IdImpianto = 106 OrElse IdImpianto = 51 OrElse IdImpianto = 75 Then
                            mySheet = allSheets.getByName("Integrazioni RAFALU")
                        ElseIf IdImpianto = 160 Then
                            mySheet = allSheets.getByName("Integrazioni FORLI")
                        ElseIf IdImpianto = 251 OrElse IdImpianto = 246 Then
                            mySheet = allSheets.getByName("Integrazioni CESENA")
                        Else
                            mySheet = allSheets.getByName("Integrazioni")
                        End If

                        For b = 4 To 250
                            Dim Cell As String = mySheet.getCellRangeByName("B" & b).String
                            If Cell <> "" Then
                                Rstdb.AddNew()
                                Rstdb("Id_Impianto").Value = IdImpianto
                                Rstdb("Id_Riferimento").Value = IdRiferimento
                                Rstdb("Nome").Value = Cell
                                Rstdb("Modo").Value = mySheet.getCellRangeByName("C" & b).String
                                Rstdb("Documentazione").Value = mySheet.getCellRangeByName("D" & b).String
                                Rstdb.Update()
                            End If
                        Next

                        Rstdb.Close()

                        'VADO A PRENDERE I DATI DAL FOGLIO PRODOTTI:
                        Dim Rstdb4 As New ADODB.Recordset
                        strsql = "select * from prodotti"
                        Rstdb4.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                        mySheet = allSheets.getByName("Prodotti")
                        Dim Prodotto As Boolean = False
                        Try 'Metto in un try i prodotti perchè in alcune schede user e pwd sono scritte in G e H invece che F  e G
                            For b = 4 To 250
                                Dim Cell As String = mySheet.getCellRangeByName("C" & b).String
                                If Cell.ToLower = "prodotto" Then Prodotto = True
                                If Cell.ToLower <> "database" AndAlso Cell.ToLower <> "prodotto" AndAlso Cell.ToLower <> "backup" AndAlso Cell <> "" Then
                                    If Cell.ToLower = "backup" Then Exit For
                                    If Prodotto = False Then
                                        Dim c As Byte
                                        Dim ArrayUtente As New ArrayList
                                        Dim ArrayPwd As New ArrayList
                                        Dim Utente As String = ""
                                        Dim Pwd As String = ""
                                        If mySheet.getCellRangeByName("F" & b).String.ToString <> "" Then
                                            For c = 0 To mySheet.getCellRangeByName("F" & b).String.ToString.Length - 1
                                                Dim car As Char = mySheet.getCellRangeByName("F" & b).String.Chars(c)
                                                If Char.IsWhiteSpace(car) = False Then
                                                    Utente += car
                                                Else
                                                    ArrayUtente.Add(Utente)
                                                    Utente = ""
                                                End If
                                                If c = mySheet.getCellRangeByName("F" & b).String.ToString.Length - 1 Then ArrayUtente.Add(Utente)
                                            Next
                                            For c = 0 To mySheet.getCellRangeByName("G" & b).String.ToString.Length - 1
                                                Dim car As Char = mySheet.getCellRangeByName("G" & b).String.Chars(c)
                                                If Char.IsWhiteSpace(car) = False Then
                                                    Pwd += car
                                                Else
                                                    ArrayPwd.Add(Pwd)
                                                    Pwd = ""
                                                End If
                                                If c = mySheet.getCellRangeByName("G" & b).String.ToString.Length - 1 Then ArrayPwd.Add(Pwd)
                                            Next

                                            'Inserisco tante righe quante sono le utenze:
                                            For c = 0 To ArrayUtente.Count - 1
                                                Rstdb4.AddNew()
                                                Rstdb4("Id_Impianto").Value = IdImpianto
                                                Rstdb4("Id_Riferimento").Value = IdRiferimento
                                                Rstdb4("Nome").Value = Cell
                                                Rstdb4("Versione").Value = mySheet.getCellRangeByName("D" & b).String
                                                Rstdb4("SidPorta").Value = mySheet.getCellRangeByName("E" & b).String
                                                Rstdb4("Utente").Value = ArrayUtente(c).ToString
                                                'La pwd la metto in un try perchè a volte il nr. di utente non corrisponde:
                                                Try
                                                    Rstdb4("Pwd").Value = ArrayPwd(c).ToString
                                                Catch ex As Exception

                                                End Try
                                                Rstdb4("Path").Value = mySheet.getCellRangeByName("H" & b).String
                                                Rstdb4("Url").Value = mySheet.getCellRangeByName("I" & b).String
                                                Rstdb4("Comandi").Value = mySheet.getCellRangeByName("J" & b).String
                                                Rstdb4("Note").Value = mySheet.getCellRangeByName("K" & b).String
                                                Rstdb4.Update()
                                            Next
                                        End If
                                    Else 'SONO SOTTO PRODOTTO E NON CICLO PER USER E PWD:
                                        Rstdb4.AddNew()
                                        Rstdb4("Id_Impianto").Value = IdImpianto
                                        Rstdb4("Id_Riferimento").Value = IdRiferimento
                                        Rstdb4("Nome").Value = Cell
                                        Rstdb4("Versione").Value = mySheet.getCellRangeByName("D" & b).String
                                        Rstdb4("SidPorta").Value = mySheet.getCellRangeByName("E" & b).String
                                        Rstdb4("Utente").Value = mySheet.getCellRangeByName("F" & b).String
                                        Rstdb4("Pwd").Value = mySheet.getCellRangeByName("G" & b).String
                                        Rstdb4("Path").Value = mySheet.getCellRangeByName("H" & b).String
                                        Rstdb4("Url").Value = mySheet.getCellRangeByName("I" & b).String
                                        Rstdb4("Comandi").Value = mySheet.getCellRangeByName("J" & b).String
                                        Rstdb4("Note").Value = mySheet.getCellRangeByName("K" & b).String
                                        Rstdb4.Update()
                                    End If
                                End If
                            Next
                            Rstdb4.Close()

                            'VADO A PRENDERE I DATI DAL FOGLIO RUBRICA:
                            strsql = "select * from rubrica"
                            Rstdb4.Open(strsql, CnndbHDA, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                            mySheet = allSheets.getByName("Rubrica")

                            Dim Tipo As String = ""
                            For b = 4 To 250
                                Dim Cell As String = mySheet.getCellRangeByName("C" & b).String
                                If Cell <> "" Then
                                    'Bold è 150, normale è 100:
                                    If mySheet.getCellRangeByName("C" & b).CharWeight = 150 Then
                                        Tipo = mySheet.getCellRangeByName("C" & b).String
                                    Else
                                        Rstdb4.AddNew()
                                        Rstdb4("Id_Impianto").Value = IdImpianto
                                        Rstdb4("Id_Riferimento").Value = IdRiferimento
                                        Rstdb4("Tipo").Value = Tipo
                                        Rstdb4("Nome").Value = Cell
                                        Rstdb4("Sede").Value = mySheet.getCellRangeByName("E" & b).String
                                        Rstdb4("Fax").Value = mySheet.getCellRangeByName("F" & b).String
                                        Rstdb4("Telefono").Value = mySheet.getCellRangeByName("G" & b).String
                                        Rstdb4("Cellulare").Value = mySheet.getCellRangeByName("H" & b).String
                                        Rstdb4("Email").Value = mySheet.getCellRangeByName("I" & b).String
                                        Rstdb4("Competenze").Value = mySheet.getCellRangeByName("J" & b).String
                                        Rstdb4("Note").Value = mySheet.getCellRangeByName("K" & b).String

                                        Rstdb4.Update()
                                    End If
                                End If
                            Next

                            Rstdb4.Close()

                        Catch ex As Exception
                            Rstdb4 = Nothing
                        End Try
                        myDoc.close(True)
                    Else
                        Rstdb.Close() 'CHIUDO IL RECORDSET CON CUI HO CONTROLLATO SE GIA' SCRITTO IN TAB QUELL'IDRIFERIMENTO
                    End If
                End If
            Catch ex As Exception
                'LOGGO L'ERRORE:
                Using sw As StreamWriter = File.AppendText(My.Application.Info.DirectoryPath & "\XlsError " & Replace(CStr(Today.Date), "/", "-") & ".txt")

                    sw.WriteLine("Id Impianto: " & IdImpianto & " " & ex.Message.ToString)

                    sw.Close()
                End Using
            End Try
        Catch ex As Exception
            'LOGGO QUELLE DI CUI NON TROVO LA SCHEDA:
            Using sw As StreamWriter = File.AppendText(My.Application.Info.DirectoryPath & "\Importa_Error " & Replace(CStr(Today.Date), "/", "-") & ".txt")

                sw.WriteLine("Id Impianto: " & IdImpianto & " " & ex.Message.ToString)

                sw.Close()
            End Using
        End Try

        Rstdb3.Close()
        CnndbHDA.Close()
        MsgBox("Dati importati per l'impianto " & NomeImpianto, MsgBoxStyle.Information)
        Application.DoEvents()
        'Copio il DB dal server coi dati aggiornati, lo devo prima chiudere:
        Cnndb.Close()
        Aggiorna.ShowDialog(Me)
        Cnndb.Open(Strconnect)
        'Rifillo tutte le tab coi dati dell'impianto:
        Call CmbImpianti_SelectedValueChanged(CmdImportaSingolo, Nothing)

    End Sub
    Private Sub CmdSkynet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSkynet.Click
        Dim Rstdb3 As New ADODB.Recordset

        strsql = "select * from prodotti where id_riferimento=" & IdRiferimento & " and nome='Sistema di Monitoraggio'"
        Rstdb3.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        If Rstdb3.RecordCount = 0 Then
            'Non c'è il link sul db, avviso e non faccio nulla:
            MsgBox("Non è presente il link a Skynet sul database, aggiornarlo e riprovare", MsgBoxStyle.Information)
        ElseIf Rstdb3("Url").Value.ToString = "" Then
            'C'è il record sul db ma non c'è l'url:
            MsgBox("Non è presente il link a Skynet sul database, aggiornarlo e riprovare", MsgBoxStyle.Information)
        Else
            'C'è il record sul db ed è valorizzato, apro il link:        
            Timer1.Enabled = True

            Process.Start(Rstdb3("Url").Value.ToString)

            TimerSkynet.Enabled = True
        End If

        Rstdb3.Close()
    End Sub
    Private Sub TabProdotti_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabProdotti.Enter
        'Carico la lista degli script sql:
        Dim Ceta As New DirectoryInfo(Application.StartupPath & "\SqlScript\")
        Dim x As Long
        Dim Lista() As FileInfo
        Lista = Ceta.GetFiles

        LstSqlScript.Items.Clear()

        For x = 0 To Lista.Count - 1
            LstSqlScript.Items.Add(Lista(x).Name)
        Next

    End Sub
    Private Sub MiniatureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiniatureToolStripMenuItem.Click
        Try
            VmHD.Show()
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub GrigliaTicket_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrigliaTicket.CellClick
        strsql = "select * from ticket where idprotocollo='" & GrigliaTicket.Rows(e.RowIndex).Cells("IdProtocolloCol").Value.ToString() & "'"
        Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        TxtProblema.Text = Rstdb("Domanda").Value.ToString
        TxtSoluzione.Text = Rstdb("RispostaDettaglio").Value.ToString

        Rstdb.Close()
    End Sub
    Private Sub CmdScheda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdScheda.Click
        If IdRiferimento = 0 Then
            MsgBox("Selezionare l'impianto di cui aprire la scheda clienti", MsgBoxStyle.Information)
            Exit Sub
        End If
        Try
            If My.Computer.Network.Ping("noemanas") = False Then
                MsgBox("Noemanas non raggiungible, scollegarsi dalla VPN per aprire la scheda cliente", MsgBoxStyle.Information)
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Noemanas non raggiungible, scollegarsi dalla VPN per aprire la scheda cliente", MsgBoxStyle.Information)
            Exit Sub
        End Try
        
        Try
            strsql = "select * from riferimento_impianti where idriferimento=" & IdRiferimento
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            Dim Impianto As String = Rstdb("nomeriferimento").Value

            Rstdb.Close()

            Dim Path As String = ""

            If Directory.Exists("\\noemanas\schedeclienti\" & Impianto & "\") Then
                Dim SchedaDir As New DirectoryInfo("\\noemanas\schedeclienti\" & Impianto & "\")
                System.Diagnostics.Process.Start(SchedaDir.FullName)

                Dim FileList As New ArrayList(SchedaDir.GetFiles())
                Dim b As Short
                For b = 0 To FileList.Count - 1
                    If FileList.Item(b).ToString().Contains("Scheda") AndAlso FileList.Item(b).ToString().Contains("Old") = False AndAlso FileList.Item(b).ToString().EndsWith("xls") Then
                        Path = SchedaDir.FullName & FileList.Item(b).ToString()
                        Exit For
                    End If
                Next
            End If

            System.Diagnostics.Process.Start(Path)

        Catch ex As Exception
            MsgBox("Impossibile aprire la scheda clienti", MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub HowToToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HowToToolStripMenuItem.Click
        HowTo.Show()
    End Sub
    Private Sub ElencoMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ElencoMenu.Click, TurnistiMenu.Click, GruppoBMenu.Click, GruppoAMenu.Click
        If My.Computer.Name.ToUpper.StartsWith("VMHD") Then
            MsgBox("Non si può aprire una VM da una VM!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If sender.name = "GruppoAMenu" Then
            VMElenco.Elenco = "GruppoA"
        ElseIf sender.name = "GruppoBMenu" Then
            VMElenco.Elenco = "GruppoB"
            'ElseIf sender.name = "GruppoCMenu" Then
            '   VMElenco.Elenco = "GruppoC"
        ElseIf sender.name = "TurnistiMenu" Then
            VMElenco.Elenco = "Turnisti"
        ElseIf sender.name = "ElencoMenu" Then
            VMElenco.Elenco = "Elenco"
        End If

        VMElenco.Show()
    End Sub
    Private Sub CmdDna_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdDna.Click
        CraFileDnaDnLab()

        If File.Exists("C:\tools\dnutility\launcher.bat") Then
            Process.Start("C:\tools\dnutility\launcher.bat")
        Else
            MsgBox("Impossibile aprire Configuratore DNA, reinstallarlo", MsgBoxStyle.Information)
            Exit Sub
        End If
    End Sub
    Private Sub CmdDnlab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdDnLab.Click
        CraFileDnaDnLab()

        If File.Exists("C:\Dnlab\Main.exe") Then
            Process.Start("C:\Dnlab\Main.exe")
        Else
            MsgBox("Impossibile aprire DnLab, reinstallarlo", MsgBoxStyle.Information)
            Exit Sub
        End If
    End Sub
    Sub CraFileDnaDnLab()

        Try
            strsql = "select * from tnsnames where idimpianto=" & IdImpianto & " and dna='1'"
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            If Rstdb.RecordCount <> 0 Then
                Using SwDna As StreamWriter = File.CreateText("C:\Dna4\Configuratore\Confdna.ini")
                    SwDna.WriteLine(";----------------------------------------------------------------" & _
                                    "; File di configurazione per: Configuratore Strumenti - DNA 4.0" & vbCrLf & _
                                    ";----------------------------------------------------------------" & vbCrLf & _
                                    "DatabaseType = ORACLE" & vbCrLf & _
                                    "; DatabaseType = { ORACLE , INTERBASE }" & vbCrLf & vbCrLf & _
                                    "Language = Italian" & vbCrLf & _
                                    "; Language = { Italian, English }" & vbCrLf & vbCrLf & _
                                    "ysnParametersLanguage = NO" & vbCrLf & _
                                    "ysnDNLab = SI" & vbCrLf & vbCrLf & _
                                    "[INTERBASE]" & vbCrLf & _
                                    "FileDB = localhost/f:/dna4/databaseI2K/dna.gdb" & vbCrLf & vbCrLf & _
                                    "[ORACLE]" & vbCrLf & _
                                    "SID = " & Rstdb("sidtns").Value.ToString & vbCrLf & _
                                    "IndirizzoServer = " & Rstdb("iptns").Value.ToString & vbCrLf & _
                                    "Porta = " & Rstdb("portatns").Value.ToString & vbCrLf & _
                                    "; Porta = { 1526 , 1521 }")
                    SwDna.Close()
                    SwDna.Dispose()
                End Using
                Using SwStat As StreamWriter = File.CreateText("C:\DNLab\Statistiche\OENew\classes\Properties\database_base.properties")
                    SwStat.WriteLine("Databases=DNLab" & vbCrLf & vbCrLf & _
                                     "Database.DNLab.Type=ORACLE" & vbCrLf & vbCrLf & _
                                     "Database.DNLab.Url=jdbc:oracle:thin:@" & Rstdb("iptns").Value.ToString & ":" & Rstdb("portatns").Value.ToString & ":" & Rstdb("sidtns").Value.ToString)

                    SwStat.Close()
                    SwStat.Dispose()
                End Using
            End If

            Rstdb.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmdDisconnectVpn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdDisconnectVpn.Click
        vpnconnection.disconnect()
        If CmdDisconnectVpn.Visible Then
            CmdDisconnectVpn.Hide()
        End If
    End Sub

    Private Sub CmdQueryMDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdQueryMDB.Click
        CmbImpianti.DroppedDown = True

        'PRODOTTI-SID:
        'strsql = "select * from prodotti where nome like 'DB%' and sidporta is not null"
        'Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        'While Rstdb.EOF <> True
        '    strsql = "select * from tnsnames where idimpianto=" & Rstdb("Id_Impianto").Value
        '    Rstdb2.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        '    If Rstdb2.RecordCount <> 0 Then
        '        Dim Pos As Short
        '        For c = 0 To Rstdb("SidPorta").Value.ToString.Length - 1
        '            Dim car As Char = Rstdb("SidPorta").Value.ToString.Chars(c)
        '            If Char.IsControl(car) Then
        '                Pos = c
        '            End If
        '        Next
        '        Dim Sid As String = Mid(Rstdb("SidPorta").Value.ToString, 1, Pos)
        '        Dim Trovato As Boolean = False
        '        While Rstdb2.EOF <> True
        '            If Sid = Rstdb2("sidtns").Value Then
        '                Trovato = True
        '                Exit While
        '            End If
        '            Rstdb2.MoveNext()
        '        End While
        '        If Trovato = False Then
        '            Using sw As StreamWriter = File.AppendText(My.Application.Info.DirectoryPath & "\SIDERRATO" & Replace(CStr(Today.Date), "/", "-") & ".txt")
        '                sw.WriteLine(vbCrLf & Rstdb("Id_Impianto").Value & "-" & Rstdb("SidPorta").Value)
        '                sw.Close()
        '            End Using
        '        End If
        '    End If
        '    Rstdb2.Close()
        '    Rstdb.MoveNext()
        'End While

        'Rstdb.Close()
        'MsgBox("Finito", MsgBoxStyle.Information)
    End Sub
    Private Sub TabRubrica_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabRubrica.Enter
        CmbRubrica.Items.Clear()
        CmbRubrica.ResetText()

        strsql = "select distinct(tipo) from rubrica where id_riferimento=" & IdRiferimento
        Rstdb2.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        While Rstdb2.EOF <> True
            CmbRubrica.Items.Add(Rstdb2("Tipo").Value)
            Rstdb2.MoveNext()
        End While

        Rstdb2.Close()
    End Sub
    Private Sub CmbRubrica_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbRubrica.SelectedValueChanged
        Me.RubricaTableAdapter.FillByTipo(Me.GestioneHDDataSet.Rubrica, IdRiferimento, CmbRubrica.SelectedItem)
    End Sub
    Private Sub AggiornaVPMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AggiornaVPNMenu.Click
        'Aggiorna.Focus()
        'Aggiorna.ShowDialog(Me)
        Call Aggiorna.AggiornaVPN()
    End Sub
    Private Sub PassaggioConsegneMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PassaggioConsegneMenu.Click
        Passaggi.Show()
    End Sub
    Private Sub SpostaMonitorMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpostaMonitorMenu.Click
        'SpostaMonitor.Show()
        Call Shell("explorer https://script.google.com/a/macros/noemalife.com/s/AKfycbzxRUG8ntn064ZcPwyjdGwu4xlKXGmGjf44NvUBIISMiujXtn0/exec", vbMaximizedFocus)
    End Sub



    Private Sub CmdAllegati_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdAllegati.Click
        If My.Computer.Network.Ping("hda") = False Then
            MsgBox("hda non raggiungible, scollegarsi dalla VPN per aprire la scheda cliente", MsgBoxStyle.Information)
            Exit Sub
        End If
        Try
            strsql = "select * from riferimento_impianti where idriferimento=" & IdRiferimento
            Rstdb.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            Dim Impianto As String = Rstdb("nomeriferimento").Value

            Rstdb.Close()

            If Directory.Exists("\\hda\AllegatiHDA\" & Impianto & "\") Then
                Dim SchedaDir As New DirectoryInfo("\\hda\AllegatiHDA\" & Impianto & "\")
                System.Diagnostics.Process.Start(SchedaDir.FullName)
            Else
                MsgBox("Impossibile aprire la cartella degli allegati", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox("Impossibile aprire la cartella degli allegati", MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub CmdVnc_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdVnc.Click
        If File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\UltraVNC\vncviewer.exe") = False Then
            MsgBox("Installare VncViewer per connettersi", MsgBoxStyle.Information)
            Exit Sub
        End If
        'LO APRO MANUALE ED ESCO:
        If ChkVnc.Checked Then
            System.Diagnostics.Process.Start(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\UltraVNC\vncviewer.exe")
            ChkVnc.Checked = False
            Exit Sub
        End If

        If GrigliaServer.SelectedRows.Count = 0 Then
            MsgBox("Selezionare le righe relative ai server a cui ci si vuole collegare", MsgBoxStyle.Information)
            Exit Sub
        End If
        Try
            For x = 0 To GrigliaServer.SelectedRows.Count - 1
                With GrigliaServer.SelectedRows(x)
                    If .Cells("PwdVncCol").Value.ToString = "" Then
                        MsgBox("Uno o più server non hanno la Password salvata sul DB, aggiornarla per connettersi automaticamente." & vbCrLf & _
                        "Per questi verrà aperta la connessione con la richiesta di credenziali", MsgBoxStyle.Information)
                        Exit For
                    End If
                End With
            Next

            For x = 0 To GrigliaServer.SelectedRows.Count - 1
                With GrigliaServer.SelectedRows(x)
                    Dim p As New ProcessStartInfo
                    If .Cells("PwdVncCol").Value.ToString = "" Then
                        'APRO CHIEDENDO PWD:
                        p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\UltraVNC\vncviewer.exe"

                        p.Arguments = .Cells("IpCol").Value.ToString

                        Process.Start(p)
                    Else
                        'APRO AUTOMATICAMENTE:
                        p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\UltraVNC\vncviewer.exe"

                        p.Arguments = .Cells("IpCol").Value.ToString & " /password " & """" & .Cells("PwdVncCol").Value.ToString & """"

                        Process.Start(p)
                    End If
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Information)
        End Try

        ChkVnc.Checked = False
    End Sub

    Private Sub RicAva_Click(sender As System.Object, e As System.EventArgs) Handles RicAva.Click
        RicercaAvanzata.Show()

    End Sub

  

    Private Sub CmbImpianti_TextChanged(ByVal sender As Object, ByVal e As EventArgs) _
     Handles CmbImpianti.TextChanged


    End Sub
End Class

