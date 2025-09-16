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
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Simple_Login_FORM
{
    public partial class CRUD_Personas : Form
    {
        public CRUD_Personas()
        {
            InitializeComponent();
			DGVPersonas.AutoGenerateColumns = true;
			DGVEmpleados.AutoGenerateColumns = true;
			DGVProveedores.AutoGenerateColumns = true;
			DGVPersonas.DataSource = itemBindingSource;
			DGVEmpleados.DataSource = itemBindingSource;
			DGVProveedores.DataSource = itemBindingSource;
		}

		private void loadTable() // load values to table
{
			try {
				// limpiar el DataGridView y el BindingSource
				//dgvItems.Rows.Clear();
				itemBindingSource.Clear();

				DataSet ds = new DataSet();

				using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
					con.Open();

					// consulta SQL
					string sqlQuery = "SELECT * FROM personas";

					// ejecutar consulta y llenar dataset
					using(MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, con)) {
						da.Fill(ds);
					}
				}

				// verificar si hay filas
				if(ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
					return;

				// recorrer filas y cargar al BindingSource
				foreach(DataRow row in ds.Tables[0].Rows) {
					itemBindingSource.Add(new item {
						ID = row["ID_persona"].ToString(),
						Name = row["nombre"].ToString(),
						Mail = row["mail"].ToString(),
						Phone = row["telefono"].ToString()
						//Price = Convert.ToDouble(row["productPrice"]),
						//isActive = Convert.ToBoolean(Convert.ToInt32(row["isActive"]))
					});
				}
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar la tabla: " + ex.Message);
			}
		}

		private void CargarClientes() {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				string query = "SELECT ID_Clientes, DNI, ID_Persona FROM clientes";
				MySqlDataAdapter da = new MySqlDataAdapter(query, con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				DGVPersonas.DataSource = dt;
			}
		}

		private void CargarEmpleados() {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				string query = "SELECT ID_empleados, rol, ID_persona FROM empleados";
				MySqlDataAdapter da = new MySqlDataAdapter(query, con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				DGVEmpleados.DataSource = dt;
			}
		}

		private void CargarProveedores() {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				string query = "SELECT ID_proveedores, pagina, ID_persona FROM proveedores";
				MySqlDataAdapter da = new MySqlDataAdapter(query, con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				DGVProveedores.DataSource = dt;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbPrice.Text != string.Empty && txtProductName.Text != string.Empty) //check if all fields are filled
                {
                    string isActive = chkActive.Checked ? "1" : "0";
                    string sqlQuery = "INSERT INTO items (productName, productPrice, isActive ) VALUES ('" + txtProductName.Text + "','" + txtbPrice.Text + "', '" + isActive + "'); ";

                    //insert data
                   // MySQLconnection con = new MySQLconnection();
                    //con.Executing(sqlQuery);

                    loadTable();
                }
                else
                {
                    MessageBox.Show("Please fill all the fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

		private void CRUD_Personas_Load(object sender, EventArgs e) {
			try {
				CargarClientes();
				CargarEmpleados();
				CargarProveedores();
			} catch(Exception ex) {
				MessageBox.Show("Error al cargar datos: " + ex.Message);
			}
		}

		private void redirectToUpdateForm(int row)
        {
            //create item object
            item Item = new item();

            //set data
            //Item.ID = dgvItems.Rows[row].Cells[0].Value.ToString();
            //Item.Name = dgvItems.Rows[row].Cells[1].Value.ToString();
			//Item.Mail = dgvItems.Rows[row].Cells[1].Value.ToString();
			//Item.Phone = dgvItems.Rows[row].Cells[1].Value.ToString();

			//show form
			Form frm = new FrmUpdateItem(Item);
            frm.ShowDialog();

            //load gridview
            loadTable();
        }

		/*private void deleteData(int row)
        {
            //string ID = dgvItems.Rows[row].Cells[0].Value.ToString();

            string sqlQuery = "DELETE FROM items WHERE ID = '" + ID + "';";

            //MySQLconnection con = new MySQLconnection();
            //con.Executing(sqlQuery);

            //Load Table
            loadTable();
       } */

		/*private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (e.ColumnIndex)
                {
                    case 4:
                        redirectToUpdateForm(e.RowIndex);
                        break;
                    case 5:
                        deleteData(e.RowIndex);
                        break;
                            
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }*/

		private void panel1_Paint(object sender, PaintEventArgs e) {

		}

		private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}
	}
}
