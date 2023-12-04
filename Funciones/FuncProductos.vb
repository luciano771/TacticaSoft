Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO


Namespace TacticaSoft.Funciones
    Public Class FuncProductos

        Public Function GrillaSeleccionarProductos(dt As DataTable, ID_Producto As String, nombre As String, precio As String)

            If dt Is Nothing Then
                dt = New DataTable()
                dt.Columns.Add("ID")
                dt.Columns.Add("Nombre")
                dt.Columns.Add("Cantidad")
                dt.Columns.Add("Precio")
            End If


            Dim found As Boolean = False

            For Each row As DataRow In dt.Rows
                If Convert.ToInt32(row("ID")) = ID_Producto Then
                    row("Cantidad") = Convert.ToInt32(row("Cantidad")) + 1
                    row("Precio") = Convert.ToInt32(row("Cantidad")) * Convert.ToDecimal(row("Precio"))
                    found = True
                    Exit For
                End If
            Next

            If Not found Then
                Dim newRow As DataRow = dt.NewRow()
                newRow("ID") = ID_Producto
                newRow("Cantidad") = 1
                newRow("Nombre") = nombre
                newRow("Precio") = precio
                dt.Rows.Add(newRow)
            End If

            Return dt
        End Function


        Public Sub VerProductos(data As DataGridView)
            Dim DAO As New ProductosDAO()
            Dim dt As New DataTable
            Dim productos As List(Of ProductosDTO)
            productos = DAO.Read()

            dt.Columns.Add("ID")
            dt.Columns.Add("Nombre")
            dt.Columns.Add("Precio")
            dt.Columns.Add("Categoria")

            For Each item In productos
                dt.Rows.Add(item.ID, item.nombre, item.precio, item.categoria)
            Next

            data.AllowUserToAddRows = False
            data.DataSource = dt
        End Sub


        Public Sub AgregarProducto(data As DataGridView, nombre As String, precio As String, categoria As String)

            Dim DAO As New ProductosDAO()
            Dim DTO As New ProductosDTO()
            DTO.nombre = nombre
            DTO.precio = precio
            DTO.categoria = categoria
            DAO.InsertarProducto(DTO)

        End Sub

        Public Sub ModificarProducto(ID As Integer, nombre As String, precio As String, categoria As String)

            Dim DAO As New ProductosDAO()
            Dim DTO As New ProductosDTO()
            DTO.ID = ID
            DTO.nombre = nombre
            DTO.precio = precio
            DTO.categoria = categoria
            DAO.ActualizarProducto(DTO)
        End Sub


    End Class
End Namespace