Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO
Imports System.Threading.Tasks


Namespace TacticaSoft

    Public Class Ventas
        Inherits Form

        Public FuncionesVentas As New Funciones.FuncVentas()
        Public IDCliente As Integer
        Public ID_ITEM_VENTA As Integer
        Public ID_VENTA As Integer
        Public IDProducto As Integer
        Public cantidad As Integer
        Public PrecioUnitario As Integer
        Public FechaInicio As DateTime
        Public FechaFin As DateTime
        Public cliente As String


        Public Sub New()
            InitializeComponent()
            FuncionesVentas.VerVentasItems(DataGridView1)
            FuncionesVentas.verVentas(DataGridView2)
        End Sub

        Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing Then
                ID_ITEM_VENTA = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                ID_VENTA = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                IDProducto = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                cantidad = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                TextBox4.Text = cantidad
                PrecioUnitario = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            End If


        End Sub
        Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

            Dim DAO As New VentasItemsDAO
            Dim DAOItems As New VentasDAO
            If (ID_ITEM_VENTA = 0 Or ID_VENTA = 0) Then
                MsgBox("Debe seleccionar un item del listado")
                Return
            End If

            Await DAO.EliminarItemVenta(ID_ITEM_VENTA)
            Await DAOItems.EliminarVenta(ID_VENTA)

            FuncionesVentas.VerVentasItems(DataGridView1)
            FuncionesVentas.verVentas(DataGridView2)

            ID_ITEM_VENTA = 0
            ID_VENTA = 0

        End Sub

        Private Sub TextBox2_TextChanged_2(sender As Object, e As EventArgs) Handles TextBox2.Click
            Form1.ventasForm.DataGridView2.Visible = True
            Dim FormProductos As New TacticaSoft.Productos()
            FormProductos.DataGridView2.Visible = True
            FormProductos.Show()
        End Sub

        Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.Click
            My.Forms.TacticaSoft_TacticaSoft_Clientes.Show()
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            FuncionesVentas.insertarventas()
            DataGridView2.AllowUserToAddRows = False
            DataGridView2.DataSource = Funciones.FuncVentas.dtProductos
            FuncionesVentas.VerVentasItems(DataGridView1)
            FuncionesVentas.verVentas(DataGridView2)
        End Sub


        Private Async Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

            Dim ventasitemsDAO As New VentasItemsDAO
            Dim ventasDAO As New VentasDAO
            Dim ventasitemsDTO As New VentasitemsDTO
            Dim ventasDTO As New VentasDTO

            ventasitemsDTO.id = ID_ITEM_VENTA
            ventasitemsDTO.idventa = ID_VENTA
            ventasitemsDTO.idproducto = IDProducto
            ventasitemsDTO.cantidad = TextBox4.Text
            ventasitemsDTO.preciounitario = PrecioUnitario

            Await ventasitemsDAO.ActualizarProducto(ventasitemsDTO)

            Dim ventas As List(Of VentasitemsDTO) = Await ventasitemsDAO.BuscarPorIdVenta(ventasitemsDTO.idventa)
            Dim total As Integer
            For Each item In ventas
                total += item.preciounitario * item.cantidad
            Next
            ventasDTO.ID = ventasitemsDTO.idventa
            ventasDTO.total = total
            Await ventasDAO.ActualizarProducto(ventasDTO)

            ID_ITEM_VENTA = 0
            ID_VENTA = 0

            FuncionesVentas.VerVentasItems(DataGridView1)
            FuncionesVentas.verVentas(DataGridView2)

        End Sub



        Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
            FechaInicio = DateTimePicker1.Value
        End Sub

        Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
            FechaFin = DateTimePicker2.Value
        End Sub



        Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
            If FechaInicio <> Nothing And FechaFin <> Nothing Then
                Dim dt1 = DataGridView2.DataSource
                Dim vista As New DataView(dt1)
                vista.RowFilter = String.Format("Fecha >= #{0:yyyy/MM/dd}# AND Fecha <= #{1:yyyy/MM/dd}#", FechaInicio, FechaFin)

                DataGridView2.DataSource = vista.ToTable()
            Else
                MsgBox("Debe seleccionar fecha de inicio y fin")
            End If
        End Sub

        Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
            If TextBox6.Text <> "" Then
                Dim dt1 = DataGridView2.DataSource
                Dim vista As New DataView(dt1)
                Dim nombreColumna As String = "Cliente" ' Reemplaza "NombreColumna" por el nombre real de la columna en la que quieres buscar
                vista.RowFilter = $"CONVERT({nombreColumna}, 'System.String') LIKE '%{TextBox6.Text}%'"
                DataGridView2.DataSource = vista.ToTable()
            Else
                FuncionesVentas.verVentas(DataGridView2)
            End If
        End Sub















        Friend WithEvents DataGridView1 As DataGridView

        Private Sub InitializeComponent()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.TextBox2 = New System.Windows.Forms.TextBox()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.DataGridView2 = New System.Windows.Forms.DataGridView()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button3 = New System.Windows.Forms.Button()
            Me.TextBox3 = New System.Windows.Forms.TextBox()
            Me.TextBox4 = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.TextBox6 = New System.Windows.Forms.TextBox()
            Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
            Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
            Me.Button4 = New System.Windows.Forms.Button()
            Me.ComboBox1 = New System.Windows.Forms.ComboBox()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DataGridView1
            '
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView1.Location = New System.Drawing.Point(22, 199)
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.Size = New System.Drawing.Size(542, 241)
            Me.DataGridView1.TabIndex = 0
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(64, 36)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Alta"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'TextBox2
            '
            Me.TextBox2.Location = New System.Drawing.Point(251, 39)
            Me.TextBox2.Name = "TextBox2"
            Me.TextBox2.Size = New System.Drawing.Size(110, 20)
            Me.TextBox2.TabIndex = 3
            Me.TextBox2.Text = "Seleccionar Producto"
            '
            'TextBox1
            '
            Me.TextBox1.Location = New System.Drawing.Point(145, 38)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(100, 20)
            Me.TextBox1.TabIndex = 5
            Me.TextBox1.Text = "Seleccionar Cliente"
            '
            'DataGridView2
            '
            Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView2.Location = New System.Drawing.Point(594, 199)
            Me.DataGridView2.Name = "DataGridView2"
            Me.DataGridView2.Size = New System.Drawing.Size(576, 241)
            Me.DataGridView2.TabIndex = 6
            '
            'Button2
            '
            Me.Button2.Location = New System.Drawing.Point(64, 82)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(75, 23)
            Me.Button2.TabIndex = 7
            Me.Button2.Text = "Baja"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button3
            '
            Me.Button3.Location = New System.Drawing.Point(64, 126)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(75, 23)
            Me.Button3.TabIndex = 8
            Me.Button3.Text = "Modificar"
            Me.Button3.UseVisualStyleBackColor = True
            '
            'TextBox3
            '
            Me.TextBox3.Location = New System.Drawing.Point(145, 129)
            Me.TextBox3.Name = "TextBox3"
            Me.TextBox3.Size = New System.Drawing.Size(100, 20)
            Me.TextBox3.TabIndex = 9
            Me.TextBox3.Text = "Producto"
            '
            'TextBox4
            '
            Me.TextBox4.Location = New System.Drawing.Point(251, 129)
            Me.TextBox4.Name = "TextBox4"
            Me.TextBox4.Size = New System.Drawing.Size(100, 20)
            Me.TextBox4.TabIndex = 10
            Me.TextBox4.Text = "Cantidad"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(614, 83)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(84, 13)
            Me.Label2.TabIndex = 14
            Me.Label2.Text = "Filtrar por cliente"
            '
            'TextBox6
            '
            Me.TextBox6.Location = New System.Drawing.Point(729, 80)
            Me.TextBox6.Name = "TextBox6"
            Me.TextBox6.Size = New System.Drawing.Size(143, 20)
            Me.TextBox6.TabIndex = 15
            '
            'DateTimePicker1
            '
            Me.DateTimePicker1.Location = New System.Drawing.Point(729, 35)
            Me.DateTimePicker1.Name = "DateTimePicker1"
            Me.DateTimePicker1.Size = New System.Drawing.Size(109, 20)
            Me.DateTimePicker1.TabIndex = 18
            '
            'DateTimePicker2
            '
            Me.DateTimePicker2.Location = New System.Drawing.Point(856, 35)
            Me.DateTimePicker2.Name = "DateTimePicker2"
            Me.DateTimePicker2.Size = New System.Drawing.Size(109, 20)
            Me.DateTimePicker2.TabIndex = 19
            '
            'Button4
            '
            Me.Button4.Location = New System.Drawing.Point(594, 32)
            Me.Button4.Name = "Button4"
            Me.Button4.Size = New System.Drawing.Size(119, 23)
            Me.Button4.TabIndex = 20
            Me.Button4.Text = "Filtrar por fecha"
            Me.Button4.UseVisualStyleBackColor = True
            '
            'ComboBox1
            '
            Me.ComboBox1.FormattingEnabled = True
            Me.ComboBox1.Location = New System.Drawing.Point(367, 39)
            Me.ComboBox1.Name = "ComboBox1"
            Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
            Me.ComboBox1.TabIndex = 21
            '
            'Ventas
            '
            Me.ClientSize = New System.Drawing.Size(1202, 475)
            Me.Controls.Add(Me.ComboBox1)
            Me.Controls.Add(Me.Button4)
            Me.Controls.Add(Me.DateTimePicker2)
            Me.Controls.Add(Me.DateTimePicker1)
            Me.Controls.Add(Me.TextBox6)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.TextBox4)
            Me.Controls.Add(Me.TextBox3)
            Me.Controls.Add(Me.Button3)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.DataGridView2)
            Me.Controls.Add(Me.TextBox1)
            Me.Controls.Add(Me.TextBox2)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.DataGridView1)
            Me.Name = "Ventas"
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents Button1 As Button
        Friend WithEvents Button2 As Button



        Friend WithEvents TextBox2 As TextBox
        Friend WithEvents TextBox1 As TextBox
        Friend WithEvents DataGridView2 As DataGridView
        Friend WithEvents Button3 As Button
        Friend WithEvents TextBox3 As TextBox
        Friend WithEvents TextBox4 As TextBox
        Friend WithEvents Label2 As Label
        Friend WithEvents TextBox6 As TextBox


        Friend WithEvents DateTimePicker1 As DateTimePicker
        Friend WithEvents DateTimePicker2 As DateTimePicker
        Friend WithEvents Button4 As Button
        Friend WithEvents ComboBox1 As ComboBox


    End Class
End Namespace