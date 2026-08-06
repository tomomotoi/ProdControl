<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmployeesForm
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
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.PositionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet4 = New ProdControl.OperationDBDataSet4()
        Me.chckbxAllowEdit = New System.Windows.Forms.CheckBox()
        Me.PositionTableAdapter = New ProdControl.OperationDBDataSet4TableAdapters.PositionTableAdapter()
        Me.btnSearchAll = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.cbFilter = New System.Windows.Forms.ComboBox()
        Me.labelFilter = New System.Windows.Forms.Label()
        Me.txSearchBox = New System.Windows.Forms.TextBox()
        Me.labelEmplName = New System.Windows.Forms.Label()
        Me.labelEmplNumber = New System.Windows.Forms.Label()
        Me.labelPosition = New System.Windows.Forms.Label()
        Me.labelPositionLevel = New System.Windows.Forms.Label()
        Me.cbSearchPosition = New System.Windows.Forms.ComboBox()
        Me.EmployeeTableAdapter1 = New ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter()
        Me.EmployeeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PositionName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.PositionLevel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isChanged = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isNew = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PositionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EmployeeId, Me.EmployeeName, Me.EmployeeNumber, Me.PositionName, Me.PositionLevel, Me.isChanged, Me.isNew})
        Me.dgView.Location = New System.Drawing.Point(1, 219)
        Me.dgView.Name = "dgView"
        Me.dgView.ReadOnly = True
        Me.dgView.RowHeadersWidth = 82
        Me.dgView.RowTemplate.Height = 33
        Me.dgView.Size = New System.Drawing.Size(1340, 626)
        Me.dgView.TabIndex = 0
        '
        'PositionBindingSource
        '
        Me.PositionBindingSource.DataMember = "Position"
        Me.PositionBindingSource.DataSource = Me.OperationDBDataSet4
        '
        'OperationDBDataSet4
        '
        Me.OperationDBDataSet4.DataSetName = "OperationDBDataSet4"
        Me.OperationDBDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'chckbxAllowEdit
        '
        Me.chckbxAllowEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chckbxAllowEdit.AutoSize = True
        Me.chckbxAllowEdit.Location = New System.Drawing.Point(12, 861)
        Me.chckbxAllowEdit.Name = "chckbxAllowEdit"
        Me.chckbxAllowEdit.Size = New System.Drawing.Size(281, 29)
        Me.chckbxAllowEdit.TabIndex = 1
        Me.chckbxAllowEdit.Text = "Режим редактирования"
        Me.chckbxAllowEdit.UseVisualStyleBackColor = True
        '
        'PositionTableAdapter
        '
        Me.PositionTableAdapter.ClearBeforeFill = True
        '
        'btnSearchAll
        '
        Me.btnSearchAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchAll.Location = New System.Drawing.Point(985, 163)
        Me.btnSearchAll.Name = "btnSearchAll"
        Me.btnSearchAll.Size = New System.Drawing.Size(169, 50)
        Me.btnSearchAll.TabIndex = 2
        Me.btnSearchAll.Text = "Найти всех"
        Me.btnSearchAll.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(1160, 163)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(169, 50)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "Найти"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Enabled = False
        Me.btnDelete.Location = New System.Drawing.Point(1160, 849)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(169, 50)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Удалить"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Enabled = False
        Me.btnAdd.Location = New System.Drawing.Point(985, 849)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(169, 50)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.Text = "Добавить"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(810, 849)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(169, 50)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cbFilter
        '
        Me.cbFilter.FormattingEnabled = True
        Me.cbFilter.Items.AddRange(New Object() {"По ФИО", "По номеру", "По должности", "По разряду"})
        Me.cbFilter.Location = New System.Drawing.Point(12, 35)
        Me.cbFilter.Name = "cbFilter"
        Me.cbFilter.Size = New System.Drawing.Size(304, 33)
        Me.cbFilter.TabIndex = 7
        '
        'labelFilter
        '
        Me.labelFilter.AutoSize = True
        Me.labelFilter.Location = New System.Drawing.Point(12, 7)
        Me.labelFilter.Name = "labelFilter"
        Me.labelFilter.Size = New System.Drawing.Size(93, 25)
        Me.labelFilter.TabIndex = 8
        Me.labelFilter.Text = "Фильтр:"
        '
        'txSearchBox
        '
        Me.txSearchBox.Location = New System.Drawing.Point(12, 117)
        Me.txSearchBox.Name = "txSearchBox"
        Me.txSearchBox.Size = New System.Drawing.Size(304, 31)
        Me.txSearchBox.TabIndex = 9
        Me.txSearchBox.Visible = False
        '
        'labelEmplName
        '
        Me.labelEmplName.AutoSize = True
        Me.labelEmplName.Location = New System.Drawing.Point(12, 89)
        Me.labelEmplName.Name = "labelEmplName"
        Me.labelEmplName.Size = New System.Drawing.Size(67, 25)
        Me.labelEmplName.TabIndex = 10
        Me.labelEmplName.Text = "ФИО:"
        Me.labelEmplName.Visible = False
        '
        'labelEmplNumber
        '
        Me.labelEmplNumber.AutoSize = True
        Me.labelEmplNumber.Location = New System.Drawing.Point(12, 89)
        Me.labelEmplNumber.Name = "labelEmplNumber"
        Me.labelEmplNumber.Size = New System.Drawing.Size(160, 25)
        Me.labelEmplNumber.TabIndex = 11
        Me.labelEmplNumber.Text = "№ сотрудника:"
        Me.labelEmplNumber.Visible = False
        '
        'labelPosition
        '
        Me.labelPosition.AutoSize = True
        Me.labelPosition.Location = New System.Drawing.Point(12, 89)
        Me.labelPosition.Name = "labelPosition"
        Me.labelPosition.Size = New System.Drawing.Size(129, 25)
        Me.labelPosition.TabIndex = 12
        Me.labelPosition.Text = "Должность:"
        Me.labelPosition.Visible = False
        '
        'labelPositionLevel
        '
        Me.labelPositionLevel.AutoSize = True
        Me.labelPositionLevel.Location = New System.Drawing.Point(15, 89)
        Me.labelPositionLevel.Name = "labelPositionLevel"
        Me.labelPositionLevel.Size = New System.Drawing.Size(90, 25)
        Me.labelPositionLevel.TabIndex = 13
        Me.labelPositionLevel.Text = "Разряд:"
        Me.labelPositionLevel.Visible = False
        '
        'cbSearchPosition
        '
        Me.cbSearchPosition.DataSource = Me.PositionBindingSource
        Me.cbSearchPosition.DisplayMember = "PositionName"
        Me.cbSearchPosition.FormattingEnabled = True
        Me.cbSearchPosition.Location = New System.Drawing.Point(12, 117)
        Me.cbSearchPosition.Name = "cbSearchPosition"
        Me.cbSearchPosition.Size = New System.Drawing.Size(304, 33)
        Me.cbSearchPosition.TabIndex = 14
        Me.cbSearchPosition.ValueMember = "PositionID"
        Me.cbSearchPosition.Visible = False
        '
        'EmployeeTableAdapter1
        '
        Me.EmployeeTableAdapter1.ClearBeforeFill = True
        '
        'EmployeeId
        '
        Me.EmployeeId.HeaderText = "EmployeeId"
        Me.EmployeeId.MinimumWidth = 10
        Me.EmployeeId.Name = "EmployeeId"
        Me.EmployeeId.ReadOnly = True
        Me.EmployeeId.Visible = False
        Me.EmployeeId.Width = 200
        '
        'EmployeeName
        '
        Me.EmployeeName.HeaderText = "ФИО"
        Me.EmployeeName.MinimumWidth = 10
        Me.EmployeeName.Name = "EmployeeName"
        Me.EmployeeName.ReadOnly = True
        Me.EmployeeName.Width = 200
        '
        'EmployeeNumber
        '
        Me.EmployeeNumber.HeaderText = "№ сотрудника"
        Me.EmployeeNumber.MinimumWidth = 10
        Me.EmployeeNumber.Name = "EmployeeNumber"
        Me.EmployeeNumber.ReadOnly = True
        Me.EmployeeNumber.Width = 200
        '
        'PositionName
        '
        Me.PositionName.DataPropertyName = "PositionName"
        Me.PositionName.DataSource = Me.PositionBindingSource
        Me.PositionName.DisplayMember = "PositionName"
        Me.PositionName.HeaderText = "Должность"
        Me.PositionName.MinimumWidth = 10
        Me.PositionName.Name = "PositionName"
        Me.PositionName.ReadOnly = True
        Me.PositionName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PositionName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.PositionName.ValueMember = "PositionID"
        Me.PositionName.Width = 200
        '
        'PositionLevel
        '
        Me.PositionLevel.HeaderText = "Разряд должности"
        Me.PositionLevel.MinimumWidth = 10
        Me.PositionLevel.Name = "PositionLevel"
        Me.PositionLevel.ReadOnly = True
        Me.PositionLevel.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PositionLevel.Width = 200
        '
        'isChanged
        '
        Me.isChanged.HeaderText = "isChanged"
        Me.isChanged.MinimumWidth = 10
        Me.isChanged.Name = "isChanged"
        Me.isChanged.ReadOnly = True
        Me.isChanged.Visible = False
        Me.isChanged.Width = 200
        '
        'isNew
        '
        Me.isNew.HeaderText = "isNew"
        Me.isNew.MinimumWidth = 10
        Me.isNew.Name = "isNew"
        Me.isNew.ReadOnly = True
        Me.isNew.Visible = False
        Me.isNew.Width = 200
        '
        'EmployeesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1341, 902)
        Me.Controls.Add(Me.cbSearchPosition)
        Me.Controls.Add(Me.labelPositionLevel)
        Me.Controls.Add(Me.labelPosition)
        Me.Controls.Add(Me.labelEmplNumber)
        Me.Controls.Add(Me.labelEmplName)
        Me.Controls.Add(Me.txSearchBox)
        Me.Controls.Add(Me.labelFilter)
        Me.Controls.Add(Me.cbFilter)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnSearchAll)
        Me.Controls.Add(Me.chckbxAllowEdit)
        Me.Controls.Add(Me.dgView)
        Me.Name = "EmployeesForm"
        Me.Text = "Сотрудники"
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PositionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgView As DataGridView
    Friend WithEvents chckbxAllowEdit As CheckBox
    Friend WithEvents OperationDBDataSet4 As OperationDBDataSet4
    Friend WithEvents PositionBindingSource As BindingSource
    Friend WithEvents PositionTableAdapter As OperationDBDataSet4TableAdapters.PositionTableAdapter
    Friend WithEvents btnSearchAll As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents cbFilter As ComboBox
    Friend WithEvents labelFilter As Label
    Friend WithEvents txSearchBox As TextBox
    Friend WithEvents labelEmplName As Label
    Friend WithEvents labelEmplNumber As Label
    Friend WithEvents labelPosition As Label
    Friend WithEvents labelPositionLevel As Label
    Friend WithEvents cbSearchPosition As ComboBox
    Friend WithEvents EmployeeTableAdapter1 As OperationDBDataSet1TableAdapters.EmployeeTableAdapter
    Friend WithEvents EmployeeId As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeName As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeNumber As DataGridViewTextBoxColumn
    Friend WithEvents PositionName As DataGridViewComboBoxColumn
    Friend WithEvents PositionLevel As DataGridViewTextBoxColumn
    Friend WithEvents isChanged As DataGridViewTextBoxColumn
    Friend WithEvents isNew As DataGridViewTextBoxColumn
End Class
