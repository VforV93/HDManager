Imports WbemScripting
Imports System.IO
'Imports VMwareRemoteConsoleWebTypeLib
Public Class VMElenco

    Dim Lavora As System.Threading.Thread
    Public Elenco As String
    Dim PathVmRC As String

    Private Sub VMElenco_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Lavora = Nothing
    End Sub
    Private Sub VMElenco_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
                MsgBox(ex.Message.ToString, MsgBoxStyle.Information)
            End Try
        End If
    End Sub
    Private Sub VMElenco_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Cetiri As String
        Cetiri = My.Computer.Info.OSFullName
        Cetiri = My.Computer.Info.OSVersion

        If My.Computer.Info.OSFullName.Contains("XP") Then
            PathVmRC = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\File Comuni\vmware\VMware Remote Console Plug-in\vmware-vmrc.exe"
        Else
            If File.Exists("C:\Program Files (x86)\Common Files\VMware\VMware Remote Console Plug-in\vmware-vmrc.exe") Then
                PathVmRC = "C:\Program Files (x86)\Common Files\VMware\VMware Remote Console Plug-in\vmware-vmrc.exe"
            Else
                PathVmRC = "C:\Program Files\Common Files\VMware\VMware Remote Console Plug-in\vmware-vmrc.exe"
            End If
        End If

        If Elenco <> "Elenco" Then
            Me.WindowState = FormWindowState.Normal
            Me.Location = New Point((My.Computer.Screen.WorkingArea.Width / 2) - 175, Me.Location.Y)
            Me.Width = 400
            Me.MaximizeBox = False
            LblTitolo.Location = New Point(GroupA.Location.X + 30, LblTitolo.Location.Y)

            If Elenco = "GruppoA" Then
                GroupB.Hide()
                'GroupC.Hide()
                GroupT.Hide()
            ElseIf Elenco = "GruppoB" Then
                GroupB.Location = GroupA.Location
                GroupA.Hide()
                'GroupC.Hide()
                GroupT.Hide()
                'ElseIf Elenco = "GruppoC" Then
                '    GroupC.Location = GroupA.Location
                '    GroupA.Hide()
                '    GroupB.Hide()
                '    GroupT.Hide()
            ElseIf Elenco = "Turnisti" Then
                GroupT.Location = GroupA.Location
                GroupA.Hide()
                GroupB.Hide()
                'GroupC.Hide()
            End If
        End If

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

        Lavora = New System.Threading.Thread(AddressOf CheckVM)
        Lavora.Priority = Threading.ThreadPriority.Normal
        Lavora.Start()
    End Sub
    Sub CheckVM()
        Try
            'Se non c'è connesisone di rete esco:
            If My.Computer.Network.IsAvailable = False Then Exit Sub
            'Controllo le VM ciclando nei controlli delle groupbox:
            'Gruppo A:
            If GroupA.Visible = True Then
                For Each Control In GroupA.Controls
                    If Control.Name.StartsWith("VM7HD") Then
                        Try
                            If My.Computer.Network.Ping(Control.Tag) = True Then
                                Dim ObjWMIService As SWbemServicesEx = DirectCast(GetObject("winmgmts://" & Control.Tag & "\root\cimv2"), SWbemServicesEx)
                                ''Dim ColItems As SWbemObjectSet = ObjWMIService.ExecQuery("SELECT * FROM Win32_LoggedOnUser", "WQL", wbemFlagReturnImmediately + wbemFlagForwardOnly)
                                Dim ColItems As SWbemObjectSet = ObjWMIService.ExecQuery("Select * from Win32_ComputerSystem")

                                'For Each objItem As Object In ColItems
                                'Se non è loggato nessuno mi restituisce null quindi ci metto il tostring:
                                If ColItems(0).UserName.ToString = "" Then
                                    GroupA.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Logoff", "", GroupA.Controls.Item("Lbl" & Control.Name), GroupA.Controls.Item("Prg" & Control.Name))
                                    'LblVmhd8.Invoke(Frm1DelegataXCambio, "Logoff", "")
                                Else
                                    GroupA.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Logged", ColItems(0).UserName.ToString, GroupA.Controls.Item("Lbl" & Control.Name), GroupA.Controls.Item("Prg" & Control.Name))
                                    'LblVmhd8.Invoke(Frm1DelegataXCambio, "Logged", ColItems(0).UserName.ToString)
                                    Try
                                        Dim Path As String = "\\noemanas\CustomerService\GestioneHD\VM\" & Control.Name & ".txt"
                                        Dim Str As String
                                        Using LeggiAt As New StreamReader(Path)
                                            LeggiAt.ReadLine()
                                            Str = LeggiAt.ReadLine.ToString
                                            If Str.Length > 30 Then
                                                Str = Str.Substring(0, 30)
                                            End If
                                            LeggiAt.Close()
                                            LeggiAt.Dispose()
                                        End Using

                                        GroupA.Controls.Item("Lbl" & Control.Name & "_2").Invoke(Frm1DelegataXCambio, "@", Str, GroupA.Controls.Item("Lbl" & Control.Name & "_2"), GroupA.Controls.Item("Prg" & Control.Name))
                                    Catch ex As Exception

                                    End Try
                                End If
                            Else 'Non la pingo e quindi vado a leggere tutto dal file sul server:
                                Dim Path As String = "\\noemanas\CustomerService\GestioneHD\VM\" & Control.Name & ".txt"
                                Dim Str, User As String
                                Using LeggiAt As New StreamReader(Path)
                                    User = LeggiAt.ReadLine()
                                    Str = LeggiAt.ReadLine.ToString
                                    If Str.Length > 30 Then
                                        Str = Str.Substring(0, 30)
                                    End If
                                    LeggiAt.Close()
                                    LeggiAt.Dispose()
                                End Using

                                GroupA.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "NoPing", User, GroupA.Controls.Item("Lbl" & Control.Name), GroupA.Controls.Item("Prg" & Control.Name))
                                GroupA.Controls.Item("Lbl" & Control.Name & "_2").Invoke(Frm1DelegataXCambio, "@", Str, GroupA.Controls.Item("Lbl" & Control.Name & "_2"), GroupA.Controls.Item("Prg" & Control.Name))
                            End If
                        Catch ex As Exception
                            GroupA.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Error", "", GroupA.Controls.Item("Lbl" & Control.Name), GroupA.Controls.Item("Prg" & Control.Name))
                            'LblVmhd8.Invoke(Frm1DelegataXCambio, "Error", "")
                        End Try
                    End If
                Next
            End If
            'Gruppo B:
            If GroupB.Visible = True Then
                For Each Control In GroupB.Controls
                    If Control.Name.StartsWith("VM7HD") Then
                        'Dim Prova As Object = Control.Name
                        Try
                            If My.Computer.Network.Ping(Control.Tag) = True Then
                                Dim ObjWMIService As SWbemServicesEx = DirectCast(GetObject("winmgmts://" & Control.Tag & "\root\cimv2"), SWbemServicesEx)
                                ''Dim ColItems As SWbemObjectSet = ObjWMIService.ExecQuery("SELECT * FROM Win32_LoggedOnUser", "WQL", wbemFlagReturnImmediately + wbemFlagForwardOnly)
                                Dim ColItems As SWbemObjectSet = ObjWMIService.ExecQuery("Select * from Win32_ComputerSystem")

                                'For Each objItem As Object In ColItems
                                'Se non è loggato nessuno mi restituisce null quindi ci metto il tostring:
                                If ColItems(0).UserName.ToString = "" Then
                                    GroupB.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Logoff", "", GroupB.Controls.Item("Lbl" & Control.Name), GroupB.Controls.Item("Prg" & Control.Name))
                                    'LblVmhd8.Invoke(Frm1DelegataXCambio, "Logoff", "")
                                Else
                                    GroupB.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Logged", ColItems(0).UserName.ToString, GroupB.Controls.Item("Lbl" & Control.Name), GroupB.Controls.Item("Prg" & Control.Name))
                                    'LblVmhd8.Invoke(Frm1DelegataXCambio, "Logged", ColItems(0).UserName.ToString)
                                    Try
                                        Dim Path As String = "\\noemanas\CustomerService\GestioneHD\VM\" & Control.Name & ".txt"
                                        Dim Str As String
                                        Using LeggiAt As New StreamReader(Path)
                                            LeggiAt.ReadLine()
                                            Str = LeggiAt.ReadLine.ToString
                                            If Str.Length > 30 Then
                                                Str = Str.Substring(0, 30)
                                            End If
                                            LeggiAt.Close()
                                            LeggiAt.Dispose()
                                        End Using

                                        GroupB.Controls.Item("Lbl" & Control.Name & "_2").Invoke(Frm1DelegataXCambio, "@", Str, GroupB.Controls.Item("Lbl" & Control.Name & "_2"), GroupB.Controls.Item("Prg" & Control.Name))
                                    Catch ex As Exception

                                    End Try
                                End If
                            Else 'Non la pingo e quindi vado a leggere tutto dal file sul server:
                                Dim Path As String = "\\noemanas\CustomerService\GestioneHD\VM\" & Control.Name & ".txt"
                                Dim Str, User As String
                                Using LeggiAt As New StreamReader(Path)
                                    User = LeggiAt.ReadLine()
                                    Str = LeggiAt.ReadLine.ToString
                                    If Str.Length > 30 Then
                                        Str = Str.Substring(0, 30)
                                    End If
                                    LeggiAt.Close()
                                    LeggiAt.Dispose()
                                End Using

                                GroupB.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "NoPing", User, GroupB.Controls.Item("Lbl" & Control.Name), GroupB.Controls.Item("Prg" & Control.Name))
                                GroupB.Controls.Item("Lbl" & Control.Name & "_2").Invoke(Frm1DelegataXCambio, "@", Str, GroupB.Controls.Item("Lbl" & Control.Name & "_2"), GroupB.Controls.Item("Prg" & Control.Name))
                            End If
                        Catch ex As Exception
                            GroupB.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Error", "", GroupB.Controls.Item("Lbl" & Control.Name), GroupB.Controls.Item("Prg" & Control.Name))
                            'LblVmhd8.Invoke(Frm1DelegataXCambio, "Error", "")
                        End Try
                    End If
                Next
            End If
            ''Gruppo C:
            'If GroupC.Visible = True Then
            '    For Each Control In GroupC.Controls
            '        If Control.Name.StartsWith("VM7HD") Then
            '            'Dim Prova As Object = Control.Name
            '            Try
            '                If My.Computer.Network.Ping(Control.Name) = True Then
            '                    Dim ObjWMIService As SWbemServicesEx = DirectCast(GetObject("winmgmts://" & Control.Name & "\root\cimv2"), SWbemServicesEx)
            '                    ''Dim ColItems As SWbemObjectSet = ObjWMIService.ExecQuery("SELECT * FROM Win32_LoggedOnUser", "WQL", wbemFlagReturnImmediately + wbemFlagForwardOnly)
            '                    Dim ColItems As SWbemObjectSet = ObjWMIService.ExecQuery("Select * from Win32_ComputerSystem")

            '                    'For Each objItem As Object In ColItems
            '                    'Se non è loggato nessuno mi restituisce null quindi ci metto il tostring:
            '                    If ColItems(0).UserName.ToString = "" Then
            '                        GroupC.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Logoff", "", GroupC.Controls.Item("Lbl" & Control.Name), GroupC.Controls.Item("Prg" & Control.Name))
            '                        'LblVmhd8.Invoke(Frm1DelegataXCambio, "Logoff", "")
            '                    Else
            '                        GroupC.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Logged", ColItems(0).UserName.ToString, GroupC.Controls.Item("Lbl" & Control.Name), GroupC.Controls.Item("Prg" & Control.Name))
            '                        'LblVmhd8.Invoke(Frm1DelegataXCambio, "Logged", ColItems(0).UserName.ToString)
            '                        Try
            '                            Dim Path As String = "\\noemanas\CustomerService\GestioneHD\VM\" & Control.Name & ".txt"
            '                            Dim Str As String
            '                            Using LeggiAt As New StreamReader(Path)
            '                                Str = LeggiAt.ReadLine.ToString
            '                                LeggiAt.Close()
            '                                LeggiAt.Dispose()
            '                            End Using

            '                            GroupC.Controls.Item("Lbl" & Control.Name & "_2").Invoke(Frm1DelegataXCambio, "@", Str, GroupC.Controls.Item("Lbl" & Control.Name & "_2"), GroupC.Controls.Item("Prg" & Control.Name))
            '                        Catch ex As Exception

            '                        End Try
            '                    End If
            '                Else 'Non la pingo e quindi vado a leggere tutto dal file sul server:
            '                    Dim Path As String = "\\noemanas\CustomerService\GestioneHD\VM\" & Control.Name & ".txt"
            '                    Dim Str, User As String
            '                    Using LeggiAt As New StreamReader(Path)
            '                        User = LeggiAt.ReadLine()
            '                        Str = LeggiAt.ReadLine.ToString
            '                        LeggiAt.Close()
            '                        LeggiAt.Dispose()
            '                    End Using

            '                    GroupC.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "NoPing", User, GroupC.Controls.Item("Lbl" & Control.Name), GroupC.Controls.Item("Prg" & Control.Name))
            '                    GroupC.Controls.Item("Lbl" & Control.Name & "_2").Invoke(Frm1DelegataXCambio, "@", Str, GroupC.Controls.Item("Lbl" & Control.Name & "_2"), GroupC.Controls.Item("Prg" & Control.Name))
            '                End If
            '            Catch ex As Exception
            '                GroupC.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Error", "", GroupC.Controls.Item("Lbl" & Control.Name), GroupC.Controls.Item("Prg" & Control.Name))
            '                'LblVmhd8.Invoke(Frm1DelegataXCambio, "Error", "")
            '            End Try
            '        End If
            '    Next
            'End If
            'Gruppo Turnisti:
            If GroupT.Visible = True Then
                For Each Control In GroupT.Controls
                    If Control.Name.StartsWith("VM7HD") Then
                        'Dim Prova As Object = Control.Name
                        Try
                            If My.Computer.Network.Ping(Control.Tag) = True Then
                                Dim ObjWMIService As SWbemServicesEx = DirectCast(GetObject("winmgmts://" & Control.Tag & "\root\cimv2"), SWbemServicesEx)
                                ''Dim ColItems As SWbemObjectSet = ObjWMIService.ExecQuery("SELECT * FROM Win32_LoggedOnUser", "WQL", wbemFlagReturnImmediately + wbemFlagForwardOnly)
                                Dim ColItems As SWbemObjectSet = ObjWMIService.ExecQuery("Select * from Win32_ComputerSystem")

                                'For Each objItem As Object In ColItems
                                'Se non è loggato nessuno mi restituisce null quindi ci metto il tostring:
                                If ColItems(0).UserName.ToString = "" Then
                                    GroupT.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Logoff", "", GroupT.Controls.Item("Lbl" & Control.Name), GroupT.Controls.Item("Prg" & Control.Name))
                                    'LblVmhd8.Invoke(Frm1DelegataXCambio, "Logoff", "")
                                Else
                                    GroupT.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Logged", ColItems(0).UserName.ToString, GroupT.Controls.Item("Lbl" & Control.Name), GroupT.Controls.Item("Prg" & Control.Name))
                                    'LblVmhd8.Invoke(Frm1DelegataXCambio, "Logged", ColItems(0).UserName.ToString)
                                    Try
                                        Dim Path As String = "\\noemanas\CustomerService\GestioneHD\VM\" & Control.Name & ".txt"
                                        Dim Str As String
                                        Using LeggiAt As New StreamReader(Path)
                                            LeggiAt.ReadLine()
                                            Str = LeggiAt.ReadLine.ToString
                                            If Str.Length > 30 Then
                                                Str = Str.Substring(0, 30)
                                            End If
                                            LeggiAt.Close()
                                            LeggiAt.Dispose()
                                        End Using

                                        GroupT.Controls.Item("Lbl" & Control.Name & "_2").Invoke(Frm1DelegataXCambio, "@", Str, GroupT.Controls.Item("Lbl" & Control.Name & "_2"), GroupT.Controls.Item("Prg" & Control.Name))
                                    Catch ex As Exception

                                    End Try
                                End If
                            Else 'Non la pingo e quindi vado a leggere tutto dal file sul server:
                                Dim Path As String = "\\noemanas\CustomerService\GestioneHD\VM\" & Control.Name & ".txt"
                                Dim Str, User As String
                                Using LeggiAt As New StreamReader(Path)
                                    User = LeggiAt.ReadLine()
                                    Str = LeggiAt.ReadLine.ToString
                                    If Str.Length > 30 Then
                                        Str = Str.Substring(0, 30)
                                    End If
                                    LeggiAt.Close()
                                    LeggiAt.Dispose()
                                End Using

                                GroupT.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "NoPing", User, GroupT.Controls.Item("Lbl" & Control.Name), GroupT.Controls.Item("Prg" & Control.Name))
                                GroupT.Controls.Item("Lbl" & Control.Name & "_2").Invoke(Frm1DelegataXCambio, "@", Str, GroupT.Controls.Item("Lbl" & Control.Name & "_2"), GroupT.Controls.Item("Prg" & Control.Name))
                            End If
                        Catch ex As Exception
                            GroupT.Controls.Item("Lbl" & Control.Name).Invoke(Frm1DelegataXCambio, "Error", "", GroupT.Controls.Item("Lbl" & Control.Name), GroupT.Controls.Item("Prg" & Control.Name))
                            'LblVmhd8.Invoke(Frm1DelegataXCambio, "Error", "")
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Delegate Function PerCambioTextBox(ByVal S As String, ByVal User As String, ByVal Lbl As Control, ByVal Prg As Control) As String 'Dichiaro un tipo di funziuone delegata che vuole un parametro stringa e 
    'restituisce un parametro stringa (i due tipi sono uguali in quest'esempio,ma non è assolutamente necessario che lo siano)
    Public Frm1DelegataXCambio As New PerCambioTextBox(AddressOf LeggiECambiaTextBox1) 'Istanzio una funzione delegata del tipo appena dichiarato
    ' e che ha come compito quello di eseguire la funzione LeggiECambiaTextBox1. E' obbliagtorio che la procedura a cui si punta, abbia uguale firma
    ' rispetto al tipo di delegata che si usa.    

    Public Function LeggiECambiaTextBox1(ByVal NuovoTesto As String, ByVal User As String, ByVal Lbl As Control, ByVal Prg As Control) As String
        'Memorizza il valore attuale della proprietà TEXT di Textbox1 e lo restituisce al ritorno dalla funzione.
        'Prima di tornare, assegna il nuovo valore a textbox1
        If NuovoTesto = "Logoff" Then
            Lbl.Text = "Libera"
            Prg.Hide()
        End If
        If NuovoTesto = "Logged" Then
            Lbl.Text = User & " connesso"
            Prg.Hide()
        End If
        If NuovoTesto = "Error" Then
            Lbl.Text = "Impossibile recuperare le info"
            Prg.Hide()
        End If
        If NuovoTesto = "NoPing" Then
            Lbl.Text = User & " connesso"
            Prg.Hide()
        End If
        If NuovoTesto = "@" Then
            Lbl.Text = "@" & User
            Prg.Hide()
        End If

        Return NuovoTesto 'Torna restituendo il testo che era contenuto in textbox1
    End Function

    Private Sub VmHD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VM7HD05.Click, _
      VM7HD17.Click, VM7HD24.Click, VM7HD31.Click, _
      VM7HD13.Click, VM7HD29.Click, VM7HD39.Click, VM7HD08.Click, _
    VM7HD14.Click, VM7HD15.Click, VM7HD21.Click, _
      VM7HD20.Click, VM7HD30.Click, _
     VM7HD06.Click, VM7HD56.Click, VM7HD55.Click, VM7HD51.Click, VM7HD37.Click, VM7HD36.Click, VM7HD35.Click, VM7HD28.Click, VM7HD27.Click, VM7HD26.Click, VM7HD18.Click, VM7HD11.Click, VM7HD10.Click, VM7HD09.Click
        'Lascio stare l'utilizzo della com del remote plugin perchè va in errore dichiarata qui se
        'si aprono e chiudono più vm... non è controllabile così! Vedere magari poi più avanti se può essere utile
        'rimetterla per averne il controllo dal programma...
        'Dim Cetiri As New VMwareRemoteConsole
        'Try
        '    Cetiri.connect("vmstd7.noemalife.loc", "hduser", "noemalife", "", "[StorageVmStd7] " & sender.name & "/" & sender.name & ".vmx", VMwareRemoteConsoleTypeLib.ConsoleFlag.VMRC_STANDALONE)
        'Catch ex As Exception
        '    MsgBox(ex.Message.ToString, MsgBoxStyle.Information)
        '    Cetiri = Nothing
        'End Try
        'Imposto la variabile a seconda dello storage su cui è la VM:
        Dim Storage As String = "VmStd7"

        Select Case sender.name <> ""
            Case sender.name = "VM7HD19"
                Storage = "VmStd6"
            Case sender.name = "VM7HD20"
                Storage = "VmStd6"
            Case sender.name = "VM7HD29"
                Storage = "VmStd6"
            Case sender.name = "VM7HD31"
                Storage = "VmStd6"
            Case sender.name = "VM7HD35"
                Storage = "VmStd6"
            Case sender.name = "VM7HD36"
                Storage = "VmStd6"
            Case sender.name = "VM7HD39"
                Storage = "VmStd6"
        End Select

        If File.Exists(PathVmRC) Then
            Dim p As New ProcessStartInfo
            Dim Param As String

            Param = "-h " & Storage & ".noemalife.loc -u hduser -p noemalife -d " & """[Storage" & Storage & "] " & sender.Name & "/" & sender.Name & ".vmx"""
            p.FileName = PathVmRC
            p.Arguments = Param
            Process.Start(p)
        Else
            MsgBox("Installare il plugin VMWare Remote Console per utilizzare le macchine virtuali", MsgBoxStyle.Information)
        End If
    End Sub

End Class
