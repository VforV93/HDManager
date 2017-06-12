Imports System.IO
Imports HDManager

'Versione 5.2.6
Public Class OpenVPN
    Implements IVPNConnection

    Private tipoConnessione As String
    Private Rstdb As ADODB.Recordset

    Sub New(ByRef Rstdb As ADODB.Recordset)
        Me.tipoConnessione = Rstdb("Tipoconnessione").Value.ToString
        Me.Rstdb = Rstdb
    End Sub

    Public ReadOnly Property getName As String Implements IVPNConnection.getName
        Get
            getName = "Open VPN"
        End Get
    End Property

    'Esegue delle verifiche preliminari per il corretto utilizzo della vpn 
    Private Function checks() As Integer
        Dim PathVPN As String = Application.StartupPath & "\vpn_hdmanager\openvpn\" & Rstdb("nomeconnessione").Value.ToString
        'Se non c'è proprio la cartella della Open vado all'avviso:
        If Directory.Exists(PathVPN) = False Then
            GoTo NoOpenVPNDir
        End If

        Dim a, b As Short
        Dim OvpnFound As Boolean = False

        Dim FileList As New ArrayList(Directory.GetFiles(PathVPN))
        For b = 0 To FileList.Count - 1
            Dim OvpnFile As New FileInfo(FileList.Item(b).ToString)
            If OvpnFile.Name = Rstdb("connessionestringa").Value.ToString Then
                OvpnFound = True
                Exit For
            End If
            'Next
            If OvpnFound = True Then Exit For
        Next

NoOpenVPNDir:
        If OvpnFound = False Then
            MsgBox("Certificato " & Rstdb("connessionestringa").Value.ToString & " non trovato nei profili della OpenVPN" & vbCrLf &
                   "Aggiornare le VPN dal menù Strumenti per collegarsi automaticamente", MsgBoxStyle.Information)
            'Rstdb.Close() connessione chiusa dal Main, connect function
            Return 1
        End If

        Return 0
    End Function

    Sub connect() Implements IVPNConnection.connect
        Dim client As String = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\OpenVpn\bin\openvpn-gui.exe"

        If checks() = 1 Then 'Se 1 si è verificato qualche problema
            Exit Sub
        End If

        Dim PathVPN As String = Rstdb("Tipoconnessione").Value
        If File.Exists(client) = False Then
            If File.Exists("C:\Program Files\OpenVpn\bin\openvpn-gui.exe") = True Then
                client = "C:\Program Files\OpenVpn\bin\openvpn-gui.exe"
            Else
                MsgBox("Impossibile trovare il client della OpenVPN per connettersi", MsgBoxStyle.Information)
                'Rstdb.Close()
                Exit Sub
            End If
        End If

        Dim Temp As Process()
        Temp = Process.GetProcesses()

        Dim x As Integer
        For x = 0 To Temp.Length - 1
            If Temp(x).ProcessName = "openvpn-gui" OrElse Temp(x).ProcessName = "openvpn" Then
                Temp(x).Kill()
            End If
        Next

        Temp = Nothing
        Application.DoEvents()

        Dim p As New ProcessStartInfo
        PathVPN = Application.StartupPath & "\vpn_hdmanager\openvpn\" & Rstdb("nomeconnessione").Value.ToString
        p.FileName = client
        '"--silent_connection 1 "
        p.Arguments = "--config_dir " & """" & PathVPN & """" & " --connect " & """" & Rstdb("connessionestringa").Value.ToString & """"

        Main.TxtUser.Text = Rstdb("utenteconnessione").Value.ToString
        Main.TxtPwd.Text = Rstdb("passwordconnessione").Value.ToString

        If Main.TxtUser.Text <> "" Then Main.TxtUser.Visible = True
        If Main.TxtPwd.Text <> "" Then Main.TxtPwd.Visible = True

        MsgBox(Rstdb("passwordconnessione").Value.ToString)
        'PROVATO IL TIMER NELLA OPEN E FUNZIA... VALUTARE SE METTERLO:
        'Me.WindowState = FormWindowState.Minimized
        Main.Timer1.Enabled = True
        Dim Pr As Process = Process.Start(p)
        Pr.WaitForInputIdle()
        Main.Timer2.Enabled = True
    End Sub

    Public Sub disconnect() Implements IVPNConnection.disconnect
        Throw New NotImplementedException()
    End Sub
End Class
