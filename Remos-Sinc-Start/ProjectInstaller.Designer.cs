namespace Remos_Sinc_Start
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            this.Remos_Sinc_Start = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = "Servicio Remos Sinc";
            this.serviceInstaller1.DisplayName = "RemosSinc";
            this.serviceInstaller1.ServiceName = "Remos_Sinc_Start";
            // 
            // Remos_Sinc_Start
            // 
            this.Remos_Sinc_Start.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.Remos_Sinc_Start.Password = null;
            this.Remos_Sinc_Start.Username = null;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceInstaller1,
            this.Remos_Sinc_Start});

        }

        #endregion
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
        public System.ServiceProcess.ServiceProcessInstaller Remos_Sinc_Start;
    }
}