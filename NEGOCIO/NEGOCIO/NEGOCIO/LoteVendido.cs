using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO.NEGOCIO
{
    public class LoteVendido
    {

        public int Id { get; set; }
        public int IdRemate { get; set; }
        public int IdLote { get; set; }

        public int IdUsuarioComprador { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Proveedor { get; set; }

        public string Comprador { get; set; }
        public string ApellidoComprador { get; set; }

        public int PrecioDeVenta { get; set;}




        //propiedades relacionadas con el lote
        public string ProveedorLote { get; set; }
        public int PrecioBase { get; set; }
        public string TipoDeLote { get; set; }
        public int CantidadEnLote { get; set; }
        public string Descripcion { get; set; }








    }
}
