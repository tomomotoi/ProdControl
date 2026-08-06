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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tbOperationTime = New System.Windows.Forms.TextBox()
        Me.cbIsStandard = New System.Windows.Forms.CheckBox()
        Me.dtPickerOpDate = New System.Windows.Forms.DateTimePicker()
        Me.lbDate = New System.Windows.Forms.Label()
        Me.lbArticul = New System.Windows.Forms.Label()
        Me.cbArticul = New System.Windows.Forms.ComboBox()
        Me.elnEmpNumber = New System.Windows.Forms.Label()
        Me.lbEmployee = New System.Windows.Forms.Label()
        Me.cbEmployee = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.cbOperation = New System.Windows.Forms.ComboBox()
        Me.lbOperation = New System.Windows.Forms.Label()
        Me.OperationOperationLogBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnInsert = New System.Windows.Forms.Button()
        Me.OperationBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ArticulBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ArticulBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationLogBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationOperationLogBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationLogBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 413)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(803, 40)
        Me.Panel1.TabIndex = 0
        '
        'Button3
        '
        Me.Button3.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button3.Location = New System.Drawing.Point(586, 9)
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
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(369, 9)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Сохранить"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.pnlTop)
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(803, 413)
        Me.Panel2.TabIndex = 1
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.btnInsert)
        Me.pnlTop.Controls.Add(Me.lbOperation)
        Me.pnlTop.Controls.Add(Me.cbOperation)
        Me.pnlTop.Controls.Add(Me.tbOperationTime)
        Me.pnlTop.Controls.Add(Me.cbIsStandard)
        Me.pnlTop.Controls.Add(Me.dtPickerOpDate)
        Me.pnlTop.Controls.Add(Me.lbDate)
        Me.pnlTop.Controls.Add(Me.lbArticul)
        Me.pnlTop.Controls.Add(Me.cbArticul)
        Me.pnlTop.Controls.Add(Me.elnEmpNumber)
        Me.pnlTop.Controls.Add(Me.lbEmployee)
        Me.pnlTop.Controls.Add(Me.cbEmployee)
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(803, 106)
        Me.pnlTop.TabIndex = 1
        '
        'tbOperationTime
        '
        Me.tbOperationTime.Enabled = False
        Me.tbOperationTime.Location = New System.Drawing.Point(404, 71)
        Me.tbOperationTime.Name = "tbOperationTime"
        Me.tbOperationTime.Size = New System.Drawing.Size(147, 20)
        Me.tbOperationTime.TabIndex = 5
        '
        'cbIsStandard
        '
        Me.cbIsStandard.AutoSize = True
        Me.cbIsStandard.Location = New System.Drawing.Point(120, 71)
        Me.cbIsStandard.Name = "cbIsStandard"
        Me.cbIsStandard.Size = New System.Drawing.Size(123, 17)
        Me.cbIsStandard.TabIndex = 4
        Me.cbIsStandard.Text = "Не нормированная"
        Me.cbIsStandard.UseVisualStyleBackColor = True
        '
        'dtPickerOpDate
        '
        Me.dtPickerOpDate.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.dtPickerOpDate.Location = New System.Drawing.Point(402, 12)
        Me.dtPickerOpDate.Name = "dtPickerOpDate"
        Me.dtPickerOpDate.Size = New System.Drawing.Size(149, 20)
        Me.dtPickerOpDate.TabIndex = 1
        Me.dtPickerOpDate.Value = New Date(2019, 7, 4, 14, 54, 35, 0)
        '
        'lbDate
        '
        Me.lbDate.AutoSize = True
        Me.lbDate.Location = New System.Drawing.Point(339, 15)
        Me.lbDate.Name = "lbDate"
        Me.lbDate.Size = New System.Drawing.Size(33, 13)
        Me.lbDate.TabIndex = 5
        Me.lbDate.Text = "Дата"
        '
        'lbArticul
        '
        Me.lbArticul.AutoSize = True
        Me.lbArticul.Location = New System.Drawing.Point(58, 39)
        Me.lbArticul.Name = "lbArticul"
        Me.lbArticul.Size = New System.Drawing.Size(48, 13)
        Me.lbArticul.TabIndex = 4
        Me.lbArticul.Text = "Артикул"
        '
        'cbArticul
        '
        Me.cbArticul.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArticul.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArticul.DataSource = Me.ArticulBindingSource1
        Me.cbArticul.DisplayMember = "ArticulName"
        Me.cbArticul.FormattingEnabled = True
        Me.cbArticul.Location = New System.Drawing.Point(121, 39)
        Me.cbArticul.Name = "cbArticul"
        Me.cbArticul.Size = New System.Drawing.Size(188, 21)
        Me.cbArticul.TabIndex = 2
        Me.cbArticul.ValueMember = "ArticulID"
        '
        'elnEmpNumber
        '
        Me.elnEmpNumber.AutoSize = True
        Me.elnEmpNumber.Location = New System.Drawing.Point(316, 19)
        Me.elnEmpNumber.Name = "elnEmpNumber"
        Me.elnEmpNumber.Size = New System.Drawing.Size(0, 13)
        Me.elnEmpNumber.TabIndex = 2
        '
        'lbEmployee
        '
        Me.lbEmployee.AutoSize = True
        Me.lbEmployee.Location = New System.Drawing.Point(55, 15)
        Me.lbEmployee.Name = "lbEmployee"
        Me.lbEmployee.Size = New System.Drawing.Size(60, 13)
        Me.lbEmployee.TabIndex = 1
        Me.lbEmployee.Text = "Сотрудник"
        '
        'cbEmployee
        '
        Me.cbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbEmployee.DataSource = Me.EmployeeBindingSource
        Me.cbEmployee.DisplayMember = "EmployeeName"
        Me.cbEmployee.FormattingEnabled = True
        Me.cbEmployee.Location = New System.Drawing.Point(121, 12)
        Me.cbEmployee.Name = "cbEmployee"
        Me.cbEmployee.Size = New System.Drawing.Size(188, 21)
        Me.cbEmployee.TabIndex = 0
        Me.cbEmployee.ValueMember = "EmployeeID"
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.DataSource = Me.OperationLogBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(107, 122)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(240, 150)
        Me.DataGridView1.TabIndex = 2
        '
        'cbOperation
        '
        Me.cbOperation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbOperation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbOperation.DataSource = Me.OperationBindingSource2
        Me.cbOperation.DisplayMember = "OperationName"
        Me.cbOperation.FormattingEnabled = True
        Me.cbOperation.Location = New System.Drawing.Point(402, 38)
        Me.cbOperation.Name = "cbOperation"
        Me.cbOperation.Size = New System.Drawing.Size(147, 21)
        Me.cbOperation.TabIndex = 3
        Me.cbOperation.ValueMember = "OperationID"
        '
        'lbOperation
        '
        Me.lbOperation.AutoSize = True
        Me.lbOperation.Location = New System.Drawing.Point(339, 39)
        Me.lbOperation.Name = "lbOperation"
        Me.lbOperation.Size = New System.Drawing.Size(57, 13)
        Me.lbOperation.TabIndex = 11
        Me.lbOperation.Text = "Операция"
        '
        'OperationOperationLogBindingSource
        '
        Me.OperationOperationLogBindingSource.DataMember = "OperationOperationLog"
        Me.OperationOperationLogBindingSource.DataSource = Me.OperationBindingSource1
        '
        'btnInsert
        '
        Me.btnInsert.Location = New System.Drawing.Point(598, 67)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(75, 23)
        Me.btnInsert.TabIndex = 12
        Me.btnInsert.Text = "Вставить"
        Me.btnInsert.UseVisualStyleBackColor = True
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
        'ArticulBindingSource
        '
        Me.ArticulBindingSource.DataMember = "Articul"
        '
        'OperationLogBindingSource
        '
        Me.OperationLogBindingSource.DataMember = "OperationLog"
        '
        'OperationBindingSource
        '
        Me.OperationBindingSource.DataMember = "Operation"
        '
        'OperationBindingSource1
        '
        Me.OperationBindingSource1.DataMember = "Operation"
        '
        'newBill
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button3
        Me.ClientSize = New System.Drawing.Size(803, 453)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "newBill"
        Me.Text = "Новая Накладная"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationOperationLogBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationLogBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents OperationLogBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OperationLogTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.OperationLogTableAdapter
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
    Friend WithEvents cbOperation As System.Windows.Forms.ComboBox
    Friend WithEvents OperationBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents lbOperation As System.Windows.Forms.Label
    Friend WithEvents OperationBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents OperationOperationLogBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents btnInsert As System.Windows.Forms.Button
End Class
