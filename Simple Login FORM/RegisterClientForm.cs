using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Simple_Login_FORM
{
    public partial class RegisterClientForm : Form
    {
        static string phone_ph = "número de 10 digitos";
        static string mail_ph = "correo@mail.com";
        static string user_ph = "nombre";
        static string surname_ph = "apellido";
        static string dom_ph = "domicilio";
        static string dni_ph = "DNI sin puntos";
        static string cuil_ph = "CUIL sin guiones";
        string[] placeholders = { phone_ph, mail_ph, user_ph, surname_ph, dom_ph, dni_ph, cuil_ph };

        private productos productosForm;

        public RegisterClientForm(productos form)
        {
            InitializeComponent();
            productosForm = form;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            PhoneBox.MaxLength = 10;
            UsernameBox.MaxLength = 20;
            SurnameBox.MaxLength = 20;
            EmailBox.MaxLength = 45;
            DomBox.MaxLength = 45;
            DNIBox.MaxLength = 8;
            CUILBox.MaxLength = 11;
            PhoneBox.KeyPress += PhoneBox_KeyPress;
            DNIBox.KeyPress += DNIBox_KeyPress;
            CUILBox.KeyPress += CUILBox_KeyPress;
        }

        private bool EsCorreoValido(string correo)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patron);
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            TextBox[] datos = { PhoneBox, EmailBox, UsernameBox, SurnameBox, DomBox, DNIBox, CUILBox };
            try
            {
                // Limpia los placeholders si quedaron
                datos.Zip(placeholders, (box, ph) =>
                    new
                    {
                        box,
                        ph
                    }).ToList().ForEach(x =>
                    {
                        if (x.box.Text == x.ph)
                            x.box.Clear();
                    });

                // Verifica si falta algún dato
                if (datos.Any(tb => string.IsNullOrWhiteSpace(tb.Text)))
                {
                    MessageBox.Show("Faltan datos para realizar el registro", "Advertencia");
                    return;
                }

                if (!EsCorreoValido(EmailBox.Text))
                {
                    MessageBox.Show("El correo electrónico no es válido", "Error");
                    return;
                }

                using (MySqlConnection con = new MySqlConnection(DBConfig.GetConnectionString()))
                {
                    con.Open();

                    // Verificar si ya existe un cliente con el mismo DNI o CUIL
                    using (MySqlCommand cmdCheck = con.CreateCommand())
                    {
                        cmdCheck.CommandText = @"SELECT COUNT(*) FROM clientes 
                                                 WHERE DNI = @dni OR cuil = @cuil";
                        cmdCheck.Parameters.AddWithValue("@dni", DNIBox.Text);
                        cmdCheck.Parameters.AddWithValue("@cuil", CUILBox.Text);
                        
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        
                        if (count > 0)
                        {
                            MessageBox.Show("Ya existe un cliente registrado con ese DNI o CUIL.", "Cliente Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // INSERT en personas
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO personas (mail, nombre, apellido, telefono, domicilio, tipo) 
                                    VALUES (@mail, @nombre, @apellido, @telefono, @domicilio, @tipo)";
                        cmd.Parameters.AddWithValue("@mail", EmailBox.Text);
                        cmd.Parameters.AddWithValue("@nombre", UsernameBox.Text);
                        cmd.Parameters.AddWithValue("@apellido", SurnameBox.Text);
                        cmd.Parameters.AddWithValue("@telefono", PhoneBox.Text);
                        cmd.Parameters.AddWithValue("@domicilio", DomBox.Text);
                        cmd.Parameters.AddWithValue("@tipo", "c");
                        cmd.ExecuteNonQuery();
                    }

                    // INSERT en clientes (usando el último ID insertado)
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO clientes (DNI, cuil, ID_persona) 
                                    VALUES (@dni, @cuil, LAST_INSERT_ID())";
                        cmd.Parameters.AddWithValue("@dni", DNIBox.Text);
                        cmd.Parameters.AddWithValue("@cuil", CUILBox.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Cliente registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizar los campos en productos.cs
                    if (productosForm != null)
                    {
                        productosForm.SetClientData(DNIBox.Text, $"{UsernameBox.Text} {SurnameBox.Text}");
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
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

        private void PhoneBox_Enter(object sender, EventArgs e)
        {
            if (PhoneBox.ForeColor != Color.Black)
            {
                PhoneBox.Text = "";
                PhoneBox.ForeColor = Color.Black;
            }
        }

        private void PhoneBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PhoneBox.Text))
            {
                PhoneBox.Text = phone_ph;
                PhoneBox.ForeColor = Color.DarkGray;
            }
        }

        private void EmailBox_Enter(object sender, EventArgs e)
        {
            if (EmailBox.ForeColor != Color.Black)
            {
                EmailBox.Text = "";
                EmailBox.ForeColor = Color.Black;
            }
        }

        private void EmailBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                EmailBox.Text = mail_ph;
                EmailBox.ForeColor = Color.DarkGray;
            }
        }

        private void UsernameBox_Enter(object sender, EventArgs e)
        {
            if (UsernameBox.ForeColor != Color.Black)
            {
                UsernameBox.Text = "";
                UsernameBox.ForeColor = Color.Black;
            }
        }

        private void UsernameBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameBox.Text))
            {
                UsernameBox.Text = user_ph;
                UsernameBox.ForeColor = Color.DarkGray;
            }
        }

        private void SurnameBox_Enter(object sender, EventArgs e)
        {
            if (SurnameBox.ForeColor != Color.Black)
            {
                SurnameBox.Text = "";
                SurnameBox.ForeColor = Color.Black;
            }
        }

        private void SurnameBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SurnameBox.Text))
            {
                SurnameBox.Text = surname_ph;
                SurnameBox.ForeColor = Color.DarkGray;
            }
        }

        private void DomBox_Enter(object sender, EventArgs e)
        {
            if (DomBox.ForeColor != Color.Black)
            {
                DomBox.Text = "";
                DomBox.ForeColor = Color.Black;
            }
        }

        private void DomBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DomBox.Text))
            {
                DomBox.Text = dom_ph;
                DomBox.ForeColor = Color.DarkGray;
            }
        }

        private void DNIBox_Enter(object sender, EventArgs e)
        {
            if (DNIBox.ForeColor != Color.Black)
            {
                DNIBox.Text = "";
                DNIBox.ForeColor = Color.Black;
            }
        }

        private void DNIBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DNIBox.Text))
            {
                DNIBox.Text = dni_ph;
                DNIBox.ForeColor = Color.DarkGray;
            }
        }

        private void CUILBox_Enter(object sender, EventArgs e)
        {
            if (CUILBox.ForeColor != Color.Black)
            {
                CUILBox.Text = "";
                CUILBox.ForeColor = Color.Black;
            }
        }

        private void CUILBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CUILBox.Text))
            {
                CUILBox.Text = cuil_ph;
                CUILBox.ForeColor = Color.DarkGray;
            }
        }

        private void PhoneBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten números", "Advertencia");
            }
        }

        private void DNIBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten números", "Advertencia");
            }
        }

        private void CUILBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten números", "Advertencia");
            }
        }
    }
}
