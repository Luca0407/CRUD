using MySql.Data.MySqlClient;
using Mysqlx.Notice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Login_FORM
{
	public partial class clientes : Form
	{
		public clientes()
		{
			InitializeComponent();
			this.TopLevel = false;
			this.FormBorderStyle = FormBorderStyle.None;
			this.AutoScroll = false; // O true si quieres scroll cuando el contenido es grande
			this.Dock = DockStyle.Fill;
			dataGridView1.KeyDown += dataGridView1_KeyDown;
			ListarNombres();
		}

		private void ListarNombres() {
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string nombres = "SELECT DISTINCT CONCAT_WS(' ', nombre, apellido) as NombreCompleto FROM personas WHERE tipo = 'c'";
					MySqlCommand cmd = new MySqlCommand(nombres, con);
					MySqlDataReader reader = cmd.ExecuteReader();

					AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

					while(reader.Read()) {
						coleccion.Add(reader.GetString("NombreCompleto"));
					}

					fullName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
					fullName.AutoCompleteSource = AutoCompleteSource.CustomSource;
					fullName.AutoCompleteCustomSource = coleccion; // asignás solo una vez
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar nombres: " + ex.Message);
			}
		}

		private void clientes_Load(object sender, EventArgs e)
		{
			ListarClientes();
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.MultiSelect = true;
		}

		public void ListarClientes()
		{
			using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
			{
				con.Open();
				string sql = @"SELECT p.ID_persona, p.nombre, p.apellido, c.DNI, p.mail, p.telefono, p.domicilio
							FROM clientes c 
							INNER JOIN personas p ON c.ID_persona = p.ID_persona
							WHERE p.tipo = 'c'";
				MySqlCommand cmd = new MySqlCommand(sql, con);
				MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				dataGridView1.DataSource = dt;
				dataGridView1.Columns["ID_persona"].Visible = false;
			}
		}

		public void CrearCliente(string nombre, string apellido, string mail, string telefono, string domicilio, string dni) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();

				// Insertar en personas
				string sqlPersona = "INSERT INTO personas (nombre, apellido, mail, telefono, domicilio, tipo) VALUES (@nombre, @apellido, @mail, @telefono, @domicilio, 'c')";
				MySqlCommand cmd = new MySqlCommand(sqlPersona, con);
				cmd.Parameters.AddWithValue("@nombre", nombre);
				cmd.Parameters.AddWithValue("@apellido", apellido);
				cmd.Parameters.AddWithValue("@mail", mail);
				cmd.Parameters.AddWithValue("@telefono", telefono);
				cmd.Parameters.AddWithValue("@domicilio", domicilio);
				cmd.ExecuteNonQuery();

				int idPersona = (int) cmd.LastInsertedId;

				// Insertar en clientes
				string sqlCliente = "INSERT INTO clientes (DNI, ID_persona) VALUES (@dni, @idPersona)";
				MySqlCommand cmd2 = new MySqlCommand(sqlCliente, con);
				cmd2.Parameters.AddWithValue("@dni", dni);
				cmd2.Parameters.AddWithValue("@idPersona", idPersona);
				cmd2.ExecuteNonQuery();
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

						string dni = fila.Cells["DNI"].Value?.ToString();
						if(string.IsNullOrEmpty(dni))
							continue;

						// 1️⃣ Buscar ID_persona
						int idPersona = 0;
						string sqlGetId = "SELECT ID_persona FROM clientes WHERE DNI = @dni";
						using(MySqlCommand cmdGet = new MySqlCommand(sqlGetId, con)) {
							cmdGet.Parameters.AddWithValue("@dni", dni);
							object resultId = cmdGet.ExecuteScalar();
							if(resultId == null)
								continue;
							idPersona = Convert.ToInt32(resultId);
						}

						// 2️⃣ Eliminar primero de clientes
						string sqlDeleteCliente = "DELETE FROM clientes WHERE DNI = @dni";
						using(MySqlCommand cmdDelCliente = new MySqlCommand(sqlDeleteCliente, con)) {
							cmdDelCliente.Parameters.AddWithValue("@dni", dni);
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
								MessageBox.Show($"No se pudo eliminar la persona con DNI {dni} porque está vinculada a otra tabla.");
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

		private bool adding = false;
		private void button1_Click(object sender, EventArgs e) {
			if(adding) {
				MessageBox.Show("Complete la inserción del cliente actual primero.", "Accion denegada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			adding = true;
			DataTable dt = (DataTable) dataGridView1.DataSource;
			DataRow nuevaFila = dt.NewRow();
			dt.Rows.Add(nuevaFila);

			// Seleccionar la nueva fila
			int rowIndex = dataGridView1.Rows.Count - 1;

			// Buscar la primera celda visible
			DataGridViewRow filaNueva = dataGridView1.Rows[rowIndex];
			DataGridViewCell celdaVisible = null;

			foreach(DataGridViewCell celda in filaNueva.Cells) {
				if(celda.Visible && !celda.ReadOnly) {
					celdaVisible = celda;
					break;
				}
			}

			if(celdaVisible != null) {
				dataGridView1.CurrentCell = celdaVisible;
				dataGridView1.BeginEdit(true);

				// ✅ Forzar foco en el control interno de edición (para poder escribir de inmediato)
				if(dataGridView1.EditingControl != null) {
					dataGridView1.EditingControl.Focus();
				}
			}

			// Bloquear las filas anteriores
			for(int i = 0; i < rowIndex; i++) {
				dataGridView1.Rows[i].ReadOnly = true;
			}
		}

		public DataTable FiltrarClientes(string nombre) {
			DataTable dt = new DataTable();
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string sql = @"SELECT * FROM
									(SELECT p.ID_persona, CONCAT(p.nombre, ' ', p.apellido) AS NombreCompleto, c.DNI, p.mail, p.telefono, p.domicilio, p.tipo
								    FROM clientes c
								    INNER JOIN personas p ON c.ID_persona = p.ID_persona) AS sub
								WHERE (@NombreCompleto IS NULL OR sub.NombreCompleto LIKE @NombreCompleto) AND sub.tipo = 'c';";
					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@NombreCompleto",
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

		private void dataGridView1_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				e.SuppressKeyPress = true; // evita que se mueva a la siguiente celda

				// ✅ Forzar guardar el texto editado antes de leer valores
				dataGridView1.EndEdit();

				if(dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
					return;

				try {
					string nombre = dataGridView1.CurrentRow.Cells["nombre"].Value?.ToString();
					string apellido = dataGridView1.CurrentRow.Cells["apellido"].Value?.ToString();
					string dni = dataGridView1.CurrentRow.Cells["DNI"].Value?.ToString();
					string mail = dataGridView1.CurrentRow.Cells["mail"].Value?.ToString();
					string telefono = dataGridView1.CurrentRow.Cells["telefono"].Value?.ToString();
					string domicilio = dataGridView1.CurrentRow.Cells["domicilio"].Value?.ToString();

					// ✅ Validar que no haya campos vacíos
					if(string.IsNullOrWhiteSpace(nombre) ||
						string.IsNullOrWhiteSpace(apellido) ||
						string.IsNullOrWhiteSpace(dni)) {
						MessageBox.Show("Completa todos los campos obligatorios antes de guardar.");
						return;
					}

					CrearCliente(nombre, apellido, mail, telefono, domicilio, dni);

					MessageBox.Show("Cliente guardado correctamente.");

					// ✅ Bloquear la fila recién creada después de insertar
					dataGridView1.CurrentRow.ReadOnly = true;
				} catch(Exception ex) {
					MessageBox.Show(ex.ToString(), "Error");
				}
			}
		}

		private void button3_Click(object sender, EventArgs e) {
			if(dataGridView1.SelectedRows.Count == 0) {
				MessageBox.Show("Por favor, selecciona una fila para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Solo permitir editar una fila a la vez
			if(dataGridView1.SelectedRows.Count > 1) {
				MessageBox.Show("Selecciona solo una fila para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using(ModClient pf = new ModClient(dataGridView1.SelectedRows[0].Cells[0])) {
				if(pf.ShowDialog() == DialogResult.OK) {
					ListarClientes();
				} else {
					MessageBox.Show("Modificación cancelada", "Cancelada");
				}

			}
		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e) {

		}

		private void label5_Click(object sender, EventArgs e) {

		}

		private void textBox4_TextChanged(object sender, EventArgs e) {

		}

		private void button4_Click(object sender, EventArgs e) {
			string nombre_completo = fullName.Text;

			DataTable resultados = FiltrarClientes(nombre_completo);
			dataGridView1.DataSource = resultados;
		}
	}
}
