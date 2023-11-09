using MercadoPago.Resource.Preference;
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
    public partial class Pago : Form
    {
        private string preferenceId;
        private WebBrowser webBrowser1;

        public Pago(string preferenceId)
        {
            InitializeComponent();
            this.preferenceId = preferenceId;

            // Inicializa el control webBrowser1
            webBrowser1 = new WebBrowser();
            this.Controls.Add(webBrowser1); // Agrega el control WebBrowser al formulario
            // Ajusta el tamaño del WebBrowser para que coincida con el tamaño del formulario
            webBrowser1.Dock = DockStyle.Fill;
            // Permite que el WebBrowser ajuste su tamaño al contenido automáticamente
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void Pago_Load(object sender, EventArgs e)
        {

            try
            {
                // URL de la página de pago
                string paymentUrl = $"https://www.mercadopago.com/checkout/v1/redirect?preference_id={preferenceId}";


                MessageBox.Show($"Preferencia cargada correctamente. {paymentUrl}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Navega a la URL en el control WebBrowser
                webBrowser1.Navigate(paymentUrl);
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir al intentar abrir la URL
                MessageBox.Show($"Error al abrir la página: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
