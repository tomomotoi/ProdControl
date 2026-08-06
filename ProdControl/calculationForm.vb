Imports System.IO
Imports System.Text
Imports ProdControl.OperationDBDataSet1

Public Class calculationForm

    Private Sub calucationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'OperationDBDataSet1.OperationLog' table. You can move, or remove it, as needed.
        'Me.OperationLogTableAdapter.Fill(Me.OperationDBDataSet1.OperationLog)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Employee' table. You can move, or remove it, as needed.
        Me.EmployeeTableAdapter.Fill(Me.OperationDBDataSet1.Employee)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub GetLogByNameDateRangeToolStripButton_Click(sender As Object, e As EventArgs)
        'Try
        'Me.OperationLogTableAdapter.GetLogByNameDateRange(Me.OperationDBDataSet1.OperationLog, New System.Nullable(Of Integer)(CType(EmployeeIDToolStripTextBox.Text, Integer)), New System.Nullable(Of Date)(CType(DateExecutionToolStripTextBox.Text, Date)), New System.Nullable(Of Date)(CType(DateExecution1ToolStripTextBox.Text, Date)))
        'Catch ex As System.Exception
        'System.Windows.Forms.MessageBox.Show(ex.Message)
        'End Try

    End Sub

    Private Sub GetByNageDateToolStripButton_Click(sender As Object, e As EventArgs)
        'Try
        'Me.OperationLogTableAdapter.GetByNageDate(Me.OperationDBDataSet1.OperationLog, New System.Nullable(Of Integer)(CType(EmployeeIDToolStripTextBox1.Text, Integer)), New System.Nullable(Of Date)(CType(DateExecutionToolStripTextBox1.Text, Date)))
        'Catch ex As System.Exception
        'System.Windows.Forms.MessageBox.Show(ex.Message)
        'End Try

    End Sub

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim conn As OleDb.OleDbConnection
        Dim exeCom As OleDb.OleDbCommand
        Dim rout As OleDb.OleDbCommand
        Dim selectedRows As Integer = dbGrid.SelectedRows().Count
        Dim i As Integer
        Dim strEmployees As String = ""
        Dim row As DataGridViewRow

        Dim emplID As String
        Dim opID As String
        Dim artID As String
        Dim itmsCount As String
        Dim execTime As Double = 0
        Dim res As Hashtable
        Dim sl As Hashtable
        Dim calcTable As Hashtable

        Dim exTime As Double = 0
        Dim exRate As Double
        Dim timeRate As Double
        Dim resArr(1) As String
        Dim docNum As String
        Dim dummyDouble As Double = 0
        Dim dtExec As Date

        If selectedRows = 0 Then
            MessageBox.Show("Выберите сотрудника для расчета")
            Exit Sub
        End If

        For i = 0 To selectedRows - 1
            row = dbGrid.SelectedRows().Item(i)
            strEmployees = strEmployees + row.Cells.Item(0).Value.ToString() + ", "
        Next

        strEmployees = strEmployees.Substring(0, Len(strEmployees) - 2)
        '1
        conn = OperationLog1TableAdapter.Connection()

        res = New Hashtable()
        sl = New Hashtable()
        calcTable = New Hashtable()

        calculateSalaryNew() 'new way to calculate salary (depends on WorkLevel)

        Try
            ' Выбираем сотрудников, для которых будет расчиитана зп и норма выработки
            exeCom = conn.CreateCommand()
            If cbDateRange.Checked Then 'если выбран интервал дат
                exeCom.CommandText = "SELECT LogID AS [Log ID], EmployeeID AS [Employee ID], OperationID AS [Operation ID], ArticulID AS [Articul ID], DateExecution AS [DateEX], TimeExecution AS [Time Execution], Count, DocumentNumber FROM OperationLog WHERE (EmployeeID IN (@Empls)) AND (DateExecution BETWEEN @FROM AND @TO) ORDER BY EmployeeID"

                exeCom.Parameters.AddWithValue("@Empls", strEmployees)
                exeCom.Parameters.AddWithValue("@FROM", dtFrom.Value.Date).DbType = DbType.Date
                exeCom.Parameters.AddWithValue("@TO", dtTo.Value.Date).DbType = DbType.Date

            Else 'за дату
                exeCom.CommandText = "SELECT LogID AS [Log ID], EmployeeID AS [Employee ID], OperationID AS [Operation ID], ArticulID AS [Articul ID], DateExecution AS [DateEX], TimeExecution AS [Time Execution], Count, DocumentNumber  FROM OperationLog WHERE (EmployeeID IN (@Empls)) AND (DateExecution = @Dt) ORDER BY EmployeeID"

                exeCom.Parameters.AddWithValue("@Empls", strEmployees)
                exeCom.Parameters.AddWithValue("@Dt", dtFrom.Value.Date).DbType = DbType.Date
            End If

            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()
            Dim db_reader As OleDb.OleDbDataReader = exeCom.ExecuteReader()

            If db_reader.HasRows Then 'проходим по тетрадке выбранных сотрудников

                rout = conn.CreateCommand()
                rout.CommandText = "SELECT ArticulID, OperationID, DopCost, TimeExecute, RateExecute, PositionLevel FROM Routing WHERE ArticulID = @Art AND OperationID = @Op"

                'первый проход для расчета времени выполнения
                While db_reader.Read()

                    emplID = db_reader("Employee ID").ToString()
                    opID = db_reader("Operation ID").ToString()
                    artID = db_reader("Articul ID").ToString()
                    itmsCount = db_reader("Count").ToString()
                    execTime = db_reader("Time Execution")
                    docNum = db_reader("DocumentNumber").ToString()
                    dtExec = CType(db_reader("DateEX"), Date)

                    'прочитать значения из техкарты
                    rout.Parameters.Clear()                     'очистить значения параметров

                    rout.Parameters.AddWithValue("@Art", artID)
                    rout.Parameters.AddWithValue("@Op", opID)

                    If Not (conn.State = ConnectionState.Open) Then
                        conn.Open()
                    End If

                    Dim routRead As OleDb.OleDbDataReader = rout.ExecuteReader()

                    If routRead.HasRows Then

                        While routRead.Read()
                            If execTime = 0 Then
                                exTime = routRead("TimeExecute")
                                exTime = Math.Round(exTime * itmsCount, 6)
                            Else
                                exTime = execTime
                                dummyDouble = routRead("TimeExecute") 'need an read action in order to contunue reading
                            End If

                            Try
                                exRate = EmployeeTableAdapter.GetEmployeeRate(emplID)
                            Catch ex As Exception
                                MessageBox.Show("Для сотрудника не указан разряд в таблице Сотрудники", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try


                            If res.Contains(emplID) Then
                                res(emplID) = res(emplID) + exTime
                            Else
                                res.Add(emplID, exTime)
                            End If

                            'If sl.Contains(emplID) Then
                            '    sl(emplID) = sl(emplID) + Math.Round((exTime * exRate), 6)
                            'Else
                            '    sl.Add(emplID, Math.Round((exTime * exRate), 6))
                            'End If

                            emplID = ""
                            opID = ""
                            artID = ""
                            itmsCount = ""
                            execTime = 0
                            docNum = ""
                            exTime = 0
                            exRate = 0
                            timeRate = 0
                        End While

                        routRead.Close()
                    Else
                        routRead.Close()
                    End If
                End While

                ' второй проход для расчета зп
                Dim resItem As DictionaryEntry
                Dim rItem As DictionaryEntry

                Dim dtWorkDays As DataTable
                Dim dtDates As DataTable
                Dim wdRes As Integer
                Dim wdDuration As Integer
                Dim rowDW As DataRow
                Dim htHours As Hashtable
                Dim htDates As Hashtable



                For Each resItem In res
                    emplID = resItem.Key
                    timeRate = resItem.Value
                    If cbDateRange.Checked Then ' расчет за период

                        htHours = New Hashtable
                        htDates = New Hashtable


                        dtWorkDays = OperationLog1TableAdapter.GetWorkDays(emplID, dtFrom.Value.Date, dtTo.Value.Date)
                        If dtWorkDays.Rows.Count > 0 Then

                            For Each rowDW In dtWorkDays.Rows
                                wdDuration = rowDW("workDay")
                                dtDates = OperationLog1TableAdapter.GetWorkDaysDates(emplID, dtFrom.Value.Date, dtTo.Value.Date, wdDuration)
                                htHours.Add(wdDuration, dtDates.Rows.Count)
                            Next
                            wdRes = 0
                            For Each rItem In htHours
                                wdRes = wdRes + (rItem.Key * rItem.Value)
                            Next

                        End If

                    Else ' расчет на дату
                        dtWorkDays = OperationLog1TableAdapter.GetWDPerDay(emplID, dtFrom.Value.Date.ToString)
                        If dtWorkDays.Rows.Count > 0 Then
                            wdRes = dtWorkDays.Rows(0).Item("workDay")
                        Else
                            wdRes = 480
                        End If

                    End If

                    If calcTable.Contains(emplID) Then
                        calcTable(emplID) = calcTable(emplID) + timeRate / wdRes
                    Else
                        calcTable.Add(emplID, timeRate / wdRes)
                    End If

                Next

            End If


            db_reader.Close()
            conn.Close()


            For i = 0 To selectedRows - 1
                row = dbGrid.SelectedRows().Item(i)
                row.Cells.Item(6).Value = Format(calcTable(row.Cells.Item(0).Value.ToString()) * 100, "###0.00")
                'row.Cells.Item(5).Value = Format(sl(row.Cells.Item(0).Value.ToString()), "##0.00")
                row.Cells.Item(7).Value = Format(res(row.Cells.Item(0).Value.ToString()), "##0.00")
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub cbDateRange_CheckedChanged(sender As Object, e As EventArgs) Handles cbDateRange.CheckedChanged
        If Me.dtTo.Visible Then
            Me.dtTo.Visible = False
            Me.lbTill.Visible = False
        Else
            Me.dtTo.Visible = True
            Me.lbTill.Visible = True
        End If
    End Sub

    Private Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()
        Dim oWrite As System.IO.StreamWriter

        Dim Qualifier As String
        Qualifier = ";"




        saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            myStream = saveFileDialog1.OpenFile()
            If (myStream IsNot Nothing) Then
                ' Code to write the stream goes here.
                oWrite = New StreamWriter(myStream, System.Text.Encoding.UTF8)
                Dim CSV As StringBuilder = New StringBuilder()

                Dim i As Integer = 1
                Dim CSVHeader As StringBuilder = New StringBuilder()
                For Each c As DataGridViewColumn In dbGrid.Columns
                    CSVHeader.Append(c.HeaderText.ToString() & Qualifier)
                    i += 1
                Next

                Dim ss As String
                ss = CSVHeader.ToString()
                oWrite.WriteLine(ss.Substring(0, Len(ss) - 2))
                oWrite.Flush()
                For r As Integer = 0 To dbGrid.SelectedRows.Count - 1

                    Dim CSVLine As StringBuilder = New StringBuilder()
                    Dim s As String = ""
                    For c As Integer = 0 To dbGrid.Columns.Count - 1
                        s = s & dbGrid.SelectedRows(r).Cells(c).Value.ToString() & Qualifier
                    Next
                    s = s.Substring(0, Len(s) - 2)
                    oWrite.WriteLine(s)
                    oWrite.Flush()
                Next
                oWrite.Close()
                oWrite = Nothing
                myStream.Close()
                MsgBox("Файл " & saveFileDialog1.FileName & " сохранен.")
            End If
        End If
    End Sub


    Private Sub DataGridToCSV(ByRef dt As DataGridView, Qualifier As String)
        Dim TempDirectory As String = "A temp Directory"
        System.IO.Directory.CreateDirectory(TempDirectory)
        Dim oWrite As System.IO.StreamWriter
        Dim file As String = System.IO.Path.GetRandomFileName & ".csv"
        oWrite = IO.File.CreateText(TempDirectory & "\" & file)

        Dim CSV As StringBuilder = New StringBuilder()

        Dim i As Integer = 1
        Dim CSVHeader As StringBuilder = New StringBuilder()
        For Each c As DataGridViewColumn In dt.Columns
            If i = 1 Then
                CSVHeader.Append(Qualifier & c.HeaderText.ToString() & Qualifier)
            Else
                CSVHeader.Append("," & Qualifier & c.HeaderText.ToString() & Qualifier)
            End If
            i += 1
        Next

        'CSV.AppendLine(CSVHeader.ToString())
        oWrite.WriteLine(CSVHeader.ToString())
        oWrite.Flush()
        For r As Integer = 0 To dt.Rows.Count - 1

            Dim CSVLine As StringBuilder = New StringBuilder()
            Dim s As String = ""
            For c As Integer = 0 To dt.Columns.Count - 1
                If c = 0 Then
                    'CSVLine.Append(Qualifier & gridResults.Rows(r).Cells(c).Value.ToString() & Qualifier)
                    s = s & Qualifier & dt.Rows(r).Cells(c).Value.ToString() & Qualifier
                Else
                    'CSVLine.Append("," & Qualifier & gridResults.Rows(r).Cells(c).Value.ToString() & Qualifier)
                    s = s & "," & Qualifier & dt.Rows(r).Cells(c).Value.ToString() & Qualifier
                End If

            Next
            oWrite.WriteLine(s)
            oWrite.Flush()
            'CSV.AppendLine(CSVLine.ToString())
            'CSVLine.Clear()
        Next
        'oWrite.Write(CSV.ToString())

        oWrite.Close()
        oWrite = Nothing

        System.Diagnostics.Process.Start(TempDirectory & "\" & file)

        GC.Collect()

    End Sub

    Private Sub calculateSalaryNew()
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim rowIsSelected As Boolean = dbGrid.SelectedRows().Count > 0

        If Not rowIsSelected Then
            MessageBox.Show("Выберете сотрудника для расчета")
            Return
        End If

        Dim selectedRow As DataGridViewRow = dbGrid.SelectedRows().Item(0)
        Dim selectedEmployeeId As Integer = selectedRow.Cells.Item(0).Value

        connection = OperationLog1TableAdapter.Connection

        Try
            Dim dateFromToCommand As String
            Dim dateFrom As Date = dtFrom.Value.Date

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text

            If cbDateRange.Checked Then
                Dim dateTo As Date = dtTo.Value.Date
                dateFromToCommand = "DateExecution >= @DateFrom AND DateExecution <= @DateTo;"
                sqlCommand.Parameters.AddWithValue("@DateFrom", dateFrom).DbType = DbType.Date
                sqlCommand.Parameters.AddWithValue("@DateTo", dateTo).DbType = DbType.Date
            Else
                dateFromToCommand = "DateExecution >= @DateFrom;"
                sqlCommand.Parameters.AddWithValue("@DateFrom", dateFrom).DbType = DbType.Date
            End If

            sqlCommand.CommandText = String.Format("SELECT OperationLog.Count, Routing.Tariff, OperationLog.TimeExecution, OperationLog.ArticulID FROM OperationLog INNER JOIN Routing ON OperationLog.ArticulID = Routing.ArticulID AND OperationLog.OperationID = Routing.OperationID WHERE OperationLog.EmployeeID = {0} AND {1}", selectedEmployeeId, dateFromToCommand)

            If connection.State.Equals(ConnectionState.Open) Then
                connection.Close()
            Else
                connection.Open()
            End If


            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()
            Dim resultList As List(Of Object) = New List(Of Object)

            If dbReader.HasRows Then
                While dbReader.Read()
                    Dim selectValue As Object() = New Object() {dbReader("Count"),
                                                        dbReader("Tariff"),
                                                        dbReader("TimeExecution"),
                                                        dbReader("ArticulID")}
                    resultList.Add(selectValue)
                End While
            End If

            'salary calculation
            'obj(0) - Count
            'obj(1) - Tariff
            'obj(2) - TimeExecution
            'obj(3) - ArticulID
            Dim salary As Double = 0D

            For Each obj As Object() In resultList
                Dim nonRatedTimeArticulId = 74

                If Not (obj(3) = nonRatedTimeArticulId) Then
                    salary += obj(0) * obj(1)
                Else
                    salary += getCurrentEmployeeTariffPerMinute(selectedEmployeeId) * obj(2)
                End If
            Next

            selectedRow.Cells.Item(5).Value = Math.Round(salary, 2).ToString()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Function getCurrentEmployeeTariffPerMinute(employeeId As Integer) As Double
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim employeeTariffPerMinute As Double

        connection = EmployeeTableAdapter.Connection

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = String.Format("SELECT PositionRateTime FROM PositionLevel WHERE PositionID = (SELECT PositionID FROM Employee WHERE EmployeeID = {0}) AND PositionLevel.PositionLevel = (SELECT Employee.PositionLevelID FROM Employee WHERE EmployeeID = {0});", employeeId)

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

            While dbReader.Read()
                employeeTariffPerMinute = dbReader("PositionRateTime")
                Exit While
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return employeeTariffPerMinute
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub
End Class