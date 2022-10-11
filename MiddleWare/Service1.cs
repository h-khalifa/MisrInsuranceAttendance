using Microsoft.Owin.Hosting;
using MiddleWare.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare
{
    public partial class STS_ZK_Comm : ServiceBase
    {
        public string baseAddress;
        private IDisposable _server;

        public STS_ZK_Comm()
        {
            InitializeComponent();
            baseAddress = ConfigurationManager.AppSettings["MiddlewareBaseAddress"].ToString();
        }

        protected override void OnStart(string[] args)
        {
            LogUtil.Debug("SERVICE STARTED!");
            _server = WebApp.Start<Startup1>(url: baseAddress);
            LogUtil.Debug("Serivice started at :" + baseAddress);

        }

        protected override void OnStop()
        {
            LogUtil.Debug("SERVICE STOPPED!");
        }
    }
}
