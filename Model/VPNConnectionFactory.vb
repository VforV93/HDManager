Public Class VPNConnectionFactory
    Public Function getIVPNConnection(ByVal IdImpianto As Long, ByRef Rstdb As ADODB.Recordset) As IVPNConnection
        If Rstdb("Tipoconnessione").Value.ToString.ToLower = "pulse secure-users" Then
            Return New PulseSicure(Rstdb)
        ElseIf Rstdb("Tipoconnessione").Value = "Forticlient" Then
            Return New Fortinet(Rstdb)
        ElseIf Rstdb("Tipoconnessione").Value = "Cisco VPN" Then
            Return New CiscoVPN(Rstdb)
        ElseIf Rstdb("Tipoconnessione").Value = "Open VPN" Then
            Return New OpenVPN(Rstdb)
        ElseIf Rstdb("Tipoconnessione").Value = "Logmein" Then
            Return New LogMeIn(Rstdb)
        ElseIf Rstdb("Tipoconnessione").Value = "Microsoft VPN" Then
            Return New MicrosoftVPN(Rstdb)
        ElseIf Rstdb("Tipoconnessione").Value = "CheckPoint" Then
            Return New CheckPoint(Rstdb)
        ElseIf InStr(Rstdb("Tipoconnessione").Value.ToString().ToLower, "anyconnect") <> 0 Then
            Dim tmp As AnyConnect = New AnyConnect(Rstdb)
            If InStr(Rstdb("Tipoconnessione").Value.ToString().ToLower, "direct") <> 0 Then
                tmp.getDirect = True
            Else
                tmp.getDirect = False
            End If
            Return tmp
        ElseIf Rstdb("Tipoconnessione").Value = "Router" Then
                Return New Router(Rstdb)
        End If

        MsgBox("[VPNConnectionFactory] impossibile identificare tipoconnessione")
        Return Nothing
    End Function

End Class
