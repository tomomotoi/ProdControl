<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.НакладныеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.НоваяToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.НайтиToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОсновныеДанныеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.АртикулToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОперацияToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ТехкартаToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ПроверкаРасценокРаботToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.СотрудникиToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.КоэффициентыДопЗатратToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОтчетыToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.НормаВыроботкиToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОтчетПоСотрудникуToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ВыходToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.УтилитыToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОчисткаТаблицыOperationLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ВосстановитьБДИзАрхиваToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ВыбратьФайлToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.АвтоToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.СоздатьКопиюБДToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОчиститьАрхивКопийБДToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OperationLogTableAdapter1 = New ProdControl.DBSetTableAdapters.OperationLogTableAdapter()
        Me.ТехкартаВязаниеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ДеталиИзделийToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ОборудованиеToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.НакладныеToolStripMenuItem, Me.ОсновныеДанныеToolStripMenuItem, Me.ОтчетыToolStripMenuItem, Me.ВыходToolStripMenuItem, Me.УтилитыToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(3, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(888, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'НакладныеToolStripMenuItem
        '
        Me.НакладныеToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.НоваяToolStripMenuItem, Me.НайтиToolStripMenuItem})
        Me.НакладныеToolStripMenuItem.Name = "НакладныеToolStripMenuItem"
        Me.НакладныеToolStripMenuItem.Size = New System.Drawing.Size(81, 22)
        Me.НакладныеToolStripMenuItem.Text = "Накладные"
        '
        'НоваяToolStripMenuItem
        '
        Me.НоваяToolStripMenuItem.Name = "НоваяToolStripMenuItem"
        Me.НоваяToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.НоваяToolStripMenuItem.Text = "Новая"
        '
        'НайтиToolStripMenuItem
        '
        Me.НайтиToolStripMenuItem.Name = "НайтиToolStripMenuItem"
        Me.НайтиToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.НайтиToolStripMenuItem.Text = "Найти"
        '
        'ОсновныеДанныеToolStripMenuItem
        '
        Me.ОсновныеДанныеToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.АртикулToolStripMenuItem, Me.ОперацияToolStripMenuItem, Me.ТехкартаToolStripMenuItem, Me.ТехкартаВязаниеToolStripMenuItem, Me.ПроверкаРасценокРаботToolStripMenuItem, Me.СотрудникиToolStripMenuItem, Me.КоэффициентыДопЗатратToolStripMenuItem, Me.ДеталиИзделийToolStripMenuItem, Me.ОборудованиеToolStripMenuItem})
        Me.ОсновныеДанныеToolStripMenuItem.Name = "ОсновныеДанныеToolStripMenuItem"
        Me.ОсновныеДанныеToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.ОсновныеДанныеToolStripMenuItem.Text = "Основные данные"
        '
        'АртикулToolStripMenuItem
        '
        Me.АртикулToolStripMenuItem.Name = "АртикулToolStripMenuItem"
        Me.АртикулToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.АртикулToolStripMenuItem.Text = "Артикул"
        '
        'ОперацияToolStripMenuItem
        '
        Me.ОперацияToolStripMenuItem.Name = "ОперацияToolStripMenuItem"
        Me.ОперацияToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.ОперацияToolStripMenuItem.Text = "Операция"
        '
        'ТехкартаToolStripMenuItem
        '
        Me.ТехкартаToolStripMenuItem.Name = "ТехкартаToolStripMenuItem"
        Me.ТехкартаToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.ТехкартаToolStripMenuItem.Text = "Тех.карта"
        '
        'ПроверкаРасценокРаботToolStripMenuItem
        '
        Me.ПроверкаРасценокРаботToolStripMenuItem.Name = "ПроверкаРасценокРаботToolStripMenuItem"
        Me.ПроверкаРасценокРаботToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.ПроверкаРасценокРаботToolStripMenuItem.Text = "Проверка расценок работ"
        '
        'СотрудникиToolStripMenuItem
        '
        Me.СотрудникиToolStripMenuItem.Name = "СотрудникиToolStripMenuItem"
        Me.СотрудникиToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.СотрудникиToolStripMenuItem.Text = "Сотрудники"
        '
        'КоэффициентыДопЗатратToolStripMenuItem
        '
        Me.КоэффициентыДопЗатратToolStripMenuItem.Name = "КоэффициентыДопЗатратToolStripMenuItem"
        Me.КоэффициентыДопЗатратToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.КоэффициентыДопЗатратToolStripMenuItem.Text = "Коэффициенты доп. затрат"
        '
        'ОтчетыToolStripMenuItem
        '
        Me.ОтчетыToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.НормаВыроботкиToolStripMenuItem, Me.ОтчетПоСотрудникуToolStripMenuItem})
        Me.ОтчетыToolStripMenuItem.Name = "ОтчетыToolStripMenuItem"
        Me.ОтчетыToolStripMenuItem.Size = New System.Drawing.Size(60, 22)
        Me.ОтчетыToolStripMenuItem.Text = "Отчеты"
        '
        'НормаВыроботкиToolStripMenuItem
        '
        Me.НормаВыроботкиToolStripMenuItem.Name = "НормаВыроботкиToolStripMenuItem"
        Me.НормаВыроботкиToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.НормаВыроботкиToolStripMenuItem.Text = "Норма выработки"
        '
        'ОтчетПоСотрудникуToolStripMenuItem
        '
        Me.ОтчетПоСотрудникуToolStripMenuItem.Name = "ОтчетПоСотрудникуToolStripMenuItem"
        Me.ОтчетПоСотрудникуToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ОтчетПоСотрудникуToolStripMenuItem.Text = "Отчет по сотруднику"
        '
        'ВыходToolStripMenuItem
        '
        Me.ВыходToolStripMenuItem.Name = "ВыходToolStripMenuItem"
        Me.ВыходToolStripMenuItem.Size = New System.Drawing.Size(53, 22)
        Me.ВыходToolStripMenuItem.Text = "Выход"
        '
        'УтилитыToolStripMenuItem
        '
        Me.УтилитыToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ОчисткаТаблицыOperationLogToolStripMenuItem, Me.ВосстановитьБДИзАрхиваToolStripMenuItem, Me.СоздатьКопиюБДToolStripMenuItem, Me.ОчиститьАрхивКопийБДToolStripMenuItem})
        Me.УтилитыToolStripMenuItem.Name = "УтилитыToolStripMenuItem"
        Me.УтилитыToolStripMenuItem.Size = New System.Drawing.Size(66, 22)
        Me.УтилитыToolStripMenuItem.Text = "Утилиты"
        '
        'ОчисткаТаблицыOperationLogToolStripMenuItem
        '
        Me.ОчисткаТаблицыOperationLogToolStripMenuItem.Name = "ОчисткаТаблицыOperationLogToolStripMenuItem"
        Me.ОчисткаТаблицыOperationLogToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ОчисткаТаблицыOperationLogToolStripMenuItem.Text = "Очистка таблицы OperationLog"
        '
        'ВосстановитьБДИзАрхиваToolStripMenuItem
        '
        Me.ВосстановитьБДИзАрхиваToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ВыбратьФайлToolStripMenuItem, Me.АвтоToolStripMenuItem})
        Me.ВосстановитьБДИзАрхиваToolStripMenuItem.Name = "ВосстановитьБДИзАрхиваToolStripMenuItem"
        Me.ВосстановитьБДИзАрхиваToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ВосстановитьБДИзАрхиваToolStripMenuItem.Text = "Восстановить БД из архива"
        '
        'ВыбратьФайлToolStripMenuItem
        '
        Me.ВыбратьФайлToolStripMenuItem.Name = "ВыбратьФайлToolStripMenuItem"
        Me.ВыбратьФайлToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.ВыбратьФайлToolStripMenuItem.Text = "Выбрать файл..."
        '
        'АвтоToolStripMenuItem
        '
        Me.АвтоToolStripMenuItem.Name = "АвтоToolStripMenuItem"
        Me.АвтоToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.АвтоToolStripMenuItem.Text = "Авто"
        '
        'СоздатьКопиюБДToolStripMenuItem
        '
        Me.СоздатьКопиюБДToolStripMenuItem.Name = "СоздатьКопиюБДToolStripMenuItem"
        Me.СоздатьКопиюБДToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.СоздатьКопиюБДToolStripMenuItem.Text = "Создать копию БД"
        '
        'ОчиститьАрхивКопийБДToolStripMenuItem
        '
        Me.ОчиститьАрхивКопийБДToolStripMenuItem.Name = "ОчиститьАрхивКопийБДToolStripMenuItem"
        Me.ОчиститьАрхивКопийБДToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ОчиститьАрхивКопийБДToolStripMenuItem.Text = "Очистить архив копий БД"
        '
        'OperationLogTableAdapter1
        '
        Me.OperationLogTableAdapter1.ClearBeforeFill = True
        '
        'ТехкартаВязаниеToolStripMenuItem
        '
        Me.ТехкартаВязаниеToolStripMenuItem.Name = "ТехкартаВязаниеToolStripMenuItem"
        Me.ТехкартаВязаниеToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.ТехкартаВязаниеToolStripMenuItem.Text = "Тех.карта (Вязание)"
        '
        'ДеталиИзделийToolStripMenuItem
        '
        Me.ДеталиИзделийToolStripMenuItem.Name = "ДеталиИзделийToolStripMenuItem"
        Me.ДеталиИзделийToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.ДеталиИзделийToolStripMenuItem.Text = "Детали изделий"
        '
        'ОборудованиеToolStripMenuItem
        '
        Me.ОборудованиеToolStripMenuItem.Name = "ОборудованиеToolStripMenuItem"
        Me.ОборудованиеToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.ОборудованиеToolStripMenuItem.Text = "Оборудование"
        '
        'mainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(888, 390)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "mainForm"
        Me.Text = "Главное окно"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents НакладныеToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents НоваяToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents НайтиToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОтчетыToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents НормаВыроботкиToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ВыходToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОсновныеДанныеToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents АртикулToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОперацияToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ТехкартаToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ОтчетПоСотрудникуToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ПроверкаРасценокРаботToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents УтилитыToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ОчисткаТаблицыOperationLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OperationLogTableAdapter1 As DBSetTableAdapters.OperationLogTableAdapter
    Friend WithEvents ВосстановитьБДИзАрхиваToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ВыбратьФайлToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents АвтоToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents СоздатьКопиюБДToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ОчиститьАрхивКопийБДToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents СотрудникиToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents КоэффициентыДопЗатратToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ТехкартаВязаниеToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ДеталиИзделийToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ОборудованиеToolStripMenuItem As ToolStripMenuItem
End Class
