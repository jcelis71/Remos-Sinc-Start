using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remos_Sinc_Start.Models
{
    public class Maquinainstancia
    {
        public Casino Casino { get; set; }

        public Maquina Maquina { get; set; }
        public int idMaquinaInstancia { get; set; }
        public int idMaquina { get; set; }
        public int idCasino { get; set; }
        public object Server { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaEstado { get; set; }
        public object ClaveMaquina { get; set; }

    }

    public class MaquinainstanciaAPI : ResponseAPI
    {
        public ICollection<Maquinainstancia> Coleccion { get; set; }
    }

}
