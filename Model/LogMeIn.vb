Imports System.IO
Imports HDManager

'Versione 5.2.6
Public Class LogMeIn
    Implements IVPNConnection

    Private tipoConnessione As String
    Private Rstdb As ADODB.Recordset

    Sub New(ByRef Rstdb As ADODB.Recordset)
        Me.tipoConnessione = Rstdb("Tipoconnessione").Value.ToString
        Me.Rstdb = Rstdb
    End Sub

    Public ReadOnly Property getName As String Implements IVPNConnection.getName
        Get
            getName = "LogMeIn"
        End Get
    End Property

    'Esegue delle verifiche preliminari per il corretto utilizzo della vpn 
    Private Sub checks()

    End Sub

    Sub connect() Implements IVPNConnection.connect
        checks()

        Main.Timer1.Enabled = True

        Process.Start("https://secure.logmein.com/login.asp")

        Main.Timer2.Enabled = True
    End Sub

    Public Sub disconnect() Implements IVPNConnection.disconnect
        Throw New NotImplementedException()
    End Sub
End Class
