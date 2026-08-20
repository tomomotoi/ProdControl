Public Class KnittingRoutingForm
    ' Флаг: были ли данные загружены из базы для текущего артикула
    Private isLoadedFromDb As Boolean = False

    Private Sub KnittingRoutingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.Coefficients". При необходимости она может быть перемещена или удалена.
        Me.CoefficientsTableAdapter.Fill(Me.OperationDBDataSet0.Coefficients)
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.KnittingMachines". При необходимости она может быть перемещена или удалена.
        Me.KnittingMachinesTableAdapter.Fill(Me.OperationDBDataSet0.KnittingMachines)
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.ClothingParts". При необходимости она может быть перемещена или удалена.
        Me.ClothingPartsTableAdapter.Fill(Me.OperationDBDataSet0.ClothingParts)
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.Articul". При необходимости она может быть перемещена или удалена.
        Me.ArticulTableAdapter.Fill(Me.OperationDBDataSet0.Articul)
        'TODO: данная строка кода позволяет загрузить данные в таблицу "OperationDBDataSet0.ClothingParts". При необходимости она может быть перемещена или удалена.
        Me.ClothingPartsTableAdapter.Fill(Me.OperationDBDataSet0.ClothingParts)

        ' Явно регистрируем обработчики событий
        AddHandler Me.tbAvgValue.TextChanged, AddressOf tbAvgValue_TextChanged
        AddHandler Me.cbCoeff1.SelectedIndexChanged, AddressOf cbCoeff1_SelectedIndexChanged
        AddHandler Me.cbCoeff2.SelectedIndexChanged, AddressOf cbCoeff2_SelectedIndexChanged
        AddHandler Me.cbModelPartAmount.SelectedIndexChanged, AddressOf cbModelPartAmount_SelectedIndexChanged
        AddHandler Me.cbMachineCarriageAmount.SelectedIndexChanged, AddressOf cbMachineCarriageAmount_SelectedIndexChanged
    End Sub

    ''' <summary>
    ''' Срабатывает при изменении значения "Средн. зн."
    ''' Запускает пересчёт "С коэфф."
    ''' </summary>
    Private Sub tbAvgValue_TextChanged(sender As Object, e As EventArgs) Handles tbAvgValue.TextChanged
        CalculateWithCoefficient()
    End Sub

    ''' <summary>
    ''' Срабатывает при изменении Коэфф1
    ''' Запускает пересчёт "С коэфф."
    ''' </summary>
    Private Sub cbCoeff1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCoeff1.SelectedIndexChanged
        CalculateWithCoefficient()
    End Sub

    ''' <summary>
    ''' Срабатывает при изменении Коэфф2
    ''' Запускает пересчёт "С коэфф."
    ''' </summary>
    Private Sub cbCoeff2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCoeff2.SelectedIndexChanged
        CalculateWithCoefficient()
    End Sub

    ''' <summary>
    ''' Срабатывает при изменении М (количество деталей)
    ''' Запускает пересчёт "С компл."
    ''' </summary>
    Private Sub cbModelPartAmount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbModelPartAmount.SelectedIndexChanged
        CalculateWithKit()
    End Sub

    ''' <summary>
    ''' Срабатывает при изменении П (количество кареток)
    ''' Запускает пересчёт "С компл."
    ''' </summary>
    Private Sub cbMachineCarriageAmount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMachineCarriageAmount.SelectedIndexChanged
        CalculateWithKit()
    End Sub

    ''' <summary>
    ''' Расчёт "С коэфф" (с коэффициентом)
    ''' Формула: с коэфф = средн_зн × коэфф1 × коэфф2
    ''' </summary>
    Private Sub CalculateWithCoefficient()
        Try
            ' Получаем значение среднего значения
            Dim avgValue As Decimal = GetDecimalValue(Me.tbAvgValue.Text)

            ' Если среднее значение пусто или равно нулю, очищаем результаты
            If String.IsNullOrWhiteSpace(Me.tbAvgValue.Text) OrElse avgValue = 0 Then
                Me.tbWithCoefficient.Text = ""
                Me.tbWithKit.Text = ""
                Return
            End If

            ' Получаем коэффициент 1 (читаем .Text вместо .SelectedItem)
            Dim coeff1 As Decimal = 1
            If Not String.IsNullOrWhiteSpace(Me.cbCoeff1.Text) Then
                Dim parsedCoeff1 As Decimal = GetDecimalValue(Me.cbCoeff1.Text)
                If parsedCoeff1 <> 0 Then coeff1 = parsedCoeff1
            End If

            ' Получаем коэффициент 2
            Dim coeff2 As Decimal = 1
            If Not String.IsNullOrWhiteSpace(Me.cbCoeff2.Text) Then
                Dim parsedCoeff2 As Decimal = GetDecimalValue(Me.cbCoeff2.Text)
                If parsedCoeff2 <> 0 Then coeff2 = parsedCoeff2
            End If

            ' Вычисляем результат: С коэфф = Средн.зн. × Коэфф1 × Коэфф2
            Dim result As Decimal = avgValue * coeff1 * coeff2

            ' Отображаем результат с 2 знаками после запятой
            Me.tbWithCoefficient.Text = result.ToString("F2")

            ' Пересчитываем "С компл"
            CalculateWithKit()

        Catch ex As Exception
            Me.tbWithCoefficient.Text = ""
            Me.tbWithKit.Text = ""
        End Try
    End Sub

    ''' <summary>
    ''' Расчёт "С компл" (с комплектацией)
    ''' Формула: с компл = с_коэфф × М / П
    ''' где М - количество деталей, П - количество кареток
    ''' </summary>
    Private Sub CalculateWithKit()
        Try
            Dim withCoeff As Decimal = GetDecimalValue(Me.tbWithCoefficient.Text)

            If String.IsNullOrWhiteSpace(Me.tbWithCoefficient.Text) Then
                Me.tbWithKit.Text = ""
                Return
            End If

            If withCoeff = 0 Then
                Me.tbWithKit.Text = "0.000"
                Return
            End If

            ' Получаем М (количество деталей)
            Dim modelPartAmount As Decimal = 1
            If Not String.IsNullOrWhiteSpace(Me.cbModelPartAmount.Text) Then
                Dim parsedValue As Decimal = GetDecimalValue(Me.cbModelPartAmount.Text)
                If parsedValue <> 0 Then modelPartAmount = parsedValue
            End If

            ' Получаем П (количество кареток)
            Dim machineCarriageAmount As Decimal = 1
            If Not String.IsNullOrWhiteSpace(Me.cbMachineCarriageAmount.Text) Then
                Dim parsedValue As Decimal = GetDecimalValue(Me.cbMachineCarriageAmount.Text)
                If parsedValue <> 0 Then machineCarriageAmount = parsedValue
            End If

            ' Вычисляем результат: С компл = С коэфф × М / П
            Dim result As Decimal = (withCoeff * modelPartAmount) / machineCarriageAmount

            Me.tbWithKit.Text = result.ToString("F2")

        Catch ex As Exception
            Me.tbWithKit.Text = ""
        End Try
    End Sub

    ''' <summary>
    ''' Вспомогательный метод для безопасного преобразования значения в Decimal
    ''' Обрабатывает запятые, точки, Nothing, пустые строки, объекты из ComboBox
    ''' </summary>
    Private Function GetDecimalValue(value As Object) As Decimal
        Try
            If value Is Nothing OrElse IsDBNull(value) Then Return 0

            ' Если передано уже числовое значение
            If TypeOf value Is Decimal Then Return CDec(value)
            If TypeOf value Is Double OrElse TypeOf value Is Single OrElse TypeOf value Is Integer Then
                Return Convert.ToDecimal(value)
            End If

            ' Преобразуем в строку
            Dim str As String = value.ToString().Trim()
            If String.IsNullOrWhiteSpace(str) Then Return 0

            ' Удаляем обычные и неразрывные пробелы (ChrW(160))
            str = str.Replace(" ", "").Replace(ChrW(160), "")

            Dim result As Decimal = 0

            ' 1. Пробуем парсинг с текущей культурой системы
            If Decimal.TryParse(str, result) Then Return result

            ' 2. Пробуем с инвариантной культурой (с заменяемой запятой на точку)
            Dim strInvariant As String = str.Replace(",", ".")
            If Decimal.TryParse(strInvariant, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, result) Then
                Return result
            End If

            ' 3. Пробуем с обратной заменой (точки на запятую)
            Dim strLocal As String = str.Replace(".", ",")
            If Decimal.TryParse(strLocal, result) Then Return result

            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ' Словарь полей и их пользовательских названий для проверки
            Dim fields As New Dictionary(Of Control, String) From {
                {cbKnittingPart, "Деталь"},
                {cbKnittingMachine, "Станок"},
                {cbModelPartAmount, "Количество деталей"},
                {cbMachineCarriageAmount, "Количество кареток"},
                {cbCoeff1, "Коэффициент 1"},
                {cbCoeff2, "Коэффициент 2"},
                {tbAvgValue, "Среднее значение"},
                {tbWithCoefficient, "С коэффициентом"},
                {tbWithKit, "С комплектом"}
            }

            ' Проверка каждого поля на заполненность
            For Each item In fields
                If String.IsNullOrWhiteSpace(item.Key.Text) Then
                    MessageBox.Show($"Поле '{item.Value}' не заполнено!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    item.Key.Focus()
                    Return
                End If
            Next

            ' Добавление новой строки в DataGridView
            Dim rowIndex As Integer = dataGridRouting.Rows.Add()
            Dim row As DataGridViewRow = dataGridRouting.Rows(rowIndex)

            ' Заполнение колонок
            row.Cells(Me.PartColumn.Name).Value = cbKnittingPart.Text
            row.Cells(Me.MachineColumn.Name).Value = cbKnittingMachine.Text
            row.Cells(Me.ModelPartAmountColumn.Name).Value = cbModelPartAmount.Text
            row.Cells(Me.MachineCarriageAmountColumn.Name).Value = cbMachineCarriageAmount.Text
            row.Cells(Me.Coeff1Column.Name).Value = cbCoeff1.Text
            row.Cells(Me.Coeff2Column.Name).Value = cbCoeff2.Text
            row.Cells(Me.AvgValueColumn.Name).Value = tbAvgValue.Text
            row.Cells(Me.WithCoefficientColumn.Name).Value = tbWithCoefficient.Text
            row.Cells(Me.WithKitColumn.Name).Value = tbWithKit.Text

        Catch ex As Exception
            MessageBox.Show("Ошибка при добавлении строки в таблицу: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Try
            ' Проверяем, есть ли выбранная строка и не является ли она служебной строкой для добавления
            If dataGridRouting.CurrentRow Is Nothing OrElse dataGridRouting.CurrentRow.IsNewRow Then
                MessageBox.Show("Выберите строку для удаления!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Запрос подтверждения у пользователя
            Dim result As DialogResult = MessageBox.Show("Вы действительно хотите удалить выбранную строку?",
                                                         "Подтверждение удаления",
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Question)

            ' Если пользователь подтвердил удаление
            If result = DialogResult.Yes Then
                dataGridRouting.Rows.Remove(dataGridRouting.CurrentRow)
            End If

        Catch ex As Exception
            MessageBox.Show("Ошибка при удалении строки: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        ' 1. Проверка выбора артикула
        If cbArticle.SelectedValue Is Nothing OrElse String.IsNullOrWhiteSpace(cbArticle.SelectedValue.ToString()) Then
            MessageBox.Show("Выберите артикул!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Проверка наличия строк в DataGridView
        Dim hasRows As Boolean = False
        For Each row As DataGridViewRow In dataGridRouting.Rows
            If Not row.IsNewRow Then
                hasRows = True
                Exit For
            End If
        Next

        If Not hasRows Then
            If isLoadedFromDb Then
                ' Пользователь загрузил карту и удалил все строки
                Dim confirmDelete As DialogResult = MessageBox.Show(
                "Вы удалили все строки из загруженной карты. Удалить эту технологическую карту из базы данных?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

                If confirmDelete <> DialogResult.Yes Then Return
            Else
                ' Карта не загружалась (новый артикул или пустой поиск)
                MessageBox.Show("Таблица пуста. Нет данных для сохранения!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        ' Получаем ID выбранного артикула
        Dim articulId As Integer = Convert.ToInt32(cbArticle.SelectedValue)

        ' Укажите вашу строку подключения к MS Access
        Dim connectionString As String = My.Settings.OperationDBConnectionString

        Using conn As New OleDbConnection(connectionString)
            Try
                conn.Open()

                ' Использование транзакции: либо сохранятся все строки, либо произойдет откат
                Using trans As OleDbTransaction = conn.BeginTransaction()
                    Try

                        ' ==================== ДОБАВЛЕНО: УДАЛЕНИЕ СТАРЫХ ЗАПИСЕЙ ====================
                        ' Перед добавлением актуальных строк очищаем все прежние записи артикула
                        Dim deleteSql As String = "DELETE FROM [KnittingRouting] WHERE [articul_id] = ?"
                        Using cmdDelete As New OleDbCommand(deleteSql, conn, trans)
                            cmdDelete.Parameters.Add("?", OleDbType.Integer).Value = articulId
                            cmdDelete.ExecuteNonQuery()
                        End Using
                        ' ============================================================================

                        ' SQL-запрос для вставки записи в KnittingRouting
                        Dim insertSql As String = "INSERT INTO [KnittingRouting] " &
                            "([articul_id], [clothing_part_id], [machine_id], [product_parts_amount], " &
                            "[machine_parts_amount], [k1], [k2], [avg_rate], [coefficient_rate], [kit_rate]) " &
                            "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

                        For Each row As DataGridViewRow In dataGridRouting.Rows
                            If row.IsNewRow Then Continue For

                            ' Извлечение текстовых названий из ячеек
                            Dim partName As String = Convert.ToString(row.Cells(Me.PartColumn.Name).Value)
                            Dim machineName As String = Convert.ToString(row.Cells(Me.MachineColumn.Name).Value)

                            ' Поиск ID по текстовому названию (замените 'name' и 'id' на имена полей в ваших деталях/станках при необходимости)
                            Dim partId As Integer = GetIdByName(conn, trans, "ClothingParts", "part_name", "id", partName)
                            ' Для оборудования вызываем специальный метод поиска по составному имени
                            Dim machineId As Integer = GetMachineIdByDisplayName(conn, trans, machineName)

                            If partId = 0 OrElse machineId = 0 Then
                                Throw New Exception($"Не удалось найти ID в базе для детали '{partName}' или оборудования '{machineName}'.")
                            End If

                            ' Заполнение параметров (в OLE DB важен строгий порядок полей!)
                            Using cmd As New OleDbCommand(insertSql, conn, trans)
                                ' 1. Ключи (Длинное целое / Long Integer)
                                cmd.Parameters.Add("?", OleDbType.Integer).Value = articulId
                                cmd.Parameters.Add("?", OleDbType.Integer).Value = partId
                                cmd.Parameters.Add("?", OleDbType.Integer).Value = machineId

                                ' 2. Количество деталей и кареток (Длинное целое / Long Integer)
                                cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(GetDecimalValue(row.Cells(Me.ModelPartAmountColumn.Name).Value))
                                cmd.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(GetDecimalValue(row.Cells(Me.MachineCarriageAmountColumn.Name).Value))

                                ' 3. Коэффициенты и нормы (Двойное с плавающей точкой / Double)
                                cmd.Parameters.Add("?", OleDbType.Double).Value = Convert.ToDouble(GetDecimalValue(row.Cells(Me.Coeff1Column.Name).Value))
                                cmd.Parameters.Add("?", OleDbType.Double).Value = Convert.ToDouble(GetDecimalValue(row.Cells(Me.Coeff2Column.Name).Value))
                                cmd.Parameters.Add("?", OleDbType.Double).Value = Convert.ToDouble(GetDecimalValue(row.Cells(Me.AvgValueColumn.Name).Value))
                                cmd.Parameters.Add("?", OleDbType.Double).Value = Convert.ToDouble(GetDecimalValue(row.Cells(Me.WithCoefficientColumn.Name).Value))
                                cmd.Parameters.Add("?", OleDbType.Double).Value = Convert.ToDouble(GetDecimalValue(row.Cells(Me.WithKitColumn.Name).Value))

                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        ' Подтверждение успешной транзакции
                        trans.Commit()
                        ' После сохранения/удаления обновляем флаг в зависимости от того, остались ли строки
                        isLoadedFromDb = hasRows
                        MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As Exception
                        trans.Rollback()
                        Throw
                    End Try
                End Using

            Catch ex As Exception
                MessageBox.Show("Ошибка при сохранении в БД: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' Вспомогательная функция для получения ID по названию записи из справочника
    ''' </summary>
    Private Function GetIdByName(conn As OleDbConnection, trans As OleDbTransaction, tableName As String, nameColumn As String, idColumn As String, nameValue As String) As Integer
        Dim sql As String = $"SELECT [{idColumn}] FROM [{tableName}] WHERE [{nameColumn}] = ?"
        Using cmd As New OleDbCommand(sql, conn, trans)
            cmd.Parameters.AddWithValue("?", nameValue)
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToInt32(result)
            End If
        End Using
        Return 0
    End Function

    ''' <summary>
    ''' Вспомогательный метод для поиска ID оборудования по составному имени (марка + модель)
    ''' </summary>
    Private Function GetMachineIdByDisplayName(conn As OleDbConnection, trans As OleDbTransaction, displayName As String) As Integer
        ' Склеиваем machine_make и machine_model прямо в SQL-запросе MS Access
        Dim sql As String = "SELECT [id] FROM [KnittingMachines] WHERE ([machine_make] & ' ' & [machine_model]) = ?"

        Using cmd As New OleDbCommand(sql, conn, trans)
            cmd.Parameters.AddWithValue("?", displayName)
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToInt32(result)
            End If
        End Using
        Return 0
    End Function

    Private Sub btnSeachArt_Click(sender As Object, e As EventArgs) Handles btnSeachArt.Click
        ' 1. Проверка выбора артикула
        If cbArticle.SelectedValue Is Nothing OrElse String.IsNullOrWhiteSpace(cbArticle.SelectedValue.ToString()) Then
            MessageBox.Show("Выберите артикул для поиска!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim articulId As Integer = Convert.ToInt32(cbArticle.SelectedValue)
        Dim connectionString As String = My.Settings.OperationDBConnectionString

        ' 2. Очищаем текущие строки в таблицы DataGridView перед загрузкой
        isLoadedFromDb = False ' Сброс флага наличия записей в таблице
        dataGridRouting.Rows.Clear()
        tbTotal.Clear() ' Очистка итогового поля перед поиском

        ' SQL-запрос с объединением таблиц (обратите внимание на скобки в FROM — это синтаксис MS Access)
        Dim sql As String = "SELECT " &
            "cp.[part_name], " &
            "CStr(km.[machine_make] & ' ' & km.[machine_model]) AS machine_display_name, " &
            "kr.[product_parts_amount], " &
            "kr.[machine_parts_amount], " &
            "kr.[k1], " &
            "kr.[k2], " &
            "kr.[avg_rate], " &
            "kr.[coefficient_rate], " &
            "kr.[kit_rate] " &
            "FROM ([KnittingRouting] AS kr " &
            "INNER JOIN [ClothingParts] AS cp ON kr.[clothing_part_id] = cp.[id]) " &
            "INNER JOIN [KnittingMachines] AS km ON kr.[machine_id] = km.[id] " &
            "WHERE kr.[articul_id] = ?"

        Using conn As New OleDbConnection(connectionString)
            Try
                conn.Open()

                Using cmd As New OleDbCommand(sql, conn)
                    cmd.Parameters.Add("?", OleDbType.Integer).Value = articulId

                    Using reader As OleDbDataReader = cmd.ExecuteReader()
                        Dim recordsFound As Boolean = False

                        Dim totalWithKit As Decimal = 0 ' Переменная-счетчик суммы

                        While reader.Read()
                            recordsFound = True
                            Dim rowIndex As Integer = dataGridRouting.Rows.Add()
                            Dim row As DataGridViewRow = dataGridRouting.Rows(rowIndex)

                            ' Заполнение колонок строки
                            row.Cells(Me.PartColumn.Name).Value = reader("part_name").ToString()
                            row.Cells(Me.MachineColumn.Name).Value = reader("machine_display_name").ToString()
                            row.Cells(Me.ModelPartAmountColumn.Name).Value = reader("product_parts_amount").ToString()
                            row.Cells(Me.MachineCarriageAmountColumn.Name).Value = reader("machine_parts_amount").ToString()
                            row.Cells(Me.Coeff1Column.Name).Value = reader("k1").ToString()
                            row.Cells(Me.Coeff2Column.Name).Value = reader("k2").ToString()

                            ' Расчет значения "Итого"
                            Dim kitRate As Decimal = Convert.ToDecimal(reader("kit_rate"))
                            totalWithKit += kitRate ' Прибавляем значение текущей строки к общей сумме

                            ' Вывод дробных полей с форматированием до 3 знаков
                            row.Cells(Me.AvgValueColumn.Name).Value = Convert.ToDecimal(reader("avg_rate")).ToString("F3")
                            row.Cells(Me.WithCoefficientColumn.Name).Value = Convert.ToDecimal(reader("coefficient_rate")).ToString("F3")
                            row.Cells(Me.WithKitColumn.Name).Value = Convert.ToDecimal(reader("kit_rate")).ToString("F3")
                        End While

                        ' Устанавливаем флаг успешной загрузки из базы
                        isLoadedFromDb = recordsFound

                        If recordsFound Then
                            ' Выводим общую сумму с округлением до 2 знаков
                            tbTotal.Text = totalWithKit.ToString("F2")
                        Else
                            tbTotal.Text = ""
                            MessageBox.Show("Записи для указанного артикула не найдены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                MessageBox.Show("Ошибка при загрузке данных: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub cbArticle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbArticle.SelectedIndexChanged
        dataGridRouting.Rows.Clear()
        tbTotal.Clear() ' Очищаем поле Итого при переключении артикула
        isLoadedFromDb = False
    End Sub
End Class