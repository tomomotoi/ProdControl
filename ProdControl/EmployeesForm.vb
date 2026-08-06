Public Class EmployeesForm
    Dim employeesIdsForRemoval As List(Of Integer) = New List(Of Integer)
    Dim employeesRowsForUpdate As Dictionary(Of Integer, Object()) = New Dictionary(Of Integer, Object())
    Dim rowsAddedIndexes As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
    Dim allowCellValueChanged As Boolean = False

    Private Sub EmployeesForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PositionTableAdapter.Fill(Me.OperationDBDataSet4.Position)

        cbFilter.SelectedIndex = -1
    End Sub

    Private Function searchEmployeesWithQuery(query As String) As List(Of Object())
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim queryResult As List(Of Object()) = New List(Of Object())

        connection = New OleDb.OleDbConnection(My.Settings.OperationDBConnectionString)

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = query

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

            While dbReader.Read()
                Dim data As Object() = {
                    dbReader("EmployeeID"),
                    dbReader("EmployeeName"),
                    dbReader("EmployeeNumber"),
                    dbReader("PositionID"),
                    dbReader("PositionLevelID"),
                    vbFalse,
                    vbFalse
                }

                queryResult.Add(data)
            End While

            allowCellValueChanged = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return queryResult
    End Function

    Private Sub updateDbViewByQuery(query As String)
        Dim filteredData As List(Of Object()) = searchEmployeesWithQuery(query)

        dgView.Rows.Clear()

        For Each dataPiece In filteredData
            dgView.Rows.Add(dataPiece)
        Next
    End Sub

    Private Sub btnSearchAll_Click(sender As Object, e As EventArgs) Handles btnSearchAll.Click
        employeesIdsForRemoval.Clear()

        Me.PositionTableAdapter.Fill(Me.OperationDBDataSet4.Position)
        Dim query As String = "SELECT * FROM Employee;"

        updateDbViewByQuery(query)
    End Sub

    Private Sub chckbxAllowEdit_CheckedChanged(sender As Object, e As EventArgs) Handles chckbxAllowEdit.CheckedChanged
        setEditingControlsEditMode(chckbxAllowEdit.Checked)
    End Sub
    Private Sub setEditingControlsEditMode(isEditable As Boolean)
        dgView.ReadOnly = Not isEditable
        btnDelete.Enabled = isEditable
        btnAdd.Enabled = isEditable
        btnSave.Enabled = isEditable
    End Sub

    Private Sub сbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
        If cbFilter.SelectedIndex = 0 Then
            txSearchBox.Text = ""
            txSearchBox.Visible = True
            labelEmplName.Visible = True

            labelEmplNumber.Visible = False
            labelPosition.Visible = False
            labelPositionLevel.Visible = False

            cbSearchPosition.Visible = False

        ElseIf cbFilter.SelectedIndex = 1 Then
            txSearchBox.Text = ""
            txSearchBox.Visible = True
            labelEmplNumber.Visible = True

            labelEmplName.Visible = False
            labelPosition.Visible = False
            labelPositionLevel.Visible = False

            cbSearchPosition.Visible = False

        ElseIf cbFilter.SelectedIndex = 2 Then
            txSearchBox.Text = ""
            txSearchBox.Visible = False
            labelEmplNumber.Visible = False

            labelEmplName.Visible = False
            labelPosition.Visible = True
            labelPositionLevel.Visible = False

            cbSearchPosition.Visible = True
        ElseIf cbFilter.SelectedIndex = 3 Then
            txSearchBox.Text = ""
            txSearchBox.Visible = True
            labelEmplName.Visible = False

            labelEmplName.Visible = False
            labelPosition.Visible = False
            labelPositionLevel.Visible = True

            cbSearchPosition.Visible = False
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        employeesIdsForRemoval.Clear()

        Dim cbFilterSelectedIndex As Integer = cbFilter.SelectedIndex

        Select Case cbFilterSelectedIndex
            Case 0
                Dim txSearchBoxText As String = txSearchBox.Text
                Dim query As String = String.Format("SELECT * FROM Employee WHERE EmployeeName LIKE '%{0}%';", txSearchBoxText)

                updateDbViewByQuery(query)

            Case 1
                Dim txSearchBoxText As String = txSearchBox.Text
                Dim query As String = String.Format("SELECT * FROM Employee WHERE EmployeeNumber LIKE '%{0}%';", txSearchBoxText)

                updateDbViewByQuery(query)

            Case 2
                Dim cbPositionSelectedValue As Integer = cbSearchPosition.SelectedValue
                Dim query As String = String.Format("SELECT * FROM Employee WHERE PositionID = {0};", cbPositionSelectedValue)

                updateDbViewByQuery(query)

            Case 3
                Dim txSearchBoxValue As Integer

                If Integer.TryParse(txSearchBox.Text, txSearchBoxValue) Then
                    Dim query As String = String.Format("SELECT * FROM Employee WHERE PositionLevelID = {0};", txSearchBoxValue)

                    updateDbViewByQuery(query)
                Else
                    MessageBox.Show("Поле 'Разряд' должно быть целочисленным!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
        End Select
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        dgView.Rows.Add()

        Dim addedRow As DataGridViewRow = dgView.Rows.Item(dgView.Rows.Count - 1)
        addedRow.Cells("isNew").Value = vbTrue

        rowsAddedIndexes.Add(addedRow)
    End Sub

    Private Sub clearRowsAddedListIfWasRemovedByRow(dgRow As DataGridViewRow)
        If rowsAddedIndexes.Contains(dgRow) Then
            rowsAddedIndexes.Remove(dgRow)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim selectedRow As DataGridViewRow = dgView.CurrentRow
        Dim selectedRowIndex As Integer = selectedRow.Index

        employeesIdsForRemoval.Add(selectedRow.Cells(0).Value)

        dgView.Rows.RemoveAt(selectedRowIndex)

        clearRowsAddedListIfWasRemovedByRow(selectedRow)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim changesSaved As Boolean = False

        If employeesIdsForRemoval.Count > 0 Then
            For Each employeeId As Integer In employeesIdsForRemoval
                EmployeeTableAdapter1.Delete(employeeId)
            Next

            changesSaved = True
            employeesIdsForRemoval.Clear()
        End If

        If rowsAddedIndexes.Count > 0 Then
            For Each row As DataGridViewRow In rowsAddedIndexes
                If Not IsNothing(row.Cells(1).Value) And
                    Not IsNothing(row.Cells(2).Value) And
                    Not IsNothing(row.Cells(3).Value) And
                    Not IsNothing(row.Cells(4).Value) Then

                    EmployeeTableAdapter1.Insert(row.Cells(1).Value, row.Cells(2).Value, Integer.Parse(row.Cells(3).Value), Integer.Parse(row.Cells(4).Value))

                Else
                    MessageBox.Show("Для добавления нового поля все ячейки должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                row.Cells("isNew").Value = vbFalse
            Next

            fetchDateAfterInsertion()

            changesSaved = True
            rowsAddedIndexes.Clear()
        End If

        If employeesRowsForUpdate.Count > 0 Then
            For Each keyValuePair In employeesRowsForUpdate
                Dim rowObject As Object() = keyValuePair.Value

                'rowObject(0) id of a record
                'TODO: Crashes when incorrect data format
                EmployeeTableAdapter1.Update(rowObject(1), rowObject(2), Integer.Parse(rowObject(3)), Integer.Parse(rowObject(4)), rowObject(0))

            Next

            changesSaved = True
            employeesRowsForUpdate.Clear()
        End If

        If changesSaved Then
            MessageBox.Show("Изменения сохранены в базе данных!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Нет изменений для сохранения!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub fetchDateAfterInsertion()
        Dim query As String = "SELECT * FROM Employee;"

        updateDbViewByQuery(query)
    End Sub

    Private Sub dgView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellValueChanged
        If allowCellValueChanged Then
            Dim changedRow As DataGridViewRow = dgView.Rows().Item(e.RowIndex)

            If Not changedRow.Cells("isNew").Value = vbTrue And Not e.ColumnIndex = 6 Then
                Dim rowObject As Object() = {changedRow.Cells(0).Value,
                    changedRow.Cells(1).Value,
                    changedRow.Cells(2).Value,
                    changedRow.Cells(3).Value,
                    changedRow.Cells(4).Value}

                If Integer.TryParse(rowObject(4), Nothing) Then
                    If employeesRowsForUpdate.ContainsKey(rowObject(0)) Then
                        employeesRowsForUpdate.Remove(rowObject(0))
                        employeesRowsForUpdate.Add(rowObject(0), rowObject)
                    Else
                        employeesRowsForUpdate.Add(rowObject(0), rowObject)
                    End If
                Else MessageBox.Show("Поле 'Разряд должности' должно быть целочисленным!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End If
    End Sub
End Class