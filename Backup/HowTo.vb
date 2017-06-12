Public Class HowTo
    Dim strsql As String
    Dim Rstdb As New ADODB.Recordset
    Private Sub HowTo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        strsql = "select * from applicazioni"
        Rstdb.Open(strsql, Main.Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        If Rstdb.RecordCount <> 0 Then
            CmbTipo.Items.Clear()

            While Rstdb.EOF <> True
                CmbTipo.Items.Add(Rstdb("DescApplicazione").Value & "    -    " & Rstdb("IdApplicazione").Value)
                Rstdb.MoveNext()
            End While
        End If

        Rstdb.Close()

    End Sub

    Private Sub CmbTipo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbTipo.SelectedValueChanged
        Dim Posizione As Short = 0
        Dim StringaAppoggio As String
        Dim IdApp As String

        Posizione = InStr(CmbTipo.Text, "    -    ")
        'NomeHowto = Mid(CmbTipo.Text, 1, (Posizione - 1))
        StringaAppoggio = Mid(CmbTipo.Text, Posizione, Len(CmbTipo.Text))
        IdApp = LTrim(Mid(StringaAppoggio, InStr(StringaAppoggio, "-") + 1))

        HowtoTableAdapter.Fill(Me.GestioneHDDataSet.howto, IdApp)

        TxtCerca.ResetText()
    End Sub

    Private Sub GrigliaHowTo_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrigliaHowTo.CellClick
        strsql = "select * from howto where idhowto=" & GrigliaHowTo.Rows(e.RowIndex).Cells("IdHowTo").Value.ToString()
        Rstdb.Open(strsql, Main.Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        TxtProblema.Text = Rstdb("problemahowto").Value.ToString
        TxtSoluzione.Text = Rstdb("soluzionehowto").Value.ToString

        Rstdb.Close()
    End Sub

    Private Sub CmdCerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCerca.Click
        If TxtCerca.Text = "" Then
            MsgBox("Inserire la parola da cercare negli HowTo", MsgBoxStyle.Information)
            Exit Sub
        End If

        TxtProblema.ResetText()
        TxtSoluzione.ResetText()
        CmbTipo.ResetText()

        HowtoTableAdapter.FillByTutti(Me.GestioneHDDataSet.howto)
     
        Dim x As Short
        With GrigliaHowTo
            GrigliaHowTo.CurrentCell = Nothing
            For x = 0 To .RowCount - 1
                If .Rows(x).Cells("ProblemaCol").Value.ToString.ToLower.Contains(TxtCerca.Text.ToLower) OrElse .Rows(x).Cells("SoluzioneCol").Value.ToString.ToLower.Contains(TxtCerca.Text.ToLower) Then
                    .Rows(x).Visible = True
                Else
                    .Rows(x).Visible = False
                End If
            Next
        End With
    End Sub

    Private Sub TxtCerca_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCerca.Click
        If TxtCerca.TextLength > 0 Then TxtCerca.SelectAll()
    End Sub
End Class