<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RicercaAvanzata
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtCercaImpianto = New System.Windows.Forms.TextBox()
        Me.CmdCercaImpianto = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ricerca:"
        '
        'TxtCercaImpianto
        '
        Me.TxtCercaImpianto.Location = New System.Drawing.Point(75, 9)
        Me.TxtCercaImpianto.Name = "TxtCercaImpianto"
        Me.TxtCercaImpianto.Size = New System.Drawing.Size(245, 20)
        Me.TxtCercaImpianto.TabIndex = 1
        '
        'CmdCercaImpianto
        '
        Me.CmdCercaImpianto.Location = New System.Drawing.Point(345, 7)
        Me.CmdCercaImpianto.Name = "CmdCercaImpianto"
        Me.CmdCercaImpianto.Size = New System.Drawing.Size(75, 23)
        Me.CmdCercaImpianto.TabIndex = 2
        Me.CmdCercaImpianto.Text = "Cerca"
        Me.CmdCercaImpianto.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(15, 45)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(405, 472)
        Me.ListBox1.TabIndex = 3
        '
        'RicercaAvanzata
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 572)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.CmdCercaImpianto)
        Me.Controls.Add(Me.TxtCercaImpianto)
        Me.Controls.Add(Me.Label1)
        Me.Name = "RicercaAvanzata"
        Me.Text = "Ricerca Avanzata"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtCercaImpianto As System.Windows.Forms.TextBox
    Friend WithEvents CmdCercaImpianto As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
End Class
