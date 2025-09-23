using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Reflection;

namespace Simple_Login_FORM
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();
			this.MaximizeBox = false;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			UsernameBox.MaxLength = 45;
			PasswordBox.MaxLength = 16;
			PasswordBox.KeyPress += PasswordBox_KeyPress;
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{

		}

		private void RegisterButton_Click(object sender, EventArgs e)
		{
			this.Hide();
			RegisterForm fm = new RegisterForm();
			fm.ShowDialog();
			this.Close();
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
			try {
				int i = 0;
				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();
					MySqlCommand cmd = con.CreateCommand();
					cmd.CommandType = CommandType.Text;
					cmd.CommandText = "select p.nombre from personas p JOIN empleados e ON p.ID_persona = e.ID_persona where p.nombre= @user and e.contraseña = @pass";
					cmd.Parameters.AddWithValue("@user", UsernameBox.Text);
					cmd.Parameters.AddWithValue("@pass", PasswordBox.Text);

					DataTable dt = new DataTable();
					MySqlDataAdapter da = new MySqlDataAdapter(cmd);
					da.Fill(dt);

					i = dt.Rows.Count;

					if(i == 0) {
						MessageBox.Show("Usuario o contraseña incorrecto.", "ERROR");
					} else {
						this.Hide();							// Estas 4 lineas
						CRUD_Personas fm = new CRUD_Personas(); // sirven para
						fm.ShowDialog();						// cerrar el formulario
						this.Close();							// actual y abrir otro.
					}
				}
			} catch(Exception el) {
				MessageBox.Show("Ingrese sus datos para iniciar sesión " + el, "Faltan datos...");
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
	}
}
