namespace EventLogListenerService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EventServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EventService = new System.ServiceProcess.ServiceInstaller();
            // 
            // EventServiceProcessInstaller
            // 
            this.EventServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.EventServiceProcessInstaller.Password = null;
            this.EventServiceProcessInstaller.Username = null;
            // 
            // EventService
            // 
            this.EventService.ServiceName = "EventService";
            this.EventService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EventServiceProcessInstaller,
            this.EventService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EventServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EventService;
    }
}