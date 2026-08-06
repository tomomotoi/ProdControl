Public Class CoefficientsForm
    Private Sub CoefficientsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'OperationDBDataSet3.Coefficients' table. You can move, or remove it, as needed.
        Me.CoefficientsTableAdapter.FillBy(Me.OperationDBDataSet3.Coefficients)

    End Sub

    Private Sub ckbxEditMode_CheckedChanged(sender As Object, e As EventArgs) Handles ckbxEditMode.CheckedChanged
        allowEdit(ckbxEditMode.Checked)
    End Sub

    Private Sub allowEdit(allow As Boolean)
        btnAdd.Enabled = allow
        btnDelete.Enabled = allow
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim selectedIndex = lbCoefficients.SelectedIndex
        Dim selectedValue = lbCoefficients.SelectedValue
        Dim selectedDouble = Double.Parse(lbCoefficients.GetItemText(lbCoefficients.SelectedItem))

        CoefficientsTableAdapter.Delete(selectedValue, selectedDouble)
        CoefficientsTableAdapter.FillBy(Me.OperationDBDataSet3.Coefficients) 'update data after delete to display changes
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim newCoefficientValue As Object = tbNewCoefficientValue.Text

        If IsNumeric(newCoefficientValue) Then
            Dim parsedValue As Double = Double.Parse(newCoefficientValue)

            If Not coefficientExists(parsedValue) Then
                CoefficientsTableAdapter.Insert(parsedValue)
                CoefficientsTableAdapter.FillBy(Me.OperationDBDataSet3.Coefficients) 'update data after insert to display changes
            Else
                MessageBox.Show("Данный коэффициент уже присутствует в базе данных!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        Else
            MessageBox.Show("Число не соответствует числовому формату или не было задано!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function coefficientExists(coefficient As Object) As Boolean
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand

        connection = CoefficientsTableAdapter.Connection

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand
            sqlCommand.CommandText = "SELECT coefficient_value FROM Coefficients WHERE coefficient_value = @newValue"
            sqlCommand.Parameters.AddWithValue("@newValue", coefficient).DbType = DbType.Double

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()
            If dbReader.HasRows() Then
                Return True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return False
    End Function
End Class