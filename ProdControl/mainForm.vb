Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class mainForm

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ВыходToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub НоваяToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles НоваяToolStripMenuItem.Click

        Dim newBill = New newBill()
        newBill.MdiParent = Me
        newBill.Show()


    End Sub

    Private Sub НормаВыроботкиToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles НормаВыроботкиToolStripMenuItem.Click

        Dim calculationForm = New calculationForm()
        calculationForm.MdiParent = Me
        calculationForm.Show()

    End Sub

    Private Sub ТехкартыToolStripMenuItem_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub ТехкартаToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ТехкартаToolStripMenuItem.Click
        Dim routingForm = New RoutingForm()
        routingForm.MdiParent = Me
        routingForm.Show()

    End Sub

    Private Sub АртикулToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles АртикулToolStripMenuItem.Click
        Dim artForm = New newArticulForm()
        artForm.MdiParent = Me
        artForm.Show()
    End Sub

    Private Sub ОтчетПоСотрудникуToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОтчетПоСотрудникуToolStripMenuItem.Click
        Dim rptForm = New ReportViewer()
        rptForm.MdiParent = Me
        rptForm.Show()
    End Sub

    Private Sub НайтиToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles НайтиToolStripMenuItem.Click
        Dim searchBillForm = New SearchBillForm()
        searchBillForm.MdiParent = Me
        searchBillForm.Show()
    End Sub

    Private Sub ПроверкаРасценокРаботToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ПроверкаРасценокРаботToolStripMenuItem.Click
        Dim tariffsCheckForm = New TariffsCheckForm()
        tariffsCheckForm.MdiParent = Me
        tariffsCheckForm.Show()
    End Sub

    Private Sub ОперацияToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОперацияToolStripMenuItem.Click
        Dim operationForm = New OperationForm()
        operationForm.MdiParent = Me
        operationForm.Show()
    End Sub

    Private Sub ОчисткаТаблицыOperationLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОчисткаТаблицыOperationLogToolStripMenuItem.Click
        Dim userChoice As DialogResult = MessageBox.Show("Вы уверены, что хотите очистить таблицу OperationLog, с записями о последних операциях? После подтверждения операция не может быть отменена!", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)

        If userChoice = DialogResult.OK Then
            MessageBox.Show("Перед очисткой таблицы OperationLog будет произведено копирование базы данных.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
            backUpCurrentDB()
            clearOperationLogTable()
        ElseIf userChoice = DialogResult.Cancel Then
            Return
        End If

    End Sub

    Private Sub backUpCurrentDbOnTheFirstDayOfTheMonth()
        If DateAndTime.Today.Day < deserializeCurrentDay() Then
            backUpCurrentDB()
        End If
    End Sub

    Private Sub backUpCurrentDB()
        Dim currentPath As String = String.Format("{0}\{1}", My.Computer.FileSystem.CurrentDirectory, "OperationDB.mdb")
        Dim currentDate As Date = My.Computer.Clock.GmtTime.AddHours(3)
        Dim copyFileName As String = String.Format("OperationDB-{0}.mdb", currentDate.ToString("dd.MM.yyyy--HH-mm-ss-ffff"))
        Dim destinationPath As String = String.Format("{0}\Архив\{1}", My.Computer.FileSystem.CurrentDirectory, copyFileName)

        Try
            My.Computer.FileSystem.CopyFile(currentPath, destinationPath)

            MessageBox.Show("Копия базы данных была успешно создана. Имя копии: " + String.Format("OperationDB-{0}.mdb", currentDate.ToString("dd.MM.yyyy--HH-mm-ss-ffff")))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearOperationLogTable()
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand

        connection = OperationLogTableAdapter1.Connection

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.CommandText = "DELETE FROM OperationLog;"

            Dim rowsAffected As Integer = 0

            rowsAffected = sqlCommand.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show(String.Format("Таблица OperationLog была успешно очищена, количество очищенных записей: {0}", rowsAffected), "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Таблица OperationLog была пустой или не удалось очистить данные!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub restoreDbFromFileName(fileName As String)
        Dim currentFilePath As String = String.Format("{0}\{1}", My.Computer.FileSystem.CurrentDirectory, "OperationDB.mdb")

        Try
            My.Computer.FileSystem.CopyFile(fileName, currentFilePath, True)
            MessageBox.Show("База данных была успешно восстановлена!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message + Environment.NewLine + "Не удалось восстановить базу данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ВыбратьФайлToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ВыбратьФайлToolStripMenuItem.Click
        Dim fileDialog As OpenFileDialog = New OpenFileDialog()

        Try
            fileDialog.InitialDirectory = My.Computer.FileSystem.CurrentDirectory
            fileDialog.Filter = "Microsoft Database files (*.mdb)|*.mdb"

            Dim userChoice As DialogResult = fileDialog.ShowDialog()

            If userChoice = DialogResult.OK Then
                Dim selectedFile As String = fileDialog.FileName

                restoreDbFromFileName(selectedFile)
            Else
                Return
            End If
        Catch ex As ArgumentException
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub СоздатьКопиюБДToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles СоздатьКопиюБДToolStripMenuItem.Click
        backUpCurrentDB()
    End Sub

    Private Sub АвтоToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles АвтоToolStripMenuItem.Click
        Dim userChoice As DialogResult = MessageBox.Show("Вы уверены, что хотите восстановить базу данных по актуальной копии? Текущая копия и изменения в ней будут утрачены!", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)

        If userChoice = DialogResult.OK Then
            Dim autoRestoreFilePath As String = String.Format("{0}\Архив", My.Computer.FileSystem.CurrentDirectory)

            Try
                Dim directoryExists As Boolean = My.Computer.FileSystem.DirectoryExists(autoRestoreFilePath)

                If directoryExists Then
                    Dim directoryInfo As IO.DirectoryInfo = My.Computer.FileSystem.GetDirectoryInfo(autoRestoreFilePath)

                    If directoryInfo.GetFiles.Length > 0 Then
                        Dim autoFileName As String = directoryInfo.EnumerateFiles("*.mdb").OrderByDescending(Function(f) f.CreationTime).First().FullName

                        restoreDbFromFileName(autoFileName)
                    ElseIf directoryInfo.GetFiles.Length = 0 Then
                        MessageBox.Show("В папке 'Архив' отсутствуют копии базы данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Папка 'Архив' отсутствует! Она будет создана автоматически после закрытия данного окна. База данных не будет восстановлена.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    My.Computer.FileSystem.CreateDirectory("Архив")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Return
        End If
    End Sub

    Private Sub ОчиститьАрхивКопийБДToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ОчиститьАрхивКопийБДToolStripMenuItem.Click
        Dim archiveDirectory As String = String.Format("{0}\Архив", My.Computer.FileSystem.CurrentDirectory)

        If My.Computer.FileSystem.DirectoryExists(archiveDirectory) Then
            Try
                Dim directoryInfo As IO.DirectoryInfo = My.Computer.FileSystem.GetDirectoryInfo(archiveDirectory)
                Dim allFilesCount As Integer = directoryInfo.GetFiles.Count()

                If allFilesCount > 0 Then
                    Dim filesAvailableForCleanUp As Integer = allFilesCount - 1

                    If filesAvailableForCleanUp = 0 Then
                        MessageBox.Show("Нет файлов доступных для очистки!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf filesAvailableForCleanUp > 0 Then
                        Dim prompt As String = String.Format("Доступно {0} файлов для очистки. Введите количество файлов, необходимое для очистки:", filesAvailableForCleanUp)
                        Dim userInput As String = InputBox(prompt, "Очистка архива")
                        Dim fileCleanUpAmount As Integer

                        If userInput = "0" Or userInput = "" Then
                            Return
                        Else
                            If Integer.TryParse(userInput, fileCleanUpAmount) Then
                                If Not (fileCleanUpAmount >= allFilesCount) Then
                                    For index As Integer = 0 To fileCleanUpAmount - 1
                                        Dim fullFileName As String = directoryInfo.EnumerateFiles("*.mdb").OrderByDescending(Function(f) f.CreationTime).Last().FullName

                                        My.Computer.FileSystem.DeleteFile(fullFileName)
                                    Next

                                    MessageBox.Show("Архив был успешно очищен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information) 'completion message
                                Else
                                    MessageBox.Show("Указанное количество превышает доступное количество файлов для очистки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            Else
                                MessageBox.Show("Введен неверный формат числа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Папка 'Архив' отсутствует! Она будет создана автоматически после закрытия данного окна.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            My.Computer.FileSystem.CreateDirectory("Архив")
        End If
    End Sub


    Private Sub getCurrentDateDayAndSerialize()
        Dim currentDay As Integer = DateAndTime.Today.Day
        Dim streamCurrentDay As FileStream = Nothing

        Try
            streamCurrentDay = New FileStream("CurrentDay.bin", FileMode.Create)
            Dim binaryFormatter As BinaryFormatter = New BinaryFormatter()

            binaryFormatter.Serialize(streamCurrentDay, currentDay)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(streamCurrentDay) Then
                streamCurrentDay.Close()
            End If
        End Try
    End Sub

    Private Function deserializeCurrentDay() As Integer
        Dim streamBinaryDay As FileStream = Nothing
        Dim deserializedDay As Integer = -1

        Try
            streamBinaryDay = New FileStream("CurrentDay.bin", FileMode.Open)
            Dim binaryFormatter As BinaryFormatter = New BinaryFormatter()
            deserializedDay = binaryFormatter.Deserialize(streamBinaryDay)

        Catch ex As FileNotFoundException
            'doesn't need to be resolved because of a call of getCurrentDateDayAndSerialize in the mainForm_Load method
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(streamBinaryDay) Then
                streamBinaryDay.Close()
            End If
        End Try

        Return deserializedDay
    End Function

    Private Sub mainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        backUpCurrentDbOnTheFirstDayOfTheMonth()

        getCurrentDateDayAndSerialize()
    End Sub

    Private Sub СотрудникиToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles СотрудникиToolStripMenuItem.Click
        Dim employeesForm = New EmployeesForm()
        employeesForm.MdiParent = Me
        employeesForm.Show()
    End Sub

    Private Sub КоэффициентыДопЗатратToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles КоэффициентыДопЗатратToolStripMenuItem.Click
        Dim coefficientsForm = New CoefficientsForm()
        coefficientsForm.MdiParent = Me
        coefficientsForm.Show()
    End Sub

    Private Sub ТехкартаВязаниеToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ТехкартаВязаниеToolStripMenuItem.Click
        Dim coefficientsForm = New KnittingRoutingForm()
        coefficientsForm.MdiParent = Me
        coefficientsForm.Show()
    End Sub

    Private Sub НоваяВязальныйЦехToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles НоваяВязальныйЦехToolStripMenuItem.Click
        Dim newKnittingInvoiceForm = New NewKnittingInvoiceForm()
        newKnittingInvoiceForm.MdiParent = Me
        newKnittingInvoiceForm.Show()
    End Sub

    Private Sub НайтиВязальныйЦехToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles НайтиВязальныйЦехToolStripMenuItem.Click
        Dim knittingSearchInvoiceForm = New KnittingSearchInvoiceForm()
        knittingSearchInvoiceForm.MdiParent = Me
        knittingSearchInvoiceForm.Show()
    End Sub
End Class
