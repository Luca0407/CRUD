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
        public ventas()
        {
            InitializeComponent();
			ListarProducto();
			ListarNombres();
			dataGridView1.Columns["X"].Width = 37;
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ventas_Load(object sender, EventArgs e)
        {

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
					string nombres = @"SELECT DISTINCT p.ID_productos, CONCAT_WS(' ', n.nombre_generico, b.nombre_marca, m.nombre_modelo) AS NombreCompleto
									FROM productos p 
									JOIN productos_genericos n ON n.ID_nombre_productos = p.nombre_producto
									JOIN marcas b ON b.ID_marcas = p.marca
									JOIN modelos m ON m.ID_modelos = p.modelo
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

		/*public DataTable FiltrarProductos(int id) {
			DataTable dt = new DataTable();
			try {
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();

					string sql = @"SELECT p.ID_productos, p.nombre, p.apellido, r.nombre_rol as rol, p.mail, p.telefono, p.domicilio, e.contraseña, p.tipo
							FROM empleados e
							INNER JOIN personas p ON e.ID_persona = p.ID_persona
							INNER JOIN roles r ON e.rol = r.ID_roles
							WHERE p.tipo = 'e'
							AND (@id IS NULL OR p.nombre LIKE @nombre OR p.apellido LIKE @id);";

					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@id", id);

						MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
						adapter.Fill(dt);
					}
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al buscar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return dt;
		}*/
		private void button3_Click(object sender, EventArgs e) {
			/*DataTable resultados = FiltrarProductos(int.Parse(CodProd.Text));

			dataGridView1.DataSource = null;
			dataGridView1.Rows.Clear();
			dataGridView1.Columns.Clear();

			dataGridView1.DataSource = resultados;

			if(dataGridView1.Columns.Contains("tipo"))
				dataGridView1.Columns["ID_persona"].Visible = false;
			dataGridView1.Columns["tipo"].Visible = false;*/
		}

		private void button1_Click(object sender, EventArgs e) {
			string a = "";
			for(int i = 2; i < DocNum.Text.Split().Length; i++) {
				a = a + DocNum.Text.Split()[i] + " ";
			}
			NameBox.Text = a.TrimEnd();	
		}

		private void button5_Click(object sender, EventArgs e) {
			DateBox.Text = DateTime.Today.ToString("dd/MM/yy");
		}
	}
}
