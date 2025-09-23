namespace Simple_Login_FORM {
	partial class RepuestosServiciosForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.DGVItems = new System.Windows.Forms.DataGridView();
			this.txtNombre = new System.Windows.Forms.TextBox();
			this.txtDescripcion = new System.Windows.Forms.TextBox();
			this.numPrecio = new System.Windows.Forms.NumericUpDown();
			this.numStock = new System.Windows.Forms.NumericUpDown();
			this.btnAgregar = new System.Windows.Forms.Button();
			this.btnActualizar = new System.Windows.Forms.Button();
			this.btnEliminar = new System.Windows.Forms.Button();
			this.btnLimpiar = new System.Windows.Forms.Button();
			this.btnRefrescar = new System.Windows.Forms.Button();
			this.rbRepuestos = new System.Windows.Forms.RadioButton();
			this.rbServicios = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.DGVItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numPrecio)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit();
			this.SuspendLayout();
			// 
			// DGVItems
			// 
			this.DGVItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGVItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGVItems.Location = new System.Drawing.Point(93, 167);
			this.DGVItems.Name = "DGVItems";
			this.DGVItems.RowHeadersWidth = 51;
			this.DGVItems.RowTemplate.Height = 24;
			this.DGVItems.Size = new System.Drawing.Size(1284, 622);
			this.DGVItems.TabIndex = 0;
			this.DGVItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVItems_CellContentClick);
			// 
			// txtNombre
			// 
			this.txtNombre.Location = new System.Drawing.Point(187, 83);
			this.txtNombre.Name = "txtNombre";
			this.txtNombre.Size = new System.Drawing.Size(100, 22);
			this.txtNombre.TabIndex = 1;
			this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
			// 
			// txtDescripcion
			// 
			this.txtDescripcion.Location = new System.Drawing.Point(339, 82);
			this.txtDescripcion.Name = "txtDescripcion";
			this.txtDescripcion.Size = new System.Drawing.Size(100, 22);
			this.txtDescripcion.TabIndex = 2;
			this.txtDescripcion.TextChanged += new System.EventHandler(this.txtDescripcion_TextChanged);
			// 
			// numPrecio
			// 
			this.numPrecio.Location = new System.Drawing.Point(489, 83);
			this.numPrecio.Name = "numPrecio";
			this.numPrecio.Size = new System.Drawing.Size(120, 22);
			this.numPrecio.TabIndex = 3;
			this.numPrecio.ValueChanged += new System.EventHandler(this.numPrecio_ValueChanged);
			// 
			// numStock
			// 
			this.numStock.Location = new System.Drawing.Point(645, 82);
			this.numStock.Name = "numStock";
			this.numStock.Size = new System.Drawing.Size(120, 22);
			this.numStock.TabIndex = 4;
			this.numStock.ValueChanged += new System.EventHandler(this.numStock_ValueChanged);
			// 
			// btnAgregar
			// 
			this.btnAgregar.Location = new System.Drawing.Point(12, 352);
			this.btnAgregar.Name = "btnAgregar";
			this.btnAgregar.Size = new System.Drawing.Size(75, 23);
			this.btnAgregar.TabIndex = 5;
			this.btnAgregar.Text = "button1";
			this.btnAgregar.UseVisualStyleBackColor = true;
			// 
			// btnActualizar
			// 
			this.btnActualizar.Location = new System.Drawing.Point(12, 396);
			this.btnActualizar.Name = "btnActualizar";
			this.btnActualizar.Size = new System.Drawing.Size(75, 23);
			this.btnActualizar.TabIndex = 6;
			this.btnActualizar.Text = "button2";
			this.btnActualizar.UseVisualStyleBackColor = true;
			// 
			// btnEliminar
			// 
			this.btnEliminar.Location = new System.Drawing.Point(12, 434);
			this.btnEliminar.Name = "btnEliminar";
			this.btnEliminar.Size = new System.Drawing.Size(75, 23);
			this.btnEliminar.TabIndex = 7;
			this.btnEliminar.Text = "button3";
			this.btnEliminar.UseVisualStyleBackColor = true;
			// 
			// btnLimpiar
			// 
			this.btnLimpiar.Location = new System.Drawing.Point(12, 478);
			this.btnLimpiar.Name = "btnLimpiar";
			this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
			this.btnLimpiar.TabIndex = 8;
			this.btnLimpiar.Text = "button4";
			this.btnLimpiar.UseVisualStyleBackColor = true;
			// 
			// btnRefrescar
			// 
			this.btnRefrescar.Location = new System.Drawing.Point(12, 522);
			this.btnRefrescar.Name = "btnRefrescar";
			this.btnRefrescar.Size = new System.Drawing.Size(75, 23);
			this.btnRefrescar.TabIndex = 9;
			this.btnRefrescar.Text = "button5";
			this.btnRefrescar.UseVisualStyleBackColor = true;
			// 
			// rbRepuestos
			// 
			this.rbRepuestos.AutoSize = true;
			this.rbRepuestos.Checked = true;
			this.rbRepuestos.Location = new System.Drawing.Point(1288, 130);
			this.rbRepuestos.Name = "rbRepuestos";
			this.rbRepuestos.Size = new System.Drawing.Size(89, 20);
			this.rbRepuestos.TabIndex = 10;
			this.rbRepuestos.TabStop = true;
			this.rbRepuestos.Text = "Productos";
			this.rbRepuestos.UseVisualStyleBackColor = true;
			// 
			// rbServicios
			// 
			this.rbServicios.AutoSize = true;
			this.rbServicios.Location = new System.Drawing.Point(1198, 130);
			this.rbServicios.Name = "rbServicios";
			this.rbServicios.Size = new System.Drawing.Size(84, 20);
			this.rbServicios.TabIndex = 11;
			this.rbServicios.TabStop = true;
			this.rbServicios.Text = "Servicios";
			this.rbServicios.UseVisualStyleBackColor = true;
			// 
			// RepuestosServiciosForm
			// 
			this.ClientSize = new System.Drawing.Size(1477, 812);
			this.Controls.Add(this.rbServicios);
			this.Controls.Add(this.rbRepuestos);
			this.Controls.Add(this.btnRefrescar);
			this.Controls.Add(this.btnLimpiar);
			this.Controls.Add(this.btnEliminar);
			this.Controls.Add(this.btnActualizar);
			this.Controls.Add(this.btnAgregar);
			this.Controls.Add(this.numStock);
			this.Controls.Add(this.numPrecio);
			this.Controls.Add(this.txtDescripcion);
			this.Controls.Add(this.txtNombre);
			this.Controls.Add(this.DGVItems);
			this.Name = "RepuestosServiciosForm";
			((System.ComponentModel.ISupportInitialize)(this.DGVItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numPrecio)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView DGVItems;
		private System.Windows.Forms.TextBox txtNombre;
		private System.Windows.Forms.TextBox txtDescripcion;
		private System.Windows.Forms.NumericUpDown numPrecio;
		private System.Windows.Forms.NumericUpDown numStock;
		private System.Windows.Forms.Button btnAgregar;
		private System.Windows.Forms.Button btnActualizar;
		private System.Windows.Forms.Button btnEliminar;
		private System.Windows.Forms.Button btnLimpiar;
		private System.Windows.Forms.Button btnRefrescar;
		private System.Windows.Forms.RadioButton rbRepuestos;
		private System.Windows.Forms.RadioButton rbServicios;
	}
}