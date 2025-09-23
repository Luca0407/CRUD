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

namespace Simple_Login_FORM {
	public partial class RepuestosServiciosForm: Form {
		// Ajusta esta cadena con tu configuración de MySQL

		// Para saber si estamos editando y el id seleccionado
		private int selectedId = -1;
		private bool editingProductos = true; // true = repuestos, false = servicios

		public RepuestosServiciosForm() {
			InitializeComponent();
			InitializeCustomComponents();
			LoadData(); // carga inicial
		}

		private void InitializeCustomComponents() {
			// Si usas diseñador, omite esta sección y asegúrate de tener:
			// dgvItems, rbRepuestos, rbServicios, txtNombre, txtDescripcion, numPrecio, numStock,
			// btnAgregar, btnActualizar, btnEliminar, btnLimpiar, btnRefrescar
			//
			// Aquí se asume que ya existen en el diseñador.
			// Conecta eventos:
			rbRepuestos.CheckedChanged += (s, e) => { editingProductos = true; ClearForm(); LoadData(); };
			rbServicios.CheckedChanged += (s, e) => { editingProductos = false; ClearForm(); LoadData(); };

			DGVItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			DGVItems.MultiSelect = false;
			DGVItems.ReadOnly = true;
			DGVItems.CellClick += DgvItems_CellClick;

			btnAgregar.Click += BtnAgregar_Click;
			btnActualizar.Click += BtnActualizar_Click;
			btnEliminar.Click += BtnEliminar_Click;
			btnLimpiar.Click += (s, e) => ClearForm();
			btnRefrescar.Click += (s, e) => LoadData();
		}

		private void LoadData() {
			string sql;
			if(editingProductos)
				sql = "SELECT tipo_producto, marca, stock, proveedor, precio_costo, precio_venta FROM productos;";
			else
				sql = "SELECT descripcion, precio FROM servicios;";

			DataTable dt = new DataTable();
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
				using(var cmd = new MySqlCommand(sql, con))
				using(var adapter = new MySqlDataAdapter(cmd)) {
					adapter.Fill(dt);
				}

				DGVItems.DataSource = dt;

				// Ajustes visuales mínimos
				if(editingProductos) {
					if(DGVItems.Columns["stock"] != null)
						DGVItems.Columns["stock"].HeaderText = "Stock";
					if(DGVItems.Columns["precio_costo"] != null)
						DGVItems.Columns["precio_costo"].DefaultCellStyle.Format = "N2";
					if(DGVItems.Columns["precio_venta"] != null)
						DGVItems.Columns["precio_venta"].DefaultCellStyle.Format = "N2";
				} else {
					if(DGVItems.Columns["precio"] != null)
						DGVItems.Columns["precio"].DefaultCellStyle.Format = "N2";
				}
			} catch(Exception ex) {
				MessageBox.Show("Error cargando datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DgvItems_CellClick(object sender, DataGridViewCellEventArgs e) {
			if(e.RowIndex < 0)
				return;

			var row = DGVItems.Rows[e.RowIndex];
			if(row == null)
				return;

			selectedId = Convert.ToInt32(row.Cells["id"].Value);
			txtNombre.Text = row.Cells["nombre"].Value?.ToString();
			txtDescripcion.Text = row.Cells["descripcion"].Value?.ToString();

			numPrecio.Value = row.Cells["precio"].Value != DBNull.Value
				? Convert.ToDecimal(row.Cells["precio"].Value)
				: 0;

			if(editingProductos) {
				numStock.Value = row.Cells["stock"].Value != DBNull.Value
					? Convert.ToDecimal(row.Cells["stock"].Value)
					: 0;
			} else {
				numStock.Value = 0;
			}
		}

		private void BtnAgregar_Click(object sender, EventArgs e) {
			string nombre = txtNombre.Text.Trim();
			string descripcion = txtDescripcion.Text.Trim();
			decimal precio = numPrecio.Value;
			int stock = (int) numStock.Value;

			if(string.IsNullOrEmpty(nombre)) {
				MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string sql;
			if(editingProductos)
				sql = "INSERT INTO repuestos (nombre, descripcion, precio, stock) VALUES (@nombre, @descripcion, @precio, @stock)";
			else
				sql = "INSERT INTO servicios (nombre, descripcion, precio) VALUES (@nombre, @descripcion, @precio)";

			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
				using(var cmd = new MySqlCommand(sql, con)) {
					cmd.Parameters.AddWithValue("@nombre", nombre);
					cmd.Parameters.AddWithValue("@descripcion", descripcion);
					cmd.Parameters.AddWithValue("@precio", precio);
					if(editingProductos)
						cmd.Parameters.AddWithValue("@stock", stock);

					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
				}

				MessageBox.Show("Registro agregado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
				ClearForm();
				LoadData();
			} catch(Exception ex) {
				MessageBox.Show("Error al agregar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnActualizar_Click(object sender, EventArgs e) {
			if(selectedId < 0) {
				MessageBox.Show("Selecciona un registro para actualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string nombre = txtNombre.Text.Trim();
			string descripcion = txtDescripcion.Text.Trim();
			decimal precio = numPrecio.Value;
			int stock = (int) numStock.Value;

			if(string.IsNullOrEmpty(nombre)) {
				MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string sql;
			if(editingProductos)
				sql = "UPDATE repuestos SET nombre=@nombre, descripcion=@descripcion, precio=@precio, stock=@stock WHERE id=@id";
			else
				sql = "UPDATE servicios SET nombre=@nombre, descripcion=@descripcion, precio=@precio WHERE id=@id";

			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
				using(var cmd = new MySqlCommand(sql, con)) {
					cmd.Parameters.AddWithValue("@nombre", nombre);
					cmd.Parameters.AddWithValue("@descripcion", descripcion);
					cmd.Parameters.AddWithValue("@precio", precio);
					cmd.Parameters.AddWithValue("@id", selectedId);
					if(editingProductos)
						cmd.Parameters.AddWithValue("@stock", stock);

					con.Open();
					int affected = cmd.ExecuteNonQuery();
					con.Close();

					if(affected > 0) {
						MessageBox.Show("Registro actualizado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
						ClearForm();
						LoadData();
					} else {
						MessageBox.Show("No se encontró el registro para actualizar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al actualizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnEliminar_Click(object sender, EventArgs e) {
			if(selectedId < 0) {
				MessageBox.Show("Selecciona un registro para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var confirm = MessageBox.Show("¿Eliminar el registro seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if(confirm != DialogResult.Yes)
				return;

			string sql = editingProductos ? "DELETE FROM repuestos WHERE id=@id" : "DELETE FROM servicios WHERE id=@id";

			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
				using(var cmd = new MySqlCommand(sql, con)) {
					cmd.Parameters.AddWithValue("@id", selectedId);
					con.Open();
					int affected = cmd.ExecuteNonQuery();
					con.Close();

					if(affected > 0) {
						MessageBox.Show("Registro eliminado.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
						ClearForm();
						LoadData();
					} else {
						MessageBox.Show("No se encontró el registro para eliminar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ClearForm() {
			selectedId = -1;
			txtNombre.Clear();
			txtDescripcion.Clear();
			numPrecio.Value = 0;
			numStock.Value = 0;
			DGVItems.ClearSelection();
		}

		private void DGVItems_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void txtNombre_TextChanged(object sender, EventArgs e) {

		}

		private void txtDescripcion_TextChanged(object sender, EventArgs e) {

		}

		private void numPrecio_ValueChanged(object sender, EventArgs e) {

		}

		private void numStock_ValueChanged(object sender, EventArgs e) {

		}
	}
}
