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
        //public string Tareas { get; set; }
      //  public int NRematador { get; set; }

        public string Nombre { get; set; } 
        public string Apellido { get; set; }
        public string Login { get; set; } 
      

    }//fin clase empleado----------------------------




    public class Rematador : Empleado
    {
        public string NRematador { get; set; }
        public decimal Comision { get; set; } // Nuevo atributo de comisión para rematadores


    }//fin clase rematador---------------------------




    public class Operador : Empleado
    {
        public string Tareas { get; set; }


    }//fin clase operador







}
