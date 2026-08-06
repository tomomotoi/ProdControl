Imports ProdControl.OperationDBDataSet2

Public Class RoutingForm
    Dim allowCalculations As Boolean = True 'fixes DBNull when closing form

    Private Sub RoutingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'OperationDBDataSet4.Position' table. You can move, or remove it, as needed.
        Me.PositionTableAdapter.Fill(Me.OperationDBDataSet4.Position)
        'TODO: This line of code loads data into the 'OperationDBDataSet3.Coefficients' table. You can move, or remove it, as needed.
        Me.CoefficientsTableAdapter.FillBy(Me.OperationDBDataSet3.Coefficients)
        'TODO: This line of code loads data into the 'OperationDBDataSet2.Routing' table. You can move, or remove it, as needed.
        Me.RoutingTableAdapter.Fill(Me.OperationDBDataSet2.Routing)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Operation' table. You can move, or remove it, as needed.
        Me.OperationTableAdapter.Fill(Me.OperationDBDataSet1.Operation)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Articul' table. You can move, or remove it, as needed.
        Me.ArticulTableAdapter.Fill(Me.OperationDBDataSet1.Articul)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Operation' table. You can move, or remove it, as needed.
        Me.OperationTableAdapter.Fill(Me.OperationDBDataSet1.Operation)

        cbDopCost.SelectedIndex = -1 'makes empty selection for cbDopCost
    End Sub

    Private Function isOperationAdded(ByVal opId As Integer) As Boolean
        Dim res As Boolean = vbFalse

        Dim row As DataGridViewRow

        For i = 0 To dgView.Rows.Count - 1

            row = dgView.Rows(i)

            If row.Cells(2).Value = opId Then  'Operation 
                res = vbTrue
                Exit For
            End If

        Next


        Return res
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim val(10) As Object

        val(0) = ""
        val(1) = cbArt.SelectedValue
        val(2) = cbOperation.SelectedValue
        val(3) = cbDopCost.GetItemText(cbDopCost.SelectedItem)
        val(4) = txExecTime.Text
        val(5) = txRate.Text
        val(6) = cbLevel.SelectedItem
        val(7) = txCost.Text
        val(8) = vbTrue
        val(9) = vbFalse

        For i As Int32 = 1 To val.Length - 3
            If String.IsNullOrEmpty(val(i)) Then
                MessageBox.Show("Перед добавлением поля должны быть заполнены!")
                Return
            End If
        Next


        If isOperationAdded(cbOperation.SelectedValue) Then
            Dim result As Integer = MessageBox.Show("Операция уже добавлена в список. Вы уверены, что хотите добавить дубликат?", "Технологическая Карта", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                dgView.Rows.Add(val)

                lbRowsAmount.Text = "Количество записей: " + (dgView.Rows.Count - 1).ToString()
            End If
        Else
            dgView.Rows.Add(val)

            lbRowsAmount.Text = "Количество записей: " + (dgView.Rows.Count - 1).ToString()
        End If



    End Sub


    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim row As DataGridViewRow

        Dim rows As IEnumerator = dgView.SelectedRows().GetEnumerator()
        rows.Reset()
        While rows.MoveNext()
            row = rows.Current()
            If row.Cells("isNew").Value Then
                dgView.Rows.Remove(row)

                lbRowsAmount.Text = "Количество записей: " + (dgView.Rows.Count - 1).ToString()
            Else
                MessageBox.Show("Операция уже используется в тех карте. Удалить можно только новую операцию", "Master Data", MessageBoxButtons.OK)
            End If

        End While


    End Sub

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Me.Close()

    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click

        Try
            ' Выбираем сотрудников, для которых будет расчиитана зп и норма выработки

            Dim row As DataGridViewRow
            Dim routID As Integer
            Dim i As Integer
            Dim artID As Integer
            Dim opID As Integer
            Dim addCost As Decimal
            Dim timeExec As Decimal
            Dim execRate As Decimal
            Dim lv As Integer
            Dim tariff As Double

            For i = 0 To dgView.Rows.Count - 1

                row = dgView.Rows(i)

                If Not IsDBNull(cbArt.SelectedValue) Then artID = cbArt.SelectedValue
                If Not IsDBNull(row.Cells(2).Value) Then opID = row.Cells(2).Value
                If Not IsDBNull(row.Cells(3).Value) Then addCost = row.Cells(3).Value
                If Not IsDBNull(row.Cells(4).Value) Then timeExec = row.Cells(4).Value
                If Not IsDBNull(row.Cells(5).Value) Then execRate = row.Cells(5).Value
                If Not IsDBNull(row.Cells(6).Value) Then lv = row.Cells(6).Value
                If Not IsDBNull(row.Cells(7).Value) Then tariff = row.Cells(7).Value

                If row.Cells("isNew").Value Then

                    Dim rr = RoutingTableAdapter.Insert(artID, opID, addCost, timeExec, execRate, lv, tariff)
                    row.Cells("isNew").Value = False

                End If

                If row.Cells("IsChanged").Value Then

                    routID = row.Cells(0).Value

                    Dim res = RoutingTableAdapter.Update(addCost, timeExec, execRate, lv, tariff, routID, artID)
                    row.Cells("IsChanged").Value = False

                End If

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            '    conn.Close()
        End Try


    End Sub

    Private Sub txDopCost_KeyPress(sender As Object, e As KeyPressEventArgs)
        'If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txExecTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txExecTime.KeyPress
        'If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
        '    e.Handled = True
        'End If
    End Sub

    Private Sub txRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txRate.KeyPress
        'If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
        '    e.Handled = True
        'End If
    End Sub

    Private Sub cbLevel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbLevel.KeyPress
        'If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
        '    e.Handled = True
        'End If
    End Sub


    Private Sub btnSeachArt_Click(sender As Object, e As EventArgs) Handles btnSeachArt.Click
        Dim conn As OleDb.OleDbConnection
        Dim exeCom As OleDb.OleDbCommand

        Dim row As DataGridViewRow


        conn = ArticulTableAdapter.Connection()

        Try
            ' выбираем операции из техкарты
            exeCom = conn.CreateCommand()
            exeCom.CommandText = "SELECT RoutingID, ArticulID, OperationID, DopCost, TimeExecute, RateExecute, PositionLevel, Tariff FROM Routing WHERE Routing.ArticulID = " + cbArt.SelectedValue.ToString + " ORDER BY Routing.OperationID"
            dgView.Rows.Clear()

            row = dgView.Rows.Item(0).Clone


            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()
            Dim db_reader As OleDb.OleDbDataReader = exeCom.ExecuteReader()

            If db_reader.HasRows Then
                While db_reader.Read()
                    Dim rowNew As Object() = New Object() {db_reader("RoutingID"),
                                                        db_reader("ArticulID"),
                                                        db_reader("OperationID"),
                                                        db_reader("DopCost"),
                                                        db_reader("TimeExecute"),
                                                        db_reader("RateExecute"),
                                                        db_reader("PositionLevel"),
                                                        db_reader("Tariff"),
                                                        vbFalse, vbFalse
                                                        }
                    dgView.Rows.Add(rowNew)

                End While

                lbRowsAmount.Text = "Количество записей: " + (dgView.Rows.Count - 1).ToString()
            End If

            calculateTariffsIfAbscent()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub calculateTariffsIfAbscent()
        If dgView.Rows.Item(0).Cells(7).Value = 0 And dgView.Rows.Count > 1 Then
            MessageBox.Show("В базе отсутствовали расценки на все или некоторые операции из списка, они будут рассчитаны. Пожалуйста сохраните изменения!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Dim connection As OleDb.OleDbConnection
            Dim sqlCommand As OleDb.OleDbCommand
            Dim commandText As String = "SELECT PositionRateTime FROM PositionLevel WHERE PositionLevel = {0} AND PositionID = (SELECT Operation.PositionID FROM Operation WHERE Operation.OperationID = {1});"

            connection = New OleDb.OleDbConnection(My.Settings.OperationDBConnectionString)
            connection.Open()

            Try
                sqlCommand = connection.CreateCommand()
                sqlCommand.CommandType = CommandType.Text
                sqlCommand.CommandText = commandText

                For i As Int32 = 0 To dgView.Rows.Count - 2 'Count - 2 because it counts an empty row for new records
                    Dim dgViewRow As DataGridViewRow = dgView.Rows.Item(i)
                    Dim operationID As Integer = dgViewRow.Cells(2).Value
                    Dim posLevel As Integer = dgViewRow.Cells(6).Value

                    sqlCommand.CommandText = String.Format(commandText, posLevel, operationID)
                    Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

                    Dim readingResult As Object = vbNull 'PositionRateTime

                    While dbReader.Read()
                        readingResult = dbReader("PositionRateTime")
                    End While

                    dbReader.Close()

                    If Not (readingResult = vbNull) Then
                        Dim timeExecute As Double = dgViewRow.Cells(4).Value
                        Dim tariff As Double = readingResult * timeExecute

                        dgViewRow.Cells(7).Value = tariff
                        dgViewRow.Cells("IsChanged").Value = True
                    Else
                        MessageBox.Show("Не удалось получить расценку за минуту для одной из операций. Вероятно в таблице PositionLevel отсутствует соответствующий PositionLevel(разряд) и/или PositionID(профессия)", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                connection.Close()
            End Try
        End If
    End Sub

    Private Sub dgView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellValueChanged
        If (e.RowIndex >= 0 And e.ColumnIndex < 7) Then
            dgView.Rows(e.RowIndex).Cells(8).Value = vbTrue

        End If

    End Sub

    Private Sub cbDopCost_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDopCost.SelectedIndexChanged
        If allowCalculations Then
            performTimeWithCoefficientCalculation()
        End If
    End Sub

    Private Sub txRate_TextChanged(sender As Object, e As EventArgs) Handles txRate.TextChanged
        If allowCalculations Then
            performTimeWithCoefficientCalculation()
        End If
    End Sub

    Private Sub performTimeWithCoefficientCalculation()
        Dim coefficientTextValue As String

        coefficientTextValue = cbDopCost.GetItemText(cbDopCost.SelectedItem)

        If Not String.IsNullOrEmpty(coefficientTextValue) And Not String.IsNullOrEmpty(txRate.Text) Then
            Try
                calculateTimeWithCoefficient()
            Catch ex As ArithmeticException
                MessageBox.Show("Поле 'Доп. затраты' и/или поле 'Норма времени' не соответствуют следующему формату:" + Environment.NewLine + Environment.NewLine + "###,## или ###.## (в случае английского языка системы)" + Environment.NewLine + Environment.NewLine + "Обратите внимание на разделитель целой и дробной части!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub calculateTimeWithCoefficient()
        Dim timeWithCoefficient, coefficient, timeRate As Double
        Dim coefficientTextValue, timeRateTextValue As String

        coefficientTextValue = cbDopCost.GetItemText(cbDopCost.SelectedItem)
        timeRateTextValue = txRate.Text

        If IsNumeric(coefficientTextValue) And IsNumeric(timeRateTextValue) Then
            coefficient = Double.Parse(coefficientTextValue)
            timeRate = Double.Parse(timeRateTextValue)

            timeWithCoefficient = Math.Round(coefficient * timeRate, 2)

            txExecTime.Text = timeWithCoefficient.ToString()
        Else
            txExecTime.Text = ""
            Throw New System.ArithmeticException("Operand Format Exception")
        End If
    End Sub

    Private Sub cbLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLevel.SelectedIndexChanged
        performOperationCostCalculations()
    End Sub

    Private Sub performOperationCostCalculations()
        Dim positionLevelTextValue As String

        positionLevelTextValue = cbLevel.SelectedItem

        If Not String.IsNullOrEmpty(positionLevelTextValue) And allowCalculations Then
            Try
                calculateOperationCost()
            Catch ex As NullReferenceException
                MessageBox.Show("Результат запроса к БД вернул NULL!" + Environment.NewLine + "Вероятно для данного разряда отсутствует информация в БД.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Catch ex As Exception
                'MessageBox.Show("Не удалось корректно привести типы: (Object -> Double / String -> Double)")
            End Try
        End If
    End Sub

    Private Sub calculateOperationCost()
        Dim positionCostPerMinute As Double
        Dim queryResult As Object = getPositionRateTimeValueForProfessionAndItsLevel()

        If Not (queryResult = vbNull) Then
            Dim queryResultIsDouble As Boolean
            Dim timeWithCoefficientIsDouble As Boolean
            Dim timeWithCoefficientTextValue As String = txExecTime.Text

            Double.TryParse(queryResult.ToString, queryResultIsDouble)
            Double.TryParse(timeWithCoefficientTextValue, timeWithCoefficientIsDouble)

            If queryResultIsDouble And timeWithCoefficientIsDouble Then
                Dim totalCost, timeWithCoefficient As Double

                positionCostPerMinute = Double.Parse(queryResult.ToString())
                timeWithCoefficient = Double.Parse(timeWithCoefficientTextValue)

                totalCost = timeWithCoefficient * positionCostPerMinute

                txCost.Text = totalCost.ToString()
            Else
                txCost.Text = ""
                Throw New Exception("Couldn't parse Object to Double or String to Double")
            End If
        Else
            txCost.Text = ""
            Throw New NullReferenceException("Query Returned null")
            Return
        End If


    End Sub

    Private Function getPositionRateTimeValueForProfessionAndItsLevel() As Object
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim queryResult As Object = vbNull

        connection = New OleDb.OleDbConnection(My.Settings.OperationDBConnectionString)

        Try
            Dim selectedOperationId As Int32 = cbOperation.SelectedValue
            Dim selectedPositionLevel As Int32 = cbLevel.SelectedItem

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = String.Format("SELECT PositionLevel.PositionRateTime FROM PositionLevel INNER JOIN Operation ON PositionLevel.PositionID = Operation.PositionID  WHERE Operation.OperationID = {0} AND PositionLevel.PositionLevel = {1}", selectedOperationId, selectedPositionLevel)

            If connection.State.Equals(ConnectionState.Open) Then
                connection.Close()
            Else
                connection.Open()
            End If


            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

            If dbReader.HasRows Then
                While dbReader.Read()
                    Dim selectValue As Object = dbReader("PositionRateTime")
                    queryResult = selectValue
                End While
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return queryResult
    End Function

    Private Sub cbOperation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbOperation.SelectedIndexChanged
        performOperationCostCalculations()
    End Sub

    Private Sub RoutingForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        allowCalculations = False 'fixes DBNull when closing form
    End Sub

    Private Sub txExecTime_TextChanged(sender As Object, e As EventArgs) Handles txExecTime.TextChanged
        performOperationCostCalculations()
    End Sub

    Private Sub cbLevel_TextChanged(sender As Object, e As EventArgs) Handles cbLevel.TextChanged
        Dim counter As Int16 = 0
        For Each value As Object In cbLevel.Items
            If value = DirectCast(cbLevel.Text, Object) Then
                cbLevel.SelectedIndex = counter
                Exit For
            End If

            counter += 1
        Next
    End Sub
End Class