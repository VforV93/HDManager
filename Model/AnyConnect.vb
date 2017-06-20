Imports System.IO
Imports HDManager

'Gestione impianti_connessioni
'tipoconnessione = 'AnyConnectDirect' or 'AnyConnect'

Public Class AnyConnect
    Implements IVPNConnection

    Private tipoConnessione As String
    Private Rstdb As ADODB.Recordset
    Private direct As Boolean

    Sub New(ByRef Rstdb As ADODB.Recordset)
        Me.tipoConnessione = Rstdb("Tipoconnessione").Value.ToString
        Me.Rstdb = Rstdb
    End Sub

    Public ReadOnly Property getName As String Implements IVPNConnection.getName
        Get
            getName = "AnyConnect"
        End Get
    End Property

    Public Property getDirect As Boolean
        Set(value As Boolean)
            Me.direct = value
        End Set
        Get
            getDirect = Me.direct
        End Get
    End Property

    Private Sub writeConnectionFile(path As String)
        Using sw As StreamWriter = File.CreateText(path & "\tmpConnection.txt")
            sw.WriteLine("connect " & Rstdb("connessionestringa").Value)
            sw.WriteLine("1")
            sw.WriteLine(Rstdb("utenteconnessione").Value)
            sw.WriteLine(Rstdb("passwordconnessione").Value)
            sw.WriteLine("y")
        End Using

        Using sw As StreamWriter = File.CreateText(path & "\tmpConnection.bat")
            sw.WriteLine("""" & My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco\Cisco AnyConnect Secure Mobility Client\vpncli.exe"" -s < """ & path & "\tmpConnection.txt""")
        End Using
    End Sub


    'Esegue delle verifiche preliminari per il corretto utilizzo della vpn 
    Private Function checks() As Boolean
        If Me.getDirect Then
            If File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco\Cisco AnyConnect Secure Mobility Client\vpncli.exe") = False Then
                MsgBox("Impossibile trovare il client della VPN Cisco AnyConnect per connettersi", MsgBoxStyle.Information)
                'Rstdb.Close()
                Return True
            End If
        Else
            'MsgBox(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Cisco\Cisco AnyConnect Secure Mobility Client\Profile")
            If My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Cisco\Cisco AnyConnect Secure Mobility Client\Profile") = False Then
                'If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Cisco\Cisco AnyConnect Secure Mobility Client\Profile") = False Then
                MsgBox("Impossibile trovare Cisco nella direcotry ProgramData, non è possibile copiarvi il profilo per la connessione", MsgBoxStyle.Information)
                'Rstdb.Close()
                Return True
            End If
            If File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Cisco\Cisco AnyConnect Secure Mobility Client\Profile\" & Rstdb("connessionestringa").Value) Then
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Cisco\Cisco AnyConnect Secure Mobility Client\Profile\" & Rstdb("connessionestringa").Value)
            End If

            If File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco\Cisco AnyConnect Secure Mobility Client\vpnui.exe") = False Then
                MsgBox("Impossibile trovare l'eseguibile vpnui.exe", MsgBoxStyle.Information)
                'Rstdb.Close()
                Return True
            End If
        End If
    End Function

    Sub connect() Implements IVPNConnection.connect
        If checks() Then    'la funzione ritorna True nel caso in cui ci sia qualche problema e bisogna abortire la connessioe
            Exit Sub
        End If

        If Me.getDirect Then
            writeConnectionFile(Application.StartupPath)
            'DISCONNETTO LA VPN NEL CASO IN CUI FOSSE GIA' COLLEGATA:
            Me.disconnect()
            Threading.Thread.Sleep(2500)
            Dim tmpSource As String = Application.StartupPath & "\tmpConnection.txt"
            Process.Start(Application.StartupPath & "\tmpConnection.bat")
            Main.CmdDisconnectVpn.Visible = True
        Else
            'se sono nel caso di AnyConnet NON Direct devo copiare il profilo dalla forlder vpn_hdmanager\anyConnect
            'nella cartella dei profili di Cisco
            MsgBox(Application.StartupPath & "\vpn_hdmanager\anyConnect\" & Rstdb("connessionestringa").Value)
            My.Computer.FileSystem.CopyFile(
                 Application.StartupPath & "\vpn_hdmanager\anyConnect\" & Rstdb("connessionestringa").Value,
                 Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\Cisco\Cisco AnyConnect Secure Mobility Client\Profile\" & Rstdb("connessionestringa").Value)
            Process.Start(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco\Cisco AnyConnect Secure Mobility Client\vpnui.exe")
            Main.CmdDisconnectVpn.Visible = True

            Main.TxtUser.Text = Rstdb("utenteconnessione").Value.ToString
            Main.TxtPwd.Text = Rstdb("passwordconnessione").Value.ToString

            If Main.TxtUser.Text <> "" Then Main.TxtUser.Visible = True
            If Main.TxtPwd.Text <> "" Then Main.TxtPwd.Visible = True
        End If
    End Sub

    Public Sub disconnect() Implements IVPNConnection.disconnect
        If File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco\Cisco AnyConnect Secure Mobility Client\vpncli.exe") = False Then
            MsgBox("Impossibile trovare il client della VPN Cisco AnyConnect per disconnettersi", MsgBoxStyle.Information)
            'Rstdb.Close()
            Exit Sub
        End If

        Dim p As New ProcessStartInfo
        p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco\Cisco AnyConnect Secure Mobility Client\vpncli.exe"
        p.Arguments = "disconnect"
        Process.Start(p)
    End Sub
End Class
