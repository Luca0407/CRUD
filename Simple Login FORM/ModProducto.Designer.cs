namespace Simple_Login_FORM {
	partial class ModProducto {
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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.prodBox = new System.Windows.Forms.ComboBox();
			this.stockBox = new System.Windows.Forms.NumericUpDown();
			this.brandBox = new System.Windows.Forms.ComboBox();
			this.modelBox = new System.Windows.Forms.ComboBox();
			this.costBox = new System.Windows.Forms.NumericUpDown();
			this.sellBox = new System.Windows.Forms.NumericUpDown();
			this.pageBox = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.stockBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.costBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sellBox)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(754, 82);
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
			this.label1.Size = new System.Drawing.Size(61, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Producto";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label2.Location = new System.Drawing.Point(22, 150);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Stock";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label3.Location = new System.Drawing.Point(160, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Marca";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label4.Location = new System.Drawing.Point(325, 150);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(103, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Precio de Venta";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label5.Location = new System.Drawing.Point(160, 150);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 16);
			this.label5.TabIndex = 11;
			this.label5.Text = "Precio de costo";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label6.Location = new System.Drawing.Point(325, 53);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Modelo";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(754, 179);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(110, 45);
			this.button2.TabIndex = 13;
			this.button2.Text = "Cancelar";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.label7.Location = new System.Drawing.Point(515, 111);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(139, 16);
			this.label7.TabIndex = 15;
			this.label7.Text = "Página del Proveedor";
			// 
			// prodBox
			// 
			this.prodBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.prodBox.FormattingEnabled = true;
			this.prodBox.Location = new System.Drawing.Point(25, 82);
			this.prodBox.Name = "prodBox";
			this.prodBox.Size = new System.Drawing.Size(121, 24);
			this.prodBox.TabIndex = 17;
			// 
			// stockBox
			// 
			this.stockBox.Location = new System.Drawing.Point(25, 179);
			this.stockBox.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.stockBox.Name = "stockBox";
			this.stockBox.Size = new System.Drawing.Size(105, 22);
			this.stockBox.TabIndex = 18;
			// 
			// brandBox
			// 
			this.brandBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.brandBox.FormattingEnabled = true;
			this.brandBox.Location = new System.Drawing.Point(163, 82);
			this.brandBox.Name = "brandBox";
			this.brandBox.Size = new System.Drawing.Size(128, 24);
			this.brandBox.TabIndex = 19;
			// 
			// modelBox
			// 
			this.modelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.modelBox.FormattingEnabled = true;
			this.modelBox.Location = new System.Drawing.Point(328, 82);
			this.modelBox.Name = "modelBox";
			this.modelBox.Size = new System.Drawing.Size(128, 24);
			this.modelBox.TabIndex = 20;
			// 
			// costBox
			// 
			this.costBox.Location = new System.Drawing.Point(163, 179);
			this.costBox.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
			this.costBox.Name = "costBox";
			this.costBox.Size = new System.Drawing.Size(146, 22);
			this.costBox.TabIndex = 21;
			// 
			// sellBox
			// 
			this.sellBox.Location = new System.Drawing.Point(328, 179);
			this.sellBox.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.sellBox.Name = "sellBox";
			this.sellBox.Size = new System.Drawing.Size(160, 22);
			this.sellBox.TabIndex = 22;
			// 
			// pageBox
			// 
			this.pageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pageBox.FormattingEnabled = true;
			this.pageBox.Location = new System.Drawing.Point(518, 142);
			this.pageBox.Name = "pageBox";
			this.pageBox.Size = new System.Drawing.Size(225, 24);
			this.pageBox.TabIndex = 23;
			// 
			// ModProducto
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(900, 300);
			this.Controls.Add(this.pageBox);
			this.Controls.Add(this.sellBox);
			this.Controls.Add(this.costBox);
			this.Controls.Add(this.modelBox);
			this.Controls.Add(this.brandBox);
			this.Controls.Add(this.stockBox);
			this.Controls.Add(this.prodBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ModProducto";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ModProducto";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.ModProducto_Load);
			((System.ComponentModel.ISupportInitialize)(this.stockBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.costBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sellBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox prodBox;
		private System.Windows.Forms.NumericUpDown stockBox;
		private System.Windows.Forms.ComboBox brandBox;
		private System.Windows.Forms.ComboBox modelBox;
		private System.Windows.Forms.NumericUpDown costBox;
		private System.Windows.Forms.NumericUpDown sellBox;
		private System.Windows.Forms.ComboBox pageBox;
	}
}