﻿Namespace TacticaSoft.DTO
    Public Class VentasitemsDTO
        Private _id As Integer
        Private _idventa As String
        Private _idproducto As String
        Private _preciounitario As String
        Private _cantidad As String
        Private _preciototal As String
        Private _ventas As VentasDTO
        Private _productos As ProductosDTO
        Public Property id As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        Public Property idventa As String
            Get
                Return _idventa
            End Get
            Set(value As String)
                _idventa = value
            End Set
        End Property

        Public Property idproducto As String
            Get
                Return _idproducto
            End Get
            Set(value As String)
                _idproducto = value
            End Set
        End Property

        Public Property preciounitario As String
            Get
                Return _preciounitario
            End Get
            Set(value As String)
                _preciounitario = value
            End Set
        End Property

        Public Property cantidad As String
            Get
                Return _cantidad
            End Get
            Set(value As String)
                _cantidad = value
            End Set
        End Property

        Public Property preciototal As String
            Get
                Return _preciototal
            End Get
            Set(value As String)
                _preciototal = value
            End Set
        End Property

        Public Property ventas As VentasDTO
            Get
                Return _ventas
            End Get
            Set(value As VentasDTO)
                _ventas = value
            End Set
        End Property

        Public Property productos As ProductosDTO
            Get
                Return _productos
            End Get
            Set(value As ProductosDTO)
                _productos = value
            End Set
        End Property
    End Class

End Namespace
