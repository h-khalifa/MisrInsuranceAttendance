using MiddleWare.Helpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare
{
    public class AttendanceCollectingJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                LogUtil.Debug("Scheduled task started!.");
                //1. get all defined devices in db
                //2. load all logs from each device according to latest communication time
                //3. push all logs to db
            }
            catch(Exception ex)
            {
                LogUtil.Error(ex);
            }
            return Task.CompletedTask;
        }
    }
}
