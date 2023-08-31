﻿using NEGOCIO.NEGOCIO;
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
    public partial class Preeventas : Form
    {

        private NegocioLotesRemates negocioLotesRemates = new NegocioLotesRemates();
        public Preeventas()
        {
            InitializeComponent();
            ConfigurarFormulario();
            ConfigurarColumnasDataGridView();
            CargarPreeventasEnGrilla();
            ActualizarColoresGrillaPreeventas();

        }


        private void ConfigurarFormulario()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

        }


        private void ConfigurarColumnasDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns.Add("id_remate", "N° REMATE");
            dataGridView1.Columns.Add("id_lote", "N° LOTE");
            dataGridView1.Columns.Add("id_usuario", "N° USUARIO");

            dataGridView1.Columns.Add("nombre_usuario", "NOMBRE");
            dataGridView1.Columns.Add("apellido_usuario", "APELLIDO");
            dataGridView1.Columns.Add("correo_usuario", "CORREO");
            dataGridView1.Columns.Add("celular_usuario", "CELULAR");
            dataGridView1.Columns.Add("proveedor_lote", "PROVEEDOR LOTE");
            dataGridView1.Columns.Add("precio_de_venta", "$ VENTA");
            dataGridView1.Columns.Add("precio_base_lote", "PRECIO RESERVA");
            dataGridView1.Columns.Add("tipo_lote", "TIPO LOTE");
            dataGridView1.Columns.Add("cantidad_en_lote", "CANTIDAD");

            dataGridView1.Columns.Add("descripcion_lote", "DESCRIPCIÓN");
            dataGridView1.Columns.Add("fecha_creacion", "FECHA");

            // Ajustar tamaños de columnas según tus necesidades
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;

            // Opcional: Cambiar el estilo de fuente y encabezados
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
        }


        private void CargarPreeventasEnGrilla()
        {
            try
            {
                var preeventas = negocioLotesRemates.ObtenerPreeventas();

                // Limpiar datos anteriores en la grilla (si es necesario)
                dataGridView1.Rows.Clear();

                foreach (var preventa in preeventas)
                {
                    dataGridView1.Rows.Add(
                        preventa.id,
                        preventa.id_remate,
                        preventa.id_lote,
                        preventa.id_usuario,

                        preventa.nombre_usuario,
                        preventa.apellido_usuario,
                        preventa.correo_usuario,
                        preventa.celular_usuario,
                        preventa.proveedor_lote,
                        preventa.precio_de_venta,
                        preventa.precio_base_lote,
                        preventa.tipo_lote,
                        preventa.cantidad_en_lote,
                        preventa.descripcion_lote,
                        preventa.fecha_creacion
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las preeventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonVenderLote_Click(object sender, EventArgs e)
        {
            try
            {
                int filaSeleccionada = dataGridView1.CurrentRow.Index;

                string apellidoComprador = dataGridView1.Rows[filaSeleccionada].Cells["apellido_usuario"].Value.ToString();
                int idUsuarioComprador = Convert.ToInt32(dataGridView1.Rows[filaSeleccionada].Cells["id_usuario"].Value);

                int idRemate = Convert.ToInt32(dataGridView1.Rows[filaSeleccionada].Cells["id_remate"].Value);
                int idLote = Convert.ToInt32(dataGridView1.Rows[filaSeleccionada].Cells["id_lote"].Value);
                string proveedorLote = dataGridView1.Rows[filaSeleccionada].Cells["proveedor_lote"].Value.ToString();
                string compradorLote = dataGridView1.Rows[filaSeleccionada].Cells["nombre_usuario"].Value.ToString();
                decimal precioVenta = Convert.ToDecimal(dataGridView1.Rows[filaSeleccionada].Cells["precio_de_venta"].Value);

                LoteVendido loteVendido = new LoteVendido
                {
                    IdRemate = idRemate,
                    IdLote = idLote,
                    IdUsuarioComprador = idUsuarioComprador,
                    FechaVenta = DateTime.Today,
                    Proveedor = proveedorLote,
                    Comprador = compradorLote + " " + apellidoComprador,
                    PrecioDeVenta = (int)precioVenta
                };

                negocioLotesRemates.MarcarLoteComoVendido(loteVendido);

                MessageBox.Show("Lote vendido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPreeventasEnGrilla(); // Recargar la grilla de preeventas para reflejar los cambios

                ActualizarColoresGrillaPreeventas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al marcar el lote como vendido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonQuitarLoteDeVendidos_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione el lote que desea quitar de la lista de vendidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int idRemate = Convert.ToInt32(selectedRow.Cells["id_remate"].Value);
            int idLote = Convert.ToInt32(selectedRow.Cells["id_lote"].Value);

            DialogResult result = MessageBox.Show("¿Está seguro de quitar este lote de la lista de vendidos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Llamar al método en la instancia de la capa de negocio para quitar el lote vendido
                    negocioLotesRemates.QuitarLoteVendido(idRemate, idLote);

                    MessageBox.Show("Lote quitado de la lista de vendidos exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarPreeventasEnGrilla(); // Recargar la grilla de preeventas para reflejar los cambios
                    ActualizarColoresGrillaPreeventas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al quitar el lote de la lista de vendidos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarColoresGrillaPreeventas()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int idLote = Convert.ToInt32(row.Cells["id_lote"].Value);
                bool vendido = negocioLotesRemates.VerificarLoteVendido(idLote);

                if (vendido)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
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

        private void button5_Click(object sender, EventArgs e)
        {
            CargarPreeventasEnGrilla(); // Recargar la grilla de preeventas para reflejar los cambios
            ActualizarColoresGrillaPreeventas();
        }
    }
}