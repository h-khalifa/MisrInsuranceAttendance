using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare.ZK.models
{
    public class FPDevice
    {
        public string ip { get; set; }
        public int port { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Mac { get; set; }
        public string Serial { get; set; }
        public string FirmWare { get; set; }
        public string Platform { get; set; }
        public DateTime Production { get; set; }
    }
}
