Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO


Namespace TacticaSoft

    Public Class Ventas
        Inherits Form

        Public Sub New()
            InitializeComponent()
            VerVentas()
            VerVentasItems()
        End Sub

        Private Sub VerVentas()
            Dim DAO As New VentasDAO
            Dim listaVentas As List(Of VentasDTO) = DAO.Read()

            Dim dt As New DataTable()
            dt.Columns.Add("Nombre")
            dt.Columns.Add("Fecha")
            dt.Columns.Add("Total")

            For Each item In listaVentas
                dt.Rows.Add(item.clientes.cliente, item.fecha, item.total)
            Next
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.DataSource = dt
        End Sub




        Private Sub VerVentasItems()
            Dim DAO As New VentasItemsDAO
            Dim listaVentasItems As List(Of VentasitemsDTO) = DAO.Read()

            Dim dt As New DataTable()
            dt.Columns.Add("nombre")
            dt.Columns.Add("preciounitario")
            dt.Columns.Add("preciototal")

            For Each item In listaVentasItems
                dt.Rows.Add(item.productos.nombre, item.preciounitario, item.preciototal)
            Next

            DataGridView2.AllowUserToAddRows = False

            DataGridView2.DataSource = dt
        End Sub





        Friend WithEvents DataGridView1 As DataGridView
        Friend WithEvents DataGridView2 As DataGridView

        Private Sub InitializeComponent()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.DataGridView2 = New System.Windows.Forms.DataGridView()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DataGridView1
            '
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView1.Location = New System.Drawing.Point(12, 48)
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.Size = New System.Drawing.Size(384, 308)
            Me.DataGridView1.TabIndex = 0
            '
            'DataGridView2
            '
            Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView2.Location = New System.Drawing.Point(420, 48)
            Me.DataGridView2.Name = "DataGridView2"
            Me.DataGridView2.Size = New System.Drawing.Size(369, 308)
            Me.DataGridView2.TabIndex = 1
            '
            'Ventas
            '
            Me.ClientSize = New System.Drawing.Size(1062, 452)
            Me.Controls.Add(Me.DataGridView2)
            Me.Controls.Add(Me.DataGridView1)
            Me.Name = "Ventas"
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub


    End Class
End Namespace