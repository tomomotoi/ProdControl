<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OperationForm
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
        Me.txNewOperationName = New System.Windows.Forms.TextBox()
        Me.cbProfession = New System.Windows.Forms.ComboBox()
        Me.PositionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet4 = New ProdControl.OperationDBDataSet4()
        Me.btnAddOperation = New System.Windows.Forms.Button()
        Me.labelOperationName = New System.Windows.Forms.Label()
        Me.labelProfession = New System.Windows.Forms.Label()
        Me.PositionTableAdapter = New ProdControl.OperationDBDataSet4TableAdapters.PositionTableAdapter()
        Me.lbExistingOperations = New System.Windows.Forms.ListBox()
        Me.OperationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet1 = New ProdControl.OperationDBDataSet1()
        Me.labelExistingOperations = New System.Windows.Forms.Label()
        Me.OperationTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.OperationTableAdapter()
        Me.btnDeleteOperation = New System.Windows.Forms.Button()
        Me.labelSearchOperation = New System.Windows.Forms.Label()
        Me.txSearchOperation = New System.Windows.Forms.TextBox()
        Me.labelExistingOperationProfession = New System.Windows.Forms.Label()
        Me.txExistingOperationProfession = New System.Windows.Forms.TextBox()
        CType(Me.PositionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txNewOperationName
        '
        Me.txNewOperationName.Location = New System.Drawing.Point(12, 63)
        Me.txNewOperationName.Name = "txNewOperationName"
        Me.txNewOperationName.Size = New System.Drawing.Size(381, 31)
        Me.txNewOperationName.TabIndex = 0
        '
        'cbProfession
        '
        Me.cbProfession.DataSource = Me.PositionBindingSource
        Me.cbProfession.DisplayMember = "PositionName"
        Me.cbProfession.FormattingEnabled = True
        Me.cbProfession.Location = New System.Drawing.Point(428, 63)
        Me.cbProfession.Name = "cbProfession"
        Me.cbProfession.Size = New System.Drawing.Size(359, 33)
        Me.cbProfession.TabIndex = 1
        Me.cbProfession.ValueMember = "PositionID"
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
        'btnAddOperation
        '
        Me.btnAddOperation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddOperation.Location = New System.Drawing.Point(10, 808)
        Me.btnAddOperation.Name = "btnAddOperation"
        Me.btnAddOperation.Size = New System.Drawing.Size(775, 46)
        Me.btnAddOperation.TabIndex = 2
        Me.btnAddOperation.Text = "Добавить"
        Me.btnAddOperation.UseVisualStyleBackColor = True
        '
        'labelOperationName
        '
        Me.labelOperationName.AutoSize = True
        Me.labelOperationName.Location = New System.Drawing.Point(7, 21)
        Me.labelOperationName.Name = "labelOperationName"
        Me.labelOperationName.Size = New System.Drawing.Size(282, 25)
        Me.labelOperationName.TabIndex = 3
        Me.labelOperationName.Text = "Название новой операции:"
        '
        'labelProfession
        '
        Me.labelProfession.AutoSize = True
        Me.labelProfession.Location = New System.Drawing.Point(423, 21)
        Me.labelProfession.Name = "labelProfession"
        Me.labelProfession.Size = New System.Drawing.Size(131, 25)
        Me.labelProfession.TabIndex = 4
        Me.labelProfession.Text = "Профессия:"
        '
        'PositionTableAdapter
        '
        Me.PositionTableAdapter.ClearBeforeFill = True
        '
        'lbExistingOperations
        '
        Me.lbExistingOperations.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbExistingOperations.DataSource = Me.OperationBindingSource
        Me.lbExistingOperations.DisplayMember = "OperationName"
        Me.lbExistingOperations.FormattingEnabled = True
        Me.lbExistingOperations.HorizontalScrollbar = True
        Me.lbExistingOperations.ItemHeight = 25
        Me.lbExistingOperations.Location = New System.Drawing.Point(10, 252)
        Me.lbExistingOperations.Name = "lbExistingOperations"
        Me.lbExistingOperations.Size = New System.Drawing.Size(774, 454)
        Me.lbExistingOperations.TabIndex = 5
        Me.lbExistingOperations.ValueMember = "OperationID"
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
        'labelExistingOperations
        '
        Me.labelExistingOperations.AutoSize = True
        Me.labelExistingOperations.Location = New System.Drawing.Point(6, 214)
        Me.labelExistingOperations.Name = "labelExistingOperations"
        Me.labelExistingOperations.Size = New System.Drawing.Size(349, 25)
        Me.labelExistingOperations.TabIndex = 6
        Me.labelExistingOperations.Text = "Список существующих операций:"
        '
        'OperationTableAdapter
        '
        Me.OperationTableAdapter.ClearBeforeFill = True
        '
        'btnDeleteOperation
        '
        Me.btnDeleteOperation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteOperation.Location = New System.Drawing.Point(11, 860)
        Me.btnDeleteOperation.Name = "btnDeleteOperation"
        Me.btnDeleteOperation.Size = New System.Drawing.Size(775, 46)
        Me.btnDeleteOperation.TabIndex = 7
        Me.btnDeleteOperation.Text = "Удалить"
        Me.btnDeleteOperation.UseVisualStyleBackColor = True
        '
        'labelSearchOperation
        '
        Me.labelSearchOperation.AutoSize = True
        Me.labelSearchOperation.Location = New System.Drawing.Point(7, 115)
        Me.labelSearchOperation.Name = "labelSearchOperation"
        Me.labelSearchOperation.Size = New System.Drawing.Size(181, 25)
        Me.labelSearchOperation.TabIndex = 8
        Me.labelSearchOperation.Text = "Поиск операции:"
        '
        'txSearchOperation
        '
        Me.txSearchOperation.Location = New System.Drawing.Point(12, 154)
        Me.txSearchOperation.Name = "txSearchOperation"
        Me.txSearchOperation.Size = New System.Drawing.Size(381, 31)
        Me.txSearchOperation.TabIndex = 9
        '
        'labelExistingOperationProfession
        '
        Me.labelExistingOperationProfession.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.labelExistingOperationProfession.AutoSize = True
        Me.labelExistingOperationProfession.Location = New System.Drawing.Point(7, 724)
        Me.labelExistingOperationProfession.Name = "labelExistingOperationProfession"
        Me.labelExistingOperationProfession.Size = New System.Drawing.Size(390, 25)
        Me.labelExistingOperationProfession.TabIndex = 10
        Me.labelExistingOperationProfession.Text = "Профессия существующей операции:"
        '
        'txExistingOperationProfession
        '
        Me.txExistingOperationProfession.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txExistingOperationProfession.Location = New System.Drawing.Point(10, 752)
        Me.txExistingOperationProfession.Name = "txExistingOperationProfession"
        Me.txExistingOperationProfession.ReadOnly = True
        Me.txExistingOperationProfession.Size = New System.Drawing.Size(383, 31)
        Me.txExistingOperationProfession.TabIndex = 11
        '
        'OperationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 918)
        Me.Controls.Add(Me.txExistingOperationProfession)
        Me.Controls.Add(Me.labelExistingOperationProfession)
        Me.Controls.Add(Me.txSearchOperation)
        Me.Controls.Add(Me.labelSearchOperation)
        Me.Controls.Add(Me.btnDeleteOperation)
        Me.Controls.Add(Me.labelExistingOperations)
        Me.Controls.Add(Me.lbExistingOperations)
        Me.Controls.Add(Me.labelProfession)
        Me.Controls.Add(Me.labelOperationName)
        Me.Controls.Add(Me.btnAddOperation)
        Me.Controls.Add(Me.cbProfession)
        Me.Controls.Add(Me.txNewOperationName)
        Me.Name = "OperationForm"
        Me.Text = "Операция"
        CType(Me.PositionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txNewOperationName As TextBox
    Friend WithEvents cbProfession As ComboBox
    Friend WithEvents btnAddOperation As Button
    Friend WithEvents labelOperationName As Label
    Friend WithEvents labelProfession As Label
    Friend WithEvents OperationDBDataSet4 As OperationDBDataSet4
    Friend WithEvents PositionBindingSource As BindingSource
    Friend WithEvents PositionTableAdapter As OperationDBDataSet4TableAdapters.PositionTableAdapter
    Friend WithEvents lbExistingOperations As ListBox
    Friend WithEvents labelExistingOperations As Label
    Friend WithEvents OperationDBDataSet1 As OperationDBDataSet1
    Friend WithEvents OperationBindingSource As BindingSource
    Friend WithEvents OperationTableAdapter As OperationDBDataSet1TableAdapters.OperationTableAdapter
    Friend WithEvents btnDeleteOperation As Button
    Friend WithEvents labelSearchOperation As Label
    Friend WithEvents txSearchOperation As TextBox
    Friend WithEvents labelExistingOperationProfession As Label
    Friend WithEvents txExistingOperationProfession As TextBox
End Class
