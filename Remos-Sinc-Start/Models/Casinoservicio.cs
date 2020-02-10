using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remos_Sinc_Start.Models
{
    public class Casinoservicio
    {
        public Casino Casino { get; set; }
        public Servicio Servicio { get; set; }
        public int idCasinoServicio { get; set; }
        public int idCasino { get; set; }
        public int idServicio { get; set; }
        public string HoraDesde { get; set; }
        public string HoraHasta { get; set; }
        public bool DobleDia { get; set; }
        public bool Activo { get; set; }
        public string DiasServicio { get; set; }
        public DateTime FechaEstado { get; set; }
        public string DescripcionCasinoServicio { get; set; }
    }

    public class CasinoservicioAPI : ResponseAPI
    {
        public ICollection<Casinoservicio> Coleccion { get; set; }
    }

}
