Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO

Namespace TacticaSoft
    Public Class Clientes
        Inherits Form
        Private ID As String
        Public Sub New()
            InitializeComponent()
            VerClientes()
        End Sub



        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            AgregarCliente()
        End Sub


        Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
            ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        End Sub

        Private Sub VerClientes()
            Try
                Dim DAO As New ClientesDAO()
                DataGridView1.DataSource = DAO.Read()
            Catch ex As Exception
                ' Manejar la excepción aquí (puedes registrarla, mostrar un mensaje, etc.)
                Console.WriteLine("Error al obtener registros: " & ex.Message)
            End Try
        End Sub

        Private Sub AgregarCliente()
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Debe completar los 3 campos")
                Return
            End If



            Try
                Dim DAO As New ClientesDAO()
                Dim DTO As New ClientesDTO()
                DTO.cliente = TextBox1.Text
                DTO.telefono = TextBox2.Text
                DTO.correo = TextBox3.Text
                DAO.InsertarCliente(DTO)
                VerClientes()
            Catch ex As Exception
                Console.WriteLine("Error al insertar registros: " & ex.Message)
            End Try
        End Sub

        Public Sub EliminarClientePorID(idCliente As Integer)
            Dim dao As New ClientesDAO()
            dao.EliminarCliente(idCliente)
        End Sub


        Private Sub ModificarCliente()

            Try
                Dim DAO As New ClientesDAO()
                Dim DTO As New ClientesDTO()
                DTO.ID = ID
                DTO.cliente = TextBox1.Text
                DTO.telefono = TextBox2.Text
                DTO.correo = TextBox3.Text
                DAO.ActualizarCliente(DTO)
                VerClientes()
            Catch ex As Exception
                Console.WriteLine("Error al insertar registros: " & ex.Message)
            End Try
        End Sub




        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            If ID <> vbEmpty Then
                EliminarClientePorID(ID)
                VerClientes()
            Else
                MsgBox("Debe Seleccionar un registro")
            End If

        End Sub

        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
            ModificarCliente()
        End Sub


        Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
            BuscarProducto(TextBox4.Text)
        End Sub

        Private Sub BuscarProducto(nombre As String)

            If TextBox4.Text.Trim() = "" Then
                VerClientes()
                Return
            End If

            Try
                Dim DAO As New ClientesDAO()

                DataGridView1.DataSource = DAO.Buscar(nombre)

            Catch ex As Exception
                Console.WriteLine("Error al obtener los registros: " & ex.Message)
            End Try
        End Sub



        Private Sub InitializeComponent()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button3 = New System.Windows.Forms.Button()
            Me.TextBox2 = New System.Windows.Forms.TextBox()
            Me.TextBox3 = New System.Windows.Forms.TextBox()
            Me.Button4 = New System.Windows.Forms.Button()
            Me.TextBox4 = New System.Windows.Forms.TextBox()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DataGridView1
            '
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGridView1.Location = New System.Drawing.Point(27, 20)
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.Size = New System.Drawing.Size(468, 336)
            Me.DataGridView1.TabIndex = 0
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(623, 20)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Alta"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'TextBox1
            '
            Me.TextBox1.Location = New System.Drawing.Point(593, 94)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(100, 20)
            Me.TextBox1.TabIndex = 2
            '
            'Button2
            '
            Me.Button2.Location = New System.Drawing.Point(715, 20)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(75, 23)
            Me.Button2.TabIndex = 3
            Me.Button2.Text = "Baja"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button3
            '
            Me.Button3.Location = New System.Drawing.Point(815, 20)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(75, 23)
            Me.Button3.TabIndex = 4
            Me.Button3.Text = "Modificacion"
            Me.Button3.UseVisualStyleBackColor = True
            '
            'TextBox2
            '
            Me.TextBox2.Location = New System.Drawing.Point(715, 94)
            Me.TextBox2.Name = "TextBox2"
            Me.TextBox2.Size = New System.Drawing.Size(100, 20)
            Me.TextBox2.TabIndex = 5
            '
            'TextBox3
            '
            Me.TextBox3.Location = New System.Drawing.Point(830, 94)
            Me.TextBox3.Name = "TextBox3"
            Me.TextBox3.Size = New System.Drawing.Size(100, 20)
            Me.TextBox3.TabIndex = 6
            '
            'Button4
            '
            Me.Button4.Location = New System.Drawing.Point(623, 175)
            Me.Button4.Name = "Button4"
            Me.Button4.Size = New System.Drawing.Size(75, 23)
            Me.Button4.TabIndex = 7
            Me.Button4.Text = "Buscar"
            Me.Button4.UseVisualStyleBackColor = True
            '
            'TextBox4
            '
            Me.TextBox4.Location = New System.Drawing.Point(726, 178)
            Me.TextBox4.Name = "TextBox4"
            Me.TextBox4.Size = New System.Drawing.Size(100, 20)
            Me.TextBox4.TabIndex = 8
            '
            'Clientes
            '
            Me.ClientSize = New System.Drawing.Size(980, 368)
            Me.Controls.Add(Me.TextBox4)
            Me.Controls.Add(Me.Button4)
            Me.Controls.Add(Me.TextBox3)
            Me.Controls.Add(Me.TextBox2)
            Me.Controls.Add(Me.Button3)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.TextBox1)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.DataGridView1)
            Me.Name = "Clientes"
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Friend WithEvents DataGridView1 As DataGridView
        Friend WithEvents Button1 As Button
        Friend WithEvents TextBox1 As TextBox
        Friend WithEvents Button2 As Button
        Friend WithEvents Button3 As Button
        Friend WithEvents TextBox2 As TextBox
        Friend WithEvents TextBox3 As TextBox
        Friend WithEvents Button4 As Button
        Friend WithEvents TextBox4 As TextBox


    End Class
End Namespace