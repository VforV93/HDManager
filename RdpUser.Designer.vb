<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RdpUser
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RdpUser))
        Me.LblTitolo = New System.Windows.Forms.Label
        Me.LblUser = New System.Windows.Forms.Label
        Me.CmbUser = New System.Windows.Forms.ComboBox
        Me.CmdOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LblTitolo
        '
        Me.LblTitolo.AutoSize = True
        Me.LblTitolo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LblTitolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblTitolo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LblTitolo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitolo.Location = New System.Drawing.Point(69, 4)
        Me.LblTitolo.Name = "LblTitolo"
        Me.LblTitolo.Size = New System.Drawing.Size(205, 16)
        Me.LblTitolo.TabIndex = 4
        Me.LblTitolo.Text = "Seleziona utenza con cui collegarsi a"
        Me.LblTitolo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblUser
        '
        Me.LblUser.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblUser.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUser.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LblUser.Location = New System.Drawing.Point(69, 49)
        Me.LblUser.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblUser.Name = "LblUser"
        Me.LblUser.Size = New System.Drawing.Size(81, 26)
        Me.LblUser.TabIndex = 70
        Me.LblUser.Text = "Username:"
        Me.LblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmbUser
        '
        Me.CmbUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbUser.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbUser.FormattingEnabled = True
        Me.CmbUser.Location = New System.Drawing.Point(158, 52)
        Me.CmbUser.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CmbUser.Name = "CmbUser"
        Me.CmbUser.Size = New System.Drawing.Size(169, 22)
        Me.CmbUser.TabIndex = 69
        '
        'CmdOk
        '
        Me.CmdOk.ForeColor = System.Drawing.Color.Navy
        Me.CmdOk.Location = New System.Drawing.Point(123, 79)
        Me.CmdOk.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CmdOk.Name = "CmdOk"
        Me.CmdOk.Size = New System.Drawing.Size(80, 25)
        Me.CmdOk.TabIndex = 71
        Me.CmdOk.Text = "OK"
        Me.CmdOk.UseVisualStyleBackColor = True
        '
        'RdpUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(340, 116)
        Me.Controls.Add(Me.CmdOk)
        Me.Controls.Add(Me.LblUser)
        Me.Controls.Add(Me.CmbUser)
        Me.Controls.Add(Me.LblTitolo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RdpUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Utenti Multipli"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblTitolo As System.Windows.Forms.Label
    Friend WithEvents LblUser As System.Windows.Forms.Label
    Friend WithEvents CmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents CmdOk As System.Windows.Forms.Button
End Class
