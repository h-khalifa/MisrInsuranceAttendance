using DataAceess;
using DataAceess.Models;
using DataAceess.Repositories;
using MiddleWare.Helpers;
using MiddleWare.ZK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MiddleWare.Controllers
{
    public class DevicesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ConnectAndSave(string Ip, int Port)
        {
            string link = Request.RequestUri.ToString();
            LogUtil.Debug(" DevicesController Called: " + link);
            LogUtil.Debug(" DevicesController IP: " + Ip);
            LogUtil.Debug(" DevicesController PORT: " + Port);
            

            try
            {
                Ip = Ip.Trim();
                UnitOfWork db = new UnitOfWork(new DataAceess.Models.AttendanceEntities(), new EmployesRepository(), new BaseRepository<Branch>(), new BaseRepository<FingerPrintDevice>(), new BaseRepository<AttendanceLog>());

                var temp = db.DevicesRepo.Find(d => d.IP == Ip && d.Port == Port);
                int deviceID = -1;
                
                if (temp == null)
                {
                    DeviceCommunicator communicator = new DeviceCommunicator(Ip, Port);

                    bool connected = communicator.Connect();
                    if (connected)
                    {
                        FingerPrintDevice device = new FingerPrintDevice()
                        {
                            IP = Ip,
                            Port = Port
                        };
                        var deviceInfo = communicator.GetDeviceInfo();
                        device.FirmwareVer = deviceInfo.FirmWare;
                        device.Mac = deviceInfo.Mac;
                        device.Platform = deviceInfo.Platform;
                        device.ProductionTime = deviceInfo.Production;
                        device.SerialNumber = deviceInfo.Serial;
                        device.Vendor = deviceInfo.Vendor;
                        device.Model = deviceInfo.Name;
                        var dev = db.DevicesRepo.Add(device);
                        db.Submit();
                        db.Dispose();

                        communicator.Disconnect();
                        deviceID = dev.ID;
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadGateway, communicator.LatestError);
                    }
                }
                else
                {
                    deviceID = temp.ID;
                    return Request.CreateResponse(HttpStatusCode.Conflict, "The Information entered already existing with ID: " + temp.ID);
                }
                return Request.CreateResponse(HttpStatusCode.OK, deviceID);
            }
            catch(Exception ex)
            {
                LogUtil.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


            [HttpGet]
        public HttpResponseMessage UpdateInfo(int id)
        {
            string link = Request.RequestUri.ToString();
            LogUtil.Debug(" DevicesController Called: " + link);
            try
            {
                UnitOfWork db = new UnitOfWork(new DataAceess.Models.AttendanceEntities(), new EmployesRepository(), new BaseRepository<Branch>(), new BaseRepository<FingerPrintDevice>(), new BaseRepository<AttendanceLog>());

                var device = db.DevicesRepo.GetById(id);
                if(device == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                DeviceCommunicator communicator = new DeviceCommunicator(device.IP, (int)device.Port.Value);

                bool connected = communicator.Connect();
                if (connected)
                {
                    var deviceInfo = communicator.GetDeviceInfo();
                    device.FirmwareVer = deviceInfo.FirmWare;
                    device.Mac = deviceInfo.Mac;
                    device.Platform = deviceInfo.Platform;
                    device.ProductionTime = deviceInfo.Production;
                    device.SerialNumber = deviceInfo.Serial;
                    device.Vendor = deviceInfo.Vendor;
                    device.Model = deviceInfo.Name;
                    db.DevicesRepo.Edit(device);
                    db.Submit();
                    db.Dispose();

                    communicator.Disconnect();
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadGateway, communicator.LatestError);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                LogUtil.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }
    }
}
