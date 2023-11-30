Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO


Namespace TacticaSoft
    Public Class Productos
        Inherits Form


        Private ID_Producto As Integer
        Private dt As DataTable



        Public Sub New()
            InitializeComponent()
            VerProductos()
            llenarCmbCategorias()
        End Sub


        Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
            ID_Producto = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            Dim Precio = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            Dim Nombre = DataGridView1.Rows(e.RowIndex).Cells(1).Value

            If dt Is Nothing Then
                dt = New DataTable()
                dt.Columns.Add("ID")
                dt.Columns.Add("Nombre")
                dt.Columns.Add("Cantidad")
                dt.Columns.Add("Precio")
            End If


            Dim found As Boolean = False

            For Each row As DataRow In dt.Rows
                If Convert.ToInt32(row("ID")) = ID_Producto Then
                    row("Cantidad") = Convert.ToInt32(row("Cantidad")) + 1
                    row("Precio") = Convert.ToInt32(row("Cantidad")) * Convert.ToDecimal(row("Precio"))
                    found = True
                    Exit For
                End If
            Next

            If Not found Then
                Dim newRow As DataRow = dt.NewRow()
                newRow("ID") = ID_Producto
                newRow("Cantidad") = 1
                newRow("Nombre") = Nombre
                newRow("Precio") = Precio
                dt.Rows.Add(newRow)
            End If

            DataGridView2.AllowUserToAddRows = False
            DataGridView2.DataSource = dt

            If My.Forms.TacticaSoft_TacticaSof_Ventas IsNot Nothing Then
                My.Forms.TacticaSoft_TacticaSof_Ventas.dt = dt
            End If

        End Sub

        Private Sub VerProductos()
            Try
                Dim DAO As New ProductosDAO()
                DataGridView1.DataSource = DAO.Read()
            Catch ex As Exception
                ' Manejar la excepción aquí (puedes registrarla, mostrar un mensaje, etc.)
                Console.WriteLine("Error al obtener registros: " & ex.Message)
            End Try
        End Sub


        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            AgregarProducto()
            llenarCmbCategorias()
        End Sub



        Private Sub AgregarProducto()
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Debe completar los 3 campos")
                Return
            End If

            Try
                Dim DAO As New ProductosDAO()
                Dim DTO As New ProductosDTO()
                DTO.nombre = TextBox1.Text
                DTO.precio = TextBox2.Text
                DTO.categoria = TextBox3.Text
                DAO.InsertarProducto(DTO)
                VerProductos()
            Catch ex As Exception
                Console.WriteLine("Error al insertar registros: " & ex.Message)
            End Try
        End Sub


        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

            If ID_Producto <> vbEmpty Then
                EliminarProductoPorID(ID_Producto)
                VerProductos()
                llenarCmbCategorias()
            Else
                MsgBox("Debe Seleccionar un registro")
            End If
        End Sub

        Public Sub EliminarProductoPorID(id As Integer)
            Dim DAO As New ProductosDAO()
            DAO.EliminarProducto(id)
        End Sub


        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
            If ID_Producto <> vbEmpty Then
                ModificarProducto()
                VerProductos()
                llenarCmbCategorias()
            Else
                MsgBox("Debe Seleccionar un registro")
            End If
        End Sub

        Private Sub ModificarProducto()

            Try
                Dim DAO As New ProductosDAO()
                Dim DTO As New ProductosDTO()
                DTO.ID = ID_Producto
                DTO.nombre = TextBox1.Text
                DTO.precio = TextBox2.Text
                DTO.categoria = TextBox3.Text
                DAO.ActualizarProducto(DTO)
            Catch ex As Exception
                Console.WriteLine("Error al insertar registros: " & ex.Message)
            End Try
        End Sub




        Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
            BuscarProducto(TextBox4.Text)
        End Sub

        Private Sub BuscarProducto(nombre As String)

            If TextBox4.Text.Trim() = "" Then
                VerProductos()
                Return
            End If

            Try
                Dim DAO As New ProductosDAO()

                DataGridView1.DataSource = DAO.Buscar(nombre)

            Catch ex As Exception
                Console.WriteLine("Error al obtener los registros: " & ex.Message)
            End Try
        End Sub


        Private Sub llenarCmbCategorias()

            Dim DAO As New ProductosDAO
            Dim listaCategorias As List(Of ProductosDTO) = DAO.Categorias()

            ' Crear un elemento por defecto "Seleccionar categoría"
            Dim categoriaDefault As New ProductosDTO()
            categoriaDefault.categoria = "Seleccionar"
            listaCategorias.Insert(0, categoriaDefault)

            ComboBox1.DisplayMember = "Categoria"
            ComboBox1.ValueMember = "Categoria"
            ComboBox1.DataSource = listaCategorias

        End Sub
        Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

            If ComboBox1.SelectedIndex <> 0 Then
                Dim DAO As New ProductosDAO
                DataGridView1.DataSource = DAO.FiltrarCategoria(ComboBox1.Text)
            End If
            If ComboBox1.SelectedIndex = 0 Then
                VerProductos()
            End If
        End Sub






        Private Sub InitializeComponent()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button3 = New System.Windows.Forms.Button()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.TextBox2 = New System.Windows.Forms.TextBox()
            Me.TextBox3 = New System.Windows.Forms.TextBox()
            Me.TextBox4 = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ComboBox1 = New System.Windows.Forms.ComboBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.DataGridView2 = New System.Windows.Forms.DataGridView()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DataGridView1
            '
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView1.Location = New System.Drawing.Point(12, 12)
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.Size = New System.Drawing.Size(487, 354)
            Me.DataGridView1.TabIndex = 0
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(604, 10)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Alta"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Button2
            '
            Me.Button2.Location = New System.Drawing.Point(699, 10)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(75, 23)
            Me.Button2.TabIndex = 2
            Me.Button2.Text = "Baja"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button3
            '
            Me.Button3.Location = New System.Drawing.Point(797, 10)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(75, 23)
            Me.Button3.TabIndex = 3
            Me.Button3.Text = "Modificacion"
            Me.Button3.UseVisualStyleBackColor = True
            '
            'TextBox1
            '
            Me.TextBox1.Location = New System.Drawing.Point(579, 52)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(100, 20)
            Me.TextBox1.TabIndex = 5
            '
            'TextBox2
            '
            Me.TextBox2.Location = New System.Drawing.Point(699, 52)
            Me.TextBox2.Name = "TextBox2"
            Me.TextBox2.Size = New System.Drawing.Size(100, 20)
            Me.TextBox2.TabIndex = 6
            '
            'TextBox3
            '
            Me.TextBox3.Location = New System.Drawing.Point(818, 52)
            Me.TextBox3.Name = "TextBox3"
            Me.TextBox3.Size = New System.Drawing.Size(100, 20)
            Me.TextBox3.TabIndex = 7
            '
            'TextBox4
            '
            Me.TextBox4.Location = New System.Drawing.Point(699, 85)
            Me.TextBox4.Name = "TextBox4"
            Me.TextBox4.Size = New System.Drawing.Size(121, 20)
            Me.TextBox4.TabIndex = 8
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(576, 88)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(96, 13)
            Me.Label1.TabIndex = 9
            Me.Label1.Text = "Buscar por nombre"
            '
            'ComboBox1
            '
            Me.ComboBox1.FormattingEnabled = True
            Me.ComboBox1.Location = New System.Drawing.Point(699, 112)
            Me.ComboBox1.Name = "ComboBox1"
            Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
            Me.ComboBox1.TabIndex = 10
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(576, 115)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(97, 13)
            Me.Label2.TabIndex = 11
            Me.Label2.Text = "Filtrar por categoria"
            '
            'DataGridView2
            '
            Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView2.Location = New System.Drawing.Point(515, 151)
            Me.DataGridView2.Name = "DataGridView2"
            Me.DataGridView2.Size = New System.Drawing.Size(416, 215)
            Me.DataGridView2.TabIndex = 12
            '
            'Productos
            '
            Me.ClientSize = New System.Drawing.Size(943, 378)
            Me.Controls.Add(Me.DataGridView2)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.ComboBox1)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.TextBox4)
            Me.Controls.Add(Me.TextBox3)
            Me.Controls.Add(Me.TextBox2)
            Me.Controls.Add(Me.TextBox1)
            Me.Controls.Add(Me.Button3)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.DataGridView1)
            Me.Name = "Productos"
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents DataGridView1 As DataGridView
        Friend WithEvents Button1 As Button
        Friend WithEvents Button2 As Button
        Friend WithEvents Button3 As Button
        Friend WithEvents TextBox1 As TextBox
        Friend WithEvents TextBox2 As TextBox
        Friend WithEvents TextBox3 As TextBox
        Friend WithEvents TextBox4 As TextBox
        Friend WithEvents Label1 As Label
        Friend WithEvents ComboBox1 As ComboBox
        Friend WithEvents Label2 As Label
        Friend WithEvents DataGridView2 As DataGridView


    End Class
End Namespace




