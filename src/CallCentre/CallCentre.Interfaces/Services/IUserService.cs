using CallCentre.Model;
using System;
using System.Collections.Generic;

namespace CallCentre.Interfaces.Services
{
    public interface IUserService
    {
        ApplicationUser CreateUser(ApplicationUser user);
        ApplicationUser UpdateUser(ApplicationUser user);
        ApplicationUser DeleteUser(ApplicationUser user);
        ApplicationUser FindUser(Guid userId);
        ApplicationUser FindUserByEmail(string EmailAddress);
        ApplicationUser FindUserForLogin(string EmailAddress, string Password);
        ICollection<ApplicationUser> GetUserList();
    }
}
