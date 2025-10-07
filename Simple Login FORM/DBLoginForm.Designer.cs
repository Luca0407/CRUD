namespace Simple_Login_FORM {
	partial class DBLoginForm {
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
			this.UserBox = new System.Windows.Forms.TextBox();
			this.PassBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// UserBox
			// 
			this.UserBox.Location = new System.Drawing.Point(40, 42);
			this.UserBox.Name = "UserBox";
			this.UserBox.Size = new System.Drawing.Size(100, 22);
			this.UserBox.TabIndex = 0;
			// 
			// PassBox
			// 
			this.PassBox.Location = new System.Drawing.Point(40, 110);
			this.PassBox.Name = "PassBox";
			this.PassBox.Size = new System.Drawing.Size(100, 22);
			this.PassBox.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Black;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.ForeColor = System.Drawing.Color.Transparent;
			this.button1.Location = new System.Drawing.Point(167, 189);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(87, 36);
			this.button1.TabIndex = 2;
			this.button1.Text = "Aplicar";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(40, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "usuario SQL";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(40, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "contraseña SQL";
			// 
			// DBLoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(266, 237);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.PassBox);
			this.Controls.Add(this.UserBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DBLoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DBLoginForm";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.DBLoginForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox UserBox;
		private System.Windows.Forms.TextBox PassBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}