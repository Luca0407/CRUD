using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Simple_Login_FORM
{
    public partial class RegisterProductForm : Form
    {
        private productos productosForm;

        public RegisterProductForm(productos form = null)
        {
            InitializeComponent();
            productosForm = form;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            // Load ComboBoxes
            CargarNombresGenericos();
            CargarProveedores();
            CargarMarcas();
            CargarModelos();
            
            // Set numeric constraints
            PrecioCostoBox.DecimalPlaces = 2;
            PrecioVentaBox.DecimalPlaces = 2;
            StockBox.Minimum = 0;
            StockBox.Maximum = 9999;
        }

        private void CargarNombresGenericos()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();
                    string query = "SELECT ID_nombre_productos, nombre_generico FROM productos_genericos ORDER BY nombre_generico ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    NombreBox.DataSource = dt;
                    NombreBox.DisplayMember = "nombre_generico";
                    NombreBox.ValueMember = "ID_nombre_productos";
                    NombreBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar nombres de productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProveedores()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();
                    string query = @"SELECT prov.ID_proveedores, p.nombre 
                                     FROM proveedores prov
                                     INNER JOIN personas p ON prov.ID_persona = p.ID_persona
                                     WHERE p.tipo = 'p'
                                     ORDER BY p.nombre ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ProveedorBox.DataSource = dt;
                    ProveedorBox.DisplayMember = "nombre";
                    ProveedorBox.ValueMember = "ID_proveedores";
                    ProveedorBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMarcas()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();
                    string query = "SELECT ID_marcas, nombre_marca FROM marcas ORDER BY nombre_marca ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    MarcaBox.DataSource = dt;
                    MarcaBox.DisplayMember = "nombre_marca";
                    MarcaBox.ValueMember = "ID_marcas";
                    MarcaBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarModelos()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();
                    string query = "SELECT ID_modelos, nombre_modelo FROM modelos ORDER BY nombre_modelo ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    ModeloBox.DataSource = dt;
                    ModeloBox.DisplayMember = "nombre_modelo";
                    ModeloBox.ValueMember = "ID_modelos";
                    ModeloBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar modelos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos estén completos
                if (NombreBox.SelectedIndex == -1 || ProveedorBox.SelectedIndex == -1 || 
                    MarcaBox.SelectedIndex == -1 || ModeloBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, seleccione todos los campos requeridos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (PrecioCostoBox.Value <= 0 || PrecioVentaBox.Value <= 0)
                {
                    MessageBox.Show("Los precios deben ser mayores a 0.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (StockBox.Value < 0)
                {
                    MessageBox.Show("El stock no puede ser negativo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();

                    // Verificar si ya existe un producto con los mismos datos
                    using (MySqlCommand cmdCheck = con.CreateCommand())
                    {
                        cmdCheck.CommandText = @"SELECT COUNT(*) FROM productos 
                                                 WHERE nombre_producto = @nombre 
                                                 AND proveedor = @proveedor 
                                                 AND marca = @marca 
                                                 AND modelo = @modelo";
                        cmdCheck.Parameters.AddWithValue("@nombre", NombreBox.SelectedValue);
                        cmdCheck.Parameters.AddWithValue("@proveedor", ProveedorBox.SelectedValue);
                        cmdCheck.Parameters.AddWithValue("@marca", MarcaBox.SelectedValue);
                        cmdCheck.Parameters.AddWithValue("@modelo", ModeloBox.SelectedValue);

                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Ya existe un producto registrado con esa combinación de nombre, proveedor, marca y modelo.", 
                                            "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insertar el nuevo producto
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO productos (nombre_producto, precio_costo, precio_venta, stock, proveedor, marca, modelo) 
                                            VALUES (@nombre, @costo, @venta, @stock, @proveedor, @marca, @modelo)";
                        cmd.Parameters.AddWithValue("@nombre", NombreBox.SelectedValue);
                        cmd.Parameters.AddWithValue("@costo", PrecioCostoBox.Value);
                        cmd.Parameters.AddWithValue("@venta", PrecioVentaBox.Value);
                        cmd.Parameters.AddWithValue("@stock", StockBox.Value);
                        cmd.Parameters.AddWithValue("@proveedor", ProveedorBox.SelectedValue);
                        cmd.Parameters.AddWithValue("@marca", MarcaBox.SelectedValue);
                        cmd.Parameters.AddWithValue("@modelo", ModeloBox.SelectedValue);
                        cmd.ExecuteNonQuery();

                        long productoId = cmd.LastInsertedId;

                        // Obtener el nombre completo del producto para mostrarlo
                        string nombreCompleto = $"{NombreBox.Text} {MarcaBox.Text} {ModeloBox.Text}";

                        MessageBox.Show("Producto registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Actualizar los campos en productos.cs
                        if (productosForm != null)
                        {
                            productosForm.SetProductData(productoId.ToString(), nombreCompleto);
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void RegisterProductForm_Load(object sender, EventArgs e) {

		}
	}
}
