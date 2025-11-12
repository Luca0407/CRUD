using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Login_FORM
{
    public partial class productos : Form {
	private string combomarca = @"SELECT DISTINCT ID_marcas, nombre_marca FROM marcas ORDER BY nombre_marca ASC";
	private string combomodelo = @"SELECT DISTINCT ID_modelos, nombre_modelo FROM modelos ORDER BY nombre_modelo ASC";
	private int userRole;
	private int currentTipoProducto = 3;
	private DataGridView currentDGV;

	public productos(int role)
	{
		InitializeComponent();
			Descripcion.MaxLength = 250;
		userRole = role;
		
		// Mostrar por defecto la primera pestaña
		currentDGV = DGVdisp;
		currentTipoProducto = 3;
		CargarProductos(3, DGVdisp);
		
		// Apply role-based filtering for TECNICO
		if (userRole == 2) // TECNICO - only show devices tab
		{
			Products.TabPages.Remove(misc);
			Products.TabPages.Remove(repuest);
		}
		
		// Initially hide panel1 since default tab is Dispositivos
		panel1.Visible = false;
		
		// Suscribirse al evento de cambio de pestaña
		Products.SelectedIndexChanged += Products_SelectedIndexChanged;
		
		// Configure autocomplete for client search
		CargarAutocompleteClientes();
		buscarCliente.Click += buscarCliente_Click;
		
		// Configure autocomplete for product search
		CargarAutocompleteProductos();
		button9.Click += button9_Click;
		
		// Make ClientName and prodName readonly
		ClientName.ReadOnly = true;
		prodName.ReadOnly = true;
		
		// Configure create client button
		createClient.Click += createClient_Click;
		
		// Configure create product button
		button7.Click += button7_Click;
		
		// Configure load button
		loadButton.Click += loadButton_Click;
	}

	private void CargarProductos(int tipoProducto, DataGridView dgv) {
		using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
			con.Open();

			string sql = @"
            SELECT p.ID_productos, g.nombre_generico as Producto, m.nombre_marca as Marca, n.nombre_modelo as Modelo, 
                   p.stock as Stock, p.precio_costo as Costo, p.precio_venta as Venta, v.pagina as Pagina
            FROM productos p
            JOIN productos_genericos g ON p.nombre_producto = g.ID_nombre_productos
            JOIN proveedores v ON p.proveedor = v.ID_proveedores
            JOIN marcas m ON p.marca = m.ID_marcas
            JOIN modelos n ON p.modelo = n.ID_modelos
            WHERE g.tipo_producto = @tipo";

			using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
				cmd.Parameters.AddWithValue("@tipo", tipoProducto);

				MySqlDataAdapter da = new MySqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);

				dgv.DataSource = dt;

				// Configuración de columnas
				if(dgv.Columns.Contains("ID_productos"))
					dgv.Columns["ID_productos"].Visible = false;
			}
		}
	}

	private void CargarComboBox(ComboBox combo, string query, string display, string value) {
		using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
			con.Open();
			MySqlDataAdapter da = new MySqlDataAdapter(query, con);
			DataTable dt = new DataTable();
			da.Fill(dt);

			combo.DataSource = dt;
			combo.DisplayMember = display;
			combo.ValueMember = value;
			combo.SelectedIndex = -1; // sin selección inicial
		}
	}

	private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

		private void DGVdisp_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void Filtro_Click(object sender, EventArgs e) {
			if(Products.SelectedTab == devices) {
				currentDGV = DGVdisp;
				currentTipoProducto = 3;
				filtrar_Productos(DGVdisp, 3);
			} else if(Products.SelectedTab == misc) {
				currentDGV = DGVacc;
				currentTipoProducto = 1;
				filtrar_Productos(DGVacc, 1);
			} else if(Products.SelectedTab == repuest) {
				currentDGV = DGVrep;
				currentTipoProducto = 2;
				filtrar_Productos(DGVrep, 2);
			}
		}

		private void filtrar_Productos(DataGridView dgv, int tipo) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();

				// Base query
				StringBuilder sql = new StringBuilder(@"
            SELECT p.ID_productos, g.nombre_generico as Producto, m.nombre_marca as Marca, n.nombre_modelo as Modelo, 
                   p.stock as Stock, p.precio_costo as Costo, p.precio_venta as Venta, v.pagina as Pagina
            FROM productos p
            JOIN productos_genericos g ON p.nombre_producto = g.ID_nombre_productos
            JOIN proveedores v ON p.proveedor = v.ID_proveedores
            JOIN marcas m ON p.marca = m.ID_marcas
            JOIN modelos n ON p.modelo = n.ID_modelos
            WHERE 1=1");

				// Agregar filtros dinámicos según lo que el usuario haya seleccionado
				if(BrandBox.SelectedValue != null && BrandBox.SelectedValue != DBNull.Value)
					sql.Append(" AND p.marca = @marca");

				if(ModBox.SelectedValue != null && ModBox.SelectedValue != DBNull.Value)
					sql.Append(" AND p.modelo = @modelo");

				if(Stock.Value > 0)
					sql.Append(" AND p.stock >= @stock");

				sql.Append(" AND g.tipo_producto = @tipo");

				using(MySqlCommand cmd = new MySqlCommand(sql.ToString(), con)) {
					// Asignar parámetros solo si corresponden
					if(BrandBox.SelectedValue != null && BrandBox.SelectedValue != DBNull.Value)
						cmd.Parameters.AddWithValue("@marca", BrandBox.SelectedValue);

					if(ModBox.SelectedValue != null && ModBox.SelectedValue != DBNull.Value)
						cmd.Parameters.AddWithValue("@modelo", ModBox.SelectedValue);

					if(Stock.Value > 0)
						cmd.Parameters.AddWithValue("@stock", Stock.Value);

					cmd.Parameters.AddWithValue("@tipo", tipo);

					MySqlDataAdapter da = new MySqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);

					dgv.DataSource = dt;
					dgv.Columns["ID_productos"].Visible = false;
					ModBox.SelectedIndex = -1;
					BrandBox.SelectedIndex = -1;
				}
			}
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void productos_Load(object sender, EventArgs e) {
			devices.Text = "Dispositivos";
			repuest.Text = "Accesorios";
			misc.Text = "Repuestos";

			currentDGV = DGVdisp;
			currentTipoProducto = 3;
			CargarProductos(3, DGVdisp);
			
			// Only load other tabs if user is not TECNICO
			if (userRole != 2)
			{
				CargarProductos(1, DGVacc);
				CargarProductos(2, DGVrep);
			}
			
			CargarComboBox(BrandBox, combomarca, "nombre_marca", "ID_marcas");
			CargarComboBox(ModBox, combomodelo, "nombre_modelo", "ID_modelos");
		}

		private void Stock_ValueChanged(object sender, EventArgs e) {

		}

		private void editButton_Click(object sender, EventArgs e) {
			if(currentDGV.SelectedRows.Count != 1) {
				MessageBox.Show("Por favor, selecciona solo una fila para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var cellValue = currentDGV.SelectedRows[0].Cells[0].Value;
			if(cellValue == null || !int.TryParse(cellValue.ToString(), out int idNum)) {
				MessageBox.Show("El ID de la fila seleccionada no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			using(ModProducto pf = new ModProducto(idNum, currentTipoProducto)) {
				if(pf.ShowDialog() == DialogResult.OK) {
					CargarProductos(currentTipoProducto, currentDGV);
				} else {
					MessageBox.Show("Modificación cancelada", "Cancelada");
				}
			}
		}

		private void tabPage1_Click(object sender, EventArgs e) {

		}

		private void Products_SelectedIndexChanged(object sender, EventArgs e) {
			if(Products.SelectedTab == devices) {
				currentDGV = DGVdisp;
				currentTipoProducto = 3;
				panel1.Visible = false;
			} else if(Products.SelectedTab == misc) {
				currentDGV = DGVacc;
				currentTipoProducto = 1;
				panel1.Visible = false;
			} else if(Products.SelectedTab == repuest) {
				currentDGV = DGVrep;
				currentTipoProducto = 2;
				panel1.Visible = false;
			} else if(Products.SelectedTab == tabPage2 || Products.SelectedTab == tabPage1) {
				// Ingreso Reparacion or Servicio Reparacion tabs
				panel1.Visible = true;
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void CargarAutocompleteProductos() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string query = @"SELECT p.ID_productos, CONCAT_WS(' ', pg.nombre_generico, m.nombre_marca, mo.nombre_modelo) AS NombreCompleto
									 FROM productos p 
									 JOIN productos_genericos pg ON pg.ID_nombre_productos = p.nombre_producto
									 JOIN marcas m ON m.ID_marcas = p.marca
									 LEFT JOIN modelos mo ON mo.ID_modelos = p.modelo
									 ORDER BY NombreCompleto ASC";
					MySqlCommand cmd = new MySqlCommand(query, con);
					MySqlDataReader reader = cmd.ExecuteReader();
		
					AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
		
					while(reader.Read()) {
						string id = reader.GetValue(reader.GetOrdinal("ID_productos")).ToString();
						string nombreCompleto = reader.GetString("NombreCompleto");
						coleccion.Add($"{id} - {nombreCompleto}");
					}
		
					codProd.AutoCompleteMode = AutoCompleteMode.Suggest;
					codProd.AutoCompleteSource = AutoCompleteSource.CustomSource;
					codProd.AutoCompleteCustomSource = coleccion;
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar autocomplete de productos: " + ex.Message);
			}
		}
		
		private void CargarAutocompleteClientes() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string query = @"SELECT c.DNI, CONCAT_WS(' ', p.nombre, p.apellido) as NombreCompleto 
									 FROM clientes c
									 INNER JOIN personas p ON c.ID_persona = p.ID_persona
									 WHERE p.tipo = 'c'
									 ORDER BY NombreCompleto ASC";
					MySqlCommand cmd = new MySqlCommand(query, con);
					MySqlDataReader reader = cmd.ExecuteReader();

				AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

					while(reader.Read()) {
						string dni = reader.GetString("DNI");
						string nombreCompleto = reader.GetString("NombreCompleto");
						coleccion.Add($"{dni} - {nombreCompleto}");
					}

					DocClient.AutoCompleteMode = AutoCompleteMode.Suggest;
					DocClient.AutoCompleteSource = AutoCompleteSource.CustomSource;
					DocClient.AutoCompleteCustomSource = coleccion;
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar autocomplete de clientes: " + ex.Message);
			}
		}

		public void SetClientData(string dni, string nombreCompleto) {
			DocClient.Text = dni;
			ClientName.Text = nombreCompleto;
			// Reload autocomplete to include the new client
			CargarAutocompleteClientes();
		}
		
		public void SetProductData(string id, string nombreCompleto) {
			codProd.Text = id;
			prodName.Text = nombreCompleto;
			// Reload autocomplete to include the new product
			CargarAutocompleteProductos();
		}

		private void createClient_Click(object sender, EventArgs e) {
			using (RegisterClientForm form = new RegisterClientForm(this)) {
				form.ShowDialog();
			}
		}
		
		private void button7_Click(object sender, EventArgs e) {
			using (RegisterProductForm form = new RegisterProductForm(this)) {
				form.ShowDialog();
			}
		}
		
		private void loadButton_Click(object sender, EventArgs e) {
			// Validar que todos los campos estén completos
			if (string.IsNullOrWhiteSpace(DocClient.Text)) {
				MessageBox.Show("Por favor, seleccione un cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		
			if (string.IsNullOrWhiteSpace(codProd.Text)) {
				MessageBox.Show("Por favor, seleccione un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		
			if (numericUpDown2.Value <= 0) {
				MessageBox.Show("La cantidad debe ser mayor a 0.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		
			if (string.IsNullOrWhiteSpace(Descripcion.Text)) {
				MessageBox.Show("Por favor, ingrese una descripción del problema.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		
			// Agregar los datos al DataGridView
			int rowIndex = dataGridView2.Rows.Add(
				"", // IDService - se dejará vacío por ahora o se puede auto-generar
				ClientName.Text, // Cliente
				prodName.Text, // Producto
				numericUpDown2.Value, // Cantidad
				Descripcion.Text // Descripcion del Problema
			);
		
			// Opcional: Guardar el ID del producto en el Tag de la fila para uso posterior
			dataGridView2.Rows[rowIndex].Tag = codProd.Text;
		
			// Limpiar los campos después de agregar
			codProd.Clear();
			prodName.Clear();
			numericUpDown2.Value = 0;
			Descripcion.Clear();
			codProd.Focus();
		}
		
		private void button9_Click(object sender, EventArgs e) {
			string busqueda = codProd.Text.Trim();
				
			if(string.IsNullOrWhiteSpace(busqueda)) {
				MessageBox.Show("Por favor, ingrese un código de producto para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
						
					// Extract ID if text contains " - " (autocomplete format)
					string idText = busqueda;
					if(busqueda.Contains(" - ")) {
						string[] parts = busqueda.Split(new string[] { " - " }, StringSplitOptions.None);
						if(parts.Length >= 2) {
							codProd.Text = parts[0].Trim();
							prodName.Text = parts[1].Trim();
							return;
						}
						idText = parts[0].Trim();
					}
						
					string query = @"SELECT p.ID_productos, CONCAT_WS(' ', pg.nombre_generico, m.nombre_marca, mo.nombre_modelo) AS NombreCompleto
									 FROM productos p 
									 JOIN productos_genericos pg ON pg.ID_nombre_productos = p.nombre_producto
									 JOIN marcas m ON m.ID_marcas = p.marca
									 LEFT JOIN modelos mo ON mo.ID_modelos = p.modelo
									 WHERE p.ID_productos LIKE @busqueda
									 OR pg.nombre_generico LIKE @busqueda
									 OR m.nombre_marca LIKE @busqueda
									 OR mo.nombre_modelo LIKE @busqueda
									 OR CONCAT_WS(' ', pg.nombre_generico, m.nombre_marca, mo.nombre_modelo) LIKE @busquedaCompleta
									 LIMIT 1";
						
					MySqlCommand cmd = new MySqlCommand(query, con);
					cmd.Parameters.AddWithValue("@busqueda", $"%{idText}%");
					cmd.Parameters.AddWithValue("@busquedaCompleta", idText);
						
					MySqlDataReader reader = cmd.ExecuteReader();
						
					if(reader.Read()) {
						codProd.Text = reader.GetValue(reader.GetOrdinal("ID_productos")).ToString();
						prodName.Text = reader.GetString("NombreCompleto");
					} else {
						MessageBox.Show("No se encontró ningún producto con ese criterio de búsqueda.", "Producto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
						codProd.Clear();
						prodName.Clear();
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al buscar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buscarCliente_Click(object sender, EventArgs e) {
			string busqueda = DocClient.Text.Trim();
			
			if(string.IsNullOrWhiteSpace(busqueda)) {
				MessageBox.Show("Por favor, ingrese un DNI o nombre para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					
					// Extract DNI if text contains " - " (autocomplete format)
					string dniText = busqueda;
					if(busqueda.Contains(" - ")) {
						string[] parts = busqueda.Split(new string[] { " - " }, StringSplitOptions.None);
						if(parts.Length >= 2) {
							DocClient.Text = parts[0].Trim();
							ClientName.Text = parts[1].Trim();
							return;
						}
						dniText = parts[0].Trim();
					}
					
					string query = @"SELECT c.DNI, CONCAT_WS(' ', p.nombre, p.apellido) as NombreCompleto
									 FROM clientes c
									 INNER JOIN personas p ON c.ID_persona = p.ID_persona
									 WHERE p.tipo = 'c'
									 AND (c.DNI LIKE @busqueda 
									      OR p.nombre LIKE @busqueda 
									      OR p.apellido LIKE @busqueda
									      OR CONCAT_WS(' ', p.nombre, p.apellido) LIKE @busquedaCompleta)
									 LIMIT 1";
					
					MySqlCommand cmd = new MySqlCommand(query, con);
					cmd.Parameters.AddWithValue("@busqueda", $"%{dniText}%");
					cmd.Parameters.AddWithValue("@busquedaCompleta", dniText);
					
					MySqlDataReader reader = cmd.ExecuteReader();
					
					if(reader.Read()) {
						DocClient.Text = reader.GetString("DNI");
						ClientName.Text = reader.GetString("NombreCompleto");
					} else {
						MessageBox.Show("No se encontró ningún cliente con ese criterio de búsqueda.", "Cliente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
						DocClient.Clear();
						ClientName.Clear();
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al buscar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e) {

		}

		private void label15_Click(object sender, EventArgs e) {

		}
	}
}
