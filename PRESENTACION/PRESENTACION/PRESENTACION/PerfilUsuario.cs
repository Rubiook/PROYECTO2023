using Microsoft.VisualBasic.Logging;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRESENTACION.PRESENTACION
{
    public partial class PerfilUsuario : Form
    {
        private Login Login { get; set; } // Propiedad para almacenar la instancia de Login
        private bool showPassword = false; // Variable de control

        public PerfilUsuario()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            textBox2.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
        }
        public Usuario ObtenerUsuarioActual()
        {
            if (Login != null && Login.UsuarioActual != null)
            {
                return Login.UsuarioActual;
            }
            return null;
        }
        public void AsignarLogin(Login login)
        {
            Login = login; // Asignar el objeto Login a la propiedad Login del formulario
        }

        public void CargarDatosUsuario(Login login)
        {
            if (login != null && login.UsuarioActual != null)
            {
                Usuario usuarioActual = login.UsuarioActual;
                label5.Text = login.ObtenerLoginUsuarioActual();
                label2.Text = login.ObtenerRolUsuarioActual();
                label7.Text = login.ObtenerNombreUsuarioActual();
                label6.Text = usuarioActual.apellido;
                label8.Text = usuarioActual.direccion;
                label9.Text = usuarioActual.correo;
                label10.Text = usuarioActual.celular;
            }
            else
            {
                MessageBox.Show("La instancia de Login o UsuarioActual es nula.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }






        private void button1_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = textBox2.Text;
            string confirmarContraseña = textBox3.Text;

            if (string.IsNullOrEmpty(nuevaContraseña))
            {
                MessageBox.Show("El campo de contraseña no puede estar vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detener la ejecución del método si el login está vacío
            }
            if (string.IsNullOrEmpty(confirmarContraseña))
            {
                MessageBox.Show("El campo de confirmacion de contraseña no puede estar vacío", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detener la ejecución del método si el login está vacío
            }
            if (nuevaContraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detener la ejecución del método si las contraseñas no coinciden
            }

            string nombreUsuarioActual = Login.ObtenerLoginUsuarioActual(); // Llama al método para obtener el nombre de usuario
            RepositorioUsuarios repositorio = new RepositorioUsuarios();
            string contraseñaHasheada = Usuario.HashPassword(nuevaContraseña);

            bool modificado = repositorio.ModificarContraseña(nombreUsuarioActual, contraseñaHasheada);

            if (modificado)
            {
                MessageBox.Show("Contraseña modificada con éxito", "Modificar Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Limpiar los campos de contraseña después de modificarla
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Error al modificar la contraseña para el usuario: " + nombreUsuarioActual, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }






        private void button5_Click_1(object sender, EventArgs e)
        {
            if (showPassword)
            {
                textBox2.UseSystemPasswordChar = true; // Ocultar el carácter de contraseña
                button5.BackgroundImage = Image.FromFile("PRESENTACION\\img\\show.png"); // Cambiar la imagen a "Mostrar contraseña"
            }
            else
            {
                textBox2.UseSystemPasswordChar = false; // Mostrar el carácter de contraseña
                button5.BackgroundImage = Image.FromFile("PRESENTACION\\img\\hide.png");  // Cambiar la imagen a "ocultar pass"
            }

            showPassword = !showPassword; // Alternar el valor de la variable de control
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (showPassword)
            {
                textBox3.UseSystemPasswordChar = true; // Ocultar el carácter de contraseña
                button2.BackgroundImage = Image.FromFile("PRESENTACION\\img\\show.png");  // Cambiar la imagen a "Mostrar contraseña"
            }
            else
            {
                textBox3.UseSystemPasswordChar = false; // Mostrar el carácter de contraseña
                button2.BackgroundImage = Image.FromFile("PRESENTACION\\img\\hide.png");  // Cambiar la imagen a "ocultar pass"
            }

            showPassword = !showPassword; // Alternar el valor de la variable de control
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

        private void PerfilUsuario_Load(object sender, EventArgs e)
        {
            CargarDatosUsuario(Login);
        }
    }
}
