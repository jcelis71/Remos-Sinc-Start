using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Remos_Sinc_Start.Models;
using System.Net.Http;

namespace Remos_Sinc_Start.Services
{
    public class Servicios : ServicesBase
    {

        string APIResponse_OK = ConfigurationManager.AppSettings["APIResponse_OK"];
        string APIResponse_BR = ConfigurationManager.AppSettings["APIResponse_BR"];

        string UrlAPI = ConfigurationManager.AppSettings["URLBaseRemosAPI"];
        string UrlSET = "ContratoAsignacion";

        public CasinoservicioAPI GetViewServiciosActivos(string idMaquinaInstancia)
        {
            UrlSET = "CasinoServicio/ServiciosActuales?idMaquinaInstancia=" + idMaquinaInstancia;

            IEnumerable<Casinoservicio> ContratoAsignacion = null;

            CasinoservicioAPI ContratoAsignacionAPIs = new CasinoservicioAPI();

            try
            {
                using (var client = new HttpClient())
                {
                    ContratoAsignacionAPIs.gResponseAPI =  GetTokenPlus();

                    if (ContratoAsignacionAPIs.gResponseAPI.Status == APIResponse_OK)
                    {

                        var getTask = client.AddTokenToHeader(ContratoAsignacionAPIs.gResponseAPI.Token, UrlAPI).GetAsync(UrlSET);
                        getTask.Wait();

                        var Result = getTask.Result;
                        if (Result.IsSuccessStatusCode)
                        {
                            var readTask = Result.Content.ReadAsAsync<CasinoservicioAPI>();
                            readTask.Wait();
                            ContratoAsignacionAPIs = readTask.Result;
                        }
                        else
                        {
                            int StatusCode = (int)Result.StatusCode;
                            ContratoAsignacionAPIs.gResponseAPI.Status = StatusCode.ToString();
                            ContratoAsignacionAPIs.gResponseAPI.Response = Result.ReasonPhrase.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ContratoAsignacionAPIs.gResponseAPI.Status = APIResponse_BR;
                ContratoAsignacionAPIs.gResponseAPI.Response = ex.InnerException.Message;
            }

            ContratoAsignacion = ContratoAsignacionAPIs.Coleccion;

            return ContratoAsignacionAPIs;

        }



    }
}
