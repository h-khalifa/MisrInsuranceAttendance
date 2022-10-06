using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare.ZK.models
{
    public class AttLog
    {
        public string EmpCode { get; set; }
        public DateTime time { get; set; }
        public dwVerifyMode vericationMode { get; set; }
        public dwInOutMode AttStatus { get; set; }

    }

    public enum dwVerifyMode
    {
        password = 0,
        finger = 1,
        card = 2

    }

    public enum dwInOutMode
    {
        CheckIn,
        CheckOut,
        BreakOut,
        BreakIn,
        OT_In,
        OT_Out,
    }
}
