Imports ProdControl.OperationDBDataSet2

Public Class newBill

    Dim shouldBeEmpty As Boolean = False

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub newBill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'OperationDBDataSet12.IdleOperation' table. You can move, or remove it, as needed.
        Me.IdleOperationTableAdapter.Fill(Me.OperationDBDataSet12.IdleOperation)

        'TODO: This line of code loads data into the 'OperationDBDataSet12.Employee' table. You can move, or remove it, as needed.
        Me.EmployeeTableAdapter1.Fill(Me.OperationDBDataSet12.Employee)
        'TODO: This line of code loads data into the 'OperationDBDataSet12.Articul' table. You can move, or remove it, as needed.
        Me.ArticulTableAdapter1.Fill(Me.OperationDBDataSet12.Articul)
        'TODO: This line of code loads data into the 'OperationDBDataSet12.Operation' table. You can move, or remove it, as needed.
        Me.OperationTableAdapter1.Fill(Me.OperationDBDataSet12.Operation)
        'TODO: This line of code loads data into the 'OperationDBDataSet11.OperationLog' table. You can move, or remove it, as needed.
        Me.OperationLog1TableAdapter.Fill(Me.OperationDBDataSet11.OperationLog1)

        cbArticul_SelectedIndexChanged(sender, e)
        tbOperationTime.Text = 0
        tbWorkday.Text = 480
        tbCount.Text = 0
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lbOperation.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles cbIsStandard.CheckedChanged
        If (cbIsStandard.Checked) Then
            tbOperationTime.Enabled = True
            cbTypeOfNonRateSituation.Enabled = True
            btnCalculateExecutionTime.Enabled = True

            tbCount.Enabled = False
            tbCount.Text = 0
        Else
            tbOperationTime.Enabled = False
            cbTypeOfNonRateSituation.Enabled = False
            tbCount.Enabled = True
            btnCalculateExecutionTime.Enabled = False

            tbOperationTime.Text = 0
        End If
    End Sub

    Private Sub AddNewRecord()

    End Sub

    Private Sub cbOperation_SelectionChangeCommitted(sender As Object, e As EventArgs)


    End Sub

    Private Sub cbOperation_TextUpdate(sender As Object, e As EventArgs)

    End Sub

    Private Sub cbOperation_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Dim strMember = OperationBindingSource2.DataMember
        Dim drvTime As DataRowView
        Dim opRow As OperationDBDataSet1.OperationRow
        drvTime = cbOperation.SelectedItem

        If Not IsNothing(drvTime) Then
            opRow = drvTime.Row
            tbOperationTime.Text = opRow.OperationTimewithKoef
        End If


    End Sub

    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        'OperationLogBindingSource.DataSource

        Dim emplID As Integer
        Dim artID As Integer
        Dim dtDateExecu As Date
        'Dim tExec As Integer
        Dim bStandard As Boolean
        Dim opId As Integer
        'Dim row As DataGridViewRow

        If tbWorkday.Text = "" Then
            '            MsgBox("Поле продолжительность рабочего времени не заполнено!", MsgBoxStyle.Exclamation, "Ввод данных")
            MessageBox.Show("Поле продолжительность рабочего времени не заполнено!", "Рабочий день",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWorkday.Focus()
            Exit Sub
        Else


        End If

        emplID = cbEmployee.DataSource.Current.Row.EmployeeID

        artID = cbArticul.DataSource.Current.Row.ArticulID
        'tExec = Integer.Parse(tbOperationTime.Text)
        bStandard = cbIsStandard.Checked
        dtDateExecu = dtPickerOpDate.Value
        opId = cbOperation.DataSource.Current.Row.OperationID

        Try
            Dim operationTime As Double = Double.Parse(tbOperationTime.Text)

            Dim row As Object() = New Object() {cbEmployee.SelectedValue,
                                            cbEmployee.SelectedItem.Row.EmployeeName,
                                            tbLogNum.Text,
                                            dtPickerOpDate.Value,
                                            cbArticul.SelectedValue,
                                            cbArticul.SelectedItem.Row.ArticulName,
                                            operationTime.ToString(),
                                            tbCount.Text,
                                            cbOperation.SelectedValue,
                                            cbOperation.SelectedItem.Row.OperationName,
                                            tbWorkday.Text}
            dgOperationLog.Rows.Add(row)
        Catch ex As Exception
            MessageBox.Show("Неверный формат числа в поле 'Время выполнения'!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
    End Sub

    Private Sub pnlTop_Paint(sender As Object, e As PaintEventArgs) Handles pnlTop.Paint

    End Sub

    Private Sub cbOperation_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cbOperation.SelectedIndexChanged
        'Dim strMember = OperationBindingSource2.DataMember
        Dim drvTime As DataRowView
        Dim opRow As OperationDBDataSet1.OperationRow
        drvTime = cbOperation.SelectedItem
        'GetOperationExecTime()

        If Not IsNothing(drvTime) Then
            opRow = drvTime.Row
            tbOperationTime.Text = opRow.OperationTimewithKoef
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rows As DataGridViewRowCollection
        Dim rowsCount As Integer
        Dim i As Integer
        Dim row As DataGridViewRow
        Dim a As Object
        Dim employee As Integer
        Dim operation As Integer
        Dim articul As Integer
        Dim dateex As Date
        Dim exectime As Integer
        Dim count As Integer
        Dim doc_num As String
        Dim workDay As Integer

        rows = dgOperationLog.Rows
        rowsCount = rows.Count - 1

        For i = 0 To rowsCount - 1
            row = rows.Item(i)
            employee = row.Cells.Item(0).Value
            operation = row.Cells.Item(8).Value
            articul = row.Cells.Item(4).Value
            dateex = CType(row.Cells.Item(3).Value, Date)
            exectime = row.Cells.Item(6).Value
            count = row.Cells.Item(7).Value
            doc_num = row.Cells.Item(2).Value
            workDay = CType(row.Cells.Item(10).Value, Integer)

            a = OperationLog1TableAdapter.Insert(employee,
                                                operation,
                                                articul,
                                                dateex.Date,
                                                exectime,
                                                count,
                                                doc_num, workDay)

        Next

        MsgBox("Новые записи сохранены в Базе данных", MsgBoxStyle.Information, "Сохранение")

        dgOperationLog.Rows.Clear()

    End Sub

    Private Sub cbArticul_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbArticul.SelectedIndexChanged
        Dim conn As OleDb.OleDbConnection
        Dim exeCom As OleDb.OleDbCommand
        Dim opID As String = ""

        If cbArticul.SelectedValue Is Nothing Then
            Exit Sub
        End If

        conn = OperationTableAdapter1.Connection()

        Try
            ' выбираем операции из техкарты
            exeCom = conn.CreateCommand()
            exeCom.CommandText = "SELECT Routing.OperationID As [OID] FROM Routing WHERE Routing.ArticulID = " + cbArticul.SelectedValue.ToString + " ORDER BY Routing.OperationID"

            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()
            Dim db_reader As OleDb.OleDbDataReader = exeCom.ExecuteReader()

            If db_reader.HasRows Then
                While db_reader.Read()
                    'addCost = routRead("DopCost")

                    opID = opID + db_reader("OID").ToString + ", "

                End While

                opID = opID.Substring(0, Len(opID) - 2)

                OperationBindingSource3.Filter = "OperationID in (" + opID + ")"
                cbOperation.Enabled = True

            Else
                OperationBindingSource3.Filter = "OperationID = 0"
                cbOperation.Enabled = False

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles tbWorkday.TextChanged
        Dim inWD As Integer
        If tbWorkday.Text <> "" Then
            inWD = tbWorkday.Text
            If inWD > 480 Then
                MessageBox.Show("Продолжительность рабочего дня не может превышать 480", "Рабочий день",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)

                tbWorkday.Text = 480
            End If
        End If

    End Sub


    Private Sub tbWorkday_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbWorkday.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub tbCount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbCount.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub tbOperationTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbOperationTime.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub dtPickerOpDate_ValueChanged(sender As Object, e As EventArgs) Handles dtPickerOpDate.ValueChanged
        tbWorkday.Text = ""
    End Sub

    Private Sub cbEmployee_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbEmployee.SelectedValueChanged
        tbWorkday.Text = ""
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim selectedDataGridRowIndex As Integer = dgOperationLog.CurrentRow.Index

        If selectedDataGridRowIndex < dgOperationLog.Rows.Count - 1 Then
            dgOperationLog.Rows.RemoveAt(selectedDataGridRowIndex)
        End If
    End Sub

    Private Sub btnCalculateExecutionTime_Click(sender As Object, e As EventArgs) Handles btnCalculateExecutionTime.Click
        Dim executionTime As Double
        Dim situationPercentage = getIdleTimePercentage()

        If Double.TryParse(tbOperationTime.Text, executionTime) And Not IsDBNull(situationPercentage) Then
            Dim result = executionTime * situationPercentage

            tbOperationTime.Text = result
        Else
            MessageBox.Show("Неверный формат даты в поле 'Время выполнения' или не удалось получить процент простоя из БД!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function getIdleTimePercentage() As Double
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim selectedSituationId As Integer = cbTypeOfNonRateSituation.SelectedValue
        Dim queryResult As Double = vbNull

        connection = New OleDb.OleDbConnection(My.Settings.OperationDBConnectionString)

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = String.Format("SELECT cost_percentage FROM IdleOperation WHERE idle_operation_id = {0}", selectedSituationId)

            Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

            While dbReader.Read()
                queryResult = dbReader("cost_percentage") / 100
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return queryResult
    End Function
End Class