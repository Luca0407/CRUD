using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Login_FORM {
	public partial class ModProducto: Form {
		private int idprod;
		private int tipo;
		public ModProducto(int idprod, int tipo) {
			InitializeComponent();
			this.idprod = idprod;
			this.tipo = tipo;
			CargarComboBoxes(tipo);
			ProductoSeleccionado(idprod);
			//this.KeyDown += new KeyEventHandler(add_KeyDown);
			this.KeyPreview = true;
		}
		private void ModProducto_Load(object sender, EventArgs e) {

		}

		private void add_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				button1.Focus();
				//button1_Click(sender, e);
			}
		}

		private void CargarComboBoxes(int tipo) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();

				// Cargar productos genéricos en prodBox
				string sqlProd = @"SELECT ID_nombre_productos, nombre_generico FROM productos_genericos WHERE tipo_producto = @tipo ORDER BY nombre_generico ASC";
				using(MySqlCommand cmdProd = new MySqlCommand(sqlProd, con)) {
					cmdProd.Parameters.AddWithValue("@tipo", tipo);
					MySqlDataAdapter daProd = new MySqlDataAdapter(cmdProd);
					DataTable dtProd = new DataTable();
					daProd.Fill(dtProd);
					prodBox.DataSource = dtProd;
					prodBox.DisplayMember = "nombre_generico";
					prodBox.ValueMember = "ID_nombre_productos";
				}

				// Cargar marcas en brandBox
				string sqlBrand = @"SELECT ID_marcas, nombre_marca FROM marcas ORDER BY nombre_marca ASC";
				MySqlDataAdapter daBrand = new MySqlDataAdapter(sqlBrand, con);
				DataTable dtBrand = new DataTable();
				daBrand.Fill(dtBrand);
				brandBox.DataSource = dtBrand;
				brandBox.DisplayMember = "nombre_marca";
				brandBox.ValueMember = "ID_marcas";

				// Cargar modelos en modelBox
				string sqlModel = @"SELECT ID_modelos, nombre_modelo FROM modelos ORDER BY nombre_modelo ASC";
				MySqlDataAdapter daModel = new MySqlDataAdapter(sqlModel, con);
				DataTable dtModel = new DataTable();
				daModel.Fill(dtModel);
				modelBox.DataSource = dtModel;
				modelBox.DisplayMember = "nombre_modelo";
				modelBox.ValueMember = "ID_modelos";

				string sqlPage = @"SELECT ID_proveedores, pagina FROM proveedores ORDER BY pagina ASC";
				MySqlDataAdapter daPage = new MySqlDataAdapter(sqlPage, con);
				DataTable dtPage = new DataTable();
				daPage.Fill(dtPage);
				pageBox.DataSource = dtPage;
				pageBox.DisplayMember = "pagina";
				pageBox.ValueMember = "ID_proveedores";
			}
		}

		private void ProductoSeleccionado(int id) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				string sql = @"SELECT p.ID_productos, p.nombre_producto, p.marca, p.modelo, 
						p.stock, p.precio_costo, p.precio_venta, p.proveedor
						FROM productos p
						WHERE p.ID_productos = @id
						LIMIT 1;";
				using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@id", id);

						using(MySqlDataReader reader = cmd.ExecuteReader()) {
							if(reader.Read()) {
								// Seleccionar los valores en los ComboBoxes usando los IDs
								prodBox.SelectedValue = reader["nombre_producto"];
								brandBox.SelectedValue = reader["marca"];
								modelBox.SelectedValue = reader["modelo"];
								stockBox.Value = Convert.ToDecimal(reader["stock"]);
								costBox.Value = Convert.ToDecimal(reader["precio_costo"]);
								sellBox.Value = Convert.ToDecimal(reader["precio_venta"]);
								pageBox.SelectedValue = reader["proveedor"];
							} else {
								MessageBox.Show("No se encontró el producto con ese ID.",
									"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
				
				}
			}

		public bool ActualizarProducto(Products datos, int id) {
			try {
				// Validaciones
				if(prodBox.SelectedValue == null ||
				brandBox.SelectedValue == null ||
				modelBox.SelectedValue == null ||
				stockBox.Value < 0 ||
				costBox.Value <= 0 ||
				sellBox.Value <= 0 ||
				pageBox.SelectedValue == null) {
					MessageBox.Show("Faltan datos para realizar la actualización", "Advertencia");
					return false;
				}

				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					// Ahora actualizamos el producto
					string sql = @"UPDATE productos 
							   SET nombre_producto = @NombreProducto,
								   marca = @Marca,
								   modelo = @Modelo,
								   stock = @Stock,
								   precio_costo = @Costo,
								   precio_venta = @Venta,
								   proveedor = @Proveedor
							   WHERE ID_productos = @Id";

					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@NombreProducto", prodBox.SelectedValue);
						cmd.Parameters.AddWithValue("@Marca", brandBox.SelectedValue);
						cmd.Parameters.AddWithValue("@Modelo", modelBox.SelectedValue);
						cmd.Parameters.AddWithValue("@Stock", stockBox.Value);
						cmd.Parameters.AddWithValue("@Costo", costBox.Value);
						cmd.Parameters.AddWithValue("@Venta", sellBox.Value);
						cmd.Parameters.AddWithValue("@Proveedor", pageBox.SelectedValue);
						cmd.Parameters.AddWithValue("@Id", id);

						int rowsAffected = cmd.ExecuteNonQuery();

						if(rowsAffected == 0) {
							MessageBox.Show("No se pudo actualizar el producto", "Error");
							return false;
						}
					}
				}

				return true;
			} catch { return false; }
			}

		private void button1_Click(object sender, EventArgs e) {
			Products producto = new Products() {
				Nombre = prodBox.Text,
				Marca = brandBox.Text,
				Modelo = modelBox.Text,
				Stock = stockBox.Value.ToString(),
				Costo = costBox.Value.ToString(),
				Venta = sellBox.Value.ToString(),
				Pagina = pageBox.Text
			};

			if(ActualizarProducto(producto, this.idprod)) {
				MessageBox.Show("Datos actualizados con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.DialogResult = DialogResult.OK;
				this.Close();
			} else {
				MessageBox.Show("Algo salio mal", "!");
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void Box_KeyPress(object sender, KeyPressEventArgs e) {
			if(char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == ' ') {
				e.Handled = false; // ✅ permitido
			} else {
				e.Handled = true;  // ❌ bloqueado
				MessageBox.Show("Caracter no permitido en el campo", "Advertencia");
			}
		}

		private void NumBox_KeyPress(object sender, KeyPressEventArgs e) {
			if(char.IsNumber(e.KeyChar)) {
				e.Handled = false; // ✅ permitido
			} else {
				e.Handled = true;  // ❌ bloqueado
				MessageBox.Show("Caracter no permitido en el campo", "Advertencia");
			}
		}
	}
}
