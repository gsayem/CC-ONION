using CallCentre.Interfaces.Repository;
using CallCentre.Interfaces.Services;
using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Services
{
    public class CostingTimeSlotService : ICostingTimeSlotService
    {
        IRepository<CostingTimeSlot> _costingTimeSlotRepository;
        public CostingTimeSlotService(IRepository<CostingTimeSlot> costingTimeSlotRepository)
        {
            _costingTimeSlotRepository = costingTimeSlotRepository;
        }
        public ICollection<CostingTimeSlot> GetAllCostingTimeSlot()
        {
            return _costingTimeSlotRepository.GetAll();
        }

        public CostingTimeSlot InsertCostingTimeSlot(CostingTimeSlot costData)
        {
            _costingTimeSlotRepository.Add(costData);

            return costData;
        }
    }
}
