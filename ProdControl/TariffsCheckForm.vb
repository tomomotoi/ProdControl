Public Class TariffsCheckForm
    Dim selectedArticulsDataSource As List(Of Articul) = New List(Of Articul)

    Private Structure Articul
        Private _articulId As Integer
        Private _articulName As String

        Sub New(articulId As Integer, articulName As String)
            Me._articulId = articulId
            Me._articulName = articulName
        End Sub

        Public Property ArticulId() As Integer
            Get
                Return Me._articulId
            End Get
            Set(value As Integer)
                Me._articulId = value
            End Set
        End Property

        Public Property ArticulName() As String
            Get
                Return Me._articulName
            End Get
            Set(value As String)
                Me._articulName = value
            End Set
        End Property
    End Structure

    Private Sub tariffsCheckForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'OperationDBDataSet6.Routing' table. You can move, or remove it, as needed.
        Me.RoutingTableAdapter.Fill(Me.OperationDBDataSet6.Routing)
        'TODO: This line of code loads data into the 'OperationDBDataSet1.Articul' table. You can move, or remove it, as needed.
        Me.ArticulTableAdapter.Fill(Me.OperationDBDataSet1.Articul)

        setupSelectedArticulsListBoxDataSource()
    End Sub

    Private Sub btnCheckTariffs_Click(sender As Object, e As EventArgs) Handles btnCheckTariffs.Click
        If chckbxSelectAll.Checked And selectedArticulsDataSource.Count > 0 Then
            Dim userChoice As DialogResult = MessageBox.Show("Вы уверены, что хотите запустить проверку наличия расценок работ для всех артикулов?", "Уведомление", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

            If userChoice = DialogResult.OK Then
                MessageBox.Show("Идет проверка наличия расценок работ для всех артикулов, пожалуйста подождите окончания операции!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
                calculateTariffs()
                'calculation
            ElseIf userChoice = DialogResult.Cancel Then
                Return
            End If

        ElseIf Not chckbxSelectAll.Checked And selectedArticulsDataSource.Count > 0 Then
            MessageBox.Show("Идет проверка наличия расценок работ для выбранных артикулов, пожалуйста подождите окончания операции!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
            calculateTariffs()
        Else
            MessageBox.Show("Не было выбрано ни одного артикула для проверки расценок операций!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub calculateTariffs()
        Dim selectedArticulsIds As List(Of Integer) = getSelectedArticulsIds()
        Dim connection As OleDb.OleDbConnection
        Dim sqlCommand As OleDb.OleDbCommand
        Dim emptyFound As Boolean 'flag to define information box output text

        connection = ArticulTableAdapter.Connection

        Try
            connection.Open()

            sqlCommand = connection.CreateCommand()
            sqlCommand.CommandType = CommandType.Text

            For Each articulId As Integer In selectedArticulsIds
                sqlCommand.CommandText = String.Format("SELECT Routing.OperationID, Operation.PositionID, Routing.PositionLevel, Routing.DopCost, Routing.TimeExecute, Routing.RateExecute, Routing.Tariff, PositionLevel.PositionRateTime FROM (Routing INNER JOIN Operation ON Routing.OperationID = Operation.OperationID) INNER JOIN PositionLevel ON Routing.PositionLevel = PositionLevel.[PositionLevel] AND Operation.PositionID = PositionLevel.PositionID WHERE ArticulID = {0};", articulId)

                Dim dbReader As OleDb.OleDbDataReader = sqlCommand.ExecuteReader()

                While dbReader.Read()
                    Dim dbData As Object() = New Object() {dbReader("OperationID"),
                        dbReader("PositionID"),
                        dbReader("PositionLevel"),
                        dbReader("DopCost"),
                        dbReader("TimeExecute"),
                        dbReader("RateExecute"),
                        dbReader("Tariff"),
                        dbReader("PositionRateTime")}

                    If dbData(6) = 0 Then
                        Dim operationId As Integer = dbData(0)
                        Dim timeWithCoefficient As Double = dbData(4)
                        Dim tariffPerMinute As Double = dbData(7)

                        Dim tariff As Double = timeWithCoefficient * tariffPerMinute

                        RoutingTableAdapter.Update(tariff, articulId, operationId)

                        emptyFound = True
                    End If
                End While

                dbReader.Close()
            Next

            If emptyFound Then
                MessageBox.Show("Операция завершена! Были найдены артикулы с операциями без расценки. В базу данных были внесены новые расценки.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show("Операция завершена! Операций без расценок под соответствующие артикулы не было найдено.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Function getSelectedArticulsIds() As List(Of Integer)
        Dim selectedArticulsIds As List(Of Integer) = New List(Of Integer)

        For Each item As Articul In lbSelectedArticuls.Items
            Dim articulId = item.ArticulId

            selectedArticulsIds.Add(articulId)
        Next

        Return selectedArticulsIds
    End Function

    Private Sub setupSelectedArticulsListBoxDataSource()
        lbSelectedArticuls.DataSource = selectedArticulsDataSource
        lbSelectedArticuls.DisplayMember = "ArticulName"
        lbSelectedArticuls.ValueMember = "ArticulId"
    End Sub

    Private Sub selectArticulButton_Click(sender As Object, e As EventArgs) Handles btnSelectArticul.Click
        Dim selectedArticulId = cbArticul.SelectedValue
        Dim selectedArticulValue = cbArticul.GetItemText(cbArticul.SelectedItem)

        Dim newItem As Articul = New Articul(selectedArticulId, selectedArticulValue)

        If Not selectedArticulsDataSource.Contains(newItem) Then
            selectedArticulsDataSource.Add(newItem)
        Else
            Return
        End If
        lbSelectedArticuls.DataSource = Nothing
        setupSelectedArticulsListBoxDataSource()
    End Sub

    Private Sub btnRemoveFromList_Click(sender As Object, e As EventArgs) Handles btnRemoveFromList.Click
        Dim selectedArticulFromListBox As Articul = lbSelectedArticuls.SelectedItem

        selectedArticulsDataSource.Remove(selectedArticulFromListBox)
        lbSelectedArticuls.DataSource = Nothing
        setupSelectedArticulsListBoxDataSource()
    End Sub

    Private Sub chckbxSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chckbxSelectAll.CheckedChanged
        clearSelectedArticulsList()

        If DirectCast(sender, CheckBox).Checked Then
            For Each item As Object In cbArticul.Items
                Dim articulId = item.Row(0)
                Dim articulName = item.Row(1)

                Dim newItem As Articul = New Articul(articulId, articulName)

                selectedArticulsDataSource.Add(newItem)
            Next

            setupSelectedArticulsListBoxDataSource()
        End If
    End Sub

    Private Sub btnClearList_Click(sender As Object, e As EventArgs) Handles btnClearList.Click
        clearSelectedArticulsList()

        If chckbxSelectAll.Checked Then
            chckbxSelectAll.Checked = False
        End If
    End Sub

    Private Sub clearSelectedArticulsList()
        selectedArticulsDataSource.Clear()
        lbSelectedArticuls.DataSource = Nothing
    End Sub
End Class