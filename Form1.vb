Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO



Namespace TacticaSoft
    Public Class Form1
        Inherits Form

        Public Shared clientesForm As TacticaSoft.Clientes
        Public Shared productosForm As TacticaSoft.Productos
        Public Shared ventasForm As TacticaSoft.Ventas


        Public Sub New()
            InitializeComponent()
            MostrarClientesForm()
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs)
            My.Forms.TacticaSoft_TacticaSoft_Clientes.Show()
        End Sub
        Private Sub Button2_Click(sender As Object, e As EventArgs)
            My.Forms.TacticaSoft_TacticaSoft_Productos.Show()
        End Sub

        Private Sub Button3_Click(sender As Object, e As EventArgs)
            My.Forms.TacticaSoft_TacticaSoft_Ventas.Show()

        End Sub





        Private Sub InitializeComponent()
            Me.TabControl = New System.Windows.Forms.TabControl()
            Me.TabPage1 = New System.Windows.Forms.TabPage()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.TabPage3 = New System.Windows.Forms.TabPage()
            Me.TabPage4 = New System.Windows.Forms.TabPage()
            Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
            Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.TabControl.SuspendLayout()
            Me.TabPage4.SuspendLayout()
            Me.SuspendLayout()
            '
            'TabControl
            '
            Me.TabControl.Controls.Add(Me.TabPage1)
            Me.TabControl.Controls.Add(Me.TabPage2)
            Me.TabControl.Controls.Add(Me.TabPage3)
            Me.TabControl.Controls.Add(Me.TabPage4)
            Me.TabControl.Location = New System.Drawing.Point(12, 12)
            Me.TabControl.Name = "TabControl"
            Me.TabControl.SelectedIndex = 0
            Me.TabControl.Size = New System.Drawing.Size(1183, 498)
            Me.TabControl.TabIndex = 3
            '
            'TabPage1
            '
            Me.TabPage1.Location = New System.Drawing.Point(4, 22)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage1.Size = New System.Drawing.Size(1175, 472)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Clientes"
            Me.TabPage1.UseVisualStyleBackColor = True
            '
            'TabPage2
            '
            Me.TabPage2.Location = New System.Drawing.Point(4, 22)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage2.Size = New System.Drawing.Size(1175, 472)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "Productos"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'TabPage3
            '
            Me.TabPage3.Location = New System.Drawing.Point(4, 22)
            Me.TabPage3.Name = "TabPage3"
            Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage3.Size = New System.Drawing.Size(1175, 472)
            Me.TabPage3.TabIndex = 2
            Me.TabPage3.Text = "Ventas"
            Me.TabPage3.UseVisualStyleBackColor = True
            '
            'TabPage4
            '
            Me.TabPage4.Controls.Add(Me.DateTimePicker2)
            Me.TabPage4.Controls.Add(Me.DateTimePicker1)
            Me.TabPage4.Controls.Add(Me.Button2)
            Me.TabPage4.Controls.Add(Me.Button1)
            Me.TabPage4.Location = New System.Drawing.Point(4, 22)
            Me.TabPage4.Name = "TabPage4"
            Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage4.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.TabPage4.Size = New System.Drawing.Size(1175, 472)
            Me.TabPage4.TabIndex = 3
            Me.TabPage4.Text = "Reportes"
            Me.TabPage4.UseVisualStyleBackColor = True
            '
            'DateTimePicker2
            '
            Me.DateTimePicker2.Location = New System.Drawing.Point(481, 230)
            Me.DateTimePicker2.Name = "DateTimePicker2"
            Me.DateTimePicker2.Size = New System.Drawing.Size(200, 20)
            Me.DateTimePicker2.TabIndex = 3
            '
            'DateTimePicker1
            '
            Me.DateTimePicker1.Location = New System.Drawing.Point(266, 230)
            Me.DateTimePicker1.Name = "DateTimePicker1"
            Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
            Me.DateTimePicker1.TabIndex = 2
            '
            'Button2
            '
            Me.Button2.Location = New System.Drawing.Point(90, 227)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(156, 23)
            Me.Button2.TabIndex = 1
            Me.Button2.Text = "Generar reporte de productos por fecha"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(90, 93)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(156, 23)
            Me.Button1.TabIndex = 0
            Me.Button1.Text = "Generar reporte de ventas"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Form1
            '
            Me.ClientSize = New System.Drawing.Size(1260, 570)
            Me.Controls.Add(Me.TabControl)
            Me.Name = "Form1"
            Me.TabControl.ResumeLayout(False)
            Me.TabPage4.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents TabControl As TabControl
        Friend WithEvents TabPage1 As TabPage
        Friend WithEvents TabPage2 As TabPage
        Friend WithEvents TabPage3 As TabPage



        Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl.SelectedIndexChanged
            Select Case TabControl.SelectedTab.Text
                Case "Clientes"
                    MostrarClientesForm()
                Case "Productos"
                    MostrarProductosForm()
                Case "Ventas"
                    MostrarVentasForm()
            End Select
        End Sub

        Private Sub MostrarClientesForm()
            If clientesForm Is Nothing Then
                clientesForm = New TacticaSoft.Clientes()
                clientesForm.TopLevel = False
                clientesForm.FormBorderStyle = FormBorderStyle.None
                clientesForm.Dock = DockStyle.Fill
                TabPage1.Controls.Add(clientesForm)
                clientesForm.Show()
            Else
                clientesForm.Show()
            End If
            If Not ventasForm Is Nothing Then
                ventasForm.Hide()
            End If

            If Not productosForm Is Nothing Then
                productosForm.Hide()
            End If
        End Sub

        Private Sub MostrarProductosForm()
            If productosForm Is Nothing Then
                productosForm = New TacticaSoft.Productos()
                productosForm.TopLevel = False
                productosForm.FormBorderStyle = FormBorderStyle.None
                productosForm.Dock = DockStyle.Fill
                TabPage2.Controls.Add(productosForm)
                productosForm.Show()
            Else
                productosForm.Show()
            End If
            If Not ventasForm Is Nothing Then
                ventasForm.Hide()
            End If

            If Not clientesForm Is Nothing Then
                clientesForm.Hide()
            End If
        End Sub


        Private Sub MostrarVentasForm()
            If ventasForm Is Nothing Then
                ventasForm = New TacticaSoft.Ventas()
                ventasForm.TopLevel = False
                ventasForm.FormBorderStyle = FormBorderStyle.None
                ventasForm.Dock = DockStyle.Fill
                TabPage3.Controls.Add(ventasForm)
                ventasForm.Show()
            Else
                ventasForm.Show()
            End If
            If Not productosForm Is Nothing Then
                productosForm.Hide()
            End If

            If Not clientesForm Is Nothing Then
                clientesForm.Hide()
            End If
        End Sub

        Friend WithEvents TabPage4 As TabPage
        Friend WithEvents Button1 As Button

        Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
            My.Forms.ReporteVentas1.ShowDialog()
        End Sub

        Friend WithEvents Button2 As Button
        Friend WithEvents DateTimePicker2 As DateTimePicker
        Friend WithEvents DateTimePicker1 As DateTimePicker



        Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click


            If DateTimePicker1.Value < DateTimePicker2.Value Then
                My.Forms.ReporteProductos.ShowDialog()
            Else
                MsgBox("La fecha final debe ser mayor a la fecha inicial")
            End If



        End Sub


    End Class
End Namespace
