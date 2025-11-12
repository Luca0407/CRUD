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
    public partial class ventas : Form
    {
		private decimal totalVenta = 0;

        public ventas()
        {
            InitializeComponent();
			ListarProducto();
			ListarNombres();
			CargarMetodosPago();
			dataGridView1.Columns["X"].Width = 37;
			dataGridView1.CellContentClick += DataGridView1_CellContentClick;
			comboBox1.SelectedIndex = 0;
			// Set numeric updown minimum
			numericUpDown1.Minimum = 1;
			numericUpDown1.Value = 1;
			// Make price and stock fields readonly
			textBox6.ReadOnly = true;
			textBox7.ReadOnly = true;
			textBox8.ReadOnly = true;
			textBox10.ReadOnly = true;
			NameBox.ReadOnly = true;
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			// Document type changed
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ventas_Load(object sender, EventArgs e)
        {
			DateBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

		private void ListarNombres() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string nombres = @"SELECT DISTINCT c.dni, CONCAT_WS(' ', p.nombre, p.apellido) as NombreCompleto FROM personas p
									JOIN clientes c ON p.ID_persona = c.ID_persona WHERE tipo = 'c' AND c.dni LIKE @busqueda;";
					MySqlCommand cmd = new MySqlCommand(nombres, con);
					// Usamos el comodín para que el autocompletado funcione (busca IDs que COMIENZAN con el texto)
					cmd.Parameters.AddWithValue("@busqueda", "%" + DocNum.Text + "%");

					MySqlDataReader reader = cmd.ExecuteReader();

					AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

					while(reader.Read()) {
						// 1. Obtener el ID (usando GetString() o GetInt32().ToString(), dependiendo del driver/versión)
						string id = reader.GetValue(reader.GetOrdinal("dni")).ToString();

						// 2. Obtener el Nombre Completo
						string nombre = reader.GetString("NombreCompleto");

						// 3. CONCATENAR AMBOS para la sugerencia: "ID - Nombre Completo"
						string sugerencia = id + " - " + nombre;

						// Agregar la sugerencia completa a la colección
						coleccion.Add(sugerencia);
					}

					DocNum.AutoCompleteMode = AutoCompleteMode.Suggest;
					DocNum.AutoCompleteSource = AutoCompleteSource.CustomSource;
					DocNum.AutoCompleteCustomSource = coleccion;
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar clientes: " + ex.Message);
			}
		}

		private void ListarProducto() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();

					// La consulta SQL es correcta para filtrar por ID y obtener el nombre concatenado.
					string nombres = @"SELECT DISTINCT p.ID_productos, CONCAT_WS(' ', pg.nombre_generico, m.nombre_marca, mo.nombre_modelo) AS NombreCompleto
									FROM productos p 
									JOIN productos_genericos pg ON pg.ID_nombre_productos = p.nombre_producto
									JOIN marcas m ON m.ID_marcas = p.marca
									LEFT JOIN modelos mo ON mo.ID_modelos = p.modelo
									WHERE p.ID_productos LIKE @busqueda;";

					MySqlCommand cmd = new MySqlCommand(nombres, con);
					// Usamos el comodín para que el autocompletado funcione (busca IDs que COMIENZAN con el texto)
					cmd.Parameters.AddWithValue("@busqueda", "%" + CodProd.Text + "%");

					MySqlDataReader reader = cmd.ExecuteReader();

					AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

					while(reader.Read()) {
						// 1. Obtener el ID (usando GetString() o GetInt32().ToString(), dependiendo del driver/versión)
						string id = reader.GetValue(reader.GetOrdinal("ID_productos")).ToString();

						// 2. Obtener el Nombre Completo
						string nombre = reader.GetString("NombreCompleto");

						// 3. CONCATENAR AMBOS para la sugerencia: "ID - Nombre Completo"
						string sugerencia = id + " - " + nombre;

						// Agregar la sugerencia completa a la colección
						coleccion.Add(sugerencia);
					}

					CodProd.AutoCompleteMode = AutoCompleteMode.Suggest;
					CodProd.AutoCompleteSource = AutoCompleteSource.CustomSource;
					CodProd.AutoCompleteCustomSource = coleccion;
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar productos: " + ex.Message);
			}
		}

		private void CargarMetodosPago() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string sql = "SELECT ID_pagos, metodo_pago FROM pagos";
					MySqlCommand cmd = new MySqlCommand(sql, con);
					MySqlDataReader reader = cmd.ExecuteReader();

					comboBox1.Items.Clear();
					while(reader.Read()) {
						comboBox1.Items.Add(new { 
							Text = reader.GetString("metodo_pago"), 
							Value = reader.GetInt32("ID_pagos") 
						});
					}
					comboBox1.DisplayMember = "Text";
					comboBox1.ValueMember = "Value";
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar métodos de pago: " + ex.Message);
			}
		}

		private void CargarProducto(int idProducto) {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string sql = @"SELECT p.ID_productos, 
										CONCAT_WS(' ', pg.nombre_generico, m.nombre_marca, mo.nombre_modelo) AS NombreCompleto,
										p.precio_venta,
										p.stock
									FROM productos p
									JOIN productos_genericos pg ON pg.ID_nombre_productos = p.nombre_producto
									JOIN marcas m ON m.ID_marcas = p.marca
									LEFT JOIN modelos mo ON mo.ID_modelos = p.modelo
									WHERE p.ID_productos = @id";

					MySqlCommand cmd = new MySqlCommand(sql, con);
					cmd.Parameters.AddWithValue("@id", idProducto);
					MySqlDataReader reader = cmd.ExecuteReader();

					if(reader.Read()) {
						textBox5.Text = reader.GetString("NombreCompleto");
						textBox6.Text = reader.GetDecimal("precio_venta").ToString("N2");
						textBox7.Text = reader.GetInt32("stock").ToString();
						numericUpDown1.Maximum = reader.GetInt32("stock");
						numericUpDown1.Value = 1;
					} else {
						MessageBox.Show("Producto no encontrado");
						LimpiarCamposProducto();
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar producto: " + ex.Message);
			}
		}

		private void LimpiarCamposProducto() {
			textBox5.Clear();
			textBox6.Clear();
			textBox7.Clear();
			numericUpDown1.Value = 1;
		}

		private void AgregarProductoAGrid() {
			if(string.IsNullOrWhiteSpace(CodProd.Text)) {
				MessageBox.Show("Ingrese un código de producto");
				return;
			}

			if(string.IsNullOrWhiteSpace(textBox5.Text)) {
				MessageBox.Show("Busque el producto primero");
				return;
			}

			if(numericUpDown1.Value <= 0) {
				MessageBox.Show("La cantidad debe ser mayor a 0");
				return;
			}

			// Get product ID from CodProd textbox
			string codProdText = CodProd.Text.Trim();
			int idProducto;
			if(codProdText.Contains("-")) {
				string[] parts = codProdText.Split('-');
				if(!int.TryParse(parts[0].Trim(), out idProducto)) {
					MessageBox.Show("Código de producto inválido");
					return;
				}
			} else {
				if(!int.TryParse(codProdText, out idProducto)) {
					MessageBox.Show("Código de producto inválido");
					return;
				}
			}

			string producto = textBox5.Text;
			decimal precio = decimal.Parse(textBox6.Text);
			int cantidad = (int)numericUpDown1.Value;
			decimal subtotal = precio * cantidad;

			// Check if product already exists in grid
			foreach(DataGridViewRow row in dataGridView1.Rows) {
				if(row.Cells["Producto"].Value != null && 
				   row.Cells["Producto"].Value.ToString() == producto) {
					MessageBox.Show("El producto ya está en la lista");
					return;
				}
			}

			// Add to grid with ID as Tag
			int rowIndex = dataGridView1.Rows.Add(producto, precio.ToString("N2"), cantidad, subtotal.ToString("N2"), "X");
			dataGridView1.Rows[rowIndex].Tag = idProducto; // Store product ID

			ActualizarTotal();
			LimpiarCamposProducto();
			CodProd.Clear();
			CodProd.Focus();
		}

		private void ActualizarTotal() {
			totalVenta = 0;
			foreach(DataGridViewRow row in dataGridView1.Rows) {
				if(row.Cells["SubTotal"].Value != null) {
					decimal subtotal = decimal.Parse(row.Cells["SubTotal"].Value.ToString());
					totalVenta += subtotal;
				}
			}
			textBox8.Text = totalVenta.ToString("N2");
			CalcularCambio();
		}

		private void CalcularCambio() {
			if(decimal.TryParse(textBox9.Text, out decimal pagaCon)) {
				decimal cambio = pagaCon - totalVenta;
				textBox10.Text = cambio >= 0 ? cambio.ToString("N2") : "0.00";
			} else {
				textBox10.Text = "0.00";
			}
		}

		private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
			if(e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["X"].Index) {
				dataGridView1.Rows.RemoveAt(e.RowIndex); //try
				ActualizarTotal();
			}
		}

		private void GuardarVenta() {
			try {
				// Validations
				if(string.IsNullOrWhiteSpace(DocNum.Text)) {
					MessageBox.Show("Seleccione un cliente");
					return;
				}

				// Check if there are actual products (excluding the empty row)
				int productCount = 0;
				foreach(DataGridViewRow row in dataGridView1.Rows) {
					if(row.Tag != null && !row.IsNewRow) {
						productCount++;
					}
				}

				if(productCount == 0) {
					MessageBox.Show("Agregue productos a la venta");
					return;
				}

				if(comboBox1.SelectedIndex < 0) {
					MessageBox.Show("Seleccione un método de pago");
					return;
				}

				if(!decimal.TryParse(textBox9.Text, out decimal pagaCon) || pagaCon < totalVenta) {
					MessageBox.Show("El monto pagado debe ser mayor o igual al total");
					return;
				}

				// Get client DNI
				string dniText = DocNum.Text.Trim();
				string dni = dniText.Contains("-") ? dniText.Split('-')[0].Trim() : dniText;

				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					MySqlTransaction transaction = con.BeginTransaction();

					try {
						// Get client ID
						string sqlCliente = "SELECT ID_clientes FROM clientes WHERE DNI = @dni";
						MySqlCommand cmdCliente = new MySqlCommand(sqlCliente, con, transaction);
						cmdCliente.Parameters.AddWithValue("@dni", dni);
						object clienteId = cmdCliente.ExecuteScalar();

						if(clienteId == null || clienteId == DBNull.Value) {
							MessageBox.Show("Cliente no encontrado");
							transaction.Rollback();
							return;
						}

						// ✅ Get employee with activo = 1
						string sqlEmpleado = "SELECT ID_empleados FROM empleados WHERE activo = 1 LIMIT 1";
						MySqlCommand cmdEmpleado = new MySqlCommand(sqlEmpleado, con, transaction);
						object empleadoObj = cmdEmpleado.ExecuteScalar();

						if(empleadoObj == null || empleadoObj == DBNull.Value) {
							MessageBox.Show("Error: No se encontró ningún empleado activo en sesión.");
							transaction.Rollback();
							return;
						}

						int empleadoId = Convert.ToInt32(empleadoObj);

						// Validate stock availability BEFORE processing the sale
						foreach(DataGridViewRow row in dataGridView1.Rows) {
							if(row.Tag != null && !row.IsNewRow) {
								int productoId = (int) row.Tag;
								int cantidadSolicitada = int.Parse(row.Cells["Cantidad"].Value.ToString());

								string sqlCheckStock = "SELECT stock FROM productos WHERE ID_productos = @id";
								MySqlCommand cmdCheckStock = new MySqlCommand(sqlCheckStock, con, transaction);
								cmdCheckStock.Parameters.AddWithValue("@id", productoId);
								object stockObj = cmdCheckStock.ExecuteScalar();

								if(stockObj == null) {
									MessageBox.Show($"Producto ID {productoId} no encontrado");
									transaction.Rollback();
									return;
								}

								int stockDisponible = Convert.ToInt32(stockObj);
								if(stockDisponible < cantidadSolicitada) {
									MessageBox.Show($"Stock insuficiente para el producto '{row.Cells["Producto"].Value}'.\nDisponible: {stockDisponible}, Solicitado: {cantidadSolicitada}");
									transaction.Rollback();
									return;
								}
							}
						}

						// Get or create caja
						string sqlCaja = "SELECT ID_caja FROM caja WHERE dia_cierre IS NULL LIMIT 1";
						MySqlCommand cmdCaja = new MySqlCommand(sqlCaja, con, transaction);
						object cajaId = cmdCaja.ExecuteScalar();

						if(cajaId == null || cajaId == DBNull.Value) {
							// Create a new caja
							string sqlCrearCaja = @"INSERT INTO caja (dia_hoy, `dinero inicial`, empleado_apertura) 
											VALUES (@fecha, 0, @empleado)";
							MySqlCommand cmdCrearCaja = new MySqlCommand(sqlCrearCaja, con, transaction);
							cmdCrearCaja.Parameters.AddWithValue("@fecha", DateTime.Today);
							cmdCrearCaja.Parameters.AddWithValue("@empleado", empleadoId);
							cmdCrearCaja.ExecuteNonQuery();
							cajaId = cmdCrearCaja.LastInsertedId;
						}

						// Get payment method ID
						dynamic selectedPago = comboBox1.SelectedItem;
						int tipoPagoId = selectedPago.Value;

						// Insert venta
						string sqlVenta = @"INSERT INTO ventas (fecha_venta, costo_total, costo_pagado, ID_cliente, ID_empleado, ID_caja, tipo_pago)
									VALUES (@fecha, @total, @pagado, @cliente, @empleado, @caja, @tipoPago)";
						MySqlCommand cmdVenta = new MySqlCommand(sqlVenta, con, transaction);
						cmdVenta.Parameters.AddWithValue("@fecha", DateTime.Today);
						cmdVenta.Parameters.AddWithValue("@total", totalVenta);
						cmdVenta.Parameters.AddWithValue("@pagado", pagaCon);
						cmdVenta.Parameters.AddWithValue("@cliente", clienteId);
						cmdVenta.Parameters.AddWithValue("@empleado", empleadoId);
						cmdVenta.Parameters.AddWithValue("@caja", cajaId);
						cmdVenta.Parameters.AddWithValue("@tipoPago", tipoPagoId);
						cmdVenta.ExecuteNonQuery();
						long ventaId = cmdVenta.LastInsertedId;

						// Insert detalles_venta and update stock
						foreach(DataGridViewRow row in dataGridView1.Rows) {
							if(row.Tag != null && !row.IsNewRow) {
								int productoId = (int) row.Tag;
								int cantidad = int.Parse(row.Cells["Cantidad"].Value.ToString());
								decimal precioUnit = decimal.Parse(row.Cells["Precio"].Value.ToString());
								decimal subtotal = decimal.Parse(row.Cells["SubTotal"].Value.ToString());

								string sqlDetalle = @"INSERT INTO detalles_venta (cantidad, precio_unitario, subtotal, pagado, producto, ID_venta)
												VALUES (@cantidad, @precio, @subtotal, 1, @producto, @venta)";
								MySqlCommand cmdDetalle = new MySqlCommand(sqlDetalle, con, transaction);
								cmdDetalle.Parameters.AddWithValue("@cantidad", cantidad);
								cmdDetalle.Parameters.AddWithValue("@precio", precioUnit);
								cmdDetalle.Parameters.AddWithValue("@subtotal", subtotal);
								cmdDetalle.Parameters.AddWithValue("@producto", productoId);
								cmdDetalle.Parameters.AddWithValue("@venta", ventaId);
								cmdDetalle.ExecuteNonQuery();

								string sqlStock = "UPDATE productos SET stock = stock - @cantidad WHERE ID_productos = @id";
								MySqlCommand cmdStock = new MySqlCommand(sqlStock, con, transaction);
								cmdStock.Parameters.AddWithValue("@cantidad", cantidad);
								cmdStock.Parameters.AddWithValue("@id", productoId);
								cmdStock.ExecuteNonQuery();
							}
						}

						transaction.Commit();
						MessageBox.Show("Venta registrada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
						LimpiarFormulario();

					} catch(Exception ex) {
						transaction.Rollback();
						throw new Exception("Error en la transacción: " + ex.Message);
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al guardar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}



		private void LimpiarFormulario() {
			DocNum.Clear();
			NameBox.Clear();
			CodProd.Clear();
			LimpiarCamposProducto();
			dataGridView1.Rows.Clear();
			textBox8.Clear();
			textBox9.Clear();
			textBox10.Clear();
			totalVenta = 0;
			DateBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
			DocNum.Focus();
		}

		private void button3_Click(object sender, EventArgs e) {
			// Search product button
			if(string.IsNullOrWhiteSpace(CodProd.Text)) {
				MessageBox.Show("Ingrese un código de producto");
				return;
			}

			string codProdText = CodProd.Text.Trim();
			int idProducto;

			// Extract ID from autocomplete format "ID - Name" or just ID
			if(codProdText.Contains("-")) {
				string[] parts = codProdText.Split('-');
				if(!int.TryParse(parts[0].Trim(), out idProducto)) {
					MessageBox.Show("Código de producto inválido");
					return;
				}
			} else {
				if(!int.TryParse(codProdText, out idProducto)) {
					MessageBox.Show("Código de producto inválido");
					return;
				}
			}

			CargarProducto(idProducto);
		}

		private void button1_Click(object sender, EventArgs e) {
			// Search client button
			if(string.IsNullOrWhiteSpace(DocNum.Text)) {
				MessageBox.Show("Ingrese un DNI");
				return;
			}

			string dniText = DocNum.Text.Trim();
			string dni;
			
			// Extract DNI and name from autocomplete format "DNI - Name"
			if(dniText.Contains("-")) {
				string[] parts = dniText.Split(new string[] { " - " }, StringSplitOptions.None);
				if(parts.Length >= 2) {
					DocNum.Text = parts[0].Trim();
					NameBox.Text = parts[1].Trim();
				} else {
					dni = dniText;
				}
			} else {
				dni = dniText;
				// Search in database for name
				try {
					using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
						con.Open();
						string sql = "SELECT CONCAT_WS(' ', p.nombre, p.apellido) as NombreCompleto FROM personas p JOIN clientes c ON p.ID_persona = c.ID_persona WHERE c.dni = @dni";
						MySqlCommand cmd = new MySqlCommand(sql, con);
						cmd.Parameters.AddWithValue("@dni", dni);
						object resultado = cmd.ExecuteScalar();
						if(resultado != null) {
							NameBox.Text = resultado.ToString();

						} else {
							MessageBox.Show("Cliente no encontrado");
							NameBox.Clear();
						}
					}
				} catch(Exception ex) {
					MessageBox.Show("Error al buscar cliente: " + ex.Message);
				}
			}
		}

		private void button5_Click(object sender, EventArgs e) {
			DateBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
		}

		private void button4_Click(object sender, EventArgs e) {
			// Create sale
			GuardarVenta();
		}

		private void textBox9_TextChanged(object sender, EventArgs e) {
			
		}

		private void button2_Click_1(object sender, EventArgs e) {
			// Add product to cart
			AgregarProductoAGrid();
		}

		private void textBox9_TextChanged_1(object sender, EventArgs e) {
			// Calculate change when "paga con" changes
			CalcularCambio();
		}

		private void button4_Click_1(object sender, EventArgs e) {
			// Create sale
			GuardarVenta();
		}

		private void button6_Click(object sender, EventArgs e) {
			textBox9.Text = textBox8.Text;
		}
	}

}
