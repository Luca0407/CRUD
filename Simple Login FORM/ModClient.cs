using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Login_FORM {
	public partial class ModClient: Form {
		private string id_cliente;
		public ModClient(DataGridViewCell id) {
			InitializeComponent();
			id_cliente = id.Value.ToString();
			ClienteSeleccionado(int.Parse(id_cliente));
		}

		private void label2_Click(object sender, EventArgs e) {
			
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

		public void ActualizarCliente(string nombre, string apellido, string mail, string telefono, string domicilio, string dni) {
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

			}
		}

		private void button1_Click(object sender, EventArgs e) {
			ActualizarCliente(nameBox.Text, surnameBox.Text, mailBox.Text, phoneBox.Text, domBox.Text, dniBox.Text);
			MessageBox.Show("Datos actualizados con exito!");
			this.DialogResult = DialogResult.OK;
			this.Close();	
		}

		private void button2_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void ModClient_Load(object sender, EventArgs e) {

		}
	}
}
