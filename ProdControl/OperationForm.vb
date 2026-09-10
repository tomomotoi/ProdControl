Public Class OperationForm
    Private Sub OperationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Operation' table. You can move, or remove it, as needed.
        Me.OperationTableAdapter.Fill(Me.OperationDBDataSet1.Operation)
        'TODO: This line of code loads data into the 'OperationDBDataSet4.Position' table. You can move, or remove it, as needed.
        Me.PositionTableAdapter.Fill(Me.OperationDBDataSet4.Position)

    End Sub

    Private Sub btnAddOperation_Click(sender As Object, e As EventArgs) Handles btnAddOperation.Click
        Dim operationName As String = txNewOperationName.Text
        Dim operationProfession As Integer = cbProfession.SelectedValue

        Try
            If Not checkIfOperationExists(operationName) And Not (txNewOperationName.Text = "") Then
                With OperationBindingSource
                    .AddNew()
                    DirectCast(.Current, DataRowView)("OperationName") = operationName
                    DirectCast(.Current, DataRowView)("OperationKoefAddCost") = 0
                    DirectCast(.Current, DataRowView)("OperationTimewithKoef") = 0
                    DirectCast(.Current, DataRowView)("OpetationRate") = 0
                    DirectCast(.Current, DataRowView)("PositionID") = operationProfession
                    .EndEdit()
                End With
                OperationTableAdapter.Update(OperationDBDataSet1.Operation)
                refreshOperationTableAdapter()

                MessageBox.Show("Операция была успешно добавлена в базу данных!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf txNewOperationName.Text = "" Then
                MessageBox.Show("Поле 'Название новой операции' не должно быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Else
                MessageBox.Show("Такая операция уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function checkIfOperationExists(operationName As String) As Boolean
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim exists As Boolean = False

        connection = PositionTableAdapter.Connection

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = "SELECT * FROM Operation WHERE OperationName LIKE @OperationName"
            sqlCommand.Parameters.AddWithValue("@OperationName", operationName)

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

            While dbReader.Read()
                exists = True
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return exists
    End Function

    Private Sub btnDeleteOperation_Click(sender As Object, e As EventArgs) Handles btnDeleteOperation.Click
        Dim selectedOperation As Object = lbExistingOperations.SelectedItem
        Dim selectedId As Integer = lbExistingOperations.SelectedValue

        Try
            If Not checkIfOperationIsUsed(selectedId) Then
                With OperationBindingSource
                    .Remove(selectedOperation)
                    .EndEdit()
                End With

                OperationTableAdapter.Delete(selectedId)
                refreshOperationTableAdapter()

            Else
                MessageBox.Show("Данная операция используется в текущих производственных процессах!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function checkIfOperationIsUsed(operationId As Integer) As Boolean
        Dim isUsed As Boolean = False
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand

        connection = OperationTableAdapter.Connection

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = "SELECT * FROM Routing WHERE OperationID = @OperationID"
            sqlCommand.Parameters.AddWithValue("@OperationName", operationId)

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

            While dbReader.Read()
                isUsed = True
                Exit While
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return isUsed
    End Function

    Private Sub refreshOperationTableAdapter()
        Me.OperationTableAdapter.Fill(Me.OperationDBDataSet1.Operation)
    End Sub

    Private Sub txSearchOperation_TextChanged(sender As Object, e As EventArgs) Handles txSearchOperation.TextChanged
        Dim searchString As String = txSearchOperation.Text
        Dim searchedOperationIndex As Integer = lbExistingOperations.FindString(searchString)

        lbExistingOperations.SelectedIndex = searchedOperationIndex
    End Sub

    Private Sub lbExistingOperations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbExistingOperations.SelectedIndexChanged
        Dim selectedOperationId = lbExistingOperations.SelectedValue

        txExistingOperationProfession.Text = getExistingOperationProfession(selectedOperationId)
    End Sub

    Private Function getExistingOperationProfession(operationId As Integer) As String
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim queryResult As String = ""

        connection = New OleDb.OleDbConnection(My.Settings.OperationDBConnectionString)

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = String.Format("SELECT [Position].[PositionName] FROM [Operation] INNER JOIN [Position] ON [Operation].[PositionID] = [Position].[PositionID] WHERE [Operation].[OperationID] = {0}", operationId)

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

            While dbReader.Read
                queryResult = DirectCast(dbReader("PositionName"), String)
            End While

            Return queryResult
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return queryResult
    End Function
End Class