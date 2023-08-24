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
    public partial class AñadirFotoAlLote : Form
    {
        private Lote selectedLote;
        private string imagenPath;
        private bool nuevaImagenCargada = false;
        private void CargarFotoLote_Load(object sender, EventArgs e)
        {

        }
        public AñadirFotoAlLote(Lote lote)
        {
            InitializeComponent();
            selectedLote = lote;
            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Ajusta la imagen sin distorsionarla
            this.StartPosition = FormStartPosition.CenterScreen; //centrar ventana
            this.FormBorderStyle = FormBorderStyle.FixedSingle; //No Redimensionar
            this.MaximizeBox = false;
        }

        //aceptar---------------------
        private void button2_Click(object sender, EventArgs e)
        {

            if (nuevaImagenCargada)
            {

                if (PictureBox1.Image != null)
                {
                    // Convierte la imagen a un arreglo de bytes para almacenar en la base de datos
                    byte[] imagenData;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat);
                        imagenData = ms.ToArray();
                    }

                    if (imagenData.Length <= 10485760)
                    {
                        // Actualizar la base de datos con la imagen del lote
                        using (MySqlConnection connection = Conexion.obtenerConexion())
                        {
                            string query = "UPDATE lote SET imagen = @Imagen WHERE id = @LoteId";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Imagen", imagenData);
                                command.Parameters.AddWithValue("@LoteId", selectedLote.id);

                                //connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Imagen cargada exitosamente en el lote.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("La imagen seleccionada es demasiado grande.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una imagen antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("La imagen es la misma, cargue una nueva antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        //cargar------------------------------------------
        private void button1_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagenPath = openFileDialog.FileName;

                    // Verificar el tamaño de la imagen (por ejemplo, máximo 5 MB)
                    FileInfo fileInfo = new FileInfo(imagenPath);
                    long maxSizeInBytes = 5 * 1024 * 1024; // 5 MB
                    if (fileInfo.Length > maxSizeInBytes)
                    {
                        MessageBox.Show("La imagen es demasiado grande. Por favor, seleccione una imagen más pequeña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Verificar el tipo de imagen
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                    string fileExtension = Path.GetExtension(imagenPath);
                    if (!allowedExtensions.Contains(fileExtension.ToLower()))
                    {
                        MessageBox.Show("El formato de imagen no es válido. Por favor, seleccione una imagen en formato JPG, JPEG o PNG.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cargar la imagen en el PictureBox si pasa las verificaciones
                    PictureBox1.Image = Image.FromFile(imagenPath);
                    nuevaImagenCargada = true;
                }
            }
        }
        public void CargarFoto(byte[] fotoData)
        {
            if (fotoData != null && fotoData.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(fotoData))
                {
                    PictureBox1.Image = Image.FromStream(ms);
                }
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
    }

}

