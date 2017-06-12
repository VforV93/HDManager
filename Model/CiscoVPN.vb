Imports System.IO
Imports HDManager

'Versione 5.2.6
Public Class CiscoVPN
    Implements IVPNConnection

    Private tipoConnessione As String
    Private Rstdb As ADODB.Recordset

    Sub New(ByRef Rstdb As ADODB.Recordset)
        Me.tipoConnessione = Rstdb("Tipoconnessione").Value.ToString
        Me.Rstdb = Rstdb
    End Sub

    Public ReadOnly Property getName As String Implements IVPNConnection.getName
        Get
            getName = "Cisco VPN"
        End Get
    End Property

    'Esegue delle verifiche preliminari per il corretto utilizzo della vpn 
    Private Function checks() As Boolean
        Dim PathVPN As String = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco Systems\VPN Client\profiles\"
        If File.Exists(PathVPN & Rstdb("connessionestringa").Value.ToString & ".pcf") = False Then
            MsgBox("Certificato " & Rstdb("connessionestringa").Value.ToString & ".pcf non trovato nei profili della VPN Cisco" & vbCrLf &
                   "Aggiornare le VPN dal menù Strumenti per collegarsi automaticamente", MsgBoxStyle.Information)
            Return True
        End If
        If File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco Systems\VPN Client\vpnclient.exe") = False OrElse File.Exists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco Systems\VPN Client\vpngui.exe") = False Then
            MsgBox("Impossibile trovare il client della VPN Cisco per connettersi", MsgBoxStyle.Information)
            Return True
        End If
    End Function

    Sub connect() Implements IVPNConnection.connect
        If checks() Then
            Exit Sub
        End If

        Dim p As New ProcessStartInfo
        'DISCONNETTO LA VPN NEL CASO IN CUI FOSSE GIA' COLLEGATA:
        p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco Systems\VPN Client\vpnclient.exe"
        p.Arguments = "disconnect"
        Process.Start(p)
        '' Specify the location of the binary
        p.FileName = My.Computer.FileSystem.SpecialDirectories.ProgramFiles & "\Cisco Systems\VPN Client\vpngui.exe"
        'Con riga di comando:
        'p.Arguments = "-c " & """" & Pcf & """" & " user ettori.dianoema pwd dnmttr25"
        'Lanciando vpngui:
        'p.Arguments = "-c " & "-user ettori.dianoema -pwd dnmttr25 " & """" & Pcf & """"

        p.Arguments = "-c -sd" & " -user " & Rstdb("utenteconnessione").Value.ToString & " -pwd " & Rstdb("passwordconnessione").Value.ToString & " " &
        """" & Rstdb("connessionestringa").Value.ToString & """"

        Process.Start(p)
    End Sub

    Public Sub disconnect() Implements IVPNConnection.disconnect
        Throw New NotImplementedException()
    End Sub
End Class
