using MySql.Data.MySqlClient;
using Simple_Login_FORM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;

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

			// Guardamos temporalmente en DbConfig
			DBConfig.UserId = user;
			DBConfig.Password = pass;

			// Intentamos conectar
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				try {
					con.Open(); // Si abre, el usuario existe y la pass es correcta
					this.DialogResult = DialogResult.OK;
					this.Close();
				} catch(MySql.Data.MySqlClient.MySqlException ex) {
					MessageBox.Show("Error al conectar con la base de datos:\n" + ex.Message, "Conexión fallida");
					// Si falla, limpiamos lo guardado
					DBConfig.UserId = null;
					DBConfig.Password = null;
				}
			}
		}


		private void DBLoginForm_Load(object sender, EventArgs e) {

		}
	}
}
