using CallCentre.Interfaces.Repository;
using CallCentre.Interfaces.Services;
using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallCentre.Services
{
    public class UserLoginHistoryService : IUserLoginHistoryService
    {
        IRepository<UserLoginHistory> _userLoginHistoryRepository;
        public UserLoginHistoryService(IRepository<UserLoginHistory> userLoginHistoryRepository)
        {
            _userLoginHistoryRepository = userLoginHistoryRepository;
        }
        public UserLoginHistory CreateUserLoginHistory(UserLoginHistory history)
        {
            _userLoginHistoryRepository.Add(history);
            return history;
        }
        public UserLoginHistory GetUserLastLoginInformation(Guid userId)
        {
            return _userLoginHistoryRepository.FindAll(s => s.User_Id == userId).OrderByDescending(o => o.LoginTime).FirstOrDefault();
        }

        public ICollection<UserLoginHistory> GetUserHistoryList(Guid userId)
        {
            return _userLoginHistoryRepository.FindAll(s => s.User_Id == userId);
        }

        public ICollection<UserLoginHistory> GetUserHistoryList()
        {
            return _userLoginHistoryRepository.GetAll();
        }
    }
}
