<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MachinesForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MachinesForm))
        Me.labelList = New System.Windows.Forms.Label()
        Me.dgvMachines = New System.Windows.Forms.DataGridView()
        Me.gboxMachineParameters = New System.Windows.Forms.GroupBox()
        Me.tbId = New System.Windows.Forms.TextBox()
        Me.labelId = New System.Windows.Forms.Label()
        Me.tbModel = New System.Windows.Forms.TextBox()
        Me.tbMake = New System.Windows.Forms.TextBox()
        Me.labelModel = New System.Windows.Forms.Label()
        Me.labelMake = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSaveChanges = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClearFields = New System.Windows.Forms.Button()
        CType(Me.dgvMachines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gboxMachineParameters.SuspendLayout()
        Me.SuspendLayout()
        '
        'labelList
        '
        Me.labelList.AutoSize = True
        Me.labelList.Location = New System.Drawing.Point(12, 20)
        Me.labelList.Name = "labelList"
        Me.labelList.Size = New System.Drawing.Size(118, 13)
        Me.labelList.TabIndex = 0
        Me.labelList.Text = "Список оборудования"
        '
        'dgvMachines
        '
        Me.dgvMachines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMachines.Location = New System.Drawing.Point(15, 36)
        Me.dgvMachines.Name = "dgvMachines"
        Me.dgvMachines.Size = New System.Drawing.Size(455, 160)
        Me.dgvMachines.TabIndex = 1
        '
        'gboxMachineParameters
        '
        Me.gboxMachineParameters.Controls.Add(Me.tbId)
        Me.gboxMachineParameters.Controls.Add(Me.labelId)
        Me.gboxMachineParameters.Controls.Add(Me.tbModel)
        Me.gboxMachineParameters.Controls.Add(Me.tbMake)
        Me.gboxMachineParameters.Controls.Add(Me.labelModel)
        Me.gboxMachineParameters.Controls.Add(Me.labelMake)
        Me.gboxMachineParameters.Location = New System.Drawing.Point(476, 36)
        Me.gboxMachineParameters.Name = "gboxMachineParameters"
        Me.gboxMachineParameters.Size = New System.Drawing.Size(312, 100)
        Me.gboxMachineParameters.TabIndex = 3
        Me.gboxMachineParameters.TabStop = False
        Me.gboxMachineParameters.Text = "Параметры оборудования"
        '
        'tbId
        '
        Me.tbId.Enabled = False
        Me.tbId.Location = New System.Drawing.Point(129, 21)
        Me.tbId.Name = "tbId"
        Me.tbId.Size = New System.Drawing.Size(177, 20)
        Me.tbId.TabIndex = 10
        '
        'labelId
        '
        Me.labelId.AutoSize = True
        Me.labelId.Location = New System.Drawing.Point(6, 24)
        Me.labelId.Name = "labelId"
        Me.labelId.Size = New System.Drawing.Size(21, 13)
        Me.labelId.TabIndex = 9
        Me.labelId.Text = "ID:"
        '
        'tbModel
        '
        Me.tbModel.Location = New System.Drawing.Point(129, 74)
        Me.tbModel.Name = "tbModel"
        Me.tbModel.Size = New System.Drawing.Size(177, 20)
        Me.tbModel.TabIndex = 8
        '
        'tbMake
        '
        Me.tbMake.Location = New System.Drawing.Point(129, 48)
        Me.tbMake.Name = "tbMake"
        Me.tbMake.Size = New System.Drawing.Size(177, 20)
        Me.tbMake.TabIndex = 7
        '
        'labelModel
        '
        Me.labelModel.AutoSize = True
        Me.labelModel.Location = New System.Drawing.Point(6, 77)
        Me.labelModel.Name = "labelModel"
        Me.labelModel.Size = New System.Drawing.Size(123, 13)
        Me.labelModel.TabIndex = 6
        Me.labelModel.Text = "Модель оборудования:"
        '
        'labelMake
        '
        Me.labelMake.AutoSize = True
        Me.labelMake.Location = New System.Drawing.Point(6, 51)
        Me.labelMake.Name = "labelMake"
        Me.labelMake.Size = New System.Drawing.Size(117, 13)
        Me.labelMake.TabIndex = 5
        Me.labelMake.Text = "Марка оборудования:"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(476, 143)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(153, 23)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.Text = "Добавить"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnSaveChanges
        '
        Me.btnSaveChanges.Location = New System.Drawing.Point(635, 143)
        Me.btnSaveChanges.Name = "btnSaveChanges"
        Me.btnSaveChanges.Size = New System.Drawing.Size(153, 23)
        Me.btnSaveChanges.TabIndex = 5
        Me.btnSaveChanges.Text = "Сохранить изменения"
        Me.btnSaveChanges.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(476, 172)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(153, 23)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Удалить"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnClearFields
        '
        Me.btnClearFields.Location = New System.Drawing.Point(635, 173)
        Me.btnClearFields.Name = "btnClearFields"
        Me.btnClearFields.Size = New System.Drawing.Size(153, 23)
        Me.btnClearFields.TabIndex = 7
        Me.btnClearFields.Text = "Очистить поля"
        Me.btnClearFields.UseVisualStyleBackColor = True
        '
        'MachinesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 208)
        Me.Controls.Add(Me.btnClearFields)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSaveChanges)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.gboxMachineParameters)
        Me.Controls.Add(Me.dgvMachines)
        Me.Controls.Add(Me.labelList)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MachinesForm"
        Me.Text = "Оборудование"
        CType(Me.dgvMachines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gboxMachineParameters.ResumeLayout(False)
        Me.gboxMachineParameters.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents labelList As Label
    Friend WithEvents dgvMachines As DataGridView
    Friend WithEvents gboxMachineParameters As GroupBox
    Friend WithEvents tbModel As TextBox
    Friend WithEvents tbMake As TextBox
    Friend WithEvents labelModel As Label
    Friend WithEvents labelMake As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnSaveChanges As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClearFields As Button
    Friend WithEvents tbId As TextBox
    Friend WithEvents labelId As Label
End Class
