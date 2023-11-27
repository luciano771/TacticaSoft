Imports TacticaSoft.TacticaSoft.DAO
Imports TacticaSoft.TacticaSoft.DTO



Namespace TacticaSoft
    Public Class Form1
        Inherits Form

        Friend WithEvents Button1 As Button
        Friend WithEvents Button2 As Button
        Public Sub New()
            InitializeComponent()

        End Sub





        Private Sub InitializeComponent()
            Me.Button1 = New System.Windows.Forms.Button()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.Button3 = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'Button1
            '
            Me.Button1.Location = New System.Drawing.Point(140, 12)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(75, 23)
            Me.Button1.TabIndex = 1
            Me.Button1.Text = "Alta cliente"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Button2
            '
            Me.Button2.Location = New System.Drawing.Point(273, 12)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(90, 23)
            Me.Button2.TabIndex = 1
            Me.Button2.Text = "Alta producto"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'Button3
            '
            Me.Button3.Location = New System.Drawing.Point(433, 12)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(90, 23)
            Me.Button3.TabIndex = 2
            Me.Button3.Text = "Alta ventas"
            Me.Button3.UseVisualStyleBackColor = True
            '
            'Form1
            '
            Me.ClientSize = New System.Drawing.Size(1021, 435)
            Me.Controls.Add(Me.Button3)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.Button1)
            Me.Name = "Form1"
            Me.ResumeLayout(False)

        End Sub



        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            Clientes.Show()
        End Sub
        Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
            Productos.Show()
        End Sub

        Friend WithEvents Button3 As Button

        Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
            Ventas.Show()
        End Sub
    End Class
End Namespace
