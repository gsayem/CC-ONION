using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Interfaces.Services
{
    public interface ICalleeCallDurationService
    {
        CalleeCallDuration CreateCalleeCallDuration(CalleeCallDuration calleeCallDuration);
        CalleeCallDuration UpdatCalleeCallDuration(CalleeCallDuration calleeCallDuration);        
        CalleeCallDuration FindCalleeCallDuration(Guid Id);
        ICollection<CalleeCallDuration> GetCalleeList();
        void DeleteRecord(CalleeCallDuration calleeCallDuratio);
    }
}
