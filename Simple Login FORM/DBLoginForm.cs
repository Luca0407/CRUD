using MySql.Data.MySqlClient;
using Simple_Login_FORM;
using System;
using System.Windows.Forms;

namespace Simple_Login_FORM {
	public partial class DBLoginForm: Form {
		public DBLoginForm() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			string user = UserBox.Text.Trim();
			string pass = PassBox.Text.Trim();

			if(string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass)) {
				MessageBox.Show("Debes ingresar usuario y contraseña.", "Advertencia");
				return;
			}

			DBConfig.UserId = user;
			DBConfig.Password = pass;

			if(TryLogin(DBConfig.UserId, DBConfig.Password)) {
				// Guardamos en Settings
				Properties.Settings.Default.UserId = user;
				Properties.Settings.Default.Password = pass;
				Properties.Settings.Default.Save();

				this.DialogResult = DialogResult.OK;
				this.Close();
			} else {
				DBConfig.UserId = null;
				DBConfig.Password = null;
				MessageBox.Show("Usuario o contraseña incorrectos.", "Conexión fallida");
			}
		}

		private void DBLoginForm_Load(object sender, EventArgs e) {
			// Cargar desde Settings
			string user = Properties.Settings.Default.UserId;
			string pass = Properties.Settings.Default.Password;

			if(!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass)) {
				// Mostrar en los TextBox
				UserBox.Text = user;
				PassBox.Text = pass;

				// Intentar login automático
				if(TryLogin(user, pass)) {
					DBConfig.UserId = user;
					DBConfig.Password = pass;

					this.BeginInvoke(new Action(() =>
					{
						this.DialogResult = DialogResult.OK;
						this.Close();
					}));
				}
			}
		}

		private bool TryLogin(string user, string pass) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				try {
					con.Open();
					return true;
				} catch {
					return false;
				}
			}
		}
	}
}
