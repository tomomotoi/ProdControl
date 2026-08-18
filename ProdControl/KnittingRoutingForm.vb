Public Class KnittingRoutingForm
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
End Class