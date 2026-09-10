Imports Microsoft.Reporting.WinForms

Public Class ReportViewer

    Private Sub ReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'OperationDBDataSet1.ReportEmployee' table. You can move, or remove it, as needed.
        Me.ReportEmployeeTableAdapter.Fill(Me.OperationDBDataSet1.ReportEmployee)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.ReportEmployee' table. You can move, or remove it, as needed.
        'Me.ReportEmployeeTableAdapter.Fill(Me.OperationDBDataSet1.ReportEmployee)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Employee' table. You can move, or remove it, as needed.
        Me.EmployeeTableAdapter.Fill(Me.OperationDBDataSet1.Employee)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.ReportEmployee' table. You can move, or remove it, as needed.
        ' Me.ReportEmployeeTableAdapter.Fill(Me.OperationDBDataSet1.ReportEmployee)



    End Sub

    Private Sub btnLoadReport_Click(sender As Object, e As EventArgs) Handles btnLoadReport.Click
        Dim selectedRows As Integer = dgvEmployee.SelectedRows().Count
        Dim strEmployees As String = ""
        Dim row As DataGridViewRow
        Dim dfFrom As Date
        Dim dfTo As Date
        Dim i As Integer
        Dim RPDS As ReportDataSource


        If selectedRows = 0 Then
            MessageBox.Show("Выберите сотрудника для расчета")
            Exit Sub
        End If

        For i = 0 To selectedRows - 1
            row = dgvEmployee.SelectedRows().Item(i)
            strEmployees = strEmployees + row.Cells.Item(0).Value.ToString() + ", "
        Next
        strEmployees = strEmployees.Substring(0, Len(strEmployees) - 2)

        dfFrom = dtpFrom.Value.Date
        dfTo = dtpTo.Value.Date

        If dfFrom > dfTo Then
            MessageBox.Show("Дата начала периода не может быть больше даты конца периода.")
            Exit Sub
        End If

        Dim dataTable As OperationDBDataSet1.ReportEmployeeDataTable
        dataTable = New OperationDBDataSet1.ReportEmployeeDataTable()
        dataTable = Me.ReportEmployeeTableAdapter.GetEmployeeData(strEmployees, dfFrom, dfTo)

        If dataTable.Rows.Count <= 0 Then
            MessageBox.Show("Нет данных по Сотруднику.")
            Exit Sub

        End If
        dataTable.Rows(0).SetField(6, 12)


        'Me.ReportEmployeeTableAdapter.Fill(dataTable)

        'Me.ReportByEmployee.RefreshReport()

        'ReportByEmployeeViewer.Reset()
        Me.ReportByEmployeeViewer.LocalReport.DataSources.Clear()
        RPDS = New ReportDataSource()
        RPDS.Name = "ReportEmployee"
        RPDS.Value = dataTable
        Me.ReportByEmployeeViewer.LocalReport.DataSources.Add(RPDS)
        'Me.ReportByEmployeeViewer.LocalReport.ReportEmbeddedResource = "ProdControl.ReportEmployee.rdlc"
        Me.ReportByEmployeeViewer.RefreshReport()

        'ReportByEmployee.LocalReport.DataSources(0).Value = 


        Me.panelEmployee.Visible = False
        Me.panelReport.Visible = True


    End Sub

    Private Sub dtpTo_ValueChanged(sender As Object, e As EventArgs) Handles dtpTo.ValueChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.panelEmployee.Visible = True
        Me.panelReport.Visible = False
    End Sub
End Class