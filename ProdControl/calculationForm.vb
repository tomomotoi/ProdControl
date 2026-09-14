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
        Dim selectedRows As Integer = dbGrid.SelectedRows().Count
        If selectedRows = 0 Then
            MessageBox.Show("Выберите сотрудника для расчета", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim conn As OleDb.OleDbConnection = OperationLog1TableAdapter.Connection
        Dim res As New Hashtable()
        Dim calcTable As New Hashtable()

        For i As Integer = 0 To selectedRows - 1
            Dim row As DataGridViewRow = dbGrid.SelectedRows().Item(i)
            Dim emplID As Integer = Convert.ToInt32(row.Cells.Item(0).Value)
            Dim posId As Integer = GetEmployeePositionId(emplID)

            If posId = 9 Then
                ' --- РАСЧЕТ ДЛЯ ВЯЗАЛЬЩИЦЫ ---
                CalculateKnittingSalary(emplID, row)

                ' Расчет суммарного эффективного времени для нормы выработки
                Dim totalEffectiveTime As Double = 0
                Try
                    If conn.State = ConnectionState.Open Then conn.Close()
                    conn.Open()
                    Using cmd As OleDb.OleDbCommand = conn.CreateCommand()
                        Dim sql As String = "SELECT time_execution, machines_in_zone FROM KnittingOperationLog WHERE employee_id = @EmpID"
                        If cbDateRange.Checked Then
                            sql &= " AND (date_execution BETWEEN @FROM AND @TO)"
                            cmd.Parameters.AddWithValue("@EmpID", emplID)
                            cmd.Parameters.AddWithValue("@FROM", dtFrom.Value.Date).DbType = DbType.Date
                            cmd.Parameters.AddWithValue("@TO", dtTo.Value.Date).DbType = DbType.Date
                        Else
                            sql &= " AND (date_execution = @Dt)"
                            cmd.Parameters.AddWithValue("@EmpID", emplID)
                            cmd.Parameters.AddWithValue("@Dt", dtFrom.Value.Date).DbType = DbType.Date
                        End If
                        cmd.CommandText = sql
                        Using rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()
                            While rdr.Read()
                                Dim tExec As Double = If(IsDBNull(rdr("time_execution")), 0, Convert.ToDouble(rdr("time_execution")))
                                Dim mZone As Integer = If(IsDBNull(rdr("machines_in_zone")) OrElse Convert.ToInt32(rdr("machines_in_zone")) = 0, 1, Convert.ToInt32(rdr("machines_in_zone")))
                                totalEffectiveTime += (tExec / mZone)
                            End While
                        End Using
                    End Using
                Finally
                    If conn.State = ConnectionState.Open Then conn.Close()
                End Try

                res(emplID.ToString()) = totalEffectiveTime

                ' Расчет отработанных минут (рабочего дня)
                Dim wdRes As Integer = 480
                Dim dtWorkDays As DataTable
                If cbDateRange.Checked Then
                    dtWorkDays = OperationLog1TableAdapter.GetWorkDays(emplID.ToString(), dtFrom.Value.Date, dtTo.Value.Date)
                    If dtWorkDays.Rows.Count > 0 Then
                        wdRes = 0
                        For Each rowDW As DataRow In dtWorkDays.Rows
                            Dim wdDuration As Integer = rowDW("workDay")
                            Dim dtDates As DataTable = OperationLog1TableAdapter.GetWorkDaysDates(emplID.ToString(), dtFrom.Value.Date, dtTo.Value.Date, wdDuration)
                            wdRes += (wdDuration * dtDates.Rows.Count)
                        Next
                    End If
                Else
                    dtWorkDays = OperationLog1TableAdapter.GetWDPerDay(emplID.ToString(), dtFrom.Value.Date.ToString())
                    If dtWorkDays.Rows.Count > 0 Then wdRes = dtWorkDays.Rows(0).Item("workDay")
                End If

                calcTable(emplID.ToString()) = If(wdRes > 0, totalEffectiveTime / wdRes, 0)

            Else
                ' --- РАСЧЕТ ДЛЯ ШВЕЙ (И ОСТАЛЬНЫХ ДОЛЖНОСТЕЙ) ---
                calculateSalaryNewRow(emplID, row)

                ' Первичный проход расчета времени выполнения швеи
                Dim exTimeSum As Double = 0
                Try
                    If conn.State = ConnectionState.Open Then conn.Close()
                    conn.Open()
                    Using exeCom As OleDb.OleDbCommand = conn.CreateCommand()
                        If cbDateRange.Checked Then
                            exeCom.CommandText = "SELECT OperationID, ArticulID, TimeExecution, Count FROM OperationLog WHERE EmployeeID = @EmpID AND (DateExecution BETWEEN @FROM AND @TO)"
                            exeCom.Parameters.AddWithValue("@EmpID", emplID)
                            exeCom.Parameters.AddWithValue("@FROM", dtFrom.Value.Date).DbType = DbType.Date
                            exeCom.Parameters.AddWithValue("@TO", dtTo.Value.Date).DbType = DbType.Date
                        Else
                            exeCom.CommandText = "SELECT OperationID, ArticulID, TimeExecution, Count FROM OperationLog WHERE EmployeeID = @EmpID AND DateExecution = @Dt"
                            exeCom.Parameters.AddWithValue("@EmpID", emplID)
                            exeCom.Parameters.AddWithValue("@Dt", dtFrom.Value.Date).DbType = DbType.Date
                        End If

                        Using db_reader As OleDb.OleDbDataReader = exeCom.ExecuteReader()
                            While db_reader.Read()
                                Dim execTime As Double = db_reader("TimeExecution")
                                Dim itmsCount As Double = Convert.ToDouble(db_reader("Count"))
                                Dim artID As String = db_reader("ArticulID").ToString()
                                Dim opID As String = db_reader("OperationID").ToString()

                                If execTime = 0 Then
                                    Using rout As OleDb.OleDbCommand = conn.CreateCommand()
                                        rout.CommandText = "SELECT TimeExecute FROM Routing WHERE ArticulID = @Art AND OperationID = @Op"
                                        rout.Parameters.AddWithValue("@Art", artID)
                                        rout.Parameters.AddWithValue("@Op", opID)
                                        Dim tObj = rout.ExecuteScalar()
                                        If tObj IsNot Nothing AndAlso Not IsDBNull(tObj) Then
                                            exTimeSum += Math.Round(Convert.ToDouble(tObj) * itmsCount, 6)
                                        End If
                                    End Using
                                Else
                                    exTimeSum += execTime
                                End If
                            End While
                        End Using
                    End Using
                Finally
                    If conn.State = ConnectionState.Open Then conn.Close()
                End Try

                res(emplID.ToString()) = exTimeSum

                Dim wdRes As Integer = 480
                Dim dtWorkDays As DataTable
                If cbDateRange.Checked Then
                    dtWorkDays = OperationLog1TableAdapter.GetWorkDays(emplID.ToString(), dtFrom.Value.Date, dtTo.Value.Date)
                    If dtWorkDays.Rows.Count > 0 Then
                        wdRes = 0
                        For Each rowDW As DataRow In dtWorkDays.Rows
                            Dim wdDuration As Integer = rowDW("workDay")
                            Dim dtDates As DataTable = OperationLog1TableAdapter.GetWorkDaysDates(emplID.ToString(), dtFrom.Value.Date, dtTo.Value.Date, wdDuration)
                            wdRes += (wdDuration * dtDates.Rows.Count)
                        Next
                    End If
                Else
                    dtWorkDays = OperationLog1TableAdapter.GetWDPerDay(emplID.ToString(), dtFrom.Value.Date.ToString())
                    If dtWorkDays.Rows.Count > 0 Then wdRes = dtWorkDays.Rows(0).Item("workDay")
                End If

                calcTable(emplID.ToString()) = If(wdRes > 0, exTimeSum / wdRes, 0)
            End If

            ' Заполнение колонок таблицы для текущей строки
            row.Cells.Item(6).Value = Format(Convert.ToDouble(calcTable(emplID.ToString())) * 100, "###0.00")
            row.Cells.Item(7).Value = Format(Convert.ToDouble(res(emplID.ToString())), "##0.00")
        Next
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

    Private Sub calculateSalaryNewRow(selectedEmployeeId As Integer, selectedRow As DataGridViewRow)
        Dim connection As OleDb.OleDbConnection = OperationLog1TableAdapter.Connection
        Dim sqlCommand As OleDb.OleDbCommand

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

            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
            connection.Open()

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()
            Dim resultList As New List(Of Object())

            If dbReader.HasRows Then
                While dbReader.Read()
                    Dim selectValue As Object() = New Object() {
                    dbReader("Count"),
                    dbReader("Tariff"),
                    dbReader("TimeExecution"),
                    dbReader("ArticulID")
                }
                    resultList.Add(selectValue)
                End While
            End If

            Dim salary As Double = 0D
            For Each obj As Object() In resultList
                Dim nonRatedTimeArticulId = 74
                If Not (Convert.ToInt32(obj(3)) = nonRatedTimeArticulId) Then
                    salary += Convert.ToDouble(obj(0)) * Convert.ToDouble(obj(1))
                Else
                    salary += getCurrentEmployeeTariffPerMinute(selectedEmployeeId) * Convert.ToDouble(obj(2))
                End If
            Next

            selectedRow.Cells.Item(5).Value = Math.Round(salary, 2).ToString()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If connection.State = ConnectionState.Open Then connection.Close()
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


    Private Function GetEmployeePositionId(employeeId As Integer) As Integer
        Dim posId As Integer = 0
        Dim conn As OleDb.OleDbConnection = EmployeeTableAdapter.Connection
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()
            Using cmd As OleDb.OleDbCommand = conn.CreateCommand()
                cmd.CommandText = "SELECT PositionID FROM Employee WHERE EmployeeID = @EmpID"
                cmd.Parameters.AddWithValue("@EmpID", employeeId)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    posId = Convert.ToInt32(result)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Ошибка при определении должности: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
        Return posId
    End Function

    Private Sub CalculateKnittingSalary(emplID As Integer, row As DataGridViewRow)
        Dim conn As OleDb.OleDbConnection = OperationLog1TableAdapter.Connection
        Dim totalSalary As Double = 0

        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim cmd As OleDb.OleDbCommand = conn.CreateCommand()

            ' Группируем по количеству машин в зоне обслуживания
            Dim sql As String = "SELECT machines_in_zone, SUM(time_execution) AS zone_time " &
                            "FROM KnittingOperationLog WHERE employee_id = @EmpID "

            If cbDateRange.Checked Then
                sql &= " AND (date_execution BETWEEN @FROM AND @TO)"
                cmd.Parameters.AddWithValue("@EmpID", emplID)
                cmd.Parameters.AddWithValue("@FROM", dtFrom.Value.Date).DbType = DbType.Date
                cmd.Parameters.AddWithValue("@TO", dtTo.Value.Date).DbType = DbType.Date
            Else
                sql &= " AND (date_execution = @Dt)"
                cmd.Parameters.AddWithValue("@EmpID", emplID)
                cmd.Parameters.AddWithValue("@Dt", dtFrom.Value.Date).DbType = DbType.Date
            End If

            sql &= " GROUP BY machines_in_zone"
            cmd.CommandText = sql

            Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader()

            ' Для каждого подблока машин делим набранную сумму времени на их количество и прибавляем к итоговой ЗП
            While reader.Read()
                Dim zoneTime As Double = If(IsDBNull(reader("zone_time")), 0, Convert.ToDouble(reader("zone_time")))
                Dim mZone As Integer = If(IsDBNull(reader("machines_in_zone")) OrElse Convert.ToInt32(reader("machines_in_zone")) = 0, 1, Convert.ToInt32(reader("machines_in_zone")))

                ' Формула: (Сумма времени зоны_1 / Зона_1) + (Сумма времени зоны_2 / Зона_2) + ...
                totalSalary += (zoneTime / mZone)
            End While
            reader.Close()

            Dim finalValue As String = Format(Math.Round(totalSalary, 2), "0.00")

            row.Cells.Item(5).Value = finalValue ' Заработная плата
            row.Cells.Item(6).Value = finalValue ' Норма Выработки
            row.Cells.Item(7).Value = finalValue ' Время выполнения

        Catch ex As Exception
            MessageBox.Show("Ошибка расчета для вязальщицы: " & ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
End Class