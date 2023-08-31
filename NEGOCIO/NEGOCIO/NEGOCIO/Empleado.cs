using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO.NEGOCIO
{
    public class Empleado
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public decimal SueldoMensual { get; set; }
        public string Tareas { get; set; }


        public string Nombre { get; set; } 
        public string Apellido { get; set; }
        public string Login { get; set; } 
      

    }
}
