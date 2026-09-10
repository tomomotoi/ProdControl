<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class KnittingRoutingForm
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KnittingRoutingForm))
        Me.btnSeachArt = New System.Windows.Forms.Button()
        Me.lbArticul = New System.Windows.Forms.Label()
        Me.cbArticle = New System.Windows.Forms.ComboBox()
        Me.ArticulBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet0 = New OperationDBDataSet0()
        Me.LabelClothingPart = New System.Windows.Forms.Label()
        Me.cbKnittingPart = New System.Windows.Forms.ComboBox()
        Me.ClothingPartsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelModelPartsAmount = New System.Windows.Forms.Label()
        Me.cbModelPartAmount = New System.Windows.Forms.ComboBox()
        Me.cbMachineCarriageAmount = New System.Windows.Forms.ComboBox()
        Me.LabelMachineCarreagesAmount = New System.Windows.Forms.Label()
        Me.cbKnittingMachine = New System.Windows.Forms.ComboBox()
        Me.KnittingMachinesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelKnittingMachine = New System.Windows.Forms.Label()
        Me.cbCoeff2 = New System.Windows.Forms.ComboBox()
        Me.CoefficientsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbCoeff1 = New System.Windows.Forms.ComboBox()
        Me.LabelAdditionalCoefficient = New System.Windows.Forms.Label()
        Me.LabelWithCoefficient = New System.Windows.Forms.Label()
        Me.LabelAvgValue = New System.Windows.Forms.Label()
        Me.LabelWithKit = New System.Windows.Forms.Label()
        Me.tbAvgValue = New System.Windows.Forms.TextBox()
        Me.tbWithCoefficient = New System.Windows.Forms.TextBox()
        Me.tbWithKit = New System.Windows.Forms.TextBox()
        Me.tbTotal = New System.Windows.Forms.TextBox()
        Me.LabelTotal = New System.Windows.Forms.Label()
        Me.ArticulTableAdapter = New OperationDBDataSet0TableAdapters.ArticulTableAdapter()
        Me.ClothingPartsTableAdapter = New OperationDBDataSet0TableAdapters.ClothingPartsTableAdapter()
        Me.KnittingMachinesTableAdapter = New OperationDBDataSet0TableAdapters.KnittingMachinesTableAdapter()
        Me.CoefficientsTableAdapter = New OperationDBDataSet0TableAdapters.CoefficientsTableAdapter()
        Me.CoefficientsBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.dataGridRouting = New System.Windows.Forms.DataGridView()
        Me.btSave = New System.Windows.Forms.Button()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.PartColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ModelPartAmountColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineCarriageAmountColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Coeff1Column = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Coeff2Column = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AvgValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WithCoefficientColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WithKitColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbRate = New System.Windows.Forms.TextBox()
        Me.LabelRate = New System.Windows.Forms.Label()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClothingPartsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KnittingMachinesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CoefficientsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CoefficientsBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dataGridRouting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSeachArt
        '
        Me.btnSeachArt.Location = New System.Drawing.Point(149, 25)
        Me.btnSeachArt.Name = "btnSeachArt"
        Me.btnSeachArt.Size = New System.Drawing.Size(75, 23)
        Me.btnSeachArt.TabIndex = 19
        Me.btnSeachArt.Text = "Найти"
        Me.btnSeachArt.UseVisualStyleBackColor = True
        '
        'lbArticul
        '
        Me.lbArticul.AutoSize = True
        Me.lbArticul.Location = New System.Drawing.Point(9, 10)
        Me.lbArticul.Name = "lbArticul"
        Me.lbArticul.Size = New System.Drawing.Size(48, 13)
        Me.lbArticul.TabIndex = 18
        Me.lbArticul.Text = "Артикул"
        '
        'cbArticle
        '
        Me.cbArticle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArticle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArticle.DataSource = Me.ArticulBindingSource
        Me.cbArticle.DisplayMember = "ArticulName"
        Me.cbArticle.FormattingEnabled = True
        Me.cbArticle.Location = New System.Drawing.Point(12, 26)
        Me.cbArticle.Name = "cbArticle"
        Me.cbArticle.Size = New System.Drawing.Size(131, 21)
        Me.cbArticle.TabIndex = 17
        Me.cbArticle.ValueMember = "ArticulID"
        '
        'ArticulBindingSource
        '
        Me.ArticulBindingSource.DataMember = "Articul"
        Me.ArticulBindingSource.DataSource = Me.OperationDBDataSet0
        '
        'OperationDBDataSet0
        '
        Me.OperationDBDataSet0.DataSetName = "OperationDBDataSet0"
        Me.OperationDBDataSet0.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LabelClothingPart
        '
        Me.LabelClothingPart.AutoSize = True
        Me.LabelClothingPart.Location = New System.Drawing.Point(9, 56)
        Me.LabelClothingPart.Name = "LabelClothingPart"
        Me.LabelClothingPart.Size = New System.Drawing.Size(48, 13)
        Me.LabelClothingPart.TabIndex = 20
        Me.LabelClothingPart.Text = "Деталь:"
        '
        'cbKnittingPart
        '
        Me.cbKnittingPart.DataSource = Me.ClothingPartsBindingSource
        Me.cbKnittingPart.DisplayMember = "part_name"
        Me.cbKnittingPart.FormattingEnabled = True
        Me.cbKnittingPart.Location = New System.Drawing.Point(12, 72)
        Me.cbKnittingPart.Name = "cbKnittingPart"
        Me.cbKnittingPart.Size = New System.Drawing.Size(131, 21)
        Me.cbKnittingPart.TabIndex = 21
        Me.cbKnittingPart.ValueMember = "id"
        '
        'ClothingPartsBindingSource
        '
        Me.ClothingPartsBindingSource.DataMember = "ClothingParts"
        Me.ClothingPartsBindingSource.DataSource = Me.OperationDBDataSet0
        '
        'LabelModelPartsAmount
        '
        Me.LabelModelPartsAmount.AutoSize = True
        Me.LabelModelPartsAmount.Location = New System.Drawing.Point(291, 56)
        Me.LabelModelPartsAmount.Name = "LabelModelPartsAmount"
        Me.LabelModelPartsAmount.Size = New System.Drawing.Size(19, 13)
        Me.LabelModelPartsAmount.TabIndex = 22
        Me.LabelModelPartsAmount.Text = "М:"
        '
        'cbModelPartAmount
        '
        Me.cbModelPartAmount.FormattingEnabled = True
        Me.cbModelPartAmount.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50"})
        Me.cbModelPartAmount.Location = New System.Drawing.Point(294, 72)
        Me.cbModelPartAmount.Name = "cbModelPartAmount"
        Me.cbModelPartAmount.Size = New System.Drawing.Size(57, 21)
        Me.cbModelPartAmount.TabIndex = 23
        '
        'cbMachineCarriageAmount
        '
        Me.cbMachineCarriageAmount.FormattingEnabled = True
        Me.cbMachineCarriageAmount.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cbMachineCarriageAmount.Location = New System.Drawing.Point(358, 72)
        Me.cbMachineCarriageAmount.Name = "cbMachineCarriageAmount"
        Me.cbMachineCarriageAmount.Size = New System.Drawing.Size(57, 21)
        Me.cbMachineCarriageAmount.TabIndex = 25
        '
        'LabelMachineCarreagesAmount
        '
        Me.LabelMachineCarreagesAmount.AutoSize = True
        Me.LabelMachineCarreagesAmount.Location = New System.Drawing.Point(355, 56)
        Me.LabelMachineCarreagesAmount.Name = "LabelMachineCarreagesAmount"
        Me.LabelMachineCarreagesAmount.Size = New System.Drawing.Size(18, 13)
        Me.LabelMachineCarreagesAmount.TabIndex = 24
        Me.LabelMachineCarreagesAmount.Text = "П:"
        '
        'cbKnittingMachine
        '
        Me.cbKnittingMachine.DataSource = Me.KnittingMachinesBindingSource
        Me.cbKnittingMachine.DisplayMember = "machine_displayname"
        Me.cbKnittingMachine.FormattingEnabled = True
        Me.cbKnittingMachine.Location = New System.Drawing.Point(150, 72)
        Me.cbKnittingMachine.Name = "cbKnittingMachine"
        Me.cbKnittingMachine.Size = New System.Drawing.Size(121, 21)
        Me.cbKnittingMachine.TabIndex = 27
        Me.cbKnittingMachine.ValueMember = "id"
        '
        'KnittingMachinesBindingSource
        '
        Me.KnittingMachinesBindingSource.DataMember = "KnittingMachines"
        Me.KnittingMachinesBindingSource.DataSource = Me.OperationDBDataSet0
        '
        'LabelKnittingMachine
        '
        Me.LabelKnittingMachine.AutoSize = True
        Me.LabelKnittingMachine.Location = New System.Drawing.Point(147, 56)
        Me.LabelKnittingMachine.Name = "LabelKnittingMachine"
        Me.LabelKnittingMachine.Size = New System.Drawing.Size(83, 13)
        Me.LabelKnittingMachine.TabIndex = 26
        Me.LabelKnittingMachine.Text = "Оборудование:"
        '
        'cbCoeff2
        '
        Me.cbCoeff2.DataSource = Me.CoefficientsBindingSource1
        Me.cbCoeff2.DisplayMember = "coefficient_value"
        Me.cbCoeff2.FormattingEnabled = True
        Me.cbCoeff2.Location = New System.Drawing.Point(500, 72)
        Me.cbCoeff2.Name = "cbCoeff2"
        Me.cbCoeff2.Size = New System.Drawing.Size(56, 21)
        Me.cbCoeff2.TabIndex = 31
        Me.cbCoeff2.ValueMember = "coefficient_id"
        '
        'CoefficientsBindingSource
        '
        Me.CoefficientsBindingSource.DataMember = "Coefficients"
        Me.CoefficientsBindingSource.DataSource = Me.OperationDBDataSet0
        '
        'cbCoeff1
        '
        Me.cbCoeff1.DataSource = Me.CoefficientsBindingSource
        Me.cbCoeff1.DisplayMember = "coefficient_value"
        Me.cbCoeff1.FormattingEnabled = True
        Me.cbCoeff1.Location = New System.Drawing.Point(438, 72)
        Me.cbCoeff1.Name = "cbCoeff1"
        Me.cbCoeff1.Size = New System.Drawing.Size(56, 21)
        Me.cbCoeff1.TabIndex = 29
        Me.cbCoeff1.ValueMember = "coefficient_id"
        '
        'LabelAdditionalCoefficient
        '
        Me.LabelAdditionalCoefficient.AutoSize = True
        Me.LabelAdditionalCoefficient.Location = New System.Drawing.Point(435, 56)
        Me.LabelAdditionalCoefficient.Name = "LabelAdditionalCoefficient"
        Me.LabelAdditionalCoefficient.Size = New System.Drawing.Size(117, 13)
        Me.LabelAdditionalCoefficient.TabIndex = 28
        Me.LabelAdditionalCoefficient.Text = "Коэфф. уч. доп. затр.:"
        '
        'LabelWithCoefficient
        '
        Me.LabelWithCoefficient.AutoSize = True
        Me.LabelWithCoefficient.Location = New System.Drawing.Point(73, 109)
        Me.LabelWithCoefficient.Name = "LabelWithCoefficient"
        Me.LabelWithCoefficient.Size = New System.Drawing.Size(57, 13)
        Me.LabelWithCoefficient.TabIndex = 34
        Me.LabelWithCoefficient.Text = "С коэфф.:"
        '
        'LabelAvgValue
        '
        Me.LabelAvgValue.AutoSize = True
        Me.LabelAvgValue.Location = New System.Drawing.Point(9, 109)
        Me.LabelAvgValue.Name = "LabelAvgValue"
        Me.LabelAvgValue.Size = New System.Drawing.Size(62, 13)
        Me.LabelAvgValue.TabIndex = 32
        Me.LabelAvgValue.Text = "Средн. зн.:"
        '
        'LabelWithKit
        '
        Me.LabelWithKit.AutoSize = True
        Me.LabelWithKit.Location = New System.Drawing.Point(136, 109)
        Me.LabelWithKit.Name = "LabelWithKit"
        Me.LabelWithKit.Size = New System.Drawing.Size(55, 13)
        Me.LabelWithKit.TabIndex = 36
        Me.LabelWithKit.Text = "С компл.:"
        '
        'tbAvgValue
        '
        Me.tbAvgValue.Location = New System.Drawing.Point(12, 125)
        Me.tbAvgValue.Name = "tbAvgValue"
        Me.tbAvgValue.Size = New System.Drawing.Size(58, 20)
        Me.tbAvgValue.TabIndex = 38
        '
        'tbWithCoefficient
        '
        Me.tbWithCoefficient.Enabled = False
        Me.tbWithCoefficient.Location = New System.Drawing.Point(76, 125)
        Me.tbWithCoefficient.Name = "tbWithCoefficient"
        Me.tbWithCoefficient.Size = New System.Drawing.Size(58, 20)
        Me.tbWithCoefficient.TabIndex = 39
        '
        'tbWithKit
        '
        Me.tbWithKit.Enabled = False
        Me.tbWithKit.Location = New System.Drawing.Point(139, 125)
        Me.tbWithKit.Name = "tbWithKit"
        Me.tbWithKit.Size = New System.Drawing.Size(58, 20)
        Me.tbWithKit.TabIndex = 40
        '
        'tbTotal
        '
        Me.tbTotal.Enabled = False
        Me.tbTotal.Location = New System.Drawing.Point(139, 168)
        Me.tbTotal.Name = "tbTotal"
        Me.tbTotal.Size = New System.Drawing.Size(58, 20)
        Me.tbTotal.TabIndex = 42
        '
        'LabelTotal
        '
        Me.LabelTotal.AutoSize = True
        Me.LabelTotal.Location = New System.Drawing.Point(136, 152)
        Me.LabelTotal.Name = "LabelTotal"
        Me.LabelTotal.Size = New System.Drawing.Size(40, 13)
        Me.LabelTotal.TabIndex = 41
        Me.LabelTotal.Text = "Итого:"
        '
        'ArticulTableAdapter
        '
        Me.ArticulTableAdapter.ClearBeforeFill = True
        '
        'ClothingPartsTableAdapter
        '
        Me.ClothingPartsTableAdapter.ClearBeforeFill = True
        '
        'KnittingMachinesTableAdapter
        '
        Me.KnittingMachinesTableAdapter.ClearBeforeFill = True
        '
        'CoefficientsTableAdapter
        '
        Me.CoefficientsTableAdapter.ClearBeforeFill = True
        '
        'CoefficientsBindingSource1
        '
        Me.CoefficientsBindingSource1.DataMember = "Coefficients"
        Me.CoefficientsBindingSource1.DataSource = Me.OperationDBDataSet0
        '
        'dataGridRouting
        '
        Me.dataGridRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridRouting.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PartColumn, Me.MachineColumn, Me.ModelPartAmountColumn, Me.MachineCarriageAmountColumn, Me.Coeff1Column, Me.Coeff2Column, Me.AvgValueColumn, Me.WithCoefficientColumn, Me.WithKitColumn})
        Me.dataGridRouting.Location = New System.Drawing.Point(12, 194)
        Me.dataGridRouting.Name = "dataGridRouting"
        Me.dataGridRouting.Size = New System.Drawing.Size(776, 244)
        Me.dataGridRouting.TabIndex = 43
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btSave.Location = New System.Drawing.Point(713, 165)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(75, 23)
        Me.btSave.TabIndex = 46
        Me.btSave.Text = "Сохранить"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDel.Location = New System.Drawing.Point(631, 165)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(75, 23)
        Me.btnDel.TabIndex = 45
        Me.btnDel.Text = "Удалить"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(549, 165)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 44
        Me.btnAdd.Text = "Добавить"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'PartColumn
        '
        Me.PartColumn.HeaderText = "Деталь"
        Me.PartColumn.Name = "PartColumn"
        Me.PartColumn.ReadOnly = True
        '
        'MachineColumn
        '
        Me.MachineColumn.HeaderText = "Оборудование"
        Me.MachineColumn.Name = "MachineColumn"
        Me.MachineColumn.ReadOnly = True
        '
        'ModelPartAmountColumn
        '
        Me.ModelPartAmountColumn.HeaderText = "М"
        Me.ModelPartAmountColumn.Name = "ModelPartAmountColumn"
        Me.ModelPartAmountColumn.ReadOnly = True
        Me.ModelPartAmountColumn.Width = 20
        '
        'MachineCarriageAmountColumn
        '
        Me.MachineCarriageAmountColumn.HeaderText = "П"
        Me.MachineCarriageAmountColumn.Name = "MachineCarriageAmountColumn"
        Me.MachineCarriageAmountColumn.ReadOnly = True
        Me.MachineCarriageAmountColumn.Width = 20
        '
        'Coeff1Column
        '
        Me.Coeff1Column.HeaderText = "Коэф. доп. 1"
        Me.Coeff1Column.Name = "Coeff1Column"
        Me.Coeff1Column.ReadOnly = True
        Me.Coeff1Column.Width = 30
        '
        'Coeff2Column
        '
        Me.Coeff2Column.HeaderText = "Коэф. доп. 2"
        Me.Coeff2Column.Name = "Coeff2Column"
        Me.Coeff2Column.ReadOnly = True
        Me.Coeff2Column.Width = 30
        '
        'AvgValueColumn
        '
        Me.AvgValueColumn.HeaderText = "Средн. зн."
        Me.AvgValueColumn.Name = "AvgValueColumn"
        Me.AvgValueColumn.ReadOnly = True
        Me.AvgValueColumn.Width = 65
        '
        'WithCoefficientColumn
        '
        Me.WithCoefficientColumn.HeaderText = "С коэфф."
        Me.WithCoefficientColumn.Name = "WithCoefficientColumn"
        Me.WithCoefficientColumn.ReadOnly = True
        Me.WithCoefficientColumn.Width = 65
        '
        'WithKitColumn
        '
        Me.WithKitColumn.HeaderText = "С компл."
        Me.WithKitColumn.Name = "WithKitColumn"
        Me.WithKitColumn.ReadOnly = True
        Me.WithKitColumn.Width = 65
        '
        'tbRate
        '
        Me.tbRate.Enabled = False
        Me.tbRate.Location = New System.Drawing.Point(485, 167)
        Me.tbRate.Name = "tbRate"
        Me.tbRate.Size = New System.Drawing.Size(58, 20)
        Me.tbRate.TabIndex = 48
        '
        'LabelRate
        '
        Me.LabelRate.AutoSize = True
        Me.LabelRate.Location = New System.Drawing.Point(482, 151)
        Me.LabelRate.Name = "LabelRate"
        Me.LabelRate.Size = New System.Drawing.Size(44, 13)
        Me.LabelRate.TabIndex = 47
        Me.LabelRate.Text = "Норма:"
        '
        'KnittingRoutingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.tbRate)
        Me.Controls.Add(Me.LabelRate)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.dataGridRouting)
        Me.Controls.Add(Me.tbTotal)
        Me.Controls.Add(Me.LabelTotal)
        Me.Controls.Add(Me.tbWithKit)
        Me.Controls.Add(Me.tbWithCoefficient)
        Me.Controls.Add(Me.tbAvgValue)
        Me.Controls.Add(Me.LabelWithKit)
        Me.Controls.Add(Me.LabelWithCoefficient)
        Me.Controls.Add(Me.LabelAvgValue)
        Me.Controls.Add(Me.cbCoeff2)
        Me.Controls.Add(Me.cbCoeff1)
        Me.Controls.Add(Me.LabelAdditionalCoefficient)
        Me.Controls.Add(Me.cbKnittingMachine)
        Me.Controls.Add(Me.LabelKnittingMachine)
        Me.Controls.Add(Me.cbMachineCarriageAmount)
        Me.Controls.Add(Me.LabelMachineCarreagesAmount)
        Me.Controls.Add(Me.cbModelPartAmount)
        Me.Controls.Add(Me.LabelModelPartsAmount)
        Me.Controls.Add(Me.cbKnittingPart)
        Me.Controls.Add(Me.LabelClothingPart)
        Me.Controls.Add(Me.btnSeachArt)
        Me.Controls.Add(Me.lbArticul)
        Me.Controls.Add(Me.cbArticle)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "KnittingRoutingForm"
        Me.Text = "Тех. карта (Вязание)"
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClothingPartsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KnittingMachinesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CoefficientsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CoefficientsBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dataGridRouting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSeachArt As Button
    Friend WithEvents lbArticul As Label
    Friend WithEvents cbArticle As ComboBox
    Friend WithEvents LabelClothingPart As Label
    Friend WithEvents cbKnittingPart As ComboBox
    Friend WithEvents LabelModelPartsAmount As Label
    Friend WithEvents cbModelPartAmount As ComboBox
    Friend WithEvents cbMachineCarriageAmount As ComboBox
    Friend WithEvents LabelMachineCarreagesAmount As Label
    Friend WithEvents cbKnittingMachine As ComboBox
    Friend WithEvents LabelKnittingMachine As Label
    Friend WithEvents cbCoeff2 As ComboBox
    Friend WithEvents cbCoeff1 As ComboBox
    Friend WithEvents LabelAdditionalCoefficient As Label
    Friend WithEvents LabelWithCoefficient As Label
    Friend WithEvents LabelAvgValue As Label
    Friend WithEvents LabelWithKit As Label
    Friend WithEvents tbAvgValue As TextBox
    Friend WithEvents tbWithCoefficient As TextBox
    Friend WithEvents tbWithKit As TextBox
    Friend WithEvents tbTotal As TextBox
    Friend WithEvents LabelTotal As Label
    Friend WithEvents OperationDBDataSet0 As OperationDBDataSet0
    Friend WithEvents ArticulBindingSource As BindingSource
    Friend WithEvents ArticulTableAdapter As OperationDBDataSet0TableAdapters.ArticulTableAdapter
    Friend WithEvents ClothingPartsBindingSource As BindingSource
    Friend WithEvents ClothingPartsTableAdapter As OperationDBDataSet0TableAdapters.ClothingPartsTableAdapter
    Friend WithEvents KnittingMachinesBindingSource As BindingSource
    Friend WithEvents KnittingMachinesTableAdapter As OperationDBDataSet0TableAdapters.KnittingMachinesTableAdapter
    Friend WithEvents CoefficientsBindingSource As BindingSource
    Friend WithEvents CoefficientsTableAdapter As OperationDBDataSet0TableAdapters.CoefficientsTableAdapter
    Friend WithEvents CoefficientsBindingSource1 As BindingSource
    Friend WithEvents dataGridRouting As DataGridView
    Friend WithEvents btSave As Button
    Friend WithEvents btnDel As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents PartColumn As DataGridViewTextBoxColumn
    Friend WithEvents MachineColumn As DataGridViewTextBoxColumn
    Friend WithEvents ModelPartAmountColumn As DataGridViewTextBoxColumn
    Friend WithEvents MachineCarriageAmountColumn As DataGridViewTextBoxColumn
    Friend WithEvents Coeff1Column As DataGridViewTextBoxColumn
    Friend WithEvents Coeff2Column As DataGridViewTextBoxColumn
    Friend WithEvents AvgValueColumn As DataGridViewTextBoxColumn
    Friend WithEvents WithCoefficientColumn As DataGridViewTextBoxColumn
    Friend WithEvents WithKitColumn As DataGridViewTextBoxColumn
    Friend WithEvents tbRate As TextBox
    Friend WithEvents LabelRate As Label
End Class
