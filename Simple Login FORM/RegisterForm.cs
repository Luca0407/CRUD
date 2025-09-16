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
using static Org.BouncyCastle.Math.EC.ECCurve;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Simple_Login_FORM
{
    public partial class RegisterForm : Form
    {
		static string phone_ph = "número de 10 digitos";
		static string pass_ph = "hasta16caractere";
		static string mail_ph = "correo@mail.com";
		static string user_ph = "usuario";
		string[] placeholders = { phone_ph, pass_ph, mail_ph, user_ph};
		

		MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString());
        public RegisterForm()
        {
            InitializeComponent();
			this.MaximizeBox = false;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			PasswordBox.MaxLength = 16;
			PhoneBox.MaxLength = 10;
			UsernameBox.MaxLength = 45;
			EmailBox.MaxLength = 45;
			PasswordBox.KeyPress += PasswordBox_KeyPress;
			PhoneBox.KeyPress += PhoneBox_KeyPress;
		}

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm fm = new LoginForm();
			fm.ShowDialog();
			this.Close();
		}

		private bool EsCorreoValido(string correo) {
			string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
			return Regex.IsMatch(correo, patron);
		}

		private void CreateButton_Click(object sender, EventArgs e) {
			System.Windows.Forms.TextBox[] datos = { PhoneBox, PasswordBox, EmailBox, UsernameBox };

			try {
				// Limpia los placeholders si quedaron
				datos.Zip(placeholders, (box, ph) =>
					new {
						box,
						ph
					}).ToList().ForEach(x => {
						if(x.box.Text == x.ph)
							x.box.Clear();
					});

				// Verifica si falta algún dato
				if(datos.Any(tb => string.IsNullOrWhiteSpace(tb.Text))) {
					MessageBox.Show("Faltan datos para realizar el registro", "Advertencia");
					return;
				}

				if(!EsCorreoValido(EmailBox.Text)) {
					MessageBox.Show("El correo electrónico no es válido", "Error");
					return;
				}

				con.Open();

				// 🔹 INSERT en personas
				using(MySqlCommand cmd = con.CreateCommand()) {
					cmd.CommandText = @"INSERT INTO personas (mail, nombre, telefono) 
                                VALUES (@mail, @nombre, @telefono)";
					cmd.Parameters.AddWithValue("@mail", EmailBox.Text);
					cmd.Parameters.AddWithValue("@nombre", UsernameBox.Text);
					cmd.Parameters.AddWithValue("@telefono", PhoneBox.Text);
					cmd.ExecuteNonQuery();
				}

				// 🔹 INSERT en empleados (usando el último ID insertado)
				using(MySqlCommand cmd = con.CreateCommand()) {
					cmd.CommandText = @"INSERT INTO empleados (rol, contraseña, ID_persona) 
                                VALUES (
                                    (SELECT ID_roles FROM roles WHERE nombre_rol = @rol),
                                    @password,
                                    LAST_INSERT_ID()
                                )";
					cmd.Parameters.AddWithValue("@rol", RoleBox.Text);
					cmd.Parameters.AddWithValue("@password", PasswordBox.Text);
					cmd.ExecuteNonQuery();
				}

				MessageBox.Show("Usuario registrado con éxito!");

				this.Hide();
				LoginForm fm = new LoginForm();
				fm.ShowDialog();
			} catch(Exception ex) {
				MessageBox.Show("Error: " + ex.Message);
			} finally {
				if(con.State == ConnectionState.Open)
					con.Close();
			}
		}

		private void label3_Click(object sender, EventArgs e) {

		}

		private void label2_Click(object sender, EventArgs e) {

		}

		private void PhoneBox_Enter(object sender, EventArgs e) {
			if(PhoneBox.ForeColor != Color.Black) {
				PhoneBox.Text = "";
				PhoneBox.ForeColor = Color.Black;
			}
		}

		private void PhoneBox_Leave(object sender, EventArgs e) {
			if(string.IsNullOrWhiteSpace(PhoneBox.Text)) {
				PhoneBox.Text = phone_ph;
				PhoneBox.ForeColor = Color.DarkGray;
			}
		}

		private void PasswordBox_Enter(object sender, EventArgs e) {
			if(PasswordBox.ForeColor != Color.Black) {
				PasswordBox.Text = "";
				PasswordBox.ForeColor = Color.Black;
			}
		}

		private void PasswordBox_Leave(object sender, EventArgs e) {
			if(string.IsNullOrWhiteSpace(PasswordBox.Text)) {
				PasswordBox.Text = pass_ph;
				PasswordBox.ForeColor = Color.DarkGray;
			}
		}

		private void EmailBox_Enter(object sender, EventArgs e) {
			if(EmailBox.ForeColor != Color.Black) {
				EmailBox.Text = "";
				EmailBox.ForeColor = Color.Black;
			}
		}

		private void EmailBox_Leave(object sender, EventArgs e) {
			if(string.IsNullOrWhiteSpace(EmailBox.Text)) {
				EmailBox.Text = mail_ph;
				EmailBox.ForeColor = Color.DarkGray;
			}
		}

		private void UsernameBox_Enter(object sender, EventArgs e) {
			if(UsernameBox.ForeColor != Color.Black) {
				UsernameBox.Text = "";
				UsernameBox.ForeColor = Color.Black;
			}
		}

		private void UsernameBox_Leave(object sender, EventArgs e) {
			if(string.IsNullOrWhiteSpace(UsernameBox.Text)) {
				UsernameBox.Text = user_ph;
				UsernameBox.ForeColor = Color.DarkGray;
			}
		}

		private void RegisterForm_Load(object sender, EventArgs e) {

		}

		private void label4_Click(object sender, EventArgs e) {

		}

		private void RoleBox_SelectedIndexChanged(object sender, EventArgs e) {
			// Guarda la opción actual
			string selected = RoleBox.SelectedItem.ToString();

			// Definí cuál es la opción por defecto
			string defaultOption = "recepcionista";

			// Definí cuáles requieren contraseña
			List<string> opcionesConPassword = new List<string> { "administrador", "técnico" };

			if(opcionesConPassword.Contains(selected)) {
				using(PassForm pf = new PassForm()) {
					if(pf.ShowDialog() == DialogResult.OK && pf.ValidatedPassword) {
						// ✅ Se mantiene la selección
						return;
					} else {
						// ❌ Contraseña incorrecta → volver a la opción por defecto
						RoleBox.SelectedItem = defaultOption;
					}
				}
			}
		}

		private void PasswordBox_KeyPress(object sender, KeyPressEventArgs e) {
			if(char.IsLetterOrDigit(e.KeyChar) ||
				char.IsControl(e.KeyChar) || // backspace, delete, enter
				char.IsPunctuation(e.KeyChar) ||
				char.IsSymbol(e.KeyChar)) {
				e.Handled = false; // ✅ permitido
			} else {
				e.Handled = true;  // ❌ bloqueado
				MessageBox.Show("Caracter no permitido en la contraseña", "Advertencia");
			}
		}

		private void PhoneBox_KeyPress(object sender, KeyPressEventArgs e) {
			// Permite solo números y teclas de control (ej. backspace, borrar)
			if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) {
				e.Handled = true; // Bloquea el caracter
				MessageBox.Show("Solo se permiten números", "Advertencia");
			}
		}
	}
}