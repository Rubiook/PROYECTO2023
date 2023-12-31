﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
using NEGOCIO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRESENTACION
{
    public partial class InicioSesion : Form
    {
        private RegistroUsuario ventanaRegistro; // Variable para almacenar la ventana de registro



        //VENTANA LOGIN-------------------------------------------
        public InicioSesion()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle;//no redimensionar
            this.MaximizeBox = false;
            // this.ControlBox = false; // Quitar los botones de la ventana


        }

        //FONDO VENTANA LOGIN------------------------------------------------
        private void Form2_Load(object sender, EventArgs e)
        {
            //   this.FormBorderStyle = FormBorderStyle.None; // Eliminar el borde de la ventana
            // Establecer el valor inicial del TextBox
            // Cambiar el color del texto a gris claro
            // Agregar el manejador de eventos



        }

        // Sobreescribir el método OnFormClosing para forzar cierre desde cruz
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Verificar si el botón de cierre fue presionado por el usuario
            if (e.CloseReason == CloseReason.UserClosing)
            {

                // Forzar el cierre de la aplicación
                Application.Exit();
            }

            base.OnFormClosing(e);
        }
        //LBL LOGIN---------------------------------------------------
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //LBL INGRESE USUARIO----------------------------------------
        private void label2_Click(object sender, EventArgs e)
        {

        }
        //LBL INGRESE CONTRASEÑA----------------------------------------
        private void label3_Click(object sender, EventArgs e)
        {

        }

        //TXT INGRESE USUARIO----------------------------------------
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //TXT INGRESE USUARIO----------------------------------------
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }



        private void limpiarCamposLogin()
        {
            textBox1.Clear();
            textBox2.Clear();
        }



        //LINK REGISTRAR USUARIOS----------------------------------------------------------------


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ventanaRegistro = new RegistroUsuario();
            ventanaRegistro.ShowDialog();
            this.Hide();
            limpiarCamposLogin();

        }

        //BTN INGRESAR-------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {

            string nombreUsuario = this.textBox1.Text.Trim(); // Elimina espacios en blanco al principio y al final
            string pass = textBox2.Text;

            // Verificar si el nombre de usuario está vacío
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                MessageBox.Show("Ingrese su nombre de usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                textBox1.Focus();
                return; // Salir del método para evitar la autenticación

            }

            // Verificar si la contraseña está vacía
            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Ingrese su contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                textBox2.Focus();
                return; // Salir del método para evitar la autenticación

            }

            // Continuar con la autenticación solo si ambos campos tienen valores
            Login login = new Login();
            nombreUsuario = nombreUsuario.ToUpper();
            string passHash = Usuario.HashPassword(pass);

            Usuario usr = new Usuario(nombreUsuario, passHash);

            bool correcto = login.Autenticar(usr);
            if (!correcto)
            {
                MessageBox.Show("Usuario o contraseña incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
                button1.BackColor = Color.Transparent;
                button1.ForeColor = Color.White;
                button1.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
            }
            else
            {
                // Obtener el usuario autenticado
                Usuario usuarioActual = login.UsuarioActual;

                Ventana1 ventanaInicio = new Ventana1(login);
                ventanaInicio.Login = login; // Asignar la instancia de Login al formulario Ventana1
                limpiarCamposLogin();
                this.Hide();
                ventanaInicio.ShowDialog(); // MODAL
                this.Close();
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Cambiar los colores 
                button1.BackColor = Color.FromArgb(5, 150, 200, 255); // Fondo azul claro con opacidad
                button1.ForeColor = Color.FromArgb(100, 0, 100, 255);
                button1.FlatAppearance.BorderColor = Color.FromArgb(0, 25, 255); // Cambiar color del borde

                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    if (!string.IsNullOrWhiteSpace(textBox2.Text))
                    {
                        button1.PerformClick();
                        e.Handled = true;
                    }
                    else
                    {
                        MessageBox.Show("Ingrese su contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        textBox2.Focus();
                        // Restaurar los colores al salir el mouse del botón
                        button1.BackColor = Color.Transparent;
                        button1.ForeColor = Color.White;
                        button1.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese su nombre de usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    textBox1.Focus();
                    // Restaurar los colores al salir el mouse del botón
                    button1.BackColor = Color.Transparent;
                    button1.ForeColor = Color.White;
                    button1.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
                }
            }
        }


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

            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox2.Focus(); // Enfocar el textBox2 para ingresar la contraseña
            }

        }


        private void VentanaLogin_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            // Realiza aquí las acciones que desees antes de cerrar la ventana

            // Si deseas anular el cierre, puedes hacerlo así:
            // e.Cancel = true; // Esto evitará que la ventana se cierre

            // Realiza tus acciones personalizadas, como ocultar la ventana o mostrar un mensaje

            // Por ejemplo, mostrar un MessageBox y luego ocultar la ventana
            //DialogResult result = MessageBox.Show("¿Desea salir de la aplicación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // this.Hide();

            //if (result == DialogResult.Yes)
            // {   // Si deseas cerrar la aplicación por completo, puedes hacerlo con Application.Exit()
            Application.Exit();
            // }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {

            // Cambiar los colores al pasar el mouse sobre el botón
            button1.BackColor = Color.FromArgb(5, 150, 200, 255); // Fondo azul claro con opacidad
            button1.ForeColor = Color.FromArgb(100, 0, 100, 255);
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 25, 255); // Cambiar color del borde

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde

        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {

        }



        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_Layout(object sender, LayoutEventArgs e)
        {
            // Restaurar los colores al salir el mouse del botón
            button1.BackColor = Color.Transparent;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.BorderColor = DefaultForeColor; // Restaurar color del borde
        }

        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }



        //fin-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    }
}
