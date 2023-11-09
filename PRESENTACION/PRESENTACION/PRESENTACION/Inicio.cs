

using Microsoft.VisualBasic.Logging;
using NEGOCIO;
using NEGOCIO.NEGOCIO;
using PRESENTACION.PRESENTACION;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class Ventana1 : Form
    {
        public Login Login { get; set; } // Propiedad para almacenar la instancia de Login
        private Login loginInstance;
        private LotesAsignados negocioLotesRemates;
        private bool menuVisible = false;
        private int menuWidth = 150; // Ancho final del menú
        private int animationSpeed = 10; // Velocidad de la animación 
        private bool isMenuOpen = false; // Variable para rastrear si el menú está abierto
        private Point posicionOriginal;
        private Size tamanoOriginal;


        //FORM VENTANA ------------------------------------------------
        public Ventana1(Login loginInstance)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            panel1.Width = 45; // Ancho inicial del menú (barra pequeña)                              // this.FormBorderStyle = FormBorderStyle.None; // Eliminar el borde de la ventana


            this.Login = loginInstance; // Asignar la instancia de Login al formulario Ventana1
            MostrarRolUsuario(); // Llama al método para mostrar el rol 
            MostrarNombreUsuario();
            MostrarFechaActual();
            negocioLotesRemates = new LotesAsignados();



            // Configurar temporizadores
            timerOpenMenu = new System.Windows.Forms.Timer();
            timerOpenMenu.Interval = animationSpeed; // Intervalo de tiempo para la animación de apertura
            timerOpenMenu.Tick += new EventHandler(timerOpenMenu_Tick);
            ActualizarProximoRemateEnBoton();


        }

        //FONDO VENTANA --------------------------------------------------------------
        private void Ventana1_Load(object sender, EventArgs e)
        {
            //ActualizarProximoRemateEnLabel();


            DateTime fechaLimite = DateTime.Today.AddDays(-1); // Por ejemplo, consideramos remates antiguos de hace 30 días o más
            negocioLotesRemates.DesasignarLotesNoVendidosEnRematesAntiguos(fechaLimite);


            string rolUsuarioActual = Login.ObtenerRolUsuarioActual();
            if (rolUsuarioActual == "VENDEDOR" || rolUsuarioActual == "COMPRADOR" || rolUsuarioActual == "REMATADOR")
            {
                button5.Visible = false;
                button2.Visible = false;
                button7.Visible = false;
                button12.Visible = false;

            }
            if (rolUsuarioActual == "VENDEDOR" || rolUsuarioActual == "COMPRADOR" || rolUsuarioActual == "REMATADOR" || rolUsuarioActual == "OPERADOR")
            {
                button10.Visible = false;
            }

        }




        private void MostrarFechaActual()
        {
            lblFechaActual.Text = $"Fecha de Hoy: {DateTime.Now.ToString("dd/MM/yyyy")}";
        }


        //BTN EXIT ------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        //MENU SUPERIOR -----------------------------------------------------------------
        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void mantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //SUBMENUS--------------------------------------------------------------------
        private void agregarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Login != null)
            {
                string rolUsuarioActual = Login.ObtenerRolUsuarioActual();

                if (rolUsuarioActual == "ADMINISTRADOR" || rolUsuarioActual == "OPERADOR")
                {
                    GestionDeLotesYRemates ventanaRematesYLotes = new GestionDeLotesYRemates();
                    this.Hide();
                    ventanaRematesYLotes.ShowDialog(); //MODAL
                    this.Show();
                    MostrarRolUsuario();
                    //  ActualizarProximoRemateEnLabel();
                    MostrarNombreUsuario();

                }
                else
                {
                    MessageBox.Show("Error: No tiene permisos para acceder aqui.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Error: No se ha iniciado sesi�n correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        // M�todo para mostrar el rol del usuario logueado en el Label
        private void MostrarRolUsuario()
        {
            if (Login != null)
            {
                string rolUsuarioActual = Login.ObtenerRolUsuarioActual();
                label3.Text = rolUsuarioActual;
            }
            else
            {
                label3.Text = "Error rol de usuario";
            }
        }
        private void MostrarNombreUsuario()
        {
            if (Login != null)
            {

                string nombreUsuarioActual = Login.UsuarioActual.login; // Obtener el nombre de usuario del objeto Usuario
                label2.Text = nombreUsuarioActual;
            }
            else
            {
                label2.Text = "Error nombre de usuario";
            }
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Login != null)
            {
                string rolUsuarioActual = Login.ObtenerRolUsuarioActual();

                if (rolUsuarioActual == "ADMINISTRADOR" || rolUsuarioActual == "OPERADOR")
                {
                    MantenimientoUsuarios cuartoForm = new MantenimientoUsuarios();
                    this.Hide();
                    cuartoForm.ShowDialog(); //MODAL
                    this.Show();
                    MostrarRolUsuario();
                    //ActualizarProximoRemateEnLabel();
                    MostrarNombreUsuario();
                }
                else
                {
                    MessageBox.Show("Error: No tiene permisos para acceder aqui.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Error: No se ha iniciado sesión correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblRemate_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timerOpenMenu.Stop();
            timerCloseMenu.Stop();

            if (!isMenuOpen)
            {
                timerOpenMenu.Start();
            }
            else
            {
                timerCloseMenu.Start();
            }
            isMenuOpen = !isMenuOpen;
        }

        private void timerOpenMenu_Tick(object sender, EventArgs e)
        {
            if (panel1.Width >= 315) // Menú está completamente abierto
            {
                timerOpenMenu.Stop();
                isMenuOpen = true;
            }
            else
            {
                panel1.Width += 45;
            }
        }

        private void timerCloseMenu_Tick(object sender, EventArgs e)
        {
            if (panel1.Width <= 45) // Menú está completamente cerrado
            {
                timerCloseMenu.Stop();
                isMenuOpen = false;
            }
            else
            {
                panel1.Width = 45;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Login != null)
            {
                string rolUsuarioActual = Login.ObtenerRolUsuarioActual();



                if (rolUsuarioActual == "ADMINISTRADOR" || rolUsuarioActual == "OPERADOR")
                {
                    MantenimientoUsuarios cuartoForm = new MantenimientoUsuarios();

                    cuartoForm.ShowDialog(); //MODAL

                    MostrarRolUsuario();
                    //ActualizarProximoRemateEnLabel();
                    MostrarNombreUsuario();
                }
                else
                {
                    MessageBox.Show("Error: No tiene permisos para acceder aqui.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Error: No se ha iniciado sesión correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button5.BackColor = Color.FromArgb(169, 247, 154); // Rojo claro
            button5.ForeColor = Color.FromArgb(48, 155, 27);
            button5.FlatAppearance.BorderColor = Color.FromArgb(55, 238, 18); // Cambiar color del borde
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button5.BackColor = Color.Transparent;
            button5.ForeColor = Color.White;
            button5.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            if (Login != null)
            {
                string rolUsuarioActual = Login.ObtenerRolUsuarioActual();

                if (rolUsuarioActual == "ADMINISTRADOR" || rolUsuarioActual == "OPERADOR")
                {
                    GestionDeLotesYRemates ventanaRematesYLotes = new GestionDeLotesYRemates();
                    //this.Hide();
                    ventanaRematesYLotes.ShowDialog(); //MODAL
                    //this.Show();
                    MostrarRolUsuario();
                    //ActualizarProximoRemateEnLabel();
                    MostrarNombreUsuario();

                }
                else
                {
                    MessageBox.Show("Error: No tiene permisos para acceder aqui.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Error: No se ha iniciado sesi�n correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button2.BackColor = Color.FromArgb(169, 247, 154); // Rojo claro
            button2.ForeColor = Color.FromArgb(48, 155, 27);
            button2.FlatAppearance.BorderColor = Color.FromArgb(55, 238, 18); // Cambiar color del borde

        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button2.BackColor = Color.Transparent;
            button2.ForeColor = Color.White;
            button2.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button1.BackColor = Color.Black; // Fondo azul claro con opacidad

            button1.FlatAppearance.BorderColor = Color.Blue; // Cambiar color del borde

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button1.BackColor = Color.Transparent;

            button1.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {


            if (Login != null)
            {

                PerfilUsuario perfilUsuarioForm = new PerfilUsuario();
                perfilUsuarioForm.AsignarLogin(Login); // Asigna la instancia de Login al formulario
                perfilUsuarioForm.CargarDatosUsuario(Login); // Carga los datos del usuario
                perfilUsuarioForm.ShowDialog();

            }
            else
            {
                MessageBox.Show("La instancia de Login es nula.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            posicionOriginal = button6.Location;
            tamanoOriginal = button6.Size;

            int aumento = 10; // Cantidad de aumento del tamaño

            int nuevoAncho = tamanoOriginal.Width + aumento;
            int nuevoAlto = tamanoOriginal.Height + aumento;

            int nuevaPosX = posicionOriginal.X - aumento / 2;
            int nuevaPosY = posicionOriginal.Y - aumento / 2;

            button6.BackColor = Color.FromArgb(0, 0, 0, 0);
            button6.Size = new Size(nuevoAncho, nuevoAlto);
            button6.Location = new Point(nuevaPosX, nuevaPosY);
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = Color.Transparent;
            button6.Size = tamanoOriginal;
            button6.Location = posicionOriginal;
            button6.FlatAppearance.BorderColor = DefaultForeColor;
            button6.FlatAppearance.BorderSize = 0;
        }




        private void button3_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {

        }

        private void button3_MouseEnter_1(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button3.BackColor = Color.FromArgb(255, 200, 200); // Rojo claro
            button3.ForeColor = Color.FromArgb(255, 100, 100);
            button3.FlatAppearance.BorderColor = Color.Red; // Cambiar color del borde
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea Cerrar Sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }


        private void button3_MouseLeave_1(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button3.BackColor = Color.Transparent;
            button3.ForeColor = Color.White;
            button3.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button4.BackColor = Color.FromArgb(255, 200, 200); // Rojo claro
            button4.ForeColor = Color.FromArgb(255, 100, 100);
            button4.FlatAppearance.BorderColor = Color.Red; // Cambiar color del borde

        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {

            // Restaurar los colores al salir el mouse del botón
            button4.BackColor = Color.Transparent;
            button4.ForeColor = Color.White;
            button4.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }


        //BOTON SALIR --------------------------------------------------------------------------------------------------------------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro desea salir del programa?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        //BOTON LOTES DEL PROXIMO REMATE------------------------------------------------------------------------------------------------------
        private void button8_Click(object sender, EventArgs e)
        {

            bool hayLotes = negocioLotesRemates.HayLotesEnElProximoRemate();

            if (hayLotes)
            {
                VerLotesProximoRemate lotesProximoRemate = new VerLotesProximoRemate();
                lotesProximoRemate.ShowDialog(); // Abre la ventana de lotes del próximo remate
            }
            else
            {
                MessageBox.Show("No hay lotes en el próximo remate.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {
            VerRemates verRemates = new VerRemates();

            verRemates.ShowDialog(); //MODAL
        }

        private void button8_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button8.BackColor = Color.FromArgb(150, 200, 255); // Fondo azul claro con opacidad
            button8.ForeColor = Color.FromArgb(0, 100, 255);
            button8.FlatAppearance.BorderColor = Color.FromArgb(0, 20, 255); // Cambiar color del borde

        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {

            // Restaurar los colores al salir el mouse del botón
            button8.BackColor = Color.Transparent;
            button8.ForeColor = Color.White;
            button8.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button9.BackColor = Color.FromArgb(150, 200, 255); // Fondo azul claro con opacidad
            button9.ForeColor = Color.FromArgb(0, 100, 255);
            button9.FlatAppearance.BorderColor = Color.FromArgb(0, 20, 255); // Cambiar color del borde

        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button9.BackColor = Color.Transparent;
            button9.ForeColor = Color.White;
            button9.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void buttonSoporte_Click(object sender, EventArgs e)
        {
            string correoUsuario = Login.ObtenerCorreoUsuarioActual();
            Soporte soporte = new Soporte(label2.Text, label3.Text, correoUsuario);
            soporte.ShowDialog();
        }


        //BOTON MANTENIMIENTO EMPLEADOS ----------------------------------------------------------------------
        private void button10_Click(object sender, EventArgs e)
        {

            if (Login != null)
            {
                string rolUsuarioActual = Login.ObtenerRolUsuarioActual();

                if (rolUsuarioActual == "ADMINISTRADOR")
                {
                    MantenimientoEmpleados mantenimientoDeEmpleados = new MantenimientoEmpleados();
                    mantenimientoDeEmpleados.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Error: No tiene permisos para acceder aqui.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Error: No se ha iniciado sesión correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (Login != null)
            {
                string rolUsuarioActual = Login.ObtenerRolUsuarioActual();

                if (rolUsuarioActual == "ADMINISTRADOR" || rolUsuarioActual == "OPERADOR")
                {
                    HistorialVentas historialVentas = new HistorialVentas();
                    historialVentas.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Error: No tiene permisos para acceder aqui.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Error: No se ha iniciado sesión correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button10.BackColor = Color.FromArgb(169, 247, 154); // Rojo claro
            button10.ForeColor = Color.FromArgb(48, 155, 27);
            button10.FlatAppearance.BorderColor = Color.FromArgb(55, 238, 18); // Cambiar color del borde

        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button10.BackColor = Color.Transparent;
            button10.ForeColor = Color.White;
            button10.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button7.BackColor = Color.FromArgb(169, 247, 154); // Rojo claro
            button7.ForeColor = Color.FromArgb(48, 155, 27);
            button7.FlatAppearance.BorderColor = Color.FromArgb(55, 238, 18); // Cambiar color del borde

        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {

            // Restaurar los colores al salir el mouse del botón
            button7.BackColor = Color.Transparent;
            button7.ForeColor = Color.White;
            button7.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DateTime fechaHoy = DateTime.Today;
            List<Remate> remates = negocioLotesRemates.ObtenerRematesOrdenadosPorFecha();

            Remate remateHoy = remates.FirstOrDefault(remate => remate.Fecha.Date == fechaHoy);

            if (remateHoy != null)
            {
                VerLotesDeHoy ventanaLotesDeHoy = new VerLotesDeHoy(remateHoy.Id);
                ventanaLotesDeHoy.LoginInstance = Login;
                ventanaLotesDeHoy.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay lotes para hoy", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button11_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button11.BackColor = Color.FromArgb(150, 200, 255); // Fondo azul claro con opacidad
            button11.ForeColor = Color.FromArgb(0, 100, 255);
            button11.FlatAppearance.BorderColor = Color.FromArgb(0, 20, 255); // Cambiar color del borde

        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button11.BackColor = Color.Transparent;
            button11.ForeColor = Color.White;
            button11.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }


        public void ActualizarProximoRemateEnBoton()
        {
            List<Remate> remates = negocioLotesRemates.ObtenerRematesOrdenadosPorFecha();
            DateTime fechaActual = DateTime.Now;

            foreach (Remate remate in remates)
            {
                if (remate.Fecha > fechaActual)
                {
                    Console.WriteLine($"Próximo remate encontrado: {remate.Fecha.ToString("dd/MM/yyyy")}");

                    button8.Text = $"Lotes del Próximo Remate: {remate.Fecha.ToString("dd/MM/yyyy")}";

                    // Llamada a la función para cargar los lotes del remate encontrado
                    //CargarLotesUltimoRemate(remate.Id); // Pasar el ID del remate
                    MostrarFechaActual(); // Mostrar la fecha actual en otro Label
                    return; // Salir del bucle una vez que se encuentra el próximo remate
                }
            }

            Console.WriteLine("No hay remates programados en el futuro.");
            button8.Text = "No hay Próximos Remates";
            MostrarFechaActual(); // Mostrar la fecha actual incluso si no hay remates programados
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Preeventas preeventas = new Preeventas();
            preeventas.ShowDialog();
        }

        private void button12_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button12.BackColor = Color.FromArgb(169, 247, 154); // Rojo claro
            button12.ForeColor = Color.FromArgb(48, 155, 27);
            button12.FlatAppearance.BorderColor = Color.FromArgb(55, 238, 18); // Cambiar color del borde

        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button12.BackColor = Color.Transparent;
            button12.ForeColor = Color.White;
            button12.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void Ventana1_Activated(object sender, EventArgs e)
        {
            ActualizarProximoRemateEnBoton();
        }






        //fin-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    }
}