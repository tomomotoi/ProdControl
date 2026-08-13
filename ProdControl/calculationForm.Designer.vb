<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class calculationForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(calculationForm))
        Me.EmployeeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet1 = New ProdControl.OperationDBDataSet1()
        Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
        Me.OperationLogBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeeTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter()
        Me.OperationLog1TableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.OperationLog1TableAdapter()
        Me.LogWorkDayTableAdapter1 = New ProdControl.OperationDBDataSet1TableAdapters.LogWorkDayTableAdapter()
        Me.dbGrid = New System.Windows.Forms.DataGridView()
        Me.EmployeeIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PositionIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PositionLevelIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.salary = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.timerate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeExecution = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btExport = New System.Windows.Forms.Button()
        Me.cbDateRange = New System.Windows.Forms.CheckBox()
        Me.btnCalculate = New System.Windows.Forms.Button()
        Me.lbTill = New System.Windows.Forms.Label()
        Me.lbFrom = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationLogBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'OperationLogBindingSource
        '
        Me.OperationLogBindingSource.DataMember = "OperationLog1"
        Me.OperationLogBindingSource.DataSource = Me.OperationDBDataSet1
        '
        'EmployeeTableAdapter
        '
        Me.EmployeeTableAdapter.ClearBeforeFill = True
        '
        'OperationLog1TableAdapter
        '
        Me.OperationLog1TableAdapter.ClearBeforeFill = True
        '
        'LogWorkDayTableAdapter1
        '
        Me.LogWorkDayTableAdapter1.ClearBeforeFill = True
        '
        'dbGrid
        '
        Me.dbGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dbGrid.AutoGenerateColumns = False
        Me.dbGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dbGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EmployeeIDDataGridViewTextBoxColumn, Me.EmployeeNameDataGridViewTextBoxColumn, Me.EmployeeNumberDataGridViewTextBoxColumn, Me.PositionIDDataGridViewTextBoxColumn, Me.PositionLevelIDDataGridViewTextBoxColumn, Me.salary, Me.timerate, Me.TimeExecution})
        Me.dbGrid.DataSource = Me.EmployeeBindingSource
        Me.dbGrid.Location = New System.Drawing.Point(0, 0)
        Me.dbGrid.Margin = New System.Windows.Forms.Padding(15, 16, 15, 16)
        Me.dbGrid.Name = "dbGrid"
        Me.dbGrid.RowHeadersWidth = 82
        Me.dbGrid.Size = New System.Drawing.Size(692, 386)
        Me.dbGrid.TabIndex = 1
        '
        'EmployeeIDDataGridViewTextBoxColumn
        '
        Me.EmployeeIDDataGridViewTextBoxColumn.DataPropertyName = "EmployeeID"
        Me.EmployeeIDDataGridViewTextBoxColumn.HeaderText = "Номер сотрудника"
        Me.EmployeeIDDataGridViewTextBoxColumn.MinimumWidth = 10
        Me.EmployeeIDDataGridViewTextBoxColumn.Name = "EmployeeIDDataGridViewTextBoxColumn"
        Me.EmployeeIDDataGridViewTextBoxColumn.Width = 200
        '
        'EmployeeNameDataGridViewTextBoxColumn
        '
        Me.EmployeeNameDataGridViewTextBoxColumn.DataPropertyName = "EmployeeName"
        Me.EmployeeNameDataGridViewTextBoxColumn.HeaderText = "Имя сотрудника"
        Me.EmployeeNameDataGridViewTextBoxColumn.MinimumWidth = 10
        Me.EmployeeNameDataGridViewTextBoxColumn.Name = "EmployeeNameDataGridViewTextBoxColumn"
        Me.EmployeeNameDataGridViewTextBoxColumn.Width = 200
        '
        'EmployeeNumberDataGridViewTextBoxColumn
        '
        Me.EmployeeNumberDataGridViewTextBoxColumn.DataPropertyName = "EmployeeNumber"
        Me.EmployeeNumberDataGridViewTextBoxColumn.HeaderText = "Табельный номер"
        Me.EmployeeNumberDataGridViewTextBoxColumn.MinimumWidth = 10
        Me.EmployeeNumberDataGridViewTextBoxColumn.Name = "EmployeeNumberDataGridViewTextBoxColumn"
        Me.EmployeeNumberDataGridViewTextBoxColumn.Width = 200
        '
        'PositionIDDataGridViewTextBoxColumn
        '
        Me.PositionIDDataGridViewTextBoxColumn.DataPropertyName = "PositionID"
        Me.PositionIDDataGridViewTextBoxColumn.HeaderText = "Должность"
        Me.PositionIDDataGridViewTextBoxColumn.MinimumWidth = 10
        Me.PositionIDDataGridViewTextBoxColumn.Name = "PositionIDDataGridViewTextBoxColumn"
        Me.PositionIDDataGridViewTextBoxColumn.Visible = False
        Me.PositionIDDataGridViewTextBoxColumn.Width = 200
        '
        'PositionLevelIDDataGridViewTextBoxColumn
        '
        Me.PositionLevelIDDataGridViewTextBoxColumn.DataPropertyName = "PositionLevelID"
        Me.PositionLevelIDDataGridViewTextBoxColumn.HeaderText = "Разряд"
        Me.PositionLevelIDDataGridViewTextBoxColumn.MinimumWidth = 10
        Me.PositionLevelIDDataGridViewTextBoxColumn.Name = "PositionLevelIDDataGridViewTextBoxColumn"
        Me.PositionLevelIDDataGridViewTextBoxColumn.Width = 200
        '
        'salary
        '
        Me.salary.HeaderText = "Заработная плата"
        Me.salary.MinimumWidth = 10
        Me.salary.Name = "salary"
        Me.salary.ReadOnly = True
        Me.salary.Width = 200
        '
        'timerate
        '
        Me.timerate.HeaderText = "Норма Выроботки"
        Me.timerate.MinimumWidth = 10
        Me.timerate.Name = "timerate"
        Me.timerate.ReadOnly = True
        Me.timerate.Width = 200
        '
        'TimeExecution
        '
        Me.TimeExecution.HeaderText = "Время выполнения"
        Me.TimeExecution.MinimumWidth = 10
        Me.TimeExecution.Name = "TimeExecution"
        Me.TimeExecution.Width = 200
        '
        'btExport
        '
        Me.btExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btExport.Location = New System.Drawing.Point(430, 402)
        Me.btExport.Name = "btExport"
        Me.btExport.Size = New System.Drawing.Size(75, 23)
        Me.btExport.TabIndex = 15
        Me.btExport.Text = "Выгрузить"
        Me.btExport.UseVisualStyleBackColor = True
        '
        'cbDateRange
        '
        Me.cbDateRange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbDateRange.AutoSize = True
        Me.cbDateRange.Location = New System.Drawing.Point(20, 429)
        Me.cbDateRange.Name = "cbDateRange"
        Me.cbDateRange.Size = New System.Drawing.Size(80, 17)
        Me.cbDateRange.TabIndex = 14
        Me.cbDateRange.Text = "За Период"
        Me.cbDateRange.UseVisualStyleBackColor = True
        '
        'btnCalculate
        '
        Me.btnCalculate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCalculate.Location = New System.Drawing.Point(511, 402)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(75, 23)
        Me.btnCalculate.TabIndex = 13
        Me.btnCalculate.Text = "Рассчитать"
        Me.btnCalculate.UseVisualStyleBackColor = True
        '
        'lbTill
        '
        Me.lbTill.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbTill.AutoSize = True
        Me.lbTill.Location = New System.Drawing.Point(225, 408)
        Me.lbTill.Name = "lbTill"
        Me.lbTill.Size = New System.Drawing.Size(21, 13)
        Me.lbTill.TabIndex = 12
        Me.lbTill.Text = "По"
        Me.lbTill.Visible = False
        '
        'lbFrom
        '
        Me.lbFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbFrom.AutoSize = True
        Me.lbFrom.Location = New System.Drawing.Point(17, 409)
        Me.lbFrom.Name = "lbFrom"
        Me.lbFrom.Size = New System.Drawing.Size(76, 13)
        Me.lbFrom.TabIndex = 11
        Me.lbFrom.Text = "Дата тетради"
        '
        'dtTo
        '
        Me.dtTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(250, 406)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(98, 20)
        Me.dtTo.TabIndex = 10
        Me.dtTo.Visible = False
        '
        'dtFrom
        '
        Me.dtFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(99, 406)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(122, 20)
        Me.dtFrom.TabIndex = 9
        Me.dtFrom.Value = New Date(2019, 7, 6, 22, 46, 33, 0)
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(592, 403)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Закрыть"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'calculationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 469)
        Me.Controls.Add(Me.btExport)
        Me.Controls.Add(Me.cbDateRange)
        Me.Controls.Add(Me.btnCalculate)
        Me.Controls.Add(Me.lbTill)
        Me.Controls.Add(Me.lbFrom)
        Me.Controls.Add(Me.dtTo)
        Me.Controls.Add(Me.dtFrom)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dbGrid)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "calculationForm"
        Me.Text = "Расчет"
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationLogBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OperationDBDataSet1 As ProdControl.OperationDBDataSet1
    Friend WithEvents EmployeeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents EmployeeTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter
    Friend WithEvents OperationLog1TableAdapter As ProdControl.OperationDBDataSet1TableAdapters.OperationLog1TableAdapter
    Friend WithEvents OperationLogBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dlgSave As System.Windows.Forms.SaveFileDialog
    Friend WithEvents LogWorkDayTableAdapter1 As ProdControl.OperationDBDataSet1TableAdapters.LogWorkDayTableAdapter
    Friend WithEvents dbGrid As DataGridView
    Friend WithEvents EmployeeIDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeNameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeNumberDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PositionIDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PositionLevelIDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents salary As DataGridViewTextBoxColumn
    Friend WithEvents timerate As DataGridViewTextBoxColumn
    Friend WithEvents TimeExecution As DataGridViewTextBoxColumn
    Friend WithEvents btExport As Button
    Friend WithEvents cbDateRange As CheckBox
    Friend WithEvents btnCalculate As Button
    Friend WithEvents lbTill As Label
    Friend WithEvents lbFrom As Label
    Friend WithEvents dtTo As DateTimePicker
    Friend WithEvents dtFrom As DateTimePicker
    Friend WithEvents Button1 As Button
End Class
