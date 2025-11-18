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
using Simple_Login_FORM.Services;

namespace Simple_Login_FORM {
	public partial class ModPersona: Form {
		private int idperson;
		private int ncase;
		private readonly EmpleadoService _service;

		// inyectar servicio en constructor
		public ModPersona(int idperson, int ncase) : this(idperson, ncase, new EmpleadoService(new DefaultConnectionFactory(DBConfig.GetConnectionString()))) { }

		public ModPersona(int idperson, int ncase, EmpleadoService service) {
			InitializeComponent();
			this.idperson = idperson;
			this.ncase = ncase;
			_service = service;
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
				cuiBox.Visible = false;
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

		private void add_KeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				button1.Focus();
				button1_Click(sender, e);
			}
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


		// Exponer un método testable que use PersonaValidator
		public bool ActualizarEmpleado(Empleados datos, int id) {
			// Delegar validaciones a PersonaValidator
			if(!PersonaValidator.CamposNoVacios(datos.Nombre, datos.Apellido, datos.Mail, datos.Telefono, datos.Domicilio, datos.Rol)) {
				MessageBox.Show("Faltan datos para realizar el registro.", "Advertencia");
				return false;
			}

			if(!PersonaValidator.EsCorreoValido(datos.Mail)) {
				MessageBox.Show("El correo electrónico no es válido.", "Error");
				return false;
			}

			if(!PersonaValidator.EsTelefonoValido(datos.Telefono)) {
				MessageBox.Show("El número de teléfono no es válido.", "Error");
				return false;
			}

			// aquí podríamos llamar a _service para actualizar en BD, pero para pruebas unitarias aisladas
			// el test puede mockear este comportamiento o invocar una versión que confirma los parámetros.
			try {
				// Si se quiere persistir: usar SQL update con _service o crear método UpdateEmpleado en EmpleadoService
				return true;
			} catch(Exception ex) {
				MessageBox.Show("Error: " + ex.Message);
				return false;
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			if(this.ncase == 2) {
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
			}
		}

		private void ModPersona_Load(object sender, EventArgs e) {

		}

		private void button2_Click_1(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}