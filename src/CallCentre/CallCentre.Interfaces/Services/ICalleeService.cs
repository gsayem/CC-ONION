using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Interfaces.Services
{
    public interface ICalleeService
    {
        Callee CreateCallee(Callee callee);
        Callee UpdateCallee(Callee callee);
        Callee DeleteCallee(Callee callee);
        Callee FindCallee(Guid calleeId);
        Callee FindCalleeByPhoneNumber(string phone);

        ICollection<Callee> GetCalleeList();
    }
}
