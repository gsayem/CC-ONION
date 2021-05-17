using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CallCentre.Model
{
    public class Callee : BaseModel
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual ICollection<CalleeCallDuration> CalleeCallDurations { set; get; }
    }    
}
