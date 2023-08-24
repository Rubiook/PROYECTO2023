namespace PRESENTACION.PRESENTACION
{
    partial class LotesAsignadosAlRemate
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LotesAsignadosAlRemate));
            dataGridViewLotesAsignados = new DataGridView();
            btnDesasignarLote = new Button();
            lblRemate = new Label();
            button1 = new Button();
            buttonVenderLote = new Button();
            textBoxComprador = new TextBox();
            labelComprador = new Label();
            buttonQuitarLoteDeVendidos = new Button();
            textBoxPrecioDeVenta = new TextBox();
            labelPrecioDeVenta = new Label();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dataGridViewLotesAsignados).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewLotesAsignados
            // 
            dataGridViewLotesAsignados.AllowUserToAddRows = false;
            dataGridViewLotesAsignados.AllowUserToDeleteRows = false;
            dataGridViewLotesAsignados.AllowUserToResizeColumns = false;
            dataGridViewLotesAsignados.AllowUserToResizeRows = false;
            dataGridViewLotesAsignados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLotesAsignados.BackgroundColor = Color.FromArgb(3, 6, 3);
            dataGridViewLotesAsignados.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewLotesAsignados.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewLotesAsignados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewLotesAsignados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewLotesAsignados.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewLotesAsignados.Location = new Point(110, 155);
            dataGridViewLotesAsignados.MultiSelect = false;
            dataGridViewLotesAsignados.Name = "dataGridViewLotesAsignados";
            dataGridViewLotesAsignados.ReadOnly = true;
            dataGridViewLotesAsignados.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewLotesAsignados.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewLotesAsignados.RowTemplate.Height = 27;
            dataGridViewLotesAsignados.Size = new Size(982, 358);
            dataGridViewLotesAsignados.TabIndex = 23;
            dataGridViewLotesAsignados.CellClick += dataGridViewLotesAsignados_CellClick;
            dataGridViewLotesAsignados.CellContentClick += dataGridViewLotesAsignados_CellContentClick;
            // 
            // btnDesasignarLote
            // 
            btnDesasignarLote.BackColor = Color.Transparent;
            btnDesasignarLote.BackgroundImage = (Image)resources.GetObject("btnDesasignarLote.BackgroundImage");
            btnDesasignarLote.BackgroundImageLayout = ImageLayout.Zoom;
            btnDesasignarLote.Cursor = Cursors.Hand;
            btnDesasignarLote.FlatAppearance.BorderColor = Color.FromArgb(0, 5, 25, 255);
            btnDesasignarLote.FlatAppearance.BorderSize = 0;
            btnDesasignarLote.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnDesasignarLote.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnDesasignarLote.FlatStyle = FlatStyle.Flat;
            btnDesasignarLote.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            btnDesasignarLote.ForeColor = Color.White;
            btnDesasignarLote.Location = new Point(144, 543);
            btnDesasignarLote.Name = "btnDesasignarLote";
            btnDesasignarLote.Size = new Size(82, 76);
            btnDesasignarLote.TabIndex = 53;
            toolTip1.SetToolTip(btnDesasignarLote, "Desasignar Lote");
            btnDesasignarLote.UseVisualStyleBackColor = false;
            btnDesasignarLote.Click += buttonDesasignarLote_Click;
            // 
            // lblRemate
            // 
            lblRemate.AutoSize = true;
            lblRemate.BackColor = Color.Transparent;
            lblRemate.BorderStyle = BorderStyle.Fixed3D;
            lblRemate.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            lblRemate.ForeColor = Color.White;
            lblRemate.Location = new Point(244, 61);
            lblRemate.Name = "lblRemate";
            lblRemate.Size = new Size(2, 56);
            lblRemate.TabIndex = 54;
            lblRemate.Click += lblRemate_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.GreenYellow;
            button1.FlatAppearance.MouseDownBackColor = Color.Red;
            button1.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.White;
            button1.Location = new Point(1060, 590);
            button1.Name = "button1";
            button1.Size = new Size(130, 50);
            button1.TabIndex = 55;
            button1.Text = "Regresar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
            // 
            // buttonVenderLote
            // 
            buttonVenderLote.BackColor = Color.Transparent;
            buttonVenderLote.BackgroundImage = (Image)resources.GetObject("buttonVenderLote.BackgroundImage");
            buttonVenderLote.BackgroundImageLayout = ImageLayout.Zoom;
            buttonVenderLote.Cursor = Cursors.Hand;
            buttonVenderLote.FlatAppearance.BorderColor = Color.FromArgb(0, 5, 25, 255);
            buttonVenderLote.FlatAppearance.BorderSize = 0;
            buttonVenderLote.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonVenderLote.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonVenderLote.FlatStyle = FlatStyle.Flat;
            buttonVenderLote.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            buttonVenderLote.ForeColor = Color.White;
            buttonVenderLote.Location = new Point(788, 542);
            buttonVenderLote.Name = "buttonVenderLote";
            buttonVenderLote.Size = new Size(81, 80);
            buttonVenderLote.TabIndex = 56;
            buttonVenderLote.TextImageRelation = TextImageRelation.ImageAboveText;
            toolTip1.SetToolTip(buttonVenderLote, "Marcar Lote como Vendido");
            buttonVenderLote.UseVisualStyleBackColor = false;
            buttonVenderLote.Click += button2_Click_1;
            // 
            // textBoxComprador
            // 
            textBoxComprador.BackColor = Color.FromArgb(3, 6, 3);
            textBoxComprador.BorderStyle = BorderStyle.FixedSingle;
            textBoxComprador.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxComprador.ForeColor = SystemColors.Window;
            textBoxComprador.Location = new Point(525, 545);
            textBoxComprador.Name = "textBoxComprador";
            textBoxComprador.Size = new Size(230, 32);
            textBoxComprador.TabIndex = 58;
            // 
            // labelComprador
            // 
            labelComprador.AutoSize = true;
            labelComprador.BackColor = Color.Transparent;
            labelComprador.BorderStyle = BorderStyle.Fixed3D;
            labelComprador.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            labelComprador.ForeColor = Color.White;
            labelComprador.Location = new Point(373, 545);
            labelComprador.Name = "labelComprador";
            labelComprador.Size = new Size(146, 32);
            labelComprador.TabIndex = 57;
            labelComprador.Text = "Comprador: ";
            // 
            // buttonQuitarLoteDeVendidos
            // 
            buttonQuitarLoteDeVendidos.BackColor = Color.Transparent;
            buttonQuitarLoteDeVendidos.BackgroundImage = (Image)resources.GetObject("buttonQuitarLoteDeVendidos.BackgroundImage");
            buttonQuitarLoteDeVendidos.BackgroundImageLayout = ImageLayout.Zoom;
            buttonQuitarLoteDeVendidos.Cursor = Cursors.Hand;
            buttonQuitarLoteDeVendidos.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            buttonQuitarLoteDeVendidos.FlatAppearance.BorderSize = 0;
            buttonQuitarLoteDeVendidos.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            buttonQuitarLoteDeVendidos.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 0, 0);
            buttonQuitarLoteDeVendidos.FlatStyle = FlatStyle.Flat;
            buttonQuitarLoteDeVendidos.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            buttonQuitarLoteDeVendidos.ForeColor = Color.White;
            buttonQuitarLoteDeVendidos.Location = new Point(894, 551);
            buttonQuitarLoteDeVendidos.Name = "buttonQuitarLoteDeVendidos";
            buttonQuitarLoteDeVendidos.Size = new Size(65, 68);
            buttonQuitarLoteDeVendidos.TabIndex = 59;
            buttonQuitarLoteDeVendidos.TextImageRelation = TextImageRelation.ImageAboveText;
            toolTip1.SetToolTip(buttonQuitarLoteDeVendidos, "Quitar lote de Vendido");
            buttonQuitarLoteDeVendidos.UseVisualStyleBackColor = false;
            buttonQuitarLoteDeVendidos.Click += button3_Click_1;
            // 
            // textBoxPrecioDeVenta
            // 
            textBoxPrecioDeVenta.BackColor = Color.FromArgb(3, 6, 3);
            textBoxPrecioDeVenta.BorderStyle = BorderStyle.FixedSingle;
            textBoxPrecioDeVenta.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxPrecioDeVenta.ForeColor = SystemColors.Window;
            textBoxPrecioDeVenta.Location = new Point(567, 585);
            textBoxPrecioDeVenta.Name = "textBoxPrecioDeVenta";
            textBoxPrecioDeVenta.Size = new Size(188, 32);
            textBoxPrecioDeVenta.TabIndex = 61;
            textBoxPrecioDeVenta.TextChanged += textBoxPrecioDeVenta_TextChanged;
            // 
            // labelPrecioDeVenta
            // 
            labelPrecioDeVenta.AutoSize = true;
            labelPrecioDeVenta.BackColor = Color.Transparent;
            labelPrecioDeVenta.BorderStyle = BorderStyle.Fixed3D;
            labelPrecioDeVenta.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            labelPrecioDeVenta.ForeColor = Color.White;
            labelPrecioDeVenta.Location = new Point(373, 583);
            labelPrecioDeVenta.Name = "labelPrecioDeVenta";
            labelPrecioDeVenta.Size = new Size(188, 32);
            labelPrecioDeVenta.TabIndex = 60;
            labelPrecioDeVenta.Text = "Precio de venta: ";
            // 
            // LotesAsignadosAlRemate
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1202, 652);
            Controls.Add(textBoxPrecioDeVenta);
            Controls.Add(labelPrecioDeVenta);
            Controls.Add(buttonQuitarLoteDeVendidos);
            Controls.Add(textBoxComprador);
            Controls.Add(labelComprador);
            Controls.Add(buttonVenderLote);
            Controls.Add(button1);
            Controls.Add(lblRemate);
            Controls.Add(btnDesasignarLote);
            Controls.Add(dataGridViewLotesAsignados);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LotesAsignadosAlRemate";
            Text = "Lotes Asignados";
            FormClosing += VentanaLotesAsignados_FormClosing;
            Load += VentanaLotesAsignados_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewLotesAsignados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public DataGridView dataGridViewLotesAsignados;
        private Button btnDesasignarLote;
        private Label lblRemate;
        private Button button1;
        private Button buttonVenderLote;
        private TextBox textBoxComprador;
        private Label labelComprador;
        private Button buttonQuitarLoteDeVendidos;
        private TextBox textBoxPrecioDeVenta;
        private Label labelPrecioDeVenta;
        private ToolTip toolTip1;
    }
}