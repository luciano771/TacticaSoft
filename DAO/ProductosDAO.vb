Imports System.Data.SqlClient
Imports TacticaSoft.TacticaSoft.DTO
Imports System.Data
Namespace TacticaSoft.DAO
    Public Class ProductosDAO
        Inherits Conexion

        Private Commando As New SqlCommand

        Public Function Read() As List(Of ProductosDTO)
            Dim listaProducto As New List(Of ProductosDTO)()
            Try
                Using conexion = ObtenerConexion() ' Obtener la conexión de la clase base
                    Try
                        conexion.Open()

                    Catch ex As Exception
                        Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
                    End Try
                    Commando.Connection = conexion
                    Commando.CommandText = "Select * from productos"

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
                    conexion.Close()
                End Using

            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaProducto
        End Function


        Public Sub InsertarProducto(producto As ProductosDTO)
            Dim consulta As String = "INSERT INTO productos (nombre, precio, categoria) VALUES (@nombre, @Telefono, @Correo)"

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        cmd.Parameters.AddWithValue("@nombre", producto.nombre)
                        cmd.Parameters.AddWithValue("@Telefono", producto.precio)
                        cmd.Parameters.AddWithValue("@Correo", producto.categoria)
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
            Dim consulta As String = "DELETE from productos where id = @id"

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

        Public Sub ActualizarProducto(producto As ProductosDTO)
            Dim consulta As String = "UPDATE productos SET "

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        Dim camposActualizados As New List(Of String)()

                        If Not String.IsNullOrEmpty(producto.nombre) Or producto.nombre <> "" Then
                            camposActualizados.Add("Nombre = @nombre")
                            cmd.Parameters.AddWithValue("@nombre", producto.nombre)
                        End If

                        If Not String.IsNullOrEmpty(producto.precio) Or producto.precio <> "" AndAlso Single.TryParse(producto.precio, Nothing) Then
                            camposActualizados.Add("precio = @precio")
                            cmd.Parameters.Add("@precio", SqlDbType.Float).Value = Single.Parse(producto.precio)
                        End If

                        If Not String.IsNullOrEmpty(producto.categoria) Or producto.categoria <> "" Then
                            camposActualizados.Add("Categoria = @categoria")
                            cmd.Parameters.AddWithValue("@categoria", producto.categoria)
                        End If

                        consulta += String.Join(", ", camposActualizados)
                        consulta += " WHERE ID = @id"

                        cmd.Parameters.AddWithValue("@id", producto.ID)
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
                Dim consulta As String = "SELECT * FROM productos WHERE nombre LIKE '%' + @nombre + '%'"

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
