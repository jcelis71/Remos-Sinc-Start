using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Remos_Sinc_Start
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Remos_Sinc_Start()
            };
            ServiceBase.Run(ServicesToRun);

            //#if (!DEBUG)
            //           ServiceBase[] ServicesToRun;
            //           ServicesToRun = new ServiceBase[]
            //    {
            //         new MyWindowsService()
            //    };
            //           ServiceBase.Run(ServicesToRun);
            //#else
            //            Remos_Sinc_Start myWindowsServ = new Remos_Sinc_Start();
            //            myWindowsServ.T2_Proceso();

            //#endif


        }
    }
}
