using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO.NEGOCIO
{
    public class Remate
    {

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Rematador { get; set; }
        public string TipoDeRemate { get; set; }


        public List<Lote> LotesDelRemate { get; set; }

        public Remate()
        {
            LotesDelRemate = new List<Lote>();
        }

    }
}
