
namespace Simple_Login_FORM
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.EmailBox = new System.Windows.Forms.TextBox();
			this.UsernameBox = new System.Windows.Forms.TextBox();
			this.PasswordBox = new System.Windows.Forms.TextBox();
			this.Email = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.CreateButton = new System.Windows.Forms.Button();
			this.ReturnButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.PhoneBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.RoleBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SurnameBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.DomBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// EmailBox
			// 
			this.EmailBox.ForeColor = System.Drawing.Color.DarkGray;
			this.EmailBox.Location = new System.Drawing.Point(17, 148);
			this.EmailBox.Margin = new System.Windows.Forms.Padding(4);
			this.EmailBox.Name = "EmailBox";
			this.EmailBox.Size = new System.Drawing.Size(244, 22);
			this.EmailBox.TabIndex = 2;
			this.EmailBox.Text = "correo@mail.com";
			this.EmailBox.Enter += new System.EventHandler(this.EmailBox_Enter);
			this.EmailBox.Leave += new System.EventHandler(this.EmailBox_Leave);
			// 
			// UsernameBox
			// 
			this.UsernameBox.ForeColor = System.Drawing.Color.DarkGray;
			this.UsernameBox.Location = new System.Drawing.Point(16, 38);
			this.UsernameBox.Margin = new System.Windows.Forms.Padding(4);
			this.UsernameBox.Name = "UsernameBox";
			this.UsernameBox.Size = new System.Drawing.Size(244, 22);
			this.UsernameBox.TabIndex = 0;
			this.UsernameBox.Text = "nombre";
			this.UsernameBox.Enter += new System.EventHandler(this.UsernameBox_Enter);
			this.UsernameBox.Leave += new System.EventHandler(this.UsernameBox_Leave);
			// 
			// PasswordBox
			// 
			this.PasswordBox.ForeColor = System.Drawing.Color.DarkGray;
			this.PasswordBox.Location = new System.Drawing.Point(18, 208);
			this.PasswordBox.Margin = new System.Windows.Forms.Padding(4);
			this.PasswordBox.Name = "PasswordBox";
			this.PasswordBox.Size = new System.Drawing.Size(244, 22);
			this.PasswordBox.TabIndex = 3;
			this.PasswordBox.Text = "hasta16caractere";
			this.PasswordBox.Enter += new System.EventHandler(this.PasswordBox_Enter);
			this.PasswordBox.Leave += new System.EventHandler(this.PasswordBox_Leave);
			// 
			// Email
			// 
			this.Email.AutoSize = true;
			this.Email.ForeColor = System.Drawing.Color.Transparent;
			this.Email.Location = new System.Drawing.Point(13, 129);
			this.Email.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Email.Name = "Email";
			this.Email.Size = new System.Drawing.Size(48, 16);
			this.Email.TabIndex = 5;
			this.Email.Text = "Correo";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(12, 18);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Nombre";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(14, 188);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Contraseña";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// CreateButton
			// 
			this.CreateButton.BackColor = System.Drawing.Color.Black;
			this.CreateButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.CreateButton.ForeColor = System.Drawing.Color.Transparent;
			this.CreateButton.Location = new System.Drawing.Point(15, 434);
			this.CreateButton.Margin = new System.Windows.Forms.Padding(4);
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.Size = new System.Drawing.Size(245, 28);
			this.CreateButton.TabIndex = 8;
			this.CreateButton.Text = "Registrarse";
			this.CreateButton.UseVisualStyleBackColor = false;
			this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
			// 
			// ReturnButton
			// 
			this.ReturnButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ReturnButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ReturnButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ReturnButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ReturnButton.ForeColor = System.Drawing.Color.Transparent;
			this.ReturnButton.Location = new System.Drawing.Point(94, 470);
			this.ReturnButton.Margin = new System.Windows.Forms.Padding(4);
			this.ReturnButton.Name = "ReturnButton";
			this.ReturnButton.Size = new System.Drawing.Size(77, 28);
			this.ReturnButton.TabIndex = 9;
			this.ReturnButton.Text = "Cancelar";
			this.ReturnButton.UseVisualStyleBackColor = false;
			this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(14, 247);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 16);
			this.label3.TabIndex = 10;
			this.label3.Text = "Teléfono";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// PhoneBox
			// 
			this.PhoneBox.ForeColor = System.Drawing.Color.DarkGray;
			this.PhoneBox.Location = new System.Drawing.Point(17, 267);
			this.PhoneBox.Margin = new System.Windows.Forms.Padding(4);
			this.PhoneBox.Name = "PhoneBox";
			this.PhoneBox.Size = new System.Drawing.Size(244, 22);
			this.PhoneBox.TabIndex = 4;
			this.PhoneBox.Text = "número de 10 digitos";
			this.PhoneBox.Enter += new System.EventHandler(this.PhoneBox_Enter);
			this.PhoneBox.Leave += new System.EventHandler(this.PhoneBox_Leave);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(15, 359);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Rol";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// RoleBox
			// 
			this.RoleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RoleBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.RoleBox.FormattingEnabled = true;
			this.RoleBox.Items.AddRange(new object[] {
            "administrador",
            "técnico",
            "recepcionista"});
			this.RoleBox.Location = new System.Drawing.Point(16, 378);
			this.RoleBox.Name = "RoleBox";
			this.RoleBox.Size = new System.Drawing.Size(244, 24);
			this.RoleBox.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.Transparent;
			this.label5.Location = new System.Drawing.Point(13, 73);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 16);
			this.label5.TabIndex = 13;
			this.label5.Text = "Apellido";
			this.label5.Click += new System.EventHandler(this.label5_Click);
			// 
			// SurnameBox
			// 
			this.SurnameBox.ForeColor = System.Drawing.Color.DarkGray;
			this.SurnameBox.Location = new System.Drawing.Point(17, 93);
			this.SurnameBox.Margin = new System.Windows.Forms.Padding(4);
			this.SurnameBox.Name = "SurnameBox";
			this.SurnameBox.Size = new System.Drawing.Size(244, 22);
			this.SurnameBox.TabIndex = 1;
			this.SurnameBox.Text = "apellido";
			this.SurnameBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			this.SurnameBox.Enter += new System.EventHandler(this.SurnameBox_Enter);
			this.SurnameBox.Leave += new System.EventHandler(this.SurnameBox_Leave);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.Transparent;
			this.label6.Location = new System.Drawing.Point(13, 301);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 16);
			this.label6.TabIndex = 15;
			this.label6.Text = "Domicilio";
			// 
			// DomBox
			// 
			this.DomBox.ForeColor = System.Drawing.Color.DarkGray;
			this.DomBox.Location = new System.Drawing.Point(16, 321);
			this.DomBox.Margin = new System.Windows.Forms.Padding(4);
			this.DomBox.Name = "DomBox";
			this.DomBox.Size = new System.Drawing.Size(244, 22);
			this.DomBox.TabIndex = 5;
			this.DomBox.Text = "domicilio";
			this.DomBox.TextChanged += new System.EventHandler(this.DomBox_TextChanged);
			this.DomBox.Enter += new System.EventHandler(this.DomBox_Enter);
			this.DomBox.Leave += new System.EventHandler(this.DomBox_Leave);
			// 
			// RegisterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(284, 522);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.DomBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.SurnameBox);
			this.Controls.Add(this.RoleBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.PhoneBox);
			this.Controls.Add(this.ReturnButton);
			this.Controls.Add(this.CreateButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Email);
			this.Controls.Add(this.PasswordBox);
			this.Controls.Add(this.UsernameBox);
			this.Controls.Add(this.EmailBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RegisterForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Registrarse";
			this.Load += new System.EventHandler(this.RegisterForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.TextBox UsernameBox;
		private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.TextBox PasswordBox;
		private System.Windows.Forms.TextBox PhoneBox;
		private System.Windows.Forms.ComboBox RoleBox;
		private System.Windows.Forms.Label Email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button ReturnButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox SurnameBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox DomBox;
	}
}