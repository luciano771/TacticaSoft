Imports System.Data.SqlClient
Imports TacticaSoft.TacticaSoft.DTO
Imports System.Data
Namespace TacticaSoft.DAO
    Public Class ClientesDAO
        Inherits Conexion

        Private Commando As New SqlCommand

        Public Function Read() As List(Of ClientesDTO)
            Dim listaClientes As New List(Of ClientesDTO)()

            Try
                Using conexion = ObtenerConexion() ' Obtener la conexión de la clase base
                    Try
                        conexion.Open()

                    Catch ex As Exception
                        Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
                    End Try
                    Commando.Connection = conexion
                    Commando.CommandText = "Select * from clientes"

                    Using reader = Commando.ExecuteReader()
                        While reader.Read()
                            Dim cliente As New ClientesDTO()
                            cliente.ID = Convert.ToInt32(reader(0))
                            cliente.cliente = reader(1).ToString()
                            cliente.telefono = reader(2).ToString()
                            cliente.correo = reader(3).ToString()
                            listaClientes.Add(cliente)
                        End While
                    End Using
                    conexion.Close()
                End Using

            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaClientes
        End Function


        Public Sub InsertarCliente(cliente As ClientesDTO)
            Dim consulta As String = "INSERT INTO Clientes (Cliente, Telefono, Correo) VALUES (@Cliente, @Telefono, @Correo)"

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        cmd.Parameters.AddWithValue("@Cliente", cliente.cliente)
                        cmd.Parameters.AddWithValue("@Telefono", cliente.telefono)
                        cmd.Parameters.AddWithValue("@Correo", cliente.correo)
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

        Public Sub EliminarCliente(ID As Integer)
            Dim consulta As String = "DELETE from clientes where id = @id"

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

        Public Async Function ActualizarCliente(cliente As ClientesDTO) As Task
            Dim consulta As String = "UPDATE clientes SET "

            Using conexion = ObtenerConexion()
                Try
                    conexion.Open()

                    Using cmd As New SqlCommand(consulta, conexion)
                        Dim camposActualizados As New List(Of String)()

                        If Not String.IsNullOrEmpty(cliente.cliente) Or cliente.cliente <> "" Then
                            camposActualizados.Add("cliente = @cliente")
                            cmd.Parameters.AddWithValue("@cliente", cliente.cliente)
                        End If

                        If Not String.IsNullOrEmpty(cliente.telefono) Or cliente.telefono <> "" Then
                            camposActualizados.Add("telefono = @telefono")
                            cmd.Parameters.AddWithValue("@telefono", cliente.telefono)
                        End If

                        If Not String.IsNullOrEmpty(cliente.correo) Or cliente.correo <> "" Then
                            camposActualizados.Add("correo = @correo")
                            cmd.Parameters.AddWithValue("@correo", cliente.correo)
                        End If

                        consulta += String.Join(", ", camposActualizados)
                        consulta += " WHERE id = @id"

                        cmd.Parameters.AddWithValue("@id", cliente.ID)
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

        Public Function Buscar(clientes As String) As List(Of ClientesDTO)
            Dim listaClientes As New List(Of ClientesDTO)()

            Try
                Dim consulta As String = "SELECT * FROM clientes WHERE cliente LIKE '%' + @clientes + '%'"

                Using conexion = ObtenerConexion()
                    conexion.Open()

                    Using Commando As New SqlCommand(consulta, conexion)
                        Commando.Parameters.AddWithValue("@clientes", clientes)

                        Using reader = Commando.ExecuteReader()
                            While reader.Read()
                                Dim cliente As New ClientesDTO()
                                cliente.ID = Convert.ToInt32(reader(0))
                                cliente.cliente = reader(1).ToString()
                                cliente.telefono = reader(2).ToString()
                                cliente.correo = reader(3).ToString()
                                listaClientes.Add(cliente)
                            End While
                        End Using
                    End Using

                    conexion.Close()
                End Using
            Catch ex As Exception
                Console.WriteLine("Error al obtener datos de la base de datos: " & ex.Message)
            End Try

            Return listaClientes
        End Function



    End Class
End Namespace
