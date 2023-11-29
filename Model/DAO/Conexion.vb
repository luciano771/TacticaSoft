Imports System.Configuration
Imports System.Data.SqlClient
Namespace TacticaSoft.DAO
    Public Class Conexion

        Protected connectionString As String
        Public Sub New()
            ' Obtener la cadena de conexión desde la configuración
            connectionString = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        End Sub

        Public Function ObtenerConexion() As SqlConnection
            Dim conexion As New SqlConnection(connectionString)
            Return conexion
        End Function

    End Class
End Namespace

