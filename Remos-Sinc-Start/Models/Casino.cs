using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remos_Sinc_Start.Models
{
    public class Casino
    {
        public Maquinainstancia[] MaquinaInstancia { get; set; }
        public int idCasino { get; set; }
        public bool Activo { get; set; }
        public string CodigoCentroCosto { get; set; }
        public string DescripcionCasino { get; set; }
        public byte[] LogoCasino { get; set; }
        public DateTime FechaEstado { get; set; }
    }
}
