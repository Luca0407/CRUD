namespace Simple_Login_FORM
{
	partial class Reportes
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.chartVentas = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.panelTop = new System.Windows.Forms.Panel();
			this.lblTitulo = new System.Windows.Forms.Label();
			this.btnActualizar = new System.Windows.Forms.Button();
			this.panelStats = new System.Windows.Forms.Panel();
			this.lblDiasConVentas = new System.Windows.Forms.Label();
			this.lblPromedioDiario = new System.Windows.Forms.Label();
			this.lblTotalVentas = new System.Windows.Forms.Label();
			this.lblMensaje = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.chartVentas)).BeginInit();
			this.panelTop.SuspendLayout();
			this.panelStats.SuspendLayout();
			this.SuspendLayout();
			// 
			// chartVentas
			// 
			this.chartVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			chartArea1.Name = "ChartArea1";
			this.chartVentas.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chartVentas.Legends.Add(legend1);
			this.chartVentas.Location = new System.Drawing.Point(12, 100);
			this.chartVentas.Name = "chartVentas";
			series1.ChartArea = "ChartArea1";
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			this.chartVentas.Series.Add(series1);
			this.chartVentas.Size = new System.Drawing.Size(1256, 500);
			this.chartVentas.TabIndex = 0;
			this.chartVentas.Text = "chart1";
			// 
			// panelTop
			// 
			this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
			this.panelTop.Controls.Add(this.btnActualizar);
			this.panelTop.Controls.Add(this.lblTitulo);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(1280, 80);
			this.panelTop.TabIndex = 1;
			// 
			// lblTitulo
			// 
			this.lblTitulo.AutoSize = true;
			this.lblTitulo.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
			this.lblTitulo.ForeColor = System.Drawing.Color.White;
			this.lblTitulo.Location = new System.Drawing.Point(25, 23);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(363, 32);
			this.lblTitulo.TabIndex = 0;
			this.lblTitulo.Text = "Reporte de Ventas - 30 Días";
			// 
			// btnActualizar
			// 
			this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnActualizar.BackColor = System.Drawing.Color.White;
			this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnActualizar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
			this.btnActualizar.Location = new System.Drawing.Point(1120, 18);
			this.btnActualizar.Name = "btnActualizar";
			this.btnActualizar.Size = new System.Drawing.Size(140, 45);
			this.btnActualizar.TabIndex = 1;
			this.btnActualizar.Text = "Actualizar";
			this.btnActualizar.UseVisualStyleBackColor = false;
			this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
			// 
			// panelStats
			// 
			this.panelStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
			this.panelStats.Controls.Add(this.lblDiasConVentas);
			this.panelStats.Controls.Add(this.lblPromedioDiario);
			this.panelStats.Controls.Add(this.lblTotalVentas);
			this.panelStats.Location = new System.Drawing.Point(12, 620);
			this.panelStats.Name = "panelStats";
			this.panelStats.Size = new System.Drawing.Size(1256, 80);
			this.panelStats.TabIndex = 2;
			// 
			// lblDiasConVentas
			// 
			this.lblDiasConVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDiasConVentas.AutoSize = true;
			this.lblDiasConVentas.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.lblDiasConVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblDiasConVentas.Location = new System.Drawing.Point(900, 30);
			this.lblDiasConVentas.Name = "lblDiasConVentas";
			this.lblDiasConVentas.Size = new System.Drawing.Size(175, 19);
			this.lblDiasConVentas.TabIndex = 2;
			this.lblDiasConVentas.Text = "Días con Ventas: 0";
			// 
			// lblPromedioDiario
			// 
			this.lblPromedioDiario.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblPromedioDiario.AutoSize = true;
			this.lblPromedioDiario.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.lblPromedioDiario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblPromedioDiario.Location = new System.Drawing.Point(480, 30);
			this.lblPromedioDiario.Name = "lblPromedioDiario";
			this.lblPromedioDiario.Size = new System.Drawing.Size(210, 19);
			this.lblPromedioDiario.TabIndex = 1;
			this.lblPromedioDiario.Text = "Promedio Diario: $0.00";
			// 
			// lblTotalVentas
			// 
			this.lblTotalVentas.AutoSize = true;
			this.lblTotalVentas.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.lblTotalVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lblTotalVentas.Location = new System.Drawing.Point(25, 30);
			this.lblTotalVentas.Name = "lblTotalVentas";
			this.lblTotalVentas.Size = new System.Drawing.Size(260, 19);
			this.lblTotalVentas.TabIndex = 0;
			this.lblTotalVentas.Text = "Total Ventas (30 días): $0.00";
			// 
			// lblMensaje
			// 
			this.lblMensaje.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblMensaje.AutoSize = true;
			this.lblMensaje.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
			this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
			this.lblMensaje.Location = new System.Drawing.Point(380, 350);
			this.lblMensaje.Name = "lblMensaje";
			this.lblMensaje.Size = new System.Drawing.Size(520, 22);
			this.lblMensaje.TabIndex = 3;
			this.lblMensaje.Text = "No hay ventas registradas en los últimos 30 días";
			this.lblMensaje.Visible = false;
			// 
			// Reportes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1280, 720);
			this.Controls.Add(this.lblMensaje);
			this.Controls.Add(this.panelStats);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.chartVentas);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Reportes";
			this.Text = "Reportes";
			this.Load += new System.EventHandler(this.Reportes_Load);
			((System.ComponentModel.ISupportInitialize)(this.chartVentas)).EndInit();
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.panelStats.ResumeLayout(false);
			this.panelStats.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chartVentas;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.Button btnActualizar;
		private System.Windows.Forms.Panel panelStats;
		private System.Windows.Forms.Label lblTotalVentas;
		private System.Windows.Forms.Label lblPromedioDiario;
		private System.Windows.Forms.Label lblDiasConVentas;
		private System.Windows.Forms.Label lblMensaje;
	}
}
