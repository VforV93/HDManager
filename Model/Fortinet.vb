Imports System.IO
Imports HDManager

'Versione 5.2.6
Public Class Fortinet
    Implements IVPNConnection

    Private tipoConnessione As String
    Private Rstdb As ADODB.Recordset

    Sub New(ByRef Rstdb As ADODB.Recordset)
        Me.tipoConnessione = Rstdb("Tipoconnessione").Value.ToString
        Me.Rstdb = Rstdb
    End Sub

    Public ReadOnly Property getName As String Implements IVPNConnection.getName
        Get
            getName = "Fortinet"
        End Get
    End Property

    'Esegue delle verifiche preliminari per il corretto utilizzo della vpn 
    Private Sub checks()

    End Sub

    Sub connect() Implements IVPNConnection.connect
        checks()

        If File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Fortinet\Sslvpnclient\Fortisslvpnclient.exe") = False Then
            MsgBox("Impossibile trovare il client della Fortinet per connettersi", MsgBoxStyle.Information)
            Rstdb.Close()
            Exit Sub
        End If

        Dim p As New ProcessStartInfo

        p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Fortinet\Sslvpnclient\Fortisslvpnclient.exe"
        p.Arguments = "connect -h " & """" & Rstdb("connessionestringa").Value.ToString & """" & " -u " & """" & Rstdb("utenteconnessione").Value.ToString & """" & ":" & """" & Rstdb("passwordconnessione").Value.ToString & """"

        Process.Start(p)
    End Sub

    Public Sub disconnect() Implements IVPNConnection.disconnect
        Throw New NotImplementedException()
    End Sub
End Class
