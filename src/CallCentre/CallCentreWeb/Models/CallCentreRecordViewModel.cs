using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreWeb.Models
{
    public class CallCentreRecordViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string LastCallDateTime { get; set; }
        public string TotalCallDuration { get; set; }
        public string TotalCharge { get; set; }
    }

    public class CallDurationViewModel
    {
        public CallDurationViewModel()
        {
            CallDurationList = new List<CallDurationViewModel>();
        }
        public string Id { get; set; }
        public string CalleeId { get; set; }
        public string CallDateTime { get; set; }
        public string CallDuration { get; set; }
        public string Charge { get; set; }
        public List<CallDurationViewModel> CallDurationList { set; get; }
    }
}
