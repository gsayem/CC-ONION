using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Interfaces.Services
{
    public interface ICostingTimeSlotService
    {
        CostingTimeSlot InsertCostingTimeSlot(CostingTimeSlot costData);
        ICollection<CostingTimeSlot> GetAllCostingTimeSlot();
    }
}
