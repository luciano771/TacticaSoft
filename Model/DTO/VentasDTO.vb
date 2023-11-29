Namespace TacticaSoft.DTO
    Public Class VentasDTO
        Private _id As Integer
        Private _idcliente As String
        Private _fecha As String
        Private _total As String

        Public Property ID As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        Public Property idcliente As String
            Get
                Return _idcliente
            End Get
            Set(value As String)
                _idcliente = value
            End Set
        End Property

        Public Property fecha As String
            Get
                Return _fecha
            End Get
            Set(value As String)
                _fecha = value
            End Set
        End Property

        Public Property total As String
            Get
                Return _total
            End Get
            Set(value As String)
                _total = value
            End Set
        End Property

    End Class

End Namespace
