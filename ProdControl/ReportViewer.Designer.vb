<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportViewer
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
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportViewer))
        Me.ReportEmployeeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet1 = New ProdControl.OperationDBDataSet1()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.btnLoadReport = New System.Windows.Forms.Button()
        Me.EmployeeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.OperationDBDataSet1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeeBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.panelEmployee = New System.Windows.Forms.Panel()
        Me.dgvEmployee = New System.Windows.Forms.DataGridView()
        Me.EmployeeIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.panelReport = New System.Windows.Forms.Panel()
        Me.ReportByEmployeeViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.ReportEmployeeTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.ReportEmployeeTableAdapter()
        Me.EmployeeTableAdapter = New ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter()
        Me.ReportEmployeeBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.ReportEmployeeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OperationDBDataSet1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeeBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelEmployee.SuspendLayout()
        CType(Me.dgvEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeeBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelReport.SuspendLayout()
        CType(Me.ReportEmployeeBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportEmployeeBindingSource
        '
        Me.ReportEmployeeBindingSource.DataMember = "ReportEmployee"
        Me.ReportEmployeeBindingSource.DataSource = Me.OperationDBDataSet1
        '
        'OperationDBDataSet1
        '
        Me.OperationDBDataSet1.DataSetName = "OperationDBDataSet1"
        Me.OperationDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtpTo)
        Me.Panel1.Controls.Add(Me.dtpFrom)
        Me.Panel1.Controls.Add(Me.btnLoadReport)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 368)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(962, 66)
        Me.Panel1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(880, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Назад"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(254, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(19, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "до"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(52, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Период от"
        '
        'dtpTo
        '
        Me.dtpTo.Location = New System.Drawing.Point(278, 23)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(145, 20)
        Me.dtpTo.TabIndex = 3
        '
        'dtpFrom
        '
        Me.dtpFrom.Location = New System.Drawing.Point(117, 23)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(128, 20)
        Me.dtpFrom.TabIndex = 2
        '
        'btnLoadReport
        '
        Me.btnLoadReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadReport.Location = New System.Drawing.Point(798, 20)
        Me.btnLoadReport.Name = "btnLoadReport"
        Me.btnLoadReport.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadReport.TabIndex = 0
        Me.btnLoadReport.Text = "Отчет"
        Me.btnLoadReport.UseVisualStyleBackColor = True
        '
        'EmployeeBindingSource
        '
        Me.EmployeeBindingSource.DataMember = "Employee"
        Me.EmployeeBindingSource.DataSource = Me.OperationDBDataSet1BindingSource
        '
        'OperationDBDataSet1BindingSource
        '
        Me.OperationDBDataSet1BindingSource.DataSource = Me.OperationDBDataSet1
        Me.OperationDBDataSet1BindingSource.Position = 0
        '
        'EmployeeBindingSource1
        '
        Me.EmployeeBindingSource1.DataMember = "Employee"
        Me.EmployeeBindingSource1.DataSource = Me.OperationDBDataSet1BindingSource
        '
        'panelEmployee
        '
        Me.panelEmployee.Controls.Add(Me.dgvEmployee)
        Me.panelEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelEmployee.Location = New System.Drawing.Point(0, 0)
        Me.panelEmployee.Name = "panelEmployee"
        Me.panelEmployee.Size = New System.Drawing.Size(962, 368)
        Me.panelEmployee.TabIndex = 2
        '
        'dgvEmployee
        '
        Me.dgvEmployee.AllowUserToAddRows = False
        Me.dgvEmployee.AllowUserToDeleteRows = False
        Me.dgvEmployee.AutoGenerateColumns = False
        Me.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEmployee.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EmployeeIDDataGridViewTextBoxColumn, Me.EmployeeNameDataGridViewTextBoxColumn, Me.EmployeeNumberDataGridViewTextBoxColumn})
        Me.dgvEmployee.DataSource = Me.EmployeeBindingSource2
        Me.dgvEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEmployee.Location = New System.Drawing.Point(0, 0)
        Me.dgvEmployee.Name = "dgvEmployee"
        Me.dgvEmployee.ReadOnly = True
        Me.dgvEmployee.RowHeadersWidth = 82
        Me.dgvEmployee.Size = New System.Drawing.Size(962, 368)
        Me.dgvEmployee.TabIndex = 0
        '
        'EmployeeIDDataGridViewTextBoxColumn
        '
        Me.EmployeeIDDataGridViewTextBoxColumn.DataPropertyName = "EmployeeID"
        Me.EmployeeIDDataGridViewTextBoxColumn.HeaderText = "EmployeeID"
        Me.EmployeeIDDataGridViewTextBoxColumn.MinimumWidth = 10
        Me.EmployeeIDDataGridViewTextBoxColumn.Name = "EmployeeIDDataGridViewTextBoxColumn"
        Me.EmployeeIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.EmployeeIDDataGridViewTextBoxColumn.Visible = False
        Me.EmployeeIDDataGridViewTextBoxColumn.Width = 200
        '
        'EmployeeNameDataGridViewTextBoxColumn
        '
        Me.EmployeeNameDataGridViewTextBoxColumn.DataPropertyName = "EmployeeName"
        Me.EmployeeNameDataGridViewTextBoxColumn.HeaderText = "Сотрудник"
        Me.EmployeeNameDataGridViewTextBoxColumn.MinimumWidth = 10
        Me.EmployeeNameDataGridViewTextBoxColumn.Name = "EmployeeNameDataGridViewTextBoxColumn"
        Me.EmployeeNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.EmployeeNameDataGridViewTextBoxColumn.Width = 200
        '
        'EmployeeNumberDataGridViewTextBoxColumn
        '
        Me.EmployeeNumberDataGridViewTextBoxColumn.DataPropertyName = "EmployeeNumber"
        Me.EmployeeNumberDataGridViewTextBoxColumn.HeaderText = "Табельный номер"
        Me.EmployeeNumberDataGridViewTextBoxColumn.MinimumWidth = 10
        Me.EmployeeNumberDataGridViewTextBoxColumn.Name = "EmployeeNumberDataGridViewTextBoxColumn"
        Me.EmployeeNumberDataGridViewTextBoxColumn.ReadOnly = True
        Me.EmployeeNumberDataGridViewTextBoxColumn.Width = 200
        '
        'EmployeeBindingSource2
        '
        Me.EmployeeBindingSource2.DataMember = "Employee"
        Me.EmployeeBindingSource2.DataSource = Me.OperationDBDataSet1BindingSource
        '
        'panelReport
        '
        Me.panelReport.Controls.Add(Me.ReportByEmployeeViewer)
        Me.panelReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelReport.Location = New System.Drawing.Point(0, 0)
        Me.panelReport.Name = "panelReport"
        Me.panelReport.Size = New System.Drawing.Size(962, 368)
        Me.panelReport.TabIndex = 3
        Me.panelReport.Visible = False
        '
        'ReportByEmployeeViewer
        '
        Me.ReportByEmployeeViewer.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "ReportEmployee"
        ReportDataSource1.Value = Me.ReportEmployeeBindingSource
        Me.ReportByEmployeeViewer.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportByEmployeeViewer.LocalReport.ReportEmbeddedResource = "ProdControl.ReportEmployee.rdlc"
        Me.ReportByEmployeeViewer.Location = New System.Drawing.Point(0, 0)
        Me.ReportByEmployeeViewer.Name = "ReportByEmployeeViewer"
        Me.ReportByEmployeeViewer.Size = New System.Drawing.Size(962, 368)
        Me.ReportByEmployeeViewer.TabIndex = 1
        '
        'ReportEmployeeTableAdapter
        '
        Me.ReportEmployeeTableAdapter.ClearBeforeFill = True
        '
        'EmployeeTableAdapter
        '
        Me.EmployeeTableAdapter.ClearBeforeFill = True
        '
        'ReportEmployeeBindingSource1
        '
        Me.ReportEmployeeBindingSource1.DataMember = "ReportEmployee"
        Me.ReportEmployeeBindingSource1.DataSource = Me.OperationDBDataSet1BindingSource
        '
        'ReportViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(962, 434)
        Me.Controls.Add(Me.panelReport)
        Me.Controls.Add(Me.panelEmployee)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ReportViewer"
        Me.Text = "ReportViewer"
        CType(Me.ReportEmployeeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.EmployeeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OperationDBDataSet1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeeBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelEmployee.ResumeLayout(False)
        CType(Me.dgvEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeeBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelReport.ResumeLayout(False)
        CType(Me.ReportEmployeeBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportEmployeeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OperationDBDataSet1 As ProdControl.OperationDBDataSet1
    Friend WithEvents ReportEmployeeTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.ReportEmployeeTableAdapter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents OperationDBDataSet1BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents btnLoadReport As System.Windows.Forms.Button
    Friend WithEvents EmployeeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents EmployeeTableAdapter As ProdControl.OperationDBDataSet1TableAdapters.EmployeeTableAdapter
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents EmployeeBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents panelEmployee As System.Windows.Forms.Panel
    Friend WithEvents dgvEmployee As System.Windows.Forms.DataGridView
    Friend WithEvents EmployeeIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmployeeNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmployeeNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmployeeBindingSource2 As System.Windows.Forms.BindingSource
    Friend WithEvents panelReport As System.Windows.Forms.Panel
    Friend WithEvents ReportByEmployeeViewer As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ReportEmployeeBindingSource1 As System.Windows.Forms.BindingSource
End Class
