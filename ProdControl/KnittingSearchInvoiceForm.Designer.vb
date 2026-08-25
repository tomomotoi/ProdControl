<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KnittingSearchInvoiceForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KnittingSearchInvoiceForm))
        Me.chckbxEditingMode = New System.Windows.Forms.CheckBox()
        Me.labelDateTo = New System.Windows.Forms.Label()
        Me.datePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.labelDateFrom = New System.Windows.Forms.Label()
        Me.datePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.labelFilter = New System.Windows.Forms.Label()
        Me.cbFilter = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.txDocNum = New System.Windows.Forms.TextBox()
        Me.labelDocNum = New System.Windows.Forms.Label()
        Me.chckbxByPeriod = New System.Windows.Forms.CheckBox()
        Me.btnSearchAll = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.labelArticul = New System.Windows.Forms.Label()
        Me.cbArticul = New System.Windows.Forms.ComboBox()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.LogId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.OperationName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ArticulName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DateExecution = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeExecution = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocumentNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.workDay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isChanged = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chckbxEditingMode
        '
        Me.chckbxEditingMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chckbxEditingMode.AutoSize = True
        Me.chckbxEditingMode.Location = New System.Drawing.Point(11, 446)
        Me.chckbxEditingMode.Margin = New System.Windows.Forms.Padding(2)
        Me.chckbxEditingMode.Name = "chckbxEditingMode"
        Me.chckbxEditingMode.Size = New System.Drawing.Size(147, 17)
        Me.chckbxEditingMode.TabIndex = 37
        Me.chckbxEditingMode.Text = "Режим редактирования"
        Me.chckbxEditingMode.UseVisualStyleBackColor = True
        '
        'labelDateTo
        '
        Me.labelDateTo.AutoSize = True
        Me.labelDateTo.Location = New System.Drawing.Point(455, 63)
        Me.labelDateTo.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelDateTo.Name = "labelDateTo"
        Me.labelDateTo.Size = New System.Drawing.Size(51, 13)
        Me.labelDateTo.TabIndex = 36
        Me.labelDateTo.Text = "Дата по:"
        Me.labelDateTo.Visible = False
        '
        'datePickerTo
        '
        Me.datePickerTo.Enabled = False
        Me.datePickerTo.Location = New System.Drawing.Point(457, 84)
        Me.datePickerTo.Margin = New System.Windows.Forms.Padding(2)
        Me.datePickerTo.Name = "datePickerTo"
        Me.datePickerTo.Size = New System.Drawing.Size(183, 20)
        Me.datePickerTo.TabIndex = 35
        Me.datePickerTo.Visible = False
        '
        'labelDateFrom
        '
        Me.labelDateFrom.AutoSize = True
        Me.labelDateFrom.Location = New System.Drawing.Point(455, 18)
        Me.labelDateFrom.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelDateFrom.Name = "labelDateFrom"
        Me.labelDateFrom.Size = New System.Drawing.Size(45, 13)
        Me.labelDateFrom.TabIndex = 34
        Me.labelDateFrom.Text = "Дата с:"
        Me.labelDateFrom.Visible = False
        '
        'datePickerFrom
        '
        Me.datePickerFrom.Enabled = False
        Me.datePickerFrom.Location = New System.Drawing.Point(457, 39)
        Me.datePickerFrom.Margin = New System.Windows.Forms.Padding(2)
        Me.datePickerFrom.Name = "datePickerFrom"
        Me.datePickerFrom.Size = New System.Drawing.Size(183, 20)
        Me.datePickerFrom.TabIndex = 33
        Me.datePickerFrom.Visible = False
        '
        'labelFilter
        '
        Me.labelFilter.AutoSize = True
        Me.labelFilter.Location = New System.Drawing.Point(203, 18)
        Me.labelFilter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelFilter.Name = "labelFilter"
        Me.labelFilter.Size = New System.Drawing.Size(50, 13)
        Me.labelFilter.TabIndex = 32
        Me.labelFilter.Text = "Фильтр:"
        '
        'cbFilter
        '
        Me.cbFilter.FormattingEnabled = True
        Me.cbFilter.Items.AddRange(New Object() {"По сотруднику", "По артикулу", "Сотрудник + артикул", "По номеру квитанции"})
        Me.cbFilter.Location = New System.Drawing.Point(205, 38)
        Me.cbFilter.Margin = New System.Windows.Forms.Padding(2)
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.Size = New System.Drawing.Size(161, 21)
        Me.cbFilter.TabIndex = 31
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(553, 448)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(86, 25)
        Me.btnSave.TabIndex = 30
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Enabled = False
        Me.btnDelete.Location = New System.Drawing.Point(463, 448)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(86, 25)
        Me.btnDelete.TabIndex = 29
        Me.btnDelete.Text = "Удалить"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'txDocNum
        '
        Me.txDocNum.Enabled = False
        Me.txDocNum.Location = New System.Drawing.Point(11, 38)
        Me.txDocNum.Margin = New System.Windows.Forms.Padding(2)
        Me.txDocNum.Name = "txDocNum"
        Me.txDocNum.Size = New System.Drawing.Size(180, 20)
        Me.txDocNum.TabIndex = 28
        Me.txDocNum.Visible = False
        '
        'labelDocNum
        '
        Me.labelDocNum.AutoSize = True
        Me.labelDocNum.Location = New System.Drawing.Point(11, 18)
        Me.labelDocNum.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelDocNum.Name = "labelDocNum"
        Me.labelDocNum.Size = New System.Drawing.Size(100, 13)
        Me.labelDocNum.TabIndex = 27
        Me.labelDocNum.Text = "Номер квитанции:"
        Me.labelDocNum.Visible = False
        '
        'chckbxByPeriod
        '
        Me.chckbxByPeriod.AutoSize = True
        Me.chckbxByPeriod.Location = New System.Drawing.Point(379, 39)
        Me.chckbxByPeriod.Margin = New System.Windows.Forms.Padding(2)
        Me.chckbxByPeriod.Name = "chckbxByPeriod"
        Me.chckbxByPeriod.Size = New System.Drawing.Size(78, 17)
        Me.chckbxByPeriod.TabIndex = 26
        Me.chckbxByPeriod.Text = "За период"
        Me.chckbxByPeriod.UseVisualStyleBackColor = True
        '
        'btnSearchAll
        '
        Me.btnSearchAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchAll.Location = New System.Drawing.Point(465, 114)
        Me.btnSearchAll.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSearchAll.Name = "btnSearchAll"
        Me.btnSearchAll.Size = New System.Drawing.Size(85, 22)
        Me.btnSearchAll.TabIndex = 25
        Me.btnSearchAll.Text = "Найти все"
        Me.btnSearchAll.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(553, 113)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(85, 23)
        Me.btnSearch.TabIndex = 24
        Me.btnSearch.Text = "Найти"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'labelArticul
        '
        Me.labelArticul.AutoSize = True
        Me.labelArticul.Location = New System.Drawing.Point(11, 64)
        Me.labelArticul.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelArticul.Name = "labelArticul"
        Me.labelArticul.Size = New System.Drawing.Size(51, 13)
        Me.labelArticul.TabIndex = 23
        Me.labelArticul.Text = "Артикул:"
        '
        'cbArticul
        '
        Me.cbArticul.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArticul.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArticul.DisplayMember = "ArticulName"
        Me.cbArticul.FormattingEnabled = True
        Me.cbArticul.Location = New System.Drawing.Point(11, 83)
        Me.cbArticul.Margin = New System.Windows.Forms.Padding(2)
        Me.cbArticul.Name = "cbArticul"
        Me.cbArticul.Size = New System.Drawing.Size(180, 21)
        Me.cbArticul.TabIndex = 22
        Me.cbArticul.ValueMember = "ArticulID"
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
        Me.dgView.Location = New System.Drawing.Point(11, 140)
        Me.dgView.Margin = New System.Windows.Forms.Padding(2)
        Me.dgView.Name = "dgView"
        Me.dgView.ReadOnly = True
        Me.dgView.RowHeadersWidth = 82
        Me.dgView.RowTemplate.Height = 33
        Me.dgView.Size = New System.Drawing.Size(628, 304)
        Me.dgView.TabIndex = 21
        '
        'LogId
        '
        Me.LogId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.LogId.HeaderText = "LogId"
        Me.LogId.MinimumWidth = 10
        Me.LogId.Name = "LogId"
        Me.LogId.ReadOnly = True
        Me.LogId.Width = 59
        '
        'EmployeeName
        '
        Me.EmployeeName.HeaderText = "Сотрудник"
        Me.EmployeeName.MinimumWidth = 10
        Me.EmployeeName.Name = "EmployeeName"
        Me.EmployeeName.ReadOnly = True
        Me.EmployeeName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EmployeeName.Width = 170
        '
        'OperationName
        '
        Me.OperationName.HeaderText = "Операция"
        Me.OperationName.MinimumWidth = 10
        Me.OperationName.Name = "OperationName"
        Me.OperationName.ReadOnly = True
        Me.OperationName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.OperationName.Width = 150
        '
        'ArticulName
        '
        Me.ArticulName.HeaderText = "Артикул"
        Me.ArticulName.MinimumWidth = 10
        Me.ArticulName.Name = "ArticulName"
        Me.ArticulName.ReadOnly = True
        Me.ArticulName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArticulName.Width = 120
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
        'KnittingSearchInvoiceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(647, 485)
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
        Me.Controls.Add(Me.cbArticul)
        Me.Controls.Add(Me.dgView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "KnittingSearchInvoiceForm"
        Me.Text = "Найти накладную (вяз. цех.)"
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chckbxEditingMode As CheckBox
    Friend WithEvents labelDateTo As Label
    Friend WithEvents datePickerTo As DateTimePicker
    Friend WithEvents labelDateFrom As Label
    Friend WithEvents datePickerFrom As DateTimePicker
    Friend WithEvents labelFilter As Label
    Friend WithEvents cbFilter As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents txDocNum As TextBox
    Friend WithEvents labelDocNum As Label
    Friend WithEvents chckbxByPeriod As CheckBox
    Friend WithEvents btnSearchAll As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents labelArticul As Label
    Friend WithEvents cbArticul As ComboBox
    Friend WithEvents dgView As DataGridView
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
End Class
