Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO


Namespace TacticaSoft.Funciones
    Public Class FuncVentas
        Public Shared dtProductos As DataTable
        Public Shared idcliente As Integer
        Public Sub verVentas(data As DataGridView)

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


            data.AllowUserToAddRows = False
            data.DataSource = dt
        End Sub


        Public Sub VerVentasItems(data As DataGridView)
            Dim DAO As New VentasItemsDAO
            Dim listaVentasItems As List(Of VentasitemsDTO) = DAO.Read()

            Dim dt As New DataTable()
            dt.Columns.Add("IDItemVenta")
            dt.Columns.Add("NroVenta")
            dt.Columns.Add("IDProducto")
            dt.Columns.Add("producto")
            dt.Columns.Add("cantidad")
            dt.Columns.Add("preciounitario")
            dt.Columns.Add("preciototal")

            For Each item In listaVentasItems
                dt.Rows.Add(item.id, item.idventa, item.productos.ID, item.productos.nombre, item.cantidad, item.preciounitario, item.preciototal)
            Next

            data.AllowUserToAddRows = False
            data.DataSource = dt
        End Sub


        Public Sub insertarventas()

            Dim ventas As New VentasDTO
            Dim ventasDAO As New VentasDAO

            Dim ventasitems As New VentasitemsDTO
            Dim ventasitemsDAO As New VentasItemsDAO

            Dim productos As New ProductosDTO
            Dim productosDAO As New ProductosDAO

            Dim precioVenta As Integer

            For Each row As DataRow In dtProductos.Rows
                precioVenta = precioVenta + row("Precio")
            Next


            ventas.idcliente = idcliente
            ventas.fecha = DateTime.Now
            ventas.total = precioVenta
            Dim IDVenta = ventasDAO.InsertarVenta(ventas)


            For Each row As DataRow In dtProductos.Rows
                ventasitems.idventa = IDVenta
                ventasitems.idproducto = row("ID")
                ventasitems.cantidad = row("Cantidad")
                ventasitems.preciototal = row("Precio")
                ventasitems.preciounitario = row("Precio") / row("Cantidad")
                ventasitemsDAO.InsertarVentasItems(ventasitems)
            Next


        End Sub



    End Class
End Namespace