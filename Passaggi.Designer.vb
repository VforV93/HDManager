<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Passaggi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Passaggi))
        Me.LblTurnista = New System.Windows.Forms.Label
        Me.CmbTurnista = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CmdCrea = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'LblTurnista
        '
        Me.LblTurnista.BackColor = System.Drawing.Color.Wheat
        Me.LblTurnista.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblTurnista.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTurnista.ForeColor = System.Drawing.Color.Navy
        Me.LblTurnista.Location = New System.Drawing.Point(12, 59)
        Me.LblTurnista.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblTurnista.Name = "LblTurnista"
        Me.LblTurnista.Size = New System.Drawing.Size(79, 26)
        Me.LblTurnista.TabIndex = 89
        Me.LblTurnista.Text = "Turnista:"
        Me.LblTurnista.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmbTurnista
        '
        Me.CmbTurnista.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbTurnista.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbTurnista.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTurnista.FormattingEnabled = True
        Me.CmbTurnista.Location = New System.Drawing.Point(94, 62)
        Me.CmbTurnista.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CmbTurnista.MaxDropDownItems = 15
        Me.CmbTurnista.Name = "CmbTurnista"
        Me.CmbTurnista.Size = New System.Drawing.Size(227, 22)
        Me.CmbTurnista.TabIndex = 88
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(258, 16)
        Me.Label1.TabIndex = 92
        Me.Label1.Text = "Scegliere il turnista a cui passare le consegne:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmdCrea
        '
        Me.CmdCrea.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdCrea.Location = New System.Drawing.Point(111, 107)
        Me.CmdCrea.Name = "CmdCrea"
        Me.CmdCrea.Size = New System.Drawing.Size(80, 23)
        Me.CmdCrea.TabIndex = 93
        Me.CmdCrea.Text = "Crea Email"
        Me.CmdCrea.UseVisualStyleBackColor = True
        '
        'Passaggi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(332, 158)
        Me.Controls.Add(Me.CmdCrea)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblTurnista)
        Me.Controls.Add(Me.CmbTurnista)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Passaggi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Passaggio Consegne"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblTurnista As System.Windows.Forms.Label
    Friend WithEvents CmbTurnista As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmdCrea As System.Windows.Forms.Button
End Class
