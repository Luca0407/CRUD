using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Simple_Login_FORM {
	public partial class empleados: Form {
		public empleados() {
			InitializeComponent();
			this.TopLevel = false;
			this.FormBorderStyle = FormBorderStyle.None;
			this.AutoScroll = false; // O true si quieres scroll cuando el contenido es grande
			this.Dock = DockStyle.Fill;
			ListarNombres();
		}

		private void ListarNombres() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string nombres = "SELECT DISTINCT CONCAT_WS(' ', nombre, apellido) as NombreCompleto FROM personas WHERE tipo = 'e'";
					MySqlCommand cmd = new MySqlCommand(nombres, con);
					MySqlDataReader reader = cmd.ExecuteReader();

					AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

					while(reader.Read()) {
						coleccion.Add(reader.GetString("NombreCompleto"));
					}

					fullName.AutoCompleteMode = AutoCompleteMode.Suggest;
					fullName.AutoCompleteSource = AutoCompleteSource.CustomSource;
					fullName.AutoCompleteCustomSource = coleccion; // asignás solo una vez
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar nombres: " + ex.Message);
			}
		}

		private void clientes_Load(object sender, EventArgs e) {
			ListarEmpleado();
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.MultiSelect = true;
		}

		public void ListarEmpleado() {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				string sql = @"SELECT p.ID_persona, p.nombre, p.apellido, r.nombre_rol as rol, p.mail, p.telefono, p.domicilio, e.contraseña, p.tipo
							FROM empleados e
							INNER JOIN personas p ON e.ID_persona = p.ID_persona
							INNER JOIN roles r ON e.rol = r.ID_roles
							WHERE p.tipo = 'e'";
				MySqlCommand cmd = new MySqlCommand(sql, con);
				MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				dataGridView1.DataSource = dt;
				dataGridView1.Columns["ID_persona"].Visible = false;
				dataGridView1.Columns["tipo"].Visible = false;
			}
		}

		public void EliminarCliente() {
			if(dataGridView1.SelectedRows.Count == 0) {
				MessageBox.Show("Selecciona al menos una fila para eliminar.");
				return;
			}

			DialogResult result = MessageBox.Show(
				"¿Seguro que deseas eliminar los registros seleccionados?",
				"Confirmar eliminación",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning
			);

			if(result != DialogResult.Yes)
				return;

			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();

					foreach(DataGridViewRow fila in dataGridView1.SelectedRows) {
						if(fila.IsNewRow)
							continue;

						string id = fila.Cells["ID_persona"].Value?.ToString();
						if(string.IsNullOrEmpty(id))
							continue;

						// 1️⃣ Buscar ID_persona
						int idPersona = 0;
						string sqlGetId = "SELECT ID_persona FROM empleados WHERE ID_persona = @id";
						using(MySqlCommand cmdGet = new MySqlCommand(sqlGetId, con)) {
							cmdGet.Parameters.AddWithValue("@id", id);
							object resultId = cmdGet.ExecuteScalar();
							if(resultId == null)
								continue;
							idPersona = Convert.ToInt32(resultId);
						}

						// 2️⃣ Eliminar primero de empleados
						string sqlDeleteCliente = "DELETE FROM empleados WHERE ID_persona = @id";
						using(MySqlCommand cmdDelCliente = new MySqlCommand(sqlDeleteCliente, con)) {
							cmdDelCliente.Parameters.AddWithValue("@id", idPersona);
							cmdDelCliente.ExecuteNonQuery();
						}

						// 3️⃣ Luego eliminar de personas (si no está bloqueada por FK)
						try {
							string sqlDeletePersona = "DELETE FROM personas WHERE ID_persona = @idPersona";
							using(MySqlCommand cmdDelPersona = new MySqlCommand(sqlDeletePersona, con)) {
								cmdDelPersona.Parameters.AddWithValue("@idPersona", idPersona);
								cmdDelPersona.ExecuteNonQuery();
							}
						} catch(MySqlException ex) {
							if(ex.Number == 1451) // Error de restricción de clave foránea
							{
								MessageBox.Show($"No se pudo eliminar la persona con id {id} porque está vinculada a otra tabla.");
							} else {
								throw;
							}
						}

						// 4️⃣ Quitar la fila visualmente
						dataGridView1.Rows.Remove(fila);
					}

					MessageBox.Show("Eliminación completada.");
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al eliminar: " + ex.Message);
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			EliminarCliente();
		}

		private void button1_Click(object sender, EventArgs e) {
			using(RegisterForm pf = new RegisterForm()) {
				if(pf.ShowDialog() == DialogResult.OK) {
					ListarEmpleado();


				} else {
					MessageBox.Show("Inserción cancelada", "Cancelada");
				}
			}
		}

		public DataTable FiltrarClientes(string nombre) {
			DataTable dt = new DataTable();
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();

					string sql = @"SELECT p.ID_persona, p.nombre, p.apellido, r.nombre_rol as rol, p.mail, p.telefono, p.domicilio, e.contraseña, p.tipo
							FROM empleados e
							INNER JOIN personas p ON e.ID_persona = p.ID_persona
							INNER JOIN roles r ON e.rol = r.ID_roles
							WHERE p.tipo = 'e'
							AND (@nombre IS NULL OR p.nombre LIKE @nombre OR p.apellido LIKE @nombre);";

					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@nombre",
							string.IsNullOrWhiteSpace(nombre) ? (object) DBNull.Value : $"%{nombre}%");

						MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
						adapter.Fill(dt);
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al buscar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return dt;
		}

		private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e) {
			if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
				e.Handled = true;
			}
		}

		private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e) {
			if(!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ') {
				e.Handled = true;
			}
		}

		private void button3_Click(object sender, EventArgs e) {
			if(dataGridView1.SelectedRows.Count != 1) {
				MessageBox.Show("Por favor, selecciona solo una fila para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			var cellValue = dataGridView1.SelectedRows[0].Cells[0].Value;
			if(cellValue == null || !int.TryParse(cellValue.ToString(), out int idNum)) {
				MessageBox.Show("El ID de la fila seleccionada no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			using(ModPersona pf = new ModPersona(idNum, 2)) {
				if(pf.ShowDialog() == DialogResult.OK) {
					ListarEmpleado();
				} else {
					MessageBox.Show("Modificación cancelada", "Cancelada");
				}
			}
		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

		}

		private void label3_Click(object sender, EventArgs e) {

		}

		private void label5_Click(object sender, EventArgs e) {

		}

		private void button4_Click(object sender, EventArgs e) {
			string nombre_completo = fullName.Text;
			DataTable resultados = FiltrarClientes(nombre_completo);

			dataGridView1.DataSource = null;
			dataGridView1.Rows.Clear();
			dataGridView1.Columns.Clear();

			dataGridView1.DataSource = resultados;

			if(dataGridView1.Columns.Contains("tipo"))
				dataGridView1.Columns["ID_persona"].Visible = false;
				dataGridView1.Columns["tipo"].Visible = false;
		}

		private void fullName_TextChanged(object sender, EventArgs e) {

		}

		private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) {

		}
	}
}
