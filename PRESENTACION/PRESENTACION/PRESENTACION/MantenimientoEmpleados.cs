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
        private readonly NegocioLotesRemates negocioLotesRemates;
        private readonly RepositorioUsuarios repositorioUsuarios;
        public MantenimientoEmpleados()
        {
            InitializeComponent();
            InitializeDateTimePickers();
            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            negocioLotesRemates = new NegocioLotesRemates();
            repositorioUsuarios = new RepositorioUsuarios();

            // Agregar columnas al DataGridView
            dataGridView1.Columns.Add("Id", "ID");
            dataGridView1.Columns.Add("Login", "USUARIO");
            dataGridView1.Columns.Add("NombreCompleto", "NOMBRE COMPLETO");
            dataGridView1.Columns.Add("HoraEntrada", "HORA ENTRADA");
            dataGridView1.Columns.Add("HoraSalida", "HORA SALIDA");
            dataGridView1.Columns.Add("SueldoMensual", "SUELDO MENSUAL");
            dataGridView1.Columns.Add("Tareas", "TAREAS");

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[5].Width = 100;
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
                    negocioLotesRemates.EliminarEmpleado(idEmpleado);
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
            // Verificar si hay una fila seleccionada en la grilla
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Verificar si todos los campos están completos
                if (comboBoxElegirUsuario.SelectedIndex == -1 || string.IsNullOrEmpty(textBoxSueldoMensual.Text) || string.IsNullOrEmpty(textBoxTareas.Text))
                {
                    MessageBox.Show("Completa todos los campos antes de modificar el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método si no están completos todos los campos
                }

                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int idEmpleado = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                string loginUsuario = comboBoxElegirUsuario.Text;
                TimeSpan horaEntrada = dateTimePickerHoraEntrada.Value.TimeOfDay;
                TimeSpan horaSalida = dateTimePickerHoraSalida.Value.TimeOfDay;
                decimal sueldoMensual = Convert.ToDecimal(textBoxSueldoMensual.Text);
                string tareas = textBoxTareas.Text;

                // Actualizar el empleado en la capa de negocios
                negocioLotesRemates.ModificarEmpleado(idEmpleado, loginUsuario, horaEntrada, horaSalida, sueldoMensual, tareas);

                limpiarCamposYrecargar();
                MessageBox.Show("Empleado modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
            {
                MessageBox.Show("Selecciona un empleado para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void buttonRegistrarEmpleado_Click(object sender, EventArgs e)
        {
            if (comboBoxElegirUsuario.SelectedItem != null)
            {
                string usuarioOperador = comboBoxElegirUsuario.SelectedItem.ToString();

                // Verificar si el usuario ya está registrado como empleado
                if (UsuarioEstaRegistradoComoEmpleado(usuarioOperador))
                {
                    MessageBox.Show("El usuario ya está registrado como empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método si el usuario ya está registrado
                }

                Usuario usuario = repositorioUsuarios.ObtenerUsuarioPorLogin(usuarioOperador);

                if (usuario != null)
                {
                    int idUsuarioOperador = usuario.id;

                    TimeSpan horaEntrada = dateTimePickerHoraEntrada.Value.TimeOfDay;
                    TimeSpan horaSalida = dateTimePickerHoraSalida.Value.TimeOfDay;

                    // Verificar si todos los campos están completos
                    if (string.IsNullOrEmpty(textBoxSueldoMensual.Text) || string.IsNullOrEmpty(textBoxTareas.Text))
                    {
                        MessageBox.Show("Completa todos los campos antes de registrar el empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Salir del método si no están completos todos los campos
                    }

                    // Intenta convertir el valor del TextBox a decimal de manera segura
                    decimal sueldoMensual;
                    if (Decimal.TryParse(textBoxSueldoMensual.Text, out sueldoMensual))
                    {
                        string tareas = textBoxTareas.Text;

                        negocioLotesRemates.AgregarEmpleado(idUsuarioOperador, horaEntrada, horaSalida, sueldoMensual, tareas);
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
                MessageBox.Show("Selecciona un usuario operador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


            // Llamar al método para actualizar la grilla de empleados
            ActualizarGrillaEmpleados();

        }


        private void limpiarCamposYrecargar()
        {

            // Limpiar campos después de agregar empleado
            comboBoxElegirUsuario.SelectedIndex = -1;
            dateTimePickerHoraEntrada.Value = DateTime.Now.Date;
            dateTimePickerHoraSalida.Value = DateTime.Now.Date;
            textBoxSueldoMensual.Clear();
            textBoxTareas.Clear();
            // Llamar al método para actualizar la grilla de empleados nuevamente
            ActualizarGrillaEmpleados();
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
                    // Obtenemos el nombre completo concatenando nombre y apellido
                    string nombreCompleto = $"{empleado.Nombre} {empleado.Apellido}";

                    dataGridView1.Rows.Add(empleado.Id, empleado.Login, nombreCompleto, empleado.HoraEntrada, empleado.HoraSalida, empleado.SueldoMensual, empleado.Tareas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegurarse de que se haya hecho clic en una fila válida
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Obtener los datos de la fila seleccionada
                string loginUsuario = row.Cells["Login"].Value.ToString(); // Obtener el nombre de usuario

                // Mostrar el nombre de usuario en el ComboBox
                comboBoxElegirUsuario.Text = loginUsuario;

                // Continuar con la lógica para mostrar los otros datos en los campos correspondientes
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
    }//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
}
