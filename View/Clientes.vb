Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO

Namespace TacticaSoft
    Public Class Clientes
        Inherits Form

        Public ID As String
        Public FuncionesClientes As New TacticaSoft.Funciones.FuncCliente()







        Public Sub New()
            InitializeComponent()
            FuncionesClientes.verClientes(DataGridView1)
        End Sub




        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            AgregarCliente()
        End Sub


        Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 AndAlso DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing Then

                ID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                Funciones.FuncVentas.idcliente = ID

                If Form1.ventasForm.Visible Then
                    Form1.ventasForm.TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                    Form1.ventasForm.TextBox1.Refresh()
                    Me.Hide()
                End If
            End If




        End Sub



        Private Sub AgregarCliente()
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Debe completar los 3 campos")
                Return
            End If

            FuncionesClientes.InsertarCliente(TextBox1.Text, TextBox2.Text, TextBox3.Text)
            FuncionesClientes.verClientes(DataGridView1)

        End Sub

        Public Sub EliminarClientePorID(idCliente As Integer)
            Dim dao As New ClientesDAO()
            dao.EliminarCliente(idCliente)
        End Sub







        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            If ID <> Nothing Then
                EliminarClientePorID(ID)
                FuncionesClientes.verClientes(DataGridView1)
            Else
                MsgBox("Debe Seleccionar un registro")
            End If

        End Sub

        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
            FuncionesClientes.actualizarClientes(ID, TextBox1.Text, TextBox2.Text, TextBox3.Text)
            FuncionesClientes.verClientes(DataGridView1)
        End Sub


        Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
            BuscarProducto(TextBox4.Text)
        End Sub

        Private Sub BuscarProducto(nombre As String)

            If TextBox4.Text.Trim() = "" Then
                FuncionesClientes.verClientes(DataGridView1)
                Return
            End If

            If TextBox4.Text <> "" Then
                Dim dt1 = DataGridView1.DataSource
                Dim vista As New DataView(dt1)
                vista.RowFilter = $"CONVERT(Nombre, 'System.String') LIKE '%{TextBox4.Text}%'"
                DataGridView1.DataSource = vista.ToTable()
            Else
                FuncionesClientes.verClientes(DataGridView1)
            End If

        End Sub



        Private Sub InitializeComponent()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.TextBox1 = New System.Windows.Forms.TextBox()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button3 = New System.Windows.Forms.Button()
            Me.TextBox2 = New System.Windows.Forms.TextBox()
            Me.TextBox3 = New System.Windows.Forms.TextBox()
            Me.TextBox4 = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
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
            Me.TextBox1.Location = New System.Drawing.Point(670, 78)
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
            Me.TextBox2.Location = New System.Drawing.Point(670, 130)
            Me.TextBox2.Name = "TextBox2"
            Me.TextBox2.Size = New System.Drawing.Size(100, 20)
            Me.TextBox2.TabIndex = 5
            '
            'TextBox3
            '
            Me.TextBox3.Location = New System.Drawing.Point(670, 104)
            Me.TextBox3.Name = "TextBox3"
            Me.TextBox3.Size = New System.Drawing.Size(100, 20)
            Me.TextBox3.TabIndex = 6
            '
            'TextBox4
            '
            Me.TextBox4.Location = New System.Drawing.Point(738, 162)
            Me.TextBox4.Name = "TextBox4"
            Me.TextBox4.Size = New System.Drawing.Size(100, 20)
            Me.TextBox4.TabIndex = 8
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(620, 165)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(96, 13)
            Me.Label1.TabIndex = 9
            Me.Label1.Text = "Buscar por nombre"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(620, 78)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(44, 13)
            Me.Label2.TabIndex = 10
            Me.Label2.Text = "Nombre"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(620, 104)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(38, 13)
            Me.Label3.TabIndex = 11
            Me.Label3.Text = "Correo"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(620, 130)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(49, 13)
            Me.Label4.TabIndex = 12
            Me.Label4.Text = "Telefono"
            '
            'Clientes
            '
            Me.ClientSize = New System.Drawing.Size(980, 368)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.TextBox4)
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
        Friend WithEvents TextBox4 As TextBox
        Friend WithEvents Label1 As Label
        Friend WithEvents Label2 As Label
        Friend WithEvents Label3 As Label
        Friend WithEvents Label4 As Label

    End Class
End Namespace