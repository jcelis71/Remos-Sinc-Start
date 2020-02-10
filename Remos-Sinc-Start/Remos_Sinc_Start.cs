using Remos_Sinc_Start.Models;
using Remos_Sinc_Start.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace Remos_Sinc_Start
{
    public partial class Remos_Sinc_Start : ServiceBase
    {

        Servicios ser = new Servicios();

        IEnumerable<Casinoservicio> list;
        CasinoservicioAPI CasinoServicios;

        private Double Frecuencia_Timer = (Double.Parse(ConfigurationManager.AppSettings["Frecuencia_Timer"].ToString()) * 60000);
        private Double Frecuencia_ServiciosActivos = (Double.Parse(ConfigurationManager.AppSettings["Frecuencia_ServiciosActivos"].ToString()) * 3600000);
        private Double Frecuencia_GetPutSpool = (Double.Parse(ConfigurationManager.AppSettings["Frecuencia_GetPutSpool"].ToString()));
        private Double Frecuencia_SendTransactions = (Double.Parse(ConfigurationManager.AppSettings["Frecuencia_SendTransactions"].ToString()));
        private string idMaquinaInstancia = ConfigurationManager.AppSettings["idMaquinaInstancia"].ToString();

        Timer T1 = new Timer();
        Timer T2 = new Timer();

        DateTime HoraGetPutSpool;
        DateTime HoraSendTransactions;

        // Set the Interval to 1 seconds (1000 milliseconds).
        public Remos_Sinc_Start()
        {
            InitializeComponent();

            try
            {
                HoraGetPutSpool = DateTime.Now.AddMinutes(Frecuencia_GetPutSpool);
                HoraSendTransactions = DateTime.Now.AddMinutes(Frecuencia_SendTransactions);

                CargaServicios();
            }
            catch(Exception ex)
            {
                this.WriteToFile("Error: " + ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            this.WriteToFile("In OnStart.");

            // TODO: Add code here to start your service.
            try
            {
                T1.Interval = (Frecuencia_ServiciosActivos);
                T1.AutoReset = true;
                T1.Enabled = true;
                T1.Start();
                T1.Elapsed += new ElapsedEventHandler(T0_Proceso);

                // TODO: Add code here to start your service.
         
                T2.Interval = (Frecuencia_Timer);
                T2.AutoReset = true;
                T2.Enabled = true;
                T2.Start();
                T2.Elapsed += new ElapsedEventHandler(T1_Proceso);
            }
            catch (Exception ex)
            {
                this.WriteToFile("Error: " + ex.Message);
            }
        }

        public void OnStarTest()
        {
            this.WriteToFile("In OnStart.");

            T1.Interval = (Frecuencia_ServiciosActivos);
            T1.AutoReset = true;
            T1.Enabled = true;
            T1.Start();
            T1.Elapsed += new ElapsedEventHandler(T0_Proceso);

            // TODO: Add code here to start your service.

            T2.Interval = (Frecuencia_Timer);
            T2.AutoReset = true;
            T2.Enabled = true;
            T2.Start();
            T2.Elapsed += new ElapsedEventHandler(T1_Proceso);
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            T1.Enabled = false;
            T2.Enabled = false;

            this.WriteToFile("In OnStop.");
        }

        public void T0_Proceso(object sender, EventArgs e)
        {
            CargaServicios();
        }

        public void CargaServicios()
        {
            try
            {
                CasinoServicios = ser.GetViewServiciosActivos(idMaquinaInstancia);

                if (CasinoServicios.gResponseAPI.Status == "200")
                {
                    list = CasinoServicios.Coleccion.ToList();
                }
                else
                {
                    this.WriteToFile("Error: " + CasinoServicios.gResponseAPI.Response);
                }

            }
            catch (Exception ex)
            {
                this.WriteToFile("Error: " + ex.Message);
            }
        }

        public void T1_Proceso(object sender, EventArgs e)
        {
            try { 
                
                DateTime FechaActual = DateTime.Now;

                bool EjecutarProcesoGetPutSpool = false;
                bool EjecutarProcesoSendTransactions = false;
   
                if (FechaActual.ToShortTimeString() == HoraGetPutSpool.ToShortTimeString())
                {
                    EjecutarProcesoGetPutSpool = BuscaHoraSerivicio(HoraGetPutSpool);

                    //Console.WriteLine("Se ejecuto GetPutSpool: " + HoraGetPutSpool.ToShortTimeString());
                    this.WriteToFile("Se ejecuto GetPutSpool: " + HoraGetPutSpool.ToShortTimeString());
                    HoraGetPutSpool = HoraGetPutSpool.AddMinutes(Frecuencia_GetPutSpool);
                }

                if (FechaActual.ToShortTimeString() == HoraSendTransactions.ToShortTimeString())
                {
                    EjecutarProcesoSendTransactions = BuscaHoraSerivicio(HoraSendTransactions);
                    //Console.WriteLine("Se ejecuto SendTransactions: " + HoraSendTransactions.ToShortTimeString());
                    this.WriteToFile("Se ejecuto SendTransactions: " + HoraSendTransactions.ToShortTimeString());
                    HoraSendTransactions = HoraSendTransactions.AddMinutes(Frecuencia_SendTransactions);
                }


                if (EjecutarProcesoGetPutSpool)
                {
                    Process calc = new Process { StartInfo = { FileName = @"calc.exe" } };
                    calc.Start();

                    calc.WaitForExit();
                }

                if (EjecutarProcesoSendTransactions)
                {
                    Process notepad = new Process { StartInfo = { FileName = @"notepad.exe" } };
                    notepad.Start();

                    notepad.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                this.WriteToFile("Error: " + ex.Message);
            }

        }

        public bool BuscaHoraSerivicio(DateTime FechaCompara)
        {
          
            bool Retorna = true;

            try
            {
                foreach (var item in list.ToList())
                {
 
                    string hhmm1 = item.HoraDesde.Substring(0, 5);
                    string hhmm2 = item.HoraHasta.Substring(0, 5);

                    DateTime FechaInicio = Convert.ToDateTime(hhmm1);
                    DateTime FechaFin = Convert.ToDateTime(hhmm2);

                    if (FechaCompara >= FechaInicio & FechaCompara <= FechaFin)
                    { 
                        Retorna = false;
                        //Console.WriteLine("Encontro Rango Servicio: " + FechaInicio + ">="+ FechaCompara + "<="+  FechaFin);
                        this.WriteToFile("Encontro Rango Servicio: " + FechaInicio + ">=" + FechaCompara + "<=" + FechaFin);
                    }
               
                }            
            }
            catch (Exception ex)
            {
                this.WriteToFile("Error: " + ex.Message);
                Retorna = true;
            }

            return Retorna;
        }


        private void WriteToFile(string text)
        {
            string path = "C:\\ServiceLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
    }
}
