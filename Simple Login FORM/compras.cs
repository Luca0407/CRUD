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
    public partial class compras : Form
    {
        private decimal totalCompra = 0;

        public compras()
        {
            InitializeComponent();
            ListarProducto();
            ListarProveedores();
            CargarMetodosPago();
            dataGridView1.Columns["X"].Width = 37;
           
            comboBox1.SelectedIndex = 0;
            // Set numeric updown minimum
            numericUpDown1.Minimum = 1;
            numericUpDown1.Value = 1;
            // Make stock fields readonly, but allow price to be manually entered
          
            NameBox.ReadOnly = true;
            // Allow manual price entry
            textBox6.ReadOnly = false;
        }

        private void compras_Load(object sender, EventArgs e)
        {
            DateBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void ListarProveedores()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();
                    string proveedores = @"SELECT DISTINCT pr.ID_proveedores, CONCAT_WS(' ', p.nombre, p.apellido) as NombreCompleto FROM personas p
                                    JOIN proveedores pr ON p.ID_persona = pr.ID_persona WHERE p.tipo = 'p' AND pr.ID_proveedores LIKE @busqueda;";
                    MySqlCommand cmd = new MySqlCommand(proveedores, con);
                    // Usamos el comodín para que el autocompletado funcione (busca IDs que COMIENZAN con el texto)
                    cmd.Parameters.AddWithValue("@busqueda", "%" + DocNum.Text + "%");

                    MySqlDataReader reader = cmd.ExecuteReader();

                    AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

                    while (reader.Read())
                    {
                        // 1. Obtener el ID (usando GetString() o GetInt32().ToString(), dependiendo del driver/versión)
                        string id = reader.GetValue(reader.GetOrdinal("ID_proveedores")).ToString();

                        // 2. Obtener el Nombre Completo
                        string nombre = reader.GetString("NombreCompleto");

                        // 3. CONCATENAR AMBOS para la sugerencia: "ID - Nombre Completo"
                        string sugerencia = id + " - " + nombre;

                        // Agregar la sugerencia completa a la colección
                        coleccion.Add(sugerencia);
                    }

                    DocNum.AutoCompleteMode = AutoCompleteMode.Suggest;
                    DocNum.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    DocNum.AutoCompleteCustomSource = coleccion;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message);
            }
        }

        private void ListarProducto()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();

                    // La consulta SQL es correcta para filtrar por ID y obtener el nombre concatenado.
                    string nombres = @"SELECT DISTINCT p.ID_productos, CONCAT_WS(' ', pg.nombre_generico, m.nombre_marca, mo.nombre_modelo) AS NombreCompleto
                                    FROM productos p 
                                    JOIN productos_genericos pg ON pg.ID_nombre_productos = p.nombre_producto
                                    JOIN marcas m ON m.ID_marcas = p.marca
                                    LEFT JOIN modelos mo ON mo.ID_modelos = p.modelo
                                    WHERE p.ID_productos LIKE @busqueda;";

                    MySqlCommand cmd = new MySqlCommand(nombres, con);
                    // Usamos el comodín para que el autocompletado funcione (busca IDs que COMIENZAN con el texto)
                    cmd.Parameters.AddWithValue("@busqueda", "%" + CodProd.Text + "%");

                    MySqlDataReader reader = cmd.ExecuteReader();

                    AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

                    while (reader.Read())
                    {
                        // 1. Obtener el ID (usando GetString() o GetInt32().ToString(), dependiendo del driver/versión)
                        string id = reader.GetValue(reader.GetOrdinal("ID_productos")).ToString();

                        // 2. Obtener el Nombre Completo
                        string nombre = reader.GetString("NombreCompleto");

                        // 3. CONCATENAR AMBOS para la sugerencia: "ID - Nombre Completo"
                        string sugerencia = id + " - " + nombre;

                        // Agregar la sugerencia completa a la colección
                        coleccion.Add(sugerencia);
                    }

                    CodProd.AutoCompleteMode = AutoCompleteMode.Suggest;
                    CodProd.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    CodProd.AutoCompleteCustomSource = coleccion;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        private void CargarMetodosPago()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();
                    string sql = "SELECT ID_pagos, metodo_pago FROM pagos";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    comboBox1.Items.Clear();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(new
                        {
                            Text = reader.GetString("metodo_pago"),
                            Value = reader.GetInt32("ID_pagos")
                        });
                    }
                    comboBox1.DisplayMember = "Text";
                    comboBox1.ValueMember = "Value";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar métodos de pago: " + ex.Message);
            }
        }

        private void CargarProducto(int idProducto)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();
                    string sql = @"SELECT p.ID_productos, 
                                        CONCAT_WS(' ', pg.nombre_generico, m.nombre_marca, mo.nombre_modelo) AS NombreCompleto,
                                        p.precio_costo,
                                        p.stock
                                    FROM productos p
                                    JOIN productos_genericos pg ON pg.ID_nombre_productos = p.nombre_producto
                                    JOIN marcas m ON m.ID_marcas = p.marca
                                    LEFT JOIN modelos mo ON mo.ID_modelos = p.modelo
                                    WHERE p.ID_productos = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        textBox5.Text = reader.GetString("NombreCompleto");
                        // Store the default price but don't overwrite if user has already entered a price
                        string defaultPrice = reader.GetDecimal("precio_costo").ToString("N2");
                        if (string.IsNullOrWhiteSpace(textBox6.Text))
                        {
                            textBox6.Text = defaultPrice;
                        }
                        
                        numericUpDown1.Value = 1;
                    }
                    else
                    {
                        MessageBox.Show("Producto no encontrado");
                        LimpiarCamposProducto();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar producto: " + ex.Message);
            }
        }

        private void LimpiarCamposProducto()
        {
            textBox5.Clear();
            textBox6.Clear();
            
            numericUpDown1.Value = 1;
        }

        private void AgregarProductoAGrid()
        {
            if (string.IsNullOrWhiteSpace(CodProd.Text))
            {
                MessageBox.Show("Ingrese un código de producto");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Busque el producto primero");
                return;
            }

            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0");
                return;
            }

            // Get product ID from CodProd textbox
            string codProdText = CodProd.Text.Trim();
            int idProducto;
            if (codProdText.Contains("-"))
            {
                string[] parts = codProdText.Split('-');
                if (!int.TryParse(parts[0].Trim(), out idProducto))
                {
                    MessageBox.Show("Código de producto inválido");
                    return;
                }
            }
            else
            {
                if (!int.TryParse(codProdText, out idProducto))
                {
                    MessageBox.Show("Código de producto inválido");
                    return;
                }
            }

            string producto = textBox5.Text;
            // Use manually entered price if available, otherwise use the product's default price
            decimal precio;
            if (!decimal.TryParse(textBox6.Text, out precio))
            {
                MessageBox.Show("Ingrese un precio válido");
                return;
            }
            int cantidad = (int)numericUpDown1.Value;
            decimal subtotal = precio * cantidad;

            // Check if product already exists in grid
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Producto"].Value != null &&
                   row.Cells["Producto"].Value.ToString() == producto)
                {
                    MessageBox.Show("El producto ya está en la lista");
                    return;
                }
            }

            // Add to grid with ID as Tag
            int rowIndex = dataGridView1.Rows.Add(producto, precio.ToString("N2"), cantidad, subtotal.ToString("N2"), "X");
            dataGridView1.Rows[rowIndex].Tag = idProducto; // Store product ID

            
            LimpiarCamposProducto();
            CodProd.Clear();
            CodProd.Focus();
        }

       
       

       

        private void GuardarCompra()
        {
            try
            {
                // Validations
                if (string.IsNullOrWhiteSpace(DocNum.Text))
                {
                    MessageBox.Show("Seleccione un proveedor");
                    return;
                }

                // Check if there are actual products (excluding the empty row)
                int productCount = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Tag != null && !row.IsNewRow)
                    {
                        productCount++;
                    }
                }

                if (productCount == 0)
                {
                    MessageBox.Show("Agregue productos a la compra");
                    return;
                }

                if (comboBox1.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione un método de pago");
                    return;
                }

                

                // Get provider ID
                string provText = DocNum.Text.Trim();
                string provId = provText.Contains("-") ? provText.Split('-')[0].Trim() : provText;

                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();
                    MySqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // Get provider ID
                        string sqlProveedor = "SELECT ID_proveedores FROM proveedores WHERE ID_proveedores = @provId";
                        MySqlCommand cmdProveedor = new MySqlCommand(sqlProveedor, con, transaction);
                        cmdProveedor.Parameters.AddWithValue("@provId", provId);
                        object proveedorId = cmdProveedor.ExecuteScalar();

                        if (proveedorId == null || proveedorId == DBNull.Value)
                        {
                            MessageBox.Show("Proveedor no encontrado");
                            transaction.Rollback();
                            return;
                        }

                        // ✅ Get employee with activo = 1
                        string sqlEmpleado = "SELECT ID_empleados FROM empleados WHERE activo = 1 LIMIT 1";
                        MySqlCommand cmdEmpleado = new MySqlCommand(sqlEmpleado, con, transaction);
                        object empleadoObj = cmdEmpleado.ExecuteScalar();

                        if (empleadoObj == null || empleadoObj == DBNull.Value)
                        {
                            MessageBox.Show("Error: No se encontró ningún empleado activo en sesión.");
                            transaction.Rollback();
                            return;
                        }

                        int empleadoId = Convert.ToInt32(empleadoObj);

                        // Get or create caja
                        string sqlCaja = "SELECT ID_caja FROM caja WHERE dia_cierre IS NULL LIMIT 1";
                        MySqlCommand cmdCaja = new MySqlCommand(sqlCaja, con, transaction);
                        object cajaId = cmdCaja.ExecuteScalar();

                        if (cajaId == null || cajaId == DBNull.Value)
                        {
                            // Create a new caja
                            string sqlCrearCaja = @"INSERT INTO caja (dia_hoy, `dinero inicial`, empleado_apertura) 
                                            VALUES (@fecha, 0, @empleado)";
                            MySqlCommand cmdCrearCaja = new MySqlCommand(sqlCrearCaja, con, transaction);
                            cmdCrearCaja.Parameters.AddWithValue("@fecha", DateTime.Today);
                            cmdCrearCaja.Parameters.AddWithValue("@empleado", empleadoId);
                            cmdCrearCaja.ExecuteNonQuery();
                            cajaId = cmdCrearCaja.LastInsertedId;
                        }

                        // Get payment method ID
                        dynamic selectedPago = comboBox1.SelectedItem;
                        int tipoPagoId = selectedPago.Value;

                        // Insert compra
                        string sqlCompra = @"INSERT INTO compras (fecha_compra, costo_total, costo_pagado, ID_proveedor, ID_empleado, ID_caja, tipo_pago)
                                    VALUES (@fecha, @total, @pagado, @proveedor, @empleado, @caja, @tipoPago)";
                        MySqlCommand cmdCompra = new MySqlCommand(sqlCompra, con, transaction);
                        cmdCompra.Parameters.AddWithValue("@fecha", DateTime.Today);
                        cmdCompra.Parameters.AddWithValue("@total", totalCompra);
                       
                        cmdCompra.Parameters.AddWithValue("@proveedor", proveedorId);
                        cmdCompra.Parameters.AddWithValue("@empleado", empleadoId);
                        cmdCompra.Parameters.AddWithValue("@caja", cajaId);
                        cmdCompra.Parameters.AddWithValue("@tipoPago", tipoPagoId);
                        cmdCompra.ExecuteNonQuery();
                        long compraId = cmdCompra.LastInsertedId;

                        // Insert detalles_compra and update stock
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Tag != null && !row.IsNewRow)
                            {
                                int productoId = (int)row.Tag;
                                int cantidad = int.Parse(row.Cells["Cantidad"].Value.ToString());
                                decimal precioUnit = decimal.Parse(row.Cells["Precio"].Value.ToString());
                                decimal subtotal = decimal.Parse(row.Cells["SubTotal"].Value.ToString());

                                string sqlDetalle = @"INSERT INTO detalles_compra (cantidad, precio_unitario, subtotal, pagado, producto, ID_compra)
                                                VALUES (@cantidad, @precio, @subtotal, 1, @producto, @compra)";
                                MySqlCommand cmdDetalle = new MySqlCommand(sqlDetalle, con, transaction);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdDetalle.Parameters.AddWithValue("@precio", precioUnit);
                                cmdDetalle.Parameters.AddWithValue("@subtotal", subtotal);
                                cmdDetalle.Parameters.AddWithValue("@producto", productoId);
                                cmdDetalle.Parameters.AddWithValue("@compra", compraId);
                                cmdDetalle.ExecuteNonQuery();

                                // Update stock (increase for purchases)
                                string sqlStock = "UPDATE productos SET stock = stock + @cantidad WHERE ID_productos = @id";
                                MySqlCommand cmdStock = new MySqlCommand(sqlStock, con, transaction);
                                cmdStock.Parameters.AddWithValue("@cantidad", cantidad);
                                cmdStock.Parameters.AddWithValue("@id", productoId);
                                cmdStock.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Compra registrada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error en la transacción: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            DocNum.Clear();
            NameBox.Clear();
            CodProd.Clear();
            LimpiarCamposProducto();
            dataGridView1.Rows.Clear();
           
            totalCompra = 0;
            DateBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
            DocNum.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Search product button
            if (string.IsNullOrWhiteSpace(CodProd.Text))
            {
                MessageBox.Show("Ingrese un código de producto");
                return;
            }

            string codProdText = CodProd.Text.Trim();
            int idProducto;

            // Extract ID from autocomplete format "ID - Name" or just ID
            if (codProdText.Contains("-"))
            {
                string[] parts = codProdText.Split('-');
                if (!int.TryParse(parts[0].Trim(), out idProducto))
                {
                    MessageBox.Show("Código de producto inválido");
                    return;
                }
            }
            else
            {
                if (!int.TryParse(codProdText, out idProducto))
                {
                    MessageBox.Show("Código de producto inválido");
                    return;
                }
            }

            CargarProducto(idProducto);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Search provider button
            if (string.IsNullOrWhiteSpace(DocNum.Text))
            {
                MessageBox.Show("Ingrese un ID de proveedor");
                return;
            }

            string provText = DocNum.Text.Trim();
            string provId;

            // Extract provider ID and name from autocomplete format "ID - Name"
            if (provText.Contains("-"))
            {
                string[] parts = provText.Split(new string[] { " - " }, StringSplitOptions.None);
                if (parts.Length >= 2)
                {
                    DocNum.Text = parts[0].Trim();
                    NameBox.Text = parts[1].Trim();
                }
                else
                {
                    provId = provText;
                }
            }
            else
            {
                provId = provText;
                // Search in database for name
                try
                {
                    using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                    {
                        con.Open();
                        string sql = "SELECT CONCAT_WS(' ', p.nombre, p.apellido) as NombreCompleto FROM personas p JOIN proveedores pr ON p.ID_persona = pr.ID_persona WHERE pr.ID_proveedores = @provId";
                        MySqlCommand cmd = new MySqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@provId", provId);
                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null)
                        {
                            NameBox.Text = resultado.ToString();

                        }
                        else
                        {
                            MessageBox.Show("Proveedor no encontrado");
                            NameBox.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar proveedor: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateBox.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Add product to cart
            AgregarProductoAGrid();
        }

        private void textBox9_TextChanged_1(object sender, EventArgs e)
        {
            // Calculate change when "paga con" changes
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Create purchase
            GuardarCompra();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            // Update subtotal when price changes
            ActualizarSubtotal();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Update subtotal when quantity changes
            ActualizarSubtotal();
        }

        private void ActualizarSubtotal()
        {
            // Update the subtotal display when price or quantity changes
            if (decimal.TryParse(textBox6.Text, out decimal precio) && 
                int.TryParse(numericUpDown1.Value.ToString(), out int cantidad))
            {
                decimal subtotal = precio * cantidad;
                // Note: This is just for display purposes, the actual subtotal is calculated when adding to grid
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }
    }
}