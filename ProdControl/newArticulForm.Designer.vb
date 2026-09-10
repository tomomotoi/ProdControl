<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newArticulForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(newArticulForm))
        Me.txtArt = New System.Windows.Forms.TextBox()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.lbArt = New System.Windows.Forms.Label()
        Me.lbDesc = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.DBSet = New DBSet()
        Me.ArticulTableAdapter1 = New DBSetTableAdapters.ArticulTableAdapter()
        CType(Me.DBSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtArt
        '
        Me.txtArt.Location = New System.Drawing.Point(80, 15)
        Me.txtArt.Name = "txtArt"
        Me.txtArt.Size = New System.Drawing.Size(100, 20)
        Me.txtArt.TabIndex = 0
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(80, 43)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(237, 20)
        Me.txtDesc.TabIndex = 1
        '
        'lbArt
        '
        Me.lbArt.AutoSize = True
        Me.lbArt.Location = New System.Drawing.Point(12, 18)
        Me.lbArt.Name = "lbArt"
        Me.lbArt.Size = New System.Drawing.Size(48, 13)
        Me.lbArt.TabIndex = 2
        Me.lbArt.Text = "Артикул"
        '
        'lbDesc
        '
        Me.lbDesc.AutoSize = True
        Me.lbDesc.Location = New System.Drawing.Point(12, 46)
        Me.lbDesc.Name = "lbDesc"
        Me.lbDesc.Size = New System.Drawing.Size(57, 13)
        Me.lbDesc.TabIndex = 3
        Me.lbDesc.Text = "Описание"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(151, 88)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(232, 88)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Закрыть"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'DBSet
        '
        Me.DBSet.DataSetName = "DBSet"
        Me.DBSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ArticulTableAdapter1
        '
        Me.ArticulTableAdapter1.ClearBeforeFill = True
        '
        'newArticulForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 131)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lbDesc)
        Me.Controls.Add(Me.lbArt)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtArt)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "newArticulForm"
        Me.Text = "Артикул"
        CType(Me.DBSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtArt As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents lbArt As System.Windows.Forms.Label
    Friend WithEvents lbDesc As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents ArticulTableAdapter1 As DBSetTableAdapters.ArticulTableAdapter
    Friend WithEvents DBSet As DBSet
End Class
