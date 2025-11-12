namespace Simple_Login_FORM
{
    partial class productos
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			this.Products = new System.Windows.Forms.TabControl();
			this.devices = new System.Windows.Forms.TabPage();
			this.DGVdisp = new System.Windows.Forms.DataGridView();
			this.repuest = new System.Windows.Forms.TabPage();
			this.DGVrep = new System.Windows.Forms.DataGridView();
			this.misc = new System.Windows.Forms.TabPage();
			this.DGVacc = new System.Windows.Forms.DataGridView();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Estado = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.Filtro = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ModBox = new System.Windows.Forms.ComboBox();
			this.BrandBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.Stock = new System.Windows.Forms.NumericUpDown();
			this.Products.SuspendLayout();
			this.devices.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DGVdisp)).BeginInit();
			this.repuest.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DGVrep)).BeginInit();
			this.misc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DGVacc)).BeginInit();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Stock)).BeginInit();
			this.SuspendLayout();
			// 
			// Products
			// 
			this.Products.Controls.Add(this.devices);
			this.Products.Controls.Add(this.repuest);
			this.Products.Controls.Add(this.misc);
			this.Products.Controls.Add(this.tabPage1);
			this.Products.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Products.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
			this.Products.Location = new System.Drawing.Point(24, 158);
			this.Products.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Products.Name = "Products";
			this.Products.SelectedIndex = 0;
			this.Products.Size = new System.Drawing.Size(1229, 537);
			this.Products.TabIndex = 0;
			this.Products.Tag = "";
			// 
			// devices
			// 
			this.devices.AutoScroll = true;
			this.devices.Controls.Add(this.DGVdisp);
			this.devices.Location = new System.Drawing.Point(4, 27);
			this.devices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.devices.Name = "devices";
			this.devices.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.devices.Size = new System.Drawing.Size(1221, 506);
			this.devices.TabIndex = 0;
			this.devices.Text = "Dispositivos";
			// 
			// DGVdisp
			// 
			this.DGVdisp.AllowUserToAddRows = false;
			this.DGVdisp.AllowUserToDeleteRows = false;
			this.DGVdisp.AllowUserToResizeColumns = false;
			this.DGVdisp.AllowUserToResizeRows = false;
			this.DGVdisp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGVdisp.BackgroundColor = System.Drawing.Color.OldLace;
			this.DGVdisp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DGVdisp.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
			this.DGVdisp.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DGVdisp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.DGVdisp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGVdisp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.DGVdisp.Location = new System.Drawing.Point(0, 0);
			this.DGVdisp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.DGVdisp.Name = "DGVdisp";
			this.DGVdisp.ReadOnly = true;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DGVdisp.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
			this.DGVdisp.RowHeadersWidth = 51;
			this.DGVdisp.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.DGVdisp.RowTemplate.Height = 24;
			this.DGVdisp.Size = new System.Drawing.Size(1216, 501);
			this.DGVdisp.TabIndex = 0;
			this.DGVdisp.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVdisp_CellContentClick);
			// 
			// repuest
			// 
			this.repuest.Controls.Add(this.DGVrep);
			this.repuest.Location = new System.Drawing.Point(4, 27);
			this.repuest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.repuest.Name = "repuest";
			this.repuest.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.repuest.Size = new System.Drawing.Size(1221, 506);
			this.repuest.TabIndex = 1;
			this.repuest.Text = "Repuestos";
			this.repuest.Click += new System.EventHandler(this.tabPage2_Click);
			// 
			// DGVrep
			// 
			this.DGVrep.AllowUserToAddRows = false;
			this.DGVrep.AllowUserToDeleteRows = false;
			this.DGVrep.AllowUserToResizeColumns = false;
			this.DGVrep.AllowUserToResizeRows = false;
			this.DGVrep.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGVrep.BackgroundColor = System.Drawing.Color.OldLace;
			this.DGVrep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DGVrep.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
			this.DGVrep.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DGVrep.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.DGVrep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGVrep.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.DGVrep.Location = new System.Drawing.Point(0, 0);
			this.DGVrep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.DGVrep.Name = "DGVrep";
			this.DGVrep.ReadOnly = true;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DGVrep.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.DGVrep.RowHeadersWidth = 51;
			this.DGVrep.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.DGVrep.RowTemplate.Height = 24;
			this.DGVrep.Size = new System.Drawing.Size(1216, 501);
			this.DGVrep.TabIndex = 0;
			// 
			// misc
			// 
			this.misc.BackColor = System.Drawing.Color.OldLace;
			this.misc.Controls.Add(this.DGVacc);
			this.misc.Location = new System.Drawing.Point(4, 27);
			this.misc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.misc.Name = "misc";
			this.misc.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.misc.Size = new System.Drawing.Size(1221, 506);
			this.misc.TabIndex = 2;
			this.misc.Text = "Accesorios";
			this.misc.Click += new System.EventHandler(this.tabPage3_Click);
			// 
			// DGVacc
			// 
			this.DGVacc.AllowUserToAddRows = false;
			this.DGVacc.AllowUserToDeleteRows = false;
			this.DGVacc.AllowUserToResizeColumns = false;
			this.DGVacc.AllowUserToResizeRows = false;
			this.DGVacc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGVacc.BackgroundColor = System.Drawing.Color.OldLace;
			this.DGVacc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.DGVacc.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
			this.DGVacc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DGVacc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
			this.DGVacc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGVacc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.DGVacc.Location = new System.Drawing.Point(0, 0);
			this.DGVacc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.DGVacc.Name = "DGVacc";
			this.DGVacc.ReadOnly = true;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DGVacc.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			this.DGVacc.RowHeadersWidth = 51;
			this.DGVacc.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.DGVacc.RowTemplate.Height = 24;
			this.DGVacc.Size = new System.Drawing.Size(1216, 501);
			this.DGVacc.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.OldLace;
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.label9);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.numericUpDown1);
			this.tabPage1.Controls.Add(this.button4);
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.textBox5);
			this.tabPage1.Controls.Add(this.textBox4);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.dataGridView1);
			this.tabPage1.Controls.Add(this.textBox3);
			this.tabPage1.Controls.Add(this.textBox2);
			this.tabPage1.Controls.Add(this.textBox1);
			this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabPage1.Location = new System.Drawing.Point(4, 27);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage1.Size = new System.Drawing.Size(1221, 506);
			this.tabPage1.TabIndex = 3;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(1017, 23);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(66, 18);
			this.label10.TabIndex = 14;
			this.label10.Text = "Cantidad";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(772, 23);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(51, 18);
			this.label9.TabIndex = 13;
			this.label9.Text = "Precio";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(556, 18);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(69, 18);
			this.label8.TabIndex = 12;
			this.label8.Text = "Producto";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(319, 18);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(105, 18);
			this.label7.TabIndex = 11;
			this.label7.Text = "Cod. Producto";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 86);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(111, 18);
			this.label6.TabIndex = 10;
			this.label6.Text = "Nombre Cliente";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(1021, 49);
			this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(69, 24);
			this.numericUpDown1.TabIndex = 9;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(481, 41);
			this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(48, 34);
			this.button4.TabIndex = 8;
			this.button4.Text = "button4";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(173, 37);
			this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(48, 34);
			this.button3.TabIndex = 7;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(323, 46);
			this.textBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(151, 24);
			this.textBox5.TabIndex = 6;
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(4, 108);
			this.textBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(159, 24);
			this.textBox4.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 18);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(107, 18);
			this.label5.TabIndex = 4;
			this.label5.Text = "N° Documento";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Producto,
            this.Precio,
            this.Cantidad,
            this.SubTotal,
            this.Estado});
			this.dataGridView1.Location = new System.Drawing.Point(8, 159);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.Size = new System.Drawing.Size(1211, 336);
			this.dataGridView1.TabIndex = 3;
			// 
			// ID
			// 
			this.ID.HeaderText = "ID";
			this.ID.MinimumWidth = 6;
			this.ID.Name = "ID";
			// 
			// Producto
			// 
			this.Producto.HeaderText = "Producto";
			this.Producto.MinimumWidth = 6;
			this.Producto.Name = "Producto";
			// 
			// Precio
			// 
			this.Precio.HeaderText = "Precio";
			this.Precio.MinimumWidth = 6;
			this.Precio.Name = "Precio";
			// 
			// Cantidad
			// 
			this.Cantidad.HeaderText = "Cantidad";
			this.Cantidad.MinimumWidth = 6;
			this.Cantidad.Name = "Cantidad";
			// 
			// SubTotal
			// 
			this.SubTotal.HeaderText = "SubTotal";
			this.SubTotal.MinimumWidth = 6;
			this.SubTotal.Name = "SubTotal";
			// 
			// Estado
			// 
			this.Estado.HeaderText = "Estado";
			this.Estado.Items.AddRange(new object[] {
            "Completado",
            "En Proceso",
            "Rechazado"});
			this.Estado.MinimumWidth = 6;
			this.Estado.Name = "Estado";
			this.Estado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(776, 46);
			this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(185, 24);
			this.textBox3.TabIndex = 2;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(560, 46);
			this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(181, 24);
			this.textBox2.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(4, 42);
			this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(159, 24);
			this.textBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FloralWhite;
			this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
			this.button1.FlatAppearance.BorderSize = 2;
			this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(1091, 121);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(164, 46);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = false;
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.FloralWhite;
			this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button2.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
			this.button2.FlatAppearance.BorderSize = 2;
			this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(1091, 66);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(164, 46);
			this.button2.TabIndex = 2;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = false;
			// 
			// editButton
			// 
			this.editButton.BackColor = System.Drawing.Color.FloralWhite;
			this.editButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.editButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
			this.editButton.FlatAppearance.BorderSize = 2;
			this.editButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.editButton.Location = new System.Drawing.Point(1091, 14);
			this.editButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(164, 46);
			this.editButton.TabIndex = 3;
			this.editButton.Text = "Modificar producto";
			this.editButton.UseVisualStyleBackColor = false;
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// Filtro
			// 
			this.Filtro.BackColor = System.Drawing.Color.FloralWhite;
			this.Filtro.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Filtro.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
			this.Filtro.FlatAppearance.BorderSize = 2;
			this.Filtro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCyan;
			this.Filtro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Filtro.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
			this.Filtro.Location = new System.Drawing.Point(687, 62);
			this.Filtro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Filtro.Name = "Filtro";
			this.Filtro.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Filtro.Size = new System.Drawing.Size(55, 55);
			this.Filtro.TabIndex = 5;
			this.Filtro.Text = "🔍";
			this.Filtro.UseVisualStyleBackColor = false;
			this.Filtro.Click += new System.EventHandler(this.Filtro_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.Location = new System.Drawing.Point(25, 31);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 20);
			this.label1.TabIndex = 6;
			this.label1.Text = "Filtrar por:";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.Location = new System.Drawing.Point(269, 82);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 20);
			this.label2.TabIndex = 7;
			this.label2.Text = "Modelo";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label3.Location = new System.Drawing.Point(56, 82);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 20);
			this.label3.TabIndex = 8;
			this.label3.Text = "Marca";
			// 
			// ModBox
			// 
			this.ModBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ModBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ModBox.FormattingEnabled = true;
			this.ModBox.Location = new System.Drawing.Point(352, 79);
			this.ModBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ModBox.Name = "ModBox";
			this.ModBox.Size = new System.Drawing.Size(121, 24);
			this.ModBox.TabIndex = 10;
			// 
			// BrandBox
			// 
			this.BrandBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BrandBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BrandBox.FormattingEnabled = true;
			this.BrandBox.Items.AddRange(new object[] {
            "--- Seleccione la Marca ---"});
			this.BrandBox.Location = new System.Drawing.Point(128, 79);
			this.BrandBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.BrandBox.Name = "BrandBox";
			this.BrandBox.Size = new System.Drawing.Size(121, 24);
			this.BrandBox.TabIndex = 11;
			this.BrandBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label4.Location = new System.Drawing.Point(501, 79);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 20);
			this.label4.TabIndex = 12;
			this.label4.Text = "Stock";
			// 
			// Stock
			// 
			this.Stock.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Stock.Location = new System.Drawing.Point(572, 81);
			this.Stock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Stock.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.Stock.Name = "Stock";
			this.Stock.Size = new System.Drawing.Size(63, 22);
			this.Stock.TabIndex = 13;
			this.Stock.ValueChanged += new System.EventHandler(this.Stock_ValueChanged);
			// 
			// productos
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.OldLace;
			this.ClientSize = new System.Drawing.Size(1280, 720);
			this.Controls.Add(this.Stock);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.BrandBox);
			this.Controls.Add(this.ModBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Filtro);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.Products);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "productos";
			this.Text = "productos";
			this.Load += new System.EventHandler(this.productos_Load);
			this.Products.ResumeLayout(false);
			this.devices.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DGVdisp)).EndInit();
			this.repuest.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DGVrep)).EndInit();
			this.misc.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DGVacc)).EndInit();
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Stock)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Products;
        private System.Windows.Forms.TabPage devices;
        private System.Windows.Forms.TabPage repuest;
        private System.Windows.Forms.TabPage misc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button Filtro;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView DGVdisp;
		private System.Windows.Forms.DataGridView DGVrep;
		private System.Windows.Forms.DataGridView DGVacc;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox ModBox;
		private System.Windows.Forms.ComboBox BrandBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown Stock;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
        private System.Windows.Forms.DataGridViewComboBoxColumn Estado;
    }
}