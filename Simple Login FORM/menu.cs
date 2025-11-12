using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using MySqlX.XDevAPI;


namespace Simple_Login_FORM
{
    public partial class menu : Form
    {
		private int userRole;

		public menu(int role)
        {
            InitializeComponent();
			userRole = role;
			
			// Asegurate que panel9 se redimensione con el form
			panel9.Dock = DockStyle.Fill;

			// Evento: cuando cambie el tamaño del panel forzamos ajuste de hijos
			panel9.SizeChanged += Panel9_SizeChanged;

			// También hook al Resize del formulario principal por si hiciera falta
			this.Resize += Menu_Resize;
			
			// Apply role-based filtering
			ApplyRoleBasedAccess();
		}

		private void ApplyRoleBasedAccess()
		{
			// Role 2 = TECNICO: Can only access PRODUCTOS (devices only) and CLIENTES
			// Role 3 = RECEPCIONISTA: Can access VENTAS, PRODUCTOS, CLIENTES, and REPORTES
			// Role 1 = ADMINISTRADOR: Can access everything (no restrictions)

			if (userRole == 2) // TECNICO
			{
				// Hide menu buttons that TECNICO cannot access
				btnVentas.Visible = false;
				panel4.Visible = false;
				
				btnCompras.Visible = false;
				panel5.Visible = false;
				
				btnEmpleado.Visible = false;
				panel7.Visible = false;
				
				btnReportes.Visible = false;
				panel8.Visible = false;
			}
			else if (userRole == 3) // RECEPCIONISTA
			{
				// Hide menu buttons that RECEPCIONISTA cannot access
				btnCompras.Visible = false;
				panel5.Visible = false;
				
				btnEmpleado.Visible = false;
				panel7.Visible = false;
			}
			// Role 1 (ADMINISTRADOR) has no restrictions, all buttons remain visible
		}

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        

        private void btnMaximizar_Click(object sender, EventArgs e) // Botón de maximizar
        {
            this.WindowState = FormWindowState.Maximized;
			btnMaximizar.Visible = false;
            pictureBox3.Visible = true;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Normal;
            pictureBox3.Visible = false;
            btnMaximizar.Visible = true;

        }


        private void btnMinimizar_Click(object sender, EventArgs e) // Botón de minimizar
        {
            this.WindowState = FormWindowState.Minimized;
            // ⚠️ no toco la visibilidad de otros botones acá
        }

		private void Menu_Resize(object sender, EventArgs e) {
			// Llamamos al mismo manejador para centralizar la lógica
			Panel9_SizeChanged(panel9, EventArgs.Empty);
		}

		private void Panel9_SizeChanged(object sender, EventArgs e) {
			// Forzamos a que cada control llene exactamente el panel
			foreach(Control ctrl in panel9.Controls) {
				// Si es un Form (lo están siendo) o cualquier control, fija tamaño y posición
				ctrl.Dock = DockStyle.Fill;
				ctrl.Location = new Point(0, 0);
				ctrl.Size = panel9.ClientSize;
				ctrl.BringToFront();
				ctrl.Refresh();
			}
		}

		[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint ="SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int  lParam);

        
        

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {
			panel9.Controls.Clear();

			// Crea la instancia
			empleados frmEmpleado = new empleados();

			// Asegura que se comporte como control hijo y se ajuste al panel
			frmEmpleado.TopLevel = false;
			frmEmpleado.FormBorderStyle = FormBorderStyle.None;
			frmEmpleado.Dock = DockStyle.Fill;

			// Agrega y muestra
			panel9.SuspendLayout();
			panel9.Controls.Add(frmEmpleado);
			frmEmpleado.Show();
			frmEmpleado.Location = new Point(0, 0);
			frmEmpleado.Size = panel9.ClientSize;
			frmEmpleado.BringToFront();
			panel9.ResumeLayout(false);

			// Ejecutar el ajuste por si el panel cambió poco después
			Panel9_SizeChanged(panel9, EventArgs.Empty);
		}

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
			panel9.Controls.Clear();

			// Crea la instancia
			Reportes frmReportes = new Reportes();

			// Asegura que se comporte como control hijo y se ajuste al panel
			frmReportes.TopLevel = false;
			frmReportes.FormBorderStyle = FormBorderStyle.None;
			frmReportes.Dock = DockStyle.Fill;

			// Agrega y muestra
			panel9.SuspendLayout();
			panel9.Controls.Add(frmReportes);
			frmReportes.Show();
			frmReportes.Location = new Point(0, 0);
			frmReportes.Size = panel9.ClientSize;
			frmReportes.BringToFront();
			panel9.ResumeLayout(false);
			// Ejecutar el ajuste por si el panel cambió poco después
			Panel9_SizeChanged(panel9, EventArgs.Empty);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
			panel9.Controls.Clear();

			// Crea la instancia
			proveedor frmProveedor = new proveedor();

			// Asegura que se comporte como control hijo y se ajuste al panel
			frmProveedor.TopLevel = false;
			frmProveedor.FormBorderStyle = FormBorderStyle.None;
			frmProveedor.Dock = DockStyle.Fill; // ← Esto hace que se ajuste al 100% del panel

			// Agrega y muestra
			panel9.SuspendLayout();
			panel9.Controls.Add(frmProveedor);
			frmProveedor.Show();
			frmProveedor.Location = new Point(0, 0);
			frmProveedor.Size = panel9.ClientSize;
			frmProveedor.BringToFront();
			panel9.ResumeLayout(false);
			// Ejecutar el ajuste por si el panel cambió poco después
			Panel9_SizeChanged(panel9, EventArgs.Empty);
		}

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
			panel9.Controls.Clear();

			// Crea la instancia
			ventas frmVentas = new ventas();

			// Asegura que se comporte como control hijo y se ajuste al panel
			frmVentas.TopLevel = false;
			frmVentas.FormBorderStyle = FormBorderStyle.None;
			frmVentas.Dock = DockStyle.Fill; // ← Esto hace que se ajuste al 100% del panel

			// Agrega y muestra
			panel9.SuspendLayout();
			panel9.Controls.Add(frmVentas);
			frmVentas.Show();
			frmVentas.Location = new Point(0, 0);
			frmVentas.Size = panel9.ClientSize;
			frmVentas.BringToFront();
			panel9.ResumeLayout(false);
			// Ejecutar el ajuste por si el panel cambió poco después
			Panel9_SizeChanged(panel9, EventArgs.Empty);
		}

        

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
           
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            // Limpia el panel
            panel9.Controls.Clear();

            // Crea la instancia
            clientes frmClientes = new clientes();

            // Asegura que se comporte como control hijo y se ajuste al panel
            frmClientes.TopLevel = false;
            frmClientes.FormBorderStyle = FormBorderStyle.None;
            frmClientes.Dock = DockStyle.Fill; // ← Esto hace que se ajuste al 100% del panel

			// Agrega y muestra
			panel9.SuspendLayout();
			panel9.Controls.Add(frmClientes);
            frmClientes.Show();
			frmClientes.Location = new Point(0, 0);
			frmClientes.Size = panel9.ClientSize;
			frmClientes.BringToFront();
			panel9.ResumeLayout(false);

			Panel9_SizeChanged(panel9, EventArgs.Empty);
		}

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

		private void Productos_Click(object sender, EventArgs e) {
			panel9.Controls.Clear();

			// Crea la instancia
			productos frmProductos = new productos(userRole);

			// Asegura que se comporte como control hijo y se ajuste al panel
			frmProductos.TopLevel = false;
			frmProductos.FormBorderStyle = FormBorderStyle.None;
			frmProductos.Dock = DockStyle.Fill;

			// Agrega y muestra
			panel9.SuspendLayout();
			panel9.Controls.Add(frmProductos);
			frmProductos.Show();
			frmProductos.Location = new Point(0, 0);
			frmProductos.Size = panel9.ClientSize;
			frmProductos.BringToFront();
			panel9.ResumeLayout(false);
			// Ejecutar el ajuste por si el panel cambió poco después
			Panel9_SizeChanged(panel9, EventArgs.Empty);
		}

		private void button1_Click(object sender, EventArgs e) {
			/*panel9.Controls.Clear();

			// Crea la instancia
			compras frmProductos = new productos(userRole);

			// Asegura que se comporte como control hijo y se ajuste al panel
			frmProductos.TopLevel = false;
			frmProductos.FormBorderStyle = FormBorderStyle.None;
			frmProductos.Dock = DockStyle.Fill;

			// Agrega y muestra
			panel9.SuspendLayout();
			panel9.Controls.Add(frmProductos);
			frmProductos.Show();
			frmProductos.Location = new Point(0, 0);
			frmProductos.Size = panel9.ClientSize;
			frmProductos.BringToFront();
			panel9.ResumeLayout(false);
			// Ejecutar el ajuste por si el panel cambió poco después
			Panel9_SizeChanged(panel9, EventArgs.Empty);*/
		}
	}
}
