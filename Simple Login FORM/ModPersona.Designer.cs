namespace Simple_Login_FORM {
	partial class ModPersona {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.surnameBox = new System.Windows.Forms.TextBox();
			this.mailBox = new System.Windows.Forms.TextBox();
			this.phoneBox = new System.Windows.Forms.TextBox();
			this.domBox = new System.Windows.Forms.TextBox();
			this.dniBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.roleBox = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.cuiBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(756, 59);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(110, 45);
			this.button1.TabIndex = 7;
			this.button1.Text = "Cargar datos";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label1.Location = new System.Drawing.Point(22, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Nombre";
			// 
			// nameBox
			// 
			this.nameBox.Location = new System.Drawing.Point(25, 82);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(174, 22);
			this.nameBox.TabIndex = 0;
			// 
			// surnameBox
			// 
			this.surnameBox.Location = new System.Drawing.Point(25, 156);
			this.surnameBox.Name = "surnameBox";
			this.surnameBox.Size = new System.Drawing.Size(174, 22);
			this.surnameBox.TabIndex = 3;
			// 
			// mailBox
			// 
			this.mailBox.Location = new System.Drawing.Point(216, 82);
			this.mailBox.Name = "mailBox";
			this.mailBox.Size = new System.Drawing.Size(202, 22);
			this.mailBox.TabIndex = 1;
			// 
			// phoneBox
			// 
			this.phoneBox.Location = new System.Drawing.Point(433, 156);
			this.phoneBox.Name = "phoneBox";
			this.phoneBox.Size = new System.Drawing.Size(143, 22);
			this.phoneBox.TabIndex = 5;
			// 
			// domBox
			// 
			this.domBox.Location = new System.Drawing.Point(216, 156);
			this.domBox.Name = "domBox";
			this.domBox.Size = new System.Drawing.Size(202, 22);
			this.domBox.TabIndex = 4;
			// 
			// dniBox
			// 
			this.dniBox.Location = new System.Drawing.Point(433, 82);
			this.dniBox.Name = "dniBox";
			this.dniBox.Size = new System.Drawing.Size(143, 22);
			this.dniBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label2.Location = new System.Drawing.Point(22, 127);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Apellido";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label3.Location = new System.Drawing.Point(213, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Email";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label4.Location = new System.Drawing.Point(430, 127);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(61, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Telefono";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label5.Location = new System.Drawing.Point(213, 127);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(63, 16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Domicilio";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label6.Location = new System.Drawing.Point(434, 53);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(30, 16);
			this.label6.TabIndex = 12;
			this.label6.Text = "DNI";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(756, 156);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(110, 45);
			this.button2.TabIndex = 13;
			this.button2.Text = "Cancelar";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// roleBox
			// 
			this.roleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.roleBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.roleBox.FormattingEnabled = true;
			this.roleBox.Items.AddRange(new object[] {
            "administrador",
            "técnico",
            "recepcionista"});
			this.roleBox.Location = new System.Drawing.Point(433, 82);
			this.roleBox.Name = "roleBox";
			this.roleBox.Size = new System.Drawing.Size(143, 24);
			this.roleBox.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label7.Location = new System.Drawing.Point(590, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(29, 16);
			this.label7.TabIndex = 15;
			this.label7.Text = "Cuil";
			// 
			// cuiBox
			// 
			this.cuiBox.Location = new System.Drawing.Point(593, 121);
			this.cuiBox.Name = "cuiBox";
			this.cuiBox.Size = new System.Drawing.Size(144, 22);
			this.cuiBox.TabIndex = 16;
			// 
			// ModPersona
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(888, 254);
			this.Controls.Add(this.cuiBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.roleBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dniBox);
			this.Controls.Add(this.domBox);
			this.Controls.Add(this.phoneBox);
			this.Controls.Add(this.mailBox);
			this.Controls.Add(this.surnameBox);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ModPersona";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ModPersona";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.ModPersona_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.TextBox surnameBox;
		private System.Windows.Forms.TextBox mailBox;
		private System.Windows.Forms.TextBox phoneBox;
		private System.Windows.Forms.TextBox domBox;
		private System.Windows.Forms.TextBox dniBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ComboBox roleBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox cuiBox;
	}
}