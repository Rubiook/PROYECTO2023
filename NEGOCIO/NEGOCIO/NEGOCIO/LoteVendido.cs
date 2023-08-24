using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO.NEGOCIO
{
    public class LoteVendido
    {

        public int IdRemate { get; set; }
        public int IdLote { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Proveedor { get; set; }
        public string Comprador { get; set; }

        public int PrecioDeVenta { get; set;}



    }
}
