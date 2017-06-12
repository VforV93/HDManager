<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HowTo
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HowTo))
        Me.LblTipo = New System.Windows.Forms.Label
        Me.CmbTipo = New System.Windows.Forms.ComboBox
        Me.HowtoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GestioneHDDataSet = New HDManager.gestioneHDDataSet
        Me.LblSoluzione = New System.Windows.Forms.Label
        Me.TxtSoluzione = New System.Windows.Forms.RichTextBox
        Me.LblProblema = New System.Windows.Forms.Label
        Me.TxtProblema = New System.Windows.Forms.RichTextBox
        Me.TxtCerca = New System.Windows.Forms.TextBox
        Me.LblCerca = New System.Windows.Forms.Label
        Me.CmdCerca = New System.Windows.Forms.Button
        Me.HowtoTableAdapter = New HDManager.gestioneHDDataSetTableAdapters.howtoTableAdapter
        Me.GrigliaHowTo = New System.Windows.Forms.DataGridView
        Me.IdHowTo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Expr2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Expr3DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ProblemaCol = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SoluzioneCol = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AttivohowtoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.HowtoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GestioneHDDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrigliaHowTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblTipo
        '
        Me.LblTipo.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblTipo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblTipo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTipo.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LblTipo.Location = New System.Drawing.Point(4, 35)
        Me.LblTipo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblTipo.Name = "LblTipo"
        Me.LblTipo.Size = New System.Drawing.Size(45, 26)
        Me.LblTipo.TabIndex = 70
        Me.LblTipo.Text = "Tipo:"
        Me.LblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmbTipo
        '
        Me.CmbTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CmbTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbTipo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbTipo.FormattingEnabled = True
        Me.CmbTipo.Location = New System.Drawing.Point(57, 39)
        Me.CmbTipo.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CmbTipo.Name = "CmbTipo"
        Me.CmbTipo.Size = New System.Drawing.Size(428, 22)
        Me.CmbTipo.TabIndex = 1
        '
        'HowtoBindingSource
        '
        Me.HowtoBindingSource.DataMember = "howto"
        Me.HowtoBindingSource.DataSource = Me.GestioneHDDataSet
        '
        'GestioneHDDataSet
        '
        Me.GestioneHDDataSet.DataSetName = "gestioneHDDataSet"
        Me.GestioneHDDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LblSoluzione
        '
        Me.LblSoluzione.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblSoluzione.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblSoluzione.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSoluzione.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LblSoluzione.Location = New System.Drawing.Point(539, 267)
        Me.LblSoluzione.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblSoluzione.Name = "LblSoluzione"
        Me.LblSoluzione.Size = New System.Drawing.Size(75, 28)
        Me.LblSoluzione.TabIndex = 95
        Me.LblSoluzione.Text = "Soluzione:"
        Me.LblSoluzione.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtSoluzione
        '
        Me.TxtSoluzione.BackColor = System.Drawing.Color.LightYellow
        Me.TxtSoluzione.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSoluzione.Location = New System.Drawing.Point(539, 298)
        Me.TxtSoluzione.Name = "TxtSoluzione"
        Me.TxtSoluzione.Size = New System.Drawing.Size(530, 290)
        Me.TxtSoluzione.TabIndex = 5
        Me.TxtSoluzione.Text = ""
        '
        'LblProblema
        '
        Me.LblProblema.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblProblema.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblProblema.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProblema.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LblProblema.Location = New System.Drawing.Point(6, 267)
        Me.LblProblema.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LblProblema.Name = "LblProblema"
        Me.LblProblema.Size = New System.Drawing.Size(75, 28)
        Me.LblProblema.TabIndex = 93
        Me.LblProblema.Text = "Problema:"
        Me.LblProblema.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtProblema
        '
        Me.TxtProblema.BackColor = System.Drawing.Color.LightYellow
        Me.TxtProblema.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProblema.Location = New System.Drawing.Point(4, 298)
        Me.TxtProblema.Name = "TxtProblema"
        Me.TxtProblema.Size = New System.Drawing.Size(530, 290)
        Me.TxtProblema.TabIndex = 4
        Me.TxtProblema.Text = ""
        '
        'TxtCerca
        '
        Me.TxtCerca.Location = New System.Drawing.Point(593, 39)
        Me.TxtCerca.Name = "TxtCerca"
        Me.TxtCerca.Size = New System.Drawing.Size(405, 22)
        Me.TxtCerca.TabIndex = 2
        '
        'LblCerca
        '
        Me.LblCerca.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblCerca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCerca.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCerca.ForeColor = System.Drawing.Color.RoyalBlue
        Me.LblCerca.Location = New System.Drawing.Point(514, 35)
        Me.LblCerca.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblCerca.Name = "LblCerca"
        Me.LblCerca.Size = New System.Drawing.Size(70, 26)
        Me.LblCerca.TabIndex = 97
        Me.LblCerca.Text = "Ricerca:"
        Me.LblCerca.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmdCerca
        '
        Me.CmdCerca.ForeColor = System.Drawing.Color.Navy
        Me.CmdCerca.Location = New System.Drawing.Point(1005, 37)
        Me.CmdCerca.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CmdCerca.Name = "CmdCerca"
        Me.CmdCerca.Size = New System.Drawing.Size(60, 25)
        Me.CmdCerca.TabIndex = 3
        Me.CmdCerca.Text = "Cerca"
        Me.CmdCerca.UseVisualStyleBackColor = True
        '
        'HowtoTableAdapter
        '
        Me.HowtoTableAdapter.ClearBeforeFill = True
        '
        'GrigliaHowTo
        '
        Me.GrigliaHowTo.AllowUserToAddRows = False
        Me.GrigliaHowTo.AllowUserToDeleteRows = False
        Me.GrigliaHowTo.AllowUserToOrderColumns = True
        Me.GrigliaHowTo.AutoGenerateColumns = False
        Me.GrigliaHowTo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.GrigliaHowTo.BackgroundColor = System.Drawing.Color.DarkGray
        Me.GrigliaHowTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GrigliaHowTo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        Me.GrigliaHowTo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GrigliaHowTo.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GrigliaHowTo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GrigliaHowTo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdHowTo, Me.Expr2DataGridViewTextBoxColumn, Me.Expr3DataGridViewTextBoxColumn, Me.ProblemaCol, Me.SoluzioneCol, Me.AttivohowtoDataGridViewTextBoxColumn})
        Me.GrigliaHowTo.DataSource = Me.HowtoBindingSource
        Me.GrigliaHowTo.Location = New System.Drawing.Point(4, 90)
        Me.GrigliaHowTo.Name = "GrigliaHowTo"
        Me.GrigliaHowTo.ReadOnly = True
        Me.GrigliaHowTo.Size = New System.Drawing.Size(1014, 163)
        Me.GrigliaHowTo.TabIndex = 99
        '
        'IdHowTo
        '
        Me.IdHowTo.DataPropertyName = "Expr1"
        Me.IdHowTo.HeaderText = "Expr1"
        Me.IdHowTo.Name = "IdHowTo"
        Me.IdHowTo.ReadOnly = True
        Me.IdHowTo.Visible = False
        '
        'Expr2DataGridViewTextBoxColumn
        '
        Me.Expr2DataGridViewTextBoxColumn.DataPropertyName = "Expr2"
        Me.Expr2DataGridViewTextBoxColumn.HeaderText = "Expr2"
        Me.Expr2DataGridViewTextBoxColumn.Name = "Expr2DataGridViewTextBoxColumn"
        Me.Expr2DataGridViewTextBoxColumn.ReadOnly = True
        Me.Expr2DataGridViewTextBoxColumn.Visible = False
        '
        'Expr3DataGridViewTextBoxColumn
        '
        Me.Expr3DataGridViewTextBoxColumn.DataPropertyName = "Expr3"
        Me.Expr3DataGridViewTextBoxColumn.HeaderText = "Nome HowTo"
        Me.Expr3DataGridViewTextBoxColumn.Name = "Expr3DataGridViewTextBoxColumn"
        Me.Expr3DataGridViewTextBoxColumn.ReadOnly = True
        Me.Expr3DataGridViewTextBoxColumn.Width = 700
        '
        'ProblemaCol
        '
        Me.ProblemaCol.DataPropertyName = "Expr4"
        Me.ProblemaCol.HeaderText = "Expr4"
        Me.ProblemaCol.Name = "ProblemaCol"
        Me.ProblemaCol.ReadOnly = True
        Me.ProblemaCol.Visible = False
        '
        'SoluzioneCol
        '
        Me.SoluzioneCol.DataPropertyName = "soluzionehowto"
        Me.SoluzioneCol.HeaderText = "soluzionehowto"
        Me.SoluzioneCol.Name = "SoluzioneCol"
        Me.SoluzioneCol.ReadOnly = True
        Me.SoluzioneCol.Visible = False
        '
        'AttivohowtoDataGridViewTextBoxColumn
        '
        Me.AttivohowtoDataGridViewTextBoxColumn.DataPropertyName = "attivohowto"
        Me.AttivohowtoDataGridViewTextBoxColumn.HeaderText = "attivohowto"
        Me.AttivohowtoDataGridViewTextBoxColumn.Name = "AttivohowtoDataGridViewTextBoxColumn"
        Me.AttivohowtoDataGridViewTextBoxColumn.ReadOnly = True
        Me.AttivohowtoDataGridViewTextBoxColumn.Visible = False
        '
        'HowTo
        '
        Me.AcceptButton = Me.CmdCerca
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(1108, 593)
        Me.Controls.Add(Me.GrigliaHowTo)
        Me.Controls.Add(Me.CmdCerca)
        Me.Controls.Add(Me.LblCerca)
        Me.Controls.Add(Me.TxtCerca)
        Me.Controls.Add(Me.LblSoluzione)
        Me.Controls.Add(Me.TxtSoluzione)
        Me.Controls.Add(Me.LblProblema)
        Me.Controls.Add(Me.TxtProblema)
        Me.Controls.Add(Me.LblTipo)
        Me.Controls.Add(Me.CmbTipo)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "HowTo"
        Me.Text = "HowTo"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.HowtoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GestioneHDDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrigliaHowTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblTipo As System.Windows.Forms.Label
    Friend WithEvents CmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents HowtoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GestioneHDDataSet As HDManager.gestioneHDDataSet
    Friend WithEvents HowtoTableAdapter As HDManager.gestioneHDDataSetTableAdapters.howtoTableAdapter
    Friend WithEvents LblSoluzione As System.Windows.Forms.Label
    Friend WithEvents TxtSoluzione As System.Windows.Forms.RichTextBox
    Friend WithEvents LblProblema As System.Windows.Forms.Label
    Friend WithEvents TxtProblema As System.Windows.Forms.RichTextBox
    Friend WithEvents TxtCerca As System.Windows.Forms.TextBox
    Friend WithEvents LblCerca As System.Windows.Forms.Label
    Friend WithEvents CmdCerca As System.Windows.Forms.Button
    Friend WithEvents GrigliaHowTo As System.Windows.Forms.DataGridView
    Friend WithEvents IdHowTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expr2DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expr3DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProblemaCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SoluzioneCol As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AttivohowtoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
