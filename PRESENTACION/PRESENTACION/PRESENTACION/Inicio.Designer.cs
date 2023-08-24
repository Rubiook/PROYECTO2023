namespace PRESENTACION
{
    partial class Ventana1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventana1));
            label5 = new Label();
            lblRemate = new Label();
            mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            button1 = new Button();
            panel1 = new Panel();
            button10 = new Button();
            button7 = new Button();
            label3 = new Label();
            button3 = new Button();
            label2 = new Label();
            button9 = new Button();
            button8 = new Button();
            button2 = new Button();
            label1 = new Label();
            button6 = new Button();
            button5 = new Button();
            buttonSoporte = new Button();
            timerOpenMenu = new System.Windows.Forms.Timer(components);
            timerCloseMenu = new System.Windows.Forms.Timer(components);
            button4 = new Button();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.LightGray;
            label5.Location = new Point(49, 156);
            label5.Name = "label5";
            label5.Size = new Size(14, 21);
            label5.TabIndex = 17;
            label5.Text = " ";
            label5.Click += label5_Click;
            // 
            // lblRemate
            // 
            lblRemate.AutoSize = true;
            lblRemate.BackColor = Color.Transparent;
            lblRemate.BorderStyle = BorderStyle.Fixed3D;
            lblRemate.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            lblRemate.ForeColor = Color.White;
            lblRemate.Location = new Point(348, 23);
            lblRemate.Name = "lblRemate";
            lblRemate.Size = new Size(2, 56);
            lblRemate.TabIndex = 55;
            lblRemate.Click += lblRemate_Click;
            // 
            // mySqlCommand1
            // 
            mySqlCommand1.CacheAge = 0;
            mySqlCommand1.Connection = null;
            mySqlCommand1.EnableCaching = false;
            mySqlCommand1.Transaction = null;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 0, 0);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(38, 38);
            button1.TabIndex = 57;
            toolTip1.SetToolTip(button1, "Menú");
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(3, 6, 3);
            panel1.Controls.Add(button10);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(label5);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(312, 828);
            panel1.TabIndex = 58;
            // 
            // button10
            // 
            button10.BackColor = Color.Transparent;
            button10.Cursor = Cursors.Hand;
            button10.FlatAppearance.BorderColor = Color.GreenYellow;
            button10.FlatAppearance.MouseDownBackColor = Color.Red;
            button10.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button10.FlatStyle = FlatStyle.Popup;
            button10.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button10.ForeColor = Color.White;
            button10.Location = new Point(49, 525);
            button10.Name = "button10";
            button10.Size = new Size(220, 67);
            button10.TabIndex = 74;
            button10.Text = "Mantenimineto de Empleados";
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            button10.MouseEnter += button10_MouseEnter;
            button10.MouseLeave += button10_MouseLeave;
            // 
            // button7
            // 
            button7.BackColor = Color.Transparent;
            button7.Cursor = Cursors.Hand;
            button7.FlatAppearance.BorderColor = Color.GreenYellow;
            button7.FlatAppearance.MouseDownBackColor = Color.Red;
            button7.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button7.FlatStyle = FlatStyle.Popup;
            button7.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button7.ForeColor = Color.White;
            button7.Location = new Point(49, 671);
            button7.Name = "button7";
            button7.Size = new Size(220, 42);
            button7.TabIndex = 73;
            button7.Text = "Historial de Ventas";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            button7.MouseEnter += button7_MouseEnter;
            button7.MouseLeave += button7_MouseLeave;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(49, 156);
            label3.Name = "label3";
            label3.Size = new Size(220, 28);
            label3.TabIndex = 72;
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderColor = Color.GreenYellow;
            button3.FlatAppearance.MouseDownBackColor = Color.Red;
            button3.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button3.ForeColor = Color.White;
            button3.Location = new Point(49, 776);
            button3.Name = "button3";
            button3.Size = new Size(220, 32);
            button3.TabIndex = 59;
            button3.Text = "Cerrar sesión";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click_1;
            button3.MouseEnter += button3_MouseEnter_1;
            button3.MouseLeave += button3_MouseLeave_1;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(49, 128);
            label2.Name = "label2";
            label2.Size = new Size(220, 28);
            label2.TabIndex = 71;
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button9
            // 
            button9.BackColor = Color.Transparent;
            button9.Cursor = Cursors.Hand;
            button9.FlatAppearance.BorderColor = Color.GreenYellow;
            button9.FlatAppearance.MouseDownBackColor = Color.Red;
            button9.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button9.FlatStyle = FlatStyle.Popup;
            button9.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button9.ForeColor = Color.White;
            button9.Location = new Point(49, 258);
            button9.Name = "button9";
            button9.Size = new Size(220, 67);
            button9.TabIndex = 69;
            button9.Text = "Ver Próximos Remates";
            button9.UseVisualStyleBackColor = false;
            button9.Click += button9_Click;
            button9.MouseEnter += button9_MouseEnter;
            button9.MouseLeave += button9_MouseLeave;
            // 
            // button8
            // 
            button8.BackColor = Color.Transparent;
            button8.Cursor = Cursors.Hand;
            button8.FlatAppearance.BorderColor = Color.GreenYellow;
            button8.FlatAppearance.MouseDownBackColor = Color.Red;
            button8.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button8.FlatStyle = FlatStyle.Popup;
            button8.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button8.ForeColor = Color.White;
            button8.Location = new Point(49, 331);
            button8.Name = "button8";
            button8.Size = new Size(220, 67);
            button8.TabIndex = 68;
            button8.Text = "Lotes del próximo remate";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            button8.MouseEnter += button8_MouseEnter;
            button8.MouseLeave += button8_MouseLeave;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderColor = Color.GreenYellow;
            button2.FlatAppearance.MouseDownBackColor = Color.Red;
            button2.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = Color.White;
            button2.Location = new Point(49, 598);
            button2.Name = "button2";
            button2.Size = new Size(220, 67);
            button2.TabIndex = 64;
            button2.Text = "Gestión de \r\nLotes y Remates";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click_1;
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.LightGray;
            label1.Location = new Point(62, 126);
            label1.Name = "label1";
            label1.Size = new Size(14, 21);
            label1.TabIndex = 63;
            label1.Text = " ";
            label1.Click += label1_Click;
            // 
            // button6
            // 
            button6.AutoSize = true;
            button6.BackColor = Color.Transparent;
            button6.BackgroundImage = (Image)resources.GetObject("button6.BackgroundImage");
            button6.BackgroundImageLayout = ImageLayout.Zoom;
            button6.Cursor = Cursors.Hand;
            button6.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button6.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 0, 0);
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            button6.ForeColor = Color.White;
            button6.Location = new Point(122, 37);
            button6.Name = "button6";
            button6.Size = new Size(75, 75);
            button6.TabIndex = 59;
            button6.TextImageRelation = TextImageRelation.ImageAboveText;
            toolTip1.SetToolTip(button6, "Perfil de Usuario");
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            button6.KeyPress += button6_KeyPress;
            button6.MouseEnter += button6_MouseEnter;
            button6.MouseLeave += button6_MouseLeave;
            // 
            // button5
            // 
            button5.BackColor = Color.Transparent;
            button5.Cursor = Cursors.Hand;
            button5.FlatAppearance.BorderColor = Color.GreenYellow;
            button5.FlatAppearance.MouseDownBackColor = Color.Red;
            button5.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button5.FlatStyle = FlatStyle.Popup;
            button5.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button5.ForeColor = Color.White;
            button5.Location = new Point(49, 452);
            button5.Name = "button5";
            button5.Size = new Size(220, 67);
            button5.TabIndex = 62;
            button5.Text = "Mantenimiento\r\nDe Usuarios";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            button5.MouseEnter += button5_MouseEnter;
            button5.MouseLeave += button5_MouseLeave;
            // 
            // buttonSoporte
            // 
            buttonSoporte.AutoSize = true;
            buttonSoporte.BackColor = Color.Transparent;
            buttonSoporte.BackgroundImage = (Image)resources.GetObject("buttonSoporte.BackgroundImage");
            buttonSoporte.BackgroundImageLayout = ImageLayout.Zoom;
            buttonSoporte.Cursor = Cursors.Hand;
            buttonSoporte.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            buttonSoporte.FlatAppearance.BorderSize = 0;
            buttonSoporte.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            buttonSoporte.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 0, 0);
            buttonSoporte.FlatStyle = FlatStyle.Flat;
            buttonSoporte.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSoporte.ForeColor = Color.White;
            buttonSoporte.Location = new Point(1186, 12);
            buttonSoporte.Name = "buttonSoporte";
            buttonSoporte.Size = new Size(57, 50);
            buttonSoporte.TabIndex = 73;
            buttonSoporte.TextImageRelation = TextImageRelation.ImageAboveText;
            toolTip1.SetToolTip(buttonSoporte, "Soporte Técnico");
            buttonSoporte.UseVisualStyleBackColor = false;
            buttonSoporte.Click += buttonSoporte_Click;
            // 
            // timerOpenMenu
            // 
            timerOpenMenu.Tick += timerOpenMenu_Tick;
            // 
            // timerCloseMenu
            // 
            timerCloseMenu.Tick += timerCloseMenu_Tick;
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.Cursor = Cursors.Hand;
            button4.FlatAppearance.BorderColor = Color.GreenYellow;
            button4.FlatAppearance.MouseDownBackColor = Color.Red;
            button4.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button4.FlatStyle = FlatStyle.Popup;
            button4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            button4.ForeColor = Color.White;
            button4.Location = new Point(1131, 763);
            button4.Name = "button4";
            button4.Size = new Size(112, 51);
            button4.TabIndex = 60;
            button4.Text = "Salir";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            button4.MouseEnter += button4_MouseEnter;
            button4.MouseLeave += button4_MouseLeave;
            // 
            // Ventana1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1255, 827);
            Controls.Add(button4);
            Controls.Add(panel1);
            Controls.Add(lblRemate);
            Controls.Add(buttonSoporte);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Ventana1";
            Text = "Inicio";
            Load += Ventana1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label5;
        private Label lblRemate;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private Button button1;
        private Panel panel1;
        private Button button5;
        private System.Windows.Forms.Timer timerOpenMenu;
        private System.Windows.Forms.Timer timerCloseMenu;
        private Button button6;
        private Label label1;
        private Button button2;
        private Button button9;
        private Button button8;
        private Label label3;
        private Label label2;
        private Button button3;
        private Button button4;
        private ToolTip toolTip1;
        private Button buttonSoporte;
        private Button button10;
        private Button button7;
    }
}