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
                            productos.nombre = reader(2).ToString()
                            ventasitems.productos = productos
                            ventasitems.cantidad = reader(3).ToString()
                            ventasitems.preciounitario = reader(4).ToString()
                            ventasitems.preciototal = reader(5).ToString()
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


        Public Sub ActualizarProducto(ventasitems As VentasitemsDTO)
            Dim consulta As String = "UPDATE ventas SET "

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()
                    Using cmd As New SqlCommand(consulta, conexion)
                        Dim camposActualizados As New List(Of String)()

                        If Not String.IsNullOrEmpty(ventasitems.idventa) Or ventasitems.idventa <> "" Then
                            camposActualizados.Add("IDVenta = @idventa")
                            cmd.Parameters.AddWithValue("@idcliente", ventasitems.idventa)
                        End If

                        If Not String.IsNullOrEmpty(ventasitems.idproducto) Or ventasitems.idproducto <> "" AndAlso Single.TryParse(ventasitems.idproducto, Nothing) Then
                            camposActualizados.Add("IDProducto = @idproducto")
                            cmd.Parameters.Add("@fecha", SqlDbType.Float).Value = Single.Parse(ventasitems.idproducto)
                        End If

                        If Not String.IsNullOrEmpty(ventasitems.preciounitario) Or ventasitems.preciounitario <> "" Then
                            camposActualizados.Add("PrecioUnitario = @preciounitario")
                            cmd.Parameters.AddWithValue("@preciounitario", ventasitems.preciounitario)
                        End If
                        If Not String.IsNullOrEmpty(ventasitems.cantidad) Or ventasitems.cantidad <> "" Then
                            camposActualizados.Add("Cantidad = @cantidad")
                            cmd.Parameters.AddWithValue("@cantidad", ventasitems.cantidad)
                        End If
                        If Not String.IsNullOrEmpty(ventasitems.preciototal) Or ventasitems.preciototal <> "" Then
                            camposActualizados.Add("PrecioTotal = @preciototal")
                            cmd.Parameters.AddWithValue("@preciototal", ventasitems.preciototal)
                        End If

                        consulta += String.Join(", ", camposActualizados)
                        consulta += " WHERE ID = @id"

                        cmd.Parameters.AddWithValue("@id", ventasitems.id)
                        cmd.CommandText = consulta

                        cmd.ExecuteNonQuery()
                    End Using

                Catch ex As Exception
                    Console.WriteLine("Error al actualizar datos en la base de datos: " & ex.Message)

                Finally
                    If conexion.State = ConnectionState.Open Then
                        conexion.Close()
                    End If
                End Try
            End Using
        End Sub


        Public Function BuscarPorId(id As Integer) As List(Of VentasitemsDTO)
            Dim listaventasitems As New List(Of VentasitemsDTO)()
            Try
                Using conexion = ObtenerConexion() ' Obtener la conexión de la clase base
                    conexion.Open()
                    Commando.Connection = conexion
                    Commando.CommandText = "select * from ventasitems WHERE id = @id"
                    Commando.Parameters.AddWithValue("@id", id)
                    Using reader = Commando.ExecuteReader()
                        While reader.Read()
                            Dim ventasitems As New VentasitemsDTO()
                            ventasitems.id = reader(0).ToString()
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





    End Class
End Namespace
