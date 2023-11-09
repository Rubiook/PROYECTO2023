using MercadoPago.Client.Preference;
using MercadoPago.Config;
using NEGOCIO.NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;

namespace PRESENTACION.PRESENTACION
{
    public partial class Preeventas : Form
    {

        private LotesAsignados negocioLotesRemates = new LotesAsignados();
        private string preferenceId;
       
       

        public Preeventas()
        {
            InitializeComponent();
            ConfigurarFormulario();
            ConfigurarColumnasDataGridView();
            CargarPreeventasEnGrilla();
            ActualizarColoresGrillaPreeventas();
            

        }


        private void ConfigurarFormulario()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

        }


        private void ConfigurarColumnasDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns.Add("id_lote", "N° LOTE");
            dataGridView1.Columns.Add("id_usuario", "N° USUARIO");
            dataGridView1.Columns.Add("precio_de_venta", "$ VENTA");
            dataGridView1.Columns.Add("nombre_usuario", "NOMBRE");
            dataGridView1.Columns.Add("apellido_usuario", "APELLIDO");
            dataGridView1.Columns.Add("correo_usuario", "CORREO");
            dataGridView1.Columns.Add("celular_usuario", "CELULAR");
            dataGridView1.Columns.Add("proveedor_lote", "PROVEEDOR LOTE");
            dataGridView1.Columns.Add("precio_base_lote", "$ RESERVA");
            dataGridView1.Columns.Add("tipo_lote", "TIPO LOTE");
            dataGridView1.Columns.Add("cantidad_en_lote", "CANTIDAD");
            dataGridView1.Columns.Add("descripcion_lote", "DESCRIPCIÓN");
            dataGridView1.Columns.Add("id_remate", "N° REMATE");
            dataGridView1.Columns.Add("fecha_creacion", "FECHA");


            // Ajustar tamaños de columnas según tus necesidades
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;

            // Opcional: Cambiar el estilo de fuente y encabezados
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
        }


        private void CargarPreeventasEnGrilla()
        {
            try
            {
                var preeventas = negocioLotesRemates.ObtenerPreeventas();

                // Limpiar datos anteriores en la grilla (si es necesario)
                dataGridView1.Rows.Clear();

                foreach (var preventa in preeventas)
                {
                    dataGridView1.Rows.Add(
                          preventa.id,
                         preventa.id_lote,
                        preventa.id_usuario,
                        preventa.precio_de_venta,
                        preventa.nombre_usuario,
                        preventa.apellido_usuario,
                        preventa.correo_usuario,
                        preventa.celular_usuario,
                        preventa.proveedor_lote,
                        preventa.precio_base_lote,
                        preventa.tipo_lote,
                        preventa.cantidad_en_lote,
                        preventa.descripcion_lote,
                        preventa.id_remate,
                        preventa.fecha_creacion.ToString("dd/MM/yyyy")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las preeventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




        private void buttonVenderLote_Click(object sender, EventArgs e)
        {
            try
            {
                int filaSeleccionada = dataGridView1.CurrentRow.Index;

                // string apellidoComprador = dataGridView1.Rows[filaSeleccionada].Cells["apellido_usuario"].Value.ToString();
                // int idUsuarioComprador = Convert.ToInt32(dataGridView1.Rows[filaSeleccionada].Cells["id_usuario"].Value);

                // Obtener el id_preventa basado en el id_lote y id_remate

                int idLote = Convert.ToInt32(dataGridView1.Rows[filaSeleccionada].Cells["id_lote"].Value);
                int idPreventa = Convert.ToInt32(dataGridView1.Rows[filaSeleccionada].Cells["id"].Value);
                int idRemate = Convert.ToInt32(dataGridView1.Rows[filaSeleccionada].Cells["id_remate"].Value);
                // string proveedorLote = dataGridView1.Rows[filaSeleccionada].Cells["proveedor_lote"].Value.ToString();
                // string compradorLote = dataGridView1.Rows[filaSeleccionada].Cells["nombre_usuario"].Value.ToString();
                // decimal precioVenta = Convert.ToDecimal(dataGridView1.Rows[filaSeleccionada].Cells["precio_de_venta"].Value);





                negocioLotesRemates.MarcarLoteComoVendido(idLote, idRemate, idPreventa);

                MessageBox.Show("Lote vendido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPreeventasEnGrilla(); // Recargar la grilla de preeventas para reflejar los cambios

                ActualizarColoresGrillaPreeventas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al marcar el lote como vendido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonQuitarLoteDeVendidos_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione el lote que desea quitar de la lista de vendidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int idRemate = Convert.ToInt32(selectedRow.Cells["id_remate"].Value);
            int idLote = Convert.ToInt32(selectedRow.Cells["id_lote"].Value);

            DialogResult result = MessageBox.Show("¿Está seguro de quitar este lote de la lista de vendidos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Llamar al método en la instancia de la capa de negocio para quitar el lote vendido
                    negocioLotesRemates.QuitarLoteVendido(idRemate, idLote);

                    MessageBox.Show("Lote quitado de la lista de vendidos exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarPreeventasEnGrilla(); // Recargar la grilla de preeventas para reflejar los cambios
                    ActualizarColoresGrillaPreeventas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al quitar el lote de la lista de vendidos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarColoresGrillaPreeventas()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int idLote = Convert.ToInt32(row.Cells["id_lote"].Value);
                bool vendido = negocioLotesRemates.VerificarLoteVendido(idLote);

                if (vendido)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                }
            }
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button3.BackColor = Color.FromArgb(255, 200, 200); // Rojo claro
            button3.ForeColor = Color.FromArgb(255, 100, 100);
            button3.FlatAppearance.BorderColor = Color.Red; // Cambiar color del borde

        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button3.BackColor = Color.Transparent;
            button3.ForeColor = Color.White;
            button3.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CargarPreeventasEnGrilla(); // Recargar la grilla de preeventas para reflejar los cambios
            ActualizarColoresGrillaPreeventas();
        }







        //BOTON PARA ABRIR VENTANANA WEB REFERENCIADA A MERCADO PAGO ---------------------------------------------
        private async void button1_Click(object sender, EventArgs e)
        {
            // Configurar el token de acceso de Mercado Pago
            MercadoPagoConfig.AccessToken = "TEST-3746812080475118-090321-81fbd038ee20be3c52907d87665957cf-416650804";

            if (dataGridView1.SelectedRows.Count > 0)
            {
                decimal precioVenta = ObtenerPrecioVentaDesdeGrilla();

                if (precioVenta > 0)
                {
                    try
                    {
                        // Crear preferencia de pago
                        var preference = new PreferenceRequest
                        {
                            Items = new List<PreferenceItemRequest>
                    {
                        new PreferenceItemRequest
                        {
                            Title = "Producto de prueba",
                            Quantity = 1,
                            CurrencyId = "UYU",
                            UnitPrice = precioVenta
                        }
                    }
                        };

                        var client = new PreferenceClient();
                        Preference preferenceResponse = await client.CreateAsync(preference);

                        string preferenceId = preferenceResponse.Id;

                        // Mostrar alerta de inicio de preferencia
                       // MessageBox.Show($"Preferencia creada exitosamente. Preference ID: {preferenceId}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Crear una instancia del formulario de pago y pasar el preferenceId
                        Pago formularioPago = new Pago(preferenceId);

                        // Mostrar el formulario de pago como un cuadro de diálogo modal
                        formularioPago.ShowDialog();

                        // Mostrar alerta de finalización de pago
                        MessageBox.Show("Proceso de pago completado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Mostrar alerta de error
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Mostrar alerta de precio no válido
                    MessageBox.Show("El precio de venta debe ser mayor que 0", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Mostrar alerta de ninguna venta seleccionada
                MessageBox.Show("Por favor, seleccione la venta que desea pagar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private decimal ObtenerPrecioVentaDesdeGrilla()
        {
            try
            {
                // Verificar si se ha seleccionado una fila en la grilla
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Obtener el índice de la fila seleccionada
                    int filaSeleccionada = dataGridView1.SelectedRows[0].Index;

                    // Obtener el valor de la celda "Precio de Venta" en la fila seleccionada
                    string precioVentaStr = dataGridView1.Rows[filaSeleccionada].Cells["precio_de_venta"].Value.ToString();

                    // Convertir el valor a decimal directamente
                    if (decimal.TryParse(precioVentaStr, out decimal precioVenta))
                    {
                        return precioVenta;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que pueda ocurrir al obtener el precio
                MessageBox.Show("Error al obtener el precio de venta desde la grilla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Si no se pudo obtener el precio, retorna 0 o un valor predeterminado
            return 0;
        }

        private void Preeventas_Load(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = false;
            }
        }


    }
}
