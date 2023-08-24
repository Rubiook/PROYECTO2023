 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO.NEGOCIO
{
    public class Lote
    {
        private int loteId;
        private string? proveedor;
        private decimal precioBase;
        private string? tipoAnimal;
        private int cantidadAnimales;



        public int id { get; set; }
        public string proveedor_lote { get; set; }
        public int precio_base { get; set; }
        public string tipo_de_lote { get; set; }
        public int cantidad_en_lote { get; set; }
        public int id_remate { get; set; }
        public string descripcion { get; set; }
        public byte[] imagen { get; set; } // Cambiado a byte[] para almacenar datos binarios de la imagen


    }
}
