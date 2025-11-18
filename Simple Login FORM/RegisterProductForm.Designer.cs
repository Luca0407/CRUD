namespace Simple_Login_FORM
{
    partial class RegisterProductForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.NombreBox = new System.Windows.Forms.ComboBox();
			this.ProveedorBox = new System.Windows.Forms.ComboBox();
			this.MarcaBox = new System.Windows.Forms.ComboBox();
			this.ModeloBox = new System.Windows.Forms.ComboBox();
			this.PrecioCostoBox = new System.Windows.Forms.NumericUpDown();
			this.PrecioVentaBox = new System.Windows.Forms.NumericUpDown();
			this.StockBox = new System.Windows.Forms.NumericUpDown();
			this.RegisterButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.PrecioCostoBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PrecioVentaBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StockBox)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(131, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(269, 29);
			this.label1.TabIndex = 0;
			this.label1.Text = "Restock de Productos";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// NombreBox
			// 
			this.NombreBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.NombreBox.FormattingEnabled = true;
			this.NombreBox.Location = new System.Drawing.Point(150, 80);
			this.NombreBox.Name = "NombreBox";
			this.NombreBox.Size = new System.Drawing.Size(280, 24);
			this.NombreBox.TabIndex = 1;
			// 
			// ProveedorBox
			// 
			this.ProveedorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProveedorBox.FormattingEnabled = true;
			this.ProveedorBox.Location = new System.Drawing.Point(150, 240);
			this.ProveedorBox.Name = "ProveedorBox";
			this.ProveedorBox.Size = new System.Drawing.Size(280, 24);
			this.ProveedorBox.TabIndex = 2;
			// 
			// MarcaBox
			// 
			this.MarcaBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MarcaBox.FormattingEnabled = true;
			this.MarcaBox.Location = new System.Drawing.Point(150, 280);
			this.MarcaBox.Name = "MarcaBox";
			this.MarcaBox.Size = new System.Drawing.Size(280, 24);
			this.MarcaBox.TabIndex = 3;
			// 
			// ModeloBox
			// 
			this.ModeloBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ModeloBox.FormattingEnabled = true;
			this.ModeloBox.Location = new System.Drawing.Point(150, 320);
			this.ModeloBox.Name = "ModeloBox";
			this.ModeloBox.Size = new System.Drawing.Size(280, 24);
			this.ModeloBox.TabIndex = 4;
			// 
			// PrecioCostoBox
			// 
			this.PrecioCostoBox.DecimalPlaces = 2;
			this.PrecioCostoBox.Location = new System.Drawing.Point(150, 120);
			this.PrecioCostoBox.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.PrecioCostoBox.Name = "PrecioCostoBox";
			this.PrecioCostoBox.Size = new System.Drawing.Size(280, 22);
			this.PrecioCostoBox.TabIndex = 5;
			// 
			// PrecioVentaBox
			// 
			this.PrecioVentaBox.DecimalPlaces = 2;
			this.PrecioVentaBox.Location = new System.Drawing.Point(150, 160);
			this.PrecioVentaBox.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.PrecioVentaBox.Name = "PrecioVentaBox";
			this.PrecioVentaBox.Size = new System.Drawing.Size(280, 22);
			this.PrecioVentaBox.TabIndex = 6;
			// 
			// StockBox
			// 
			this.StockBox.Location = new System.Drawing.Point(150, 200);
			this.StockBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.StockBox.Name = "StockBox";
			this.StockBox.Size = new System.Drawing.Size(280, 22);
			this.StockBox.TabIndex = 7;
			// 
			// RegisterButton
			// 
			this.RegisterButton.BackColor = System.Drawing.Color.FloralWhite;
			this.RegisterButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.RegisterButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
			this.RegisterButton.FlatAppearance.BorderSize = 2;
			this.RegisterButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
			this.RegisterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RegisterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.RegisterButton.Location = new System.Drawing.Point(100, 370);
			this.RegisterButton.Name = "RegisterButton";
			this.RegisterButton.Size = new System.Drawing.Size(120, 40);
			this.RegisterButton.TabIndex = 8;
			this.RegisterButton.Text = "Comprar";
			this.RegisterButton.UseVisualStyleBackColor = false;
			this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.BackColor = System.Drawing.Color.FloralWhite;
			this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
			this.CancelButton.FlatAppearance.BorderSize = 2;
			this.CancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
			this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.CancelButton.Location = new System.Drawing.Point(280, 370);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(120, 40);
			this.CancelButton.TabIndex = 9;
			this.CancelButton.Text = "Cancelar";
			this.CancelButton.UseVisualStyleBackColor = false;
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label2.Location = new System.Drawing.Point(70, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 18);
			this.label2.TabIndex = 10;
			this.label2.Text = "Nombre";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label3.Location = new System.Drawing.Point(30, 122);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 18);
			this.label3.TabIndex = 11;
			this.label3.Text = "Precio Costo";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label4.Location = new System.Drawing.Point(30, 162);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(92, 18);
			this.label4.TabIndex = 12;
			this.label4.Text = "Precio Venta";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label5.Location = new System.Drawing.Point(80, 202);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 18);
			this.label5.TabIndex = 13;
			this.label5.Text = "Stock";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label6.Location = new System.Drawing.Point(50, 242);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(77, 18);
			this.label6.TabIndex = 14;
			this.label6.Text = "Proveedor";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label7.Location = new System.Drawing.Point(75, 282);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(50, 18);
			this.label7.TabIndex = 15;
			this.label7.Text = "Marca";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.label8.Location = new System.Drawing.Point(65, 322);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(58, 18);
			this.label8.TabIndex = 16;
			this.label8.Text = "Modelo";
			// 
			// RegisterProductForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.OldLace;
			this.ClientSize = new System.Drawing.Size(500, 450);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.CancelButton);
			this.Controls.Add(this.RegisterButton);
			this.Controls.Add(this.StockBox);
			this.Controls.Add(this.PrecioVentaBox);
			this.Controls.Add(this.PrecioCostoBox);
			this.Controls.Add(this.ModeloBox);
			this.Controls.Add(this.MarcaBox);
			this.Controls.Add(this.ProveedorBox);
			this.Controls.Add(this.NombreBox);
			this.Controls.Add(this.label1);
			this.Name = "RegisterProductForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Registro Rápido de Producto";
			this.Load += new System.EventHandler(this.RegisterProductForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.PrecioCostoBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PrecioVentaBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StockBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox NombreBox;
        private System.Windows.Forms.ComboBox ProveedorBox;
        private System.Windows.Forms.ComboBox MarcaBox;
        private System.Windows.Forms.ComboBox ModeloBox;
        private System.Windows.Forms.NumericUpDown PrecioCostoBox;
        private System.Windows.Forms.NumericUpDown PrecioVentaBox;
        private System.Windows.Forms.NumericUpDown StockBox;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
