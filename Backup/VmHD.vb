Imports System.IO
Public Class VmHD
    Private Sub VmHD_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        For Each Control In Me.Controls
            If Control.ToString = "AxVMwareRemoteConsoleTypeLib.AxVMwareEmbeddedRemoteConsole" Then
                If Control.isConnected Then Control.Disconnect()
                Control = Nothing
            End If
        Next
    End Sub
    Private Sub VmHD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AxVMHD32.connect("vmstd7.noemalife.loc", "hduser", "noemalife", "", "[StorageVmStd7] VmHD32/VmHD32.vmx", VMwareRemoteConsoleTypeLib.ConsoleFlag.VMRC_EMBEDDED)
        AxVMHD33.connect("vmstd7.noemalife.loc", "hduser", "noemalife", "", "[StorageVmStd7] VmHD33/VmHD33.vmx", VMwareRemoteConsoleTypeLib.ConsoleFlag.VMRC_EMBEDDED)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'QUESTO E' PER APRIRE LA CONSOLE:
        'Dim Cetiri As New VMwareRemoteConsoleTypeLib.VMwareRemoteConsole

        'Cetiri.connect("vmstd7.noemalife.loc", "hduser", "noemalife", "", "[StorageVmStd7] VmHD33/VmHD33.vmx", VMwareRemoteConsoleTypeLib.ConsoleFlag.VMRC_STANDALONE)

        VMFULL.AxVMHD.connect("vmstd7.noemalife.loc", "hduser", "noemalife", "", "[StorageVmStd7] VmHD32/VmHD32.vmx", VMwareRemoteConsoleTypeLib.ConsoleFlag.VMRC_EMBEDDED)
        VMFULL.Show()
    End Sub
End Class
