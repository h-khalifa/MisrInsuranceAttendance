using MiddleWare.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public STS_ZK_Comm()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogUtil.Debug("SERVICE STARTED!");
        }

        protected override void OnStop()
        {
            LogUtil.Debug("SERVICE STOPPED!");
        }
    }
}
