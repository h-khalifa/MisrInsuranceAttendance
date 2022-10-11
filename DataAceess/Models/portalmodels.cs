using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAceess.Models
{
    [MetadataType(typeof(FingerPrintDeviceMetaData))]
    public partial class FingerPrintDevice
    {

    }
    public class FingerPrintDeviceMetaData
    {
        [Display(Name ="Device Name")]
        public string Name { get; set; }
        [Display(Name = "Device Description")]
        public string Desc { get; set; }
        [Display(Name = "Device Location")]
        public string LocationDesc { get; set; }
        [Display(Name = "Ip Address"), Required]
        public string IP { get; set; }
        [Display(Name = "Port"), Required]
        public Nullable<long> Port { get; set; }
        [Display(Name = "Mac Address")]
        public string Mac { get; set; }
        [Display(Name = "Device Model")]
        public string Model { get; set; }
        [Display(Name = "Vendor")]
        public string Vendor { get; set; }
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        [Display(Name = "Firmware Version")]
        public string FirmwareVer { get; set; }
        [Display(Name = "Platform")]
        public string Platform { get; set; }
        [Display(Name = "Production Time")]
        public Nullable<System.DateTime> ProductionTime { get; set; }
        [Display(Name = "Latest Connection Time")]
        public Nullable<System.DateTime> LatestLogTime { get; set; }
    }

    [MetadataType(typeof(BranchMetaData))]
    public partial class Branch
    {

    }

    public class BranchMetaData
    {
        [Display(Name = "Branch Name"), Required]
        public string Name { get; set; }
        [Display(Name = "Branch Address")]
        public string Desc { get; set; }

    }
}
