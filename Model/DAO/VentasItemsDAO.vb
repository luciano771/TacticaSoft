Imports System.Data.SqlClient
Imports TacticaSoft.TacticaSoft.DTO
Imports System.Data
Imports System.Threading.Tasks

Namespace TacticaSoft.DAO
    Public Class VentasItemsDAO
        Inherits Conexion

        Private Commando As New SqlCommand

        Public Function Read() As List(Of VentasitemsDTO)
            Dim listaventasitems As New List(Of VentasitemsDTO)()
            Try
                Using conexion = ObtenerConexion() ' Obtener la conexión de la clase base
                    conexion.Open()
                    Commando.Connection = conexion
                    Commando.CommandText =
                                               "Select 
                                                vi.ID,
                                                V.ID AS VentaNro,
                                                vi.IDProducto,
	                                            p.nombre,
	                                            vi.Cantidad,
	                                            vi.PrecioUnitario,
	                                            vi.PrecioTotal 
                                            from 
	                                            ventasitems vi,
	                                            clientes c,
	                                            productos p,
	                                            ventas v
                                            where 
	                                            vi.IDProducto = p.ID and
	                                            v.ID = vi.IDVenta and
	                                            v.IDCliente = c.ID"
                    Using reader = Commando.ExecuteReader()
                        While reader.Read()
                            Dim productos As New ProductosDTO()
                            Dim ventasitems As New VentasitemsDTO()
                            ventasitems.id = reader(0).ToString()
                            ventasitems.idventa = reader(1).ToString()
                            productos.ID = reader(2).ToString()
                            productos.nombre = reader(3).ToString()
                            ventasitems.productos = productos
                            ventasitems.cantidad = reader(4).ToString()
                            ventasitems.preciounitario = reader(5).ToString()
                            ventasitems.preciototal = reader(6).ToString()
                            listaventasitems.Add(ventasitems)
                        End While
                    End Using
                    conexion.Close()
                End Using

            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaventasitems
        End Function


        Public Sub InsertarVentasItems(ventasItems As VentasitemsDTO)
            Dim consulta As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@idventa, @idproducto, @preciounitario,@cantidad,@preciototal)"
            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        cmd.Parameters.AddWithValue("@idventa", ventasItems.idventa)
                        cmd.Parameters.AddWithValue("@idproducto", ventasItems.idproducto)
                        cmd.Parameters.AddWithValue("@preciounitario", ventasItems.preciounitario)
                        cmd.Parameters.AddWithValue("@cantidad", ventasItems.cantidad)
                        cmd.Parameters.AddWithValue("@preciototal", ventasItems.preciototal)
                        cmd.ExecuteNonQuery()
                    End Using

                Catch ex As Exception
                    Console.WriteLine("Error al insertar datos en la base de datos: " & ex.Message)

                Finally
                    If conexion.State = ConnectionState.Open Then
                        conexion.Close()
                    End If
                End Try
            End Using
        End Sub

        Public Async Function EliminarItemVenta(ID As Integer) As Task
            Dim consulta As String = "DELETE from ventasitems where id = @id"

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        cmd.Parameters.AddWithValue("@id", ID)
                        Await cmd.ExecuteNonQueryAsync()
                    End Using

                Catch ex As Exception
                    Console.WriteLine("Error al borrar datos en la base de datos: " & ex.Message)

                Finally
                    If conexion.State = ConnectionState.Open Then
                        conexion.Close()
                    End If
                End Try
            End Using
        End Function



        Public Async Function ActualizarProducto(ventasitems As VentasitemsDTO) As Task

            ventasitems.preciototal = ventasitems.cantidad * ventasitems.preciounitario

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Dim consulta As String = "UPDATE ventasitems SET "
                    Dim camposActualizados As New List(Of String)()

                    If Not String.IsNullOrEmpty(ventasitems.cantidad) AndAlso ventasitems.cantidad <> "" Then
                        camposActualizados.Add("Cantidad = @cantidad")
                        camposActualizados.Add("PrecioTotal = @preciototal")
                    End If

                    ' Verifica si hay campos actualizados
                    If camposActualizados.Count > 0 Then
                        consulta += String.Join(", ", camposActualizados)
                        consulta += " WHERE id = @id"

                        Using cmd As New SqlCommand(consulta, conexion)
                            cmd.Parameters.AddWithValue("@id", ventasitems.id)
                            If Not String.IsNullOrEmpty(ventasitems.cantidad) AndAlso ventasitems.cantidad <> "" Then
                                cmd.Parameters.AddWithValue("@cantidad", ventasitems.cantidad)
                                cmd.Parameters.AddWithValue("@preciototal", ventasitems.preciototal)
                            End If

                            Await cmd.ExecuteNonQueryAsync()
                        End Using
                    Else
                        Console.WriteLine("No se proporcionaron campos para actualizar.")
                    End If

                Catch ex As Exception
                    Console.WriteLine("Error al actualizar datos en la base de datos: " & ex.Message)

                Finally
                    If conexion.State = ConnectionState.Open Then
                        conexion.Close()
                    End If
                End Try
            End Using
        End Function



        Public Async Function BuscarPorId(id As Integer) As Task(Of List(Of VentasitemsDTO))
            Dim listaventasitems As New List(Of VentasitemsDTO)()

            Try
                Using conexion = ObtenerConexion()
                    conexion.Open()
                    Dim consulta As String = "SELECT 
                                        vi.ID,
                                        V.ID AS VentaNro,
                                        vi.IDProducto,
	                                    p.nombre,
	                                    vi.Cantidad,
	                                    vi.PrecioUnitario,
	                                    vi.PrecioTotal 
                                    FROM 
	                                    ventasitems vi,
	                                    clientes c,
	                                    productos p,
	                                    ventas v
                                    WHERE 
	                                    vi.IDProducto = p.ID AND
	                                    v.ID = vi.IDVenta AND
	                                    v.IDCliente = c.ID AND
                                        vi.ID = @idventa"

                    Using commando As New SqlCommand(consulta, conexion)
                        commando.Parameters.AddWithValue("@idventa", id)

                        Using reader = Await commando.ExecuteReaderAsync()
                            While reader.Read()
                                Dim Ventasitems As New VentasitemsDTO()
                                Dim productos As New ProductosDTO()

                                Ventasitems.id = reader(0).ToString()
                                Ventasitems.idventa = reader(1).ToString()
                                productos.ID = reader(2).ToString()
                                productos.nombre = reader(3).ToString()
                                Ventasitems.productos = productos
                                Ventasitems.cantidad = reader(4).ToString()
                                Ventasitems.preciounitario = reader(5).ToString()
                                Ventasitems.preciototal = reader(6).ToString()

                                listaventasitems.Add(Ventasitems)
                            End While
                        End Using
                    End Using

                    conexion.Close()
                End Using
            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaventasitems
        End Function


        Public Async Function BuscarPorIdVenta(id As Integer) As Task(Of List(Of VentasitemsDTO))
            Dim listaventasitems As New List(Of VentasitemsDTO)()

            Try
                Using conexion = ObtenerConexion()
                    conexion.Open()
                    Dim consulta As String = "SELECT 
                                        vi.ID,
                                        V.ID AS VentaNro,
                                        vi.IDProducto,
	                                    p.nombre,
	                                    vi.Cantidad,
	                                    vi.PrecioUnitario,
	                                    vi.PrecioTotal 
                                    FROM 
	                                    ventasitems vi,
	                                    clientes c,
	                                    productos p,
	                                    ventas v
                                    WHERE 
	                                    vi.IDProducto = p.ID AND
	                                    v.ID = vi.IDVenta AND
	                                    v.IDCliente = c.ID AND
                                        vi.IDVenta = @idventa"

                    Using commando As New SqlCommand(consulta, conexion)
                        commando.Parameters.AddWithValue("@idventa", id)

                        Using reader = Await commando.ExecuteReaderAsync()
                            While reader.Read()
                                Dim Ventasitems As New VentasitemsDTO()
                                Dim productos As New ProductosDTO()

                                Ventasitems.id = reader(0).ToString()
                                Ventasitems.idventa = reader(1).ToString()
                                productos.ID = reader(2).ToString()
                                productos.nombre = reader(3).ToString()
                                Ventasitems.productos = productos
                                Ventasitems.cantidad = reader(4).ToString()
                                Ventasitems.preciounitario = reader(5).ToString()
                                Ventasitems.preciototal = reader(6).ToString()

                                listaventasitems.Add(Ventasitems)
                            End While
                        End Using
                    End Using

                    conexion.Close()
                End Using
            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaventasitems
        End Function






    End Class
End Namespace
