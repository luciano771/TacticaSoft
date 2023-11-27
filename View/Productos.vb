Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO


Namespace TacticaSoft
    Public Class Productos
        Inherits Form


        Private ID_Producto As Integer



        Public Sub New()
            InitializeComponent()

            VerProductos()
        End Sub


        Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
            ID_Producto = DataGridView1.Rows(e.RowIndex).Cells(0).Value
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

        Private Sub InitializeComponent()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button3 = New System.Windows.Forms.Button()
            Me.Button4 = New System.Windows.Forms.Button()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.TextBox2 = New System.Windows.Forms.TextBox()
            Me.TextBox3 = New System.Windows.Forms.TextBox()
            Me.TextBox4 = New System.Windows.Forms.TextBox()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DataGridView1
            '
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView1.Location = New System.Drawing.Point(12, 22)
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.Size = New System.Drawing.Size(487, 344)
            Me.DataGridView1.TabIndex = 0
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(604, 62)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Button2"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Button2
            '
            Me.Button2.Location = New System.Drawing.Point(699, 62)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(75, 23)
            Me.Button2.TabIndex = 2
            Me.Button2.Text = "Button3"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button3
            '
            Me.Button3.Location = New System.Drawing.Point(797, 62)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(75, 23)
            Me.Button3.TabIndex = 3
            Me.Button3.Text = "Button4"
            Me.Button3.UseVisualStyleBackColor = True
            '
            'Button4
            '
            Me.Button4.Location = New System.Drawing.Point(604, 210)
            Me.Button4.Name = "Button4"
            Me.Button4.Size = New System.Drawing.Size(75, 23)
            Me.Button4.TabIndex = 4
            Me.Button4.Text = "Button5"
            Me.Button4.UseVisualStyleBackColor = True
            '
            'TextBox1
            '
            Me.TextBox1.Location = New System.Drawing.Point(579, 142)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(100, 20)
            Me.TextBox1.TabIndex = 5
            '
            'TextBox2
            '
            Me.TextBox2.Location = New System.Drawing.Point(699, 142)
            Me.TextBox2.Name = "TextBox2"
            Me.TextBox2.Size = New System.Drawing.Size(100, 20)
            Me.TextBox2.TabIndex = 6
            '
            'TextBox3
            '
            Me.TextBox3.Location = New System.Drawing.Point(818, 142)
            Me.TextBox3.Name = "TextBox3"
            Me.TextBox3.Size = New System.Drawing.Size(100, 20)
            Me.TextBox3.TabIndex = 7
            '
            'TextBox4
            '
            Me.TextBox4.Location = New System.Drawing.Point(712, 213)
            Me.TextBox4.Name = "TextBox4"
            Me.TextBox4.Size = New System.Drawing.Size(100, 20)
            Me.TextBox4.TabIndex = 8
            '
            'Productos
            '
            Me.ClientSize = New System.Drawing.Size(943, 378)
            Me.Controls.Add(Me.TextBox4)
            Me.Controls.Add(Me.TextBox3)
            Me.Controls.Add(Me.TextBox2)
            Me.Controls.Add(Me.TextBox1)
            Me.Controls.Add(Me.Button4)
            Me.Controls.Add(Me.Button3)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.DataGridView1)
            Me.Name = "Productos"
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents DataGridView1 As DataGridView
        Friend WithEvents Button1 As Button
        Friend WithEvents Button2 As Button
        Friend WithEvents Button3 As Button
        Friend WithEvents Button4 As Button
        Friend WithEvents TextBox1 As TextBox
        Friend WithEvents TextBox2 As TextBox
        Friend WithEvents TextBox3 As TextBox
        Friend WithEvents TextBox4 As TextBox
    End Class
End Namespace




