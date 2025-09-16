using Simple_Login_FORM;
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

namespace Simple_Login_FORM {
	public partial class DBLoginForm: Form {
		public DBLoginForm() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			DBConfig.UserId = UserBox.Text;
			DBConfig.Password = PassBox.Text;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
