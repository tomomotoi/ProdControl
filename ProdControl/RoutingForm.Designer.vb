<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RoutingForm
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
        Me.OperationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet1 = New ProdControl.OperationDBDataSet1()
        Me.ArticulBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet = New ProdControl.OperationDBDataSet()
        Me.ArticulTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.ArticulTableAdapter()
        Me.OperationTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.OperationTableAdapter()
        Me.spContainer = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbRowsAmount = New System.Windows.Forms.Label()
        Me.txCost = New System.Windows.Forms.TextBox()
        Me.labelCost = New System.Windows.Forms.Label()
        Me.cbDopCost = New System.Windows.Forms.ComboBox()
        Me.CoefficientsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet3 = New ProdControl.OperationDBDataSet3()
        Me.btnSeachArt = New System.Windows.Forms.Button()
        Me.lbLevel = New System.Windows.Forms.Label()
        Me.lbTimeRate = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbDocCost = New System.Windows.Forms.Label()
        Me.lbOperation = New System.Windows.Forms.Label()
        Me.lbArticul = New System.Windows.Forms.Label()
        Me.btCancel = New System.Windows.Forms.Button()
        Me.btSave = New System.Windows.Forms.Button()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.cbLevel = New System.Windows.Forms.ComboBox()
        Me.txRate = New System.Windows.Forms.TextBox()
        Me.txExecTime = New System.Windows.Forms.TextBox()
        Me.cbOperation = New System.Windows.Forms.ComboBox()
        Me.cbArt = New System.Windows.Forms.ComboBox()
        Me.dgView = New System.Windows.Forms.DataGridView()
        Me.routingID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArtID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Operation = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DopCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExecTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RateExec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Level = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isNew = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.isChanged = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PositionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet4 = New ProdControl.OperationDBDataSet4()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet2 = New ProdControl.OperationDBDataSet2()
        Me.RoutingTableAdapter = New ProdControl.OperationDBDataSet2TableAdapters.RoutingTableAdapter()
        Me.CoefficientsTableAdapter = New ProdControl.OperationDBDataSet3TableAdapters.CoefficientsTableAdapter()
        Me.PositionTableAdapter = New ProdControl.OperationDBDataSet4TableAdapters.PositionTableAdapter()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spContainer.Panel1.SuspendLayout()
        Me.spContainer.Panel2.SuspendLayout()
        Me.spContainer.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.CoefficientsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PositionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OperationBindingSource
        '
        Me.OperationBindingSource.DataMember = "Operation"
        Me.OperationBindingSource.DataSource = Me.OperationDBDataSet1
        '
        'OperationDBDataSet1
        '
        Me.OperationDBDataSet1.DataSetName = "OperationDBDataSet1"
        Me.OperationDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ArticulBindingSource
        '
        Me.ArticulBindingSource.DataMember = "Articul"
        Me.ArticulBindingSource.DataSource = Me.OperationDBDataSet1
        '
        'OperationDBDataSet
        '
        Me.OperationDBDataSet.DataSetName = "OperationDBDataSet"
        Me.OperationDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ArticulTableAdapter
        '
        Me.ArticulTableAdapter.ClearBeforeFill = True
        '
        'OperationTableAdapter
        '
        Me.OperationTableAdapter.ClearBeforeFill = True
        '
        'spContainer
        '
        Me.spContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spContainer.Location = New System.Drawing.Point(0, 0)
        Me.spContainer.Margin = New System.Windows.Forms.Padding(6)
        Me.spContainer.Name = "spContainer"
        Me.spContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'spContainer.Panel1
        '
        Me.spContainer.Panel1.Controls.Add(Me.Panel1)
        '
        'spContainer.Panel2
        '
        Me.spContainer.Panel2.Controls.Add(Me.dgView)
        Me.spContainer.Size = New System.Drawing.Size(1244, 804)
        Me.spContainer.SplitterDistance = 323
        Me.spContainer.SplitterWidth = 15
        Me.spContainer.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.lbRowsAmount)
        Me.Panel1.Controls.Add(Me.txCost)
        Me.Panel1.Controls.Add(Me.labelCost)
        Me.Panel1.Controls.Add(Me.cbDopCost)
        Me.Panel1.Controls.Add(Me.btnSeachArt)
        Me.Panel1.Controls.Add(Me.lbLevel)
        Me.Panel1.Controls.Add(Me.lbTimeRate)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lbDocCost)
        Me.Panel1.Controls.Add(Me.lbOperation)
        Me.Panel1.Controls.Add(Me.lbArticul)
        Me.Panel1.Controls.Add(Me.btCancel)
        Me.Panel1.Controls.Add(Me.btSave)
        Me.Panel1.Controls.Add(Me.btnDel)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.cbLevel)
        Me.Panel1.Controls.Add(Me.txRate)
        Me.Panel1.Controls.Add(Me.txExecTime)
        Me.Panel1.Controls.Add(Me.cbOperation)
        Me.Panel1.Controls.Add(Me.cbArt)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1244, 323)
        Me.Panel1.TabIndex = 1
        '
        'lbRowsAmount
        '
        Me.lbRowsAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbRowsAmount.AutoSize = True
        Me.lbRowsAmount.Location = New System.Drawing.Point(21, 292)
        Me.lbRowsAmount.Name = "lbRowsAmount"
        Me.lbRowsAmount.Size = New System.Drawing.Size(241, 25)
        Me.lbRowsAmount.TabIndex = 22
        Me.lbRowsAmount.Text = "Количество записей: 0"
        '
        'txCost
        '
        Me.txCost.Enabled = False
        Me.txCost.Location = New System.Drawing.Point(26, 228)
        Me.txCost.Margin = New System.Windows.Forms.Padding(4)
        Me.txCost.Name = "txCost"
        Me.txCost.Size = New System.Drawing.Size(258, 31)
        Me.txCost.TabIndex = 21
        '
        'labelCost
        '
        Me.labelCost.AutoSize = True
        Me.labelCost.Location = New System.Drawing.Point(20, 193)
        Me.labelCost.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.labelCost.Name = "labelCost"
        Me.labelCost.Size = New System.Drawing.Size(108, 25)
        Me.labelCost.TabIndex = 20
        Me.labelCost.Text = "Расценка"
        '
        'cbDopCost
        '
        Me.cbDopCost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbDopCost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbDopCost.DataSource = Me.CoefficientsBindingSource
        Me.cbDopCost.DisplayMember = "coefficient_value"
        Me.cbDopCost.FormattingEnabled = True
        Me.cbDopCost.Location = New System.Drawing.Point(300, 137)
        Me.cbDopCost.Margin = New System.Windows.Forms.Padding(6)
        Me.cbDopCost.Name = "cbDopCost"
        Me.cbDopCost.Size = New System.Drawing.Size(216, 33)
        Me.cbDopCost.TabIndex = 17
        Me.cbDopCost.ValueMember = "coefficient_id"
        '
        'CoefficientsBindingSource
        '
        Me.CoefficientsBindingSource.DataMember = "Coefficients"
        Me.CoefficientsBindingSource.DataSource = Me.OperationDBDataSet3
        '
        'OperationDBDataSet3
        '
        Me.OperationDBDataSet3.DataSetName = "OperationDBDataSet3"
        Me.OperationDBDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnSeachArt
        '
        Me.btnSeachArt.Location = New System.Drawing.Point(300, 48)
        Me.btnSeachArt.Margin = New System.Windows.Forms.Padding(6)
        Me.btnSeachArt.Name = "btnSeachArt"
        Me.btnSeachArt.Size = New System.Drawing.Size(150, 44)
        Me.btnSeachArt.TabIndex = 16
        Me.btnSeachArt.Text = "Найти"
        Me.btnSeachArt.UseVisualStyleBackColor = True
        '
        'lbLevel
        '
        Me.lbLevel.AutoSize = True
        Me.lbLevel.Location = New System.Drawing.Point(745, 106)
        Me.lbLevel.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lbLevel.Name = "lbLevel"
        Me.lbLevel.Size = New System.Drawing.Size(84, 25)
        Me.lbLevel.TabIndex = 15
        Me.lbLevel.Text = "Разряд"
        '
        'lbTimeRate
        '
        Me.lbTimeRate.AutoSize = True
        Me.lbTimeRate.Location = New System.Drawing.Point(531, 106)
        Me.lbTimeRate.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lbTimeRate.Name = "lbTimeRate"
        Me.lbTimeRate.Size = New System.Drawing.Size(170, 25)
        Me.lbTimeRate.TabIndex = 14
        Me.lbTimeRate.Text = "Норма времени"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1007, 106)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(203, 25)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Время выполнения"
        '
        'lbDocCost
        '
        Me.lbDocCost.AutoSize = True
        Me.lbDocCost.Location = New System.Drawing.Point(295, 106)
        Me.lbDocCost.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lbDocCost.Name = "lbDocCost"
        Me.lbDocCost.Size = New System.Drawing.Size(146, 25)
        Me.lbDocCost.TabIndex = 12
        Me.lbDocCost.Text = "Доп. затраты"
        '
        'lbOperation
        '
        Me.lbOperation.AutoSize = True
        Me.lbOperation.Location = New System.Drawing.Point(20, 106)
        Me.lbOperation.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lbOperation.Name = "lbOperation"
        Me.lbOperation.Size = New System.Drawing.Size(111, 25)
        Me.lbOperation.TabIndex = 11
        Me.lbOperation.Text = "Операция"
        '
        'lbArticul
        '
        Me.lbArticul.AutoSize = True
        Me.lbArticul.Location = New System.Drawing.Point(21, 19)
        Me.lbArticul.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lbArticul.Name = "lbArticul"
        Me.lbArticul.Size = New System.Drawing.Size(94, 25)
        Me.lbArticul.TabIndex = 10
        Me.lbArticul.Text = "Артикул"
        '
        'btCancel
        '
        Me.btCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btCancel.Location = New System.Drawing.Point(1078, 273)
        Me.btCancel.Margin = New System.Windows.Forms.Padding(6)
        Me.btCancel.Name = "btCancel"
        Me.btCancel.Size = New System.Drawing.Size(150, 44)
        Me.btCancel.TabIndex = 9
        Me.btCancel.Text = "Отменить"
        Me.btCancel.UseVisualStyleBackColor = True
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btSave.Location = New System.Drawing.Point(914, 273)
        Me.btSave.Margin = New System.Windows.Forms.Padding(6)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(150, 44)
        Me.btSave.TabIndex = 8
        Me.btSave.Text = "Сохранить"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDel.Location = New System.Drawing.Point(750, 273)
        Me.btnDel.Margin = New System.Windows.Forms.Padding(6)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(150, 44)
        Me.btnDel.TabIndex = 7
        Me.btnDel.Text = "Удалить"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(586, 273)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(6)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(150, 44)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Добавить"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'cbLevel
        '
        Me.cbLevel.FormattingEnabled = True
        Me.cbLevel.Items.AddRange(New Object() {"", "1", "2", "3", "4", "5"})
        Me.cbLevel.Location = New System.Drawing.Point(750, 137)
        Me.cbLevel.Margin = New System.Windows.Forms.Padding(6)
        Me.cbLevel.Name = "cbLevel"
        Me.cbLevel.Size = New System.Drawing.Size(238, 33)
        Me.cbLevel.TabIndex = 5
        '
        'txRate
        '
        Me.txRate.Location = New System.Drawing.Point(536, 139)
        Me.txRate.Margin = New System.Windows.Forms.Padding(6)
        Me.txRate.Name = "txRate"
        Me.txRate.Size = New System.Drawing.Size(196, 31)
        Me.txRate.TabIndex = 4
        '
        'txExecTime
        '
        Me.txExecTime.Enabled = False
        Me.txExecTime.Location = New System.Drawing.Point(1012, 137)
        Me.txExecTime.Margin = New System.Windows.Forms.Padding(6)
        Me.txExecTime.Name = "txExecTime"
        Me.txExecTime.Size = New System.Drawing.Size(216, 31)
        Me.txExecTime.TabIndex = 3
        '
        'cbOperation
        '
        Me.cbOperation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbOperation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbOperation.DataSource = Me.OperationBindingSource
        Me.cbOperation.DisplayMember = "OperationName"
        Me.cbOperation.FormattingEnabled = True
        Me.cbOperation.Location = New System.Drawing.Point(26, 137)
        Me.cbOperation.Margin = New System.Windows.Forms.Padding(6)
        Me.cbOperation.Name = "cbOperation"
        Me.cbOperation.Size = New System.Drawing.Size(258, 33)
        Me.cbOperation.TabIndex = 1
        Me.cbOperation.ValueMember = "OperationID"
        '
        'cbArt
        '
        Me.cbArt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArt.DataSource = Me.ArticulBindingSource
        Me.cbArt.DisplayMember = "ArticulName"
        Me.cbArt.FormattingEnabled = True
        Me.cbArt.Location = New System.Drawing.Point(26, 50)
        Me.cbArt.Margin = New System.Windows.Forms.Padding(6)
        Me.cbArt.Name = "cbArt"
        Me.cbArt.Size = New System.Drawing.Size(258, 33)
        Me.cbArt.TabIndex = 0
        Me.cbArt.ValueMember = "ArticulID"
        '
        'dgView
        '
        Me.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.routingID, Me.ArtID, Me.Operation, Me.DopCost, Me.ExecTime, Me.RateExec, Me.Level, Me.Cost, Me.isNew, Me.isChanged})
        Me.dgView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgView.Location = New System.Drawing.Point(0, 0)
        Me.dgView.Margin = New System.Windows.Forms.Padding(6)
        Me.dgView.Name = "dgView"
        Me.dgView.RowHeadersWidth = 82
        Me.dgView.Size = New System.Drawing.Size(1244, 466)
        Me.dgView.TabIndex = 2
        '
        'routingID
        '
        Me.routingID.HeaderText = "RoutingID"
        Me.routingID.MinimumWidth = 10
        Me.routingID.Name = "routingID"
        Me.routingID.ReadOnly = True
        Me.routingID.Visible = False
        Me.routingID.Width = 200
        '
        'ArtID
        '
        Me.ArtID.HeaderText = "ArticulID"
        Me.ArtID.MinimumWidth = 10
        Me.ArtID.Name = "ArtID"
        Me.ArtID.Visible = False
        Me.ArtID.Width = 200
        '
        'Operation
        '
        Me.Operation.DataSource = Me.OperationBindingSource
        Me.Operation.DisplayMember = "OperationName"
        Me.Operation.HeaderText = "Операция"
        Me.Operation.MinimumWidth = 10
        Me.Operation.Name = "Operation"
        Me.Operation.ReadOnly = True
        Me.Operation.ValueMember = "OperationID"
        Me.Operation.Width = 150
        '
        'DopCost
        '
        Me.DopCost.HeaderText = "Коэф. уч. доп затра"
        Me.DopCost.MinimumWidth = 10
        Me.DopCost.Name = "DopCost"
        Me.DopCost.Width = 50
        '
        'ExecTime
        '
        Me.ExecTime.HeaderText = "Время с уч.коэф"
        Me.ExecTime.MinimumWidth = 10
        Me.ExecTime.Name = "ExecTime"
        Me.ExecTime.Width = 50
        '
        'RateExec
        '
        Me.RateExec.HeaderText = "Норма выработки"
        Me.RateExec.MinimumWidth = 10
        Me.RateExec.Name = "RateExec"
        Me.RateExec.Width = 50
        '
        'Level
        '
        Me.Level.HeaderText = "Разряд"
        Me.Level.MinimumWidth = 10
        Me.Level.Name = "Level"
        Me.Level.Width = 50
        '
        'Cost
        '
        Me.Cost.HeaderText = "Расценка"
        Me.Cost.MinimumWidth = 10
        Me.Cost.Name = "Cost"
        Me.Cost.Width = 120
        '
        'isNew
        '
        Me.isNew.HeaderText = "Is New"
        Me.isNew.MinimumWidth = 10
        Me.isNew.Name = "isNew"
        Me.isNew.ReadOnly = True
        Me.isNew.Visible = False
        Me.isNew.Width = 200
        '
        'isChanged
        '
        Me.isChanged.HeaderText = "IsChanged"
        Me.isChanged.MinimumWidth = 10
        Me.isChanged.Name = "isChanged"
        Me.isChanged.Visible = False
        Me.isChanged.Width = 200
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
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "Routing"
        Me.BindingSource1.DataSource = Me.OperationDBDataSet2
        '
        'OperationDBDataSet2
        '
        Me.OperationDBDataSet2.DataSetName = "OperationDBDataSet2"
        Me.OperationDBDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RoutingTableAdapter
        '
        Me.RoutingTableAdapter.ClearBeforeFill = True
        '
        'CoefficientsTableAdapter
        '
        Me.CoefficientsTableAdapter.ClearBeforeFill = True
        '
        'PositionTableAdapter
        '
        Me.PositionTableAdapter.ClearBeforeFill = True
        '
        'RoutingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1244, 804)
        Me.Controls.Add(Me.spContainer)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "RoutingForm"
        Me.Text = "Тех. карта"
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spContainer.Panel1.ResumeLayout(False)
        Me.spContainer.Panel2.ResumeLayout(False)
        CType(Me.spContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spContainer.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.CoefficientsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PositionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OperationDBDataSet As ProdControl.OperationDBDataSet
    Friend WithEvents OperationDBDataSet1 As ProdControl.OperationDBDataSet1
    Friend WithEvents ArticulBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ArticulTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.ArticulTableAdapter
    Friend WithEvents OperationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OperationTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.OperationTableAdapter
    Friend WithEvents spContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btCancel As System.Windows.Forms.Button
    Friend WithEvents btSave As System.Windows.Forms.Button
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents cbLevel As System.Windows.Forms.ComboBox
    Friend WithEvents txRate As System.Windows.Forms.TextBox
    Friend WithEvents txExecTime As System.Windows.Forms.TextBox
    Friend WithEvents txDopCost As System.Windows.Forms.TextBox
    Friend WithEvents cbOperation As System.Windows.Forms.ComboBox
    Friend WithEvents cbArt As System.Windows.Forms.ComboBox
    Friend WithEvents dgView As System.Windows.Forms.DataGridView
    Friend WithEvents lbLevel As System.Windows.Forms.Label
    Friend WithEvents lbTimeRate As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbDocCost As System.Windows.Forms.Label
    Friend WithEvents lbOperation As System.Windows.Forms.Label
    Friend WithEvents lbArticul As System.Windows.Forms.Label
    Friend WithEvents btnSeachArt As System.Windows.Forms.Button
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents OperationDBDataSet2 As ProdControl.OperationDBDataSet2
    Friend WithEvents RoutingTableAdapter As ProdControl.OperationDBDataSet2TableAdapters.RoutingTableAdapter
    Friend WithEvents cbDopCost As ComboBox
    Friend WithEvents OperationDBDataSet3 As OperationDBDataSet3
    Friend WithEvents CoefficientsBindingSource As BindingSource
    Friend WithEvents CoefficientsTableAdapter As OperationDBDataSet3TableAdapters.CoefficientsTableAdapter
    Friend WithEvents OperationDBDataSet4 As OperationDBDataSet4
    Friend WithEvents PositionBindingSource As BindingSource
    Friend WithEvents PositionTableAdapter As OperationDBDataSet4TableAdapters.PositionTableAdapter
    Friend WithEvents txCost As TextBox
    Friend WithEvents labelCost As Label
    Friend WithEvents routingID As DataGridViewTextBoxColumn
    Friend WithEvents ArtID As DataGridViewTextBoxColumn
    Friend WithEvents Operation As DataGridViewComboBoxColumn
    Friend WithEvents DopCost As DataGridViewTextBoxColumn
    Friend WithEvents ExecTime As DataGridViewTextBoxColumn
    Friend WithEvents RateExec As DataGridViewTextBoxColumn
    Friend WithEvents Level As DataGridViewTextBoxColumn
    Friend WithEvents Cost As DataGridViewTextBoxColumn
    Friend WithEvents isNew As DataGridViewCheckBoxColumn
    Friend WithEvents isChanged As DataGridViewCheckBoxColumn
    Friend WithEvents lbRowsAmount As Label
End Class
