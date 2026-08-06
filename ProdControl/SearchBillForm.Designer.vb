<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SearchBillForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.LogId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.EmployeeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet1 = New ProdControl.OperationDBDataSet1()
        Me.OperationName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.OperationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ArticulName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ArticulBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DateExecution = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeExecution = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocumentNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.workDay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isChanged = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cbEmployee = New System.Windows.Forms.ComboBox()
        Me.cbArticul = New System.Windows.Forms.ComboBox()
        Me.labelEmployee = New System.Windows.Forms.Label()
        Me.labelArticul = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnSearchAll = New System.Windows.Forms.Button()
        Me.chckbxByPeriod = New System.Windows.Forms.CheckBox()
        Me.EmployeeTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter()
        Me.ArticulTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.ArticulTableAdapter()
        Me.labelDocNum = New System.Windows.Forms.Label()
        Me.txDocNum = New System.Windows.Forms.TextBox()
        Me.OperationTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.OperationTableAdapter()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.OperationLog1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationLog1TableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.OperationLog1TableAdapter()
        Me.cbFilter = New System.Windows.Forms.ComboBox()
        Me.labelFilter = New System.Windows.Forms.Label()
        Me.datePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.labelDateFrom = New System.Windows.Forms.Label()
        Me.labelDateTo = New System.Windows.Forms.Label()
        Me.datePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.chckbxEditingMode = New System.Windows.Forms.CheckBox()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationLog1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgView
        '
        Me.dgView.AllowUserToAddRows = False
        Me.dgView.AllowUserToDeleteRows = False
        Me.dgView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LogId, Me.EmployeeName, Me.OperationName, Me.ArticulName, Me.DateExecution, Me.TimeExecution, Me.Count, Me.DocumentNumber, Me.workDay, Me.isChanged})
        Me.dgView.Location = New System.Drawing.Point(12, 260)
        Me.dgView.Margin = New System.Windows.Forms.Padding(4)
        Me.dgView.Name = "dgView"
        Me.dgView.ReadOnly = True
        Me.dgView.RowHeadersWidth = 82
        Me.dgView.RowTemplate.Height = 33
        Me.dgView.Size = New System.Drawing.Size(1255, 584)
        Me.dgView.TabIndex = 0
        '
        'LogId
        '
        Me.LogId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.LogId.HeaderText = "LogId"
        Me.LogId.MinimumWidth = 10
        Me.LogId.Name = "LogId"
        Me.LogId.ReadOnly = True
        Me.LogId.Width = 110
        '
        'EmployeeName
        '
        Me.EmployeeName.DataSource = Me.EmployeeBindingSource
        Me.EmployeeName.DisplayMember = "EmployeeName"
        Me.EmployeeName.HeaderText = "Сотрудник"
        Me.EmployeeName.MinimumWidth = 10
        Me.EmployeeName.Name = "EmployeeName"
        Me.EmployeeName.ReadOnly = True
        Me.EmployeeName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EmployeeName.ValueMember = "EmployeeID"
        Me.EmployeeName.Width = 170
        '
        'EmployeeBindingSource
        '
        Me.EmployeeBindingSource.DataMember = "Employee"
        Me.EmployeeBindingSource.DataSource = Me.OperationDBDataSet1
        '
        'OperationDBDataSet1
        '
        Me.OperationDBDataSet1.DataSetName = "OperationDBDataSet1"
        Me.OperationDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'OperationName
        '
        Me.OperationName.DataSource = Me.OperationBindingSource
        Me.OperationName.DisplayMember = "OperationName"
        Me.OperationName.HeaderText = "Операция"
        Me.OperationName.MinimumWidth = 10
        Me.OperationName.Name = "OperationName"
        Me.OperationName.ReadOnly = True
        Me.OperationName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.OperationName.ValueMember = "OperationID"
        Me.OperationName.Width = 150
        '
        'OperationBindingSource
        '
        Me.OperationBindingSource.DataMember = "Operation"
        Me.OperationBindingSource.DataSource = Me.OperationDBDataSet1
        '
        'ArticulName
        '
        Me.ArticulName.DataSource = Me.ArticulBindingSource
        Me.ArticulName.DisplayMember = "ArticulName"
        Me.ArticulName.HeaderText = "Артикул"
        Me.ArticulName.MinimumWidth = 10
        Me.ArticulName.Name = "ArticulName"
        Me.ArticulName.ReadOnly = True
        Me.ArticulName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArticulName.ValueMember = "ArticulID"
        Me.ArticulName.Width = 120
        '
        'ArticulBindingSource
        '
        Me.ArticulBindingSource.DataMember = "Articul"
        Me.ArticulBindingSource.DataSource = Me.OperationDBDataSet1
        '
        'DateExecution
        '
        Me.DateExecution.HeaderText = "Дата выполнения"
        Me.DateExecution.MinimumWidth = 10
        Me.DateExecution.Name = "DateExecution"
        Me.DateExecution.ReadOnly = True
        Me.DateExecution.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DateExecution.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DateExecution.Width = 82
        '
        'TimeExecution
        '
        Me.TimeExecution.HeaderText = "Время выполнения"
        Me.TimeExecution.MinimumWidth = 10
        Me.TimeExecution.Name = "TimeExecution"
        Me.TimeExecution.ReadOnly = True
        Me.TimeExecution.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TimeExecution.Width = 70
        '
        'Count
        '
        Me.Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Count.HeaderText = "Количество"
        Me.Count.MinimumWidth = 10
        Me.Count.Name = "Count"
        Me.Count.ReadOnly = True
        Me.Count.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DocumentNumber
        '
        Me.DocumentNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DocumentNumber.HeaderText = "Номер квитанции"
        Me.DocumentNumber.MinimumWidth = 10
        Me.DocumentNumber.Name = "DocumentNumber"
        Me.DocumentNumber.ReadOnly = True
        Me.DocumentNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'workDay
        '
        Me.workDay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.workDay.HeaderText = "Рабочий день"
        Me.workDay.MinimumWidth = 10
        Me.workDay.Name = "workDay"
        Me.workDay.ReadOnly = True
        Me.workDay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'isChanged
        '
        Me.isChanged.HeaderText = "isChanged"
        Me.isChanged.MinimumWidth = 10
        Me.isChanged.Name = "isChanged"
        Me.isChanged.ReadOnly = True
        Me.isChanged.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.isChanged.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.isChanged.Visible = False
        Me.isChanged.Width = 200
        '
        'cbEmployee
        '
        Me.cbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbEmployee.DataSource = Me.EmployeeBindingSource
        Me.cbEmployee.DisplayMember = "EmployeeName"
        Me.cbEmployee.FormattingEnabled = True
        Me.cbEmployee.Location = New System.Drawing.Point(12, 63)
        Me.cbEmployee.Margin = New System.Windows.Forms.Padding(4)
        Me.cbEmployee.Name = "cbEmployee"
        Me.cbEmployee.Size = New System.Drawing.Size(356, 33)
        Me.cbEmployee.TabIndex = 1
        Me.cbEmployee.ValueMember = "EmployeeID"
        '
        'cbArticul
        '
        Me.cbArticul.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArticul.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArticul.DataSource = Me.ArticulBindingSource
        Me.cbArticul.DisplayMember = "ArticulName"
        Me.cbArticul.FormattingEnabled = True
        Me.cbArticul.Location = New System.Drawing.Point(12, 150)
        Me.cbArticul.Margin = New System.Windows.Forms.Padding(4)
        Me.cbArticul.Name = "cbArticul"
        Me.cbArticul.Size = New System.Drawing.Size(356, 33)
        Me.cbArticul.TabIndex = 2
        Me.cbArticul.ValueMember = "ArticulID"
        '
        'labelEmployee
        '
        Me.labelEmployee.AutoSize = True
        Me.labelEmployee.Location = New System.Drawing.Point(12, 25)
        Me.labelEmployee.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelEmployee.Name = "labelEmployee"
        Me.labelEmployee.Size = New System.Drawing.Size(125, 25)
        Me.labelEmployee.TabIndex = 3
        Me.labelEmployee.Text = "Сотрудник:"
        '
        'labelArticul
        '
        Me.labelArticul.AutoSize = True
        Me.labelArticul.Location = New System.Drawing.Point(12, 113)
        Me.labelArticul.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelArticul.Name = "labelArticul"
        Me.labelArticul.Size = New System.Drawing.Size(100, 25)
        Me.labelArticul.TabIndex = 4
        Me.labelArticul.Text = "Артикул:"
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(1097, 208)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(170, 44)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "Найти"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnSearchAll
        '
        Me.btnSearchAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchAll.Location = New System.Drawing.Point(919, 210)
        Me.btnSearchAll.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearchAll.Name = "btnSearchAll"
        Me.btnSearchAll.Size = New System.Drawing.Size(170, 42)
        Me.btnSearchAll.TabIndex = 6
        Me.btnSearchAll.Text = "Найти все"
        Me.btnSearchAll.UseVisualStyleBackColor = True
        '
        'chckbxByPeriod
        '
        Me.chckbxByPeriod.AutoSize = True
        Me.chckbxByPeriod.Location = New System.Drawing.Point(748, 65)
        Me.chckbxByPeriod.Margin = New System.Windows.Forms.Padding(4)
        Me.chckbxByPeriod.Name = "chckbxByPeriod"
        Me.chckbxByPeriod.Size = New System.Drawing.Size(147, 29)
        Me.chckbxByPeriod.TabIndex = 8
        Me.chckbxByPeriod.Text = "За период"
        Me.chckbxByPeriod.UseVisualStyleBackColor = True
        '
        'EmployeeTableAdapter
        '
        Me.EmployeeTableAdapter.ClearBeforeFill = True
        '
        'ArticulTableAdapter
        '
        Me.ArticulTableAdapter.ClearBeforeFill = True
        '
        'labelDocNum
        '
        Me.labelDocNum.AutoSize = True
        Me.labelDocNum.Location = New System.Drawing.Point(12, 25)
        Me.labelDocNum.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelDocNum.Name = "labelDocNum"
        Me.labelDocNum.Size = New System.Drawing.Size(194, 25)
        Me.labelDocNum.TabIndex = 10
        Me.labelDocNum.Text = "Номер квитанции:"
        Me.labelDocNum.Visible = False
        '
        'txDocNum
        '
        Me.txDocNum.Enabled = False
        Me.txDocNum.Location = New System.Drawing.Point(12, 63)
        Me.txDocNum.Margin = New System.Windows.Forms.Padding(4)
        Me.txDocNum.Name = "txDocNum"
        Me.txDocNum.Size = New System.Drawing.Size(356, 31)
        Me.txDocNum.TabIndex = 11
        Me.txDocNum.Visible = False
        '
        'OperationTableAdapter
        '
        Me.OperationTableAdapter.ClearBeforeFill = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Enabled = False
        Me.btnDelete.Location = New System.Drawing.Point(917, 852)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(172, 48)
        Me.btnDelete.TabIndex = 12
        Me.btnDelete.Text = "Удалить"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(1095, 852)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(172, 48)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'OperationLog1BindingSource
        '
        Me.OperationLog1BindingSource.DataMember = "OperationLog1"
        Me.OperationLog1BindingSource.DataSource = Me.OperationDBDataSet1
        '
        'OperationLog1TableAdapter
        '
        Me.OperationLog1TableAdapter.ClearBeforeFill = True
        '
        'cbFilter
        '
        Me.cbFilter.FormattingEnabled = True
        Me.cbFilter.Items.AddRange(New Object() {"По сотруднику", "По артикулу", "Сотрудник + артикул", "По номеру квитанции"})
        Me.cbFilter.Location = New System.Drawing.Point(400, 63)
        Me.cbFilter.Margin = New System.Windows.Forms.Padding(4)
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.Size = New System.Drawing.Size(318, 33)
        Me.cbFilter.TabIndex = 14
        '
        'labelFilter
        '
        Me.labelFilter.AutoSize = True
        Me.labelFilter.Location = New System.Drawing.Point(396, 25)
        Me.labelFilter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelFilter.Name = "labelFilter"
        Me.labelFilter.Size = New System.Drawing.Size(93, 25)
        Me.labelFilter.TabIndex = 15
        Me.labelFilter.Text = "Фильтр:"
        '
        'datePickerFrom
        '
        Me.datePickerFrom.Enabled = False
        Me.datePickerFrom.Location = New System.Drawing.Point(905, 65)
        Me.datePickerFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.datePickerFrom.Name = "datePickerFrom"
        Me.datePickerFrom.Size = New System.Drawing.Size(362, 31)
        Me.datePickerFrom.TabIndex = 16
        Me.datePickerFrom.Visible = False
        '
        'labelDateFrom
        '
        Me.labelDateFrom.AutoSize = True
        Me.labelDateFrom.Location = New System.Drawing.Point(899, 25)
        Me.labelDateFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelDateFrom.Name = "labelDateFrom"
        Me.labelDateFrom.Size = New System.Drawing.Size(85, 25)
        Me.labelDateFrom.TabIndex = 17
        Me.labelDateFrom.Text = "Дата с:"
        Me.labelDateFrom.Visible = False
        '
        'labelDateTo
        '
        Me.labelDateTo.AutoSize = True
        Me.labelDateTo.Location = New System.Drawing.Point(899, 112)
        Me.labelDateTo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelDateTo.Name = "labelDateTo"
        Me.labelDateTo.Size = New System.Drawing.Size(98, 25)
        Me.labelDateTo.TabIndex = 19
        Me.labelDateTo.Text = "Дата по:"
        Me.labelDateTo.Visible = False
        '
        'datePickerTo
        '
        Me.datePickerTo.Enabled = False
        Me.datePickerTo.Location = New System.Drawing.Point(905, 152)
        Me.datePickerTo.Margin = New System.Windows.Forms.Padding(4)
        Me.datePickerTo.Name = "datePickerTo"
        Me.datePickerTo.Size = New System.Drawing.Size(362, 31)
        Me.datePickerTo.TabIndex = 18
        Me.datePickerTo.Visible = False
        '
        'chckbxEditingMode
        '
        Me.chckbxEditingMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chckbxEditingMode.AutoSize = True
        Me.chckbxEditingMode.Location = New System.Drawing.Point(12, 851)
        Me.chckbxEditingMode.Name = "chckbxEditingMode"
        Me.chckbxEditingMode.Size = New System.Drawing.Size(281, 29)
        Me.chckbxEditingMode.TabIndex = 20
        Me.chckbxEditingMode.Text = "Режим редактирования"
        Me.chckbxEditingMode.UseVisualStyleBackColor = True
        '
        'SearchBillForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 909)
        Me.Controls.Add(Me.chckbxEditingMode)
        Me.Controls.Add(Me.labelDateTo)
        Me.Controls.Add(Me.datePickerTo)
        Me.Controls.Add(Me.labelDateFrom)
        Me.Controls.Add(Me.datePickerFrom)
        Me.Controls.Add(Me.labelFilter)
        Me.Controls.Add(Me.cbFilter)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.txDocNum)
        Me.Controls.Add(Me.labelDocNum)
        Me.Controls.Add(Me.chckbxByPeriod)
        Me.Controls.Add(Me.btnSearchAll)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.labelArticul)
        Me.Controls.Add(Me.labelEmployee)
        Me.Controls.Add(Me.cbArticul)
        Me.Controls.Add(Me.cbEmployee)
        Me.Controls.Add(Me.dgView)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SearchBillForm"
        Me.Text = "Найти накладную"
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationLog1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgView As DataGridView
    Friend WithEvents cbEmployee As ComboBox
    Friend WithEvents cbArticul As ComboBox
    Friend WithEvents labelEmployee As Label
    Friend WithEvents labelArticul As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnSearchAll As Button
    Friend WithEvents chckbxByPeriod As CheckBox
    Friend WithEvents OperationDBDataSet1 As OperationDBDataSet1
    Friend WithEvents EmployeeBindingSource As BindingSource
    Friend WithEvents EmployeeTableAdapter As OperationDBDataSet1TableAdapters.EmployeeTableAdapter
    Friend WithEvents ArticulBindingSource As BindingSource
    Friend WithEvents ArticulTableAdapter As OperationDBDataSet1TableAdapters.ArticulTableAdapter
    Friend WithEvents labelDocNum As Label
    Friend WithEvents txDocNum As TextBox
    Friend WithEvents OperationBindingSource As BindingSource
    Friend WithEvents OperationTableAdapter As OperationDBDataSet1TableAdapters.OperationTableAdapter
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents OperationLog1BindingSource As BindingSource
    Friend WithEvents OperationLog1TableAdapter As OperationDBDataSet1TableAdapters.OperationLog1TableAdapter
    Friend WithEvents cbFilter As ComboBox
    Friend WithEvents labelFilter As Label
    Friend WithEvents datePickerFrom As DateTimePicker
    Friend WithEvents labelDateFrom As Label
    Friend WithEvents labelDateTo As Label
    Friend WithEvents datePickerTo As DateTimePicker
    Friend WithEvents LogId As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeName As DataGridViewComboBoxColumn
    Friend WithEvents OperationName As DataGridViewComboBoxColumn
    Friend WithEvents ArticulName As DataGridViewComboBoxColumn
    Friend WithEvents DateExecution As DataGridViewTextBoxColumn
    Friend WithEvents TimeExecution As DataGridViewTextBoxColumn
    Friend WithEvents Count As DataGridViewTextBoxColumn
    Friend WithEvents DocumentNumber As DataGridViewTextBoxColumn
    Friend WithEvents workDay As DataGridViewTextBoxColumn
    Friend WithEvents isChanged As DataGridViewCheckBoxColumn
    Friend WithEvents chckbxEditingMode As CheckBox
End Class
