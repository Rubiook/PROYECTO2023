namespace PRESENTACION.PRESENTACION
{
    partial class VerLotesProximoRemate
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


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerLotesProximoRemate));
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblRemate = new Label();
            button3 = new Button();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(3, 6, 3);
            flowLayoutPanel1.BorderStyle = BorderStyle.Fixed3D;
            flowLayoutPanel1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            flowLayoutPanel1.Location = new Point(105, 138);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1414, 540);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // lblRemate
            // 
            lblRemate.AutoSize = true;
            lblRemate.BackColor = Color.Transparent;
            lblRemate.BorderStyle = BorderStyle.Fixed3D;
            lblRemate.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            lblRemate.ForeColor = Color.White;
            lblRemate.Location = new Point(509, 42);
            lblRemate.Name = "lblRemate";
            lblRemate.Size = new Size(2, 56);
            lblRemate.TabIndex = 55;
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
            button3.Location = new Point(1489, 728);
            button3.Name = "button3";
            button3.Size = new Size(124, 48);
            button3.TabIndex = 61;
            button3.Text = "Regresar";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            button3.MouseEnter += button3_MouseEnter;
            button3.MouseLeave += button3_MouseLeave;
            // 
            // VerLotesProximoRemate
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1625, 788);
            Controls.Add(button3);
            Controls.Add(lblRemate);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "VerLotesProximoRemate";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ver Lotes del Próximo Remate";
            Load += LotesProximoRemate_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblRemate;
        private Button button3;
    }
}