Public Class newArticulForm

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FillByToolStripButton_Click(sender As Object, e As EventArgs)
        Try
            Me.ArticulTableAdapter1.FillBy(Me.DBSet.Articul)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim res As Integer = ArticulTableAdapter1.Insert(txtArt.Text, txtDesc.Text)
        If res > 0 Then
            MessageBox.Show("Новый Артикул " + txtArt.Text + " сохранен", "Артикул", MessageBoxButtons.OK)
        End If
    End Sub
End Class