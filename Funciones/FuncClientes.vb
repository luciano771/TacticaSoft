Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO


Namespace TacticaSoft.Funciones
    Public Class FuncCliente

        Public Sub verClientes(data As DataGridView)

            Dim DAO As New ClientesDAO
            Dim clientes As List(Of ClientesDTO) = DAO.Read()
            Dim dt As New DataTable()
            dt.Columns.Add("ID")
            dt.Columns.Add("Nombre")
            dt.Columns.Add("Telefono")
            dt.Columns.Add("Correo")




            For Each item In clientes
                dt.Rows.Add(item.ID, item.cliente, item.telefono, item.correo)
            Next


            data.AllowUserToAddRows = False
            data.DataSource = dt
        End Sub


        Public Async Sub actualizarClientes(ID, cliente, telefono, correo)
            Try
                Dim DAO As New ClientesDAO()
                Dim DTO As New ClientesDTO()
                DTO.ID = ID
                DTO.cliente = cliente
                DTO.telefono = telefono
                DTO.correo = correo
                Await DAO.ActualizarCliente(DTO)
            Catch ex As Exception
                Console.WriteLine("Error al insertar registros: " & ex.Message)
            End Try
        End Sub


        Public Sub InsertarCliente(cliente, telefono, correo)

            Try
                Dim DAO As New ClientesDAO()
                Dim DTO As New ClientesDTO()
                DTO.cliente = cliente
                DTO.telefono = telefono
                DTO.correo = correo
                DAO.InsertarCliente(DTO)
            Catch ex As Exception
                Console.WriteLine("Error al insertar registros: " & ex.Message)
            End Try

        End Sub



    End Class
End Namespace