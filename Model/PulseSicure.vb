Imports System.IO
Imports HDManager

'Versione 5.2.6
Public Class PulseSicure
    Implements IVPNConnection

    Private tipoConnessione As String
    Private Rstdb As ADODB.Recordset

    Sub New(ByRef Rstdb As ADODB.Recordset)
        Me.tipoConnessione = Rstdb("Tipoconnessione").Value.ToString
        Me.Rstdb = Rstdb
    End Sub

    Public ReadOnly Property getName As String Implements IVPNConnection.getName
        Get
            getName = "Pulse Secure"
        End Get
    End Property

    'Esegue delle verifiche preliminari per il corretto utilizzo della vpn 
    Private Sub checks()

    End Sub

    Sub connect() Implements IVPNConnection.connect
        checks()

        Using BatMVpn As New StreamWriter(Application.StartupPath & "\vpn_hdmanager\MVpn.bat")
            BatMVpn.Write("@echo off" & vbCrLf &
                          "rasdial /disconnect" & vbCrLf &
                          "rasdial " & """" & Rstdb("nomeconnessione").Value.ToString & """" & " " &
                          Rstdb("utenteconnessione").Value.ToString & " " & Rstdb("passwordconnessione").Value.ToString &
                          " /phonebook:" & """" & Application.StartupPath & """" & "\vpn_hdmanager\Mvpn\rasphone.pbk" & vbCrLf &
                          "@echo on")

            BatMVpn.Close()
            BatMVpn.Dispose()
        End Using
    End Sub

    Public Sub disconnect() Implements IVPNConnection.disconnect
        Throw New NotImplementedException()
    End Sub
End Class
