using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Simple_Login_FORM.Services;

namespace Simple_Login_FORM {
	public partial class empleados: Form {
		private readonly EmpleadoService _service;
		public empleados() : this(new EmpleadoService(new DefaultConnectionFactory(DBConfig.GetConnectionString()))) { }

		// constructor para inyección (tests)
		public empleados(EmpleadoService service) {
			InitializeComponent();
			this.TopLevel = false;
			this.FormBorderStyle = FormBorderStyle.None;
			this.AutoScroll = false;
			this.Dock = DockStyle.Fill;
			_service = service;
			ListarNombres();
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

		private void ListarNombres() {
			try {
				var dt = _service.ListarEmpleado();
				AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
				foreach(DataRow r in dt.Rows)
					coleccion.Add(string.Format("{0} {1}", r["nombre"], r["apellido"]));

				fullName.AutoCompleteMode = AutoCompleteMode.Suggest;
				fullName.AutoCompleteSource = AutoCompleteSource.CustomSource;
				fullName.AutoCompleteCustomSource = coleccion;
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
			try {
				var dt = _service.ListarEmpleado();
				dataGridView1.DataSource = dt;
				if(dataGridView1.Columns.Contains("ID_persona"))
					dataGridView1.Columns["ID_persona"].Visible = false;
				if(dataGridView1.Columns.Contains("tipo"))
					dataGridView1.Columns["tipo"].Visible = false;
			} catch(Exception ex) {
				MessageBox.Show("Error al listar: " + ex.Message);
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
				MessageBoxIcon.Warning);

			if(result != DialogResult.Yes)
				return;

			try {
				var ids = new System.Collections.Generic.List<int>();
				foreach(DataGridViewRow fila in dataGridView1.SelectedRows) {
					if(fila.IsNewRow)
						continue;
					var idobj = fila.Cells["ID_persona"].Value;
					if(idobj == null)
						continue;
					if(int.TryParse(idobj.ToString(), out int id))
						ids.Add(id);
				}

				_service.EliminarPersonas(ids.ToArray());
				MessageBox.Show("Eliminación completada.");
				ListarEmpleado();
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
					MessageBox.Show("Inserción cancelada.", "Cancelada");
				}
			}
		}

		public DataTable FiltrarClientes(string nombre) {
			return _service.FiltrarClientes(nombre);
		}

		private void button4_Click(object sender, EventArgs e) {
			string nombre_completo = fullName.Text;
			DataTable resultados = FiltrarClientes(nombre_completo);

			dataGridView1.DataSource = null;
			dataGridView1.Rows.Clear();
			dataGridView1.Columns.Clear();

			dataGridView1.DataSource = resultados;

			if(dataGridView1.Columns.Contains("ID_persona"))
				dataGridView1.Columns["ID_persona"].Visible = false;
			if(dataGridView1.Columns.Contains("tipo"))
				dataGridView1.Columns["tipo"].Visible = false;
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

			using(ModPersona pf = new ModPersona(idNum, 2, new EmpleadoService(new DefaultConnectionFactory(DBConfig.GetConnectionString())))) {
				if(pf.ShowDialog() == DialogResult.OK) {
					ListarEmpleado();
				} else {
					MessageBox.Show("Modificación cancelada", "Cancelada");
				}
			}
		}
	}
}
