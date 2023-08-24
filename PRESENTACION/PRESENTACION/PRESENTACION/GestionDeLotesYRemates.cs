using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using NEGOCIO;
using NEGOCIO.NEGOCIO;
using System.Drawing;

namespace PRESENTACION.PRESENTACION
{
    public partial class GestionDeLotesYRemates : Form
    {

        private RepositorioUsuarios repositorioUsuarios;
        private MySqlConnection conn;
        private Dictionary<int, List<Lote>> rematesConLotes = new Dictionary<int, List<Lote>>();
        private readonly NegocioLotesRemates negocioLotesRemates;
        private int selectedLoteId = -1; // Inicializado con un valor que no corresponde a ningún lote
        private bool isDataGridViewSelecting = false;
        private bool updatingComboboxes = false;
        private DataGridViewRow selectedGridRow; // En gral
        private DataGridViewRow selectedLoteGridRow; // Renombrado para mayor claridad
        private DataGridViewRow selectedRemateGridRow; // Renombrado para mayor claridad
        private DateTime fechaRemate;
        private List<int> rematesResaltados = new List<int>(); // Declarar esta lista a nivel de clase
        public Image icono_con_foto;
        public Image icono_sin_foto;

        public GestionDeLotesYRemates()
        {
            InitializeComponent();
            InitializeDateTimePickers();
            InitializeDataGridView();
            InitializeConnection();
            repositorioUsuarios = new RepositorioUsuarios();
            negocioLotesRemates = new NegocioLotesRemates();
            refresh();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }

        //INICIALIZADOR DE FORMATO DE HORAS ------------------------------------------------------
        private void InitializeDateTimePickers()
        {
            dateTimePickerHoraFin.CustomFormat = dateTimePickerHoraInicio.CustomFormat = "HH:mm";
            dateTimePickerHoraFin.Format = dateTimePickerHoraInicio.Format = DateTimePickerFormat.Custom;
            dateTimePickerHoraFin.ShowUpDown = dateTimePickerHoraInicio.ShowUpDown = true;


        }

        //INICIALIZADOR DE GRILLAS--------------------------------------------------------------
        private void InitializeDataGridView()
        {
            //GRID REMATES----------------------------------------------------------
            dataGridView1.Columns.Add("id", " ID ");
            dataGridView1.Columns.Add("fecha", " FECHA ");
            dataGridView1.Columns.Add("hora_inicio", "HORA INICIO");
            dataGridView1.Columns.Add("hora_fin", "HORA FIN");
            dataGridView1.Columns.Add("rematador", " REMATADOR ");
            dataGridView1.Columns.Add("tipo_de_remate", " TIPO REMATE ");

            DataGridViewButtonColumn btnVerLotes = new DataGridViewButtonColumn();
            btnVerLotes.Name = "VerLotes";
            btnVerLotes.HeaderText = "LOTES DEL REMATE";
            btnVerLotes.Text = "Gestionar Lotes";
            btnVerLotes.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnVerLotes);

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);


            //GRID LOTES---------------------------------------------------------------
            dataGridView2.Columns.Add("id", "ID");
            dataGridView2.Columns.Add("proveedor_lote", "PROVEEDOR");
            dataGridView2.Columns.Add("precio_base", " $RESERVA");
            dataGridView2.Columns.Add("tipo_de_lote", "TIPO DE LOTE");
            dataGridView2.Columns.Add("cantidad_en_lote", "CANTIDAD");
            dataGridView2.Columns.Add("descripcion", "DESCRIPCIÓN");


            // Agregar una columna "Foto" a la grilla
            DataGridViewImageColumn fotoColumn = new DataGridViewImageColumn();
            fotoColumn.Name = "Foto";
            fotoColumn.HeaderText = "FOTO";
            fotoColumn.Width = 10;
            fotoColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Ajusta la imagen al tamaño de la celda
            dataGridView2.Columns.Add(fotoColumn);

            DataGridViewButtonColumn btnVerRemate = new DataGridViewButtonColumn(); // Agrega esta línea
            btnVerRemate.Name = "VerRemate"; // Corregido aquí
            btnVerRemate.HeaderText = "REMATE"; // Corregido aquí
            btnVerRemate.Text = "Visualizar";
            btnVerRemate.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(btnVerRemate); // Agrega la columna de botón "Ver Remate"a

            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 120;
            dataGridView2.Columns[2].Width = 100;
            dataGridView2.Columns[3].Width = 130;
            dataGridView2.Columns[4].Width = 90;
            dataGridView2.Columns[5].Width = 280;
            dataGridView2.Columns[6].Width = 60;
            dataGridView2.Columns[7].Width = 90;
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView2.Columns[0].DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);





        }

        //INICIALIZADOR DE CONEXION-----------------------
        private void InitializeConnection()
        {
            conn = Conexion.obtenerConexion();
        }


        //BOTON VER LOTES DE LA GRILLA REMATES----------------------------------------------------------------------------
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].Name == "VerRemate")
            {
                int loteId = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Id"].Value);

                int remateId = negocioLotesRemates.ObtenerRemateIdPorLote(loteId);

                if (remateId != -1)
                {
                    dataGridView1.ClearSelection();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["Id"].Value) == remateId)
                        {
                            row.Selected = true;
                            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Este lote no está asignado a ningún remate.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }




        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Cargar las imágenes desde los archivos
            icono_con_foto = Image.FromFile("D:\\francisco\\Documents\\David Rodriguez\\Visual Estudio\\PROYECTO\\PRESENTACION\\PRESENTACION\\img\\conFoto.png");
            icono_sin_foto = Image.FromFile("D:\\francisco\\Documents\\David Rodriguez\\Visual Estudio\\PROYECTO\\PRESENTACION\\PRESENTACION\\img\\sinFoto.png");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        //REGISTRO REMATES ---------------------------------------------------------------------------
        private void buttonRegistrarRemate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaRemate = dateTimePickerFechaRemate.Value.Date;
                string rematador = textBoxRematador.Text.ToUpper();
                string tipoDeRemate = comboBoxRemate.Text;


                if (string.IsNullOrEmpty(rematador) || string.IsNullOrEmpty(tipoDeRemate))
                {
                    MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!repositorioUsuarios.ExisteRematador(rematador))
                {
                    MessageBox.Show("El rematador no existe. Por favor, verifique el nombre del rematador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (fechaRemate <= DateTime.Now)
                {
                    MessageBox.Show("La fecha del remate debe ser posterior a la fecha actual.", "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // No agregamos el remate si la fecha es inválida
                }

                // Verificar si ya existe un remate registrado para esta fecha
                if (repositorioUsuarios.ExisteRemateEnFecha(fechaRemate))
                {
                    MessageBox.Show("Ya existe un remate registrado para esta fecha.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear un nuevo objeto Remate
                Remate nuevoRemate = new Remate
                {
                    Fecha = dateTimePickerFechaRemate.Value,
                    HoraInicio = dateTimePickerHoraInicio.Value.TimeOfDay,
                    HoraFin = dateTimePickerHoraFin.Value.TimeOfDay,
                    Rematador = rematador,
                    TipoDeRemate = tipoDeRemate,

                };

                // Insertar el remate en la base de datos
                repositorioUsuarios.InsertarRemate(nuevoRemate);

                // Mantener los dateTimePickerHoraInicio y dateTimePickerHoraFin con la hora actual
                DateTime horaActual = DateTime.Now;
                dateTimePickerHoraInicio.Value = horaActual;
                dateTimePickerHoraFin.Value = horaActual;

                MessageBox.Show("Remate agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el remate: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ACTULIZAR GRILLA REMATES------------------------------------------------------------------------------------------------------------------
        public void ActualizarGrillaRemates()
        {
            try
            {
                // Obtener el ID del remate seleccionado actualmente
                int remateSeleccionadoId = (dataGridView1.CurrentRow != null) ? Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value) : -1;

                // Limpiar la grilla antes de actualizar
                dataGridView1.Rows.Clear();

                // Obtener los remates desde la base de datos
                List<Remate> remates = repositorioUsuarios.ObtenerRemates(Conexion.obtenerConexion());

                // Agregar los remates a la grilla
                foreach (Remate remate in remates)
                {
                    int rowIndex = dataGridView1.Rows.Add(remate.Id, remate.Fecha, remate.HoraInicio, remate.HoraFin, remate.Rematador, remate.TipoDeRemate);

                    if (remate.Id == remateSeleccionadoId)
                    {
                        dataGridView1.Rows[rowIndex].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los remates: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //LIMPIAR CAMPOS REMATES ---------------------------------------------------------------------------------
        private void LimpiarCamposRemate()
        {
            textBoxProveedorLote.Clear();
            textBoxPrecioDeReserva.Clear();
            textBoxRematador.Clear();
            textBoxCantidadEnLote.Clear();
            comboBoxRemate.SelectedIndex = -1; // Deseleccionar el ítem seleccionado
            dateTimePickerFechaRemate.Value = DateTime.Now.Date; // Restablecer la fecha
            dateTimePickerHoraInicio.Value = DateTime.Now.Date; // Hora actual 
            dateTimePickerHoraFin.Value = DateTime.Now.Date; // Hora actual 


        }





        //EVENTO FORMATO DE CELDA REMATE ----------------------------------------------------------------------
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {


            if (e.ColumnIndex == dataGridView1.Columns["VerLotes"].Index && e.RowIndex >= 0)
            {
                int remateId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                int lotesAsignados = negocioLotesRemates.ObtenerCantidadLotesAsignados(remateId);

                if (lotesAsignados > 0 && lotesAsignados < 2)//1
                {
                    e.CellStyle.BackColor = Color.FromArgb(7, 246, 7); // Verde 1
                    e.CellStyle.ForeColor = Color.Black;

                }
                else if (lotesAsignados > 1 && lotesAsignados < 3)//2
                {
                    e.CellStyle.BackColor = Color.FromArgb(35, 218, 9); // Verde 2
                    e.CellStyle.ForeColor = Color.Black;

                }
                else if (lotesAsignados > 2 && lotesAsignados < 4)//3
                {
                    e.CellStyle.BackColor = Color.FromArgb(50, 209, 9); // Verde 3
                    e.CellStyle.ForeColor = Color.Black;

                }
                else if (lotesAsignados > 3 && lotesAsignados < 5)//4
                {
                    e.CellStyle.BackColor = Color.FromArgb(228, 246, 9); // amarillo 1 
                    e.CellStyle.ForeColor = Color.Black;

                }
                else if (lotesAsignados > 4 && lotesAsignados < 6)//5
                {
                    e.CellStyle.BackColor = Color.FromArgb(235, 210, 9); // amarillo 2 
                    e.CellStyle.ForeColor = Color.Black;

                }
                else if (lotesAsignados > 5 && lotesAsignados < 7)//6
                {
                    e.CellStyle.BackColor = Color.FromArgb(246, 189, 9); // amarillo 3 
                    e.CellStyle.ForeColor = Color.Black;

                }
                else if (lotesAsignados > 6 && lotesAsignados < 8)//7
                {
                    e.CellStyle.BackColor = Color.FromArgb(246, 163, 9); // naranja 
                    e.CellStyle.ForeColor = Color.Black;

                }
                else if (lotesAsignados > 7 && lotesAsignados < 9)//8
                {
                    e.CellStyle.BackColor = Color.FromArgb(246, 90, 9); // rojo claro
                    e.CellStyle.ForeColor = Color.Black;
                }
                else if (lotesAsignados > 8 && lotesAsignados < 10)//9
                {
                    e.CellStyle.BackColor = Color.FromArgb(246, 25, 9); // Rojo 
                    e.CellStyle.ForeColor = Color.White;
                }
                else if (lotesAsignados >= 10) // Más de 10 lotes
                {
                    e.CellStyle.BackColor = Color.FromArgb(210, 9, 9); // Rojo fuerte
                    e.CellStyle.ForeColor = Color.White;
                    DataGridViewButtonCell cell = (DataGridViewButtonCell)dataGridView1.Rows[e.RowIndex].Cells["VerLotes"];
                    cell.FlatStyle = FlatStyle.Popup; // 3D style
                }
                else
                {
                    e.CellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    e.CellStyle.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
                }
            }

            //MOSTRAR FECHA SIN LA HORA--------------------------------------------------------------------
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Fecha"].Index)
            {
                if (e.Value is DateTime fecha)
                {
                    e.Value = fecha.ToShortDateString();
                    e.FormattingApplied = true;
                }
            }

            // MOSTRAR HORAS EN MINUTOS Y SEGUNDOS----------------------------------------------------------------------------------------------------------------
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridView1.Columns["hora_inicio"].Index || e.ColumnIndex == dataGridView1.Columns["hora_fin"].Index))
            {
                if (e.Value is TimeSpan hora)
                {
                    e.Value = hora.ToString(@"hh\:mm"); // Formatea la TimeSpan como "hh:mm"
                    e.FormattingApplied = true;
                }
            }

        }


        //EVENTO CLICK DE CELDA REMATES ----------------------------------------------------------------------------
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                selectedRemateGridRow = dataGridView1.Rows[e.RowIndex];
                MostrarDatosRemateSeleccionado(); // Llamamos al método para mostrar los datos en los campos


                dataGridView1.ClearSelection(); // Limpiar selecciones previas
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }

        }

        //MOSTRAR DATOS REMATE SELECCIONADO EN SUS RESPECTIVOS CAMPOS ------------------------------------------------------------
        private void MostrarDatosRemateSeleccionado()
        {
            if (selectedRemateGridRow != null)
            {
                // Obtener los valores de la fila seleccionada
                int remateId = Convert.ToInt32(selectedRemateGridRow.Cells["id"].Value);
                DateTime fechaRemate = Convert.ToDateTime(selectedRemateGridRow.Cells["fecha"].Value);
                TimeSpan horaInicio = TimeSpan.Parse(selectedRemateGridRow.Cells["hora_inicio"].Value.ToString());
                TimeSpan horaFin = TimeSpan.Parse(selectedRemateGridRow.Cells["hora_fin"].Value.ToString());
                string rematador = selectedRemateGridRow.Cells["rematador"].Value.ToString();
                string tipoRemate = selectedRemateGridRow.Cells["tipo_de_remate"].Value.ToString();
                // Mostrar los valores en los campos correspondientes
                dateTimePickerFechaRemate.Value = fechaRemate;
                dateTimePickerHoraInicio.Value = DateTime.Today.Add(horaInicio);
                dateTimePickerHoraFin.Value = DateTime.Today.Add(horaFin);
                textBoxRematador.Text = rematador;
                comboBoxRemate.Text = tipoRemate;

            }
        }



        //ELIMINAR REMATE ---------------------------------------------------------------------------------------------------------
        private void buttonEliminarRemate_Click(object sender, EventArgs e)
        {
            if (selectedRemateGridRow == null)
            {
                MessageBox.Show("Seleccione el remate que desea eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedRemateId = Convert.ToInt32(selectedRemateGridRow.Cells["id"].Value);

            // Crear una instancia de NegocioLotesRemates
            NegocioLotesRemates negocioLotesRemates = new NegocioLotesRemates();

            if (negocioLotesRemates.ObtenerCantidadLotesAsignados(selectedRemateId) > 0)
            {
                MessageBox.Show("No se puede eliminar el remate porque tiene lotes asignados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // No proceder con la eliminación
            }

            DialogResult result = MessageBox.Show("¿Desea eliminar este remate?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Eliminar los lotes asignados al remate
                    negocioLotesRemates.EliminarLotesAsignados(selectedRemateId);

                    // Eliminar el remate
                    negocioLotesRemates.EliminarRemate(selectedRemateId);

                    MessageBox.Show("Remate eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarGrillaRemates(); // Actualizar la grilla de remates

                    selectedRemateGridRow = null; // Limpiar la selección de fila
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el remate: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //RECARGAR---------------------------------------------------------
        public void refresh()
        {

            // Limpiar la grilla antes de actualizar
            dataGridView1.Rows.Clear();// Limpiar la grilla antes de actualizar
            dataGridView2.Rows.Clear();

            LimpiarCamposRemate(); // Limpiar campos de texto
            LimpiarCamposLote();
            ActualizarGrillaRemates();
            ActualizarGrillaLotes();
            ActualizarColoresGrillaLotes();

            //DESASIGANR LOS LOTES ANTIGUOS QUE NO SE VENDIERON ---------------------------------------------------------------------------
            DateTime fechaLimite = DateTime.Today; //.AddDays(-30); // Por ejemplo, consideramos remates antiguos de hace 30 días o más
            negocioLotesRemates.DesasignarLotesNoVendidosEnRematesAntiguos(fechaLimite);

        }

        //BTN RECARGAR------------------------------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            refresh();


        }

        //REGISTRAR LOTE-------------------------------------------------------------------------------------------------------
        private void buttonRegistrarLote_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que todos los campos estén completos
                if (!CamposLoteCompletos())
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string proveedorLote = textBoxProveedorLote.Text.ToUpper();
                int precioBase = Convert.ToInt32(textBoxPrecioDeReserva.Text);
                string tipoLote = comboBoxTipoDeLote.Text;
                int cantidadLote = Convert.ToInt32(textBoxCantidadEnLote.Text);
                string descripcion = textBoxDescripcion.Text;

                if (!repositorioUsuarios.ExisteProveedorVendedor(proveedorLote))
                {
                    MessageBox.Show("El proveedor ingresado no existe o no tiene el rol de vendedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Lote nuevoLote = new Lote
                {
                    proveedor_lote = proveedorLote,
                    precio_base = precioBase,
                    tipo_de_lote = tipoLote,
                    cantidad_en_lote = cantidadLote,
                    descripcion = descripcion
                };

                repositorioUsuarios.InsertarLote(nuevoLote);

                MessageBox.Show("Lote agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                refresh(); // Actualizar la grilla de lotes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el lote: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //VERIRFCAR CAMPOS COMPLETO DE LOTES -----------------------------------------------
        private bool CamposLoteCompletos()
        {
            return !string.IsNullOrWhiteSpace(textBoxProveedorLote.Text) &&
                   !string.IsNullOrWhiteSpace(textBoxPrecioDeReserva.Text) &&
                   !string.IsNullOrWhiteSpace(comboBoxTipoDeLote.Text) &&
                   !string.IsNullOrWhiteSpace(textBoxCantidadEnLote.Text) &&
                   !string.IsNullOrWhiteSpace(textBoxDescripcion.Text);
        }

        //ACTUALIZAR GRILLA LOTES ----------------------------------------------------------------------------------------
        private void ActualizarGrillaLotes()
        {
            try
            {
                int loteSeleccionadoId = (dataGridView2.CurrentRow != null) ? Convert.ToInt32(dataGridView2.CurrentRow.Cells["id"].Value) : -1;

                List<Lote> lotes = repositorioUsuarios.ObtenerLotes();

                dataGridView2.Rows.Clear();

                foreach (Lote lote in lotes)
                {
                    int rowIndex = dataGridView2.Rows.Add(
                        lote.id,
                        lote.proveedor_lote,
                        lote.precio_base,
                        lote.tipo_de_lote,
                        lote.cantidad_en_lote,
                        lote.descripcion,
                        lote.imagen);

                    byte[] imagenLote = repositorioUsuarios.ObtenerImagenLote(lote.id);

                    if (imagenLote == null)
                    {
                        dataGridView2.Rows[rowIndex].Cells["Foto"].Value = icono_sin_foto;
                    }
                    else
                    {
                        dataGridView2.Rows[rowIndex].Cells["Foto"].Value = icono_con_foto;
                    }

                    if (lote.id == loteSeleccionadoId)
                    {
                        dataGridView2.Rows[rowIndex].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los lotes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //LIMPIAR CAMPOS LOTES -----------------------------------
        private void LimpiarCamposLote()
        {
            textBoxProveedorLote.Clear();
            textBoxPrecioDeReserva.Clear();
            comboBoxTipoDeLote.SelectedIndex = -1;
            textBoxCantidadEnLote.Clear();
            comboBoxTipoDeRemate.SelectedIndex = -1;
            textBoxDescripcion.Clear();

        }


        // EVENTO CLICK EN CONTENIDO DE LA GRILLA REMATES -----------------------------------------------------------------------------------------------
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["VerLotes"].Index)
            {
                int remateId = ObtenerIdRemateSeleccionado(); // Obten el ID del remate seleccionado
                DateTime fechaRemate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["fecha"].Value);
                string proveedor = ObtenerProveedorDelLoteSeleccionado(); // Obten el proveedor del lote seleccionado

                GestionDeLotesYRemates gestionRematesYLotes = new GestionDeLotesYRemates(); // Debes crear una instancia aquí
                LotesAsignadosAlRemate ventanaLotes = new LotesAsignadosAlRemate(remateId, fechaRemate, negocioLotesRemates, dataGridView2, gestionRematesYLotes, proveedor);
                ventanaLotes.ShowDialog();
                refresh();
            }
        }



        //OBTENER ID REMATE SELECCIONADO ---------------------------------------------------------------------------------------------------
        private int ObtenerIdRemateSeleccionado()
        {

            int idRemateSeleccionado = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
            return idRemateSeleccionado;
        }

        //OBTENER PROVEEDOR DE LOTE SELECCIONADO -----------------------------------------------------------------
        public string ObtenerProveedorDelLoteSeleccionado()
        {
            string proveedor = textBoxProveedorLote.Text.ToUpper();
            return proveedor;
        }


        //MODIFICAR REMATE --------------------------------------------------------------------------------------------------------
        private void buttonModificarRemate_Click(object sender, EventArgs e)
        {
            if (selectedRemateGridRow == null)
            {
                MessageBox.Show("Seleccione un remate para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Desea modificar este remate?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                // Verificar si el rematador existe
                string nuevoRematador = textBoxRematador.Text.ToUpper();
                string tipoDeRemate = comboBoxRemate.Text.ToUpper();

                if (!repositorioUsuarios.ExisteRematador(nuevoRematador))
                {
                    MessageBox.Show("El rematador no existe. Por favor, verifique el nombre del rematador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(nuevoRematador) || string.IsNullOrEmpty(tipoDeRemate))
                {
                    MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    int selectedRemateId = Convert.ToInt32(selectedRemateGridRow.Cells["id"].Value);

                    // Obtener los nuevos valores de los campos
                    DateTime nuevaFecha = dateTimePickerFechaRemate.Value.Date;
                    TimeSpan nuevaHoraInicio = dateTimePickerHoraInicio.Value.TimeOfDay;
                    TimeSpan nuevaHoraFin = dateTimePickerHoraFin.Value.TimeOfDay;

                    // Actualizar el remate en la base de datos
                    bool actualizado = repositorioUsuarios.ModificarRemate(selectedRemateId, nuevoRematador, nuevaFecha, nuevaHoraInicio, nuevaHoraFin, tipoDeRemate);

                    if (actualizado)
                    {
                        MessageBox.Show("Remate modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refresh();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar el remate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Limpiar los campos después de la modificación
                    LimpiarCamposRemate();
                    // selectedRemateGridRow = null; // Limpiar la selección de fila
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el remate: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        //ELIMINAR LOTE-----------------------------------------------------------------------------------
        private void buttonEliminarLote_Click(object sender, EventArgs e)
        {

            if (selectedLoteGridRow == null)
            {
                MessageBox.Show("Seleccione un lote para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedLoteId = Convert.ToInt32(selectedLoteGridRow.Cells["id"].Value);
            bool asignado = negocioLotesRemates.VerificarLoteAsignado(selectedLoteId);
           

            DialogResult result = MessageBox.Show("¿Desea eliminar este lote?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                if (asignado)
                {
                    MessageBox.Show("No se puede eliminar el lote porque está asignado a un remate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // No proceder con la eliminación
                }


                try
                {
                    // Crear una instancia de RepositorioUsuarios (esto puede variar dependiendo de cómo estés manejando las instancias)
                    RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                    bool eliminado = repoUsuarios.EliminarLotePorId(selectedLoteId); // Cambio aquí para usar el ID del lote

                    if (eliminado)
                    {
                        MessageBox.Show("Lote eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refresh(); // Actualizar la grilla de lotes
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el lote.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el lote: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //SOLO NUMEROS EN TEXTBOX PRECIO RESERVA ---------------------------------------------------------------------
        private void textBoxPrecioBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada es un número o la tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Cancelar la entrada de la tecla si no es un número
            }

        }

        //SOLO NUMERO EN CANTIDAD EN LOTE ------------------------------------------------------------------
        private void textBoxCantidadAnimales_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada es un número o la tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Cancelar la entrada de la tecla si no es un número
            }
        }




        //MODIFICAR LOTE---------------------------------------------------------------------------
        private void buttonModificarLote_Click(object sender, EventArgs e)
        {

            if (selectedLoteGridRow == null)
            {
                MessageBox.Show("Seleccione un lote para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Verificar que todos los campos estén completos
            if (!CamposLoteCompletos())
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Obtener los nuevos valores de los campos
                int selectedLoteId = Convert.ToInt32(selectedLoteGridRow.Cells["id"].Value);
                string nuevoProveedorLote = textBoxProveedorLote.Text.ToUpper();
                int nuevoPrecioBase = Convert.ToInt32(textBoxPrecioDeReserva.Text);
                string nuevoTipoDeLote = comboBoxTipoDeLote.Text;
                int nuevaCantidadEnLote = Convert.ToInt32(textBoxCantidadEnLote.Text);
                string nuevaDescripcion = textBoxDescripcion.Text;

                // Crear un objeto Lote con los nuevos valores
                Lote loteModificado = new Lote
                {
                    id = selectedLoteId,
                    proveedor_lote = nuevoProveedorLote,
                    precio_base = nuevoPrecioBase,
                    tipo_de_lote = nuevoTipoDeLote,
                    cantidad_en_lote = nuevaCantidadEnLote,
                    descripcion = nuevaDescripcion
                };

                // Modificar el lote en la base de datos
                bool actualizado = repositorioUsuarios.ModificarLote(loteModificado);

                if (actualizado)
                {
                    MessageBox.Show("Lote modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refresh();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el lote.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el lote: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        //EVENTO CLICK DE CELDA EN LA GRILLA LOTES ----------------------------------------------------------------
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedLoteGridRow = dataGridView2.Rows[e.RowIndex];
                selectedLoteId = Convert.ToInt32(selectedLoteGridRow.Cells["id"].Value);
                MostrarDatosLoteSeleccionado();



                dataGridView2.ClearSelection();
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
        }

        private void ActualizarComboboxesSegunTipoDeLote(string tipoDeLote)
        {
            updatingComboboxes = true;

            if (tipoDeLote == "OVINO" || tipoDeLote == "BOVINO" || tipoDeLote == "PORCINO" || tipoDeLote == "EQUINO")
            {
                comboBoxTipoDeRemate.SelectedItem = "GANADO";
                comboBoxTipoDeLote.SelectedItem = tipoDeLote;
            }
            else if (tipoDeLote == "MAQUINA 1" || tipoDeLote == "MAQUINA 2" || tipoDeLote == "MAQUINA 3")
            {
                comboBoxTipoDeRemate.SelectedItem = "MAQUINARIA";
                comboBoxTipoDeLote.SelectedItem = tipoDeLote;
            }

            updatingComboboxes = false;
        }

        // MOSTRAR DATOS LOTE SELECCIONADO ----------------------------------------------------------------------
        private void MostrarDatosLoteSeleccionado()
        {
            if (selectedLoteGridRow != null)
            {
                // Mostrar los datos del lote en los campos de texto
                textBoxProveedorLote.Text = selectedLoteGridRow.Cells["proveedor_lote"].Value.ToString();
                textBoxPrecioDeReserva.Text = selectedLoteGridRow.Cells["precio_base"].Value.ToString();
                comboBoxTipoDeLote.SelectedItem = selectedLoteGridRow.Cells["tipo_de_lote"].Value.ToString();
                textBoxCantidadEnLote.Text = selectedLoteGridRow.Cells["cantidad_en_lote"].Value.ToString();
                textBoxDescripcion.Text = selectedLoteGridRow.Cells["descripcion"].Value.ToString();

                // Verificar el tipo de lote seleccionado y actualizar los combobox
                string tipoDeLoteSeleccionado = selectedLoteGridRow.Cells["tipo_de_lote"].Value.ToString();
                ActualizarComboboxesSegunTipoDeLote(tipoDeLoteSeleccionado);
            }
        }

        private void comboBoxTipoAnimal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //OBTENER TIPO LOTE SELECCIONADO EN GRILLA LOTES ----------------------------------------------
        private string ObtenerTipoLoteSeleccionado()
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                return selectedRow.Cells["tipo_de_lote"].Value.ToString();
            }

            return ""; // Retorna un valor por defecto si no hay lote seleccionado
        }


        //ASIGNAR LOTE A UN REMATE ---------------------------------------------------------------------------------------------
        private void button1_Click_1(object sender, EventArgs e)
        {




            if (selectedRemateGridRow == null || selectedLoteGridRow == null)
            {
                MessageBox.Show("Seleccione un remate y el lote que desea cargar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            int remateId = Convert.ToInt32(selectedRemateGridRow.Cells[dataGridView1.Columns["id"].Index].Value);
            int loteId = Convert.ToInt32(selectedLoteGridRow.Cells[dataGridView1.Columns["id"].Index].Value);

            // Obtener el tipo de remate del remate seleccionado
            string tipoRemateSeleccionado = negocioLotesRemates.ObtenerTipoRemate(remateId);

            // Obtener el tipo de lote del lote seleccionado (ya obtenido en el evento de selección de la grilla de lotes)
            string tipoLoteSeleccionado = ObtenerTipoLoteSeleccionado();
            if (negocioLotesRemates.LoteYaAsignado(loteId))
            {
                MessageBox.Show("Este lote ya está cargado en un remate.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Verificar y mostrar mensajes según el tipo de remate y tipo de lote
            if ((tipoRemateSeleccionado == "MAQUINARIA" && (tipoLoteSeleccionado == "MAQUINA 1" || tipoLoteSeleccionado == "MAQUINA 2" || tipoLoteSeleccionado == "MAQUINA 3")) ||
                (tipoRemateSeleccionado == "GANADO" && (tipoLoteSeleccionado == "OVINO" || tipoLoteSeleccionado == "BOVINO" || tipoLoteSeleccionado == "PORCINO" || tipoLoteSeleccionado == "EQUINO")))
            {
                // Puedes cargar el lote al remate
                negocioLotesRemates.AsignarLoteARemate(remateId, loteId);

                ActualizarGrillaLotesAsignados(remateId);
                refresh();

                MessageBox.Show("Lote cargado al remate correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El tipo de lote y el tipo de remate no coinciden.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            int lotesAsignados = negocioLotesRemates.ObtenerCantidadLotesAsignados(remateId);
            if (lotesAsignados >= 10)
            {
                MessageBox.Show("El remate ya tiene cargados 10 lotes. No se puede cargar más lotes a este remate.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }


        //ACTUALIZAR COLORES DE LA GRILLA LOTES PARA LOS LOTES ASIGNADOS A UN REMATE ------------------------------------------------------------------------
        private void ActualizarColoresGrillaLotes()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                int loteId = Convert.ToInt32(row.Cells["Id"].Value);
                bool asignado = negocioLotesRemates.VerificarLoteAsignado(loteId);

                if (asignado)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dataGridView2.DefaultCellStyle.BackColor;
                }
            }
        }

        //ACTUALIZAR GRILLA LOTES ASIGNADOS A UN REMATE ----------------------------------------------------------------
        private void ActualizarGrillaLotesAsignados(int remateId)
        {
            try
            {
                List<Lote> lotesAsignados = negocioLotesRemates.ObtenerLotesAsignadosPorRemate(remateId);
                GestionDeLotesYRemates gestionRematesYLotes = new GestionDeLotesYRemates();
                string proveedor = ObtenerProveedorDelLoteSeleccionado();
                LotesAsignadosAlRemate ventanaLotesAsignados = new LotesAsignadosAlRemate(remateId, fechaRemate, negocioLotesRemates, dataGridView2, gestionRematesYLotes, proveedor); // Crea una instancia del formulario
                ventanaLotesAsignados.dataGridViewLotesAsignados.Rows.Clear();


                foreach (Lote lote in lotesAsignados)
                {
                    ventanaLotesAsignados.dataGridViewLotesAsignados.Rows.Add(lote.id, lote.proveedor_lote, lote.precio_base, lote.tipo_de_lote, lote.cantidad_en_lote, lote.descripcion);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la grilla de lotes asignados: " + ex.Message);
            }
        }


        private void textBoxProveedorLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancelar la entrada de la tecla si no es una letra
            }
        }

        private void textBoxRematador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancelar la entrada de la tecla si no es una letra
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {

            // Cambiar los colores al pasar el mouse sobre el botón
            button1.BackColor = Color.FromArgb(255, 200, 200); // Rojo claro
            button1.ForeColor = Color.FromArgb(255, 100, 100);
            button1.FlatAppearance.BorderColor = Color.Red; // Cambiar color del borde

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }


        //MANEJAR FLECHITAS DE ARRIBA ABAJO EN GRILLA REAMATES ------------------------------------------------------------------
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                DataGridViewRow row = null;

                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    if (e.KeyCode == Keys.Up && rowIndex > 0)
                    {
                        row = dataGridView1.Rows[rowIndex - 1];
                    }
                    else if (e.KeyCode == Keys.Down && rowIndex < dataGridView1.Rows.Count - 1)
                    {
                        row = dataGridView1.Rows[rowIndex + 1];
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                }
                else if (e.KeyCode == Keys.Down)
                {
                    row = dataGridView1.Rows[0];
                }

                if (row != null)
                {
                    // Obtener los datos de la fila seleccionada
                    DataGridViewRow selectedRow = row;
                    selectedRemateGridRow = row;
                    MostrarDatosRemateSeleccionado();
                    dataGridView1.ClearSelection();
                    row.Selected = true;
                }
            }
        }

        //MANERJAR FLECHITAS ARRIBA ABAJO EN GRILLA REMATES ----------------------------------------------------------------
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                DataGridViewRow row = null;

                if (dataGridView2.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView2.SelectedCells[0].RowIndex;
                    if (e.KeyCode == Keys.Up && rowIndex > 0)
                    {
                        row = dataGridView2.Rows[rowIndex - 1];
                    }
                    else if (e.KeyCode == Keys.Down && rowIndex < dataGridView2.Rows.Count - 1)
                    {
                        row = dataGridView2.Rows[rowIndex + 1];
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                }
                else if (e.KeyCode == Keys.Down)
                {
                    row = dataGridView2.Rows[0];
                }

                if (row != null)
                {
                    // Obtener los datos de la fila seleccionada
                    selectedLoteGridRow = row;
                    selectedLoteId = Convert.ToInt32(selectedLoteGridRow.Cells["id"].Value);
                    MostrarDatosLoteSeleccionado();
                    dataGridView2.ClearSelection(); // Limpiar selecciones previas
                    row.Selected = true; // Seleccionar la fila actual
                }
            }
        }

        private void textBoxProveedorLote_TextChanged(object sender, EventArgs e)
        {

        }

        //ASIGNACION A COMBOBOX TIPO DE LOTE SEGUN LA SELECCION DE TIPO DE REMATE ----------------------------------------------
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoRemateSeleccionado = comboBoxTipoDeRemate.Text;

            comboBoxTipoDeLote.Items.Clear();

            if (tipoRemateSeleccionado == "GANADO")
            {
                comboBoxTipoDeLote.Items.Add("OVINO");
                comboBoxTipoDeLote.Items.Add("BOVINO");
                comboBoxTipoDeLote.Items.Add("PORCINO");
                comboBoxTipoDeLote.Items.Add("EQUINO");
            }
            else if (tipoRemateSeleccionado == "MAQUINARIA")
            {
                comboBoxTipoDeLote.Items.Add("MAQUINA 1");
                comboBoxTipoDeLote.Items.Add("MAQUINA 2");
                comboBoxTipoDeLote.Items.Add("MAQUINA 3");
            }

            if (comboBoxTipoDeLote.Items.Count > 0)
            {
                comboBoxTipoDeLote.SelectedIndex = 0; // Seleccionar el primer elemento por defecto
            }
        }


        //BOTON AÑADIR FOTO A UN LOTE -------------------------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridView2.CurrentRow;
                if (selectedLoteGridRow == null)
                {
                    MessageBox.Show("Seleccione el lote que desea ver o añadir una foto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int id = Convert.ToInt32(selectedRow.Cells["id"].Value);
                string proveedorLote = selectedRow.Cells["proveedor_lote"].Value.ToString();
                int precioBase = Convert.ToInt32(selectedRow.Cells["precio_base"].Value);
                string tipoDeLote = selectedRow.Cells["tipo_de_lote"].Value.ToString();
                int cantidadEnLote = Convert.ToInt32(selectedRow.Cells["cantidad_en_lote"].Value);
                string descripcionLote = selectedRow.Cells["descripcion"].Value.ToString();

                Lote selectedLote = new Lote
                {
                    id = id,
                    proveedor_lote = proveedorLote,
                    precio_base = precioBase,
                    tipo_de_lote = tipoDeLote,
                    cantidad_en_lote = cantidadEnLote,
                    descripcion = descripcionLote
                };

                // Obtén los datos de la imagen desde la base de datos
                byte[] imagenData = negocioLotesRemates.ObtenerFotoDelLote(selectedLote.id);

                using (AñadirFotoAlLote añadirFotoAlLote = new AñadirFotoAlLote(selectedLote))
                {
                    // Verifica si hay imagen y carga la imagen en el PictureBoxImagen
                    if (imagenData != null && imagenData.Length > 0)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(imagenData))
                        {
                            añadirFotoAlLote.PictureBox1.Image = Image.FromStream(memoryStream); // Carga la imagen desde el MemoryStream
                        }
                    }
                    else
                    {
                        añadirFotoAlLote.PictureBox1.Image = null; // No hay imagen disponible
                    }

                    if (añadirFotoAlLote.ShowDialog() == DialogResult.OK)
                    {
                        refresh();
                    }
                }
            }
        }

        private void textBoxPrecioDeReserva_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTipoDeRemate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                string tipoDeLoteSeleccionado = selectedRow.Cells["tipo_de_lote"].Value.ToString();


            }
        }


        private string ObtenerEstadoImagen(Lote lote)
        {
            if (lote.imagen != null && lote.imagen.Length > 0)
            {
                return "OK";
            }
            else
            {
                return "SIN FOTO";
            }
        }
        //ELIMINAR REMATE--------------------------------------------------------------------------------------------------------
        private void buttonEliminarRemate_MouseEnter(object sender, EventArgs e)
        {

            // Cambiar los colores al pasar el mouse sobre el botón
            buttonEliminarRemate.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonEliminarRemate.ForeColor = Color.FromArgb(160, 160, 160);
            buttonEliminarRemate.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde

        }

        private void buttonEliminarRemate_MouseLeave(object sender, EventArgs e)
        {

            // Restaurar los colores al salir el mouse del botón
            buttonEliminarRemate.BackColor = Color.Transparent;
            buttonEliminarRemate.ForeColor = Color.White;
            buttonEliminarRemate.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }

        //MODIFICAR REMATE--------------------------------------------------------------------------------------------------------
        private void buttonModificarRemate_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonModificarRemate.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonModificarRemate.ForeColor = Color.FromArgb(160, 160, 160);
            buttonModificarRemate.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde

        }

        private void buttonModificarRemate_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            buttonModificarRemate.BackColor = Color.Transparent;
            buttonModificarRemate.ForeColor = Color.White;
            buttonModificarRemate.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }

        //REGISTRAR REMATE--------------------------------------------------------------------------------------------------------
        private void buttonRegistrarRemate_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonRegistrarRemate.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonRegistrarRemate.ForeColor = Color.FromArgb(160, 160, 160);
            buttonRegistrarRemate.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde

        }

        private void buttonRegistrarRemate_MouseLeave(object sender, EventArgs e)
        {

            // Restaurar los colores al salir el mouse del botón
            buttonRegistrarRemate.BackColor = Color.Transparent;
            buttonRegistrarRemate.ForeColor = Color.White;
            buttonRegistrarRemate.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }








        //ELIMINAR LOTE--------------------------------------------------------------------------------------------------------
        private void buttonEliminarLote_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonEliminarLote.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonEliminarLote.ForeColor = Color.FromArgb(160, 160, 160);
            buttonEliminarLote.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde

        }

        private void buttonEliminarLote_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            buttonEliminarLote.BackColor = Color.Transparent;
            buttonEliminarLote.ForeColor = Color.White;
            buttonEliminarLote.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }

        //MODIFICAR LOTE--------------------------------------------------------------------------------------------------------
        private void buttonModificarLote_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonModificarLote.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonModificarLote.ForeColor = Color.FromArgb(160, 160, 160);
            buttonModificarLote.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde
        }

        private void buttonModificarLote_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            buttonModificarLote.BackColor = Color.Transparent;
            buttonModificarLote.ForeColor = Color.White;
            buttonModificarLote.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }



        //REGISTRAR LOTE -------------------------------------------------------------------------------------------------------

        private void buttonRegistrarLote_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            buttonRegistrarLote.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            buttonRegistrarLote.ForeColor = Color.FromArgb(160, 160, 160);
            buttonRegistrarLote.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde
        }

        private void buttonRegistrarLote_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            buttonRegistrarLote.BackColor = Color.Transparent;
            buttonRegistrarLote.ForeColor = Color.White;
            buttonRegistrarLote.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell.OwningColumn.Name == "VerLotes" && cell is DataGridViewButtonCell)
                {
                    dataGridView1.Cursor = Cursors.Hand;
                }
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell.OwningColumn.Name == "VerLotes" && cell is DataGridViewButtonCell)
                {
                    dataGridView1.Cursor = Cursors.Default;
                }
            }
        }



    }//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
}
