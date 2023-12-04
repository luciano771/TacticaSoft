Public Class ReporteProductos
    Private Sub ReporteProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: esta línea de código carga datos en la tabla 'reporte1.DataTable3' Puede moverla o quitarla según sea necesario.
        Dim fechaInicio As DateTime = My.Forms.TacticaSoft_TacticaSoft_Form1.DateTimePicker1.Value.Date
        Dim fechaFin As DateTime = My.Forms.TacticaSoft_TacticaSoft_Form1.DateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1)

        ' Crear un DataAdapter y obtener los datos filtrados
        Dim dataTableAdapter As New reporte1TableAdapters.DataTable3TableAdapter()
        Dim datosFiltrados As DataTable = dataTableAdapter.GetData(fechaInicio, fechaFin)

        ' Limpiar los DataSources actuales y agregar el nuevo DataSource al ReportViewer
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", datosFiltrados))

        ' Actualizar el ReportViewer
        ReportViewer1.RefreshReport()
    End Sub


End Class