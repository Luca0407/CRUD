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

namespace Simple_Login_FORM
{
    public partial class productos : Form {
		private string combomarca = @"SELECT DISTINCT ID_marcas, nombre_marca FROM marcas ORDER BY nombre_marca ASC";
		private string combomodelo = @"SELECT DISTINCT ID_modelos, nombre_modelo FROM modelos ORDER BY nombre_modelo ASC";

		public productos()
        {
			InitializeComponent();
			// Mostrar por defecto la primera pestaña
			CargarProductos(3, DGVdisp);
		}

		private void CargarProductos(int tipoProducto, DataGridView dgv) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();

				string sql = @"
            SELECT p.ID_productos, g.nombre_generico as Producto, m.nombre_marca as Marca, n.nombre_modelo as Modelo, 
                   p.stock as Stock, p.precio_costo as Costo, p.precio_venta as Venta, v.pagina as Pagina
            FROM productos p
            JOIN productos_genericos g ON p.nombre_producto = g.ID_nombre_productos
            JOIN proveedores v ON p.proveedor = v.ID_proveedores
            JOIN marcas m ON p.marca = m.ID_marcas
            JOIN modelos n ON p.modelo = n.ID_modelos
            WHERE g.tipo_producto = @tipo";

				using(MySqlCommand cmd = new MySqlCommand(sql, con)) {
					cmd.Parameters.AddWithValue("@tipo", tipoProducto);

					MySqlDataAdapter da = new MySqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);

					dgv.DataSource = dt;

					// Configuración de columnas
					if(dgv.Columns.Contains("ID_productos"))
						dgv.Columns["ID_productos"].Visible = false;
				}
			}
		}

		private void CargarComboBox(ComboBox combo, string query, string display, string value) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();
				MySqlDataAdapter da = new MySqlDataAdapter(query, con);
				DataTable dt = new DataTable();
				da.Fill(dt);

				combo.DataSource = dt;
				combo.DisplayMember = display;
				combo.ValueMember = value;
				combo.SelectedIndex = -1; // sin selección inicial
			}
		}

		private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

		private void DGVdisp_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void Filtro_Click(object sender, EventArgs e) {
			if(Products.SelectedTab == devices) {
				filtrar_Productos(DGVdisp, 3);
			} else if(Products.SelectedTab == misc) {
				filtrar_Productos(DGVacc, 1);
			} else if(Products.SelectedTab == repuest) {
				filtrar_Productos(DGVrep, 2);
			}
		}

		private void filtrar_Productos(DataGridView dgv, int tipo) {
			using(MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString())) {
				con.Open();

				// Base query
				StringBuilder sql = new StringBuilder(@"
            SELECT p.ID_productos, g.nombre_generico, m.nombre_marca, n.nombre_modelo, 
                   p.stock, p.precio_costo, p.precio_venta, v.pagina
            FROM productos p
            JOIN productos_genericos g ON p.nombre_producto = g.ID_nombre_productos
            JOIN proveedores v ON p.proveedor = v.ID_proveedores
            JOIN marcas m ON p.marca = m.ID_marcas
            JOIN modelos n ON p.modelo = n.ID_modelos
            WHERE 1=1");

				// Agregar filtros dinámicos según lo que el usuario haya seleccionado
				if(BrandBox.SelectedValue != null && BrandBox.SelectedValue != DBNull.Value)
					sql.Append(" AND p.marca = @marca");

				if(ModBox.SelectedValue != null && ModBox.SelectedValue != DBNull.Value)
					sql.Append(" AND p.modelo = @modelo");

				if(Stock.Value > 0)
					sql.Append(" AND p.stock >= @stock");

				sql.Append(" AND g.tipo_producto = @tipo");

				using(MySqlCommand cmd = new MySqlCommand(sql.ToString(), con)) {
					// Asignar parámetros solo si corresponden
					if(BrandBox.SelectedValue != null && BrandBox.SelectedValue != DBNull.Value)
						cmd.Parameters.AddWithValue("@marca", BrandBox.SelectedValue);

					if(ModBox.SelectedValue != null && ModBox.SelectedValue != DBNull.Value)
						cmd.Parameters.AddWithValue("@modelo", ModBox.SelectedValue);

					if(Stock.Value > 0)
						cmd.Parameters.AddWithValue("@stock", Stock.Value);

					cmd.Parameters.AddWithValue("@tipo", tipo);

					MySqlDataAdapter da = new MySqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);

					dgv.DataSource = dt;
					dgv.Columns["ID_productos"].Visible = false;
					ModBox.SelectedIndex = -1;
					BrandBox.SelectedIndex = -1;
				}
			}
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void tabPage2_Click(object sender, EventArgs e)
        {

        }

		private void productos_Load(object sender, EventArgs e) {
			devices.Text = "Dispositivos";
			repuest.Text = "Accesorios";
			misc.Text = "Repuestos";

			CargarProductos(3, DGVdisp);
			CargarProductos(1, DGVacc);
			CargarProductos(2, DGVrep);
			CargarComboBox(BrandBox, combomarca, "nombre_marca", "ID_marcas");
			CargarComboBox(ModBox, combomodelo, "nombre_modelo", "ID_modelos");

		}

		private void Stock_ValueChanged(object sender, EventArgs e) {

		}

		private void editButton_Click(object sender, EventArgs e) {

		}
	}
}
