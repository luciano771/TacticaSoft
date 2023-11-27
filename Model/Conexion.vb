Imports System.Configuration
Imports System.Data.SqlClient
Namespace TacticaSoft
    Public Class Conexion

        Protected sqlConnectionString As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString

    End Class
End Namespace

