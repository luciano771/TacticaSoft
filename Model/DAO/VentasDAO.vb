Imports System.Data.SqlClient
Imports TacticaSoft.TacticaSoft.DTO
Imports System.Data
Imports System.Threading.Tasks

Namespace TacticaSoft.DAO
    Public Class VentasDAO
        Inherits Conexion

        Private Commando As New SqlCommand

        Public Function Read() As List(Of VentasDTO)
            Dim listaventas As New List(Of VentasDTO)()
            Try
                Using conexion = ObtenerConexion() ' Obtener la conexión de la clase base
                    conexion.Open()
                    Commando.Connection = conexion
                    Commando.CommandText = "Select v.ID,c.cliente,v.fecha,v.total from ventas v,clientes c where v.IDCliente = c.ID"
                    Using reader = Commando.ExecuteReader()
                        While reader.Read()
                            Dim ventas As New VentasDTO()
                            Dim clientes As New ClientesDTO()
                            ventas.ID = reader(0).ToString()
                            clientes.cliente = reader(1).ToString()
                            ventas.clientes = clientes
                            ventas.fecha = reader(2).ToString()
                            ventas.total = reader(3).ToString()
                            listaventas.Add(ventas)
                        End While
                    End Using
                    conexion.Close()
                End Using

            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaventas
        End Function


        Public Function InsertarVenta(ventas As VentasDTO) As Integer
            Dim idVentaInsertada As Integer = 0

            Dim consulta As String = "INSERT INTO ventas (IDCliente, Fecha, Total) OUTPUT INSERTED.ID VALUES (@idcliente, @fecha, @total)"

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        cmd.Parameters.AddWithValue("@idcliente", ventas.idcliente)
                        cmd.Parameters.AddWithValue("@fecha", ventas.fecha)
                        cmd.Parameters.AddWithValue("@total", ventas.total)
                        idVentaInsertada = Convert.ToInt32(cmd.ExecuteScalar())
                    End Using

                Catch ex As Exception
                    Console.WriteLine("Error al insertar datos en la base de datos: " & ex.Message)

                Finally
                    If conexion.State = ConnectionState.Open Then
                        conexion.Close()
                    End If
                End Try
            End Using

            Return idVentaInsertada
        End Function




        Public Async Function EliminarVenta(ID As Integer) As Task

            Dim ventasitemsDAO As New VentasItemsDAO()

            Dim ListaItems = Await ventasitemsDAO.BuscarPorId(ID)

            If ListaItems.Count = 0 Then
                Dim consulta As String = "DELETE from ventas where id = @id"

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
            End If
        End Function

        Public Async Function ActualizarProducto(ventas As VentasDTO) As Task
            Dim consulta As String = "UPDATE ventas SET "

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()
                    Using cmd As New SqlCommand(consulta, conexion)
                        Dim camposActualizados As New List(Of String)()

                        If Not String.IsNullOrEmpty(ventas.idcliente) AndAlso ventas.idcliente <> "" Then
                            camposActualizados.Add("IDCliente = @idcliente")
                            cmd.Parameters.AddWithValue("@idcliente", ventas.idcliente)
                        End If

                        If Not String.IsNullOrEmpty(ventas.total) AndAlso ventas.total <> "" Then
                            camposActualizados.Add("Total = @total")
                            cmd.Parameters.AddWithValue("@total", ventas.total)
                        End If

                        consulta += String.Join(", ", camposActualizados)
                        consulta += " WHERE ID = @id"

                        cmd.Parameters.AddWithValue("@id", ventas.ID)
                        cmd.CommandText = consulta

                        Await cmd.ExecuteNonQueryAsync()
                    End Using

                Catch ex As Exception
                    Console.WriteLine("Error al actualizar datos en la base de datos: " & ex.Message)

                Finally
                    If conexion.State = ConnectionState.Open Then
                        conexion.Close()
                    End If
                End Try
            End Using
        End Function


        Public Function Buscar(nombre As String) As List(Of ProductosDTO)
            Dim listaProducto As New List(Of ProductosDTO)()

            Try
                Dim consulta As String = "SELECT * FROM ventas WHERE IDCliente LIKE '%' + @nombre + '%'"

                Using conexion = ObtenerConexion()
                    conexion.Open()

                    Using Commando As New SqlCommand(consulta, conexion)
                        Commando.Parameters.AddWithValue("@nombre", nombre)

                        Using reader = Commando.ExecuteReader()
                            While reader.Read()
                                Dim producto As New ProductosDTO()
                                producto.ID = Convert.ToInt32(reader(0))
                                producto.nombre = reader(1).ToString()
                                producto.precio = reader(2).ToString()
                                producto.categoria = reader(3).ToString()
                                listaProducto.Add(producto)
                            End While
                        End Using
                    End Using

                    conexion.Close()
                End Using
            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaProducto
        End Function

        Public Async Function BuscarPorId(id As Integer) As Task(Of List(Of VentasDTO))
            Dim listaProducto As New List(Of VentasDTO)()

            Try
                Dim consulta As String = "SELECT * FROM ventas WHERE ID = @id"

                Using conexion = ObtenerConexion()
                    conexion.Open()

                    Using Commando As New SqlCommand(consulta, conexion)
                        Commando.Parameters.AddWithValue("@id", id)

                        Using reader = Await Commando.ExecuteReaderAsync()
                            While reader.Read()
                                Dim ventas As New VentasDTO()
                                Dim clientes As New ClientesDTO()
                                ventas.ID = reader(0).ToString()
                                clientes.cliente = reader(1).ToString()
                                ventas.clientes = clientes
                                ventas.fecha = reader(2).ToString()
                                ventas.total = reader(3).ToString()
                                listaProducto.Add(ventas)
                            End While
                        End Using
                    End Using

                    conexion.Close()
                End Using
            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaProducto
        End Function





    End Class
End Namespace
