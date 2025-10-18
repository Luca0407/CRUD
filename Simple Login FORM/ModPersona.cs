using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Login_FORM {
	public partial class ModPersona: Form {
		private string id_cliente;
		public ModPersona(DataGridViewCell id) {
			InitializeComponent();
			id_cliente = id.Value.ToString();
			ClienteSeleccionado(int.Parse(id_cliente));
			nameBox.MaxLength = 20;
			surnameBox.MaxLength = 20;
			mailBox.MaxLength = 45;
			phoneBox.MaxLength = 10;
			domBox.MaxLength = 45;
			dniBox.MaxLength = 10;
			nameBox.KeyPress += Box_KeyPress;
			surnameBox.KeyPress += Box_KeyPress;
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

		private void ClienteSeleccionado(int id) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				string sql = @"SELECT p.nombre, p.apellido, c.DNI, p.mail, p.telefono, p.domicilio
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
							MessageBox.Show("No se encontró el cliente con ese ID.",
								"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				}
			}
		}

		public bool ActualizarCliente(string nombre, string apellido, string mail, string telefono, string domicilio, string dni) {
			System.Windows.Forms.TextBox[] datos = { nameBox, surnameBox, phoneBox, mailBox, domBox, dniBox };
			try {
				if(datos.Any(tb => string.IsNullOrWhiteSpace(tb.Text))) {
					MessageBox.Show("Faltan datos para realizar el registro", "Advertencia");
					return false;
				}

				if(!EsCorreoValido(mailBox.Text)) {
					MessageBox.Show("El correo electrónico no es válido", "Error");
					return false;
				}

				if(dniBox.Text.Length < 10) {
					MessageBox.Show("El número de DNI no es válido", "Error");
					return false;
				}

				if(phoneBox.Text.Length < 10) {
					MessageBox.Show("El número de teléfono no es válido", "Error");
					return false;
				}
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();

				string sql = @"UPDATE personas p JOIN clientes c ON p.ID_persona = c.ID_persona SET 
							p.nombre = @nombre,
							p.apellido = @apellido,
							p.mail = @mail,
							p.telefono = @telefono,
							p.domicilio = @domicilio,
							c.DNI = @dni
						WHERE p.ID_persona = @id;";
				MySqlCommand cmd = new MySqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@nombre", nombre);
				cmd.Parameters.AddWithValue("@apellido", apellido);
				cmd.Parameters.AddWithValue("@mail", mail);
				cmd.Parameters.AddWithValue("@telefono", telefono);
				cmd.Parameters.AddWithValue("@domicilio", domicilio);
				cmd.Parameters.AddWithValue("@dni", dni);
				cmd.Parameters.AddWithValue("@id", id_cliente);
				cmd.ExecuteNonQuery();
					return true;
				}
			} catch(Exception ex) {
				MessageBox.Show("Error: " + ex.Message);
				return false;
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			while(true) {
				if(ActualizarCliente(nameBox.Text, surnameBox.Text, mailBox.Text, phoneBox.Text, domBox.Text, dniBox.Text)) {
					MessageBox.Show("Datos actualizados con exito!");
					this.DialogResult = DialogResult.OK;
					break;
				} else { 
					return; 
				}
			}
			this.Close();
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
