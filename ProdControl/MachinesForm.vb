Imports Microsoft.ReportingServices.Rendering.ExcelOpenXmlRenderer

Public Class MachinesForm
    ' Укажите вашу строку подключения к БД Access
    Private ReadOnly connectionString As String = My.Settings.OperationDBConnectionString

    Private Sub MachinesForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    ' Загрузка и обновление данных в DataGridView
    Private Sub LoadData()
        Using conn As New OleDbConnection(connectionString)
            Try
                Dim query As String = "SELECT id, machine_make, machine_model FROM KnittingMachines ORDER BY id DESC"
                Dim adapter As New OleDbDataAdapter(query, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                dgvMachines.DataSource = dt

                ' Настройка заголовков колонок
                If dgvMachines.Columns.Count > 0 Then
                    dgvMachines.Columns("id").HeaderText = "ID"
                    dgvMachines.Columns("machine_make").HeaderText = "Марка оборудования"
                    dgvMachines.Columns("machine_model").HeaderText = "Модель оборудования"

                    dgvMachines.Columns("machine_make").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    dgvMachines.Columns("machine_model").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If

            Catch ex As Exception
                MessageBox.Show("Ошибка загрузки данных: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Перенос данных из выбранной строки таблицы в поля ввода
    Private Sub dgvMachines_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMachines.SelectionChanged
        If dgvMachines.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = dgvMachines.SelectedRows(0)
            tbId.Text = row.Cells("id").Value.ToString()
            tbMake.Text = row.Cells("machine_make").Value.ToString()
            tbModel.Text = row.Cells("machine_model").Value.ToString()
        End If
    End Sub

    ' Добавление новой записи
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrWhiteSpace(tbMake.Text) OrElse String.IsNullOrWhiteSpace(tbModel.Text) Then
            MessageBox.Show("Заполните марку и модель оборудования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As New OleDbConnection(connectionString)
            Try
                Dim query As String = "INSERT INTO KnittingMachines (machine_make, machine_model) VALUES (@make, @model)"
                Using cmd As New OleDbCommand(query, conn)
                    cmd.Parameters.AddWithValue("@make", tbMake.Text.Trim())
                    cmd.Parameters.AddWithValue("@model", tbModel.Text.Trim())

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Запись успешно добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                LoadData()

            Catch ex As Exception
                MessageBox.Show("Ошибка при добавлении: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Сохранение (Редактирование) существующей записи
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
        If String.IsNullOrEmpty(tbId.Text) Then
            MessageBox.Show("Выберите запись для редактирования из таблицы", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(tbMake.Text) OrElse String.IsNullOrWhiteSpace(tbModel.Text) Then
            MessageBox.Show("Поля марки и модели не могут быть пустыми", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As New OleDbConnection(connectionString)
            Try
                Dim query As String = "UPDATE KnittingMachines SET machine_make = @make, machine_model = @model WHERE id = @id"
                Using cmd As New OleDbCommand(query, conn)
                    cmd.Parameters.AddWithValue("@make", tbMake.Text.Trim())
                    cmd.Parameters.AddWithValue("@model", tbModel.Text.Trim())
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(tbId.Text))

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Изменения успешно сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadData()

            Catch ex As Exception
                MessageBox.Show("Ошибка при сохранении: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Удаление записи
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(tbId.Text) Then
            MessageBox.Show("Выберите запись для удаления", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim confirm = MessageBox.Show($"Удалить оборудование с ID {tbId.Text}?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            Using conn As New OleDbConnection(connectionString)
                Try
                    Dim query As String = "DELETE FROM KnittingMachines WHERE id = @id"
                    Using cmd As New OleDbCommand(query, conn)
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(tbId.Text))

                        conn.Open()
                        cmd.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Запись удалена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearFields()
                    LoadData()

                Catch ex As Exception
                    MessageBox.Show("Ошибка при удалении: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End If
    End Sub

    ' Очистка полей ввода
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClearFields.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        tbId.Clear()
        tbMake.Clear()
        tbModel.Clear()
        dgvMachines.ClearSelection()
    End Sub
End Class