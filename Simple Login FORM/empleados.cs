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
	public partial class empleados: Form {
		public empleados() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			RegisterForm fm = new RegisterForm();
			fm.FormBorderStyle = FormBorderStyle.None;
			fm.Show();
		}
	}
}
