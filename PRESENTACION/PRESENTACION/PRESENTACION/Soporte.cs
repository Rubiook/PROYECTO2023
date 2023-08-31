using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NEGOCIO;
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;

namespace PRESENTACION.PRESENTACION
{
    public partial class Soporte : Form
    {
        //private PerfilUsuario perfilUsuarioForm; // Declarar la variable para la instancia del formulario

        public PerfilUsuario perfilUsuarioForm { get; set; }
        private string nombreUsuario;
        private string rolUsuario;
        private string correoUsuario;
        private static string urlForms = "https://formspree.io/f/xlevqyjd";

        public Soporte(string nombreUsuario, string rolUsuario, string correoUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            this.nombreUsuario = nombreUsuario;
            this.rolUsuario = rolUsuario;
            this.correoUsuario = correoUsuario;
            CargarDatosUsuario();

        }

        private void buttonSoporte_Click(object sender, EventArgs e)
        {
            string phoneNumber = "+59897625167"; // Número de teléfono
            string message = "Hola, ¿cómo estás?"; // Mensaje predefinido (opcional)

            // Escapar caracteres especiales en el mensaje
            string encodedMessage = Uri.EscapeDataString(message);

            // Crear el enlace de WhatsApp Business
            string whatsappBusinessLink = $"whatsapp://send?phone={phoneNumber}&text={encodedMessage}";

            // Usar el proceso para abrir el enlace en el navegador predeterminado
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = whatsappBusinessLink,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                MessageBox.Show("No se pudo abrir WhatsApp Business: " + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isInternetConnected = CheckInternetConnection();

            if (!isInternetConnected)
            {
                MessageBox.Show("No estás conectado a Internet. Por favor, verifica tu conexión y vuelve a intentarlo.", "Sin conexión a Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnviarFormulario();
            textBox2.Clear();
        }


        private bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        private void Soporte_Load(object sender, EventArgs e)
        {

        }


        private void CargarDatosUsuario()
        {

            label1.Text = nombreUsuario;
            label2.Text = rolUsuario;
            label3.Text = correoUsuario;
        }



        private async Task EnviarFormulario()
        {
            string CorreoUsuario = correoUsuario;
            string mensaje = "Soporte Técnico Remates del campo. Usuario: " + nombreUsuario + " - " + rolUsuario + "\n Mensaje: " + textBox2.Text;

            if (!string.IsNullOrWhiteSpace(correoUsuario) && !string.IsNullOrWhiteSpace(mensaje))
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Por favor escriba un mensaje.", "Mensaje vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var httpClient = new HttpClient())
                {
                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(CorreoUsuario), "email");
                    formData.Add(new StringContent(mensaje), "message");

                    var response = await httpClient.PostAsync(urlForms, formData);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Gracias por ponerte en contacto con soporte.\nRepsponderemos a la brevedad!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al enviar el correo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(250, 250, 250);
            button1.ForeColor = Color.FromArgb(160, 160, 160);
            button1.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160);

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {

            // Restaurar los colores al salir el mouse del botón
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }
    }
}
