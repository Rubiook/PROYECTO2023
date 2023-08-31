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

namespace PRESENTACION
{

    public partial class RegistroUsuario : Form
    {

        private InicioSesion ventanaLogin; // Variable para almacenar la ventana de inicio de sesión

        //FORM VENTANA REGISTRO-----------------------------------------
        public RegistroUsuario()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // this.FormBorderStyle = FormBorderStyle.None; // Eliminar el borde de la ventana
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.ToUpper();
            string pass = textBox2.Text;
            string passHash = Usuario.HashPassword(pass);

            string confirmPassword = textBox3.Text;
            string confirmPasswordHash = Usuario.HashPassword(confirmPassword);
            string rol = comboBox1.Text;

            string nombre = textBox4.Text.ToUpper();
            string apellido = textBox5.Text.ToUpper();
            string direccion = textBox6.Text.ToLower();
            string correo = textBox7.Text.ToLower();
            string celular = textBox8.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(rol)
                || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido)
                || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(celular))
            {
                MessageBox.Show("Todos los campos deben ser completados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detener la ejecución del método si algún campo está vacío
            }

            MantenimientoUsuarios mantenimientoUsuarios = new MantenimientoUsuarios();
            // Verificar si el correo es válido
            if (!mantenimientoUsuarios.EsCorreoValido(correo))
            {
                MessageBox.Show("El correo ingresado no es válido. Por favor, ingrese un correo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detener la ejecución del método si el correo es inválido
            }

            if (passHash == confirmPasswordHash)
            {
                try
                {
                    // Crear un objeto Usuario y asignar valores
                    Usuario nuevoUsuario = new Usuario
                    {
                        login = login,
                        pass = Usuario.HashPassword(pass),
                        rol = rol,
                        nombre = nombre, // Asignar el atributo nombre
                        apellido = apellido, // Asignar el atributo apellido
                        direccion = direccion, // Asignar el atributo dirección
                        correo = correo, // Asignar el atributo correo
                        celular = celular // Asignar el atributo celular
                    };

                    RepositorioUsuarios repositorio = new RepositorioUsuarios();

                    // Verificar si el usuario ya existe
                    if (repositorio.ExisteUsuario(login))
                    {
                        MessageBox.Show("El usuario ya existe, ingrese un usuario válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool agregado = repositorio.AgregarUsuario(nuevoUsuario);

                    if (agregado)
                    {
                        MessageBox.Show("Usuario registrado con éxito", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //nicioSesion ventanaLogin = new InicioSesion();
                        //his.Hide();
                        //entanaLogin.ShowDialog(); //modal
                        //this.Show();
                        //is.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Limpiar los campos de texto después de guardar el usuario
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    comboBox1.Text = string.Empty;
                    textBox4.Text = string.Empty; // Limpiar el atributo nombre
                    textBox5.Text = string.Empty; // Limpiar el atributo apellido
                    textBox6.Text = string.Empty; // Limpiar el atributo dirección
                    textBox7.Text = string.Empty; // Limpiar el atributo correo
                    textBox8.Text = string.Empty; // Limpiar el atributo celular
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


            this.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
        }


        private bool showPassword = false; // Variable de control
        private void button5_Click(object sender, EventArgs e)
        {
            if (showPassword)
            {
                textBox2.UseSystemPasswordChar = true; // Ocultar el carácter de contraseña
                button5.BackgroundImage = Image.FromFile("D:\\francisco\\Documents\\David Rodriguez\\Visual Estudio\\PROYECTO\\PRESENTACION\\PRESENTACION\\img\\show.png"); // Cambiar la imagen a "Mostrar contraseña"
            }
            else
            {
                textBox2.UseSystemPasswordChar = false; // Mostrar el carácter de contraseña
                button5.BackgroundImage = Image.FromFile("D:\\francisco\\Documents\\David Rodriguez\\Visual Estudio\\PROYECTO\\PRESENTACION\\PRESENTACION\\img\\hide.png");  // Cambiar la imagen a "ocultar pass"
            }

            showPassword = !showPassword; // Alternar el valor de la variable de control
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (showPassword)
            {
                textBox3.UseSystemPasswordChar = true; // Ocultar el carácter de contraseña
                button2.BackgroundImage = Image.FromFile("D:\\francisco\\Documents\\David Rodriguez\\Visual Estudio\\PROYECTO\\PRESENTACION\\PRESENTACION\\img\\show.png");  // Cambiar la imagen a "Mostrar contraseña"
            }
            else
            {
                textBox3.UseSystemPasswordChar = false; // Mostrar el carácter de contraseña
                button2.BackgroundImage = Image.FromFile("D:\\francisco\\Documents\\David Rodriguez\\Visual Estudio\\PROYECTO\\PRESENTACION\\PRESENTACION\\img\\hide.png");  // Cambiar la imagen a "ocultar pass"
            }

            showPassword = !showPassword; // Alternar el valor de la variable de control
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        //NOMBRE DE USR-------------------------------------------------------------
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Verificar si la tecla presionada es una letra, un número o la tecla BackSpace
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloquear la entrada de caracteres no alfanuméricos
            }
            else
            {
                e.Handled = false; // Permitir la entrada de letras, números y tecla de retroceso
            }
        }

        private void VentanaRegister_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (ventanaLogin == null || ventanaLogin.IsDisposed)
            {
                ventanaLogin = new InicioSesion();
                ventanaLogin.Show();
                this.Close();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ventanaLogin == null || ventanaLogin.IsDisposed)
            {
                ventanaLogin = new InicioSesion();
            }

            ventanaLogin.Show();
            this.Hide();

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button1.BackColor = Color.FromArgb(150, 200, 255); // Fondo azul claro con opacidad
            button1.ForeColor = Color.FromArgb(0, 100, 255);
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 20, 255); // Cambiar color del borde

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

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada es un número o la tecla BackSpace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Bloquear la entrada de caracteres no numéricos
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada de la tecla si no es una letra
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Cancelar la entrada de la tecla si no es una letra
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Bloquear la entrada del espacio
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Bloquear la entrada del espacio
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Bloquear la entrada del espacio
            }
        }
    }//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
}
