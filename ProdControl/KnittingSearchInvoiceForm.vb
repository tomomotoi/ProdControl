Imports System.Data.OleDb

Public Class KnittingSearchInvoiceForm
    Private connectionString As String = My.Settings.OperationDBConnectionString
    Private isLoading As Boolean = False
    Private isUpdatingPicker As Boolean = False
    Private dtpCellPicker As New DateTimePicker()

    ' ==========================================
    ' 1. ИНИЦИАЛИЗАЦИЯ И НАСТРОЙКА ФОРМЫ
    ' ==========================================
    Private Sub KnittingSearchInvoiceForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.EmployeeTableAdapter.Fill(Me.OperationDBDataSet0.Employee)
            Me.ArticulTableAdapter.Fill(Me.OperationDBDataSet0.Articul)
        Catch ex As Exception
            ' Игнорируем ошибки адаптера, если таблицы заполняются вручную в LoadDictionaries
        End Try

        ' Всплывающий календарь для ячеек даты
        dtpCellPicker.Format = DateTimePickerFormat.Short
        dtpCellPicker.Visible = False
        AddHandler dtpCellPicker.ValueChanged, AddressOf dtpCellPicker_ValueChanged
        dgView.Controls.Add(dtpCellPicker)

        LoadDictionaries()

        If cbFilter.Items.Count > 0 Then cbFilter.SelectedIndex = 0
        UpdateFilterVisibility()
        chckbxByPeriod.Checked = False
        datePickerFrom.Enabled = True
        datePickerTo.Enabled = True

        chckbxEditingMode.Checked = False
        SetEditingMode(False)

        ResizeGridColumns(dgView)
    End Sub

    Private Sub LoadDictionaries()
        Using conn As New OleDbConnection(connectionString)
            Try
                conn.Open()
            Catch ex As Exception
                MessageBox.Show("Ошибка подключения к БД: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            ' 1. СОТРУДНИКИ
            Try
                Dim dtEmp As New DataTable()
                Using da As New OleDbDataAdapter("SELECT EmployeeID, EmployeeName FROM Employee", conn)
                    da.Fill(dtEmp)
                End Using

                cbEmployee.DataSource = dtEmp
                cbEmployee.DisplayMember = "EmployeeName"
                cbEmployee.ValueMember = "EmployeeID"

                If dgView.Columns.Contains("EmployeeName") AndAlso TypeOf dgView.Columns("EmployeeName") Is DataGridViewComboBoxColumn Then
                    Dim colCombo = DirectCast(dgView.Columns("EmployeeName"), DataGridViewComboBoxColumn)
                    colCombo.DataSource = dtEmp
                    colCombo.DisplayMember = "EmployeeName"
                    colCombo.ValueMember = "EmployeeID"
                    colCombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                End If
            Catch ex As Exception
                MessageBox.Show("Ошибка в справочнике Employee: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' 2. АРТИКУЛЫ
            Try
                Dim dtArt As New DataTable()
                Using da As New OleDbDataAdapter("SELECT ArticulID, ArticulName FROM Articul", conn)
                    da.Fill(dtArt)
                End Using

                cbArticul.DataSource = dtArt
                cbArticul.DisplayMember = "ArticulName"
                cbArticul.ValueMember = "ArticulID"

                If dgView.Columns.Contains("ArticulName") AndAlso TypeOf dgView.Columns("ArticulName") Is DataGridViewComboBoxColumn Then
                    Dim colCombo = DirectCast(dgView.Columns("ArticulName"), DataGridViewComboBoxColumn)
                    colCombo.DataSource = dtArt
                    colCombo.DisplayMember = "ArticulName"
                    colCombo.ValueMember = "ArticulID"
                    colCombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                End If
            Catch ex As Exception
                MessageBox.Show("Ошибка в справочнике Articul: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' 3. ДЕТАЛИ
            Try
                Dim dtPart As New DataTable()
                Using da As New OleDbDataAdapter("SELECT id, part_name FROM ClothingParts", conn)
                    da.Fill(dtPart)
                End Using

                If dgView.Columns.Contains("KnittingPartName") AndAlso TypeOf dgView.Columns("KnittingPartName") Is DataGridViewComboBoxColumn Then
                    Dim colCombo = DirectCast(dgView.Columns("KnittingPartName"), DataGridViewComboBoxColumn)
                    colCombo.DataSource = dtPart
                    colCombo.DisplayMember = "part_name"
                    colCombo.ValueMember = "id"
                    colCombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                End If
            Catch ex As Exception
                MessageBox.Show("Ошибка в справочнике ClothingParts: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' 4. ОБОРУДОВАНИЕ
            Try
                Dim dtMach As New DataTable()
                Using da As New OleDbDataAdapter("SELECT id, machine_make + ' ' + machine_model AS machine_displayname FROM KnittingMachines", conn)
                    da.Fill(dtMach)
                End Using

                If dgView.Columns.Contains("KnittingMachine") AndAlso TypeOf dgView.Columns("KnittingMachine") Is DataGridViewComboBoxColumn Then
                    Dim colCombo = DirectCast(dgView.Columns("KnittingMachine"), DataGridViewComboBoxColumn)
                    colCombo.DataSource = dtMach
                    colCombo.DisplayMember = "machine_displayname"
                    colCombo.ValueMember = "id"
                    colCombo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                End If
            Catch ex As Exception
                MessageBox.Show("Ошибка в справочнике ОБОРУДОВАНИЕ: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 2. УПРАВЛЕНИЕ ИНТЕРФЕЙСОМ И ФИЛЬТРАМИ
    ' ==========================================
    Private Sub cbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilter.SelectedIndexChanged
        UpdateFilterVisibility()
    End Sub

    Private Sub UpdateFilterVisibility()
        LabelEmployee.Visible = False
        cbEmployee.Visible = False
        labelArticul.Visible = False
        cbArticul.Visible = False
        labelDocNum.Visible = False
        txDocNum.Visible = False

        Dim slot1Label As New Point(15, 10)
        Dim slot1Control As New Point(15, 30)
        Dim slot2Label As New Point(15, 65)
        Dim slot2Control As New Point(15, 85)

        Dim selectedFilter As String = If(cbFilter.SelectedItem IsNot Nothing, cbFilter.SelectedItem.ToString(), "")

        Select Case selectedFilter
            Case "По сотруднику"
                LabelEmployee.Location = slot1Label
                cbEmployee.Location = slot1Control
                LabelEmployee.Visible = True
                cbEmployee.Visible = True

            Case "По артикулу"
                labelArticul.Location = slot1Label
                cbArticul.Location = slot1Control
                labelArticul.Visible = True
                cbArticul.Visible = True

            Case "По номеру квитанции"
                labelDocNum.Location = slot1Label
                txDocNum.Location = slot1Control
                labelDocNum.Visible = True
                txDocNum.Visible = True

            Case "Сотрудник + артикул"
                LabelEmployee.Location = slot1Label
                cbEmployee.Location = slot1Control
                LabelEmployee.Visible = True
                cbEmployee.Visible = True

                labelArticul.Location = slot2Label
                cbArticul.Location = slot2Control
                labelArticul.Visible = True
                cbArticul.Visible = True
        End Select
    End Sub

    Private Sub chckbxByPeriod_CheckedChanged(sender As Object, e As EventArgs) Handles chckbxByPeriod.CheckedChanged
        Dim isVisible As Boolean = chckbxByPeriod.Checked
        datePickerFrom.Visible = isVisible
        datePickerTo.Visible = isVisible
        labelDateFrom.Visible = isVisible
        labelDateTo.Visible = isVisible
    End Sub

    Private Sub chckbxEditingMode_CheckedChanged(sender As Object, e As EventArgs) Handles chckbxEditingMode.CheckedChanged
        SetEditingMode(chckbxEditingMode.Checked)
    End Sub

    Private Sub SetEditingMode(isEditing As Boolean)
        dgView.ReadOnly = Not isEditing
        btnDelete.Enabled = isEditing
        btnSave.Enabled = isEditing

        If dgView.Columns.Contains("isChanged") Then dgView.Columns("isChanged").ReadOnly = True
        If dgView.Columns.Contains("LogId") Then dgView.Columns("LogId").ReadOnly = True
    End Sub

    ' ==========================================
    ' 3. ПОИСК И ЗАГРУЗКА ДАННЫХ
    ' ==========================================
    Private Sub btnSearchAll_Click(sender As Object, e As EventArgs) Handles btnSearchAll.Click
        ExecuteSearch(searchAll:=True)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ExecuteSearch(searchAll:=False)
    End Sub

    Private Sub ExecuteSearch(searchAll As Boolean)
        dtpCellPicker.Visible = False
        isLoading = True
        dgView.Rows.Clear()

        Dim sql As String = "SELECT id, document_number, article_id, employee_id, part_id, amount, machine_id, date_execution, time_execution, machines_in_zone, work_day " &
                            "FROM KnittingOperationLog WHERE 1=1"
        Dim parameters As New List(Of OleDbParameter)

        If Not searchAll Then
            Dim filterType As String = If(cbFilter.SelectedItem IsNot Nothing, cbFilter.SelectedItem.ToString(), "")

            Select Case filterType
                Case "По сотруднику"
                    If cbEmployee.SelectedValue IsNot Nothing AndAlso Not IsDBNull(cbEmployee.SelectedValue) Then
                        sql &= " AND employee_id = ?"
                        parameters.Add(New OleDbParameter("?", OleDbType.Integer) With {.Value = Convert.ToInt32(cbEmployee.SelectedValue)})
                    End If

                Case "По артикулу"
                    If cbArticul.SelectedValue IsNot Nothing AndAlso Not IsDBNull(cbArticul.SelectedValue) Then
                        sql &= " AND article_id = ?"
                        parameters.Add(New OleDbParameter("?", OleDbType.Integer) With {.Value = Convert.ToInt32(cbArticul.SelectedValue)})
                    End If

                Case "Сотрудник + артикул"
                    If cbEmployee.SelectedValue IsNot Nothing AndAlso Not IsDBNull(cbEmployee.SelectedValue) AndAlso
                       cbArticul.SelectedValue IsNot Nothing AndAlso Not IsDBNull(cbArticul.SelectedValue) Then
                        sql &= " AND employee_id = ? AND article_id = ?"
                        parameters.Add(New OleDbParameter("?", OleDbType.Integer) With {.Value = Convert.ToInt32(cbEmployee.SelectedValue)})
                        parameters.Add(New OleDbParameter("?", OleDbType.Integer) With {.Value = Convert.ToInt32(cbArticul.SelectedValue)})
                    End If

                Case "По номеру квитанции"
                    If Not String.IsNullOrWhiteSpace(txDocNum.Text) Then
                        sql &= " AND document_number LIKE ?"
                        parameters.Add(New OleDbParameter("?", OleDbType.VarChar) With {.Value = "%" & txDocNum.Text.Trim() & "%"})
                    End If
            End Select

            If chckbxByPeriod.Checked Then
                sql &= " AND date_execution >= ? AND date_execution <= ?"
                parameters.Add(New OleDbParameter("?", OleDbType.Date) With {.Value = datePickerFrom.Value.Date})
                parameters.Add(New OleDbParameter("?", OleDbType.Date) With {.Value = datePickerTo.Value.Date.AddDays(1).AddSeconds(-1)})
            End If
        End If

        sql &= " ORDER BY date_execution DESC, id DESC"

        Using conn As New OleDbConnection(connectionString)
            Try
                conn.Open()
                Using cmd As New OleDbCommand(sql, conn)
                    For Each p In parameters
                        cmd.Parameters.Add(p)
                    Next

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim rowIndex As Integer = dgView.Rows.Add()
                            Dim row As DataGridViewRow = dgView.Rows(rowIndex)

                            row.Cells("LogId").Value = reader("id")
                            row.Cells("DocumentNumber").Value = If(IsDBNull(reader("document_number")), "", reader("document_number").ToString())
                            row.Cells("ArticulName").Value = If(IsDBNull(reader("article_id")), DBNull.Value, Convert.ToInt32(reader("article_id")))
                            row.Cells("EmployeeName").Value = If(IsDBNull(reader("employee_id")), DBNull.Value, Convert.ToInt32(reader("employee_id")))
                            row.Cells("KnittingPartName").Value = If(IsDBNull(reader("part_id")), DBNull.Value, Convert.ToInt32(reader("part_id")))
                            row.Cells("Count").Value = If(IsDBNull(reader("amount")), 0, reader("amount"))
                            row.Cells("KnittingMachine").Value = If(IsDBNull(reader("machine_id")), DBNull.Value, Convert.ToInt32(reader("machine_id")))

                            If Not IsDBNull(reader("date_execution")) Then
                                row.Cells("DateExecution").Value = Convert.ToDateTime(reader("date_execution")).ToString("dd.MM.yyyy")
                            Else
                                row.Cells("DateExecution").Value = ""
                            End If

                            row.Cells("TimeExecution").Value = If(IsDBNull(reader("time_execution")), 0, reader("time_execution"))
                            row.Cells("MachinesInZone").Value = If(IsDBNull(reader("machines_in_zone")), 0, reader("machines_in_zone"))
                            row.Cells("workDay").Value = If(IsDBNull(reader("work_day")), 0, reader("work_day"))

                            row.Cells("isChanged").Value = False
                        End While
                    End Using
                End Using

                If dgView.Rows.Count = 0 Then
                    MessageBox.Show("Записи по заданным критериям не найдены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                MessageBox.Show("Ошибка при поиске данных: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                isLoading = False
            End Try
        End Using

        ResizeGridColumns(dgView)
    End Sub

    ' ==========================================
    ' 4. ОБРАБОТКА ВВОДА И ВАЛИДАЦИЯ
    ' ==========================================
    Private Sub dgView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellValueChanged
        If isLoading OrElse e.RowIndex < 0 Then Return

        If dgView.Columns(e.ColumnIndex).Name <> "isChanged" Then
            dgView.Rows(e.RowIndex).Cells("isChanged").Value = True
        End If
    End Sub

    Private Sub dgView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgView.DataError
        e.ThrowException = False
    End Sub

    Private Sub dgView_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgView.EditingControlShowing
        Dim txtBox As TextBox = TryCast(e.Control, TextBox)
        If txtBox IsNot Nothing Then
            RemoveHandler txtBox.KeyPress, AddressOf Cell_KeyPress_Integer
            RemoveHandler txtBox.KeyPress, AddressOf Cell_KeyPress_Decimal

            Dim colName As String = dgView.Columns(dgView.CurrentCell.ColumnIndex).Name

            If colName = "Count" OrElse colName = "workDay" OrElse colName = "MachinesInZone" Then
                AddHandler txtBox.KeyPress, AddressOf Cell_KeyPress_Integer
            ElseIf colName = "TimeExecution" Then
                AddHandler txtBox.KeyPress, AddressOf Cell_KeyPress_Decimal
            End If
        End If
    End Sub

    Private Sub Cell_KeyPress_Integer(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Cell_KeyPress_Decimal(sender As Object, e As KeyPressEventArgs)
        Dim txtBox As TextBox = CType(sender, TextBox)
        Dim decSep As Char = Convert.ToChar(Application.CurrentCulture.NumberFormat.NumberDecimalSeparator)

        If e.KeyChar = "."c OrElse e.KeyChar = ","c Then e.KeyChar = decSep

        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> decSep Then
            e.Handled = True
            Return
        End If

        If e.KeyChar = decSep AndAlso txtBox.Text.Contains(decSep.ToString()) AndAlso txtBox.SelectionLength = 0 Then
            e.Handled = True
            Return
        End If
    End Sub

    ' ==========================================
    ' 5. УПРАВЛЕНИЕ КАЛЕНДАРЕМ
    ' ==========================================
    Private Sub dgView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellClick
        If e.RowIndex < 0 OrElse Not chckbxEditingMode.Checked Then Return

        If dgView.Columns(e.ColumnIndex).Name = "DateExecution" Then
            Dim rect As Rectangle = dgView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
            dtpCellPicker.Size = rect.Size
            dtpCellPicker.Location = rect.Location

            isUpdatingPicker = True
            Dim currentDate As DateTime
            If DateTime.TryParse(Convert.ToString(dgView.CurrentCell.Value), currentDate) Then
                dtpCellPicker.Value = currentDate
            Else
                dtpCellPicker.Value = DateTime.Now
            End If
            isUpdatingPicker = False

            dtpCellPicker.Visible = True
        Else
            dtpCellPicker.Visible = False
        End If
    End Sub

    Private Sub dtpCellPicker_ValueChanged(sender As Object, e As EventArgs)
        If isUpdatingPicker Then Return

        If dgView.CurrentCell IsNot Nothing AndAlso dgView.Columns(dgView.CurrentCell.ColumnIndex).Name = "DateExecution" Then
            dgView.CurrentCell.Value = dtpCellPicker.Value.ToString("dd.MM.yyyy")
        End If
    End Sub

    Private Sub HideCellPicker(sender As Object, e As EventArgs) Handles dgView.SelectionChanged, dgView.ColumnWidthChanged, dgView.Scroll
        dtpCellPicker.Visible = False
    End Sub

    Private Sub dgView_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellLeave
        dtpCellPicker.Visible = False
    End Sub

    ' ==========================================
    ' 6. УДАЛЕНИЕ И СОХРАНЕНИЕ
    ' ==========================================
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If Not chckbxEditingMode.Checked Then Return

        If dgView.CurrentRow IsNot Nothing AndAlso Not dgView.CurrentRow.IsNewRow Then
            Dim result As DialogResult = MessageBox.Show("Вы действительно хотите удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Dim recordId As Object = dgView.CurrentRow.Cells("LogId").Value

                If recordId IsNot Nothing AndAlso Not IsDBNull(recordId) Then
                    Using conn As New OleDbConnection(connectionString)
                        Try
                            conn.Open()
                            Using cmd As New OleDbCommand("DELETE FROM KnittingOperationLog WHERE id = ?", conn)
                                cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(recordId)
                                cmd.ExecuteNonQuery()
                            End Using
                            MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As Exception
                            MessageBox.Show("Ошибка при удалении из БД: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End Try
                    End Using
                End If

                dgView.Rows.Remove(dgView.CurrentRow)
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not chckbxEditingMode.Checked Then Return

        Dim updatedCount As Integer = 0
        Dim sqlUpdate As String = "UPDATE [KnittingOperationLog] SET " &
            "[document_number] = ?, [article_id] = ?, [employee_id] = ?, [part_id] = ?, [amount] = ?, " &
            "[machine_id] = ?, [date_execution] = ?, [time_execution] = ?, [machines_in_zone] = ?, [work_day] = ? " &
            "WHERE [id] = ?"

        Using conn As New OleDbConnection(connectionString)
            Try
                conn.Open()
                Using transaction As OleDbTransaction = conn.BeginTransaction(),
                      cmd As New OleDbCommand(sqlUpdate, conn, transaction)

                    For Each row As DataGridViewRow In dgView.Rows
                        If row.IsNewRow Then Continue For

                        If Convert.ToBoolean(row.Cells("isChanged").Value) = True Then
                            cmd.Parameters.Clear()

                            ' document_number
                            Dim docNumVal As String = If(row.Cells("DocumentNumber").Value IsNot Nothing, row.Cells("DocumentNumber").Value.ToString().Trim(), "")
                            cmd.Parameters.Add("?", OleDbType.VarChar).Value = If(String.IsNullOrEmpty(docNumVal), DBNull.Value, docNumVal)

                            ' Идентификаторы и целочисленные значения
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = GetDbValueInt(row.Cells("ArticulName").Value)
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = GetDbValueInt(row.Cells("EmployeeName").Value)
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = GetDbValueInt(row.Cells("KnittingPartName").Value)
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = GetDbValueInt(row.Cells("Count").Value, defaultValue:=0)
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = GetDbValueInt(row.Cells("KnittingMachine").Value)

                            ' date_execution
                            Dim dateStr As String = Convert.ToString(row.Cells("DateExecution").Value)
                            Dim dtResult As DateTime
                            If DateTime.TryParse(dateStr, dtResult) Then
                                cmd.Parameters.Add("?", OleDbType.Date).Value = dtResult
                            Else
                                cmd.Parameters.Add("?", OleDbType.Date).Value = DBNull.Value
                            End If

                            ' Дробные и числовые показатели
                            cmd.Parameters.Add("?", OleDbType.Double).Value = GetDbValueDouble(row.Cells("TimeExecution").Value)
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = GetDbValueInt(row.Cells("MachinesInZone").Value, defaultValue:=0)
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = GetDbValueInt(row.Cells("workDay").Value, defaultValue:=0)

                            ' WHERE id
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(row.Cells("LogId").Value)

                            cmd.ExecuteNonQuery()

                            row.Cells("isChanged").Value = False
                            updatedCount += 1
                        End If
                    Next

                    transaction.Commit()

                    If updatedCount > 0 Then
                        MessageBox.Show($"Успешно обновлено записей: {updatedCount}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Нет измененных данных для сохранения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                End Using
            Catch ex As Exception
                MessageBox.Show("Ошибка при сохранении изменений: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 7. ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ И АВТОРАЗМЕР
    ' ==========================================
    Private Function GetDbValueInt(val As Object, Optional defaultValue As Object = Nothing) As Object
        If val Is Nothing OrElse IsDBNull(val) OrElse String.IsNullOrWhiteSpace(val.ToString()) Then
            Return If(defaultValue IsNot Nothing, defaultValue, DBNull.Value)
        End If
        Dim res As Integer
        If Integer.TryParse(val.ToString(), res) Then Return res
        Return If(defaultValue IsNot Nothing, defaultValue, DBNull.Value)
    End Function

    Private Function GetDbValueDouble(val As Object, Optional defaultValue As Object = Nothing) As Object
        If val Is Nothing OrElse IsDBNull(val) OrElse String.IsNullOrWhiteSpace(val.ToString()) Then
            Return If(defaultValue IsNot Nothing, defaultValue, DBNull.Value)
        End If
        Dim res As Double
        If Double.TryParse(val.ToString().Replace(","c, "."c), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, res) Then
            Return res
        End If
        Return If(defaultValue IsNot Nothing, defaultValue, DBNull.Value)
    End Function

    Private Sub ResizeGridColumns(dg As DataGridView)
        dg.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
        dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        For Each col As DataGridViewColumn In dg.Columns
            col.MinimumWidth = 10
        Next

        dg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

        For Each col As DataGridViewColumn In dg.Columns
            If col.Visible Then
                If TypeOf col Is DataGridViewComboBoxColumn Then
                    col.Width = Math.Max(col.Width, 85)
                Else
                    col.Width = Math.Max(col.Width, 40)
                End If
            End If
        Next
    End Sub
End Class