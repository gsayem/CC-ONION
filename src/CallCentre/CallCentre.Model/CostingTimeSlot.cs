using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Model
{
    public class CostingTimeSlot : BaseModel
    {
        public CostingTimeSlot()
        {

        }

        public int StartHour { set; get; }
        public int StartMinute { set; get; }
        public int EndHour { set; get; }
        public int EndMinute { set; get; }
        public float CostPerMinute { set; get; }
    }
}
