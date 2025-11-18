using MySql.Data.MySqlClient;
using Simple_Login_FORM.Services;
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


namespace Simple_Login_FORM {
	public partial class RegisterForm: Form {
		static string phone_ph = "número de 10 digitos";
		static string pass_ph = "hasta16caractere";
		static string mail_ph = "correo@mail.com";
		static string user_ph = "nombre";
		static string surname_ph = "apellido";
		static string dom_ph = "domicilio";
		string[] placeholders = { phone_ph, pass_ph, mail_ph, user_ph, surname_ph, dom_ph };
		private readonly RegisterService _registerService;

		public RegisterForm() : this(new RegisterService(new DefaultConnectionFactory(DBConfig.GetConnectionString()))) { }

		public RegisterForm(RegisterService registerService) {
			InitializeComponent();
			_registerService = registerService;
			this.MaximizeBox = false;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			PasswordBox.MaxLength = 16;
			PhoneBox.MaxLength = 10;
			UsernameBox.MaxLength = 20;
			SurnameBox.MaxLength = 20;
			EmailBox.MaxLength = 45;
			DomBox.MaxLength = 45;
			PasswordBox.KeyPress += PasswordBox_KeyPress;
			PhoneBox.KeyPress += PhoneBox_KeyPress;
		}

		private void CreateButton_Click(object sender, EventArgs e) {
			try {
				// limpiar placeholders ya existentes en tu código original
				string mail = EmailBox.Text;
				string telefono = PhoneBox.Text;
				string password = PasswordBox.Text;
				string nombre = UsernameBox.Text;
				string apellido = SurnameBox.Text;
				string domicilio = DomBox.Text;
				string rol = RoleBox.Text;

				_registerService.RegistrarEmpleado(mail, telefono, password, nombre, apellido, domicilio, rol);

				MessageBox.Show("Empleado registrado con éxito!");
				this.DialogResult = DialogResult.OK;
				this.Close();
			} catch(ArgumentException ex) {
				MessageBox.Show(ex.Message, "Error");
			} catch(Exception) {
				MessageBox.Show("Faltan datos para realizar el registro.", "Error");
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

		private void SurnameBox_Enter(object sender, EventArgs e) {
			if(SurnameBox.ForeColor != Color.Black) {
				SurnameBox.Text = "";
				SurnameBox.ForeColor = Color.Black;
			}
		}

		private void SurnameBox_Leave(object sender, EventArgs e) {
			if(string.IsNullOrWhiteSpace(SurnameBox.Text)) {
				SurnameBox.Text = user_ph;
				SurnameBox.ForeColor = Color.DarkGray;
			}
		}

		private void DomBox_Enter(object sender, EventArgs e) {
			if(DomBox.ForeColor != Color.Black) {
				DomBox.Text = "";
				DomBox.ForeColor = Color.Black;
			}
		}

		private void DomBox_Leave(object sender, EventArgs e) {
			if(string.IsNullOrWhiteSpace(DomBox.Text)) {
				DomBox.Text = user_ph;
				DomBox.ForeColor = Color.DarkGray;
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

		private void RegisterForm_Load(object sender, EventArgs e) {

		}

		private void label4_Click(object sender, EventArgs e) {

		}

		private void label5_Click(object sender, EventArgs e) {

		}

		private void textBox1_TextChanged(object sender, EventArgs e) {

		}

		private void DomBox_TextChanged(object sender, EventArgs e) {

		}

		private void ReturnButton_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}