<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newBill
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(newBill))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.dgOperationLog = New System.Windows.Forms.DataGridView()
        Me.cEmployeeID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LogNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LogDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticulID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticulName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExecutionTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CogCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperationID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperationName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.workDay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsMenuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnCalculateExecutionTime = New System.Windows.Forms.Button()
        Me.labelTypeOfNonRateSituation = New System.Windows.Forms.Label()
        Me.cbTypeOfNonRateSituation = New System.Windows.Forms.ComboBox()
        Me.IdleOperationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet12 = New ProdControl.OperationDBDataSet1()
        Me.lbWorkDay = New System.Windows.Forms.Label()
        Me.tbWorkday = New System.Windows.Forms.TextBox()
        Me.диДщпТгь = New System.Windows.Forms.Label()
        Me.tbLogNum = New System.Windows.Forms.TextBox()
        Me.lbCount = New System.Windows.Forms.Label()
        Me.lbExecutionTime = New System.Windows.Forms.Label()
        Me.tbCount = New System.Windows.Forms.TextBox()
        Me.cbOperation = New System.Windows.Forms.ComboBox()
        Me.OperationBindingSource3 = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnInsert = New System.Windows.Forms.Button()
        Me.lbOperation = New System.Windows.Forms.Label()
        Me.tbOperationTime = New System.Windows.Forms.TextBox()
        Me.cbIsStandard = New System.Windows.Forms.CheckBox()
        Me.dtPickerOpDate = New System.Windows.Forms.DateTimePicker()
        Me.lbDate = New System.Windows.Forms.Label()
        Me.lbArticul = New System.Windows.Forms.Label()
        Me.cbArticul = New System.Windows.Forms.ComboBox()
        Me.ArticulBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.elnEmpNumber = New System.Windows.Forms.Label()
        Me.lbEmployee = New System.Windows.Forms.Label()
        Me.cbEmployee = New System.Windows.Forms.ComboBox()
        Me.EmployeeBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ArticulBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationLogBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationOperationLogBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ArticulBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationLogBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet11 = New ProdControl.OperationDBDataSet1()
        Me.OperationLog1TableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.OperationLog1TableAdapter()
        Me.OperationDBDataSet12BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationTableAdapter1 = New ProdControl.OperationDBDataSet1TableAdapters.OperationTableAdapter()
        Me.ArticulTableAdapter1 = New ProdControl.OperationDBDataSet1TableAdapters.ArticulTableAdapter()
        Me.EmployeeTableAdapter1 = New ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter()
        Me.LogWorkDayTableAdapter1 = New ProdControl.OperationDBDataSet1TableAdapters.LogWorkDayTableAdapter()
        Me.IdleOperationTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.IdleOperationTableAdapter()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgOperationLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.IdleOperationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeeBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationLogBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationOperationLogBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationLogBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet12BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 413)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(848, 40)
        Me.Panel1.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(604, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Удалить"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button3.Location = New System.Drawing.Point(766, 9)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Закрыть"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(450, 9)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(130, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Сохранить и Добавить"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(685, 9)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Сохранить"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.dgOperationLog)
        Me.Panel2.Controls.Add(Me.pnlTop)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(848, 413)
        Me.Panel2.TabIndex = 1
        '
        'dgOperationLog
        '
        Me.dgOperationLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgOperationLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOperationLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cEmployeeID, Me.EmployeeName, Me.LogNumber, Me.LogDate, Me.ArticulID, Me.ArticulName, Me.ExecutionTime, Me.CogCount, Me.OperationID, Me.OperationName, Me.workDay})
        Me.dgOperationLog.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgOperationLog.Location = New System.Drawing.Point(3, 170)
        Me.dgOperationLog.Name = "dgOperationLog"
        Me.dgOperationLog.RowHeadersWidth = 82
        Me.dgOperationLog.Size = New System.Drawing.Size(844, 237)
        Me.dgOperationLog.TabIndex = 2
        '
        'cEmployeeID
        '
        Me.cEmployeeID.HeaderText = "IDСотрудника"
        Me.cEmployeeID.MinimumWidth = 10
        Me.cEmployeeID.Name = "cEmployeeID"
        Me.cEmployeeID.ReadOnly = True
        Me.cEmployeeID.Visible = False
        Me.cEmployeeID.Width = 200
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
        'ArticulID
        '
        Me.ArticulID.HeaderText = "IDArticul"
        Me.ArticulID.MinimumWidth = 10
        Me.ArticulID.Name = "ArticulID"
        Me.ArticulID.ReadOnly = True
        Me.ArticulID.Visible = False
        Me.ArticulID.Width = 200
        '
        'ArticulName
        '
        Me.ArticulName.HeaderText = "Артикул"
        Me.ArticulName.MinimumWidth = 10
        Me.ArticulName.Name = "ArticulName"
        Me.ArticulName.ReadOnly = True
        Me.ArticulName.Width = 200
        '
        'ExecutionTime
        '
        Me.ExecutionTime.HeaderText = "Время выполнения"
        Me.ExecutionTime.MinimumWidth = 10
        Me.ExecutionTime.Name = "ExecutionTime"
        Me.ExecutionTime.ReadOnly = True
        Me.ExecutionTime.Width = 200
        '
        'CogCount
        '
        Me.CogCount.HeaderText = "Количество"
        Me.CogCount.MinimumWidth = 10
        Me.CogCount.Name = "CogCount"
        Me.CogCount.ReadOnly = True
        Me.CogCount.Width = 200
        '
        'OperationID
        '
        Me.OperationID.HeaderText = "ОперацияID"
        Me.OperationID.MinimumWidth = 10
        Me.OperationID.Name = "OperationID"
        Me.OperationID.ReadOnly = True
        Me.OperationID.Visible = False
        Me.OperationID.Width = 200
        '
        'OperationName
        '
        Me.OperationName.HeaderText = "Операция"
        Me.OperationName.MinimumWidth = 10
        Me.OperationName.Name = "OperationName"
        Me.OperationName.ReadOnly = True
        Me.OperationName.Width = 200
        '
        'workDay
        '
        Me.workDay.HeaderText = "Рабочий день"
        Me.workDay.MinimumWidth = 10
        Me.workDay.Name = "workDay"
        Me.workDay.Width = 200
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsMenuEdit, Me.tsDelete})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(155, 48)
        '
        'tsMenuEdit
        '
        Me.tsMenuEdit.Name = "tsMenuEdit"
        Me.tsMenuEdit.Size = New System.Drawing.Size(154, 22)
        Me.tsMenuEdit.Text = "Редактировать"
        '
        'tsDelete
        '
        Me.tsDelete.Name = "tsDelete"
        Me.tsDelete.Size = New System.Drawing.Size(154, 22)
        Me.tsDelete.Text = "Удалить"
        '
        'pnlTop
        '
        Me.pnlTop.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTop.Controls.Add(Me.btnCalculateExecutionTime)
        Me.pnlTop.Controls.Add(Me.labelTypeOfNonRateSituation)
        Me.pnlTop.Controls.Add(Me.cbTypeOfNonRateSituation)
        Me.pnlTop.Controls.Add(Me.lbWorkDay)
        Me.pnlTop.Controls.Add(Me.tbWorkday)
        Me.pnlTop.Controls.Add(Me.диДщпТгь)
        Me.pnlTop.Controls.Add(Me.tbLogNum)
        Me.pnlTop.Controls.Add(Me.lbCount)
        Me.pnlTop.Controls.Add(Me.lbExecutionTime)
        Me.pnlTop.Controls.Add(Me.tbCount)
        Me.pnlTop.Controls.Add(Me.cbOperation)
        Me.pnlTop.Controls.Add(Me.btnInsert)
        Me.pnlTop.Controls.Add(Me.lbOperation)
        Me.pnlTop.Controls.Add(Me.tbOperationTime)
        Me.pnlTop.Controls.Add(Me.cbIsStandard)
        Me.pnlTop.Controls.Add(Me.dtPickerOpDate)
        Me.pnlTop.Controls.Add(Me.lbDate)
        Me.pnlTop.Controls.Add(Me.lbArticul)
        Me.pnlTop.Controls.Add(Me.cbArticul)
        Me.pnlTop.Controls.Add(Me.elnEmpNumber)
        Me.pnlTop.Controls.Add(Me.lbEmployee)
        Me.pnlTop.Controls.Add(Me.cbEmployee)
        Me.pnlTop.Location = New System.Drawing.Point(4, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(843, 164)
        Me.pnlTop.TabIndex = 1
        '
        'btnCalculateExecutionTime
        '
        Me.btnCalculateExecutionTime.Enabled = False
        Me.btnCalculateExecutionTime.Location = New System.Drawing.Point(528, 115)
        Me.btnCalculateExecutionTime.Name = "btnCalculateExecutionTime"
        Me.btnCalculateExecutionTime.Size = New System.Drawing.Size(117, 22)
        Me.btnCalculateExecutionTime.TabIndex = 24
        Me.btnCalculateExecutionTime.Text = "Рассчитать время"
        Me.btnCalculateExecutionTime.UseVisualStyleBackColor = True
        '
        'labelTypeOfNonRateSituation
        '
        Me.labelTypeOfNonRateSituation.AutoSize = True
        Me.labelTypeOfNonRateSituation.Location = New System.Drawing.Point(294, 116)
        Me.labelTypeOfNonRateSituation.Name = "labelTypeOfNonRateSituation"
        Me.labelTypeOfNonRateSituation.Size = New System.Drawing.Size(75, 13)
        Me.labelTypeOfNonRateSituation.TabIndex = 23
        Me.labelTypeOfNonRateSituation.Text = "Тип ситуации"
        '
        'cbTypeOfNonRateSituation
        '
        Me.cbTypeOfNonRateSituation.DataSource = Me.IdleOperationBindingSource
        Me.cbTypeOfNonRateSituation.DisplayMember = "idle_operation_name"
        Me.cbTypeOfNonRateSituation.Enabled = False
        Me.cbTypeOfNonRateSituation.FormattingEnabled = True
        Me.cbTypeOfNonRateSituation.Location = New System.Drawing.Point(374, 115)
        Me.cbTypeOfNonRateSituation.Margin = New System.Windows.Forms.Padding(2)
        Me.cbTypeOfNonRateSituation.Name = "cbTypeOfNonRateSituation"
        Me.cbTypeOfNonRateSituation.Size = New System.Drawing.Size(147, 21)
        Me.cbTypeOfNonRateSituation.TabIndex = 22
        Me.cbTypeOfNonRateSituation.ValueMember = "idle_operation_id"
        '
        'IdleOperationBindingSource
        '
        Me.IdleOperationBindingSource.DataMember = "IdleOperation"
        Me.IdleOperationBindingSource.DataSource = Me.OperationDBDataSet12
        '
        'OperationDBDataSet12
        '
        Me.OperationDBDataSet12.DataSetName = "OperationDBDataSet1"
        Me.OperationDBDataSet12.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lbWorkDay
        '
        Me.lbWorkDay.AutoSize = True
        Me.lbWorkDay.Location = New System.Drawing.Point(538, 11)
        Me.lbWorkDay.Name = "lbWorkDay"
        Me.lbWorkDay.Size = New System.Drawing.Size(76, 13)
        Me.lbWorkDay.TabIndex = 20
        Me.lbWorkDay.Text = "Рабочий день"
        '
        'tbWorkday
        '
        Me.tbWorkday.Location = New System.Drawing.Point(618, 8)
        Me.tbWorkday.Name = "tbWorkday"
        Me.tbWorkday.Size = New System.Drawing.Size(48, 20)
        Me.tbWorkday.TabIndex = 8
        '
        'диДщпТгь
        '
        Me.диДщпТгь.AutoSize = True
        Me.диДщпТгь.Location = New System.Drawing.Point(0, 11)
        Me.диДщпТгь.Name = "диДщпТгь"
        Me.диДщпТгь.Size = New System.Drawing.Size(97, 13)
        Me.диДщпТгь.TabIndex = 18
        Me.диДщпТгь.Text = "Номер квитанции"
        '
        'tbLogNum
        '
        Me.tbLogNum.Location = New System.Drawing.Point(104, 8)
        Me.tbLogNum.Name = "tbLogNum"
        Me.tbLogNum.Size = New System.Drawing.Size(188, 20)
        Me.tbLogNum.TabIndex = 0
        '
        'lbCount
        '
        Me.lbCount.AutoSize = True
        Me.lbCount.Location = New System.Drawing.Point(304, 39)
        Me.lbCount.Name = "lbCount"
        Me.lbCount.Size = New System.Drawing.Size(66, 13)
        Me.lbCount.TabIndex = 16
        Me.lbCount.Text = "Количество"
        '
        'lbExecutionTime
        '
        Me.lbExecutionTime.AutoSize = True
        Me.lbExecutionTime.Location = New System.Drawing.Point(266, 90)
        Me.lbExecutionTime.Name = "lbExecutionTime"
        Me.lbExecutionTime.Size = New System.Drawing.Size(105, 13)
        Me.lbExecutionTime.TabIndex = 15
        Me.lbExecutionTime.Text = "Время выполнения"
        '
        'tbCount
        '
        Me.tbCount.Location = New System.Drawing.Point(374, 35)
        Me.tbCount.Name = "tbCount"
        Me.tbCount.Size = New System.Drawing.Size(147, 20)
        Me.tbCount.TabIndex = 3
        '
        'cbOperation
        '
        Me.cbOperation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbOperation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbOperation.DataSource = Me.OperationBindingSource3
        Me.cbOperation.DisplayMember = "OperationName"
        Me.cbOperation.FormattingEnabled = True
        Me.cbOperation.Location = New System.Drawing.Point(374, 60)
        Me.cbOperation.Name = "cbOperation"
        Me.cbOperation.Size = New System.Drawing.Size(147, 21)
        Me.cbOperation.TabIndex = 5
        Me.cbOperation.ValueMember = "Operation.OperationID"
        '
        'OperationBindingSource3
        '
        Me.OperationBindingSource3.DataMember = "Operation"
        Me.OperationBindingSource3.DataSource = Me.OperationDBDataSet12
        Me.OperationBindingSource3.Filter = ""
        '
        'btnInsert
        '
        Me.btnInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInsert.Location = New System.Drawing.Point(762, 132)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(75, 23)
        Me.btnInsert.TabIndex = 12
        Me.btnInsert.Text = "Вставить"
        Me.btnInsert.UseVisualStyleBackColor = True
        '
        'lbOperation
        '
        Me.lbOperation.AutoSize = True
        Me.lbOperation.Location = New System.Drawing.Point(308, 63)
        Me.lbOperation.Name = "lbOperation"
        Me.lbOperation.Size = New System.Drawing.Size(57, 13)
        Me.lbOperation.TabIndex = 11
        Me.lbOperation.Text = "Операция"
        '
        'tbOperationTime
        '
        Me.tbOperationTime.Enabled = False
        Me.tbOperationTime.Location = New System.Drawing.Point(374, 87)
        Me.tbOperationTime.Name = "tbOperationTime"
        Me.tbOperationTime.Size = New System.Drawing.Size(147, 20)
        Me.tbOperationTime.TabIndex = 7
        '
        'cbIsStandard
        '
        Me.cbIsStandard.AutoSize = True
        Me.cbIsStandard.Location = New System.Drawing.Point(528, 90)
        Me.cbIsStandard.Name = "cbIsStandard"
        Me.cbIsStandard.Size = New System.Drawing.Size(123, 17)
        Me.cbIsStandard.TabIndex = 6
        Me.cbIsStandard.Text = "Не нормированная"
        Me.cbIsStandard.UseVisualStyleBackColor = True
        '
        'dtPickerOpDate
        '
        Me.dtPickerOpDate.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.dtPickerOpDate.Location = New System.Drawing.Point(374, 8)
        Me.dtPickerOpDate.Name = "dtPickerOpDate"
        Me.dtPickerOpDate.Size = New System.Drawing.Size(149, 20)
        Me.dtPickerOpDate.TabIndex = 1
        Me.dtPickerOpDate.Value = New Date(2019, 7, 4, 14, 54, 35, 0)
        '
        'lbDate
        '
        Me.lbDate.AutoSize = True
        Me.lbDate.Location = New System.Drawing.Point(308, 11)
        Me.lbDate.Name = "lbDate"
        Me.lbDate.Size = New System.Drawing.Size(33, 13)
        Me.lbDate.TabIndex = 5
        Me.lbDate.Text = "Дата"
        '
        'lbArticul
        '
        Me.lbArticul.AutoSize = True
        Me.lbArticul.Location = New System.Drawing.Point(50, 37)
        Me.lbArticul.Name = "lbArticul"
        Me.lbArticul.Size = New System.Drawing.Size(48, 13)
        Me.lbArticul.TabIndex = 4
        Me.lbArticul.Text = "Артикул"
        '
        'cbArticul
        '
        Me.cbArticul.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArticul.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArticul.DataSource = Me.ArticulBindingSource2
        Me.cbArticul.DisplayMember = "ArticulName"
        Me.cbArticul.FormattingEnabled = True
        Me.cbArticul.Location = New System.Drawing.Point(104, 34)
        Me.cbArticul.Name = "cbArticul"
        Me.cbArticul.Size = New System.Drawing.Size(188, 21)
        Me.cbArticul.TabIndex = 2
        Me.cbArticul.ValueMember = "ArticulID"
        '
        'ArticulBindingSource2
        '
        Me.ArticulBindingSource2.DataMember = "Articul"
        Me.ArticulBindingSource2.DataSource = Me.OperationDBDataSet12
        '
        'elnEmpNumber
        '
        Me.elnEmpNumber.AutoSize = True
        Me.elnEmpNumber.Location = New System.Drawing.Point(258, 67)
        Me.elnEmpNumber.Name = "elnEmpNumber"
        Me.elnEmpNumber.Size = New System.Drawing.Size(0, 13)
        Me.elnEmpNumber.TabIndex = 2
        '
        'lbEmployee
        '
        Me.lbEmployee.AutoSize = True
        Me.lbEmployee.Location = New System.Drawing.Point(38, 63)
        Me.lbEmployee.Name = "lbEmployee"
        Me.lbEmployee.Size = New System.Drawing.Size(60, 13)
        Me.lbEmployee.TabIndex = 1
        Me.lbEmployee.Text = "Сотрудник"
        '
        'cbEmployee
        '
        Me.cbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbEmployee.DataSource = Me.EmployeeBindingSource1
        Me.cbEmployee.DisplayMember = "EmployeeName"
        Me.cbEmployee.FormattingEnabled = True
        Me.cbEmployee.Location = New System.Drawing.Point(104, 60)
        Me.cbEmployee.Name = "cbEmployee"
        Me.cbEmployee.Size = New System.Drawing.Size(188, 21)
        Me.cbEmployee.TabIndex = 4
        Me.cbEmployee.ValueMember = "EmployeeID"
        '
        'EmployeeBindingSource1
        '
        Me.EmployeeBindingSource1.DataMember = "Employee"
        Me.EmployeeBindingSource1.DataSource = Me.OperationDBDataSet12
        '
        'OperationBindingSource2
        '
        Me.OperationBindingSource2.DataMember = "Operation"
        '
        'ArticulBindingSource1
        '
        Me.ArticulBindingSource1.DataMember = "Articul"
        '
        'EmployeeBindingSource
        '
        Me.EmployeeBindingSource.DataMember = "Employee"
        '
        'OperationLogBindingSource
        '
        Me.OperationLogBindingSource.DataMember = "OperationLog1"
        '
        'OperationOperationLogBindingSource
        '
        Me.OperationOperationLogBindingSource.DataSource = Me.OperationBindingSource1
        '
        'OperationBindingSource1
        '
        Me.OperationBindingSource1.DataMember = "Operation"
        '
        'ArticulBindingSource
        '
        Me.ArticulBindingSource.DataMember = "Articul"
        '
        'OperationBindingSource
        '
        Me.OperationBindingSource.DataMember = "Operation"
        '
        'OperationLogBindingSource1
        '
        Me.OperationLogBindingSource1.DataMember = "OperationLog1"
        Me.OperationLogBindingSource1.DataSource = Me.OperationDBDataSet11
        '
        'OperationDBDataSet11
        '
        Me.OperationDBDataSet11.DataSetName = "OperationDBDataSet1"
        Me.OperationDBDataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'OperationLog1TableAdapter
        '
        Me.OperationLog1TableAdapter.ClearBeforeFill = True
        '
        'OperationDBDataSet12BindingSource
        '
        Me.OperationDBDataSet12BindingSource.DataSource = Me.OperationDBDataSet12
        Me.OperationDBDataSet12BindingSource.Position = 0
        '
        'OperationTableAdapter1
        '
        Me.OperationTableAdapter1.ClearBeforeFill = True
        '
        'ArticulTableAdapter1
        '
        Me.ArticulTableAdapter1.ClearBeforeFill = True
        '
        'EmployeeTableAdapter1
        '
        Me.EmployeeTableAdapter1.ClearBeforeFill = True
        '
        'LogWorkDayTableAdapter1
        '
        Me.LogWorkDayTableAdapter1.ClearBeforeFill = True
        '
        'IdleOperationTableAdapter
        '
        Me.IdleOperationTableAdapter.ClearBeforeFill = True
        '
        'newBill
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button3
        Me.ClientSize = New System.Drawing.Size(848, 453)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "newBill"
        Me.Text = "Новая Накладная"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgOperationLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.IdleOperationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeeBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationLogBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationOperationLogBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationLogBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet12BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents cbEmployee As System.Windows.Forms.ComboBox
    Friend WithEvents OperationDBDataSet1 As ProdControl.OperationDBDataSet1
    Friend WithEvents EmployeeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents EmployeeTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter
    Friend WithEvents elnEmpNumber As System.Windows.Forms.Label
    Friend WithEvents lbEmployee As System.Windows.Forms.Label
    Friend WithEvents OperationLogBindingSource As System.Windows.Forms.BindingSource
    
    Friend WithEvents OperationLog1TableAdapter As ProdControl.OperationDBDataSet1TableAdapters.OperationLog1TableAdapter
    Friend WithEvents ArticulBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ArticulTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.ArticulTableAdapter
    Friend WithEvents LogIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmployeeIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OperationIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticulDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DateExecutionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TimeExecutionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsStandardTimeDataGridViewCheckBoxColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cbArticul As System.Windows.Forms.ComboBox
    Friend WithEvents ArticulBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents lbArticul As System.Windows.Forms.Label
    Friend WithEvents dtPickerOpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbDate As System.Windows.Forms.Label
    Friend WithEvents OperationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OperationTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.OperationTableAdapter
    Friend WithEvents tbOperationTime As System.Windows.Forms.TextBox
    Friend WithEvents cbIsStandard As System.Windows.Forms.CheckBox
    Friend WithEvents OperationBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents lbOperation As System.Windows.Forms.Label
    Friend WithEvents OperationBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents OperationOperationLogBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents btnInsert As System.Windows.Forms.Button
    Friend WithEvents OperationDBDataSet11 As ProdControl.OperationDBDataSet1
    Friend WithEvents OperationLogBindingSource1 As System.Windows.Forms.BindingSource
    'Friend WithEvents OperationLog1TableAdapter As ProdControl.OperationDBDataSet1TableAdapters.OperationLog1TableAdapter
    Friend WithEvents cbOperation As System.Windows.Forms.ComboBox
    Friend WithEvents OperationDBDataSet12BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OperationDBDataSet12 As ProdControl.OperationDBDataSet1
    Friend WithEvents OperationBindingSource3 As System.Windows.Forms.BindingSource
    Friend WithEvents OperationTableAdapter1 As ProdControl.OperationDBDataSet1TableAdapters.OperationTableAdapter
    Friend WithEvents ArticulBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents ArticulTableAdapter1 As ProdControl.OperationDBDataSet1TableAdapters.ArticulTableAdapter
    Friend WithEvents EmployeeBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents EmployeeTableAdapter1 As ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter
    Friend WithEvents lbCount As System.Windows.Forms.Label
    Friend WithEvents lbExecutionTime As System.Windows.Forms.Label
    Friend WithEvents tbCount As System.Windows.Forms.TextBox
    Friend WithEvents dgOperationLog As System.Windows.Forms.DataGridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsMenuEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents диДщпТгь As System.Windows.Forms.Label
    Friend WithEvents tbLogNum As System.Windows.Forms.TextBox
    Friend WithEvents tbWorkday As System.Windows.Forms.TextBox
    Friend WithEvents lbWorkDay As System.Windows.Forms.Label
    Friend WithEvents LogWorkDayTableAdapter1 As ProdControl.OperationDBDataSet1TableAdapters.LogWorkDayTableAdapter
    Friend WithEvents cEmployeeID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmployeeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LogNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LogDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticulID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticulName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExecutionTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CogCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OperationID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OperationName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents workDay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnDelete As Button
    Friend WithEvents cbTypeOfNonRateSituation As ComboBox
    Friend WithEvents IdleOperationBindingSource As BindingSource
    Friend WithEvents IdleOperationTableAdapter As OperationDBDataSet1TableAdapters.IdleOperationTableAdapter
    Friend WithEvents labelTypeOfNonRateSituation As Label
    Friend WithEvents btnCalculateExecutionTime As Button
End Class
