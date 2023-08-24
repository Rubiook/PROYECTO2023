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
        public HistorialVentas()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
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
    }
}
