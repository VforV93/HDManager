<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Aggiorna
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Aggiorna))
        Me.LblInCorso = New System.Windows.Forms.Label
        Me.Progr = New System.Windows.Forms.ProgressBar
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'LblInCorso
        '
        Me.LblInCorso.AutoSize = True
        Me.LblInCorso.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LblInCorso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblInCorso.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LblInCorso.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInCorso.Location = New System.Drawing.Point(93, 20)
        Me.LblInCorso.Name = "LblInCorso"
        Me.LblInCorso.Size = New System.Drawing.Size(138, 16)
        Me.LblInCorso.TabIndex = 3
        Me.LblInCorso.Text = "Copia Database in corso"
        Me.LblInCorso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Progr
        '
        Me.Progr.Location = New System.Drawing.Point(12, 55)
        Me.Progr.MarqueeAnimationSpeed = 10
        Me.Progr.Maximum = 3000
        Me.Progr.Name = "Progr"
        Me.Progr.Size = New System.Drawing.Size(313, 23)
        Me.Progr.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.Progr.TabIndex = 4
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'Aggiorna
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(340, 116)
        Me.Controls.Add(Me.Progr)
        Me.Controls.Add(Me.LblInCorso)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Aggiorna"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aggiornamento Dati"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblInCorso As System.Windows.Forms.Label
    Friend WithEvents Progr As System.Windows.Forms.ProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
