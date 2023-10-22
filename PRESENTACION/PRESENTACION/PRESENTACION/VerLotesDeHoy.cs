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
    public partial class VerLotesDeHoy : Form
    {
        private NegocioBDD negocioLotesRemates = new NegocioBDD();
        private RepositorioUsuarios repositorioUsuarios = new RepositorioUsuarios();
        private int remateId;
        public Login LoginInstance { get; set; }

        public VerLotesDeHoy(int remateId)
        {
            InitializeComponent();
            ConfigurarFormulario();
            //  this.remateId = remateId;
            CargarLotesDeHoy(remateId);
            ActualizarRemateDeHoyEnLabel();
        }




        private void ConfigurarFormulario()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void VerLotesDeHoy_Load(object sender, EventArgs e)
        {
            string rolUsuarioActual = LoginInstance.ObtenerRolUsuarioActual();
            if (rolUsuarioActual == "OPERADOR" || rolUsuarioActual == "COMPRADOR" || rolUsuarioActual == "VENDEDOR")
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                button1.Visible = false;

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



        private void ActualizarRemateDeHoyEnLabel()
        {
            List<Remate> remates = negocioLotesRemates.ObtenerRematesOrdenadosPorFecha();
            DateTime fechaHoy = DateTime.Today;

            bool hayRemateHoy = false;

            foreach (Remate remate in remates)
            {
                if (remate.Fecha.Date == fechaHoy)
                {
                    hayRemateHoy = true;
                    lblRemate.Text = $"Remate de hoy: {remate.Fecha.ToString("dd/MM/yyyy")} <{remate.TipoDeRemate}>";
                    CargarLotesDeHoy(remate.Id); // Pasar el ID del remate
                    return; // Salir del bucle una vez que se encuentra el remate de hoy
                }
            }

            if (!hayRemateHoy)
            {
                lblRemate.Text = "Hoy no hay remate.";
                flowLayoutPanel1.Controls.Clear(); // Limpiar los lotes anteriores si los hay
            }
        }








        private void CargarLotesDeHoy(int remateId)
        {
            DateTime fechaHoy = DateTime.Now.Date;
            List<Lote> lotesDeHoy = negocioLotesRemates.ObtenerLotesPorFecha(fechaHoy);

            flowLayoutPanel1.Controls.Clear();

            foreach (Lote lote in lotesDeHoy)
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void limpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        //BOTON REALIZAR PREVENTA -------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                int idUsuarioComprador = Convert.ToInt32(textBox1.Text);
                Usuario comprador = repositorioUsuarios.ObtenerUsuarioPorId(idUsuarioComprador);

                int idLote = Convert.ToInt32(textBox2.Text);
                decimal precioVenta = Convert.ToDecimal(textBox3.Text);

                Lote lote = negocioLotesRemates.ObtenerLotePorId(idLote);
                if (lote == null)
                {
                    MessageBox.Show("El lote ingresado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comprador == null)
                {
                    MessageBox.Show("El usuario comprador no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (negocioLotesRemates.LoteYaPrevendido(idLote))
                {
                    MessageBox.Show("El lote ya está prevendido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                DialogResult result = MessageBox.Show("¿Confirma la preventa?" + "\n\n  N° Usuario: " + idUsuarioComprador + "\n  N° Lote:" + idLote + "\n  Precio De Venta: " + precioVenta, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    int idRemate = negocioLotesRemates.ObtenerRemateIdPorLote(idLote);

                    negocioLotesRemates.AgregarPreeventa(idLote, idUsuarioComprador, precioVenta,  idRemate);

                    MessageBox.Show("Lote prevendido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    limpiarCampos();

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al prevender el lote: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es un número o una tecla de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es un número o una tecla de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
            CargarLotesDeHoy(remateId);
        }
    }//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*

}
