using MiddleWare.Helpers;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            TasksScheduling();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new STS_ZK_Comm()
            };
            ServiceBase.Run(ServicesToRun);
        }
        static async Task TasksScheduling()
        {
            LogUtil.Debug("quartz.net scheduling started...");
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            DailyTimeIntervalTriggerImpl trigger = new DailyTimeIntervalTriggerImpl("Main Task", "group0", Quartz.TimeOfDay.HourAndMinuteOfDay(0, 0), Quartz.TimeOfDay.HourAndMinuteOfDay(23, 59), Quartz.IntervalUnit.Hour, 3);

            IJobDetail job = JobBuilder.Create<AttendanceCollectingJob>()
    .WithIdentity("Main Task", "group0") // name "myJob", group "group1"
    .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
