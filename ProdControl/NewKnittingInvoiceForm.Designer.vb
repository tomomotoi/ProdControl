<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewKnittingInvoiceForm
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewKnittingInvoiceForm))
        Me.labelReceiptNumber = New System.Windows.Forms.Label()
        Me.tbReceiptNumber = New System.Windows.Forms.TextBox()
        Me.labelWorkDay = New System.Windows.Forms.Label()
        Me.tbWorkday = New System.Windows.Forms.TextBox()
        Me.labelCount = New System.Windows.Forms.Label()
        Me.tbCount = New System.Windows.Forms.TextBox()
        Me.cbOperation = New System.Windows.Forms.ComboBox()
        Me.labelOperation = New System.Windows.Forms.Label()
        Me.dtPickerOpDate = New System.Windows.Forms.DateTimePicker()
        Me.labelDate = New System.Windows.Forms.Label()
        Me.labelArticul = New System.Windows.Forms.Label()
        Me.cbArticul = New System.Windows.Forms.ComboBox()
        Me.labelEmployee = New System.Windows.Forms.Label()
        Me.cbEmployee = New System.Windows.Forms.ComboBox()
        Me.labelExecutionTime = New System.Windows.Forms.Label()
        Me.tbExecutionTime = New System.Windows.Forms.TextBox()
        Me.btnInsert = New System.Windows.Forms.Button()
        Me.dgOperationLog = New System.Windows.Forms.DataGridView()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.cbKnittingMachines = New System.Windows.Forms.ComboBox()
        Me.labelKnittingMachines = New System.Windows.Forms.Label()
        Me.OperationDBDataSet0 = New ProdControl.OperationDBDataSet0()
        Me.ArticulBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ArticulTableAdapter = New ProdControl.OperationDBDataSet0TableAdapters.ArticulTableAdapter()
        Me.EmployeeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeeTableAdapter = New ProdControl.OperationDBDataSet0TableAdapters.EmployeeTableAdapter()
        Me.ClothingPartsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ClothingPartsTableAdapter = New ProdControl.OperationDBDataSet0TableAdapters.ClothingPartsTableAdapter()
        Me.KnittingMachinesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.KnittingMachinesTableAdapter = New ProdControl.OperationDBDataSet0TableAdapters.KnittingMachinesTableAdapter()
        Me.labelMachinesInZone = New System.Windows.Forms.Label()
        Me.tbMachinesInZone = New System.Windows.Forms.TextBox()
        Me.EmployeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LogNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LogDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticulName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PartName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PartAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExecutionTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MachineNumberInZone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.workDay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgOperationLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClothingPartsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KnittingMachinesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labelReceiptNumber
        '
        Me.labelReceiptNumber.AutoSize = True
        Me.labelReceiptNumber.Location = New System.Drawing.Point(9, 15)
        Me.labelReceiptNumber.Name = "labelReceiptNumber"
        Me.labelReceiptNumber.Size = New System.Drawing.Size(100, 13)
        Me.labelReceiptNumber.TabIndex = 20
        Me.labelReceiptNumber.Text = "Номер квитанции:"
        '
        'tbReceiptNumber
        '
        Me.tbReceiptNumber.Location = New System.Drawing.Point(113, 12)
        Me.tbReceiptNumber.Name = "tbReceiptNumber"
        Me.tbReceiptNumber.Size = New System.Drawing.Size(188, 20)
        Me.tbReceiptNumber.TabIndex = 19
        '
        'labelWorkDay
        '
        Me.labelWorkDay.AutoSize = True
        Me.labelWorkDay.Location = New System.Drawing.Point(575, 15)
        Me.labelWorkDay.Name = "labelWorkDay"
        Me.labelWorkDay.Size = New System.Drawing.Size(111, 13)
        Me.labelWorkDay.TabIndex = 32
        Me.labelWorkDay.Text = "Рабочий день (мин.):"
        '
        'tbWorkday
        '
        Me.tbWorkday.Location = New System.Drawing.Point(692, 12)
        Me.tbWorkday.Name = "tbWorkday"
        Me.tbWorkday.Size = New System.Drawing.Size(48, 20)
        Me.tbWorkday.TabIndex = 29
        '
        'labelCount
        '
        Me.labelCount.AutoSize = True
        Me.labelCount.Location = New System.Drawing.Point(341, 43)
        Me.labelCount.Name = "labelCount"
        Me.labelCount.Size = New System.Drawing.Size(69, 13)
        Me.labelCount.TabIndex = 31
        Me.labelCount.Text = "Количество:"
        '
        'tbCount
        '
        Me.tbCount.Location = New System.Drawing.Point(411, 39)
        Me.tbCount.Name = "tbCount"
        Me.tbCount.Size = New System.Drawing.Size(147, 20)
        Me.tbCount.TabIndex = 24
        '
        'cbOperation
        '
        Me.cbOperation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbOperation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbOperation.DataSource = Me.ClothingPartsBindingSource
        Me.cbOperation.DisplayMember = "part_name"
        Me.cbOperation.FormattingEnabled = True
        Me.cbOperation.Location = New System.Drawing.Point(411, 64)
        Me.cbOperation.Name = "cbOperation"
        Me.cbOperation.Size = New System.Drawing.Size(147, 21)
        Me.cbOperation.TabIndex = 27
        Me.cbOperation.ValueMember = "id"
        '
        'labelOperation
        '
        Me.labelOperation.AutoSize = True
        Me.labelOperation.Location = New System.Drawing.Point(364, 67)
        Me.labelOperation.Name = "labelOperation"
        Me.labelOperation.Size = New System.Drawing.Size(48, 13)
        Me.labelOperation.TabIndex = 30
        Me.labelOperation.Text = "Деталь:"
        '
        'dtPickerOpDate
        '
        Me.dtPickerOpDate.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.dtPickerOpDate.Location = New System.Drawing.Point(411, 12)
        Me.dtPickerOpDate.Name = "dtPickerOpDate"
        Me.dtPickerOpDate.Size = New System.Drawing.Size(149, 20)
        Me.dtPickerOpDate.TabIndex = 21
        Me.dtPickerOpDate.Value = New Date(2026, 8, 21, 0, 0, 0, 0)
        '
        'labelDate
        '
        Me.labelDate.AutoSize = True
        Me.labelDate.Location = New System.Drawing.Point(372, 15)
        Me.labelDate.Name = "labelDate"
        Me.labelDate.Size = New System.Drawing.Size(36, 13)
        Me.labelDate.TabIndex = 28
        Me.labelDate.Text = "Дата:"
        '
        'labelArticul
        '
        Me.labelArticul.AutoSize = True
        Me.labelArticul.Location = New System.Drawing.Point(59, 40)
        Me.labelArticul.Name = "labelArticul"
        Me.labelArticul.Size = New System.Drawing.Size(51, 13)
        Me.labelArticul.TabIndex = 25
        Me.labelArticul.Text = "Артикул:"
        '
        'cbArticul
        '
        Me.cbArticul.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArticul.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArticul.DataSource = Me.ArticulBindingSource
        Me.cbArticul.DisplayMember = "ArticulName"
        Me.cbArticul.FormattingEnabled = True
        Me.cbArticul.Location = New System.Drawing.Point(113, 37)
        Me.cbArticul.Name = "cbArticul"
        Me.cbArticul.Size = New System.Drawing.Size(188, 21)
        Me.cbArticul.TabIndex = 23
        Me.cbArticul.ValueMember = "ArticulID"
        '
        'labelEmployee
        '
        Me.labelEmployee.AutoSize = True
        Me.labelEmployee.Location = New System.Drawing.Point(47, 66)
        Me.labelEmployee.Name = "labelEmployee"
        Me.labelEmployee.Size = New System.Drawing.Size(63, 13)
        Me.labelEmployee.TabIndex = 22
        Me.labelEmployee.Text = "Сотрудник:"
        '
        'cbEmployee
        '
        Me.cbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbEmployee.DataSource = Me.EmployeeBindingSource
        Me.cbEmployee.DisplayMember = "EmployeeName"
        Me.cbEmployee.FormattingEnabled = True
        Me.cbEmployee.Location = New System.Drawing.Point(113, 63)
        Me.cbEmployee.Name = "cbEmployee"
        Me.cbEmployee.Size = New System.Drawing.Size(188, 21)
        Me.cbEmployee.TabIndex = 26
        Me.cbEmployee.ValueMember = "EmployeeID"
        '
        'labelExecutionTime
        '
        Me.labelExecutionTime.AutoSize = True
        Me.labelExecutionTime.Location = New System.Drawing.Point(303, 121)
        Me.labelExecutionTime.Name = "labelExecutionTime"
        Me.labelExecutionTime.Size = New System.Drawing.Size(108, 13)
        Me.labelExecutionTime.TabIndex = 34
        Me.labelExecutionTime.Text = "Время выполнения:"
        '
        'tbExecutionTime
        '
        Me.tbExecutionTime.Enabled = False
        Me.tbExecutionTime.Location = New System.Drawing.Point(411, 118)
        Me.tbExecutionTime.Name = "tbExecutionTime"
        Me.tbExecutionTime.Size = New System.Drawing.Size(147, 20)
        Me.tbExecutionTime.TabIndex = 33
        '
        'btnInsert
        '
        Me.btnInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInsert.Location = New System.Drawing.Point(713, 140)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(75, 23)
        Me.btnInsert.TabIndex = 35
        Me.btnInsert.Text = "Вставить"
        Me.btnInsert.UseVisualStyleBackColor = True
        '
        'dgOperationLog
        '
        Me.dgOperationLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgOperationLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOperationLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EmployeeName, Me.LogNumber, Me.LogDate, Me.ArticulName, Me.PartName, Me.PartAmount, Me.ExecutionTime, Me.MachineNumberInZone, Me.workDay})
        Me.dgOperationLog.Location = New System.Drawing.Point(12, 169)
        Me.dgOperationLog.Name = "dgOperationLog"
        Me.dgOperationLog.RowHeadersWidth = 82
        Me.dgOperationLog.Size = New System.Drawing.Size(776, 309)
        Me.dgOperationLog.TabIndex = 36
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(632, 484)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 38
        Me.btnDelete.Text = "Удалить"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(713, 484)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 37
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cbKnittingMachines
        '
        Me.cbKnittingMachines.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbKnittingMachines.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbKnittingMachines.DataSource = Me.KnittingMachinesBindingSource
        Me.cbKnittingMachines.DisplayMember = "machine_displayname"
        Me.cbKnittingMachines.FormattingEnabled = True
        Me.cbKnittingMachines.Location = New System.Drawing.Point(412, 92)
        Me.cbKnittingMachines.Name = "cbKnittingMachines"
        Me.cbKnittingMachines.Size = New System.Drawing.Size(147, 21)
        Me.cbKnittingMachines.TabIndex = 39
        Me.cbKnittingMachines.ValueMember = "id"
        '
        'labelKnittingMachines
        '
        Me.labelKnittingMachines.AutoSize = True
        Me.labelKnittingMachines.Location = New System.Drawing.Point(328, 95)
        Me.labelKnittingMachines.Name = "labelKnittingMachines"
        Me.labelKnittingMachines.Size = New System.Drawing.Size(83, 13)
        Me.labelKnittingMachines.TabIndex = 40
        Me.labelKnittingMachines.Text = "Оборудование:"
        '
        'OperationDBDataSet0
        '
        Me.OperationDBDataSet0.DataSetName = "OperationDBDataSet0"
        Me.OperationDBDataSet0.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ArticulBindingSource
        '
        Me.ArticulBindingSource.DataMember = "Articul"
        Me.ArticulBindingSource.DataSource = Me.OperationDBDataSet0
        '
        'ArticulTableAdapter
        '
        Me.ArticulTableAdapter.ClearBeforeFill = True
        '
        'EmployeeBindingSource
        '
        Me.EmployeeBindingSource.DataMember = "Employee"
        Me.EmployeeBindingSource.DataSource = Me.OperationDBDataSet0
        '
        'EmployeeTableAdapter
        '
        Me.EmployeeTableAdapter.ClearBeforeFill = True
        '
        'ClothingPartsBindingSource
        '
        Me.ClothingPartsBindingSource.DataMember = "ClothingParts"
        Me.ClothingPartsBindingSource.DataSource = Me.OperationDBDataSet0
        '
        'ClothingPartsTableAdapter
        '
        Me.ClothingPartsTableAdapter.ClearBeforeFill = True
        '
        'KnittingMachinesBindingSource
        '
        Me.KnittingMachinesBindingSource.DataMember = "KnittingMachines"
        Me.KnittingMachinesBindingSource.DataSource = Me.OperationDBDataSet0
        '
        'KnittingMachinesTableAdapter
        '
        Me.KnittingMachinesTableAdapter.ClearBeforeFill = True
        '
        'labelMachinesInZone
        '
        Me.labelMachinesInZone.AutoSize = True
        Me.labelMachinesInZone.Location = New System.Drawing.Point(300, 147)
        Me.labelMachinesInZone.Name = "labelMachinesInZone"
        Me.labelMachinesInZone.Size = New System.Drawing.Size(111, 13)
        Me.labelMachinesInZone.TabIndex = 42
        Me.labelMachinesInZone.Text = "Машин в зоне обсл.:"
        '
        'tbMachinesInZone
        '
        Me.tbMachinesInZone.Enabled = False
        Me.tbMachinesInZone.Location = New System.Drawing.Point(412, 144)
        Me.tbMachinesInZone.Name = "tbMachinesInZone"
        Me.tbMachinesInZone.Size = New System.Drawing.Size(147, 20)
        Me.tbMachinesInZone.TabIndex = 41
        '
        'EmployeeName
        '
        Me.EmployeeName.HeaderText = "ФИО"
        Me.EmployeeName.MinimumWidth = 10
        Me.EmployeeName.Name = "EmployeeName"
        Me.EmployeeName.ReadOnly = True
        Me.EmployeeName.Width = 200
        '
        'LogNumber
        '
        Me.LogNumber.HeaderText = "Номер квитанции"
        Me.LogNumber.MinimumWidth = 10
        Me.LogNumber.Name = "LogNumber"
        Me.LogNumber.ReadOnly = True
        Me.LogNumber.Width = 200
        '
        'LogDate
        '
        Me.LogDate.HeaderText = "Дата выполнения"
        Me.LogDate.MinimumWidth = 10
        Me.LogDate.Name = "LogDate"
        Me.LogDate.ReadOnly = True
        Me.LogDate.Width = 200
        '
        'ArticulName
        '
        Me.ArticulName.HeaderText = "Артикул"
        Me.ArticulName.MinimumWidth = 10
        Me.ArticulName.Name = "ArticulName"
        Me.ArticulName.ReadOnly = True
        Me.ArticulName.Width = 200
        '
        'PartName
        '
        Me.PartName.HeaderText = "Деталь"
        Me.PartName.MinimumWidth = 10
        Me.PartName.Name = "PartName"
        Me.PartName.ReadOnly = True
        Me.PartName.Width = 200
        '
        'PartAmount
        '
        Me.PartAmount.HeaderText = "Количество"
        Me.PartAmount.MinimumWidth = 10
        Me.PartAmount.Name = "PartAmount"
        Me.PartAmount.ReadOnly = True
        Me.PartAmount.Width = 200
        '
        'ExecutionTime
        '
        Me.ExecutionTime.HeaderText = "Время выполнения"
        Me.ExecutionTime.MinimumWidth = 10
        Me.ExecutionTime.Name = "ExecutionTime"
        Me.ExecutionTime.ReadOnly = True
        Me.ExecutionTime.Width = 200
        '
        'MachineNumberInZone
        '
        Me.MachineNumberInZone.HeaderText = "Машин в зоне обсл."
        Me.MachineNumberInZone.Name = "MachineNumberInZone"
        Me.MachineNumberInZone.ReadOnly = True
        '
        'workDay
        '
        Me.workDay.HeaderText = "Рабочий день"
        Me.workDay.MinimumWidth = 10
        Me.workDay.Name = "workDay"
        Me.workDay.Width = 200
        '
        'NewKnittingInvoiceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 511)
        Me.Controls.Add(Me.labelMachinesInZone)
        Me.Controls.Add(Me.tbMachinesInZone)
        Me.Controls.Add(Me.cbKnittingMachines)
        Me.Controls.Add(Me.labelKnittingMachines)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.dgOperationLog)
        Me.Controls.Add(Me.btnInsert)
        Me.Controls.Add(Me.labelExecutionTime)
        Me.Controls.Add(Me.tbExecutionTime)
        Me.Controls.Add(Me.labelWorkDay)
        Me.Controls.Add(Me.tbWorkday)
        Me.Controls.Add(Me.labelCount)
        Me.Controls.Add(Me.tbCount)
        Me.Controls.Add(Me.cbOperation)
        Me.Controls.Add(Me.labelOperation)
        Me.Controls.Add(Me.dtPickerOpDate)
        Me.Controls.Add(Me.labelDate)
        Me.Controls.Add(Me.labelArticul)
        Me.Controls.Add(Me.cbArticul)
        Me.Controls.Add(Me.labelEmployee)
        Me.Controls.Add(Me.cbEmployee)
        Me.Controls.Add(Me.labelReceiptNumber)
        Me.Controls.Add(Me.tbReceiptNumber)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewKnittingInvoiceForm"
        Me.Text = "Новая накладная (Вязальный цех)"
        CType(Me.dgOperationLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClothingPartsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KnittingMachinesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents labelReceiptNumber As Label
    Friend WithEvents tbReceiptNumber As TextBox
    Friend WithEvents labelWorkDay As Label
    Friend WithEvents tbWorkday As TextBox
    Friend WithEvents labelCount As Label
    Friend WithEvents tbCount As TextBox
    Friend WithEvents cbOperation As ComboBox
    Friend WithEvents labelOperation As Label
    Friend WithEvents dtPickerOpDate As DateTimePicker
    Friend WithEvents labelDate As Label
    Friend WithEvents labelArticul As Label
    Friend WithEvents cbArticul As ComboBox
    Friend WithEvents labelEmployee As Label
    Friend WithEvents cbEmployee As ComboBox
    Friend WithEvents labelExecutionTime As Label
    Friend WithEvents tbExecutionTime As TextBox
    Friend WithEvents btnInsert As Button
    Friend WithEvents dgOperationLog As DataGridView
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents cbKnittingMachines As ComboBox
    Friend WithEvents labelKnittingMachines As Label
    Friend WithEvents OperationDBDataSet0 As OperationDBDataSet0
    Friend WithEvents ArticulBindingSource As BindingSource
    Friend WithEvents ArticulTableAdapter As OperationDBDataSet0TableAdapters.ArticulTableAdapter
    Friend WithEvents EmployeeBindingSource As BindingSource
    Friend WithEvents EmployeeTableAdapter As OperationDBDataSet0TableAdapters.EmployeeTableAdapter
    Friend WithEvents ClothingPartsBindingSource As BindingSource
    Friend WithEvents ClothingPartsTableAdapter As OperationDBDataSet0TableAdapters.ClothingPartsTableAdapter
    Friend WithEvents KnittingMachinesBindingSource As BindingSource
    Friend WithEvents KnittingMachinesTableAdapter As OperationDBDataSet0TableAdapters.KnittingMachinesTableAdapter
    Friend WithEvents labelMachinesInZone As Label
    Friend WithEvents tbMachinesInZone As TextBox
    Friend WithEvents EmployeeName As DataGridViewTextBoxColumn
    Friend WithEvents LogNumber As DataGridViewTextBoxColumn
    Friend WithEvents LogDate As DataGridViewTextBoxColumn
    Friend WithEvents ArticulName As DataGridViewTextBoxColumn
    Friend WithEvents PartName As DataGridViewTextBoxColumn
    Friend WithEvents PartAmount As DataGridViewTextBoxColumn
    Friend WithEvents ExecutionTime As DataGridViewTextBoxColumn
    Friend WithEvents MachineNumberInZone As DataGridViewTextBoxColumn
    Friend WithEvents workDay As DataGridViewTextBoxColumn
End Class
