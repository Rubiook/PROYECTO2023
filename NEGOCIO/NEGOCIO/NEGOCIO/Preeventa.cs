using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO.NEGOCIO
{
    public class Preeventa
    {

        public int id { get; set; }
        public int id_lote { get; set; }
        public int id_usuario { get; set; }
        public decimal precio_de_venta { get; set; }
        public string nombre_usuario { get; set; }
        public string apellido_usuario { get; set; }
        public string correo_usuario { get; set; }
        public string celular_usuario { get; set; }
        public string proveedor_lote { get; set; }
        public int precio_base_lote { get; set; }
        public string tipo_lote { get; set; }
        public int cantidad_en_lote { get; set; }
        public int id_remate { get; set; }
        public string descripcion_lote { get; set; }
        public DateTime fecha_creacion { get; set; }




    }
}
