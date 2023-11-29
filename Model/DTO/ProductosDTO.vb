Namespace TacticaSoft.DTO
    Public Class ProductosDTO

        Private _id As Integer
        Private _nombre As String
        Private _precio As String
        Private _categoria As String

        Public Property ID As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        Public Property nombre As String
            Get
                Return _nombre
            End Get
            Set(value As String)
                _nombre = value
            End Set
        End Property

        Public Property precio As String
            Get
                Return _precio
            End Get
            Set(value As String)
                _precio = value
            End Set
        End Property

        Public Property categoria As String
            Get
                Return _categoria
            End Get
            Set(value As String)
                _categoria = value
            End Set
        End Property




    End Class
End Namespace
