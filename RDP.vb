Imports System.IO
Public Class RDP
    'Dim IpServer, Dominio As String
    Public Sub SubCreaRDP(ByVal Username As String, ByVal IP As String, ByVal Domain As String)
        'Me.FullAddress = IP
        'Me.UserName = Username
        'IpServer = IP
        'Dominio = Domain
        'Me.PromptForCredentials = False
        'CreaBatxRdp()
    End Sub
    Public Function CreaBatxRdp(ByVal Username As String, ByVal Password As String, ByVal IPServer As String, ByVal Dominio As String, ByVal Terminal As String) As FileInfo
        Dim Lines As String() = { _
        "@echo off", _
        "Set pwd=" & Password & "", _
        "Set hashtool=" & """" & Application.StartupPath & "\cryptRDP5.exe" & """", _
        "Set outputfile=" & """" & Application.StartupPath & "\" & Terminal & """", _
        "Set comp=" & IPServer & "", _
        "Set Domain=" & Dominio & "", _
        "Set usr=" & Username & "", _
        "for /f " & """tokens=*""" & " %%a in ('%hashtool% %pwd%') do set pwdhash=%%a", _
        "Echo screen mode id:i:2>> %outputfile%", _
        "Echo desktopwidth:i:1024>> %outputfile%", _
        "Echo desktopheight:i:768>> %outputfile%", _
        "Echo session bpp:i:24>> %outputfile%", _
        "Echo winposstr:s:0,1,32,68,800,572>> %outputfile%", _
        "Echo full address:s:%comp%>> %outputfile%", _
        "Echo compression:i:1>> %outputfile%", _
        "Echo keyboardhook:i:2>> %outputfile%", _
        "Echo audiomode:i:2>> %outputfile%", _
        "Echo redirectdrives:i:0>> %outputfile%", _
        "Echo redirectprinters:i:0>> %outputfile%", _
        "Echo redirectcomports:i:0>> %outputfile%", _
        "Echo redirectsmartcards:i:1>> %outputfile%", _
        "Echo displayconnectionbar:i:1>> %outputfile%", _
        "Echo autoreconnection enabled:i:1>> %outputfile%", _
        "Echo authentication level:i:0>> %outputfile%", _
        "Echo username:s:%usr%>> %outputfile%", _
        "Echo domain:s:%domain%>> %outputfile%", _
        "Echo alternate shell:s:>> %outputfile%", _
        "Echo shell working directory:s:>> %outputfile%", _
        "Echo password 51:b:%pwdhash%>> %outputfile%", _
        "Echo disable wallpaper:i:1>> %outputfile%", _
        "Echo disable full window drag:i:0>> %outputfile%", _
        "Echo disable menu anims:i:0>> %outputfile%", _
        "Echo disable themes:i:0>> %outputfile%", _
        "Echo disable cursor setting:i:0>> %outputfile%", _
        "Echo bitmapcachepersistenable:i:1>> %outputfile%"}

        Dim path As String = Application.StartupPath & "\RDP.cmd"
        Try
            File.WriteAllLines(path, Lines)
        Catch ex As Exception
            MsgBox("Cannot write to RDP File")
        End Try
        Dim f As FileInfo = New FileInfo(path)

        Dim objProcess As System.Diagnostics.Process
        Try
            objProcess = New System.Diagnostics.Process()
            objProcess.StartInfo.FileName = path
            objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            objProcess.Start()

            objProcess.WaitForExit()

            objProcess.Close()
        Catch
            MsgBox("Impossibile creare il file rdp per l'accesso in Terminal ", MsgBoxStyle.Exclamation)
        End Try

        Return f

    End Function
    Public Function CreateRDPFile() As FileInfo
        Dim lines As String() = { _
        "screen mode id:i:" & Me.ScreenMode, _
        "desktopwidth:i:" & Me.DesktopWidth, _
        "desktopheight:i:" & Me.DesktopHeight, _
        "session bpp:i:" & Me.SessionBPP, _
        "winposstr:s:" & Me.WinPosStr, _
        "full address:s:" & Me.FullAddress, _
        "compression:i:" & Me.Compression, _
        "keyboardhook:i:" & Me.KeyboardHook, _
        "audiomode:i:" & Me.AudioMode, _
        "redirectprinters:i:" & Me.RedirectPrinters, _
        "redirectcomports:i:" & Me.RedirectComPorts, _
        "redirectsmartcards:i:" & Me.RedirectSmartCards, _
        "redirectclipboard:i:" & Me.RedirectClipBoard, _
        "redirectposdevices:i:" & Me.RedirectPosDevices, _
        "drivestoredirect:s:" & Me.DriveStoreDirect, _
        "displayconnectionbar:i:" & Me.DisplayConnectionBar, _
        "autoreconnection enabled:i:" & Me.AutoReconnection, _
        "authentication level:i:" & Me.AuthenticationLevel, _
        "prompt for credentials:i:" & Me.PromptForCredentials, _
        "negotiate security layer:i:" & Me.NegotiateSecurityLayer, _
        "username:s:" & Me.UserName, _
        "domain:s:" & Me.Domain, _
        "remoteapplicationmode:i:" & Me.RemoteApplicationMode, _
        "alternate shell:s:" & Me.AlternateShell, _
        "shell working directory:s:" & Me.ShellWorkingDirectory, _
        "disable wallpaper:i:" & Me.DisableWallpaper, _
        "disable full window drag:i:" & Me.DisableFullWindowDrag, _
        "allow desktop composition:i:" & Me.AllowDesktopComposition, _
        "allow font smoothing:i:" & Me.AllowFontSmoothing, _
        "disable menu anims:i:" & Me.DisableMenuAnims, _
        "disable themes:i:" & Me.DisableThemes, _
        "disable cursor setting:i:" & Me.DisableCursorSettings, _
        "bitmapcachepersistenable:i:" & Me.BitMapCachePersistEnable, _
        "gatewayhostname:s:" & Me.GatewayHostName, _
        "gatewayusagemethod:i:" & Me.GatewayUsageMethod, _
        "gatewaycredentialssource:i" & Me.GatewayCredentialSource, _
        "gatewayprofileusagemethod:i:" & Me.GateWayProfileUsageMethod _
        }
        Dim guid As String = System.Guid.NewGuid.ToString
        'Dim Dir As String = Application.StartupPath & "\Impianti\" & Impianto & "\Rdp\"
        'If Directory.Exists(Dir) = False Then
        '    Directory.CreateDirectory(Dir)
        'End If

        Dim path As String = Application.StartupPath & "\Prova.rdp"
        'Try
        '    File.WriteAllLines(path, lines)
        'Catch ex As Exception
        '    MsgBox("Cannot write to RDP File")
        'End Try
        Dim f As FileInfo = New FileInfo(path)
        Return f

    End Function
#Region "Props"
    Public Property Name() As String
        Get
            Return oName
        End Get
        Set(ByVal value As String)
            oName = value
        End Set
    End Property

    Public Property FullAddress() As String
        Get
            Return oFullAddress
        End Get
        Set(ByVal value As String)
            oFullAddress = value
        End Set
    End Property

    Public Property Guid() As String
        Get
            Return oGuid
        End Get
        Set(ByVal value As String)
            oGuid = value
        End Set
    End Property

    Public Property UseLocalCursor() As Byte
        Get
            Return oUseLocalCursor
        End Get
        Set(ByVal value As Byte)
            oUseLocalCursor = value
        End Set
    End Property

    Public Property UseDesktopResize() As Byte
        Get
            Return oUseDesktopResize
        End Get
        Set(ByVal value As Byte)
            oUseDesktopResize = value
        End Set
    End Property

    Public Property FullScreen() As Byte
        Get
            Return oFullScreen
        End Get
        Set(ByVal value As Byte)
            oFullScreen = value
        End Set
    End Property

    Public Property FullColour() As Byte
        Get
            Return oFullColour
        End Get
        Set(ByVal value As Byte)
            oFullColour = value
        End Set
    End Property

    Public Property LowColourLevel() As Integer
        Get
            Return oLowColourLevel
        End Get
        Set(ByVal value As Integer)
            oLowColourLevel = value
        End Set
    End Property

    Public Property PreferredEncoding() As String
        Get
            Return oPreferredEncoding
        End Get
        Set(ByVal value As String)
            oPreferredEncoding = value
        End Set
    End Property

    Public Property AutoSelect() As Byte
        Get
            Return oAutoSelect
        End Get
        Set(ByVal value As Byte)
            oAutoSelect = value
        End Set
    End Property

    Public Property SharedVal() As Byte
        Get
            Return oSharedVal
        End Get
        Set(ByVal value As Byte)
            oSharedVal = value
        End Set
    End Property

    Public Property SendPtrEvents() As Byte
        Get
            Return oSendPtrEvents
        End Get
        Set(ByVal value As Byte)
            oSendPtrEvents = value
        End Set
    End Property

    Public Property SendKeyEvents() As Byte
        Get
            Return oSendKeyEvents
        End Get
        Set(ByVal value As Byte)
            oSendKeyEvents = value
        End Set
    End Property

    Public Property SendCutText() As Byte
        Get
            Return oSendCutText
        End Get
        Set(ByVal value As Byte)
            oSendCutText = value
        End Set
    End Property

    Public Property AcceptCutText() As Byte
        Get
            Return oAcceptCutText
        End Get
        Set(ByVal value As Byte)
            oAcceptCutText = value
        End Set
    End Property

    Public Property DisableWinKeys() As Byte
        Get
            Return oDisableWinKeys
        End Get
        Set(ByVal value As Byte)
            oDisableWinKeys = value
        End Set
    End Property

    Public Property Emulate3() As Byte
        Get
            Return oEmulate3
        End Get
        Set(ByVal value As Byte)
            oEmulate3 = value
        End Set
    End Property

    Public Property PointerEventInterval() As Integer
        Get
            Return oPointerEventInterval
        End Get
        Set(ByVal value As Integer)
            oPointerEventInterval = value
        End Set
    End Property

    Public Property Monitor() As String
        Get
            Return oMonitor
        End Get
        Set(ByVal value As String)
            oMonitor = value
        End Set
    End Property

    Public Property MenuKey() As String
        Get
            Return oMenuKey
        End Get
        Set(ByVal value As String)
            oMenuKey = value
        End Set
    End Property

    Public Property AutoReconnect() As Byte
        Get
            Return oAutoReconnect
        End Get
        Set(ByVal value As Byte)
            oAutoReconnect = value
        End Set
    End Property

    Public Property ScreenMode() As Integer
        Get
            Return oScreenMode
        End Get
        Set(ByVal value As Integer)
            oScreenMode = value
        End Set
    End Property

    Public Property DesktopWidth() As Integer
        Get
            Return oDesktopWidth
        End Get
        Set(ByVal value As Integer)
            oDesktopWidth = value
        End Set
    End Property

    Public Property DesktopHeight() As Integer
        Get
            Return oDesktopHeight
        End Get
        Set(ByVal value As Integer)
            oDesktopHeight = value
        End Set
    End Property

    Public Property SessionBPP() As Integer
        Get
            Return oSessionBPP
        End Get
        Set(ByVal value As Integer)
            oSessionBPP = value
        End Set
    End Property

    Public Property WinPosStr() As String
        Get
            Return oWinPosStr
        End Get
        Set(ByVal value As String)
            oWinPosStr = value
        End Set
    End Property

    Public Property Compression() As Integer
        Get
            Return oCompression
        End Get
        Set(ByVal value As Integer)
            oCompression = value
        End Set
    End Property

    Public Property KeyboardHook() As Integer
        Get
            Return oKeyboardHook
        End Get
        Set(ByVal value As Integer)
            oKeyboardHook = value
        End Set
    End Property

    Public Property AudioMode() As Integer
        Get
            Return oAudioMode
        End Get
        Set(ByVal value As Integer)
            oAudioMode = value
        End Set
    End Property

    Public Property RedirectPrinters() As Integer
        Get
            Return oRedirectPrinters
        End Get
        Set(ByVal value As Integer)
            oRedirectPrinters = value
        End Set
    End Property

    Public Property RedirectComPorts() As Integer
        Get
            Return oRedirectComPorts
        End Get
        Set(ByVal value As Integer)
            oRedirectComPorts = value
        End Set
    End Property

    Public Property RedirectSmartCards() As Integer
        Get
            Return oRedirectSmartCards
        End Get
        Set(ByVal value As Integer)
            oRedirectSmartCards = value
        End Set
    End Property

    Public Property RedirectClipBoard() As Integer
        Get
            Return oRedirectClipBoard
        End Get
        Set(ByVal value As Integer)
            oRedirectClipBoard = value
        End Set
    End Property

    Public Property RedirectPosDevices() As Integer
        Get
            Return oRedirectPosDevices
        End Get
        Set(ByVal value As Integer)
            oRedirectPosDevices = value
        End Set
    End Property

    Public Property DriveStoreDirect() As String
        Get
            Return oDriveStoreDirect
        End Get
        Set(ByVal value As String)
            oDriveStoreDirect = value
        End Set
    End Property

    Public Property DisplayConnectionBar() As Integer
        Get
            Return oDisplayConnectionBar
        End Get
        Set(ByVal value As Integer)
            oDisplayConnectionBar = value
        End Set
    End Property

    Public Property AutoReconnection() As Integer
        Get
            Return oAutoReconnection
        End Get
        Set(ByVal value As Integer)
            oAutoReconnection = value
        End Set
    End Property

    Public Property AuthenticationLevel() As Integer
        Get
            Return oAuthenticationLevel
        End Get
        Set(ByVal value As Integer)
            oAuthenticationLevel = value
        End Set
    End Property

    Public Property PromptForCredentials() As Integer
        Get
            Return oPromptForCredentials
        End Get
        Set(ByVal value As Integer)
            oPromptForCredentials = value
        End Set
    End Property

    Public Property NegotiateSecurityLayer() As Integer
        Get
            Return oNegotiateSecurityLayer
        End Get
        Set(ByVal value As Integer)
            oNegotiateSecurityLayer = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return oUsername
        End Get
        Set(ByVal value As String)
            oUsername = value
        End Set
    End Property

    Public Property Domain() As String
        Get
            Return oDomain
        End Get
        Set(ByVal value As String)
            oDomain = value
        End Set
    End Property

    Public Property RemoteApplicationMode() As Integer
        Get
            Return oRemoteApplicationMode
        End Get
        Set(ByVal value As Integer)
            oRemoteApplicationMode = value
        End Set
    End Property

    Public Property AlternateShell() As String
        Get
            Return oAlternateShell
        End Get
        Set(ByVal value As String)
            oAlternateShell = value
        End Set
    End Property

    Public Property ShellWorkingDirectory() As String
        Get
            Return oShellWorkingDirectory
        End Get
        Set(ByVal value As String)
            oShellWorkingDirectory = value
        End Set
    End Property

    Public Property DisableWallpaper() As Integer
        Get
            Return oDisableWallpaper
        End Get
        Set(ByVal value As Integer)
            oDisableWallpaper = value
        End Set
    End Property

    Public Property DisableFullWindowDrag()
        Get
            Return oDisableFullWindowDrag
        End Get
        Set(ByVal value)
            oDisableFullWindowDrag = value
        End Set
    End Property

    Public Property AllowDesktopComposition() As Integer
        Get
            Return oAllowDesktopComposition
        End Get
        Set(ByVal value As Integer)
            oAllowDesktopComposition = value
        End Set
    End Property

    Public Property AllowFontSmoothing() As Integer
        Get
            Return oAllowFontSmoothing
        End Get
        Set(ByVal value As Integer)
            oAllowFontSmoothing = value
        End Set
    End Property

    Public Property DisableMenuAnims() As Integer
        Get
            Return oDisableMenuAnims
        End Get
        Set(ByVal value As Integer)
            oDisableMenuAnims = value
        End Set
    End Property

    Public Property DisableThemes() As Integer
        Get
            Return oDisableThemes
        End Get
        Set(ByVal value As Integer)
            oDisableThemes = value
        End Set
    End Property

    Public Property DisableCursorSettings() As Integer
        Get
            Return oDisableCursorSettings
        End Get
        Set(ByVal value As Integer)
            oDisableCursorSettings = value
        End Set
    End Property

    Public Property BitMapCachePersistEnable() As Integer
        Get
            Return oBitMapCachePersistEnable
        End Get
        Set(ByVal value As Integer)
            oBitMapCachePersistEnable = value
        End Set
    End Property

    Public Property GatewayHostName() As String
        Get
            Return oGatewayHostName
        End Get
        Set(ByVal value As String)
            oGatewayHostName = value
        End Set
    End Property

    Public Property GatewayUsageMethod() As Integer
        Get
            Return oGateWayUsageMethod
        End Get
        Set(ByVal value As Integer)
            oGateWayUsageMethod = value
        End Set
    End Property

    Public Property GatewayCredentialSource() As Integer
        Get
            Return oGateWayCredentialSource
        End Get
        Set(ByVal value As Integer)
            oGateWayCredentialSource = value
        End Set
    End Property

    Public Property GateWayProfileUsageMethod() As Integer
        Get
            Return oGateWayProfileUsageMethod
        End Get
        Set(ByVal value As Integer)
            oGateWayProfileUsageMethod = value
        End Set
    End Property
#End Region
#Region "Vars"
    'Vars for all
    Private oName As String
    Private oFullAddress As String
    Private oGuid As String

    'Vars for VNC
    Private oUseLocalCursor As Byte
    Private oUseDesktopResize As Byte
    Private oFullScreen As Byte
    Private oFullColour As Byte
    Private oLowColourLevel As Integer
    Private oPreferredEncoding As String
    Private oAutoSelect As Byte
    Private oSharedVal As Byte
    Private oSendPtrEvents As Byte
    Private oSendKeyEvents As Byte
    Private oSendCutText As Byte
    Private oAcceptCutText As Byte
    Private oDisableWinKeys As Byte
    Private oEmulate3 As Byte
    Private oPointerEventInterval As Integer
    Private oMonitor As String
    Private oMenuKey As String
    Private oAutoReconnect As Byte

    'Vars for RDP
    Private oScreenMode As Integer
    Private oDesktopWidth As Integer
    Private oDesktopHeight As Integer
    Private oSessionBPP As Integer
    Private oWinPosStr As String
    Private oCompression As Integer
    Private oKeyboardHook As Integer
    Private oAudioMode As Integer
    Private oRedirectPrinters As Integer
    Private oRedirectComPorts As Integer
    Private oRedirectSmartCards As Integer
    Private oRedirectClipBoard As Integer
    Private oRedirectPosDevices As Integer
    Private oDriveStoreDirect As String
    Private oDisplayConnectionBar As Integer
    Private oAutoReconnection As Integer
    Private oAuthenticationLevel As Integer
    Private oPromptForCredentials As Integer
    Private oNegotiateSecurityLayer As Integer
    Private oUsername As String
    Private oDomain As String
    Private oRemoteApplicationMode As Integer
    Private oAlternateShell As String
    Private oShellWorkingDirectory As String
    Private oDisableWallpaper As Integer
    Private oDisableFullWindowDrag As Integer
    Private oAllowDesktopComposition As Integer
    Private oAllowFontSmoothing As Integer
    Private oDisableMenuAnims As Integer
    Private oDisableThemes As Integer
    Private oDisableCursorSettings As Integer
    Private oBitMapCachePersistEnable As Integer
    Private oGatewayHostName As String
    Private oGateWayUsageMethod As Integer
    Private oGateWayCredentialSource As Integer
    Private oGateWayProfileUsageMethod As Integer
#End Region
End Class
