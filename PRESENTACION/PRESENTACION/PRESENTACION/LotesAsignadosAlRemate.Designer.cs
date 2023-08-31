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
            toolTip1 = new ToolTip(components);
            button5 = new Button();
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
            dataGridViewLotesAsignados.Location = new Point(108, 141);
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
            btnDesasignarLote.Location = new Point(550, 525);
            btnDesasignarLote.Name = "btnDesasignarLote";
            btnDesasignarLote.Size = new Size(104, 95);
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
            lblRemate.Location = new Point(244, 41);
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
            button1.Location = new Point(1065, 590);
            button1.Name = "button1";
            button1.Size = new Size(125, 50);
            button1.TabIndex = 55;
            button1.Text = "Regresar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
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
            button5.TabIndex = 56;
            toolTip1.SetToolTip(button5, "Recargar");
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // LotesAsignadosAlRemate
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1202, 652);
            Controls.Add(button5);
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
        private ToolTip toolTip1;
        private Button button5;
    }
}