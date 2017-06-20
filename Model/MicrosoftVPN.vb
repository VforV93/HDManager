Imports System.IO
Imports HDManager

'Versione 5.2.6
Public Class MicrosoftVPN
    Implements IVPNConnection

    Private tipoConnessione As String
    Private Rstdb As ADODB.Recordset

    Sub New(ByRef Rstdb As ADODB.Recordset)
        Me.tipoConnessione = Rstdb("Tipoconnessione").Value.ToString
        Me.Rstdb = Rstdb
    End Sub

    Public ReadOnly Property getName As String Implements IVPNConnection.getName
        Get
            getName = "Microsoft VPN"
        End Get
    End Property

    'Esegue delle verifiche preliminari per il corretto utilizzo della vpn 
    Private Sub checks()

    End Sub

    Sub connect() Implements IVPNConnection.connect
        checks()

        'Controllo se c'è la MVpn nel file pbk
        Using LeggiMVpn As New StreamReader(Application.StartupPath & "\vpn_hdmanager\Mvpn\rasphone.pbk")
            Dim MVpn As String = LeggiMVpn.ReadToEnd

            If MVpn.Contains(Rstdb("nomeconnessione").Value.ToString) = False Then
                MsgBox("VPN Microsoft " & Rstdb("nomeconnessione").Value.ToString & " non trovata nella rubrica" & vbCrLf &
                           "Aggiornare le VPN dal menù Strumenti per collegarsi automaticamente", MsgBoxStyle.Information)
                Rstdb.Close()
                Exit Sub
            End If
            LeggiMVpn.Close()
            LeggiMVpn.Dispose()
        End Using
        'Il Rasphone apre popup con l'elenco delle connessioni:
        'Process.Start("C:\windows\system32\rasphone.exe")
        'Rasdial per connessione automatica: ad es. rasdial "Firenze - Meyer" user pwd /phonebook: Application.StartupPath & "\vpn_hdmanager\Mvpn\rasphone.pbk"
        Dim p As New ProcessStartInfo

        'p.FileName = "rasdial.exe"
        'p.UseShellExecute = True
        'p.Arguments = """" & Rstdb("nomeconnessione").Value.ToString & """" & " " & """" & Rstdb("utenteconnessione").Value.ToString & """" & " " & """" & Rstdb("passwordconnessione").Value.ToString & """" & " " & "/phonebook:" & """" & Application.StartupPath & "\vpn_hdmanager\Mvpn\rasphone.pbk" & """"
        'Process.Start(p)

        'Lanciata dal processo la connessione non mi viene l'icona nel systray e quindi non vedo se e dove è connesso...
        Using BatMVpn As New StreamWriter(Application.StartupPath & "\vpn_hdmanager\Mvpn\MVpn.bat")
            BatMVpn.Write("@echo off" & vbCrLf &
                              "rasdial /disconnect" & vbCrLf &
                              "rasdial " & """" & Rstdb("nomeconnessione").Value.ToString & """" & " " &
                              Rstdb("utenteconnessione").Value.ToString & " " & Rstdb("passwordconnessione").Value.ToString &
                              " /phonebook:" & """" & Application.StartupPath & """" & "\vpn_hdmanager\Mvpn\rasphone.pbk" & vbCrLf &
                              "@echo on")

            BatMVpn.Close()
            BatMVpn.Dispose()
        End Using

        p.FileName = Application.StartupPath & "\vpn_hdmanager\Mvpn\MVpn.bat"

        Process.Start(p)
        Main.CmdDisconnectVpn.Visible = True
    End Sub

    Public Sub disconnect() Implements IVPNConnection.disconnect
        Dim p As New ProcessStartInfo

        p.FileName = "rasdial.exe"
        p.UseShellExecute = True
        p.Arguments = "/disconnect"
        Process.Start(p)
        Main.CmdDisconnectVpn.Hide()
    End Sub
End Class