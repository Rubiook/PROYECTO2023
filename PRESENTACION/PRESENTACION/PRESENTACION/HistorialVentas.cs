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
    public partial class HistorialVentas : Form
    {
        private readonly NegocioLotesRemates negocioLotesRemates;

        public HistorialVentas()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            dataGridViewHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            negocioLotesRemates = new NegocioLotesRemates();

            dataGridViewHistorial.Columns.Add("id", " ID ");
            dataGridViewHistorial.Columns.Add("idremate", "N° REMATE");
            dataGridViewHistorial.Columns.Add("idlote", "N° LOTE");
            dataGridViewHistorial.Columns.Add("id_usuario_comprador", "N° COMPRADOR");
            //dataGridViewHistorial.Columns.Add("apellido_comprador", "APELLIDO COMPRADOR");
            dataGridViewHistorial.Columns.Add("comprador", "COMPRADOR");
            dataGridViewHistorial.Columns.Add("precio_venta", "$ VENTA");

            // columnas relacionadas con el lote

            dataGridViewHistorial.Columns.Add("precio_base", "$ RESERVA");
            dataGridViewHistorial.Columns.Add("proveedor_lote", "PROVEEDOR LOTE");
            dataGridViewHistorial.Columns.Add("tipo_de_lote", "TIPO DE LOTE");
            dataGridViewHistorial.Columns.Add("cantidad_en_lote", "CANTIDAD");
            dataGridViewHistorial.Columns.Add("descripcion", "DESCRIPCIÓN");
            dataGridViewHistorial.Columns.Add("fecha_venta", "FECHA VENTA");

            dataGridViewHistorial.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewHistorial.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            dataGridViewHistorial.Columns[0].Width = 50;
            dataGridViewHistorial.Columns[1].Width = 100;
            dataGridViewHistorial.Columns[2].Width = 80;
            dataGridViewHistorial.Columns[3].Width = 140;
            dataGridViewHistorial.Columns[4].Width = 160;

            ActualizarGrillaHistorialVentas();

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewLotesAsignados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void ActualizarGrillaHistorialVentas()
        {
            try
            {
                List<LoteVendido> historialVentas = negocioLotesRemates.ObtenerLotesVendidos();

                dataGridViewHistorial.Rows.Clear();

                foreach (LoteVendido venta in historialVentas)
                {
                    dataGridViewHistorial.Rows.Add(
                        venta.Id,
                        venta.IdRemate,
                        venta.IdLote,
                        venta.IdUsuarioComprador,
                        venta.Comprador,
                        venta.PrecioDeVenta,
                        venta.PrecioBase,
                        venta.ProveedorLote,
                        venta.TipoDeLote,
                        venta.CantidadEnLote,
                        venta.Descripcion,
                         venta.FechaVenta.ToString("dd/MM/yyyy")

                    //venta.ApellidoComprador      
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la grilla de historial de ventas: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ActualizarGrillaHistorialVentas();
        }
    }
}
