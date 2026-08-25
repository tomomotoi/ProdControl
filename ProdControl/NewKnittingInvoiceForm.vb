Public Class NewKnittingInvoiceForm
    Private Sub NewKnittingInvoiceForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.KnittingMachines". При необходимости она может быть перемещена или удалена.
        Me.KnittingMachinesTableAdapter.Fill(Me.OperationDBDataSet0.KnittingMachines)
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.ClothingParts". При необходимости она может быть перемещена или удалена.
        Me.ClothingPartsTableAdapter.Fill(Me.OperationDBDataSet0.ClothingParts)
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.Employee". При необходимости она может быть перемещена или удалена.
        Me.EmployeeTableAdapter.Fill(Me.OperationDBDataSet0.Employee)
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.Articul". При необходимости она может быть перемещена или удалена.
        Me.ArticulTableAdapter.Fill(Me.OperationDBDataSet0.Articul)

        ' Установка актуальной даты
        dtPickerOpDate.Value = DateTime.Now
        ' Ширина колонок подстраивается под текст
        dgOperationLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    ' Блокировка ввода символов не являющихся цифрами
    Private Sub tbWorkday_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbWorkday.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgOperationLog.CurrentRow IsNot Nothing AndAlso Not dgOperationLog.CurrentRow.IsNewRow Then
            dgOperationLog.Rows.Remove(dgOperationLog.CurrentRow)
        Else
            MessageBox.Show("Выберите строку для удаления!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Проверяем, есть ли данные в таблице
        Dim validRowsCount As Integer = 0
        For Each row As DataGridViewRow In dgOperationLog.Rows
            If Not row.IsNewRow Then validRowsCount += 1
        Next

        If validRowsCount = 0 Then
            MessageBox.Show("Таблица пуста. Нет данных для сохранения!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim connectionString As String = My.Settings.OperationDBConnectionString

        Dim sql As String = "INSERT INTO [KnittingOperationLog] " &
            "([document_number], [article_id], [employee_id], [part_id], [machine_id], [amount], [date_execution], [time_execution], [machines_in_zone], [work_day]) " &
            "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

        Using conn As New OleDbConnection(connectionString)
            Try
                conn.Open()
                Using transaction As OleDbTransaction = conn.BeginTransaction(),
                      cmd As New OleDbCommand(sql, conn, transaction)

                    For Each row As DataGridViewRow In dgOperationLog.Rows
                        If row.IsNewRow Then Continue For

                        cmd.Parameters.Clear()

                        ' Извлекаем значения и ID в строгой последовательности параметров SQL
                        cmd.Parameters.Add("?", OleDbType.VarChar).Value = row.Cells("LogNumber").Value.ToString()
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(row.Cells("ArticulName").Tag)
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(row.Cells("EmployeeName").Tag)
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(row.Cells("PartName").Tag)

                        cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(row.Cells("KnittingMachine").Tag)

                        cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(row.Cells("PartAmount").Value)
                        cmd.Parameters.Add("?", OleDbType.Date).Value = Convert.ToDateTime(row.Cells("LogDate").Value)
                        cmd.Parameters.Add("?", OleDbType.Double).Value = Convert.ToDouble(row.Cells("ExecutionTime").Value)
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(row.Cells("MachineNumberInZone").Value)
                        cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(row.Cells("workDay").Value)

                        cmd.ExecuteNonQuery()
                    Next

                    ' Подтверждаем транзакцию
                    transaction.Commit()
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ' Очищаем таблицу после успешного сохранения
                    dgOperationLog.Rows.Clear()

                End Using
            Catch ex As Exception
                MessageBox.Show("Ошибка при сохранении данных: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub NumericFields_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbWorkday.KeyPress,
                                                                                         tbCount.KeyPress,
                                                                                         tbMachinesInZone.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    ' Обработчик для поля tbExecutionTime (разрешает дробные числа с точностью до 2 знаков после запятой)
    Private Sub tbExecutionTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbExecutionTime.KeyPress
        ' Получаем доступ к текстовому полю, в котором происходит ввод
        Dim txtBox As TextBox = CType(sender, TextBox)

        ' Определяем разделитель дробных чисел, установленный в текущей операционной системе (точка или запятая)
        Dim decimalSeparator As Char = Convert.ToChar(Application.CurrentCulture.NumberFormat.NumberDecimalSeparator)

        ' Если пользователь нажал точку или запятую, автоматически подменяем её на системный разделитель
        If e.KeyChar = "."c OrElse e.KeyChar = ","c Then
            e.KeyChar = decimalSeparator
        End If

        ' Разрешаем ввод только цифр, управляющих клавиш (Backspace) и разделителя
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> decimalSeparator Then
            e.Handled = True
            Return
        End If

        ' Блокируем ввод второго разделителя, если он уже присутствует в строке
        If e.KeyChar = decimalSeparator AndAlso txtBox.Text.Contains(decimalSeparator.ToString()) Then
            e.Handled = True
            Return
        End If

        ' Ограничиваем ввод до двух знаков после запятой (если курсор находится правее разделителя)
        If Char.IsDigit(e.KeyChar) AndAlso txtBox.Text.Contains(decimalSeparator.ToString()) Then
            Dim parts() As String = txtBox.Text.Split(decimalSeparator)

            ' Проверяем, что после разделителя уже есть 2 цифры и курсор расположен после него
            If parts.Length > 1 AndAlso parts(1).Length >= 2 AndAlso txtBox.SelectionStart > txtBox.Text.IndexOf(decimalSeparator) Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        ' Проверяем, заполнены ли обязательные поля (включая cbKnittingMachines)
        If cbEmployee.SelectedValue Is Nothing OrElse
           cbArticul.SelectedValue Is Nothing OrElse
           cbOperation.SelectedValue Is Nothing OrElse
           cbKnittingMachines.SelectedValue Is Nothing OrElse ' === ДОБАВЛЕНО ===
           String.IsNullOrWhiteSpace(tbReceiptNumber.Text) OrElse
           String.IsNullOrWhiteSpace(tbCount.Text) OrElse
           String.IsNullOrWhiteSpace(tbWorkday.Text) Then

            MessageBox.Show("Заполните все обязательные поля перед добавлением!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Добавляем новую строку в DataGridView
        Dim rowIndex As Integer = dgOperationLog.Rows.Add()
        Dim row As DataGridViewRow = dgOperationLog.Rows(rowIndex)

        ' Отображаем текст для пользователя и сохраняем ID в свойстве .Tag
        row.Cells("EmployeeName").Value = cbEmployee.Text
        row.Cells("EmployeeName").Tag = cbEmployee.SelectedValue

        row.Cells("LogNumber").Value = tbReceiptNumber.Text
        row.Cells("LogDate").Value = dtPickerOpDate.Value.ToShortDateString()

        row.Cells("ArticulName").Value = cbArticul.Text
        row.Cells("ArticulName").Tag = cbArticul.SelectedValue

        row.Cells("PartName").Value = cbOperation.Text
        row.Cells("PartName").Tag = cbOperation.SelectedValue

        row.Cells("KnittingMachine").Value = cbKnittingMachines.Text
        row.Cells("KnittingMachine").Tag = cbKnittingMachines.SelectedValue

        row.Cells("PartAmount").Value = tbCount.Text
        row.Cells("ExecutionTime").Value = If(String.IsNullOrWhiteSpace(tbExecutionTime.Text), "0", tbExecutionTime.Text)
        row.Cells("MachineNumberInZone").Value = If(String.IsNullOrWhiteSpace(tbMachinesInZone.Text), "0", tbMachinesInZone.Text)
        row.Cells("workDay").Value = tbWorkday.Text
    End Sub
End Class