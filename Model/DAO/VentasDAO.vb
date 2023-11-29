Imports System.Data.SqlClient
Imports TacticaSoft.TacticaSoft.DTO
Imports System.Data
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
                    Commando.CommandText = "Select * from ventas"
                    Using reader = Commando.ExecuteReader()
                        While reader.Read()
                            Dim ventas As New VentasDTO()
                            ventas.ID = Convert.ToInt32(reader(0))
                            ventas.idcliente = reader(1).ToString()
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


        Public Sub InsertarProducto(ventas As VentasDTO)
            Dim consulta As String = "INSERT INTO ventas (IDCliente, Fecha, Total) VALUES (@idcliente, @fecha, @total)"

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        cmd.Parameters.AddWithValue("@idcliente", ventas.idcliente)
                        cmd.Parameters.AddWithValue("@fecha", ventas.fecha)
                        cmd.Parameters.AddWithValue("@total", ventas.total)
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

        Public Sub EliminarProducto(ID As Integer)
            Dim consulta As String = "DELETE from ventas where id = @id"

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        cmd.Parameters.AddWithValue("@id", ID)
                        cmd.ExecuteNonQuery()
                    End Using

                Catch ex As Exception
                    Console.WriteLine("Error al borrar datos en la base de datos: " & ex.Message)

                Finally
                    If conexion.State = ConnectionState.Open Then
                        conexion.Close()
                    End If
                End Try
            End Using
        End Sub

        Public Sub ActualizarProducto(ventas As VentasDTO)
            Dim consulta As String = "UPDATE ventas SET "

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()
                    Using cmd As New SqlCommand(consulta, conexion)
                        Dim camposActualizados As New List(Of String)()

                        If Not String.IsNullOrEmpty(ventas.idcliente) Or ventas.idcliente <> "" Then
                            camposActualizados.Add("IDCliente = @idcliente")
                            cmd.Parameters.AddWithValue("@idcliente", ventas.idcliente)
                        End If

                        If Not String.IsNullOrEmpty(ventas.fecha) Or ventas.fecha <> "" AndAlso Single.TryParse(ventas.fecha, Nothing) Then
                            camposActualizados.Add("Fecha = @fecha")
                            cmd.Parameters.Add("@fecha", SqlDbType.Float).Value = Single.Parse(ventas.fecha)
                        End If

                        If Not String.IsNullOrEmpty(ventas.total) Or ventas.total <> "" Then
                            camposActualizados.Add("Total = @total")
                            cmd.Parameters.AddWithValue("@total", ventas.total)
                        End If

                        consulta += String.Join(", ", camposActualizados)
                        consulta += " WHERE ID = @id"

                        cmd.Parameters.AddWithValue("@id", ventas.ID)
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





    End Class
End Namespace
