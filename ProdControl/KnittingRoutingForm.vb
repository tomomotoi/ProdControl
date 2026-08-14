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

    End Sub
End Class