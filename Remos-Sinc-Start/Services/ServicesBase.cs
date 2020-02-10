using Remos_Sinc_Start.Models;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace Remos_Sinc_Start.Services
{
    public class ServicesBase
    {
        string APIResponse_OK = ConfigurationManager.AppSettings["APIResponse_OK"];
        string APIResponse_BR = ConfigurationManager.AppSettings["APIResponse_BR"];

        string UrlAPI = ConfigurationManager.AppSettings["URLBaseRemosAPI"];
        string UrlSET = "Login/";

        public GResponseAPI GetTokenPlus()
        {
            GResponseAPI gResponseAPI = new GResponseAPI();

            if ((Boolean)Sesion.Autenticado == true)
            {
                int TokenExpireTime = int.Parse(ConfigurationManager.AppSettings["TokenExpireTime"]);//agregar

                try
                {
                    DateTime startTime = DateTime.Now;

                    DateTime TokenBeginTime = (DateTime)Sesion.TokenBeginTime;

                    DateTime endTime = TokenBeginTime.AddMinutes(TokenExpireTime);

                    TimeSpan span = endTime.Subtract(startTime);

                    if ((TokenExpireTime - span.TotalMinutes) < TokenExpireTime)
                    {
                        gResponseAPI.Status = APIResponse_OK;
                        gResponseAPI.Token = (string)Sesion.Token;
                    }
                    else
                    {
                        gResponseAPI = GetTokenAPI();
                    }
                }

                catch (Exception ex)
                {
                    gResponseAPI.Status = APIResponse_BR;
                    gResponseAPI.Response = ex.InnerException.Message;
                    gResponseAPI.Token = "";
                }
            }
            else { gResponseAPI = GetTokenAPI(); }

            return gResponseAPI;
        }

        public GResponseAPI GetTokenAPI()
        {
            string TokenAPI = string.Empty;

            GResponseAPI gResponseAPI = new GResponseAPI();

            try
            {
                string Metodo = "Autenticar";

                var request = (HttpWebRequest)WebRequest.Create(UrlAPI + UrlSET + Metodo);

                var postData = "Login=" + Uri.EscapeDataString((string)Sesion.Login);
                postData += "&Password=" + Uri.EscapeDataString((string)Sesion.Password);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                string tok = request.GetRequestStream().ToString();

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    TokenAPI = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    gResponseAPI.Status = APIResponse_OK;
                    gResponseAPI.Response = response.StatusCode + ": " + response.StatusDescription;
                    gResponseAPI.Token = TokenAPI.ToString().Replace('"', ' ').Trim();
                }
                else
                {
                    gResponseAPI.Status = APIResponse_BR;
                    gResponseAPI.Response = response.StatusCode + ": " + response.StatusDescription;
                    gResponseAPI.Token = "";
                }
            }
            catch (Exception ex)
            {
                gResponseAPI.Status = APIResponse_BR;
                gResponseAPI.Response = ex.InnerException.Message;
                gResponseAPI.Token = "";
            }

            return gResponseAPI;
        }


        public string MensajeError(string Mensaje)
        {
            return string.Format("ShowGreetings('{0}');", Mensaje);
        }
    }
}
