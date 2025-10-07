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

namespace Simple_Login_FORM
{
    public partial class proveedor : Form
    {
        public proveedor()
        {
            InitializeComponent();
        }

        public void ListarProveedores()
        {
            using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
            {
                con.Open();
                string sql = @"SELECT pr.ID_proveedores, p.nombre, p.apellido, pr.pagina, p.mail, p.telefono, p.domicilio
                           FROM proveedores pr 
                           INNER JOIN personas p ON pr.ID_persona = p.ID_persona";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine($"{dr["ID_proveedores"]} - {dr["nombre"]} {dr["apellido"]} - Página: {dr["pagina"]}");
                }
            }
        }
        // UPDATE
        public void EditarProveedor(int idProveedor, string nuevaPagina, string nuevoMail, string nuevoTelefono)
        {
            using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
            {
                con.Open();
                string sql = @"UPDATE personas p
                           INNER JOIN proveedores pr ON p.ID_persona = pr.ID_persona
                           SET pr.pagina = @pagina, p.mail = @mail, p.telefono = @telefono
                           WHERE pr.ID_proveedores = @idProveedor";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@pagina", nuevaPagina);
                cmd.Parameters.AddWithValue("@mail", nuevoMail);
                cmd.Parameters.AddWithValue("@telefono", nuevoTelefono);
                cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE
        public void EliminarProveedor(int idProveedor)
        {
            using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
            {
                con.Open();

                // Obtener ID_persona asociado
                string sqlGet = "SELECT ID_persona FROM proveedores WHERE ID_proveedores=@idProveedor";
                MySqlCommand cmdGet = new MySqlCommand(sqlGet, con);
                cmdGet.Parameters.AddWithValue("@idProveedor", idProveedor);
                int idPersona = Convert.ToInt32(cmdGet.ExecuteScalar());

                // Eliminar proveedor
                string sqlProveedor = "DELETE FROM proveedores WHERE ID_proveedores=@idProveedor";
                MySqlCommand cmdProveedor = new MySqlCommand(sqlProveedor, con);
                cmdProveedor.Parameters.AddWithValue("@idProveedor", idProveedor);
                cmdProveedor.ExecuteNonQuery();

                // Eliminar persona
                string sqlPersona = "DELETE FROM personas WHERE ID_persona=@idPersona";
                MySqlCommand cmdPersona = new MySqlCommand(sqlPersona, con);
                cmdPersona.Parameters.AddWithValue("@idPersona", idPersona);
                cmdPersona.ExecuteNonQuery();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EliminarProveedor(id);
        }
    }
}
