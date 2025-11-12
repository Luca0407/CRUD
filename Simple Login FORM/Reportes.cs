using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Simple_Login_FORM
{
	public partial class Reportes : Form
	{
		public Reportes()
		{
			InitializeComponent();
			CargarGraficoVentas();
		}

		private void CargarGraficoVentas()
		{
			try
			{
				using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
				{
					con.Open();

					// Consulta para obtener ventas de los últimos 30 días agrupadas por fecha
					string sql = @"SELECT DATE(fecha_venta) as Fecha, SUM(costo_total) as Total
								  FROM ventas 
								  WHERE fecha_venta >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
								  GROUP BY DATE(fecha_venta)
								  ORDER BY Fecha ASC";

					using (MySqlCommand cmd = new MySqlCommand(sql, con))
					{
						MySqlDataAdapter da = new MySqlDataAdapter(cmd);
						DataTable dt = new DataTable();
						da.Fill(dt);

						// Configurar el gráfico
						chartVentas.Series.Clear();
						chartVentas.ChartAreas.Clear();

						// Crear área del gráfico
						ChartArea area = new ChartArea("VentasArea");
						area.AxisX.Title = "Fecha";
						area.AxisY.Title = "Total Ventas ($)";
						area.AxisX.LabelStyle.Format = "dd/MM";
						area.AxisX.Interval = 1;
						area.AxisX.IntervalType = DateTimeIntervalType.Days;
						area.AxisX.MajorGrid.LineColor = Color.LightGray;
						area.AxisY.MajorGrid.LineColor = Color.LightGray;
						chartVentas.ChartAreas.Add(area);

						// Crear serie para ventas
						Series series = new Series("Ventas");
						series.ChartType = SeriesChartType.Column;
						series.Color = Color.FromArgb(41, 128, 185);
						series.BorderWidth = 2;

						// Si no hay datos, mostrar mensaje
						if (dt.Rows.Count == 0)
						{
							lblMensaje.Text = "No hay ventas registradas en los últimos 30 días";
							lblMensaje.Visible = true;
							chartVentas.Visible = false;
							return;
						}

						lblMensaje.Visible = false;
						chartVentas.Visible = true;

						// Agregar datos al gráfico
						decimal totalGeneral = 0;
						foreach (DataRow row in dt.Rows)
						{
							DateTime fecha = Convert.ToDateTime(row["Fecha"]);
							decimal total = Convert.ToDecimal(row["Total"]);
							totalGeneral += total;

							DataPoint punto = new DataPoint();
							punto.SetValueXY(fecha, total);
							punto.AxisLabel = fecha.ToString("dd/MM");
							punto.ToolTip = $"Fecha: {fecha:dd/MM/yyyy}\nTotal: ${total:N2}";
							series.Points.Add(punto);
						}

						chartVentas.Series.Add(series);

						// Mostrar estadísticas
						lblTotalVentas.Text = $"Total Ventas (30 días): ${totalGeneral:N2}";
						lblPromedioDiario.Text = $"Promedio Diario: ${(totalGeneral / dt.Rows.Count):N2}";
						lblDiasConVentas.Text = $"Días con Ventas: {dt.Rows.Count}";

						// Títulos del gráfico
						chartVentas.Titles.Clear();
						Title titulo = new Title("Ventas de los Últimos 30 Días");
						titulo.Font = new Font("Arial", 16, FontStyle.Bold);
						chartVentas.Titles.Add(titulo);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al cargar el gráfico: {ex.Message}", "Error", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Reportes_Load(object sender, EventArgs e)
		{
			// Evento Load ya manejado en constructor
		}

		private void btnActualizar_Click(object sender, EventArgs e)
		{
			CargarGraficoVentas();
			MessageBox.Show("Gráfico actualizado", "Información", 
				MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
