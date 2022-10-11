namespace MiddleWare
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
            this.STS_ZK_Comm = new System.ServiceProcess.ServiceProcessInstaller();
            this.STS_ZK = new System.ServiceProcess.ServiceInstaller();
            // 
            // STS_ZK_Comm
            // 
            this.STS_ZK_Comm.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.STS_ZK_Comm.Password = null;
            this.STS_ZK_Comm.Username = null;
            // 
            // STS_ZK
            // 
            this.STS_ZK.ServiceName = "STS_ZK_Communication";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.STS_ZK_Comm,
            this.STS_ZK});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller STS_ZK_Comm;
        private System.ServiceProcess.ServiceInstaller STS_ZK;
    }
}