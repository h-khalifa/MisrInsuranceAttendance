using DataAceess;
using DataAceess.Interfaces;
using DataAceess.Models;
using DataAceess.Repositories;
using MiddleWare.Helpers;
using MiddleWare.ZK;
using MiddleWare.ZK.models;
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

                UnitOfWork db = new UnitOfWork(new DataAceess.Models.AttendanceEntities(), new EmployesRepository(), new BaseRepository<Branch>(), new BaseRepository<FingerPrintDevice>(), new BaseRepository<AttendanceLog>());

                var devices = db.DevicesRepo.GetAll();

                DeviceCommunicator communicator;

                foreach (var device in devices)
                {
                    communicator = new DeviceCommunicator(device.IP, (int)device.Port.Value);
                    bool connected = communicator.Connect();
                    if (connected)
                    {
                        List<AttLog> logs;
                        var latestConn = device.LatestLogTime;
                        if (latestConn.HasValue)
                        {
                            logs = communicator.GetAttendanceByPeriod(latestConn.Value, DateTime.Now);
                        }
                        else
                        {
                            logs = communicator.GetAllAttendance();
                        }
                        communicator.Disconnect();


                        IEnumerable<AttendanceLog> attendanceLogs = logs.Select(l => new AttendanceLog()
                        {
                            DeviceID = device.ID,
                            LogTime = l.time,
                            LogType = l.AttStatus.ToString(),
                            EmployeID = db.EmployeRepo.GetOrCreateEmployeByCode(l.EmpCode).ID,
                        });
                        db.LogsRepo.AddRange(attendanceLogs);
                        db.Submit();
                    }
                    else
                    {
                        LogUtil.Debug("Connection Error to Device: " + device.IP);
                    }
                }


            }
            catch(Exception ex)
            {
                LogUtil.Error(ex);
            }
            return Task.CompletedTask;
        }
    }
}
