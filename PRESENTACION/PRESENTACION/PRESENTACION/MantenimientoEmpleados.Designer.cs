namespace PRESENTACION.PRESENTACION
{
    partial class MantenimientoEmpleados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MantenimientoEmpleados));
            buttonRegresar = new Button();
            dataGridView1 = new DataGridView();
            label3 = new Label();
            buttonRegistrarEmpleado = new Button();
            buttonModificarEmpleado = new Button();
            buttonEliminarEmpleado = new Button();
            labelElejirUsuario = new Label();
            comboBoxElegirUsuario = new ComboBox();
            textBoxSueldoMensual = new TextBox();
            labelSueldoMensual = new Label();
            dateTimePickerHoraSalida = new DateTimePicker();
            dateTimePickerHoraEntrada = new DateTimePicker();
            labelHoraSalida = new Label();
            labelHoraEntrada = new Label();
            textBoxTareas = new TextBox();
            labelTareas = new Label();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // buttonRegresar
            // 
            buttonRegresar.BackColor = Color.Transparent;
            buttonRegresar.Cursor = Cursors.Hand;
            buttonRegresar.FlatAppearance.BorderColor = Color.GreenYellow;
            buttonRegresar.FlatAppearance.MouseDownBackColor = Color.Red;
            buttonRegresar.FlatAppearance.MouseOverBackColor = Color.GreenYellow;
            buttonRegresar.FlatStyle = FlatStyle.Popup;
            buttonRegresar.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            buttonRegresar.ForeColor = Color.White;
            buttonRegresar.Location = new Point(1020, 596);
            buttonRegresar.Name = "buttonRegresar";
            buttonRegresar.Size = new Size(125, 50);
            buttonRegresar.TabIndex = 57;
            buttonRegresar.Text = "Regresar";
            buttonRegresar.UseVisualStyleBackColor = false;
            buttonRegresar.Click += button1_Click;
            buttonRegresar.MouseEnter += button1_MouseEnter;
            buttonRegresar.MouseLeave += button1_MouseLeave;
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
            dataGridView1.Location = new Point(59, 295);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.RowTemplate.Height = 27;
            dataGridView1.Size = new Size(1039, 209);
            dataGridView1.TabIndex = 58;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Font = new Font("Segoe UI", 33F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(207, 48);
            label3.Name = "label3";
            label3.Size = new Size(743, 62);
            label3.TabIndex = 60;
            label3.Text = "MANTENIMIENTO DE EMPLEADOS";
            // 
            // buttonRegistrarEmpleado
            // 
            buttonRegistrarEmpleado.BackColor = Color.Transparent;
            buttonRegistrarEmpleado.Cursor = Cursors.Hand;
            buttonRegistrarEmpleado.FlatStyle = FlatStyle.Flat;
            buttonRegistrarEmpleado.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            buttonRegistrarEmpleado.ForeColor = Color.White;
            buttonRegistrarEmpleado.Location = new Point(752, 501);
            buttonRegistrarEmpleado.Name = "buttonRegistrarEmpleado";
            buttonRegistrarEmpleado.Size = new Size(346, 56);
            buttonRegistrarEmpleado.TabIndex = 63;
            buttonRegistrarEmpleado.Text = "Registrar";
            buttonRegistrarEmpleado.UseVisualStyleBackColor = false;
            buttonRegistrarEmpleado.Click += buttonRegistrarEmpleado_Click;
            buttonRegistrarEmpleado.MouseEnter += buttonRegistrarEmpleado_MouseEnter;
            buttonRegistrarEmpleado.MouseLeave += buttonRegistrarEmpleado_MouseLeave;
            // 
            // buttonModificarEmpleado
            // 
            buttonModificarEmpleado.BackColor = Color.Transparent;
            buttonModificarEmpleado.Cursor = Cursors.Hand;
            buttonModificarEmpleado.FlatStyle = FlatStyle.Flat;
            buttonModificarEmpleado.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            buttonModificarEmpleado.ForeColor = Color.White;
            buttonModificarEmpleado.Location = new Point(400, 501);
            buttonModificarEmpleado.Name = "buttonModificarEmpleado";
            buttonModificarEmpleado.Size = new Size(354, 56);
            buttonModificarEmpleado.TabIndex = 62;
            buttonModificarEmpleado.Text = "Modificar";
            buttonModificarEmpleado.UseVisualStyleBackColor = false;
            buttonModificarEmpleado.Click += buttonModificarEmpleado_Click;
            buttonModificarEmpleado.MouseEnter += buttonModificarEmpleado_MouseEnter;
            buttonModificarEmpleado.MouseLeave += buttonModificarEmpleado_MouseLeave;
            // 
            // buttonEliminarEmpleado
            // 
            buttonEliminarEmpleado.BackColor = Color.Transparent;
            buttonEliminarEmpleado.Cursor = Cursors.Hand;
            buttonEliminarEmpleado.FlatStyle = FlatStyle.Flat;
            buttonEliminarEmpleado.Font = new Font("Segoe UI Black", 20F, FontStyle.Regular, GraphicsUnit.Point);
            buttonEliminarEmpleado.ForeColor = Color.White;
            buttonEliminarEmpleado.Location = new Point(59, 501);
            buttonEliminarEmpleado.Name = "buttonEliminarEmpleado";
            buttonEliminarEmpleado.Size = new Size(346, 56);
            buttonEliminarEmpleado.TabIndex = 61;
            buttonEliminarEmpleado.Text = "Eliminar";
            buttonEliminarEmpleado.UseVisualStyleBackColor = false;
            buttonEliminarEmpleado.Click += buttonEliminarEmpleado_Click;
            buttonEliminarEmpleado.MouseEnter += buttonEliminarEmpleado_MouseEnter;
            buttonEliminarEmpleado.MouseLeave += buttonEliminarEmpleado_MouseLeave;
            // 
            // labelElejirUsuario
            // 
            labelElejirUsuario.AutoSize = true;
            labelElejirUsuario.BackColor = Color.Transparent;
            labelElejirUsuario.BorderStyle = BorderStyle.Fixed3D;
            labelElejirUsuario.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelElejirUsuario.ForeColor = Color.White;
            labelElejirUsuario.Location = new Point(160, 159);
            labelElejirUsuario.Name = "labelElejirUsuario";
            labelElejirUsuario.Size = new Size(206, 27);
            labelElejirUsuario.TabIndex = 67;
            labelElejirUsuario.Text = "Seleccione el Usuario:";
            // 
            // comboBoxElegirUsuario
            // 
            comboBoxElegirUsuario.BackColor = Color.FromArgb(3, 6, 3);
            comboBoxElegirUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxElegirUsuario.FlatStyle = FlatStyle.Flat;
            comboBoxElegirUsuario.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            comboBoxElegirUsuario.ForeColor = Color.White;
            comboBoxElegirUsuario.FormattingEnabled = true;
            comboBoxElegirUsuario.IntegralHeight = false;
            comboBoxElegirUsuario.Items.AddRange(new object[] { "OPERADOR", "COMPRADOR", "VENDEDOR", "REMATADOR" });
            comboBoxElegirUsuario.Location = new Point(372, 159);
            comboBoxElegirUsuario.Name = "comboBoxElegirUsuario";
            comboBoxElegirUsuario.Size = new Size(216, 29);
            comboBoxElegirUsuario.TabIndex = 66;
            // 
            // textBoxSueldoMensual
            // 
            textBoxSueldoMensual.BackColor = Color.FromArgb(3, 6, 3);
            textBoxSueldoMensual.BorderStyle = BorderStyle.FixedSingle;
            textBoxSueldoMensual.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxSueldoMensual.ForeColor = SystemColors.Window;
            textBoxSueldoMensual.Location = new Point(418, 194);
            textBoxSueldoMensual.Name = "textBoxSueldoMensual";
            textBoxSueldoMensual.Size = new Size(170, 29);
            textBoxSueldoMensual.TabIndex = 65;
            textBoxSueldoMensual.KeyPress += textBoxSueldoMensual_KeyPress;
            // 
            // labelSueldoMensual
            // 
            labelSueldoMensual.AutoSize = true;
            labelSueldoMensual.BackColor = Color.Transparent;
            labelSueldoMensual.BorderStyle = BorderStyle.Fixed3D;
            labelSueldoMensual.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelSueldoMensual.ForeColor = Color.White;
            labelSueldoMensual.Location = new Point(160, 196);
            labelSueldoMensual.Name = "labelSueldoMensual";
            labelSueldoMensual.Size = new Size(252, 27);
            labelSueldoMensual.TabIndex = 64;
            labelSueldoMensual.Text = "Ingrese el Sueldo Mensual:";
            // 
            // dateTimePickerHoraSalida
            // 
            dateTimePickerHoraSalida.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePickerHoraSalida.CalendarMonthBackground = Color.Black;
            dateTimePickerHoraSalida.CalendarTitleBackColor = SystemColors.ControlText;
            dateTimePickerHoraSalida.CalendarTitleForeColor = Color.Black;
            dateTimePickerHoraSalida.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePickerHoraSalida.Format = DateTimePickerFormat.Time;
            dateTimePickerHoraSalida.Location = new Point(513, 236);
            dateTimePickerHoraSalida.Name = "dateTimePickerHoraSalida";
            dateTimePickerHoraSalida.Size = new Size(75, 29);
            dateTimePickerHoraSalida.TabIndex = 71;
            dateTimePickerHoraSalida.Value = new DateTime(2023, 8, 13, 14, 33, 10, 0);
            // 
            // dateTimePickerHoraEntrada
            // 
            dateTimePickerHoraEntrada.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePickerHoraEntrada.CalendarMonthBackground = Color.Black;
            dateTimePickerHoraEntrada.CalendarTitleBackColor = SystemColors.ControlText;
            dateTimePickerHoraEntrada.CalendarTitleForeColor = Color.Black;
            dateTimePickerHoraEntrada.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePickerHoraEntrada.Format = DateTimePickerFormat.Time;
            dateTimePickerHoraEntrada.Location = new Point(304, 235);
            dateTimePickerHoraEntrada.Name = "dateTimePickerHoraEntrada";
            dateTimePickerHoraEntrada.Size = new Size(75, 29);
            dateTimePickerHoraEntrada.TabIndex = 70;
            // 
            // labelHoraSalida
            // 
            labelHoraSalida.AutoSize = true;
            labelHoraSalida.BackColor = Color.Transparent;
            labelHoraSalida.BorderStyle = BorderStyle.Fixed3D;
            labelHoraSalida.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelHoraSalida.ForeColor = Color.White;
            labelHoraSalida.Location = new Point(385, 236);
            labelHoraSalida.Name = "labelHoraSalida";
            labelHoraSalida.Size = new Size(122, 27);
            labelHoraSalida.TabIndex = 69;
            labelHoraSalida.Text = "Hora Salida:";
            // 
            // labelHoraEntrada
            // 
            labelHoraEntrada.AutoSize = true;
            labelHoraEntrada.BackColor = Color.Transparent;
            labelHoraEntrada.BorderStyle = BorderStyle.Fixed3D;
            labelHoraEntrada.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelHoraEntrada.ForeColor = Color.White;
            labelHoraEntrada.Location = new Point(160, 237);
            labelHoraEntrada.Name = "labelHoraEntrada";
            labelHoraEntrada.Size = new Size(138, 27);
            labelHoraEntrada.TabIndex = 68;
            labelHoraEntrada.Text = "Hora Entrada:";
            // 
            // textBoxTareas
            // 
            textBoxTareas.BackColor = Color.FromArgb(3, 6, 3);
            textBoxTareas.BorderStyle = BorderStyle.FixedSingle;
            textBoxTareas.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxTareas.ForeColor = SystemColors.Window;
            textBoxTareas.Location = new Point(622, 189);
            textBoxTareas.Multiline = true;
            textBoxTareas.Name = "textBoxTareas";
            textBoxTareas.ScrollBars = ScrollBars.Vertical;
            textBoxTareas.Size = new Size(374, 76);
            textBoxTareas.TabIndex = 78;
            // 
            // labelTareas
            // 
            labelTareas.AutoSize = true;
            labelTareas.BackColor = Color.Transparent;
            labelTareas.BorderStyle = BorderStyle.Fixed3D;
            labelTareas.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelTareas.ForeColor = Color.White;
            labelTareas.Location = new Point(622, 159);
            labelTareas.Name = "labelTareas";
            labelTareas.Size = new Size(74, 27);
            labelTareas.TabIndex = 79;
            labelTareas.Text = "Tareas:";
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
            button5.TabIndex = 80;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // MantenimientoEmpleados
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1157, 658);
            Controls.Add(button5);
            Controls.Add(labelTareas);
            Controls.Add(textBoxTareas);
            Controls.Add(dateTimePickerHoraSalida);
            Controls.Add(dateTimePickerHoraEntrada);
            Controls.Add(labelHoraSalida);
            Controls.Add(labelHoraEntrada);
            Controls.Add(labelElejirUsuario);
            Controls.Add(comboBoxElegirUsuario);
            Controls.Add(textBoxSueldoMensual);
            Controls.Add(labelSueldoMensual);
            Controls.Add(buttonRegistrarEmpleado);
            Controls.Add(buttonModificarEmpleado);
            Controls.Add(buttonEliminarEmpleado);
            Controls.Add(label3);
            Controls.Add(dataGridView1);
            Controls.Add(buttonRegresar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MantenimientoEmpleados";
            Text = "Mantenimiento de Empleados";
            Load += MantenimientoEmpleados_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonRegresar;
        public DataGridView dataGridView1;
        private Label label3;
        private Button buttonRegistrarEmpleado;
        private Button buttonModificarEmpleado;
        private Button buttonEliminarEmpleado;
        private Label labelElejirUsuario;
        private ComboBox comboBoxElegirUsuario;
        private TextBox textBoxSueldoMensual;
        private Label labelSueldoMensual;
        private DateTimePicker dateTimePickerHoraSalida;
        private DateTimePicker dateTimePickerHoraEntrada;
        private Label labelHoraSalida;
        private Label labelHoraEntrada;
        private TextBox textBoxTareas;
        private Label labelTareas;
        private Button button5;
    }
}