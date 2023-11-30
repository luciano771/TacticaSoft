Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO
Imports System.Threading.Tasks


Namespace TacticaSof

    Public Class Ventas
        Inherits Form

        Public IDCliente As Integer
        Public dt As DataTable
        Public ID_ITEM_VENTA As Integer
        Public ID_VENTA As Integer


        Public Sub New()
            InitializeComponent()
            VerVentasItems()
            verVentas()
        End Sub

        Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
            ID_ITEM_VENTA = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            ID_VENTA = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        End Sub
        Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            Dim DAO As New VentasItemsDAO
            Dim DAOItems As New VentasDAO
            Await DAO.EliminarItemVenta(ID_ITEM_VENTA)
            Await DAOItems.EliminarVenta(ID_VENTA)
            VerVentasItems()
            verVentas()
        End Sub



        Private Sub verVentas()
            Dim DAO As New VentasDAO
            Dim ventas As List(Of VentasDTO) = DAO.Read()
            Dim dt As New DataTable()
            dt.Columns.Add("ID")
            dt.Columns.Add("Cliente")
            dt.Columns.Add("Fecha")
            dt.Columns.Add("Total")


            For Each item In ventas
                dt.Rows.Add(item.ID, item.clientes.cliente, item.fecha, item.total)
            Next

            DataGridView2.AllowUserToAddRows = False
            DataGridView2.DataSource = dt
        End Sub


        Private Sub VerVentasItems()
            Dim DAO As New VentasItemsDAO
            Dim listaVentasItems As List(Of VentasitemsDTO) = DAO.Read()

            Dim dt As New DataTable()
            dt.Columns.Add("IDItemVenta")
            dt.Columns.Add("NroVenta")
            dt.Columns.Add("producto")
            dt.Columns.Add("cantidad")
            dt.Columns.Add("preciounitario")
            dt.Columns.Add("preciototal")

            For Each item In listaVentasItems
                dt.Rows.Add(item.id, item.idventa, item.productos.nombre, item.cantidad, item.preciounitario, item.preciototal)
            Next

            DataGridView1.AllowUserToAddRows = False

            DataGridView1.DataSource = dt
        End Sub


        Private Sub insertarventas()

            Dim ventas As New VentasDTO
            Dim ventasDAO As New VentasDAO

            Dim ventasitems As New VentasitemsDTO
            Dim ventasitemsDAO As New VentasItemsDAO

            Dim productos As New ProductosDTO
            Dim productosDAO As New ProductosDAO

            Dim precioVenta As Integer

            For Each row As DataRow In dt.Rows
                precioVenta = precioVenta + row("Precio")

            Next


            ventas.idcliente = IDCliente
            ventas.fecha = DateTime.Now
            ventas.total = precioVenta
            Dim IDVenta = ventasDAO.InsertarVenta(ventas)


            For Each row As DataRow In dt.Rows
                ventasitems.idventa = IDVenta
                ventasitems.idproducto = row("ID")
                ventasitems.cantidad = row("Cantidad")
                ventasitems.preciototal = row("Precio")
                ventasitems.preciounitario = row("Precio") / row("Cantidad")
                ventasitemsDAO.InsertarVentasItems(ventasitems)
            Next


            IDCliente = ""
            dt = Nothing

            My.Forms.TacticaSoft_TacticaSoft_Productos.dt = Nothing
            My.Forms.TacticaSoft_TacticaSoft_Clientes.ID = 0


            VerVentasItems()
            verVentas()







        End Sub


        Private Sub TextBox2_TextChanged_2(sender As Object, e As EventArgs) Handles TextBox2.Click

            My.Forms.TacticaSoft_TacticaSoft_Productos.Show()
        End Sub
        Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.Click
            My.Forms.TacticaSoft_TacticaSoft_Clientes.Show()
        End Sub
        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            insertarventas()
        End Sub


        Friend WithEvents DataGridView1 As DataGridView

        Private Sub InitializeComponent()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.TextBox2 = New System.Windows.Forms.TextBox()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.DataGridView2 = New System.Windows.Forms.DataGridView()
            Me.Button2 = New System.Windows.Forms.Button()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DataGridView1
            '
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView1.Location = New System.Drawing.Point(12, 12)
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.Size = New System.Drawing.Size(449, 428)
            Me.DataGridView1.TabIndex = 0
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(479, 12)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Alta"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'TextBox2
            '
            Me.TextBox2.Location = New System.Drawing.Point(680, 15)
            Me.TextBox2.Name = "TextBox2"
            Me.TextBox2.Size = New System.Drawing.Size(100, 20)
            Me.TextBox2.TabIndex = 3
            Me.TextBox2.Text = "Producto"
            '
            'TextBox1
            '
            Me.TextBox1.Location = New System.Drawing.Point(574, 15)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(100, 20)
            Me.TextBox1.TabIndex = 5
            Me.TextBox1.Text = "Cliente"
            '
            'DataGridView2
            '
            Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView2.Location = New System.Drawing.Point(467, 233)
            Me.DataGridView2.Name = "DataGridView2"
            Me.DataGridView2.Size = New System.Drawing.Size(525, 207)
            Me.DataGridView2.TabIndex = 6
            '
            'Button2
            '
            Me.Button2.Location = New System.Drawing.Point(479, 63)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(75, 23)
            Me.Button2.TabIndex = 7
            Me.Button2.Text = "Baja"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Ventas
            '
            Me.ClientSize = New System.Drawing.Size(1121, 496)
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


    End Class
End Namespace