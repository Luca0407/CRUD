namespace Simple_Login_FORM
{
    partial class RegisterClientForm
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
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.SurnameBox = new System.Windows.Forms.TextBox();
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.PhoneBox = new System.Windows.Forms.TextBox();
            this.DomBox = new System.Windows.Forms.TextBox();
            this.DNIBox = new System.Windows.Forms.TextBox();
            this.CUILBox = new System.Windows.Forms.TextBox();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(120, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registro Rápido Cliente";
            // 
            // UsernameBox
            // 
            this.UsernameBox.ForeColor = System.Drawing.Color.DarkGray;
            this.UsernameBox.Location = new System.Drawing.Point(150, 80);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(280, 22);
            this.UsernameBox.TabIndex = 1;
            this.UsernameBox.Text = "nombre";
            this.UsernameBox.Enter += new System.EventHandler(this.UsernameBox_Enter);
            this.UsernameBox.Leave += new System.EventHandler(this.UsernameBox_Leave);
            // 
            // SurnameBox
            // 
            this.SurnameBox.ForeColor = System.Drawing.Color.DarkGray;
            this.SurnameBox.Location = new System.Drawing.Point(150, 120);
            this.SurnameBox.Name = "SurnameBox";
            this.SurnameBox.Size = new System.Drawing.Size(280, 22);
            this.SurnameBox.TabIndex = 2;
            this.SurnameBox.Text = "apellido";
            this.SurnameBox.Enter += new System.EventHandler(this.SurnameBox_Enter);
            this.SurnameBox.Leave += new System.EventHandler(this.SurnameBox_Leave);
            // 
            // EmailBox
            // 
            this.EmailBox.ForeColor = System.Drawing.Color.DarkGray;
            this.EmailBox.Location = new System.Drawing.Point(150, 160);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(280, 22);
            this.EmailBox.TabIndex = 3;
            this.EmailBox.Text = "correo@mail.com";
            this.EmailBox.Enter += new System.EventHandler(this.EmailBox_Enter);
            this.EmailBox.Leave += new System.EventHandler(this.EmailBox_Leave);
            // 
            // PhoneBox
            // 
            this.PhoneBox.ForeColor = System.Drawing.Color.DarkGray;
            this.PhoneBox.Location = new System.Drawing.Point(150, 200);
            this.PhoneBox.Name = "PhoneBox";
            this.PhoneBox.Size = new System.Drawing.Size(280, 22);
            this.PhoneBox.TabIndex = 4;
            this.PhoneBox.Text = "número de 10 digitos";
            this.PhoneBox.Enter += new System.EventHandler(this.PhoneBox_Enter);
            this.PhoneBox.Leave += new System.EventHandler(this.PhoneBox_Leave);
            // 
            // DomBox
            // 
            this.DomBox.ForeColor = System.Drawing.Color.DarkGray;
            this.DomBox.Location = new System.Drawing.Point(150, 240);
            this.DomBox.Name = "DomBox";
            this.DomBox.Size = new System.Drawing.Size(280, 22);
            this.DomBox.TabIndex = 5;
            this.DomBox.Text = "domicilio";
            this.DomBox.Enter += new System.EventHandler(this.DomBox_Enter);
            this.DomBox.Leave += new System.EventHandler(this.DomBox_Leave);
            // 
            // DNIBox
            // 
            this.DNIBox.ForeColor = System.Drawing.Color.DarkGray;
            this.DNIBox.Location = new System.Drawing.Point(150, 280);
            this.DNIBox.Name = "DNIBox";
            this.DNIBox.Size = new System.Drawing.Size(280, 22);
            this.DNIBox.TabIndex = 6;
            this.DNIBox.Text = "DNI sin puntos";
            this.DNIBox.Enter += new System.EventHandler(this.DNIBox_Enter);
            this.DNIBox.Leave += new System.EventHandler(this.DNIBox_Leave);
            // 
            // CUILBox
            // 
            this.CUILBox.ForeColor = System.Drawing.Color.DarkGray;
            this.CUILBox.Location = new System.Drawing.Point(150, 320);
            this.CUILBox.Name = "CUILBox";
            this.CUILBox.Size = new System.Drawing.Size(280, 22);
            this.CUILBox.TabIndex = 7;
            this.CUILBox.Text = "CUIL sin guiones";
            this.CUILBox.Enter += new System.EventHandler(this.CUILBox_Enter);
            this.CUILBox.Leave += new System.EventHandler(this.CUILBox_Leave);
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
            this.RegisterButton.Location = new System.Drawing.Point(120, 370);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(120, 40);
            this.RegisterButton.TabIndex = 8;
            this.RegisterButton.Text = "Registrar";
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
            this.CancelButton.Location = new System.Drawing.Point(260, 370);
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
            this.label3.Location = new System.Drawing.Point(70, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Apellido";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(70, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "E-mail";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(70, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 18);
            this.label5.TabIndex = 13;
            this.label5.Text = "Teléfono";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(70, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 18);
            this.label6.TabIndex = 14;
            this.label6.Text = "Domicilio";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.Location = new System.Drawing.Point(70, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 18);
            this.label7.TabIndex = 15;
            this.label7.Text = "DNI";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.Location = new System.Drawing.Point(70, 322);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 18);
            this.label8.TabIndex = 16;
            this.label8.Text = "CUIL";
            // 
            // RegisterClientForm
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
            this.Controls.Add(this.CUILBox);
            this.Controls.Add(this.DNIBox);
            this.Controls.Add(this.DomBox);
            this.Controls.Add(this.PhoneBox);
            this.Controls.Add(this.EmailBox);
            this.Controls.Add(this.SurnameBox);
            this.Controls.Add(this.UsernameBox);
            this.Controls.Add(this.label1);
            this.Name = "RegisterClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro Rápido de Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.TextBox SurnameBox;
        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.TextBox PhoneBox;
        private System.Windows.Forms.TextBox DomBox;
        private System.Windows.Forms.TextBox DNIBox;
        private System.Windows.Forms.TextBox CUILBox;
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
