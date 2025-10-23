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
		private int idperson;
		private int ncase;
		public ModPersona(int idperson, int ncase) {
			InitializeComponent();
			
			this.idperson = idperson;
			this.ncase = ncase;
			PersonaSeleccionada(idperson, ncase);
			if(ncase == 1) { // 1 = clientes
				roleBox.Visible = false;
				dniBox.MaxLength = 8;
				surnameBox.MaxLength = 20;
				surnameBox.KeyPress += Box_KeyPress;
				dniBox.KeyPress += NumBox_KeyPress;
				cuiBox.MaxLength = 11;
				cuiBox.KeyPress += NumBox_KeyPress;
			} else if(ncase == 2) { // 2 = empleados
				surnameBox.MaxLength = 20;
				surnameBox.KeyPress += Box_KeyPress;
				dniBox.Visible = false;
				label6.Text = "Rol";
				cuiBox.Visible = false;
				label7.ResetText();
			} else if(ncase == 3) { // 3 = proveedores
				roleBox.Visible = false;
				label7.ResetText();
				label2.Text = "Pagina";  //surnameBox
				label6.Text = "Cuit";  //dniBox
				cuiBox.Visible= false;
				dniBox.MaxLength = 11;
				dniBox.KeyPress += NumBox_KeyPress;
			}
			nameBox.MaxLength = 25;
			mailBox.MaxLength = 45;
			phoneBox.MaxLength = 10;
			domBox.MaxLength = 45;
			phoneBox.KeyPress += NumBox_KeyPress;
			nameBox.KeyPress += Box_KeyPress;
			this.KeyDown += new KeyEventHandler(add_KeyDown);
			this.KeyPreview = true;
		}
		private void ModPersona_Load(object sender, EventArgs e) {
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
					sql = @"SELECT p.nombre, p.apellido, c.DNI, p.mail, p.telefono, p.domicilio, c.cuil
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
								cuiBox.Text = reader["cuil"].ToString();
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
					sql = @"SELECT p.nombre, c.pagina, p.mail, p.telefono, p.domicilio, c.cuit
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
								surnameBox.Text = reader["pagina"].ToString();
								mailBox.Text = reader["mail"].ToString();
								phoneBox.Text = reader["telefono"].ToString();
								domBox.Text = reader["domicilio"].ToString();
								dniBox.Text = reader["cuit"].ToString();
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
			int rolid = 3;
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

				if(datos.Telefono.Length != 10) {
					MessageBox.Show("El número de teléfono no es válido", "Error");
					return false;
				}

				if(datos.Rol == "administrador") {
					rolid = 1;
				} else if(datos.Rol == "técnico") {
					rolid = 2;
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
						cmd.Parameters.AddWithValue("@Rol", rolid);
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
					string.IsNullOrWhiteSpace(datos.Dni) ||
					string.IsNullOrWhiteSpace(datos.Cuil)) {
					MessageBox.Show("Faltan datos para realizar el registro", "Advertencia");
					return false;
				}

				if(!EsCorreoValido(datos.Mail)) {
					MessageBox.Show("El correo electrónico no es válido", "Error");
					return false;
				}

				if(datos.Dni.Length != 8) {
					MessageBox.Show("El número de DNI no es válido", "Error");
					return false;
				}

				if(datos.Telefono.Length != 10) {
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
								c.Dni = @Dni,
								c.cuil = @Cuil
							WHERE p.ID_persona = @Id;";

					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@Nombre", datos.Nombre);
						cmd.Parameters.AddWithValue("@Apellido", datos.Apellido);
						cmd.Parameters.AddWithValue("@Mail", datos.Mail);
						cmd.Parameters.AddWithValue("@Telefono", datos.Telefono);
						cmd.Parameters.AddWithValue("@Domicilio", datos.Domicilio);
						cmd.Parameters.AddWithValue("@Dni", datos.Dni);
						cmd.Parameters.AddWithValue("@Cuil", datos.Cuil);
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

		public bool ActualizarProveedor(Proveedores datos, int id) {
			try {
				// Validaciones
				if(string.IsNullOrWhiteSpace(datos.Nombre) ||
					string.IsNullOrWhiteSpace(datos.Mail) ||
					string.IsNullOrWhiteSpace(datos.Telefono) ||
					string.IsNullOrWhiteSpace(datos.Domicilio) ||
					string.IsNullOrWhiteSpace(datos.Pagina) ||
					string.IsNullOrWhiteSpace(datos.Cuit)) {
					MessageBox.Show("Faltan datos para realizar el registro", "Advertencia");
					return false;
				}

				if(!EsCorreoValido(datos.Mail)) {
					MessageBox.Show("El correo electrónico no es válido", "Error");
					return false;
				}

				if(datos.Telefono.Length != 10) {
					MessageBox.Show("El número de teléfono no es válido", "Error");
					return false;
				}

				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					string sql = @"UPDATE personas p
							JOIN proveedores e ON p.ID_persona = e.ID_persona
							SET p.Nombre = @Nombre,
								p.Mail = @Mail,
								p.Telefono = @Telefono,
								p.Domicilio = @Domicilio,
								e.pagina = @Pagina,
								e.cuit = @Cuit
							WHERE p.ID_persona = @Id;";

					using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
						cmd.Parameters.AddWithValue("@Nombre", datos.Nombre);
						cmd.Parameters.AddWithValue("@Mail", datos.Mail);
						cmd.Parameters.AddWithValue("@Telefono", datos.Telefono);
						cmd.Parameters.AddWithValue("@Domicilio", datos.Domicilio);
						cmd.Parameters.AddWithValue("@Pagina", datos.Pagina);
						cmd.Parameters.AddWithValue("@Cuit", datos.Cuit);
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
			if(this.ncase == 1) {
				Clientes cliente = new Clientes() {
					Nombre = nameBox.Text,
					Apellido = surnameBox.Text,
					Mail = mailBox.Text,
					Telefono = phoneBox.Text,
					Domicilio = domBox.Text,
					Dni = dniBox.Text,
					Cuil = cuiBox.Text
				};

				if(ActualizarCliente(cliente, this.idperson)) {
					MessageBox.Show("Datos actualizados con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.DialogResult = DialogResult.OK;
					this.Close();
				}

			} else if(this.ncase == 2) {
				Empleados empleado = new Empleados() {
					Nombre = nameBox.Text,
					Apellido = surnameBox.Text,
					Mail = mailBox.Text,
					Telefono = phoneBox.Text,
					Domicilio = domBox.Text,
					Rol = roleBox.Text
				};

				if(ActualizarEmpleado(empleado, this.idperson)) {
					MessageBox.Show("Datos actualizados con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
			} else if(this.ncase == 3) {
				Proveedores proveedor = new Proveedores() {
					Nombre = nameBox.Text,
					Mail = mailBox.Text,
					Telefono = phoneBox.Text,
					Domicilio = domBox.Text,
					Pagina = surnameBox.Text,
					Cuit = dniBox.Text
				};

				if(ActualizarProveedor(proveedor, this.idperson)) {
					MessageBox.Show("Datos actualizados con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
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
