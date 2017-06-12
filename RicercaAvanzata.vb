Public Class RicercaAvanzata

    Private Sub CmdCercaImpianto_Click(sender As System.Object, e As System.EventArgs) Handles CmdCercaImpianto.Click
        If TxtCercaImpianto.Text = "" Then
            MsgBox("Inserire la parola da cercare negli Impianti", MsgBoxStyle.Information)
            Exit Sub
        End If
        Dim Rstdb_impianti As New ADODB.Recordset

        Dim strsql As String
        strsql = "select * from impianti where attivoimpianto='1' and nomeimpianto like '%" & TxtCercaImpianto.Text & "%' order by nomeimpianto"
        'Rstdb_impianti.Open(strsql, Cnndb, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)


    End Sub
End Class