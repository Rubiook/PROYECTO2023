namespace PRESENTACION.PRESENTACION
{
    partial class Preeventas
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preeventas));
            dataGridView1 = new DataGridView();
            buttonVenderLote = new Button();
            buttonQuitarLoteDeVendidos = new Button();
            label3 = new Label();
            button3 = new Button();
            button5 = new Button();
            button1 = new Button();
           
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.FromArgb(3, 6, 3);
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(62, 140);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.RowTemplate.Height = 27;
            dataGridView1.Size = new Size(1554, 358);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
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
            buttonVenderLote.Location = new Point(745, 528);
            buttonVenderLote.Name = "buttonVenderLote";
            buttonVenderLote.Size = new Size(94, 87);
            buttonVenderLote.TabIndex = 57;
            buttonVenderLote.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonVenderLote.UseVisualStyleBackColor = false;
            buttonVenderLote.Click += buttonVenderLote_Click;
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
            buttonQuitarLoteDeVendidos.Location = new Point(873, 532);
            buttonQuitarLoteDeVendidos.Name = "buttonQuitarLoteDeVendidos";
            buttonQuitarLoteDeVendidos.Size = new Size(79, 78);
            buttonQuitarLoteDeVendidos.TabIndex = 60;
            buttonQuitarLoteDeVendidos.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonQuitarLoteDeVendidos.UseVisualStyleBackColor = false;
            buttonQuitarLoteDeVendidos.Click += buttonQuitarLoteDeVendidos_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Font = new Font("Segoe UI", 33F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(712, 32);
            label3.Name = "label3";
            label3.Size = new Size(272, 62);
            label3.TabIndex = 64;
            label3.Text = "PREVENTAS";
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderColor = Color.GreenYellow;
            button3.FlatAppearance.MouseDownBackColor = Color.Red;
            button3.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            button3.ForeColor = Color.White;
            button3.Location = new Point(1559, 577);
            button3.Name = "button3";
            button3.Size = new Size(125, 50);
            button3.TabIndex = 65;
            button3.Text = "Regresar";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            button3.MouseEnter += button3_MouseEnter;
            button3.MouseLeave += button3_MouseLeave;
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
            button5.TabIndex = 66;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 0, 0, 0);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 0, 0, 0);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.White;
            button1.Location = new Point(1112, 532);
            button1.Name = "button1";
            button1.Size = new Size(79, 78);
            button1.TabIndex = 67;
            button1.TextImageRelation = TextImageRelation.ImageAboveText;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Preeventas
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1696, 639);
            Controls.Add(button1);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(buttonQuitarLoteDeVendidos);
            Controls.Add(buttonVenderLote);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Preeventas";
            Text = "Preeventas";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

          






        }

        #endregion

        private DataGridView dataGridView1;
        private Button buttonVenderLote;
        private Button buttonQuitarLoteDeVendidos;
        private Label label3;
        private Button button3;
        private Button button5;
        private Button button1;
    }
}