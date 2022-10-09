using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare.Helpers
{
    public class LogUtil
    {
        private static readonly ILog log = LogManager.GetLogger("RollingFile");


        static LogUtil()
        {
            XmlConfigurator.Configure();
        }

        //-----Normal Log functions , used in all over the solution
        public static void Debug(object debugInfo, Exception exception)
        {
            log.Debug(debugInfo, exception);
        }
        public static void Debug(object debugInfo)
        {
            log.Debug(debugInfo);
        }
        public static void Fatal(object debugInfo)
        {
            log.Fatal(debugInfo);
        }
        public static void Fatal(object debugInfo, Exception exception)
        {
            log.Fatal(debugInfo, exception);
        }

        public static void Error(object debugInfo)
        {
            log.Error(debugInfo);
        }
        public static void Error(object debugInfo, Exception exception)
        {
            log.Error(debugInfo, exception);
        }
        public static void InfoFormat(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }


    }

}
