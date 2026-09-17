<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DetailsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DetailsForm))
        Me.dgvParts = New System.Windows.Forms.DataGridView()
        Me.gbPartDetails = New System.Windows.Forms.GroupBox()
        Me.labelDetailName = New System.Windows.Forms.Label()
        Me.tbPartName = New System.Windows.Forms.TextBox()
        Me.labelDetailsList = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        CType(Me.dgvParts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPartDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvParts
        '
        Me.dgvParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvParts.Location = New System.Drawing.Point(12, 30)
        Me.dgvParts.Name = "dgvParts"
        Me.dgvParts.Size = New System.Drawing.Size(516, 231)
        Me.dgvParts.TabIndex = 0
        '
        'gbPartDetails
        '
        Me.gbPartDetails.Controls.Add(Me.tbPartName)
        Me.gbPartDetails.Controls.Add(Me.labelDetailName)
        Me.gbPartDetails.Location = New System.Drawing.Point(534, 30)
        Me.gbPartDetails.Name = "gbPartDetails"
        Me.gbPartDetails.Size = New System.Drawing.Size(270, 160)
        Me.gbPartDetails.TabIndex = 1
        Me.gbPartDetails.TabStop = False
        Me.gbPartDetails.Text = "Свойства детали"
        '
        'labelDetailName
        '
        Me.labelDetailName.AutoSize = True
        Me.labelDetailName.Location = New System.Drawing.Point(6, 31)
        Me.labelDetailName.Name = "labelDetailName"
        Me.labelDetailName.Size = New System.Drawing.Size(124, 13)
        Me.labelDetailName.TabIndex = 0
        Me.labelDetailName.Text = "Наименование детали:"
        '
        'tbPartName
        '
        Me.tbPartName.Location = New System.Drawing.Point(9, 47)
        Me.tbPartName.Multiline = True
        Me.tbPartName.Name = "tbPartName"
        Me.tbPartName.Size = New System.Drawing.Size(255, 95)
        Me.tbPartName.TabIndex = 1
        '
        'labelDetailsList
        '
        Me.labelDetailsList.AutoSize = True
        Me.labelDetailsList.Location = New System.Drawing.Point(12, 14)
        Me.labelDetailsList.Name = "labelDetailsList"
        Me.labelDetailsList.Size = New System.Drawing.Size(88, 13)
        Me.labelDetailsList.TabIndex = 2
        Me.labelDetailsList.Text = "Список деталей"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(534, 208)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(130, 23)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "Добавить"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(670, 208)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(129, 23)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Сохранить изменения"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(670, 237)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(129, 23)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Очистить поля"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(534, 237)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(130, 23)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Удалить"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'DetailsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 272)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.labelDetailsList)
        Me.Controls.Add(Me.gbPartDetails)
        Me.Controls.Add(Me.dgvParts)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DetailsForm"
        Me.Text = "Детали"
        CType(Me.dgvParts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPartDetails.ResumeLayout(False)
        Me.gbPartDetails.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvParts As DataGridView
    Friend WithEvents gbPartDetails As GroupBox
    Friend WithEvents tbPartName As TextBox
    Friend WithEvents labelDetailName As Label
    Friend WithEvents labelDetailsList As Label
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnDelete As Button
End Class
