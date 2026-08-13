<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CoefficientsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CoefficientsForm))
        Me.lbCoefficients = New System.Windows.Forms.ListBox()
        Me.CoefficientsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet3 = New ProdControl.OperationDBDataSet3()
        Me.labelCoefficients = New System.Windows.Forms.Label()
        Me.labelNewCoefficient = New System.Windows.Forms.Label()
        Me.tbNewCoefficientValue = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.ckbxEditMode = New System.Windows.Forms.CheckBox()
        Me.CoefficientsTableAdapter = New ProdControl.OperationDBDataSet3TableAdapters.CoefficientsTableAdapter()
        CType(Me.CoefficientsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbCoefficients
        '
        Me.lbCoefficients.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbCoefficients.DataSource = Me.CoefficientsBindingSource
        Me.lbCoefficients.DisplayMember = "coefficient_value"
        Me.lbCoefficients.FormattingEnabled = True
        Me.lbCoefficients.Location = New System.Drawing.Point(6, 77)
        Me.lbCoefficients.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.lbCoefficients.Name = "lbCoefficients"
        Me.lbCoefficients.Size = New System.Drawing.Size(390, 173)
        Me.lbCoefficients.TabIndex = 0
        Me.lbCoefficients.ValueMember = "coefficient_id"
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
        'labelCoefficients
        '
        Me.labelCoefficients.AutoSize = True
        Me.labelCoefficients.Location = New System.Drawing.Point(4, 56)
        Me.labelCoefficients.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelCoefficients.Name = "labelCoefficients"
        Me.labelCoefficients.Size = New System.Drawing.Size(88, 13)
        Me.labelCoefficients.TabIndex = 1
        Me.labelCoefficients.Text = "Коэффициенты:"
        '
        'labelNewCoefficient
        '
        Me.labelNewCoefficient.AutoSize = True
        Me.labelNewCoefficient.Location = New System.Drawing.Point(4, 10)
        Me.labelNewCoefficient.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.labelNewCoefficient.Name = "labelNewCoefficient"
        Me.labelNewCoefficient.Size = New System.Drawing.Size(116, 13)
        Me.labelNewCoefficient.TabIndex = 2
        Me.labelNewCoefficient.Text = "Новый коэффициент:"
        '
        'tbNewCoefficientValue
        '
        Me.tbNewCoefficientValue.Location = New System.Drawing.Point(119, 9)
        Me.tbNewCoefficientValue.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tbNewCoefficientValue.Name = "tbNewCoefficientValue"
        Me.tbNewCoefficientValue.Size = New System.Drawing.Size(52, 20)
        Me.tbNewCoefficientValue.TabIndex = 3
        '
        'btnAdd
        '
        Me.btnAdd.Enabled = False
        Me.btnAdd.Location = New System.Drawing.Point(180, 9)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(94, 23)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.Text = "Добавить"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Enabled = False
        Me.btnDelete.Location = New System.Drawing.Point(300, 51)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(94, 23)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Удалить"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'ckbxEditMode
        '
        Me.ckbxEditMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ckbxEditMode.AutoSize = True
        Me.ckbxEditMode.Location = New System.Drawing.Point(6, 252)
        Me.ckbxEditMode.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ckbxEditMode.Name = "ckbxEditMode"
        Me.ckbxEditMode.Size = New System.Drawing.Size(147, 17)
        Me.ckbxEditMode.TabIndex = 6
        Me.ckbxEditMode.Text = "Режим редактирования"
        Me.ckbxEditMode.UseVisualStyleBackColor = True
        '
        'CoefficientsTableAdapter
        '
        Me.CoefficientsTableAdapter.ClearBeforeFill = True
        '
        'CoefficientsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 276)
        Me.Controls.Add(Me.ckbxEditMode)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.tbNewCoefficientValue)
        Me.Controls.Add(Me.labelNewCoefficient)
        Me.Controls.Add(Me.labelCoefficients)
        Me.Controls.Add(Me.lbCoefficients)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "CoefficientsForm"
        Me.Text = "Коэффициенты доп. затрат"
        CType(Me.CoefficientsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbCoefficients As ListBox
    Friend WithEvents labelCoefficients As Label
    Friend WithEvents labelNewCoefficient As Label
    Friend WithEvents tbNewCoefficientValue As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents ckbxEditMode As CheckBox
    Friend WithEvents OperationDBDataSet3 As OperationDBDataSet3
    Friend WithEvents CoefficientsBindingSource As BindingSource
    Friend WithEvents CoefficientsTableAdapter As OperationDBDataSet3TableAdapters.CoefficientsTableAdapter
End Class
