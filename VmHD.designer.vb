<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VmHD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VmHD))
        Me.AxVMHD32 = New AxVMwareRemoteConsoleTypeLib.AxVMwareEmbeddedRemoteConsole
        Me.AxVMHD33 = New AxVMwareRemoteConsoleTypeLib.AxVMwareEmbeddedRemoteConsole
        Me.Tab = New System.Windows.Forms.TabControl
        Me.TabVM1 = New System.Windows.Forms.TabPage
        Me.TabVM2 = New System.Windows.Forms.TabPage
        CType(Me.AxVMHD32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxVMHD33, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tab.SuspendLayout()
        Me.TabVM1.SuspendLayout()
        Me.TabVM2.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxVMHD32
        '
        Me.AxVMHD32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxVMHD32.Enabled = True
        Me.AxVMHD32.Location = New System.Drawing.Point(3, 3)
        Me.AxVMHD32.Name = "AxVMHD32"
        Me.AxVMHD32.Size = New System.Drawing.Size(1067, 660)
        Me.AxVMHD32.TabIndex = 1
        '
        'AxVMHD33
        '
        Me.AxVMHD33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxVMHD33.Enabled = True
        Me.AxVMHD33.Location = New System.Drawing.Point(3, 3)
        Me.AxVMHD33.Name = "AxVMHD33"
        Me.AxVMHD33.Size = New System.Drawing.Size(1067, 660)
        Me.AxVMHD33.TabIndex = 2
        '
        'Tab
        '
        Me.Tab.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.Tab.Controls.Add(Me.TabVM1)
        Me.Tab.Controls.Add(Me.TabVM2)
        Me.Tab.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tab.HotTrack = True
        Me.Tab.Location = New System.Drawing.Point(26, 12)
        Me.Tab.Multiline = True
        Me.Tab.Name = "Tab"
        Me.Tab.SelectedIndex = 0
        Me.Tab.Size = New System.Drawing.Size(1085, 700)
        Me.Tab.TabIndex = 70
        '
        'TabVM1
        '
        Me.TabVM1.BackColor = System.Drawing.Color.DarkGray
        Me.TabVM1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TabVM1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabVM1.Controls.Add(Me.AxVMHD32)
        Me.TabVM1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabVM1.Location = New System.Drawing.Point(4, 26)
        Me.TabVM1.Name = "TabVM1"
        Me.TabVM1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabVM1.Size = New System.Drawing.Size(1077, 670)
        Me.TabVM1.TabIndex = 6
        Me.TabVM1.Text = "VMHD33"
        Me.TabVM1.UseVisualStyleBackColor = True
        '
        'TabVM2
        '
        Me.TabVM2.BackColor = System.Drawing.Color.DarkGray
        Me.TabVM2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TabVM2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabVM2.Controls.Add(Me.AxVMHD33)
        Me.TabVM2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabVM2.Location = New System.Drawing.Point(4, 26)
        Me.TabVM2.Name = "TabVM2"
        Me.TabVM2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabVM2.Size = New System.Drawing.Size(1077, 670)
        Me.TabVM2.TabIndex = 7
        Me.TabVM2.Text = "VMHD32"
        Me.TabVM2.UseVisualStyleBackColor = True
        '
        'VmHD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(1142, 716)
        Me.Controls.Add(Me.Tab)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "VmHD"
        Me.Text = "Vm HD Preview"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.AxVMHD32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxVMHD33, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tab.ResumeLayout(False)
        Me.TabVM1.ResumeLayout(False)
        Me.TabVM2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AxVMHD32 As AxVMwareRemoteConsoleTypeLib.AxVMwareEmbeddedRemoteConsole
    Friend WithEvents AxVMHD33 As AxVMwareRemoteConsoleTypeLib.AxVMwareEmbeddedRemoteConsole
    Friend WithEvents Tab As System.Windows.Forms.TabControl
    Friend WithEvents TabVM1 As System.Windows.Forms.TabPage
    Friend WithEvents TabVM2 As System.Windows.Forms.TabPage

End Class
