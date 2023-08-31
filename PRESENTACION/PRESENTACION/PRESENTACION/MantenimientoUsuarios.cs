using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using NEGOCIO;



namespace PRESENTACION
{
    public partial class MantenimientoUsuarios : Form
    {

        private int originalHeaderRowHeight; // Guarda la altura original del encabezado
        public MantenimientoUsuarios()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
                                                                // this.FormBorderStyle = FormBorderStyle.None; // Eliminar el borde de la ventana
            this.MaximizeBox = false;
            //Estas propiedades se pueden setear con el IDE
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            // Crea el control ToolTip para el msj de hover sobre le refresh
            ToolTip toolTip1 = new ToolTip();

            // Establece el mensaje emergente para el botón
            toolTip1.SetToolTip(button5, "Recargar");

            // Configura el ToolTip para mostrar siempre el mensaje emergente
            toolTip1.ShowAlways = true;
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns.Add("login", "USUARIO");
            dataGridView1.Columns.Add("contrasenia", "CONTRASEÑA");
            dataGridView1.Columns.Add("rol", "ROL"); // Cambio en el nombre de la columna
            dataGridView1.Columns.Add("nombre", "NOMBRE"); // Nueva columna
            dataGridView1.Columns.Add("apellido", "APELLIDO"); // Nueva columna
            dataGridView1.Columns.Add("direccion", "DIRECCIÓN"); // Nueva columna
            dataGridView1.Columns.Add("correo", "CORREO"); // Nueva columna
            dataGridView1.Columns.Add("celular", " CELULAR"); // Nueva columna

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[2].Width = 140;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            //textBox4.Enabled = false;
            cargoGrilla();
            // Cambiar el SortMode de todas las columnas a NotSortable
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Establecer la selección del encabezado de columnas en la grilla
            dataGridView1.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;



        }




        private void VentanaAltasUsuarios_Load(object sender, EventArgs e)
        {

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void cargoGrilla()
        {
            try
            {
                dataGridView1.Rows.Clear(); // Limpiar las filas existentes
                RepositorioUsuarios repositorio = new RepositorioUsuarios(); // Crear una instancia
                ArrayList usuarios = repositorio.obtenerUsuarios(); // Llamar al método en la instancia

                foreach (Usuario usr in usuarios)
                {
                    dataGridView1.Rows.Add(usr.id, usr.login, usr.pass, usr.rol, usr.nombre, usr.apellido, usr.direccion, usr.correo, usr.celular);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {


        }
        /* BOTON ELIMINAR DESDE ARRAYLIST
        private void button2_Click_1(object sender, EventArgs e)
        {
            // Validar que se haya ingresado un ID válido
            if (!int.TryParse(textBox4.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Por favor, Seleccione el usuario que desea eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Confirme eliminación", "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Usuario usr = new Usuario(Int32.Parse(this.textBox4.Text));
                bool borradoOk = usr.borrarUsuario();
                if (borradoOk)
                {
                    MessageBox.Show("Borrado exitoso"
                        , "Operación exitosa"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se eliminó registro"
                       , "Error al borrar"
                       , MessageBoxButtons.OK
                       , MessageBoxIcon.Error);
                }
                //antes de cargar la grilla, borro los datos anteriores
                this.dataGridView1.Rows.Clear();
                //vuelvo a cargar la tabla
                cargoGrilla();
            }
            else if (dialogResult == DialogResult.No)
            {
                //en este caso no hago nada
            }
        }*/

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Desea eliminar este Usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int selectedUserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                    RepositorioUsuarios repositorio = new RepositorioUsuarios();
                    bool eliminado = repositorio.borrarUsuario(selectedUserId);

                    if (eliminado)
                    {
                        MessageBox.Show("Usuario eliminado correctamente", "Eliminar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        limpiarCampos();
                        // Actualizar la grilla mostrando los usuarios actualizados
                        cargoGrilla();
                        //dataGridView1.Refresh(); // Asegurarse de que la grilla se actualice visualmente; 
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //REGISTRAR USR ---------------------------------------------------------------------
        private void button4_Click(object sender, EventArgs e)
        {

            string login = textBox1.Text.ToUpper();
            string pass = textBox2.Text;
            string rol = comboBox1.Text.ToUpper();

            string nombre = textBox4.Text.ToUpper(); // Nuevo atributo (nombre)
            string apellido = textBox5.Text.ToUpper(); // Nuevo atributo (apellido)
            string direccion = textBox6.Text.ToLower(); // Nuevo atributo (direccion)
            string correo = textBox7.Text.ToLower(); // Nuevo atributo (correo)
            string celular = textBox8.Text; // Nuevo atributo (celular)

            string confirmPassword = textBox3.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(celular))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!EsCorreoValido(correo))
            {
                MessageBox.Show("Por favor, ingrese un correo electrónico válido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (pass != confirmPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Crear un objeto Usuario y asignar valores
                Usuario nuevoUsuario = new Usuario
                {
                    login = login,
                    pass = Usuario.HashPassword(pass),
                    rol = rol,
                    nombre = nombre, // Nuevo atributo (nombre)
                    apellido = apellido, // Nuevo atributo (apellido)
                    direccion = direccion, // Nuevo atributo (direccion)
                    correo = correo, // Nuevo atributo (correo)
                    celular = celular // Nuevo atributo (celular)
                };

                RepositorioUsuarios repositorio = new RepositorioUsuarios();




                // Verificar si el usuario ya existe
                if (repositorio.ExisteUsuario(login))
                {
                    MessageBox.Show("El usuario ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                bool agregado = repositorio.AgregarUsuario(nuevoUsuario);

                if (agregado)
                {
                    MessageBox.Show("Usuario registrado con éxito", "Agregar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargoGrilla(); // Actualizar la grilla
                }
                else
                {
                    MessageBox.Show("Error al agregar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                limpiarCampos();

                // Actualizar la grilla mostrando los usuarios actualizados
                cargoGrilla();
                dataGridView1.Refresh(); // Asegurarse de que la grilla se actualice visualmente
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
        }

        //BOTON MODIFICAR------------------------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            int idUsuarioSeleccionado = -1; // Valor por defecto si no se obtiene el ID correctamente
            string login = textBox1.Text.ToUpper();
            string pass = textBox2.Text; pass = Usuario.HashPassword(pass); // Hashear la contraseña
            string rol = comboBox1.Text.ToUpper();

            string nombre = textBox4.Text.ToUpper(); // Nuevo atributo (nombre)
            string apellido = textBox5.Text.ToUpper(); // Nuevo atributo (apellido)
            string direccion = textBox6.Text.ToLower(); // Nuevo atributo (direccion)
            string correo = textBox7.Text.ToLower(); // Nuevo atributo (correo)
            string celular = textBox8.Text; // Nuevo atributo (celular)

            string confirmPassword = textBox3.Text;
            confirmPassword = Usuario.HashPassword(confirmPassword); // Hashear la contraseña
            RepositorioUsuarios repositorio = new RepositorioUsuarios();

            if (dataGridView1.SelectedRows.Count > 0) // Verifica si hay filas seleccionadas en la grilla
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                idUsuarioSeleccionado = Convert.ToInt32(row.Cells["id"].Value);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione el usuario a modificar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(celular))
            {
                MessageBox.Show("Por favor, complete todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!EsCorreoValido(correo))
            {
                MessageBox.Show("Por favor, ingrese un correo electrónico válido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Desea modificar este Usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {


                if (pass == confirmPassword)
                {
                    Usuario usuarioModificado = new Usuario();
                    usuarioModificado.id = idUsuarioSeleccionado; // Utilizamos el ID obtenido de la grilla
                    usuarioModificado.login = login;
                    usuarioModificado.pass = pass;
                    usuarioModificado.rol = rol;
                    usuarioModificado.nombre = nombre; // Nuevo atributo (nombre)
                    usuarioModificado.apellido = apellido; // Nuevo atributo (apellido)
                    usuarioModificado.direccion = direccion; // Nuevo atributo (direccion)
                    usuarioModificado.correo = correo; // Nuevo atributo (correo)
                    usuarioModificado.celular = celular; // Nuevo atributo (celular)

                    bool modificado = repositorio.ModificarUsuario(usuarioModificado);

                    if (modificado)
                    {
                        MessageBox.Show("Usuario modificado correctamente", "Modificar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        limpiarCampos();

                        // Actualizar la grilla mostrando los usuarios actualizados
                        cargoGrilla();
                        dataGridView1.Refresh(); // Asegurarse de que la grilla se actualice visualmente
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }





        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //textBox4.ReadOnly = true;

        }
        private void button5_MouseEnter(object sender, EventArgs e)
        {


        }
        public void limpiarCampos()
        {
            // Borrar los datos de los campos de texto
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            comboBox1.SelectedIndex = -1; // Deseleccionar el ítem seleccionado
        }
        private void button5_Click(object sender, EventArgs e)
        {

            limpiarCampos();
            // Volver a cargar la grilla con los datos actualizados
            cargoGrilla();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                DataGridViewRow row = null;

                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    if (e.KeyCode == Keys.Up && rowIndex > 0)
                    {
                        row = dataGridView1.Rows[rowIndex - 1];
                    }
                    else if (e.KeyCode == Keys.Down && rowIndex < dataGridView1.Rows.Count - 1)
                    {
                        row = dataGridView1.Rows[rowIndex + 1];
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                }
                else if (e.KeyCode == Keys.Down)
                {
                    row = dataGridView1.Rows[0];
                }

                if (row != null)
                {
                    int id = Convert.ToInt32(row.Cells["id"].Value);
                    this.textBox1.Text = row.Cells[1].Value.ToString(); //login
                    this.textBox2.Text = row.Cells[2].Value.ToString(); //contraseña                                       
                    this.comboBox1.Text = row.Cells[3].Value.ToString(); //rol
                    this.textBox4.Text = row.Cells[4].Value.ToString(); // nuevo atributo (nombre)
                    this.textBox5.Text = row.Cells[5].Value.ToString(); // nuevo atributo (apellido)
                    this.textBox6.Text = row.Cells[6].Value.ToString(); // nuevo atributo (direccion)
                    this.textBox7.Text = row.Cells[7].Value.ToString(); // nuevo atributo (correo)
                    this.textBox8.Text = row.Cells[8].Value.ToString(); // nuevo atributo (celular)
                }
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una letra, un número o una tecla de control
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {

        }

        private void button1_MouseCaptureChanged(object sender, EventArgs e)
        {

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

        private void dataGridView1_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {

        }

        public void camposMantenimientoUsuarios()
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Verifica que se haya hecho clic en una celda válida
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["id"].Value); // Índice 0: id
                this.comboBox1.Text = row.Cells[3].Value.ToString(); // rol
                this.textBox1.Text = row.Cells[1].Value.ToString(); // login
                this.textBox2.Text = row.Cells[2].Value.ToString(); // contraseña
                this.textBox4.Text = row.Cells[4].Value.ToString(); // nuevo atributo (nombre)
                this.textBox5.Text = row.Cells[5].Value.ToString(); // nuevo atributo (apellido)
                this.textBox6.Text = row.Cells[6].Value.ToString(); // nuevo atributo (direccion)
                this.textBox7.Text = row.Cells[7].Value.ToString(); // nuevo atributo (correo)
                this.textBox8.Text = row.Cells[8].Value.ToString(); // nuevo atributo (celular)
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button2.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            button2.ForeColor = Color.FromArgb(160, 160, 160);
            button2.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde
                                                                                // Restaurar los colores al salir el mouse del botón

        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
            button2.ForeColor = Color.White;
            button2.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button3.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            button3.ForeColor = Color.FromArgb(160, 160, 160);
            button3.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde
                                                                                // Restaurar los colores al salir el mouse del botón
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {

            button3.BackColor = Color.Transparent;
            button3.ForeColor = Color.White;
            button3.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            // Cambiar los colores al pasar el mouse sobre el botón
            button4.BackColor = Color.FromArgb(250, 250, 250); // Rojo claro
            button4.ForeColor = Color.FromArgb(160, 160, 160);
            button4.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160); // Cambiar color del borde
                                                                                // Restaurar los colores al salir el mouse del botón
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {

            button4.BackColor = Color.Transparent;
            button4.ForeColor = Color.White;
            button4.FlatAppearance.BorderColor = Color.White; // Restaurar color del borde
        }











        public bool EsCorreoValido(string correo)
        {
            // Verificar si el correo contiene el símbolo @
            if (!correo.Contains("@"))
            {
                return false;
            }

            // Verificar si el correo contiene un dominio después del @
            string[] partes = correo.Split('@');
            if (partes.Length != 2 || string.IsNullOrWhiteSpace(partes[1]))
            {
                return false;
            }

            // Definir una lista blanca de dominios válidos
            string[] dominiosValidos = {
                "gmail.com", "yahoo.com", "hotmail.com", "outlook.com",
                "icloud.com", "msn.com", "live.com",
                "mail.com", "rocketmail.com", "yandex.com", "zoho.com",
                "protonmail.com", "fastmail.com", "inbox.com", "me.com",

    };

            // Verificar si el dominio del correo está en la lista blanca
            string dominio = partes[1].ToLower(); // Convertir a minúsculas para comparación
            if (!dominiosValidos.Contains(dominio))
            {
                return false;
            }

            return true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una letra o una tecla especial 
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una letra o una tecla especial 
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una letra, número o un carácter especial
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es un número o una tecla de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar la pulsación del carácter
                e.Handled = true;
            }
        }





    }//*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
}

