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
	public partial class PassForm: Form {
		public bool ValidatedPassword { get; private set; } = false;
		public PassForm() {
			InitializeComponent();
			PassBox.UseSystemPasswordChar = true;
			PassBox.MaxLength = 16;

			// 🔹 Manejar evento para validar caracteres
			PassBox.KeyPress += PassBox_KeyPress;
		}

		private void PassForm_Load(object sender, EventArgs e) {

		}
		private void button1_Click(object sender, EventArgs e) {
			// 🔒 acá definís tu contraseña real
			string correctPass = "admin";

			if(PassBox.Text == correctPass) {
				ValidatedPassword = true;
				this.DialogResult = DialogResult.OK;
			} else {
				MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				ValidatedPassword = false;
				this.DialogResult = DialogResult.Cancel;
			}
			this.Close();
		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void PassBox_KeyPress(object sender, KeyPressEventArgs e) {
			if(char.IsLetterOrDigit(e.KeyChar) ||
				char.IsControl(e.KeyChar) || // backspace, delete, enter
				char.IsPunctuation(e.KeyChar) ||
				char.IsSymbol(e.KeyChar)) {
				e.Handled = false; // ✅ permitido
			} else {
				e.Handled = true;  // ❌ bloqueado
				MessageBox.Show("Caracter no permitido en la contraseña", "Advertencia");
			}
		}

		private void ShowPass_Click(object sender, EventArgs e) {
			// Alterna entre mostrar y ocultar la contraseña
			PassBox.UseSystemPasswordChar = !PassBox.UseSystemPasswordChar;

			// (Opcional) cambiar el texto del botón según el estado
			ShowPass.Text = PassBox.UseSystemPasswordChar ? "👁" : "➖";
		}
	}
}
