using NEGOCIO;
using NEGOCIO.NEGOCIO;
using Org.BouncyCastle.Asn1.BC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace PRESENTACION.PRESENTACION
{
    public partial class MantenimientoEmpleados : Form
    {
        private readonly NegocioBDD negocioLotesRemates;
        private readonly RepositorioUsuarios repositorioUsuarios;
        public MantenimientoEmpleados()
        {
            InitializeComponent();
            InitializeDateTimePickers();

            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            negocioLotesRemates = new NegocioBDD();
            repositorioUsuarios = new RepositorioUsuarios();

            // Agregar columnas al DataGridView
            dataGridView1.Columns.Add("Id", "N° EMP");
            dataGridView1.Columns.Add("Id", "N° USU");
            dataGridView1.Columns.Add("Login", "USUARIO");
            dataGridView1.Columns.Add("NombreCompleto", "NOMBRE COMPLETO");
            dataGridView1.Columns.Add("HoraEntrada", "ENTRADA");
            dataGridView1.Columns.Add("HoraSalida", "SALIDA");
            dataGridView1.Columns.Add("SueldoMensual", "SUELDO MENSUAL");
            dataGridView1.Columns.Add("Tareas", "TAREAS");
            dataGridView1.Columns.Add("NumeroRematador", "N° REM");
            dataGridView1.Columns.Add("Comision", "COMISION");


            dataGridView1.Columns[0].Width = 75;
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 11);
            dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("Arial", 11);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);


        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonRegresar.BackColor = Color.FromArgb(255, 200, 200); // Rojo claro
            buttonRegresar.ForeColor = Color.FromArgb(255, 100, 100);
            buttonRegresar.FlatAppearance.BorderColor = Color.Red; // Cambiar color del borde

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {

            // Restaurar los colores al salir el mouse del botón
            buttonRegresar.BackColor = Color.Transparent;
            buttonRegresar.ForeColor = Color.White;
            buttonRegresar.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        //boton regresar
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //INICIALIZADOR DE FORMATO DE HORAS ------------------------------------------------------
        private void InitializeDateTimePickers()
        {
            dateTimePickerHoraEntrada.CustomFormat = dateTimePickerHoraSalida.CustomFormat = "HH:mm";
            dateTimePickerHoraEntrada.Format = dateTimePickerHoraSalida.Format = DateTimePickerFormat.Custom;
            dateTimePickerHoraEntrada.ShowUpDown = dateTimePickerHoraSalida.ShowUpDown = true;
            dateTimePickerHoraEntrada.Value = DateTime.Now.Date; // Hora actual 
            dateTimePickerHoraSalida.Value = DateTime.Now.Date; // Hora actual 


        }


        //BOTON ELIMINAR EMPLEADO---------------------------------------------------------------------
        private void buttonEliminarEmpleado_Click(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada en la grilla
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int idEmpleado = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Confirmar si realmente se desea eliminar el empleado
                DialogResult result = MessageBox.Show("¿Estás seguro que deseas eliminar este empleado?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Eliminar el empleado en la capa de negocios
                    negocioLotesRemates.DarDeBajaEmpleado(idEmpleado);

                    // Limpiar campos y recargar la grilla de empleados
                    limpiarCamposYrecargar();

                    MessageBox.Show("Empleado eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un empleado para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //BOTON MODIFICAR EMPLEADO--------------------------------------------------------------------------------------------
        private void buttonModificarEmpleado_Click(object sender, EventArgs e)
        {
            if (CamposEmpleadoCompletos())
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    int idEmpleado = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                    string loginUsuario = comboBoxElegirUsuario.Text;
                    TimeSpan horaEntrada = dateTimePickerHoraEntrada.Value.TimeOfDay;
                    TimeSpan horaSalida = dateTimePickerHoraSalida.Value.TimeOfDay;
                    decimal sueldoMensual = Convert.ToDecimal(textBoxSueldoMensual.Text);
                    string tareas = textBoxTareas.Text;

                    // Verifica el tipo de usuario (Rematador u Operador)
                    string tipoUsuario = string.IsNullOrEmpty(selectedRow.Cells["Tareas"].Value.ToString()) ? "REMATADOR" : "OPERADOR";

                    // Para Rematadores
                    if (tipoUsuario == "REMATADOR")
                    {
                        string numeroRematador = txtNumeroDeRematador.Text;
                        decimal comision = (decimal)trackBar1.Value / 10;
                        negocioLotesRemates.ModificarRematador(idEmpleado, loginUsuario, horaEntrada, horaSalida, sueldoMensual, numeroRematador, comision);
                    }
                    // Para Operadores
                    else
                    {
                        negocioLotesRemates.ModificarOperador(idEmpleado, loginUsuario, horaEntrada, horaSalida, sueldoMensual, tareas);
                    }

                    // Mostrar confirmación y realizar la modificación si el usuario confirma
                    DialogResult result = MessageBox.Show("¿Estás seguro de que deseas modificar este empleado?", "Confirmar Modificación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Limpiar campos y recargar la grilla después de la modificación
                        limpiarCamposYrecargar();
                        MessageBox.Show("Empleado modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Cancelar la modificación y revertir los cambios realizados antes de la confirmación
                        // Puedes implementar la lógica específica para revertir los cambios aquí si es necesario
                        // Por ejemplo, recargar los datos originales del empleado en los controles de la interfaz
                        // y deshacer cualquier modificación hecha antes de la confirmación
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona un empleado para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos antes de modificar al empleado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool CamposEmpleadoCompletos()
        {
            if (string.IsNullOrWhiteSpace(comboBoxElegirUsuario.Text) || string.IsNullOrWhiteSpace(textBoxSueldoMensual.Text))
            {
                return false;
            }

            // Puedes agregar más verificaciones según tus requisitos aquí

            return true;
        }








        private void buttonRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            if (comboBoxElegirUsuario.SelectedItem != null)
            {
                string usuarioSeleccionado = comboBoxElegirUsuario.SelectedItem.ToString();
                Usuario usuario = repositorioUsuarios.ObtenerUsuarioPorLogin(usuarioSeleccionado);
                decimal sueldoMensual;

                if (usuario != null)
                {
                    // Verificar si el empleado ya está registrado
                    if (negocioLotesRemates.EsEmpleadoRegistrado(usuario.id))
                    {
                        MessageBox.Show("Este empleado ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Salir del método si el empleado ya está registrado
                    }

                    int idUsuario = usuario.id;
                    TimeSpan horaEntrada = dateTimePickerHoraEntrada.Value.TimeOfDay;
                    TimeSpan horaSalida = dateTimePickerHoraSalida.Value.TimeOfDay;

                    // Verificar si todos los campos están completos para rematadores
                    if (comboBoxTipoDeUsuario.SelectedItem.ToString() == "REMATADOR" && (string.IsNullOrEmpty(textBoxSueldoMensual.Text) || (string.IsNullOrEmpty(txtNumeroDeRematador.Text)) || trackBar1.Value == 0))
                    {
                        MessageBox.Show("Completa todos los campos antes de registrar el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Salir del método si no están completos todos los campos
                    }

                    if (Decimal.TryParse(textBoxSueldoMensual.Text, out sueldoMensual))
                    {
                        if (comboBoxTipoDeUsuario.SelectedItem.ToString() == "OPERADOR")
                        {
                            // Verificar si el campo de tareas está completo
                            if (string.IsNullOrEmpty(textBoxTareas.Text))
                            {
                                MessageBox.Show("Completa el campo de tareas antes de registrar el operador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Salir del método si el campo de tareas no está completo
                            }

                            // Obtener tareas para operador desde el TextBox
                            string tareas = textBoxTareas.Text;
                            negocioLotesRemates.AgregarOperador(idUsuario, horaEntrada, horaSalida, sueldoMensual, tareas);
                        }
                        else if (comboBoxTipoDeUsuario.SelectedItem.ToString() == "REMATADOR")
                        {
                            // Obtener número de rematador desde el TextBox
                            string numeroRematador = txtNumeroDeRematador.Text;
                            decimal comision = (decimal)trackBar1.Value / 10;
                            negocioLotesRemates.AgregarRematador(idUsuario, horaEntrada, horaSalida, sueldoMensual, numeroRematador, comision);
                        }

                        limpiarCamposYrecargar();
                        MessageBox.Show("Empleado registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("El valor del sueldo mensual no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener información del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }






        private bool UsuarioEstaRegistradoComoEmpleado(string loginUsuario)
        {
            List<Empleado> empleados = negocioLotesRemates.ObtenerEmpleados();
            return empleados.Any(empleado => empleado.Login == loginUsuario);
        }



        private void MantenimientoEmpleados_Load(object sender, EventArgs e)
        {
            List<string> usuariosOperadores = negocioLotesRemates.ObtenerUsuariosOperadores();
            // Llenar el ComboBox con los usuarios operadores
            comboBoxElegirUsuario.DataSource = usuariosOperadores;
            comboBoxElegirUsuario.SelectedIndex = -1;

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 500; // 500 pasos para permitir decimales
            // Llamar al método para actualizar la grilla de empleados
            ActualizarGrillaEmpleados();


            labelTareas.Visible = false;
            textBoxTareas.Visible = false;

            lblNumeroDeRematador.Visible = false;
            txtNumeroDeRematador.Visible = false;

            trackBar1.Visible = false;
            label1.Visible = false;
            labelIndiqueComision.Visible = false;
           
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = false;
            }
        }


        private void limpiarCamposYrecargar()
        {

            // Limpiar campos después de agregar empleado
            comboBoxElegirUsuario.SelectedIndex = -1;
            comboBoxTipoDeUsuario.SelectedIndex = -1;
            dateTimePickerHoraEntrada.Value = DateTime.Now.Date;
            dateTimePickerHoraSalida.Value = DateTime.Now.Date;
            textBoxSueldoMensual.Clear();
            textBoxTareas.Clear();
            txtNumeroDeRematador.Clear();  // Limpiar el campo de número de rematador
            trackBar1.Value = 0;  // Restablecer el valor del trackBar
            label1.Text = "0 %";   // Restablecer el texto del label de comisión
            // Llamar al método para actualizar la grilla de empleados nuevamente
            ActualizarGrillaEmpleados();

            labelTareas.Visible = false;
            textBoxTareas.Visible = false;

            lblNumeroDeRematador.Visible = false;
            txtNumeroDeRematador.Visible = false;

            trackBar1.Visible = false;
            label1.Visible = false;
            labelIndiqueComision.Visible = false;

        }

        private void ActualizarGrillaEmpleados()
        {
            try
            {
                // Obtener los empleados desde la capa de negocios
                List<Empleado> empleados = negocioLotesRemates.ObtenerEmpleados();

                // Limpiar la grilla antes de actualizar
                dataGridView1.Rows.Clear();

                // Agregar los empleados a la grilla
                foreach (Empleado empleado in empleados)
                {
                    if (empleado is Rematador rematador)
                    {
                        // Si es un rematador
                        dataGridView1.Rows.Add(
                            rematador.Id,
                            rematador.IdUsuario,
                            rematador.Login,
                            $"{rematador.Nombre} {rematador.Apellido}", // Mostrar nombre completo
                            rematador.HoraEntrada,
                            rematador.HoraSalida,
                            rematador.SueldoMensual,
                            string.Empty, // No hay tareas para rematadores
                            rematador.NRematador, // Número de rematador
                            rematador.Comision
                        );
                    }
                    else if (empleado is Operador operador)
                    {
                        // Si es un operador
                        dataGridView1.Rows.Add(
                            operador.Id,
                            operador.IdUsuario,
                            operador.Login,
                            $"{operador.Nombre} {operador.Apellido}", // Mostrar nombre completo
                            operador.HoraEntrada,
                            operador.HoraSalida,
                            operador.SueldoMensual,
                            operador.Tareas, // Tareas para operadores
                            string.Empty // No hay número de rematador para operadores
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Obtener el tipo de usuario basado en la presencia de tareas
                string tipoUsuario = string.IsNullOrEmpty(row.Cells["Tareas"].Value.ToString()) ? "REMATADOR" : "OPERADOR";

                // Autocompletar ComboBox de tipo de usuario
                comboBoxTipoDeUsuario.SelectedItem = tipoUsuario;

                // Autocompletar ComboBox de seleccionar usuario (nombre de usuario)
                comboBoxElegirUsuario.Text = row.Cells["Login"].Value.ToString();

                // Autocompletar campos específicos del rematador si es rematador
                if (tipoUsuario == "REMATADOR")
                {
                    txtNumeroDeRematador.Text = row.Cells["NumeroRematador"].Value.ToString();
                    trackBar1.Value = (int)(Convert.ToDecimal(row.Cells["Comision"].Value) * 10);
                    label1.Text = row.Cells["Comision"].Value.ToString() + " %";
                }
                else
                {
                    txtNumeroDeRematador.Text = "";
                    trackBar1.Value = 0;
                }

                // Continuar con la lógica para mostrar otros datos en los campos correspondientes
                TimeSpan horaEntrada = TimeSpan.Parse(row.Cells["HoraEntrada"].Value.ToString());
                TimeSpan horaSalida = TimeSpan.Parse(row.Cells["HoraSalida"].Value.ToString());
                decimal sueldoMensual = Convert.ToDecimal(row.Cells["SueldoMensual"].Value);
                string tareas = row.Cells["Tareas"].Value.ToString();

                dateTimePickerHoraEntrada.Value = DateTime.Today + horaEntrada;
                dateTimePickerHoraSalida.Value = DateTime.Today + horaSalida;
                textBoxSueldoMensual.Text = sueldoMensual.ToString();
                textBoxTareas.Text = tareas;
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            limpiarCamposYrecargar();
        }


        //BTN REGITRAR ---------------------------------------------------------------------
        private void buttonRegistrarEmpleado_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonRegistrarEmpleado.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonRegistrarEmpleado.ForeColor = Color.FromArgb(160, 160, 160);
            buttonRegistrarEmpleado.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde

        }

        private void buttonRegistrarEmpleado_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            buttonRegistrarEmpleado.BackColor = Color.Transparent;
            buttonRegistrarEmpleado.ForeColor = Color.White;
            buttonRegistrarEmpleado.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }


        //BTN MODIFICAR -------------------------------------------------------------------------------
        private void buttonModificarEmpleado_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonModificarEmpleado.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonModificarEmpleado.ForeColor = Color.FromArgb(160, 160, 160);
            buttonModificarEmpleado.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde

        }

        private void buttonModificarEmpleado_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            buttonModificarEmpleado.BackColor = Color.Transparent;
            buttonModificarEmpleado.ForeColor = Color.White;
            buttonModificarEmpleado.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }

        //BTN ELIMINAR -------------------------------------------------------------------------------
        private void buttonEliminarEmpleado_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonEliminarEmpleado.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonEliminarEmpleado.ForeColor = Color.FromArgb(160, 160, 160);
            buttonEliminarEmpleado.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde

        }

        private void buttonEliminarEmpleado_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            buttonEliminarEmpleado.BackColor = Color.Transparent;
            buttonEliminarEmpleado.ForeColor = Color.White;
            buttonEliminarEmpleado.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

                if (column.Name == "HoraEntrada" || column.Name == "HoraSalida")
                {
                    if (e.Value != null && e.Value is TimeSpan)
                    {
                        TimeSpan timeValue = (TimeSpan)e.Value;
                        e.Value = timeValue.ToString(@"hh\:mm");
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void textBoxSueldoMensual_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es un número, una coma o una tecla de control
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void comboBoxTipoDeUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si hay un elemento seleccionado en el ComboBox
            if (comboBoxTipoDeUsuario.SelectedItem != null)
            {
                // Obtener el tipo de usuario seleccionado
                string tipoUsuario = comboBoxTipoDeUsuario.SelectedItem.ToString();

                // Obtener usuarios activos basados en el tipo de usuario seleccionado
                List<string> usuariosActivos = new List<string>();

                if (tipoUsuario == "OPERADOR")
                {
                    usuariosActivos = negocioLotesRemates.ObtenerUsuariosOperadoresActivos();
                    labelTareas.Visible = true;
                    textBoxTareas.Visible = true;

                    lblNumeroDeRematador.Visible = false;
                    txtNumeroDeRematador.Visible = false;

                    trackBar1.Visible = false;
                    label1.Visible = false;
                    labelIndiqueComision.Visible = false;
                }
                else if (tipoUsuario == "REMATADOR")
                {
                    usuariosActivos = negocioLotesRemates.ObtenerUsuariosRematadoresActivos();
                    lblNumeroDeRematador.Visible = true;
                    txtNumeroDeRematador.Visible = true;

                    labelTareas.Visible = false;
                    textBoxTareas.Visible = false;

                    trackBar1.Visible = true;
                    label1.Visible = true;
                    labelIndiqueComision.Visible = true;
                }

                // Asignar la lista de usuarios activos al ComboBox "Elegir Usuario"
                comboBoxElegirUsuario.DataSource = usuariosActivos;
                comboBoxElegirUsuario.SelectedIndex = -1; // Establecer sin selección por defecto
            }
        }

        private void lblTipoDeUsuario_Click(object sender, EventArgs e)
        {

        }

        private void lblNumeroDeRematador_Click(object sender, EventArgs e)
        {

        }

        private void txtNumeroDeRematador_TextChanged(object sender, EventArgs e)
        {
            // Verificar si el TextBox tiene más de 10 dígitos y eliminar los extras
            if (txtNumeroDeRematador.Text.Length > 10)
            {
                txtNumeroDeRematador.Text = txtNumeroDeRematador.Text.Substring(0, 10);
                txtNumeroDeRematador.SelectionStart = txtNumeroDeRematador.Text.Length;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Obtener el valor decimal seleccionado por el usuario del deslizador
            decimal valorSeleccionado = (decimal)trackBar1.Value / 10; // Dividir por 10 para obtener decimales

            // Mostrar el valor seleccionado en el control Label
            label1.Text = $"{valorSeleccionado} %"; // Asigna el valor al Label
        }

        private void txtNumeroDeRematador_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y la tecla de retroceso (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
}
