Imports System.IO
Imports HDManager

'Versione 5.2.6
Public Class Router
    Implements IVPNConnection

    Private tipoConnessione As String
    Private Rstdb As ADODB.Recordset

    Sub New(ByRef Rstdb As ADODB.Recordset)
        Me.tipoConnessione = Rstdb("Tipoconnessione").Value.ToString
        Me.Rstdb = Rstdb
    End Sub

    Public ReadOnly Property getName As String Implements IVPNConnection.getName
        Get
            getName = "Router"
        End Get
    End Property

    'Esegue delle verifiche preliminari per il corretto utilizzo della vpn 
    Private Sub checks()

    End Sub

    Sub connect() Implements IVPNConnection.connect
        checks()

        Dim p As New ProcessStartInfo

        p.UseShellExecute = True
        p.FileName = "ping.exe"
        p.Arguments = Rstdb("connessionestringa").Value.ToString

        Process.Start(p)
    End Sub

    Public Sub disconnect() Implements IVPNConnection.disconnect
        Throw New NotImplementedException()
    End Sub
End Class
