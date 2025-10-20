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
	public partial class ModPersona: Form {
		private string idperson;
		public ModPersona(DataGridViewCell id, int ncase) {
			InitializeComponent();
			idperson = id.Value.ToString();
			PersonaSeleccionada(int.Parse(idperson), ncase);
			if(ncase == 1) { // 1 = clientes
				surnameBox.MaxLength = 20;
				dniBox.MaxLength = 10;
				surnameBox.KeyPress += Box_KeyPress;
				roleBox.Visible = false;
			} else if(ncase == 2) { // 2 = empleados
				surnameBox.MaxLength = 20;
				surnameBox.KeyPress += Box_KeyPress;
				dniBox.Visible = false;
				label6.Text = "rol";
			} else if(ncase == 3) { // 3 = proveedores
				surnameBox.Visible = false;
				dniBox.Visible = false;
				roleBox.Visible = false;
			}

			nameBox.MaxLength = 25;
			mailBox.MaxLength = 45;
			phoneBox.MaxLength = 10;
			domBox.MaxLength = 45;
			nameBox.KeyPress += Box_KeyPress;
			this.KeyDown += new KeyEventHandler(add_KeyDown);
			this.KeyPreview = true;
		}
		private void add_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				button1.Focus();
				button1_Click(sender, e);
			}
		}


		private bool EsCorreoValido(string correo) {
			string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
			return Regex.IsMatch(correo, patron);
		}

		private void PersonaSeleccionada(int id, int ncase) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				string sql;
				if(ncase == 1) {
					sql = @"SELECT p.nombre, p.apellido, c.DNI, p.mail, p.telefono, p.domicilio
						FROM clientes c 
						INNER JOIN personas p ON c.ID_persona = p.ID_persona
						WHERE p.ID_persona = @id
						LIMIT 1;";
					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@id", id);

						using(MySqlDataReader reader = cmd.ExecuteReader()) {
							if(reader.Read()) {
								// Asignar los valores a los TextBox
								nameBox.Text = reader["nombre"].ToString();
								surnameBox.Text = reader["apellido"].ToString();
								dniBox.Text = reader["DNI"].ToString();
								mailBox.Text = reader["mail"].ToString();
								phoneBox.Text = reader["telefono"].ToString();
								domBox.Text = reader["domicilio"].ToString();
							} else {
								MessageBox.Show("No se encontró la persona con ese ID.",
									"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
				} else if(ncase == 2) {
					sql = @"SELECT p.nombre, p.apellido, r.nombre_rol as rol, p.mail, p.telefono, p.domicilio, e.contraseña
						FROM empleados e 
						INNER JOIN personas p ON e.ID_persona = p.ID_persona
						INNER JOIN roles r ON e.rol = r.ID_roles
						WHERE p.ID_persona = @id
						LIMIT 1;";
					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@id", id);

						using(MySqlDataReader reader = cmd.ExecuteReader()) {
							if(reader.Read()) {
								// Asignar los valores a los TextBox
								nameBox.Text = reader["nombre"].ToString();
								surnameBox.Text = reader["apellido"].ToString();
								roleBox.Text = reader["rol"].ToString();
								mailBox.Text = reader["mail"].ToString();
								phoneBox.Text = reader["telefono"].ToString();
								domBox.Text = reader["domicilio"].ToString();
							} else {
								MessageBox.Show("No se encontró la persona con ese ID.",
									"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
				} else if(ncase == 3) {
					sql = @"SELECT p.nombre, c.pagina, p.mail, p.telefono, p.domicilio
						FROM proveedores c 
						INNER JOIN personas p ON c.ID_persona = p.ID_persona
						WHERE p.ID_persona = @id
						LIMIT 1;";
					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@id", id);

						using(MySqlDataReader reader = cmd.ExecuteReader()) {
							if(reader.Read()) {
								// Asignar los valores a los TextBox
								nameBox.Text = reader["nombre"].ToString();
								dniBox.Text = reader["pagina"].ToString();
								mailBox.Text = reader["mail"].ToString();
								phoneBox.Text = reader["telefono"].ToString();
								domBox.Text = reader["domicilio"].ToString();
							} else {
								MessageBox.Show("No se encontró la persona con ese ID.",
									"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
					}
				}
			}
		}

		public bool ActualizarEmpleado(Empleados datos, int id) {
			try {
				// Validaciones
				if(string.IsNullOrWhiteSpace(datos.Nombre) ||
					string.IsNullOrWhiteSpace(datos.Apellido) ||
					string.IsNullOrWhiteSpace(datos.Mail) ||
					string.IsNullOrWhiteSpace(datos.Telefono) ||
					string.IsNullOrWhiteSpace(datos.Domicilio) ||
					string.IsNullOrWhiteSpace(datos.Rol)) {
					MessageBox.Show("Faltan datos para realizar el registro", "Advertencia");
					return false;
				}

				if(!EsCorreoValido(datos.Mail)) {
					MessageBox.Show("El correo electrónico no es válido", "Error");
					return false;
				}

				if(datos.Telefono.Length < 10) {
					MessageBox.Show("El número de teléfono no es válido", "Error");
					return false;
				}

				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string sql = @"UPDATE personas p
                           JOIN empleados e ON p.ID_persona = e.ID_persona
                           SET p.Nombre = @Nombre,
                               p.Apellido = @Apellido,
                               p.Mail = @Mail,
                               p.Telefono = @Telefono,
                               p.Domicilio = @Domicilio,
                               e.Rol = @Rol
                           WHERE p.ID_persona = @Id;";

					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@Nombre", datos.Nombre);
						cmd.Parameters.AddWithValue("@Apellido", datos.Apellido);
						cmd.Parameters.AddWithValue("@Mail", datos.Mail);
						cmd.Parameters.AddWithValue("@Telefono", datos.Telefono);
						cmd.Parameters.AddWithValue("@Domicilio", datos.Domicilio);
						cmd.Parameters.AddWithValue("@Rol", datos.Rol);
						cmd.Parameters.AddWithValue("@Id", id);

						cmd.ExecuteNonQuery();
					}
				}

				return true;
			} catch(Exception ex) {
				MessageBox.Show("Error: " + ex.Message);
				return false;
			}
		}

		public bool ActualizarCliente(Clientes datos, int id) {
			try {
				// Validaciones
				if(string.IsNullOrWhiteSpace(datos.Nombre) ||
					string.IsNullOrWhiteSpace(datos.Apellido) ||
					string.IsNullOrWhiteSpace(datos.Mail) ||
					string.IsNullOrWhiteSpace(datos.Telefono) ||
					string.IsNullOrWhiteSpace(datos.Domicilio) ||
					string.IsNullOrWhiteSpace(datos.Dni)) {
					MessageBox.Show("Faltan datos para realizar el registro", "Advertencia");
					return false;
				}

				if(!EsCorreoValido(datos.Mail)) {
					MessageBox.Show("El correo electrónico no es válido", "Error");
					return false;
				}

				if(datos.Dni.Length < 10) {
					MessageBox.Show("El número de DNI no es válido", "Error");
					return false;
				}

				if(datos.Telefono.Length < 10) {
					MessageBox.Show("El número de teléfono no es válido", "Error");
					return false;
				}

				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string sql = @"UPDATE personas p
                           JOIN clientes c ON p.ID_persona = c.ID_persona
                           SET p.Nombre = @Nombre,
                               p.Apellido = @Apellido,
                               p.Mail = @Mail,
                               p.Telefono = @Telefono,
                               p.Domicilio = @Domicilio,
                               c.Dni = @Dni
                           WHERE p.ID_persona = @Id;";

					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@Nombre", datos.Nombre);
						cmd.Parameters.AddWithValue("@Apellido", datos.Apellido);
						cmd.Parameters.AddWithValue("@Mail", datos.Mail);
						cmd.Parameters.AddWithValue("@Telefono", datos.Telefono);
						cmd.Parameters.AddWithValue("@Domicilio", datos.Domicilio);
						cmd.Parameters.AddWithValue("@Dni", datos.Dni);
						cmd.Parameters.AddWithValue("@Id", id);

						cmd.ExecuteNonQuery();
					}
				}

				return true;
			} catch(Exception ex) {
				MessageBox.Show("Error: " + ex.Message);
				return false;
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			while(true) {
				Clientes cliente = new Clientes() {
					Nombre = nameBox.Text,
					Apellido = surnameBox.Text,
					Mail = mailBox.Text,
					Telefono = phoneBox.Text,
					Domicilio = domBox.Text,
					Dni = dniBox.Text
				};

				// ID del cliente que queremos actualizar (deberías tenerlo guardado previamente)
				int idCliente = Convert.ToInt32(idperson); // por ejemplo, si tenés un TextBox con el ID

				// Intentamos actualizar
				if(ActualizarCliente(cliente, idCliente)) {
					MessageBox.Show("Datos actualizados con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.DialogResult = DialogResult.OK;
					this.Close();
					break;
				} else {
					return; 
				}
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void Box_KeyPress(object sender, KeyPressEventArgs e) {
			if(char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)) {
				e.Handled = false; // ✅ permitido
			} else {
				e.Handled = true;  // ❌ bloqueado
				MessageBox.Show("Caracter no permitido en el campo", "Advertencia");
			}
		}

		private void ModClient_Load(object sender, EventArgs e) {

		}
	}
}
