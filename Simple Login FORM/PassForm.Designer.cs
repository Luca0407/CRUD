namespace Simple_Login_FORM {
	partial class PassForm {
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
			this.PassBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ShowPass = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// PassBox
			// 
			this.PassBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PassBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PassBox.Location = new System.Drawing.Point(46, 91);
			this.PassBox.MaxLength = 16;
			this.PassBox.Name = "PassBox";
			this.PassBox.Size = new System.Drawing.Size(450, 26);
			this.PassBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(42, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(495, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Ingrese la contraseña de administrador para seleccionar este rol:";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// ShowPass
			// 
			this.ShowPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ShowPass.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ShowPass.ForeColor = System.Drawing.Color.Transparent;
			this.ShowPass.Location = new System.Drawing.Point(511, 91);
			this.ShowPass.Name = "ShowPass";
			this.ShowPass.Size = new System.Drawing.Size(26, 26);
			this.ShowPass.TabIndex = 2;
			this.ShowPass.Text = "👁";
			this.ShowPass.UseVisualStyleBackColor = true;
			this.ShowPass.Click += new System.EventHandler(this.ShowPass_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Black;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.ForeColor = System.Drawing.Color.Transparent;
			this.button1.Location = new System.Drawing.Point(496, 140);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(82, 28);
			this.button1.TabIndex = 3;
			this.button1.Text = "Verificar";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// PassForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(638, 203);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.ShowPass);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.PassBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PassForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PassForm";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.PassForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox PassBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ShowPass;
		private System.Windows.Forms.Button button1;
	}
}