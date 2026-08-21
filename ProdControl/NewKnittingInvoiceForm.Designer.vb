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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewKnittingInvoiceForm))
        Me.диДщпТгь = New System.Windows.Forms.Label()
        Me.tbLogNum = New System.Windows.Forms.TextBox()
        Me.lbWorkDay = New System.Windows.Forms.Label()
        Me.tbWorkday = New System.Windows.Forms.TextBox()
        Me.lbCount = New System.Windows.Forms.Label()
        Me.tbCount = New System.Windows.Forms.TextBox()
        Me.cbOperation = New System.Windows.Forms.ComboBox()
        Me.lbOperation = New System.Windows.Forms.Label()
        Me.dtPickerOpDate = New System.Windows.Forms.DateTimePicker()
        Me.lbDate = New System.Windows.Forms.Label()
        Me.lbArticul = New System.Windows.Forms.Label()
        Me.cbArticul = New System.Windows.Forms.ComboBox()
        Me.lbEmployee = New System.Windows.Forms.Label()
        Me.cbEmployee = New System.Windows.Forms.ComboBox()
        Me.lbExecutionTime = New System.Windows.Forms.Label()
        Me.tbOperationTime = New System.Windows.Forms.TextBox()
        Me.btnInsert = New System.Windows.Forms.Button()
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
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgOperationLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'диДщпТгь
        '
        Me.диДщпТгь.AutoSize = True
        Me.диДщпТгь.Location = New System.Drawing.Point(9, 15)
        Me.диДщпТгь.Name = "диДщпТгь"
        Me.диДщпТгь.Size = New System.Drawing.Size(97, 13)
        Me.диДщпТгь.TabIndex = 20
        Me.диДщпТгь.Text = "Номер квитанции"
        '
        'tbLogNum
        '
        Me.tbLogNum.Location = New System.Drawing.Point(113, 12)
        Me.tbLogNum.Name = "tbLogNum"
        Me.tbLogNum.Size = New System.Drawing.Size(188, 20)
        Me.tbLogNum.TabIndex = 19
        '
        'lbWorkDay
        '
        Me.lbWorkDay.AutoSize = True
        Me.lbWorkDay.Location = New System.Drawing.Point(575, 15)
        Me.lbWorkDay.Name = "lbWorkDay"
        Me.lbWorkDay.Size = New System.Drawing.Size(76, 13)
        Me.lbWorkDay.TabIndex = 32
        Me.lbWorkDay.Text = "Рабочий день"
        '
        'tbWorkday
        '
        Me.tbWorkday.Location = New System.Drawing.Point(655, 12)
        Me.tbWorkday.Name = "tbWorkday"
        Me.tbWorkday.Size = New System.Drawing.Size(48, 20)
        Me.tbWorkday.TabIndex = 29
        '
        'lbCount
        '
        Me.lbCount.AutoSize = True
        Me.lbCount.Location = New System.Drawing.Point(341, 43)
        Me.lbCount.Name = "lbCount"
        Me.lbCount.Size = New System.Drawing.Size(66, 13)
        Me.lbCount.TabIndex = 31
        Me.lbCount.Text = "Количество"
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
        Me.cbOperation.DisplayMember = "OperationName"
        Me.cbOperation.FormattingEnabled = True
        Me.cbOperation.Location = New System.Drawing.Point(411, 64)
        Me.cbOperation.Name = "cbOperation"
        Me.cbOperation.Size = New System.Drawing.Size(147, 21)
        Me.cbOperation.TabIndex = 27
        Me.cbOperation.ValueMember = "Operation.OperationID"
        '
        'lbOperation
        '
        Me.lbOperation.AutoSize = True
        Me.lbOperation.Location = New System.Drawing.Point(364, 67)
        Me.lbOperation.Name = "lbOperation"
        Me.lbOperation.Size = New System.Drawing.Size(45, 13)
        Me.lbOperation.TabIndex = 30
        Me.lbOperation.Text = "Деталь"
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
        'lbDate
        '
        Me.lbDate.AutoSize = True
        Me.lbDate.Location = New System.Drawing.Point(372, 15)
        Me.lbDate.Name = "lbDate"
        Me.lbDate.Size = New System.Drawing.Size(33, 13)
        Me.lbDate.TabIndex = 28
        Me.lbDate.Text = "Дата"
        '
        'lbArticul
        '
        Me.lbArticul.AutoSize = True
        Me.lbArticul.Location = New System.Drawing.Point(59, 40)
        Me.lbArticul.Name = "lbArticul"
        Me.lbArticul.Size = New System.Drawing.Size(48, 13)
        Me.lbArticul.TabIndex = 25
        Me.lbArticul.Text = "Артикул"
        '
        'cbArticul
        '
        Me.cbArticul.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArticul.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArticul.DisplayMember = "ArticulName"
        Me.cbArticul.FormattingEnabled = True
        Me.cbArticul.Location = New System.Drawing.Point(113, 37)
        Me.cbArticul.Name = "cbArticul"
        Me.cbArticul.Size = New System.Drawing.Size(188, 21)
        Me.cbArticul.TabIndex = 23
        Me.cbArticul.ValueMember = "ArticulID"
        '
        'lbEmployee
        '
        Me.lbEmployee.AutoSize = True
        Me.lbEmployee.Location = New System.Drawing.Point(47, 66)
        Me.lbEmployee.Name = "lbEmployee"
        Me.lbEmployee.Size = New System.Drawing.Size(60, 13)
        Me.lbEmployee.TabIndex = 22
        Me.lbEmployee.Text = "Сотрудник"
        '
        'cbEmployee
        '
        Me.cbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbEmployee.DisplayMember = "EmployeeName"
        Me.cbEmployee.FormattingEnabled = True
        Me.cbEmployee.Location = New System.Drawing.Point(113, 63)
        Me.cbEmployee.Name = "cbEmployee"
        Me.cbEmployee.Size = New System.Drawing.Size(188, 21)
        Me.cbEmployee.TabIndex = 26
        Me.cbEmployee.ValueMember = "EmployeeID"
        '
        'lbExecutionTime
        '
        Me.lbExecutionTime.AutoSize = True
        Me.lbExecutionTime.Location = New System.Drawing.Point(303, 121)
        Me.lbExecutionTime.Name = "lbExecutionTime"
        Me.lbExecutionTime.Size = New System.Drawing.Size(105, 13)
        Me.lbExecutionTime.TabIndex = 34
        Me.lbExecutionTime.Text = "Время выполнения"
        '
        'tbOperationTime
        '
        Me.tbOperationTime.Enabled = False
        Me.tbOperationTime.Location = New System.Drawing.Point(411, 118)
        Me.tbOperationTime.Name = "tbOperationTime"
        Me.tbOperationTime.Size = New System.Drawing.Size(147, 20)
        Me.tbOperationTime.TabIndex = 33
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
        Me.dgOperationLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cEmployeeID, Me.EmployeeName, Me.LogNumber, Me.LogDate, Me.ArticulID, Me.ArticulName, Me.ExecutionTime, Me.CogCount, Me.OperationID, Me.OperationName, Me.workDay})
        Me.dgOperationLog.Location = New System.Drawing.Point(12, 169)
        Me.dgOperationLog.Name = "dgOperationLog"
        Me.dgOperationLog.RowHeadersWidth = 82
        Me.dgOperationLog.Size = New System.Drawing.Size(776, 309)
        Me.dgOperationLog.TabIndex = 36
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
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(713, 484)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 37
        Me.Button1.Text = "Сохранить"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBox1.DisplayMember = "OperationName"
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(411, 91)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(147, 21)
        Me.ComboBox1.TabIndex = 39
        Me.ComboBox1.ValueMember = "Operation.OperationID"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(327, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Оборудование"
        '
        'NewKnittingInvoiceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 511)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dgOperationLog)
        Me.Controls.Add(Me.btnInsert)
        Me.Controls.Add(Me.lbExecutionTime)
        Me.Controls.Add(Me.tbOperationTime)
        Me.Controls.Add(Me.lbWorkDay)
        Me.Controls.Add(Me.tbWorkday)
        Me.Controls.Add(Me.lbCount)
        Me.Controls.Add(Me.tbCount)
        Me.Controls.Add(Me.cbOperation)
        Me.Controls.Add(Me.lbOperation)
        Me.Controls.Add(Me.dtPickerOpDate)
        Me.Controls.Add(Me.lbDate)
        Me.Controls.Add(Me.lbArticul)
        Me.Controls.Add(Me.cbArticul)
        Me.Controls.Add(Me.lbEmployee)
        Me.Controls.Add(Me.cbEmployee)
        Me.Controls.Add(Me.диДщпТгь)
        Me.Controls.Add(Me.tbLogNum)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewKnittingInvoiceForm"
        Me.Text = "Новая накладная (Вязальный цех)"
        CType(Me.dgOperationLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents диДщпТгь As Label
    Friend WithEvents tbLogNum As TextBox
    Friend WithEvents lbWorkDay As Label
    Friend WithEvents tbWorkday As TextBox
    Friend WithEvents lbCount As Label
    Friend WithEvents tbCount As TextBox
    Friend WithEvents cbOperation As ComboBox
    Friend WithEvents lbOperation As Label
    Friend WithEvents dtPickerOpDate As DateTimePicker
    Friend WithEvents lbDate As Label
    Friend WithEvents lbArticul As Label
    Friend WithEvents cbArticul As ComboBox
    Friend WithEvents lbEmployee As Label
    Friend WithEvents cbEmployee As ComboBox
    Friend WithEvents lbExecutionTime As Label
    Friend WithEvents tbOperationTime As TextBox
    Friend WithEvents btnInsert As Button
    Friend WithEvents dgOperationLog As DataGridView
    Friend WithEvents cEmployeeID As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeName As DataGridViewTextBoxColumn
    Friend WithEvents LogNumber As DataGridViewTextBoxColumn
    Friend WithEvents LogDate As DataGridViewTextBoxColumn
    Friend WithEvents ArticulID As DataGridViewTextBoxColumn
    Friend WithEvents ArticulName As DataGridViewTextBoxColumn
    Friend WithEvents ExecutionTime As DataGridViewTextBoxColumn
    Friend WithEvents CogCount As DataGridViewTextBoxColumn
    Friend WithEvents OperationID As DataGridViewTextBoxColumn
    Friend WithEvents OperationName As DataGridViewTextBoxColumn
    Friend WithEvents workDay As DataGridViewTextBoxColumn
    Friend WithEvents btnDelete As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
End Class
