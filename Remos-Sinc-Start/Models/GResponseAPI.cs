using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remos_Sinc_Start.Models
{
    public class GResponseAPI
    {
        public string Status { get; set; }
        public string Response { get; set; }
        public string Token { get; set; }
    }

    public class ResponseAPI
    {
        public GResponseAPI gResponseAPI { get; set; }
    }

    public class GenericResponseAPI : ResponseAPI
    {
        public ICollection<Object> Coleccion { get; set; }
    }
}
