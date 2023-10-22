namespace PRESENTACION.PRESENTACION
{
    partial class HistorialVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistorialVentas));
            button1 = new Button();
            dataGridViewHistorial = new DataGridView();
            label3 = new Label();
            button5 = new Button();
            buttonPdfFactura = new Button();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorial).BeginInit();
            SuspendLayout();
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
            button1.Location = new Point(1374, 602);
            button1.Name = "button1";
            button1.Size = new Size(125, 50);
            button1.TabIndex = 57;
            button1.Text = "Regresar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
            // 
            // dataGridViewHistorial
            // 
            dataGridViewHistorial.AllowUserToAddRows = false;
            dataGridViewHistorial.AllowUserToDeleteRows = false;
            dataGridViewHistorial.AllowUserToResizeColumns = false;
            dataGridViewHistorial.AllowUserToResizeRows = false;
            dataGridViewHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewHistorial.BackgroundColor = Color.FromArgb(3, 6, 3);
            dataGridViewHistorial.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewHistorial.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewHistorial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewHistorial.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewHistorial.Location = new Point(41, 150);
            dataGridViewHistorial.MultiSelect = false;
            dataGridViewHistorial.Name = "dataGridViewHistorial";
            dataGridViewHistorial.ReadOnly = true;
            dataGridViewHistorial.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewHistorial.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewHistorial.RowTemplate.Height = 27;
            dataGridViewHistorial.Size = new Size(1429, 409);
            dataGridViewHistorial.TabIndex = 58;
            dataGridViewHistorial.CellContentClick += dataGridViewLotesAsignados_CellContentClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Font = new Font("Segoe UI", 33F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(506, 48);
            label3.Name = "label3";
            label3.Size = new Size(499, 62);
            label3.TabIndex = 59;
            label3.Text = "HISTORIAL DE VENTAS";
            // 
            // button5
            // 
            button5.BackColor = Color.Transparent;
            button5.BackgroundImage = (Image)resources.GetObject("button5.BackgroundImage");
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.Cursor = Cursors.Hand;
            button5.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 0, 0);
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            button5.ForeColor = Color.White;
            button5.Location = new Point(12, 12);
            button5.Name = "button5";
            button5.Size = new Size(49, 47);
            button5.TabIndex = 60;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // buttonPdfFactura
            // 
            buttonPdfFactura.BackColor = Color.Transparent;
            buttonPdfFactura.BackgroundImage = (Image)resources.GetObject("buttonPdfFactura.BackgroundImage");
            buttonPdfFactura.BackgroundImageLayout = ImageLayout.Zoom;
            buttonPdfFactura.Cursor = Cursors.Hand;
            buttonPdfFactura.FlatAppearance.BorderColor = Color.FromArgb(0, 5, 25, 255);
            buttonPdfFactura.FlatAppearance.BorderSize = 0;
            buttonPdfFactura.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonPdfFactura.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonPdfFactura.FlatStyle = FlatStyle.Flat;
            buttonPdfFactura.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            buttonPdfFactura.ForeColor = Color.White;
            buttonPdfFactura.Location = new Point(708, 565);
            buttonPdfFactura.Name = "buttonPdfFactura";
            buttonPdfFactura.Size = new Size(94, 87);
            buttonPdfFactura.TabIndex = 61;
            buttonPdfFactura.TextImageRelation = TextImageRelation.ImageAboveText;
            toolTip1.SetToolTip(buttonPdfFactura, "Factura");
            buttonPdfFactura.UseVisualStyleBackColor = false;
            buttonPdfFactura.Click += buttonPdfFactura_Click;
            // 
            // HistorialVentas
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1511, 664);
            Controls.Add(buttonPdfFactura);
            Controls.Add(button5);
            Controls.Add(label3);
            Controls.Add(dataGridViewHistorial);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HistorialVentas";
            Text = "Historial de Ventas";
            Load += HistorialVentas_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        public DataGridView dataGridViewHistorial;
        private Label label3;
        private Button button5;
        private Button buttonPdfFactura;
        private ToolTip toolTip1;
    }
}