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
        Me.lbSelectedArticuls = New System.Windows.Forms.ListBox()
        Me.OperationDBDataSet1 = New ProdControl.OperationDBDataSet1()
        Me.ArticulBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ArticulTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.ArticulTableAdapter()
        Me.btnCheckTariffs = New System.Windows.Forms.Button()
        Me.cbArticul = New System.Windows.Forms.ComboBox()
        Me.btnSelectArticul = New System.Windows.Forms.Button()
        Me.btnRemoveFromList = New System.Windows.Forms.Button()
        Me.chckbxSelectAll = New System.Windows.Forms.CheckBox()
        Me.btnClearList = New System.Windows.Forms.Button()
        Me.labelArticul = New System.Windows.Forms.Label()
        Me.labelSelectedArticuls = New System.Windows.Forms.Label()
        Me.OperationDBDataSet6 = New ProdControl.OperationDBDataSet6()
        Me.RoutingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RoutingTableAdapter = New ProdControl.OperationDBDataSet6TableAdapters.RoutingTableAdapter()
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
        Me.lbSelectedArticuls.ItemHeight = 25
        Me.lbSelectedArticuls.Location = New System.Drawing.Point(392, 79)
        Me.lbSelectedArticuls.Name = "lbSelectedArticuls"
        Me.lbSelectedArticuls.ScrollAlwaysVisible = True
        Me.lbSelectedArticuls.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbSelectedArticuls.Size = New System.Drawing.Size(396, 204)
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
        Me.btnCheckTariffs.Location = New System.Drawing.Point(12, 389)
        Me.btnCheckTariffs.Name = "btnCheckTariffs"
        Me.btnCheckTariffs.Size = New System.Drawing.Size(776, 49)
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
        Me.cbArticul.Location = New System.Drawing.Point(12, 79)
        Me.cbArticul.Name = "cbArticul"
        Me.cbArticul.Size = New System.Drawing.Size(374, 33)
        Me.cbArticul.TabIndex = 2
        Me.cbArticul.ValueMember = "ArticulID"
        '
        'btnSelectArticul
        '
        Me.btnSelectArticul.Location = New System.Drawing.Point(12, 128)
        Me.btnSelectArticul.Name = "btnSelectArticul"
        Me.btnSelectArticul.Size = New System.Drawing.Size(374, 48)
        Me.btnSelectArticul.TabIndex = 3
        Me.btnSelectArticul.Text = "Выбрать"
        Me.btnSelectArticul.UseVisualStyleBackColor = True
        '
        'btnRemoveFromList
        '
        Me.btnRemoveFromList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveFromList.Location = New System.Drawing.Point(392, 290)
        Me.btnRemoveFromList.Name = "btnRemoveFromList"
        Me.btnRemoveFromList.Size = New System.Drawing.Size(396, 44)
        Me.btnRemoveFromList.TabIndex = 4
        Me.btnRemoveFromList.Text = "Удалить из списка"
        Me.btnRemoveFromList.UseVisualStyleBackColor = True
        '
        'chckbxSelectAll
        '
        Me.chckbxSelectAll.AutoSize = True
        Me.chckbxSelectAll.Location = New System.Drawing.Point(12, 194)
        Me.chckbxSelectAll.Name = "chckbxSelectAll"
        Me.chckbxSelectAll.Size = New System.Drawing.Size(170, 29)
        Me.chckbxSelectAll.TabIndex = 5
        Me.chckbxSelectAll.Text = "Выбрать все"
        Me.chckbxSelectAll.UseVisualStyleBackColor = True
        '
        'btnClearList
        '
        Me.btnClearList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearList.Location = New System.Drawing.Point(392, 339)
        Me.btnClearList.Name = "btnClearList"
        Me.btnClearList.Size = New System.Drawing.Size(396, 44)
        Me.btnClearList.TabIndex = 6
        Me.btnClearList.Text = "Очистить список"
        Me.btnClearList.UseVisualStyleBackColor = True
        '
        'labelArticul
        '
        Me.labelArticul.AutoSize = True
        Me.labelArticul.Location = New System.Drawing.Point(12, 39)
        Me.labelArticul.Name = "labelArticul"
        Me.labelArticul.Size = New System.Drawing.Size(100, 25)
        Me.labelArticul.TabIndex = 7
        Me.labelArticul.Text = "Артикул:"
        '
        'labelSelectedArticuls
        '
        Me.labelSelectedArticuls.AutoSize = True
        Me.labelSelectedArticuls.Location = New System.Drawing.Point(387, 39)
        Me.labelSelectedArticuls.Name = "labelSelectedArticuls"
        Me.labelSelectedArticuls.Size = New System.Drawing.Size(235, 25)
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.labelSelectedArticuls)
        Me.Controls.Add(Me.labelArticul)
        Me.Controls.Add(Me.btnClearList)
        Me.Controls.Add(Me.chckbxSelectAll)
        Me.Controls.Add(Me.btnRemoveFromList)
        Me.Controls.Add(Me.btnSelectArticul)
        Me.Controls.Add(Me.cbArticul)
        Me.Controls.Add(Me.btnCheckTariffs)
        Me.Controls.Add(Me.lbSelectedArticuls)
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
