
using NEGOCIO.NEGOCIO;


using PdfSharp.Drawing;
using PdfPage = PdfSharp.Pdf.PdfPage;
using PdfDocument = PdfSharp.Pdf.PdfDocument;
using PdfSharp.Pdf.IO;
using PdfReader = PdfSharp.Pdf.IO.PdfReader;
using System.Windows.Forms;
using System.Diagnostics;

namespace PRESENTACION.PRESENTACION
{
    public partial class HistorialVentas : Form
    {
        private readonly NegocioBDD negocioLotesRemates;

        public HistorialVentas()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            dataGridViewHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            negocioLotesRemates = new NegocioBDD();

            dataGridViewHistorial.Columns.Add("id", "ID");
            dataGridViewHistorial.Columns.Add("idremate", "N° REMATE");
            dataGridViewHistorial.Columns.Add("idlote", "N° LOTE");
            dataGridViewHistorial.Columns.Add("id_usuario_comprador", "N° COMPRADOR");
            //dataGridViewHistorial.Columns.Add("comprador", "COMPRADOR");
            dataGridViewHistorial.Columns.Add("precio_venta", "$ VENTA");
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
                        venta.PrecioDeVenta,
                        venta.PrecioBase,
                        venta.ProveedorLote,
                        venta.TipoDeLote,
                        venta.CantidadEnLote,
                        venta.Descripcion,
                        venta.FechaVenta.ToString("dd/MM/yyyy")
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

        //BOTON GENERAR FACTURA --------------------------------------------------------------------------------
        private void buttonPdfFactura_Click(object sender, EventArgs e)
        {
            if (dataGridViewHistorial.SelectedRows.Count > 0)
            {
                int ventaId = Convert.ToInt32(dataGridViewHistorial.SelectedRows[0].Cells["id"].Value);
                LoteVendido venta = negocioLotesRemates.ObtenerVentaPorId(ventaId);
                string nombreArchivoPDF = $"factura_numero_{venta.Id}.pdf";

                // Obtener el directorio de trabajo actual del ejecutable
                string directorioActual = AppDomain.CurrentDomain.BaseDirectory;

                // Construir la ruta completa al directorio donde se guardará el archivo PDF
                string directorioDestino = Path.Combine(directorioActual, "ArchivosPDF");

                // Si el directorio no existe, créalo
                if (!Directory.Exists(directorioDestino))
                {
                    Directory.CreateDirectory(directorioDestino);
                }

                // Construir la ruta completa al archivo PDF de destino
                string rutaDestino = Path.Combine(directorioDestino, nombreArchivoPDF);

                // Construir la ruta al archivo FACTURA.pdf
                string rutaPlantilla = @"D:\francisco\Documents\David Rodriguez\Visual Estudio\PROYECTO\PRESENTACION\PRESENTACION\img\FACTURA.pdf";


                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos PDF|*.pdf";
                    saveFileDialog.Title = "Guardar Factura PDF";
                    saveFileDialog.FileName = nombreArchivoPDF;
                    saveFileDialog.OverwritePrompt = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string facturaPath = saveFileDialog.FileName;
                        RellenarPDF(rutaPlantilla, rutaDestino, venta);
                        MessageBox.Show("Factura generada y guardada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                FileName = rutaDestino,
                                UseShellExecute = true
                            };
                            Process.Start(startInfo);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al abrir el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Se canceló la creación del PDF.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una venta para generar la factura.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void RellenarPDF(string rutaPlantilla, string rutaDestino, LoteVendido venta)
        {
            // Obtén datos de la grilla
            string id = dataGridViewHistorial.SelectedRows[0].Cells["id"].Value.ToString();
            string numRemate = dataGridViewHistorial.SelectedRows[0].Cells["idRemate"].Value.ToString();
            string numLote = dataGridViewHistorial.SelectedRows[0].Cells["idLote"].Value.ToString();
            string numComprador = dataGridViewHistorial.SelectedRows[0].Cells["id_usuario_comprador"].Value.ToString();
            string precioVenta = dataGridViewHistorial.SelectedRows[0].Cells["precio_venta"].Value.ToString();
            //string proveedorLote = dataGridViewHistorial.SelectedRows[0].Cells["proveedor_lote"].Value.ToString();
            string tipoLote = dataGridViewHistorial.SelectedRows[0].Cells["tipo_de_lote"].Value.ToString();
            string cantidad = dataGridViewHistorial.SelectedRows[0].Cells["cantidad_en_lote"].Value.ToString();
            string descripcion = dataGridViewHistorial.SelectedRows[0].Cells["descripcion"].Value.ToString();
            string fechaVenta = dataGridViewHistorial.SelectedRows[0].Cells["fecha_venta"].Value.ToString();

            //obtener el nombre del comprador.
            string nombreComprador = negocioLotesRemates.ObtenerNombreComprador(numComprador);

            // Obtén el tipo de remate del negocio usando el ID del remate
            string tipoRemate = negocioLotesRemates.ObtenerTipoRemate(int.Parse(numRemate));

            // Configuración de las coordenadas para los datos en el PDF
            int x;
            int y;
            int anchoMaximoDescripcion = 305;
            int interlineado = 15;

            // Cargar la plantilla PDF
            PdfDocument pdf = PdfReader.Open(rutaPlantilla, PdfDocumentOpenMode.Modify);
            PdfPage page = pdf.Pages[0];
            PdfSharp.Drawing.XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12);

            // Rellenar los campos del PDF con datos de la venta

            //NUMERO FACTURA
            gfx.DrawString(id, font, XBrushes.Black, x = 120, y = 137);
            //y += espacioEntreLineas;

            //FECHA
            gfx.DrawString(fechaVenta, font, XBrushes.Black, x = 100, y = 155);
            //y += espacioEntreLineas;

            // gfx.DrawString("Proveedor: " + proveedorLote, font, XBrushes.Black, x, y);
            // y += espacioEntreLineas;


            // NOMBRE DEL COMPRADOR
            gfx.DrawString(nombreComprador, font, XBrushes.Black, x = 155, y = 243);
            //NUMERO DE COMPRADOR
            gfx.DrawString(numComprador, font, XBrushes.Black, x = 70, y = 260);
            // y += espacioEntreLineas;


            //gfx.DrawString("Tipo de Remate: " + tipoRemate, font, XBrushes.Black, x, y);
            //y += espacioEntreLineas;

            //gfx.DrawString("Número de Remate: " + numRemate, font, XBrushes.Black, x, y);
            //y += espacioEntreLineas;

            //NUMERO DE LOTE
            gfx.DrawString("N° " + numLote, font, XBrushes.Black, x = 100, y = 340);
            // y += espacioEntreLineas;

            //TIPO DE LOTE
            gfx.DrawString(tipoLote, font, XBrushes.Black, x = 150, y = 340);
            //y += espacioEntreLineas;


            //DESCRIPCION con salto de línea si excede el ancho máximo
            string descripcionFormateada = "";
            string[] palabrasDescripcion = descripcion.Split(' ');
            foreach (string palabra in palabrasDescripcion)
            {
                if (gfx.MeasureString(descripcionFormateada + palabra, font).Width <= anchoMaximoDescripcion)
                {
                    descripcionFormateada += palabra + " ";
                }
                else
                {
                    gfx.DrawString(descripcionFormateada.Trim(), font, XBrushes.Black, x = 55, y = 375);
                    y += interlineado;
                    descripcionFormateada = palabra + " ";
                }
            }
            // Agregar la última línea
            gfx.DrawString(descripcionFormateada.Trim(), font, XBrushes.Black, x, y);



            //CANTIDAD EN LOTE
            gfx.DrawString(cantidad, font, XBrushes.Black, x = 392, y = 375);
            // y += espacioEntreLineas;

            //PRECIO DE VENTA
            gfx.DrawString("$" + precioVenta, font, XBrushes.Black, x = 490, y = 375);

            //PRECIO DE TOTAL - precio de venta
            XFont fontArialBlack = new XFont("Arial Black", 16);
            gfx.DrawString("$" + precioVenta, fontArialBlack, XBrushes.Black, x = 462, y = 557);

            // Guardar el PDF generado
            pdf.Save(rutaDestino);
            // Abrir el PDF automáticamente después de guardarlo
           
        }

        private void HistorialVentas_Load(object sender, EventArgs e)
        {
            if (dataGridViewHistorial.CurrentRow != null)
            {
                dataGridViewHistorial.CurrentRow.Selected = false;
            }
        }





    }
}
