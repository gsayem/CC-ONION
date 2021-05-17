using CallCentre.Model;
using System;
using System.Collections.Generic;

namespace CallCentre.Interfaces.Services
{
    public interface IUserLoginHistoryService
    {
        UserLoginHistory CreateUserLoginHistory(UserLoginHistory history);
        UserLoginHistory GetUserLastLoginInformation(Guid userId);
        ICollection<UserLoginHistory> GetUserHistoryList(Guid userId);
        ICollection<UserLoginHistory> GetUserHistoryList();
    }
}
