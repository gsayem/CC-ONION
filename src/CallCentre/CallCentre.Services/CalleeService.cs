using CallCentre.Common.Extension;
using CallCentre.Interfaces.Repository;
using CallCentre.Interfaces.Services;
using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCentre.Services
{
    public class CalleeService : ICalleeService
    {
        private readonly IUnitOfWork _unitOfWork;
        IRepository<Callee> _calleeRepository;
        public CalleeService(IUnitOfWork unitOfWork, IRepository<Callee> calleeRepository)
        {
            _unitOfWork = unitOfWork;
            _calleeRepository = calleeRepository;
        }
        public Callee CreateCallee(Callee callee)
        {
            if (callee.FirstName.IsNullOrWhiteSpace())
            {
                throw new Exception("First name required");
            }
            if (callee.Lastname.IsNullOrWhiteSpace())
            {
                throw new Exception("Last name required");
            }
            if (callee.Phone.IsNullOrWhiteSpace())
            {
                throw new Exception("Phone number required");
            }

            _calleeRepository.Add(callee);
            return callee;
        }

        public Callee DeleteCallee(Callee callee)
        {
            var uowCalleeRepository = _unitOfWork.GetRepository<Callee>();
            var uowCalleeCallDurationRepository = _unitOfWork.GetRepository<CalleeCallDuration>();            
            foreach (var item in callee.CalleeCallDurations)
            {
                uowCalleeCallDurationRepository.Remove(item);
            }
            uowCalleeRepository.Remove(callee);
            _unitOfWork.SaveChanges();
            return callee;
        }

        public Callee FindCallee(Guid calleeId)
        {
            return _calleeRepository.GetById(calleeId);
        }

        public Callee FindCalleeByPhoneNumber(string phone)
        {
            return _calleeRepository.Find(s => s.Phone == phone);
        }

        public ICollection<Callee> GetCalleeList()
        {
            return _calleeRepository.GetAll();
        }

        public Callee UpdateCallee(Callee callee)
        {
            if (callee.FirstName.IsNullOrWhiteSpace())
            {
                throw new Exception("First name required");
            }
            if (callee.Lastname.IsNullOrWhiteSpace())
            {
                throw new Exception("Last name required");
            }
            if (callee.Phone.IsNullOrWhiteSpace())
            {
                throw new Exception("Phone number required");
            }

            _calleeRepository.Update(callee);
            return callee;
        }

    }
}
