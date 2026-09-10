<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TariffsCheckForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TariffsCheckForm))
        Me.lbSelectedArticuls = New System.Windows.Forms.ListBox()
        Me.OperationDBDataSet1 = New OperationDBDataSet1()
        Me.ArticulBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ArticulTableAdapter = New OperationDBDataSet1TableAdapters.ArticulTableAdapter()
        Me.btnCheckTariffs = New System.Windows.Forms.Button()
        Me.cbArticul = New System.Windows.Forms.ComboBox()
        Me.btnSelectArticul = New System.Windows.Forms.Button()
        Me.btnRemoveFromList = New System.Windows.Forms.Button()
        Me.chckbxSelectAll = New System.Windows.Forms.CheckBox()
        Me.btnClearList = New System.Windows.Forms.Button()
        Me.labelArticul = New System.Windows.Forms.Label()
        Me.labelSelectedArticuls = New System.Windows.Forms.Label()
        Me.OperationDBDataSet6 = New OperationDBDataSet6()
        Me.RoutingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RoutingTableAdapter = New OperationDBDataSet6TableAdapters.RoutingTableAdapter()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoutingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbSelectedArticuls
        '
        Me.lbSelectedArticuls.AllowDrop = True
        Me.lbSelectedArticuls.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbSelectedArticuls.Location = New System.Drawing.Point(196, 41)
        Me.lbSelectedArticuls.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.lbSelectedArticuls.Name = "lbSelectedArticuls"
        Me.lbSelectedArticuls.ScrollAlwaysVisible = True
        Me.lbSelectedArticuls.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbSelectedArticuls.Size = New System.Drawing.Size(200, 108)
        Me.lbSelectedArticuls.TabIndex = 0
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
        'ArticulTableAdapter
        '
        Me.ArticulTableAdapter.ClearBeforeFill = True
        '
        'btnCheckTariffs
        '
        Me.btnCheckTariffs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCheckTariffs.Location = New System.Drawing.Point(6, 202)
        Me.btnCheckTariffs.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnCheckTariffs.Name = "btnCheckTariffs"
        Me.btnCheckTariffs.Size = New System.Drawing.Size(388, 25)
        Me.btnCheckTariffs.TabIndex = 1
        Me.btnCheckTariffs.Text = "Проверить расценки"
        Me.btnCheckTariffs.UseVisualStyleBackColor = True
        '
        'cbArticul
        '
        Me.cbArticul.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbArticul.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbArticul.DataSource = Me.ArticulBindingSource
        Me.cbArticul.DisplayMember = "ArticulName"
        Me.cbArticul.FormattingEnabled = True
        Me.cbArticul.Location = New System.Drawing.Point(6, 41)
        Me.cbArticul.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cbArticul.Name = "cbArticul"
        Me.cbArticul.Size = New System.Drawing.Size(189, 21)
        Me.cbArticul.TabIndex = 2
        Me.cbArticul.ValueMember = "ArticulID"
        '
        'btnSelectArticul
        '
        Me.btnSelectArticul.Location = New System.Drawing.Point(6, 67)
        Me.btnSelectArticul.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSelectArticul.Name = "btnSelectArticul"
        Me.btnSelectArticul.Size = New System.Drawing.Size(187, 25)
        Me.btnSelectArticul.TabIndex = 3
        Me.btnSelectArticul.Text = "Выбрать"
        Me.btnSelectArticul.UseVisualStyleBackColor = True
        '
        'btnRemoveFromList
        '
        Me.btnRemoveFromList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveFromList.Location = New System.Drawing.Point(196, 151)
        Me.btnRemoveFromList.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnRemoveFromList.Name = "btnRemoveFromList"
        Me.btnRemoveFromList.Size = New System.Drawing.Size(198, 23)
        Me.btnRemoveFromList.TabIndex = 4
        Me.btnRemoveFromList.Text = "Удалить из списка"
        Me.btnRemoveFromList.UseVisualStyleBackColor = True
        '
        'chckbxSelectAll
        '
        Me.chckbxSelectAll.AutoSize = True
        Me.chckbxSelectAll.Location = New System.Drawing.Point(6, 101)
        Me.chckbxSelectAll.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.chckbxSelectAll.Name = "chckbxSelectAll"
        Me.chckbxSelectAll.Size = New System.Drawing.Size(91, 17)
        Me.chckbxSelectAll.TabIndex = 5
        Me.chckbxSelectAll.Text = "Выбрать все"
        Me.chckbxSelectAll.UseVisualStyleBackColor = True
        '
        'btnClearList
        '
        Me.btnClearList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearList.Location = New System.Drawing.Point(196, 176)
        Me.btnClearList.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnClearList.Name = "btnClearList"
        Me.btnClearList.Size = New System.Drawing.Size(198, 23)
        Me.btnClearList.TabIndex = 6
        Me.btnClearList.Text = "Очистить список"
        Me.btnClearList.UseVisualStyleBackColor = True
        '
        'labelArticul
        '
        Me.labelArticul.AutoSize = True
        Me.labelArticul.Location = New System.Drawing.Point(6, 20)
        Me.labelArticul.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelArticul.Name = "labelArticul"
        Me.labelArticul.Size = New System.Drawing.Size(51, 13)
        Me.labelArticul.TabIndex = 7
        Me.labelArticul.Text = "Артикул:"
        '
        'labelSelectedArticuls
        '
        Me.labelSelectedArticuls.AutoSize = True
        Me.labelSelectedArticuls.Location = New System.Drawing.Point(194, 20)
        Me.labelSelectedArticuls.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelSelectedArticuls.Name = "labelSelectedArticuls"
        Me.labelSelectedArticuls.Size = New System.Drawing.Size(120, 13)
        Me.labelSelectedArticuls.TabIndex = 8
        Me.labelSelectedArticuls.Text = "Выбранные артикулы:"
        '
        'OperationDBDataSet6
        '
        Me.OperationDBDataSet6.DataSetName = "OperationDBDataSet6"
        Me.OperationDBDataSet6.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RoutingBindingSource
        '
        Me.RoutingBindingSource.DataMember = "Routing"
        Me.RoutingBindingSource.DataSource = Me.OperationDBDataSet6
        '
        'RoutingTableAdapter
        '
        Me.RoutingTableAdapter.ClearBeforeFill = True
        '
        'TariffsCheckForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 234)
        Me.Controls.Add(Me.labelSelectedArticuls)
        Me.Controls.Add(Me.labelArticul)
        Me.Controls.Add(Me.btnClearList)
        Me.Controls.Add(Me.chckbxSelectAll)
        Me.Controls.Add(Me.btnRemoveFromList)
        Me.Controls.Add(Me.btnSelectArticul)
        Me.Controls.Add(Me.cbArticul)
        Me.Controls.Add(Me.btnCheckTariffs)
        Me.Controls.Add(Me.lbSelectedArticuls)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "TariffsCheckForm"
        Me.Text = "Проверка расценок"
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ArticulBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoutingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbSelectedArticuls As ListBox
    Friend WithEvents OperationDBDataSet1 As OperationDBDataSet1
    Friend WithEvents ArticulBindingSource As BindingSource
    Friend WithEvents ArticulTableAdapter As OperationDBDataSet1TableAdapters.ArticulTableAdapter
    Friend WithEvents btnCheckTariffs As Button
    Friend WithEvents cbArticul As ComboBox
    Friend WithEvents btnSelectArticul As Button
    Friend WithEvents btnRemoveFromList As Button
    Friend WithEvents chckbxSelectAll As CheckBox
    Friend WithEvents btnClearList As Button
    Friend WithEvents labelArticul As Label
    Friend WithEvents labelSelectedArticuls As Label
    Friend WithEvents OperationDBDataSet6 As OperationDBDataSet6
    Friend WithEvents RoutingBindingSource As BindingSource
    Friend WithEvents RoutingTableAdapter As OperationDBDataSet6TableAdapters.RoutingTableAdapter
End Class
