Public Class DetailsForm
    ' Подключение к БД из настроек проекта
    Private ReadOnly connectionString As String = My.Settings.OperationDBConnectionString

    Private Sub ClothingPartsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    ' Загрузка списка деталей в dgvParts
    Private Sub LoadData()
        Using conn As New OleDbConnection(connectionString)
            Try
                Dim query As String = "SELECT id, part_name FROM ClothingParts ORDER BY id DESC"
                Dim adapter As New OleDbDataAdapter(query, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                dgvParts.DataSource = dt

                ' Настройка колонок таблицы
                If dgvParts.Columns.Contains("id") Then
                    dgvParts.Columns("id").HeaderText = "ID"
                    dgvParts.Columns("id").Width = 60
                End If
                If dgvParts.Columns.Contains("part_name") Then
                    dgvParts.Columns("part_name").HeaderText = "Наименование детали"
                    dgvParts.Columns("part_name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If

            Catch ex As Exception
                MessageBox.Show("Ошибка загрузки данных: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Перенос названия из выделенной строки таблицы в поле ввода
    Private Sub dgvParts_SelectionChanged(sender As Object, e As EventArgs) Handles dgvParts.SelectionChanged
        If dgvParts.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = dgvParts.SelectedRows(0)

            If Not row.IsNewRow Then
                tbPartName.Text = If(row.Cells("part_name").Value IsNot DBNull.Value, row.Cells("part_name").Value.ToString(), "")
            End If
        End If
    End Sub

    ' Добавление новой детали
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrWhiteSpace(tbPartName.Text) Then
            MessageBox.Show("Введите наименование детали", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As New OleDbConnection(connectionString)
            Try
                Dim query As String = "INSERT INTO ClothingParts (part_name) VALUES (@part_name)"
                Using cmd As New OleDbCommand(query, conn)
                    cmd.Parameters.AddWithValue("@part_name", tbPartName.Text.Trim())

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Деталь успешно добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                LoadData()

            Catch ex As Exception
                MessageBox.Show("Ошибка при добавлении: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Сохранение редактирования существующей детали
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If dgvParts.SelectedRows.Count = 0 OrElse dgvParts.SelectedRows(0).IsNewRow Then
            MessageBox.Show("Выберите запись в таблице для редактирования", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(tbPartName.Text) Then
            MessageBox.Show("Наименование детали не может быть пустым", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedId As Integer = Convert.ToInt32(dgvParts.SelectedRows(0).Cells("id").Value)

        Using conn As New OleDbConnection(connectionString)
            Try
                Dim query As String = "UPDATE ClothingParts SET part_name = @part_name WHERE id = @id"
                Using cmd As New OleDbCommand(query, conn)
                    cmd.Parameters.AddWithValue("@part_name", tbPartName.Text.Trim())
                    cmd.Parameters.AddWithValue("@id", selectedId)

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

    ' Удаление детали
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvParts.SelectedRows.Count = 0 OrElse dgvParts.SelectedRows(0).IsNewRow Then
            MessageBox.Show("Выберите запись в таблице для удаления", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedId As Integer = Convert.ToInt32(dgvParts.SelectedRows(0).Cells("id").Value)
        Dim partName As String = dgvParts.SelectedRows(0).Cells("part_name").Value.ToString()

        Dim confirm = MessageBox.Show($"Удалить деталь ""{partName}"" (ID: {selectedId})?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            Using conn As New OleDbConnection(connectionString)
                Try
                    Dim query As String = "DELETE FROM ClothingParts WHERE id = @id"
                    Using cmd As New OleDbCommand(query, conn)
                        cmd.Parameters.AddWithValue("@id", selectedId)

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

    ' Очистка полей
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        tbPartName.Clear()
        dgvParts.ClearSelection()
    End Sub
End Class