
Public Class RdpUser
    Public ArrayUtente As New ArrayList
    Public ArrayPwd As New ArrayList
    Public NrRiga As Short

    Private Sub RdpUser_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ArrayUtente.Clear()
        ArrayPwd.Clear()
    End Sub

    Private Sub RdpUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CmbUser.Items.Clear()

        For a = 0 To ArrayUtente.Count - 1
            CmbUser.Items.Add(ArrayUtente(a))
        Next
    End Sub

    Private Sub CmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdOk.Click
        Try
            Main.GrigliaServer.SelectedRows(NrRiga).Cells("UsernameCol").Value = ArrayUtente(CmbUser.SelectedIndex)
            Main.GrigliaServer.SelectedRows(NrRiga).Cells("PasswordCol").Value = ArrayPwd(CmbUser.SelectedIndex)
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Information)
        End Try

        CmbUser.Text = ""
        Me.Close()
    End Sub
End Class