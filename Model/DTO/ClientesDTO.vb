Namespace TacticaSoft.DTO
    Public Class ClientesDTO

        Private _id As Integer
        Private _cliente As String
        Private _telefono As String
        Private _correo As String

        Public Property ID As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        Public Property cliente As String
            Get
                Return _cliente
            End Get
            Set(value As String)
                _cliente = value
            End Set
        End Property

        Public Property telefono As String
            Get
                Return _telefono
            End Get
            Set(value As String)
                _telefono = value
            End Set
        End Property

        Public Property correo As String
            Get
                Return _correo
            End Get
            Set(value As String)
                _correo = value
            End Set
        End Property




    End Class
End Namespace
