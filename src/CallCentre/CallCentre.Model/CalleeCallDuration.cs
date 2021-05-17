using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CallCentre.Model
{
    public class CalleeCallDuration : BaseModel
    {
        public DateTime CallDateTime { get; set; }
        public long CallDurationInSecond { get; set; }
        public decimal Charge { get; set; }
        public Guid Callee_Id { set; get; }

        [ForeignKey("Callee_Id")]
        public virtual Callee Callee { set; get; }
    }
}
