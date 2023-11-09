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
    public partial class VerRemates : Form
    {
        private LotesAsignados negocioLotesRemates = new LotesAsignados();
        public VerRemates()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            CargarRemates();
        }




        private void CrearCardRemate(int idRemate, DateTime fechaRemate, TimeSpan horaInicio, TimeSpan horaFin, string rematador, string tipoRemate)
        {
            Panel card = new Panel();
            card.BackColor = Color.FromArgb(0, 3, 6, 3); // Verde oscuro
            card.BorderStyle = BorderStyle.Fixed3D;
            card.Width = 525;
            card.Height = 200;

            Label labelFechaRemate = new Label();
            labelFechaRemate.Text = "FECHA DEL REMATE: " + fechaRemate.ToString("dd/MM/yyyy");
            labelFechaRemate.Font = new Font("Arial", 22, FontStyle.Bold);
            labelFechaRemate.Width = 500;
            labelFechaRemate.Height = 40;
            labelFechaRemate.Location = new Point(10, 10);
            labelFechaRemate.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelFechaRemate);


            Label labelHoraInicio = new Label();
            labelHoraInicio.Text = "HORA DE INICIO: " + horaInicio.ToString(@"hh\:mm");
            labelHoraInicio.Font = new Font("Arial", 14);
            labelHoraInicio.Location = new Point(10, 60);
            labelHoraInicio.Width = 500;
            labelHoraInicio.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelHoraInicio);

            Label labelHoraFin = new Label();
            labelHoraFin.Text = "HORA DE FIN: " + horaFin.ToString(@"hh\:mm");
            labelHoraFin.Font = new Font("Arial", 14);
            labelHoraFin.Location = new Point(10, 80);
            labelHoraFin.Width = 500;
            labelHoraFin.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelHoraFin);

            Label labelRematador = new Label();
            labelRematador.Text = "RAMATADOR: " + rematador;
            labelRematador.Font = new Font("Arial", 14);
            labelRematador.Location = new Point(10, 120);
            labelRematador.Width = 500;
            labelRematador.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelRematador);

            Label labelTipoRemate = new Label();
            labelTipoRemate.Text = "TIPO DE REMATE: " + tipoRemate;
            labelTipoRemate.Font = new Font("Arial", 14);
            labelTipoRemate.Location = new Point(10, 140);
            labelTipoRemate.Width = 500;
            labelTipoRemate.ForeColor = Color.White; // Letras en blanco
            card.Controls.Add(labelTipoRemate);



            // Agregar la card al FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(card);
        }


        private void CargarRemates()
        {
            List<Remate> rematesPosteriores = negocioLotesRemates.ObtenerRematesPosteriores(DateTime.Now);

            flowLayoutPanel1.Controls.Clear();

            foreach (Remate remate in rematesPosteriores)
            {
                int idRemate = remate.Id;
                DateTime fechaRemate = remate.Fecha;
                TimeSpan horaInicio = remate.HoraInicio;
                TimeSpan horaFin = remate.HoraFin;
                string rematador = remate.Rematador.ToString();
                string tipoRemate = remate.TipoDeRemate.ToString();

                CrearCardRemate(idRemate, fechaRemate, horaInicio, horaFin, rematador, tipoRemate);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            CargarRemates();
        }
    }
}
