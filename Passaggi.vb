Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.IO
Public Class Passaggi
    Dim Conn As SqlConnection = New SqlConnection("server=hda.noemalife.loc;uid=helpdesk;pwd=noema_hd;database=HDA")
    Dim Strsql As String
    Dim Turnista, TurnistaDopo As String
    Dim Op As Boolean

    Private Sub Passaggi_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Conn.Close()
    End Sub
    Private Sub Passaggi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Se non raggiungo Hda avviso ed esco:
        If My.Computer.Network.Ping("hda") = False Then
            MsgBox("Server HDA non raggiungibile", MsgBoxStyle.Information)
            Me.Close()
            Me.Dispose()
        End If

        Turnista = Microsoft.VisualBasic.Right(My.User.Name, My.User.Name.Length - InStr(My.User.Name, "\"))
        Me.Text += " - " & Turnista

        Conn.Open()

        Strsql = "select * from TabPersonale where incarico='Turnista'"
        Dim da As New SqlDataAdapter(Strsql, Conn)
        Dim ds As New DataSet
        da.Fill(ds, "TabPersonale")

        With CmbTurnista
            .DataSource = ds.Tables("TabPersonale")
            .DisplayMember = "Username"
            .ValueMember = "IdTecnico"
            .SelectedIndex = -1
        End With

        Op = True
    End Sub

    Private Sub CmdCrea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCrea.Click
        Dim Conn2 As SqlConnection = New SqlConnection("server=hda.noemalife.loc;uid=helpdesk;pwd=noema_hd;database=HDA")
        Dim MyCommand, MyCommand2 As SqlCommand
        Dim TkTurnisti, TkPassati, TkInTurno, TkScalato, TkPassato As String
        Dim Rstdb As SqlDataReader

        TkTurnisti = ""
        TkPassati = ""
        TkInTurno = ""
        TkScalato = ""
        TkPassato = ""

        Dim InizioTurno As String
        If Now.Hour >= 13 AndAlso Now.Hour <= 16 Then
            InizioTurno = Now.Date & " 06:00:00"
        ElseIf Now.Hour >= 20 AndAlso Now.Hour <= 23 Then
            InizioTurno = Now.Date & " 14:00:00"
        ElseIf Now.Hour >= 5 AndAlso Now.Hour <= 8 Then
            InizioTurno = Now.Date.AddDays(-1) & " 22:00:00"
        Else            
            InizioTurno = ""
            MsgBox("Fuori orario per il passaggio consegne", MsgBoxStyle.Information)
            Me.Close()
            Me.Dispose()
            Exit Sub
        End If

        Try
            Conn2.Open()
            MyCommand = New SqlCommand("select IdTEcnico from TabPersonale where username='" & Turnista & "'", Conn)
            Dim IdTurnista As String = MyCommand.ExecuteScalar
            'TODO: TOGLIERE POI:
            'IdTurnista = "P373D" max "P417D" Elly "P274D" Marku "P398D" Chiara "P422D" Ferraresi
            'IdTurnista = "P422D"
            'InizioTurno = "23/11/2013 06:00:00"
            'FineTurno = "23/11/2013 14:00:00"

            MyCommand = New SqlCommand("SELECT distinct(c.IDProtocollo),c.Data,c.IDStato,c.FornCli,c.Riferimento FROM TABPersonaleAttivitaProspetto p,TABChiamateAttivita a, TABChiamate c " & _
                                        "where p.IDProtocollo = a.IDAttivita and a.IDChiamata= c.IDProtocollo and p.IDPersonale='" & IdTurnista & "'" & _
                                        "and p.DataFine>=Convert(Datetime,'" & InizioTurno & "', 105) and p.DataFine<=Convert(Datetime,'" & Now & "', 105) and upper(c.Riferimento) not like '%MONITOR%' " & _
                                        "order by c.IDProtocollo", Conn2)
            Rstdb = MyCommand.ExecuteReader

            While Rstdb.Read
                'PRENDO LO STATO DEL TICKET:
                Dim Stato As String = ""
                If Rstdb("idstato") = "S7D" Then
                    Stato = "Risolto"
                ElseIf Rstdb("idstato") = "S1D" Then
                    Stato = "Inserito"
                ElseIf Rstdb("idstato") = "S2" Then
                    Stato = "In corso"
                ElseIf Rstdb("idstato") = "S4D" Then
                    Stato = "Sospeso"
                ElseIf Rstdb("idstato") = "S5" Then
                    Stato = "Chiuso"
                End If

                If Rstdb("Data") < InizioTurno Then
                    'CONTROLLO SE E' TICKET TURNISTA O PASSATO DA GRUPPO/TURNO PRECEDENTE:                
                    MyCommand2 = New SqlCommand("select count(*) from TABChiamateModifiche where IDProtocollo='" & Rstdb("IdProtocollo") & "' " & _
                                                "and (param1='HD-TURNISTI' or param2='HD-TURNISTI')", Conn)
                    If MyCommand2.ExecuteScalar > 0 Then
                        TkTurnisti += Rstdb("IdProtocollo") & " " & Rstdb("FornCli") & " (" & Stato & ")" & vbCrLf
                    Else
                        TkPassati += Rstdb("IdProtocollo") & " " & Rstdb("FornCli") & " (" & Stato & ")" & vbCrLf
                    End If
                Else
                    'TICKET IN TURNO:
                    TkInTurno += Rstdb("IdProtocollo") & " " & Rstdb("FornCli") & " (" & Stato & ")" & vbCrLf
                End If
                'CONTROLLO SE E' STATO SCALATO:            
                MyCommand2 = New SqlCommand("select Escalation from TABUserDef_DataForm_1 where IDProtocollo='" & Rstdb("IdProtocollo") & "' and Escalation is not null", Conn)
                Dim Reperibile As String = MyCommand2.ExecuteScalar
                If Trim(Reperibile) <> "" Then
                    TkScalato += Rstdb("IdProtocollo") & " " & Rstdb("FornCli") & " (" & Stato & ") " & Reperibile & vbCrLf
                End If
                'CONTROLLO SE E' STATO PASSATO AL TURNISTA SUCCESSIVO:
                MyCommand2 = New SqlCommand("select count(*) from TABChiamateModifiche where IDProtocollo='" & Rstdb("IdProtocollo") & "'" & _
                                            "and Data>=Convert(Datetime,'" & InizioTurno & "', 105) and Data<=Convert(Datetime,'" & Now & "', 105) and IdTo='" & TurnistaDopo & "'", Conn)
                If MyCommand2.ExecuteScalar > 0 Then
                    TkPassato += Rstdb("IdProtocollo") & " " & Rstdb("FornCli") & " (" & Stato & ")" & vbCrLf
                End If
            End While

            Rstdb.Close()
            Conn2.Close()

            Dim CorpoEmail As String
            CorpoEmail = "Eseguite assistenze passate dal turno precedente (comprese quelle assegnate dal Responsabile di Gruppo):" & vbCrLf & _
                         TkPassati & vbCrLf & vbCrLf & _
                         "Eseguite assistenze a fronte di chiamate ricevute da Teletiempo:" & vbCrLf & _
                         TkInTurno & vbCrLf & vbCrLf & _
                         "Eseguite assistenze in HD-TURNISTI:" & vbCrLf & _
                         TkTurnisti & vbCrLf & vbCrLf & _
                         "Escalation:" & vbCrLf & _
                         TkScalato & vbCrLf & vbCrLf & _
                         "Passo assistenze a turnista che mi segue:" & vbCrLf & _
                         TkPassato & vbCrLf & vbCrLf & _
                         "Consegne passate al turnista successivo: " & vbCrLf & CmbTurnista.Text
            Try
                Dim HdOutlook As New Outlook.Application
                Dim HdOutlookSpace As Outlook.NameSpace
                Dim Inbox As Outlook.MAPIFolder

                HdOutlookSpace = HdOutlook.GetNamespace("MAPI")
                Dim newMail As Microsoft.Office.Interop.Outlook.MailItem
                Inbox = HdOutlookSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox)
                newMail = Inbox.Items.Add(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)
                If InizioTurno.Contains("06:") Then
                    newMail.Subject = "PASSAGGIO_CONSEGNE: Fine turno 6-14 " & Turnista
                ElseIf InizioTurno.Contains("14:") Then
                    newMail.Subject = "PASSAGGIO_CONSEGNE: Fine turno 14-22 " & Turnista
                ElseIf InizioTurno.Contains("22:") Then
                    newMail.Subject = "PASSAGGIO_CONSEGNE: Fine turno 22-6 " & Turnista
                Else
                    newMail.Subject = "PASSAGGIO_CONSEGNE: Fine turno " & Turnista
                End If

                newMail.BodyFormat = Outlook.OlBodyFormat.olFormatPlain
                newMail.To = CmbTurnista.Text & "@noemalife.com"
                newMail.CC = "cs_headboard@noemalife.com"
                newMail.Body = CorpoEmail
                'newMail.SaveSentMessageFolder = Inbox
                newMail.Display()
            Catch ex As Exception
                'NON C'E OUTLOOK E QUINDI SCRIVO IN UN TXT E BONA:            
                MsgBox("Outlook non installato, verrà creato un file di testo da utilizzare in un altro Email Client o via Browser ", MsgBoxStyle.Information)
                Using sw As StreamWriter = File.CreateText(Application.StartupPath & "\Passaggio_Consegne.txt")

                    sw.WriteLine(CorpoEmail)

                    sw.Close()
                End Using
                System.Diagnostics.Process.Start(Application.StartupPath & "\Passaggio_Consegne.txt")
            End Try
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub CmbTurnista_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbTurnista.KeyPress
        e.KeyChar = ""
    End Sub

    Private Sub CmbTurnista_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbTurnista.SelectedValueChanged
        If Op = False Then Exit Sub

        TurnistaDopo = CmbTurnista.SelectedValue
    End Sub
End Class