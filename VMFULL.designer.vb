<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VMFULL
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VMFULL))
        Me.AxVMHD = New AxVMwareRemoteConsoleTypeLib.AxVMwareEmbeddedRemoteConsole
        CType(Me.AxVMHD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxVMHD
        '
        Me.AxVMHD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxVMHD.Enabled = True
        Me.AxVMHD.Location = New System.Drawing.Point(0, 0)
        Me.AxVMHD.Name = "AxVMHD"
        Me.AxVMHD.Size = New System.Drawing.Size(1013, 498)
        Me.AxVMHD.TabIndex = 2
        '
        'VMFULL
        '
        Me.ClientSize = New System.Drawing.Size(1013, 498)
        Me.Controls.Add(Me.AxVMHD)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VMFULL"
        Me.Text = "VmHD"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.AxVMHD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblImpianto As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents AxVMwareRemoteConsole1 As AxVMwareRemoteConsoleTypeLib.AxVMwareRemoteConsole
    Friend WithEvents AxVMHD As AxVMwareRemoteConsoleTypeLib.AxVMwareEmbeddedRemoteConsole

End Class
