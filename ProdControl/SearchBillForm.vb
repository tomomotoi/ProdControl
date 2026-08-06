Public Class SearchBillForm
    Dim logIdsForRemoval As List(Of Integer) = New List(Of Integer)
    Dim logRowsForUpdate As Dictionary(Of Integer, Object()) = New Dictionary(Of Integer, Object())
    Dim allowCellValueChanged As Boolean = False

    Dim dateTimePicker As DateTimePicker = New DateTimePicker()
    Dim rectangle As Rectangle

    Private Sub SearchBillForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'OperationDBDataSet1.OperationLog1' table. You can move, or remove it, as needed.
        Me.OperationLog1TableAdapter.Fill(Me.OperationDBDataSet1.OperationLog1)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Operation' table. You can move, or remove it, as needed.
        Me.OperationTableAdapter.Fill(Me.OperationDBDataSet1.Operation)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Articul' table. You can move, or remove it, as needed.
        Me.ArticulTableAdapter.Fill(Me.OperationDBDataSet1.Articul)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Employee' table. You can move, or remove it, as needed.
        Me.EmployeeTableAdapter.Fill(Me.OperationDBDataSet1.Employee)

        cbFilter.SelectedIndex = 0

        dgView.Controls.Add(dateTimePicker)
        dateTimePicker.Format = DateTimePickerFormat.Custom
        dateTimePicker.Visible = False
        dateTimePicker.Enabled = False
        AddHandler dateTimePicker.ValueChanged, AddressOf dateTimePicker_ValueChanged
    End Sub

    Private Sub dateTimePicker_ValueChanged(sender As Object, e As EventArgs)
        dgView.BeginEdit(True)
        dgView.CurrentCell.Value = dateTimePicker.Value
        dgView.EndEdit(True)
    End Sub

    Private Sub btnSearchAll_Click(sender As Object, e As EventArgs) Handles btnSearchAll.Click
        Dim query As String = "SELECT * FROM OperationLog;"

        updateDbViewByQuery(query)
    End Sub

    Private Function searchBillsWithQuery(query As String) As List(Of Object())
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim queryResult As List(Of Object()) = New List(Of Object())

        connection = OperationLog1TableAdapter.Connection

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = query

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

            While dbReader.Read()
                Dim data As Object() = {
                    dbReader("LogID"),
                    dbReader("EmployeeID"),
                    dbReader("OperationID"),
                    dbReader("ArticulID"),
                    dbReader("DateExecution"),
                    dbReader("TimeExecution"),
                    dbReader("Count"),
                    dbReader("DocumentNumber"),
                    dbReader("workDay"),
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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim selectedRow As DataGridViewRow = dgView.CurrentRow
        Dim selectedRowIndex As Integer = selectedRow.Index

        logIdsForRemoval.Add(selectedRow.Cells(0).Value)

        dgView.Rows.RemoveAt(selectedRowIndex)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim changesSaved As Boolean = False

        If logIdsForRemoval.Count > 0 Then
            For Each logId As Integer In logIdsForRemoval
                OperationLog1TableAdapter.Delete(logId)
            Next

            changesSaved = True
            logIdsForRemoval.Clear()
        End If

        If logRowsForUpdate.Count > 0 Then
            For Each keyValuePair In logRowsForUpdate
                Dim rowObject As Object() = keyValuePair.Value

                OperationLog1TableAdapter.Update(rowObject(1), rowObject(2), rowObject(3), rowObject(4), Integer.Parse(rowObject(5)), Integer.Parse(rowObject(6)), rowObject(7), Integer.Parse(rowObject(8)), rowObject(0))
            Next

            changesSaved = True
            logRowsForUpdate.Clear()
        End If

        If changesSaved Then
            MessageBox.Show("Изменения сохранены в базе данных!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Нет изменений для сохранения!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    'note: this method is being triggered when first opening SearchBillForm
    Private Sub dgView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellValueChanged
        If allowCellValueChanged Then
            Dim changedRow As DataGridViewRow = dgView.Rows().Item(e.RowIndex)

            Dim rowObject As Object() = {changedRow.Cells(0).Value,
            changedRow.Cells(1).Value,
            changedRow.Cells(2).Value,
            changedRow.Cells(3).Value,
            changedRow.Cells(4).Value,
            changedRow.Cells(5).Value,
            changedRow.Cells(6).Value,
            changedRow.Cells(7).Value,
            changedRow.Cells(8).Value}

            If Integer.TryParse(rowObject(5), Nothing) And Integer.TryParse(rowObject(6), Nothing) And Integer.TryParse(rowObject(8), Nothing) Then
                If logRowsForUpdate.ContainsKey(rowObject(0)) Then
                    logRowsForUpdate.Remove(rowObject(0))
                    logRowsForUpdate.Add(rowObject(0), rowObject)
                Else
                    logRowsForUpdate.Add(rowObject(0), rowObject)
                End If
            Else
                MessageBox.Show("Поля 'Время выполнения', 'Количество' и 'Рабочий день' должны быть целочисленными!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim datePeriodQuery As String = ""

        If chckbxByPeriod.Checked Then
            Dim dateFrom As Date = datePickerFrom.Value
            Dim dateTo As Date = datePickerTo.Value

            datePeriodQuery = String.Format(" AND (DateExecution >= #{0}# AND DateExecution <= #{1}#)", dateFrom.ToString("d", Globalization.CultureInfo.InvariantCulture), dateTo.ToString("d", Globalization.CultureInfo.InvariantCulture))
        End If

        If cbFilter.SelectedIndex = 0 Then
            Dim query As String = String.Format("SELECT * FROM OperationLog WHERE EmployeeID = {0}{1}", cbEmployee.SelectedValue, datePeriodQuery)

            updateDbViewByQuery(query)

        ElseIf cbFilter.SelectedIndex = 1 Then
            Dim query As String = String.Format("SELECT * FROM OperationLog WHERE ArticulID = {0}{1}", cbArticul.SelectedValue, datePeriodQuery)

            updateDbViewByQuery(query)

        ElseIf cbFilter.SelectedIndex = 2 Then
            Dim query As String = String.Format("SELECT * FROM OperationLog WHERE EmployeeID = {0} AND ArticulID = {1}{2}", cbEmployee.SelectedValue, cbArticul.SelectedValue, datePeriodQuery)

            updateDbViewByQuery(query)

        ElseIf cbFilter.SelectedIndex = 3 Then
            Dim query As String = String.Format("SELECT * FROM OperationLog WHERE DocumentNumber LIKE '{0}'{1}", txDocNum.Text, datePeriodQuery)

            updateDbViewByQuery(query)

        End If
    End Sub

    Private Sub updateDbViewByQuery(query As String)
        Dim filteredData As List(Of Object()) = searchBillsWithQuery(query)

        dgView.Rows.Clear()

        For Each dataPiece In filteredData
            dgView.Rows.Add(dataPiece)
        Next
    End Sub

    Private Sub сbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
        Dim filterCb As ComboBox = DirectCast(sender, ComboBox)

        If filterCb.SelectedIndex = 0 Then 'by employee
            cbEmployee.Visible = True
            cbEmployee.Enabled = True

            labelEmployee.Visible = True

            cbArticul.Visible = False
            cbArticul.Enabled = False
            cbArticul.Location = New Point(7, 80)

            labelArticul.Visible = False
            labelArticul.Location = New Point(7, 60)

            txDocNum.Visible = False
            txDocNum.Enabled = False

            labelDocNum.Visible = False

        ElseIf filterCb.SelectedIndex = 1 Then 'by article
            cbEmployee.Visible = False
            cbEmployee.Enabled = False

            labelEmployee.Visible = False

            cbArticul.Visible = True
            cbArticul.Enabled = True
            cbArticul.Location = New Point(6, 33)

            labelArticul.Visible = True
            labelArticul.Location = New Point(6, 13)

            txDocNum.Visible = False
            txDocNum.Enabled = False

            labelDocNum.Visible = False

        ElseIf filterCb.SelectedIndex = 2 Then 'by employee + article
            cbEmployee.Visible = True
            cbEmployee.Enabled = True

            labelEmployee.Visible = True

            cbArticul.Visible = True
            cbArticul.Enabled = True
            cbArticul.Location = New Point(7, 80)

            labelArticul.Visible = True
            labelArticul.Location = New Point(7, 60)

            txDocNum.Visible = False
            txDocNum.Enabled = False

            labelDocNum.Visible = False

        ElseIf filterCb.SelectedIndex = 3 Then 'by document number
            cbEmployee.Visible = False
            cbEmployee.Enabled = False

            labelEmployee.Visible = False

            cbArticul.Visible = False
            cbArticul.Enabled = False
            cbArticul.Location = New Point(7, 80)

            labelArticul.Visible = False
            cbArticul.Location = New Point(7, 60)

            txDocNum.Visible = True
            txDocNum.Enabled = True

            labelDocNum.Visible = True

        End If
    End Sub

    Private Sub chckbxByPeriod_CheckedChanged(sender As Object, e As EventArgs) Handles chckbxByPeriod.CheckedChanged
        Dim withDateCheckBox As CheckBox = DirectCast(sender, CheckBox)

        If withDateCheckBox.Checked Then
            labelDateFrom.Visible = True
            labelDateTo.Visible = True

            datePickerFrom.Enabled = True
            datePickerFrom.Visible = True

            datePickerTo.Enabled = True
            datePickerTo.Visible = True

        Else
            labelDateFrom.Visible = False
            labelDateTo.Visible = False

            datePickerFrom.Enabled = False
            datePickerFrom.Visible = False

            datePickerTo.Enabled = False
            datePickerTo.Visible = False
        End If
    End Sub

    Private Sub dgView_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellDoubleClick
        If e.ColumnIndex = 4 Then
            rectangle = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)

            dateTimePicker.Size = New Size(rectangle.Width, rectangle.Height)
            dateTimePicker.Location = New Point(rectangle.X, rectangle.Y)
            dateTimePicker.Visible = True

            dateTimePicker.Value = Date.Parse(dgView.CurrentCell.Value)
        End If
    End Sub

    Private Sub dgView_Scroll(sender As Object, e As ScrollEventArgs) Handles dgView.Scroll
        hideEditDatePicker()
    End Sub

    Private Sub dgView_CurrentCellChanged(sender As Object, e As EventArgs) Handles dgView.CurrentCellChanged
        hideEditDatePicker()
    End Sub

    Private Sub hideEditDatePicker()
        If dateTimePicker.Visible Then
            dateTimePicker.Visible = False
        End If
    End Sub

    Private Sub chckbxEditingMode_CheckedChanged(sender As Object, e As EventArgs) Handles chckbxEditingMode.CheckedChanged
        setEditingControlsEditMode(chckbxEditingMode.Checked)
    End Sub

    Private Sub setEditingControlsEditMode(isEditable As Boolean)
        dgView.ReadOnly = Not isEditable
        dateTimePicker.Enabled = isEditable
        btnDelete.Enabled = isEditable
        btnSave.Enabled = isEditable
    End Sub
End Class