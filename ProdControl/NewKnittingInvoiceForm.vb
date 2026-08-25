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

    End Sub
End Class