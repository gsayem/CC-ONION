using CallCentre.Common.Extension;
using CallCentre.Interfaces.Repository;
using CallCentre.Interfaces.Services;
using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Services
{
    public class CalleeCallDurationService : ICalleeCallDurationService
    {
        IRepository<CalleeCallDuration> _calleeCallDurationRepository;
        public CalleeCallDurationService(IRepository<CalleeCallDuration> calleeCallDurationRepository)
        {
            _calleeCallDurationRepository = calleeCallDurationRepository;
        }
        public CalleeCallDuration CreateCalleeCallDuration(CalleeCallDuration calleeCallDuration)
        {
            if (calleeCallDuration.CallDateTime == null)
            {
                throw new Exception("Call date time required");
            }
            if (calleeCallDuration.CallDurationInSecond <= 0)
            {
                throw new Exception("Call duration can't be zero or negetive.");
            }
            if (calleeCallDuration.Charge < 0)
            {
                throw new Exception("Charge can't be negetive");
            }

            _calleeCallDurationRepository.Add(calleeCallDuration);
            return calleeCallDuration;
        }

        public CalleeCallDuration UpdatCalleeCallDuration(CalleeCallDuration calleeCallDuration)
        {
            if (calleeCallDuration.CallDateTime == null)
            {
                throw new Exception("Call date time required");
            }
            if (calleeCallDuration.CallDurationInSecond <= 0)
            {
                throw new Exception("Call duration can't be zero");
            }
            if (calleeCallDuration.Charge <= 0)
            {
                throw new Exception("Charge can't be negetive");
            }

            _calleeCallDurationRepository.Update(calleeCallDuration);
            return calleeCallDuration;
        }

        CalleeCallDuration ICalleeCallDurationService.FindCalleeCallDuration(Guid Id)
        {
            return _calleeCallDurationRepository.GetById(Id);
        }

        ICollection<CalleeCallDuration> ICalleeCallDurationService.GetCalleeList()
        {
            return _calleeCallDurationRepository.GetAll();
        }
        public void DeleteRecord(CalleeCallDuration calleeCallDuration)
        {
            _calleeCallDurationRepository.Remove(calleeCallDuration);
        }
    }
}
