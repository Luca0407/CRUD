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
			UsernameBox.MaxLength = 20;
			PasswordBox.MaxLength = 16;
			PasswordBox.KeyPress += PasswordBox_KeyPress;
			this.KeyPreview = true;
			this.KeyDown += new KeyEventHandler(onKeyDown);
		}

		private void onKeyDown(object sender, KeyEventArgs e) {
			if(e.KeyCode == Keys.Enter) {
				LoginButton.Focus();
				LoginButton_Click(sender, e);
			}
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{

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
						// Get user role
						MySqlCommand roleCmd = con.CreateCommand();
						roleCmd.CommandType = CommandType.Text;
						roleCmd.CommandText = "SELECT e.rol FROM empleados e JOIN personas p ON e.ID_persona = p.ID_persona WHERE p.nombre = @user AND e.contraseña = @pass";
						roleCmd.Parameters.AddWithValue("@user", UsernameBox.Text);
						roleCmd.Parameters.AddWithValue("@pass", PasswordBox.Text);
						int userRole = Convert.ToInt32(roleCmd.ExecuteScalar());

						// Update activo field to 1 for successful login
						MySqlCommand updateCmd = con.CreateCommand();
						updateCmd.CommandType = CommandType.Text;
						updateCmd.CommandText = "UPDATE empleados e JOIN personas p ON e.ID_persona = p.ID_persona SET e.activo = 1 WHERE p.nombre = @user AND e.contraseña = @pass";
						updateCmd.Parameters.AddWithValue("@user", UsernameBox.Text);
						updateCmd.Parameters.AddWithValue("@pass", PasswordBox.Text);
						updateCmd.ExecuteNonQuery();

						this.Hide();
						menu fm = new menu(userRole); // Pass the user role to menu
						fm.ShowDialog();
						this.Close();
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

        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {
			
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
			
        }

        private void UsernameBox_Enter(object sender, EventArgs e)
        {
			if (UsernameBox.Text == "USUARIO") {
				UsernameBox.Text = "";
				UsernameBox.ForeColor = Color.LightGray;

			 
			 }
        }

        private void UsernameBox_Leave(object sender, EventArgs e)
        {
			if(UsernameBox.Text == "") {
				UsernameBox.Text = "USUARIO";
				UsernameBox.ForeColor = Color.DimGray;

			}
        }

        private void PasswordBox_Enter(object sender, EventArgs e)
        {
			if (PasswordBox.Text != "")
			{
				PasswordBox.Text = "";
				PasswordBox.ForeColor = Color.LightGray;
				PasswordBox.UseSystemPasswordChar = true;
				PasswordBox.PasswordChar = '*';
			}
        }

        private void PasswordBox_Leave(object sender, EventArgs e)
        {
			if (PasswordBox.Text == "")
			{
				PasswordBox.Text = "CONTRASEÑA";
				PasswordBox.ForeColor = Color.DimGray;
				PasswordBox.UseSystemPasswordChar = false;
				PasswordBox.PasswordChar = (char) 0;
			}
        }
    }
}
