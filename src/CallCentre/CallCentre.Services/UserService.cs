using CallCentre.Common.Extension;
using CallCentre.Interfaces.Repository;
using CallCentre.Interfaces.Services;
using CallCentre.Model;
using System;
using System.Collections.Generic;

namespace CallCentre.Services
{
    public class UserService : IUserService
    {
        IRepository<ApplicationUser> _userRepository;
        public UserService(IRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }
        public ApplicationUser CreateUser(ApplicationUser user)
        {
            if (user.GivenName.IsNullOrWhiteSpace())
            {
                throw new Exception("User given name required");
            }
            //if (user.Surname.IsNullOrWhiteSpace())
            //{
            //    throw new Exception("User surname required");
            //}
            if (user.EmailAddress.IsNullOrWhiteSpace())
            {
                throw new Exception("User Email Address required");
            }
            if (user.PasswordHash.IsNullOrWhiteSpace())
            {
                throw new Exception("User Password required");
            }

            _userRepository.Add(user);
            return user;
        }

        public ApplicationUser DeleteUser(ApplicationUser user)
        {
            _userRepository.Remove(user);
            return user;
        }

        public ApplicationUser FindUser(Guid userId)
        {
            return _userRepository.GetById(userId);
        }
        public ApplicationUser FindUserByEmail(string EmailAddress)
        {
            return _userRepository.Find(u => u.EmailAddress == EmailAddress);
        }
        public ApplicationUser FindUserForLogin(string EmailAddress, string Password)
        {
            return _userRepository.Find(u => u.EmailAddress == EmailAddress && u.PasswordHash == Password);
        }

        public ICollection<ApplicationUser> GetUserList()
        {
            return _userRepository.GetAll();
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            if (user.GivenName.IsNullOrWhiteSpace())
            {
                throw new Exception("User given name required");
            }
            if (user.Surname.IsNullOrWhiteSpace())
            {
                throw new Exception("User surname required");
            }
            if (user.EmailAddress.IsNullOrWhiteSpace())
            {
                throw new Exception("User Email Address required");
            }
            if (user.PasswordHash.IsNullOrWhiteSpace())
            {
                throw new Exception("User Password required");
            }

            _userRepository.Update(user);
            return user;
        }

    }
}
