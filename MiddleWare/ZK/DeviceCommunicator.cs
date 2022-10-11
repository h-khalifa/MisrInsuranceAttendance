using MiddleWare.Helpers;
using MiddleWare.ZK.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare.ZK
{
    public class DeviceCommunicator
    {
        zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        string ip;
        int port;
        bool res;
        bool isConnected;
        int machineNumber = 1;
        string ZKDateTimeFormat = "yyyy-MM-dd hh:mm:ss";

        public DeviceCommunicator(string Ip, int Port)
        {
            ip = Ip;
            port = Port;
        }

        public bool Connect()
        {
            axCZKEM1.OnConnected += AxCZKEM1_OnConnected;
            axCZKEM1.OnDisConnected += AxCZKEM1_OnDisConnected;
            res = axCZKEM1.Connect_Net(ip, port);
            isConnected = res;
            if (isConnected)
            {
                machineNumber = axCZKEM1.MachineNumber;
            }
            else
            {
                LogError();
            }
            return res;
        }

        public FPDevice GetDeviceInfo()
        {
            FPDevice device = new FPDevice();
            device.ip = ip;
            device.port = port;

            if (isConnected)
            {
                axCZKEM1.EnableDevice(machineNumber, false);//disable the device

                string sn;
                res = axCZKEM1.GetSerialNumber(machineNumber, out sn);
                if (res)
                {
                    LogUtil.Debug("1. Serial Number: " + sn);
                    device.Serial = sn;
                }
                else
                {
                    LogError();
                }

                string mac = "";
                res = axCZKEM1.GetDeviceMAC(machineNumber, ref mac);
                if (res)
                {
                    LogUtil.Debug("2. Mac Address: " + mac);
                    device.Mac = mac;
                }
                else
                {
                    LogError();
                }

                string vendor = "";
                res = axCZKEM1.GetVendor(ref vendor);
                if (res)
                {
                    LogUtil.Debug("3. Vendor: " + vendor);
                    device.Vendor = vendor;
                }
                else
                {
                    LogError();
                }

                string deviceName;
                res = axCZKEM1.GetProductCode(machineNumber, out deviceName);
                if (res)
                {
                    LogUtil.Debug("4. Device Name: " + deviceName);
                    device.Name = deviceName;
                }
                else
                {
                    LogError();
                }

                string firmVer = "";
                res = axCZKEM1.GetFirmwareVersion(machineNumber, ref firmVer);
                if (res)
                {
                    device.FirmWare = firmVer;
                    LogUtil.Debug("5. FirmWare version: " + firmVer);
                }
                else
                {
                    LogError();
                }

                string platform = "";
                res = axCZKEM1.GetPlatform(machineNumber, ref platform);
                if (res)
                {
                    device.Platform = platform;
                    LogUtil.Debug("6. Platform: " + platform);
                }
                else
                {
                    LogError();
                }

                string prodTime = "";
                res = axCZKEM1.GetDeviceStrInfo(machineNumber, 1, out prodTime);
                if (res)
                {
                    LogUtil.Debug("7. Device info (product time): " + prodTime);
                    DateTime temp;
                    res = DateTime.TryParse(prodTime, out temp);
                    if (res)
                    {
                        device.Production = DateTime.Parse(prodTime);
                    }
                }



                axCZKEM1.EnableDevice(machineNumber, true);//enable the device
            }


            return device;
        }

        public List<AttLog> GetAllAttendance()
        {
            List<AttLog> AttLogs = new List<AttLog>();

            axCZKEM1.EnableDevice(machineNumber, false);//disable the device
            if (axCZKEM1.ReadGeneralLogData(machineNumber))
            {
                string sdwEnrollNumber = "";
                int idwVerifyMode = 0;
                int idwInOutMode = 0;
                int idwYear = 0;
                int idwMonth = 0;
                int idwDay = 0;
                int idwHour = 0;
                int idwMinute = 0;
                int idwSecond = 0;
                int idwWorkcode = 0;

                while (axCZKEM1.SSR_GetGeneralLogData(machineNumber, out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    AttLog log = new AttLog()
                    {
                        AttStatus = (dwInOutMode)idwInOutMode,
                        EmpCode = sdwEnrollNumber,
                        vericationMode = (dwVerifyMode)idwVerifyMode,
                        time = DateTime.Parse(idwYear + "-" + idwMonth + "-" + idwDay + " " + idwHour + ":" + idwMinute + ":" + idwSecond)
                    };
                    AttLogs.Add(log);
                }
            }
            else
            {
                LogError();
            }
            axCZKEM1.EnableDevice(machineNumber, true);//enable the device
            return AttLogs;
        }
        public List<AttLog> GetAttendanceByPeriod(DateTime starting, DateTime ending)
        {
            List<AttLog> AttLogs = new List<AttLog>();

            axCZKEM1.EnableDevice(machineNumber, false);//disable the device
            if (axCZKEM1.ReadTimeGLogData(machineNumber, starting.ToString(ZKDateTimeFormat), ending.ToString(ZKDateTimeFormat)))
            {
                string sdwEnrollNumber = "";
                int idwVerifyMode = 0;
                int idwInOutMode = 0;
                int idwYear = 0;
                int idwMonth = 0;
                int idwDay = 0;
                int idwHour = 0;
                int idwMinute = 0;
                int idwSecond = 0;
                int idwWorkcode = 0;

                while (axCZKEM1.SSR_GetGeneralLogData(machineNumber, out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    AttLog log = new AttLog()
                    {
                        AttStatus = (dwInOutMode)idwInOutMode,
                        EmpCode = sdwEnrollNumber,
                        vericationMode = (dwVerifyMode)idwVerifyMode,
                        time = DateTime.Parse(idwYear + "-" + idwMonth + "-" + idwDay + " " + idwHour + ":" + idwMinute + ":" + idwSecond)
                    };
                    AttLogs.Add(log);
                }
            }
            else
            {
                LogError();
            }
            axCZKEM1.EnableDevice(machineNumber, true);//enable the device
            return AttLogs;
        }

        public void Disconnect()
        {
            axCZKEM1.Disconnect();
            //return res;
        }

        private void AxCZKEM1_OnDisConnected()
        {
            string msg = "Disconnected from device: " + ip;
            LogUtil.Debug(msg);
            isConnected = false;
        }

        private void AxCZKEM1_OnConnected()
        {
            string msg = "Connected to device: " + ip;
            LogUtil.Debug(msg);
            isConnected = true;
        }

        private void LogError()
        {
            int idwErrorCode = 0;
            axCZKEM1.GetLastError(ref idwErrorCode);
            string msg;
            switch (idwErrorCode)
            {
                case 0:
                    msg = "Connected successfully";
                    break;
                case 1:
                    msg = "Failed to invoke the interface";
                    break;
                case 2:
                    msg = "Failed to initialize";
                    break;
                case 3:
                    msg = "Failed to initialize parameters";
                    break;
                //case 4:
                //    msg = "";
                //    break;
                case 5:
                    msg = "Data mode read error";
                    break;
                case 6:
                    msg = "Wrong password";
                    break;
                case 7:
                    msg = "Reply error";
                    break;
                case 8:
                    msg = "Receive timeout";
                    break;
                case 307:
                    msg = "Connection timeout";
                    break;
                case 201:
                    msg = "Device is busy";
                    break;
                default:
                    msg = "undefinded error code: " + idwErrorCode;
                    break;
            }

            LogUtil.Debug(msg);
        }
    }

}
