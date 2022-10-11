using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal.Models.viewmodels
{
    [MetadataType(typeof(FingerPrintDeviceMetaData))]
    public partial class FingerPrintDevice
    {

    }
    public class FingerPrintDeviceMetaData
    {
        [StringLength(10, ErrorMessage = "First name must be 25 characters or less in length.")]
        [Required(ErrorMessage = "First name is required.")]
        public String FirstName { get; set; }
    }
}