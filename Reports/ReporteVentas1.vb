Public Class ReporteVentas1
    Private Sub ReporteVentas1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: esta línea de código carga datos en la tabla 'reporte1.DataTable2' Puede moverla o quitarla según sea necesario.
        Me.DataTable2TableAdapter.Fill(Me.reporte1.DataTable2)
        'TODO: esta línea de código carga datos en la tabla 'reporte1.DataTable1' Puede moverla o quitarla según sea necesario.
        Me.DataTable1TableAdapter.Fill(Me.reporte1.DataTable1)

        Me.ReportViewer1.RefreshReport()
    End Sub
End Class