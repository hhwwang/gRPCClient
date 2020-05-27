using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UtilityApplications.Models
{
    public class TrialLinkModel
    {
        [DisplayName("State/Province:")]
        public string State { get; set; }

        [DisplayName("Environment:")]
        public int Server { get; set; }

        public string TrialURL { get; set; } = "";
    }
}
