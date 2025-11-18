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
		
		// Configure autocomplete for ingreso reparacion search
		CargarAutocompleteIngresoReparacion();
		
		// Configure autocomplete for repuestos (RepBox)
		CargarAutocompleteRepuestos();
		button2.Click += button2_Click;
		
		// Make repName and repPrice readonly
		repName.ReadOnly = true;
		
		// Configure loadServ button
		loadServ.Click += loadServ_Click;
		
		// Configure dataGridView1 CellValueChanged event for Estado column
		dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
		dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
		
		// Configure dataGridView1 CellClick event for X button column
		dataGridView1.CellClick += dataGridView1_CellClick;
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

	private void CargarIngresoReparacion() {
		try {
			using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				
				// Get list of terminated service IDs from dataGridView1
				List<string> terminatedIds = new List<string>();
				foreach (DataGridViewRow row in dataGridView1.Rows) {
					if (!row.IsNewRow) {
						string estado = row.Cells["Estado"].Value?.ToString();
						if (estado != null && estado.ToLower() == "terminado") {
							string idServicio = row.Cells["ID_Ingreso"].Value?.ToString();
							if (!string.IsNullOrEmpty(idServicio)) {
								terminatedIds.Add(idServicio);
							}
						}
					}
				}
				
				// Consulta para obtener los datos de ingreso_reparacion con información adicional
				string sql = @"SELECT ir.idingreso_reparacion, 
							  CONCAT(p.nombre, ' ', p.apellido) as cliente,
							  CONCAT(pg.nombre_generico, ' ', m.nombre_marca, ' ', mo.nombre_modelo) as producto,
							  ir.cantidad,
							  ir.descripcion
							  FROM ingreso_reparacion ir
							  JOIN clientes c ON ir.cliente = c.ID_clientes
							  JOIN personas p ON c.ID_persona = p.ID_persona
							  JOIN productos pr ON ir.producto = pr.ID_productos
							  JOIN productos_genericos pg ON pr.nombre_producto = pg.ID_nombre_productos
							  JOIN marcas m ON pr.marca = m.ID_marcas
							  JOIN modelos mo ON pr.modelo = mo.ID_modelos";
				
				MySqlCommand cmd = new MySqlCommand(sql, con);
				MySqlDataAdapter da = new MySqlDataAdapter(cmd);
				DataTable dt = new DataTable();
				da.Fill(dt);
				
				// Limpiar el DataGridView antes de cargar nuevos datos
				dataGridView2.Rows.Clear();
				
				// Agregar los datos al DataGridView, excluyendo los terminados
				foreach (DataRow row in dt.Rows) {
					string idIngreso = row["idingreso_reparacion"].ToString();
					
					// Skip if this ingreso is already in terminated state
					if (terminatedIds.Contains(idIngreso)) {
						continue;
					}
					
					int rowIndex = dataGridView2.Rows.Add(
						row["idingreso_reparacion"].ToString(), // ID servicio
						row["cliente"].ToString(), // Cliente
						row["producto"].ToString(), // Producto
						row["cantidad"].ToString(), // Cantidad
						row["descripcion"].ToString() // Descripción del problema
					);
					
					// Guardar el ID del ingreso en el Tag de la fila
					dataGridView2.Rows[rowIndex].Tag = row["idingreso_reparacion"];
				}
			}
		} catch (Exception ex) {
			MessageBox.Show("Error al cargar los datos de ingreso reparación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			tabPage3.Text = "Servicios";

			currentDGV = DGVdisp;
			currentTipoProducto = 3;
			CargarProductos(3, DGVdisp);
			
			// Only load other tabs if user is not TECNICO
			if (userRole != 2)
			{
				CargarProductos(1, DGVacc);
				CargarProductos(2, DGVrep);
			}
			
			// Load services data
			CargarServicios();
			
			// Load ingreso reparacion data
			CargarIngresoReparacion();
			
			// Load estados to Estado column in dataGridView1
			CargarEstados();
			
			CargarComboBox(BrandBox, combomarca, "nombre_marca", "ID_marcas");
			CargarComboBox(ModBox, combomodelo, "nombre_modelo", "ID_modelos");
		}

		private void saveButton_Click(object sender, EventArgs e) {
			GuardarIngresoReparacion();
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
			} else if(Products.SelectedTab == tabPage3) {
				// Servicios tab selected - refresh the data
				CargarServicios();
				panel1.Visible = false;
			} else if(Products.SelectedTab == tabPage2) {
				// Ingreso Reparacion tab selected - refresh the data
				CargarIngresoReparacion();
				panel1.Visible = false;
			} else if(Products.SelectedTab == tabPage1) {
				// Servicio Reparacion tab
				// Load estados when entering this tab
				CargarEstados();
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
		
		private void CargarAutocompleteIngresoReparacion() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
									
					// Get list of terminated service IDs from dataGridView1
					List<string> terminatedIds = new List<string>();
					foreach (DataGridViewRow row in dataGridView1.Rows) {
						if (!row.IsNewRow) {
							string estado = row.Cells["Estado"].Value?.ToString();
							if (estado != null && estado.ToLower() == "terminado") {
								string idServicio = row.Cells["ID_Ingreso"].Value?.ToString();
								if (!string.IsNullOrEmpty(idServicio)) {
									terminatedIds.Add(idServicio);
								}
							}
						}
					}
									
					string query = @"SELECT ir.idingreso_reparacion, CONCAT_WS(' ', p.nombre, p.apellido) as NombreCompleto
									 FROM ingreso_reparacion ir
									 JOIN clientes c ON ir.cliente = c.ID_clientes
									 JOIN personas p ON c.ID_persona = p.ID_persona
									 ORDER BY ir.idingreso_reparacion DESC";
					MySqlCommand cmd = new MySqlCommand(query, con);
					MySqlDataReader reader = cmd.ExecuteReader();
		
					AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
		
					while(reader.Read()) {
						string id = reader.GetValue(reader.GetOrdinal("idingreso_reparacion")).ToString();
						string nombreCompleto = reader.GetString("NombreCompleto");
										
						// Skip if this ingreso is already in terminated state
						if (terminatedIds.Contains(id)) {
							continue;
						}
										
						coleccion.Add($"{id} - {nombreCompleto}");
					}
		
					ServBox.AutoCompleteMode = AutoCompleteMode.Suggest;
					ServBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
					ServBox.AutoCompleteCustomSource = coleccion;
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar autocomplete de ingreso reparación: " + ex.Message);
			}
		}
		
		private void CargarEstados() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string query = "SELECT nombre_estado FROM estados ORDER BY ID_estados ASC";
					MySqlCommand cmd = new MySqlCommand(query, con);
					MySqlDataReader reader = cmd.ExecuteReader();
		
					// Clear existing items in Estado column
					DataGridViewComboBoxColumn estadoColumn = (DataGridViewComboBoxColumn)dataGridView1.Columns["Estado"];
					estadoColumn.Items.Clear();
		
					// Add estados from database
					while(reader.Read()) {
						string nombreEstado = reader.GetString("nombre_estado");
						estadoColumn.Items.Add(nombreEstado);
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar estados: " + ex.Message);
			}
		}
		
		private void CargarAutocompleteRepuestos() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string query = @"SELECT p.ID_productos, pg.nombre_generico
									 FROM productos p
									 JOIN productos_genericos pg ON p.nombre_producto = pg.ID_nombre_productos
									 WHERE pg.tipo_producto = 1
									 ORDER BY pg.nombre_generico ASC";
					MySqlCommand cmd = new MySqlCommand(query, con);
					MySqlDataReader reader = cmd.ExecuteReader();
		
					AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
		
					while(reader.Read()) {
						string id = reader.GetValue(reader.GetOrdinal("ID_productos")).ToString();
						string nombreGenerico = reader.GetString("nombre_generico");
						coleccion.Add($"{id} - {nombreGenerico}");
					}
		
					RepBox.AutoCompleteMode = AutoCompleteMode.Suggest;
					RepBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
					RepBox.AutoCompleteCustomSource = coleccion;
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar autocomplete de repuestos: " + ex.Message);
			}
		}
		
		private void button2_Click(object sender, EventArgs e) {
			string busqueda = RepBox.Text.Trim();
		
			if(string.IsNullOrWhiteSpace(busqueda)) {
				MessageBox.Show("Por favor, ingrese un código de repuesto para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
		
					// Extract ID if text contains " - " (autocomplete format)
					string idText = busqueda;
					if(busqueda.Contains(" - ")) {
						string[] parts = busqueda.Split(new string[] { " -" }, StringSplitOptions.None);
						if(parts.Length >= 2) {
							idText = parts[0].Trim();
						}
					}
		
					string query = @"SELECT p.ID_productos, pg.nombre_generico, p.precio_venta
									 FROM productos p
									 JOIN productos_genericos pg ON p.nombre_producto = pg.ID_nombre_productos
									 WHERE pg.tipo_producto = 1
									 AND (p.ID_productos = @id OR pg.nombre_generico LIKE @busqueda)
									 LIMIT 1";
		
					MySqlCommand cmd = new MySqlCommand(query, con);
					cmd.Parameters.AddWithValue("@id", idText);
					cmd.Parameters.AddWithValue("@busqueda", $"%{busqueda}%");
		
					MySqlDataReader reader = cmd.ExecuteReader();
		
					if(reader.Read()) {
						RepBox.Text = reader.GetValue(reader.GetOrdinal("ID_productos")).ToString();
						repName.Text = reader.GetString("nombre_generico");
						repPrice.Value = reader.GetDecimal("precio_venta");
					} else {
						MessageBox.Show("No se encontró ningún repuesto con ese criterio de búsqueda.", "Repuesto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
						RepBox.Clear();
						repName.Clear();
						repPrice.Value = 0;
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al buscar repuesto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		private void GuardarIngresoReparacion() {
			// Validar que haya filas en el DataGridView
			if (dataGridView2.Rows.Count == 0) {
				MessageBox.Show("No hay servicios para guardar. Por favor, agregue al menos un servicio usando el botón 'Cargar'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		
			try {
				using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					
					int serviciosGuardados = 0;
					
					// Recorrer todas las filas del DataGridView
					foreach (DataGridViewRow row in dataGridView2.Rows) {
						if (row.IsNewRow) continue;
						
						// Si la fila ya tiene un ID de servicio, significa que ya fue guardada
						if (!string.IsNullOrWhiteSpace(row.Cells["IDService"].Value?.ToString())) {
							continue;
						}
						
						// Obtener datos de la fila
						string clienteNombre = row.Cells["Cliente"].Value?.ToString();
						string productoNombre = row.Cells["Prod"].Value?.ToString();
						string cantidadStr = row.Cells["Cant"].Value?.ToString();
						string descripcion = row.Cells["Problema"].Value?.ToString();
						
						// Validar que los datos no estén vacíos
						if (string.IsNullOrWhiteSpace(clienteNombre) || 
							string.IsNullOrWhiteSpace(productoNombre) || 
							string.IsNullOrWhiteSpace(cantidadStr) || 
							string.IsNullOrWhiteSpace(descripcion)) {
							continue;
						}
						
						if (!int.TryParse(cantidadStr, out int cantidad) || cantidad <= 0) {
							continue;
						}
						
						// Obtener el ID del producto desde el Tag de la fila
						int productoId = 0;
						if (row.Tag != null && int.TryParse(row.Tag.ToString(), out productoId)) {
							// Usar el ID del Tag
						} else {
							// Si no hay Tag, intentar buscar el producto por nombre
							string sqlBuscarProducto = @"SELECT p.ID_productos 
														FROM productos p 
														JOIN productos_genericos pg ON p.nombre_producto = pg.ID_nombre_productos
														JOIN marcas m ON p.marca = m.ID_marcas
														LEFT JOIN modelos mo ON p.modelo = mo.ID_modelos
														WHERE CONCAT_WS(' ', pg.nombre_generico, m.nombre_marca, mo.nombre_modelo) = @nombreProducto
														LIMIT 1";
							using (MySqlCommand cmdBuscar = new MySqlCommand(sqlBuscarProducto, con)) {
								cmdBuscar.Parameters.AddWithValue("@nombreProducto", productoNombre);
								object result = cmdBuscar.ExecuteScalar();
								if (result != null) {
									productoId = Convert.ToInt32(result);
								} else {
									continue; // No se encontró el producto, saltar esta fila
								}
							}
						}
						
						// Obtener el ID del cliente por nombre
						int clienteId = 0;
						string sqlCliente = @"SELECT c.ID_clientes 
												FROM clientes c 
												INNER JOIN personas p ON c.ID_persona = p.ID_persona 
												WHERE CONCAT_WS(' ', p.nombre, p.apellido) = @nombreCliente
												LIMIT 1";
						using (MySqlCommand cmdCliente = new MySqlCommand(sqlCliente, con)) {
							cmdCliente.Parameters.AddWithValue("@nombreCliente", clienteNombre);
							object result = cmdCliente.ExecuteScalar();
							if (result != null) {
								clienteId = Convert.ToInt32(result);
							} else {
								continue; // No se encontró el cliente, saltar esta fila
							}
						}
						
						// Insertar en la tabla ingreso_reparacion
						string sqlIngreso = @"INSERT INTO ingreso_reparacion (descripcion, producto, cantidad, cliente) 
												VALUES (@descripcion, @producto, @cantidad, @cliente);
												SELECT LAST_INSERT_ID();";
						using (MySqlCommand cmdIngreso = new MySqlCommand(sqlIngreso, con)) {
							cmdIngreso.Parameters.AddWithValue("@descripcion", descripcion);
							cmdIngreso.Parameters.AddWithValue("@producto", productoId);
							cmdIngreso.Parameters.AddWithValue("@cantidad", cantidad);
							cmdIngreso.Parameters.AddWithValue("@cliente", clienteId);
							
							object insertedId = cmdIngreso.ExecuteScalar();
							if (insertedId != null) {
								// Actualizar el ID de servicio en el DataGridView
								row.Cells["IDService"].Value = insertedId.ToString();
								serviciosGuardados++;
							}
						}
					}
					
					if (serviciosGuardados > 0) {
						MessageBox.Show($"Se guardaron {serviciosGuardados} servicio(s) de reparación correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
						
						// Limpiar los campos de texto
						DocClient.Clear();
						ClientName.Clear();
						codProd.Clear();
						prodName.Clear();
						numericUpDown2.Value = 0;
						Descripcion.Clear();
					} else {
						MessageBox.Show("No se guardó ningún servicio. Todos los servicios ya fueron guardados o tienen datos incompletos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			} catch (Exception ex) {
				MessageBox.Show("Error al guardar los ingresos de reparación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void delButton_Click(object sender, EventArgs e) {
			EliminarProducto();
		}

		public void EliminarProducto() {
			if (currentDGV.SelectedRows.Count == 0) {
				MessageBox.Show("Selecciona al menos una fila para eliminar.");
				return;
			}

			DialogResult result = MessageBox.Show(
				"¿Seguro que deseas eliminar los registros seleccionados?",
				"Confirmar eliminación",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning
			);

			if (result != DialogResult.Yes)
				return;

			try {
				using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();

					foreach (DataGridViewRow fila in currentDGV.SelectedRows) {
						if (fila.IsNewRow)
							continue;

						string id = fila.Cells["ID_productos"].Value?.ToString();
						if (string.IsNullOrEmpty(id))
							continue;

						// Eliminar el producto
						try {
							string sqlDeleteProducto = "DELETE FROM productos WHERE ID_productos = @id";
							using (MySqlCommand cmdDelProducto = new MySqlCommand(sqlDeleteProducto, con)) {
								cmdDelProducto.Parameters.AddWithValue("@id", id);
								cmdDelProducto.ExecuteNonQuery();
							}

							// Quitar la fila visualmente
							currentDGV.Rows.Remove(fila);
						} catch (MySqlException ex) {
							if (ex.Number == 1451) { // Error de restricción de clave foránea
								MessageBox.Show($"No se pudo eliminar el producto con ID {id} porque está vinculado a una venta o reparación.", 
									"Error de eliminación", MessageBoxButtons.OK, MessageBoxIcon.Error);
								return;
							} else {
								throw;
							}
						}
					}

					MessageBox.Show("Eliminación completada.");
				}
			} catch (Exception ex) {
				MessageBox.Show("Error al eliminar: " + ex.Message);
			}
		}

		private void tabPage2_Click(object sender, EventArgs e) {

		}

		private void addButton_Click(object sender, EventArgs e) {
			using(RegisterProductForm pf = new RegisterProductForm()) {
				if(pf.ShowDialog() == DialogResult.OK) {
					// Recargar el grid actual después de agregar el producto
					CargarProductos(currentTipoProducto, currentDGV);
					MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
				} else {
					MessageBox.Show("Inserción cancelada", "Cancelada");
				}
			}
		}

		private void DGVrep_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void CargarServicios() {
		using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
			con.Open();

			string sql = @"SELECT ID_servicios as 'ID Servicio', descripcion as 'Descripción', precio as 'Precio' 
						  FROM servicios";

			MySqlCommand cmd = new MySqlCommand(sql, con);
			MySqlDataAdapter da = new MySqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);

			dataGridView3.DataSource = dt;
		}
	}
	
		private void loadServ_Click(object sender, EventArgs e) {
			// Validar que todos los campos estén completos
			if (string.IsNullOrWhiteSpace(ServBox.Text)) {
				MessageBox.Show("Por favor, ingrese un ID de servicio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(RepBox.Text)) {
				MessageBox.Show("Por favor, seleccione un repuesto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (servPrice.Value <= 0) {
				MessageBox.Show("El precio del servicio debe ser mayor a 0.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();

					// Extract ID if text contains " - " (autocomplete format)
					string idIngresoText = ServBox.Text.Trim();
					if(ServBox.Text.Contains(" - ")) {
						string[] parts = ServBox.Text.Split(new string[] { " -" }, StringSplitOptions.None);
						if(parts.Length >= 2) {
							idIngresoText = parts[0].Trim();
						}
					}

					// Get ingreso_reparacion data including client name and cantidad
					string queryIngreso = @"SELECT ir.idingreso_reparacion, ir.cantidad,
											CONCAT_WS(' ', p.nombre, p.apellido) as NombreCliente
											FROM ingreso_reparacion ir
											JOIN clientes c ON ir.cliente = c.ID_clientes
											JOIN personas p ON c.ID_persona = p.ID_persona
											WHERE ir.idingreso_reparacion = @idIngreso";

					MySqlCommand cmdIngreso = new MySqlCommand(queryIngreso, con);
					cmdIngreso.Parameters.AddWithValue("@idIngreso", idIngresoText);
					MySqlDataReader readerIngreso = cmdIngreso.ExecuteReader();

					if(!readerIngreso.Read()) {
						MessageBox.Show("No se encontró el servicio de reparación especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						readerIngreso.Close();
						return;
					}

					string idServicio = readerIngreso.GetValue(readerIngreso.GetOrdinal("idingreso_reparacion")).ToString();
					int cantidad = readerIngreso.GetInt32("cantidad");
					string nombreCliente = readerIngreso.GetString("NombreCliente");
					readerIngreso.Close();

					// Calculate SubTotal: (repPrice * cantidad) + servPrice
					decimal precioRepuesto = repPrice.Value;
					decimal precioServicio = servPrice.Value;
					decimal subtotal = (precioRepuesto * cantidad) + precioServicio;
					
					// Get repuesto ID from RepBox
					string idRepuestoText = RepBox.Text.Trim();
					if(RepBox.Text.Contains(" - ")) {
						string[] parts = RepBox.Text.Split(new string[] { " -" }, StringSplitOptions.None);
						if(parts.Length >= 2) {
							idRepuestoText = parts[0].Trim();
						}
					}

					// Add row to dataGridView1
					int rowIndex = dataGridView1.Rows.Add(
						idServicio,          // ID servicio
						nombreCliente,       // Nombre del Cliente
						repName.Text,        // Repuesto (nombre_generico)
						precioRepuesto,      // Precio Unitario
						cantidad,            // Cantidad
						subtotal,            // SubTotal
						"en espera"         // Estado (default)
					);
					
					// Store repuesto ID in the row's Tag for later use
					dataGridView1.Rows[rowIndex].Tag = idRepuestoText;

					// Clear all input fields
					ServBox.Clear();
					RepBox.Clear();
					repName.Clear();
					repPrice.Value = 0;
					servPrice.Value = 0;
					ServBox.Focus();
					
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar el servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	
		private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
			if (dataGridView1.IsCurrentCellDirty) {
				dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}
			
		private void GuardarServiciosTerminados() {
			try {
				using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
						
					int serviciosGuardados = 0;
						
					// Iterate through all rows in dataGridView1
					foreach (DataGridViewRow row in dataGridView1.Rows) {
						if (row.IsNewRow) continue;
							
						// Check if estado is "terminado"
						string estado = row.Cells["Estado"].Value?.ToString();
						if (estado == null || estado.ToLower() != "terminado") continue;
							
						// Get data from the row
						string idIngresoStr = row.Cells["ID_Ingreso"].Value?.ToString();
						string subtotalStr = row.Cells["SubTotal"].Value?.ToString();
						string idRepuestoStr = row.Tag?.ToString(); // Get repuesto ID from Tag
							
						if (string.IsNullOrWhiteSpace(idIngresoStr) || 
							string.IsNullOrWhiteSpace(subtotalStr) || 
							string.IsNullOrWhiteSpace(idRepuestoStr)) {
							continue;
						}
							
						if (!int.TryParse(idIngresoStr, out int idIngreso) ||
							!decimal.TryParse(subtotalStr, out decimal subtotal) ||
							!int.TryParse(idRepuestoStr, out int idRepuesto)) {
							continue;
						}
							
						// Get estado ID from database
						string queryEstadoId = "SELECT ID_estados FROM estados WHERE nombre_estado = @nombreEstado";
						MySqlCommand cmdEstado = new MySqlCommand(queryEstadoId, con);
						cmdEstado.Parameters.AddWithValue("@nombreEstado", estado);
						object estadoIdObj = cmdEstado.ExecuteScalar();
							
						if (estadoIdObj == null || !int.TryParse(estadoIdObj.ToString(), out int estadoId)) {
							continue;
						}
							
						// Check if this service already exists in servicio_reparacion
						string queryCheck = @"SELECT COUNT(*) FROM servicio_reparacion 
											  WHERE ingreso = @ingreso AND repuesto = @repuesto";
						MySqlCommand cmdCheck = new MySqlCommand(queryCheck, con);
						cmdCheck.Parameters.AddWithValue("@ingreso", idIngreso);
						cmdCheck.Parameters.AddWithValue("@repuesto", idRepuesto);
						int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
							
						if (count > 0) {
							// Already exists, skip
							continue;
						}
							
						// Insert into servicio_reparacion
						string sqlInsert = @"INSERT INTO servicio_reparacion 
											 (ingreso, repuesto, subtotal, estado) 
											 VALUES (@ingreso, @repuesto, @subtotal, @estado)";
							
						MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, con);
						cmdInsert.Parameters.AddWithValue("@ingreso", idIngreso);
						cmdInsert.Parameters.AddWithValue("@repuesto", idRepuesto);
						cmdInsert.Parameters.AddWithValue("@subtotal", subtotal);
						cmdInsert.Parameters.AddWithValue("@estado", estadoId);
							
						cmdInsert.ExecuteNonQuery();
						serviciosGuardados++;
					}
						
					if (serviciosGuardados > 0) {
						MessageBox.Show($"Se guardaron {serviciosGuardados} servicio(s) terminado(s) en la base de datos.", 
										"Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			} catch (Exception ex) {
				MessageBox.Show("Error al guardar servicios terminados: " + ex.Message, 
								"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	
		private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
			// Check if the changed cell is in the Estado column
			if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Estado"].Index) {
				DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
				string estado = row.Cells["Estado"].Value?.ToString();

				// If estado is "terminado", refresh the ingreso_reparacion tab
				if (estado != null && estado.ToLower() == "terminado") {
					// Reload the ingreso_reparacion data to exclude terminated entries
					CargarIngresoReparacion();
					// Also refresh the autocomplete for ServBox
					CargarAutocompleteIngresoReparacion();
				}
			}
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
			// Check if the clicked cell is in the X button column
			if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["x"].Index) {
				try {
					// Confirm deletion
					DialogResult result = MessageBox.Show(
						"¿Está seguro de eliminar este servicio?",
						"Confirmar eliminación",
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question
					);
					
					if (result == DialogResult.Yes) {
						// Remove the row
						dataGridView1.Rows.RemoveAt(e.RowIndex);
					}
				} catch (Exception ex) {
					MessageBox.Show("Error al eliminar el servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

        private void label5_Click(object sender, EventArgs e)
        {

        }

		private void saveServ_Click(object sender, EventArgs e) {
			GuardarServiciosTerminados();
		}

		private void button7_Click_1(object sender, EventArgs e) {

		}
	}
}
