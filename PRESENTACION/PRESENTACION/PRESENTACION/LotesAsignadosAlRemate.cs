using MySql.Data.MySqlClient;
using NEGOCIO;
using NEGOCIO.NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PRESENTACION.PRESENTACION
{
    public partial class LotesAsignadosAlRemate : Form
    {
        private readonly MySqlConnection _conexion;
        private readonly NegocioLotesRemates _negocioLotesRemates;
        private readonly int _idRemateSeleccionado;
        private int idRemateSeleccionado = -1;
        private int remateId;
        private NegocioLotesRemates negocioLotesRemates;
        private RepositorioUsuarios repositorioUsuarios;
        private int selectedLoteId = -1; // Inicializado con un valor que no corresponde a ningún lote
        private DataGridView dataGridView2;
        private GestionDeLotesYRemates gestionRematesYLotes;
        private DateTime FechaRemate;
        public string Proveedor { get; private set; }
        public LotesAsignadosAlRemate(int remateId, DateTime fechaRemate, NegocioLotesRemates negocioLotesRemates, DataGridView dataGridView2, GestionDeLotesYRemates gestionRematesYLotes, string proveedor)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.dataGridView2 = dataGridView2; // Asignar el objeto pasado al campo local
            this.MaximizeBox = false;
            //this.FormBorderStyle = FormBorderStyle.None; // Eliminar el borde de la ventana
            this.Proveedor = proveedor; // Almacena el proveedor en un campo de la ventana
            FechaRemate = fechaRemate;

            dataGridViewLotesAsignados.Columns.Add("id", " N° LOTE ");
            dataGridViewLotesAsignados.Columns.Add("lote", "PROVEEDOR ");
            dataGridViewLotesAsignados.Columns.Add("precio_base", "$ RESERVA");
            dataGridViewLotesAsignados.Columns.Add("tipo_de_lote", "TIPO LOTE");
            dataGridViewLotesAsignados.Columns.Add("cantidad_en_lote", " CANT EN LOTE");
            dataGridViewLotesAsignados.Columns.Add("descripcion", " DESCRIPCIÓN");
            dataGridViewLotesAsignados.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewLotesAsignados.Columns[0].DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewLotesAsignados.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            dataGridViewLotesAsignados.Columns[0].Width = 80;
            idRemateSeleccionado = remateId;

            _conexion = Conexion.obtenerConexion();
            _idRemateSeleccionado = idRemateSeleccionado;
            this.remateId = remateId;
            this.negocioLotesRemates = negocioLotesRemates;

            repositorioUsuarios = new RepositorioUsuarios(); // Inicializar el repositorio aquí

            ActualizarGrillaLotesAsignados(idRemateSeleccionado);
            this.gestionRematesYLotes = gestionRematesYLotes;



        }

        private void ActualizarGrillaLotesAsignados(int remateId)
        {
            try
            {
                List<Lote> lotesAsignados = negocioLotesRemates.ObtenerLotesAsignadosPorRemate(remateId);

                dataGridViewLotesAsignados.Rows.Clear();

                foreach (Lote lote in lotesAsignados)
                {
                    dataGridViewLotesAsignados.Rows.Add(
                        lote.id,
                        lote.proveedor_lote,
                        lote.precio_base,
                        lote.tipo_de_lote,
                        lote.cantidad_en_lote,
                        lote.descripcion
                    );
                }
                // Actualiza el lblRemate con la fecha del remate
                lblRemate.Text = "Lotes para el remate del " + FechaRemate.ToString("dd/MM/yyyy");
                ActualizarColoresLotesVendidos();
                //limpiarCamposLotesAsignados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la grilla de lotes asignados: " + ex.Message);
            }
        }

        private void VentanaLotesAsignados_Load(object sender, EventArgs e)
        {
            ActualizarGrillaLotesAsignados(remateId);
            dataGridViewLotesAsignados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridViewLotesAsignados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Limpiar selecciones previas
            // dataGridViewLotesAsignados.Rows[e.RowIndex].Selected = true; // Seleccionar toda la fila
        }

        //BOTON DESASIGNAR ------------------------------------------------
        private void buttonDesasignarLote_Click(object sender, EventArgs e)
        {


            if (dataGridViewLotesAsignados.SelectedRows.Count > 0) // Verifica si hay al menos una fila seleccionada
            {

                DataGridViewRow selectedRow = dataGridViewLotesAsignados.SelectedRows[0];
                int loteId = Convert.ToInt32(selectedRow.Cells["id"].Value); // Obtén el ID del lote seleccionado

                if (negocioLotesRemates.ExisteLoteVendido(loteId))
                {
                    MessageBox.Show("No se puede desasignar. El lote ha sido vendido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    DialogResult result = MessageBox.Show("¿Desea desasignar este lote del remate?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // Llama al método de negocio para desasignar el lote
                        negocioLotesRemates.DesasignarLoteDeRemate(loteId);

                        MessageBox.Show("Lote desasignado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Actualiza la grilla de lotes
                        ActualizarGrillaLotesAsignados(remateId);
                        ActualizarColoresLotesVendidos();
                        gestionRematesYLotes.refresh();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al desasignar el lote: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un lote para desasignar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }


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

        private void dataGridViewLotesAsignados_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblRemate_Click(object sender, EventArgs e)
        {

        }

        private void VentanaLotesAsignados_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            gestionRematesYLotes.refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Evento que se dispara al seleccionar un remate en la interfaz

        private void ActualizarColoresLotesVendidos()
        {
            foreach (DataGridViewRow row in dataGridViewLotesAsignados.Rows)
            {
                int loteId = Convert.ToInt32(row.Cells["Id"].Value);
                bool asignado = negocioLotesRemates.VerificarLoteVendido(loteId);

                if (asignado)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dataGridViewLotesAsignados.DefaultCellStyle.BackColor;
                }
            }
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
        public int ObtenerIdLoteSeleccionado()
        {
            // Aquí obtienes y devuelves el ID del remate seleccionado en esta ventana
            int idLoteSeleccionado = Convert.ToInt32(dataGridViewLotesAsignados.SelectedRows[0].Cells["id"].Value);
            return idLoteSeleccionado;
        }


        /* //BOTON VENDER LOTE ---------------------------------------------------------------------------
         private void button2_Click_1(object sender, EventArgs e)
         {
             // Obtener el comprador en mayúsculas y verificar su existencia y rol
             string compradorLote = textBoxComprador.Text.ToUpper();

             if (!repositorioUsuarios.ExisteComprador(compradorLote))
             {
                 MessageBox.Show("El cliente ingresado no existe o no tiene el rol de comprador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             // Verificar que se haya ingresado el precio de venta
             if (!int.TryParse(textBoxPrecioDeVenta.Text, out int precioVenta))
             {
                 MessageBox.Show("Ingrese un precio de venta válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }

             try
             {
                 int idRemate = remateId;
                 int idLote = ObtenerIdLoteSeleccionado();

                 // Aquí obtén la lista de lotes asignados por medio del método
                 List<Lote> lotesAsignados = negocioLotesRemates.ObtenerLotesAsignadosPorRemate(idRemate);

                 // Obtener la información del lote seleccionado de la lista de lotes asignados
                 Lote loteSeleccionado = lotesAsignados.FirstOrDefault(lote => lote.id == idLote);

                 if (loteSeleccionado == null)
                 {
                     MessageBox.Show("Error al obtener información del lote seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }

                 int precioDeVenta = Convert.ToInt32(textBoxPrecioDeVenta.Text);

                 LoteVendido loteVendido = new LoteVendido
                 {
                     IdRemate = idRemate,
                     IdLote = idLote,
                     FechaVenta = DateTime.Today,
                     Proveedor = loteSeleccionado.proveedor_lote, // Usar el proveedor del lote seleccionado
                     Comprador = compradorLote,
                     PrecioDeVenta = precioDeVenta
                 };

                 negocioLotesRemates.MarcarLoteComoVendido(loteVendido);

                 MessageBox.Show("Lote vendido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 ActualizarGrillaLotesAsignados(remateId);

                 // ActualizarColoresLotesVendidos();

             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error al marcar el lote como vendido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }


         }

         public void limpiarCamposLotesAsignados()
         {
             textBoxPrecioDeVenta.Clear();
             textBoxComprador.Clear();
         }
 */
        /*
        //boton quitar lista de vendidos-----------------------------------------------------------------------------------------
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewLotesAsignados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione el lote que desea quitar de la lista de vendidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewLotesAsignados.SelectedRows[0];
            int idRemate = remateId; // Reemplaza esto con el código para obtener el ID del remate seleccionado
            int idLote = Convert.ToInt32(selectedRow.Cells["id"].Value); // Obtén el ID del lote seleccionado

            DialogResult result = MessageBox.Show("¿Está seguro de quitar este lote de la lista de vendidos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Llamar al método en la instancia de la capa de negocio para quitar el lote vendido
                    negocioLotesRemates.QuitarLoteVendido(idRemate, idLote);

                    MessageBox.Show("Lote quitado de la lista de vendidos exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizar la grilla de lotes asignados y otros elementos según sea necesario
                    ActualizarGrillaLotesAsignados(remateId);
                    ActualizarColoresLotesVendidos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al quitar el lote de la lista de vendidos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        */
        private void textBoxPrecioDeVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxComprador_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una letra, un número o una tecla de control
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void textBoxPrecioDeVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es un número o una tecla de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
                                          
            ActualizarGrillaLotesAsignados(remateId);
            ActualizarColoresLotesVendidos();
            gestionRematesYLotes.refresh();
        }
    }//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
}