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

namespace PRESENTACION.PRESENTACION
{
    public partial class VerLotesProximoRemate : Form
    {
        private NegocioBDD negocioLotesRemates = new NegocioBDD();

        public VerLotesProximoRemate()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CargarDatos();
        }

        private void ConfigurarFormulario()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CargarDatos()
        {
            Remate proximoRemate = negocioLotesRemates.ObtenerProximoRemate();

            if (proximoRemate != null)
            {
                lblRemate.Text = $"PRÓXIMO REMATE: {proximoRemate.Fecha.ToString("dd/MM/yyyy")} <{proximoRemate.TipoDeRemate}> ";
                CargarLotesProximoRemate(proximoRemate.Id);
            }
            else
            {
                lblRemate.Text = "No hay remates programados.";
            }
        }

        private void CrearCard(string numeroLote, string tipoDeLote, string precioBase, string cantidadEnLote, string descripcionLote, byte[] imagenData, string proveedorLote)
        {
            Panel card = new Panel();
            card.BorderStyle = BorderStyle.Fixed3D;
            card.Width = 690; // Ajustado al tamaño deseado
            card.Height = 320;

            int mitadAncho = card.Width / 2;

            Label labelNumeroLote = new Label();
            labelNumeroLote.Text = "LOTE ---- N°" + numeroLote + " -----";
            labelNumeroLote.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            labelNumeroLote.Location = new Point(10, 10);
            labelNumeroLote.Width = mitadAncho - 50;
            labelNumeroLote.Height = 50;
            labelNumeroLote.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelNumeroLote);

            Label labelProveedor = new Label();
            labelProveedor.Text = "PROVEEDOR: " + proveedorLote;
            labelProveedor.Font = new Font("Segoe UI", 12);
            labelProveedor.Location = new Point(10, 60); // Ajusta la posición según tus necesidades
            labelProveedor.Width = mitadAncho - 50; // Ajustado al ancho de la mitad izquierda
            labelProveedor.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelProveedor);

            Label labelTipoDeLote = new Label();
            labelTipoDeLote.Text = "TIPO DE LOTE: " + tipoDeLote;
            labelTipoDeLote.Font = new Font("Segoe UI", 12);
            labelTipoDeLote.Location = new Point(10, 90);
            labelTipoDeLote.Width = mitadAncho - 50; // Ajustado al ancho de la mitad izquierda
            labelTipoDeLote.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelTipoDeLote);

            Label labelPrecioBase = new Label();
            labelPrecioBase.Text = "PRECIO DE RESERVA: " + precioBase;
            labelPrecioBase.Font = new Font("Segoe UI", 12);
            labelPrecioBase.Location = new Point(10, 120);
            labelPrecioBase.Width = mitadAncho - 50;
            labelPrecioBase.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelPrecioBase);

            Label labelCantidadEnLote = new Label();
            labelCantidadEnLote.Text = "CANTIDAD EN EL LOTE: " + cantidadEnLote;
            labelCantidadEnLote.Font = new Font("Segoe UI", 12);
            labelCantidadEnLote.Location = new Point(10, 150);
            labelCantidadEnLote.Width = mitadAncho - 50;
            labelCantidadEnLote.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelCantidadEnLote);

            Label labelDescripcion = new Label();
            labelDescripcion.Text = "DESCRIPCIÓN: " + descripcionLote;
            labelDescripcion.Font = new Font("Segoe UI", 12);
            labelDescripcion.Location = new Point(10, 180);
            labelDescripcion.AutoSize = true;
            labelDescripcion.Width = mitadAncho - 60; // Ajustado al ancho de la mitad izquierda
            labelDescripcion.MaximumSize = new Size(mitadAncho - 40, 0); // Ajustado al ancho de la mitad izquierda
            labelDescripcion.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelDescripcion);

            // Obtener la altura total de la información (lado izquierdo de la card)
            int informacionHeight = labelDescripcion.Bottom - labelNumeroLote.Top;

            // Ajustar la posición del Label si el texto es demasiado largo
            if (labelDescripcion.Height > informacionHeight)
            {
                int newY = labelNumeroLote.Bottom + 4; // Colocar debajo de la información
                labelDescripcion.Location = new Point(10, newY);
            }

            PictureBox pictureBoxImagen = new PictureBox();
            pictureBoxImagen.Width = mitadAncho + 10; // Ancho de la mitad derecha
            pictureBoxImagen.Height = card.Height - 20;
            pictureBoxImagen.Location = new Point(mitadAncho - 30, 0); // Inicio en la mitad derecha
            pictureBoxImagen.SizeMode = PictureBoxSizeMode.Zoom; // Ajusta la imagen sin distorsionarla
            if (imagenData != null && imagenData.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream(imagenData))
                {
                    pictureBoxImagen.Image = Image.FromStream(memoryStream); // Carga la imagen desde el MemoryStream
                    card.Controls.Add(pictureBoxImagen);
                }
            }
            else
            {
                pictureBoxImagen.Image = null; // No hay imagen disponible
            }

            card.Controls.Add(pictureBoxImagen);

            flowLayoutPanel1.Controls.Add(card);
        }


        /*
        private void ActualizarProximoRemateEnLabel()
        {

            List<Remate> remates = negocioLotesRemates.ObtenerRematesOrdenadosPorFecha();
            DateTime fechaActual = DateTime.Now;

            foreach (Remate remate in remates)
            {
                if (remate.Fecha > fechaActual)
                {
                    Console.WriteLine($"Próximo remate encontrado: {remate.Fecha.ToString("dd/MM/yyyy")}");

                    lblRemate.Text = $"Próximo remate: {remate.Fecha.ToString("dd/MM/yyyy")}";

                    // Llamada a la función para cargar los lotes del remate encontrado
                    CargarLotesProximoRemate(remate.Id); // Pasar el ID del remate
                    return; // Salir del bucle una vez que se encuentra el próximo remate
                }
            }

            Console.WriteLine("No hay remates programados en el futuro.");
            lblRemate.Text = "No hay remates programados.";
        }
        */

        

        private void CargarLotesProximoRemate(int remateId)
        {
            List<Lote> lotes = negocioLotesRemates.ObtenerLotesAsignadosPorRemate(remateId);

            flowLayoutPanel1.Controls.Clear();

            foreach (Lote lote in lotes)
            {
                string numeroLote = lote.id.ToString();
                string tipoDeLote = lote.tipo_de_lote;
                string precioBase = "$UYU " + lote.precio_base.ToString();
                string cantidadEnLote = lote.cantidad_en_lote.ToString();
                string descripcion = lote.descripcion;
                byte[] imagenData = lote.imagen;
                string proveedorLote = lote.proveedor_lote;

                CrearCard(numeroLote, tipoDeLote, precioBase, cantidadEnLote, descripcion, imagenData, proveedorLote);
            }
        }


        private void LotesProximoRemate_Load(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}

