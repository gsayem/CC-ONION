using CallCentre.Common.Extension;
using CallCentre.Interfaces.Services;
using CallCentre.Model;
using CallCentreWeb.Extensions;
using CallCentreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreWeb.Controllers
{
    public class UserController : BaseController
    {
        public UserController() : base()
        {

        }
        public IActionResult UserRegistration()
        {
            return View();
        }

        public IActionResult UserList([FromServices] IUserService userService)
        {
            List<UserLoginHistoryViewModel> userLoginHistoryViewModels = new List<UserLoginHistoryViewModel>();
            userService.GetUserList().ToList().ForEach(f =>
            {
                userLoginHistoryViewModels.Add(new UserLoginHistoryViewModel
                {
                    UserId = f.Id.ToString(),
                    EmailAddress = f.EmailAddress,
                    GivenName = f.GivenName,
                    Surname = f.Surname,
                    LastLogin = f.UserLoginHistories.Count() == 0 ? "NEVER" : f.UserLoginHistories.OrderByDescending(s => s.LoginTime).FirstOrDefault()?.LoginTime.ToDisplayDateFormat()
                });
            });
            return View(userLoginHistoryViewModels);
        }

        [HttpPost]
        public JsonResult UserRegistration(UserViewModel user, [FromServices] IUserService userService)
        {
            string message = String.Empty;
            string GivenName = string.Empty;
            string Surname = string.Empty;

            Result code = Result.Failure;
            if (user == null)
            {
                message = "User data not found";
            }

            if (message.IsNullOrWhiteSpace() && user.FullName.IsNullOrWhiteSpace())
            {
                message = "User full name required";
            }
            else
            {
                GivenName = user.FullName;
                var splitName = user.FullName.Trim().Split(" ");
                if (splitName.Length > 1)
                {
                    Surname = splitName[splitName.Length - 1]; // Last part is surname
                    GivenName = string.Join(" ", splitName.Take(splitName.Length - 1)); // Rest of the items go for given name
                }
            }

            if (message.IsNullOrWhiteSpace() && user.EmailAddress.IsNullOrWhiteSpace())
            {
                message = "User email address required";
            }
            if (message.IsNullOrWhiteSpace() && !user.EmailAddress.IsValidEmailAddress())
            {
                message = "Invalid email address format";
            }
            if (message.IsNullOrWhiteSpace() && user.Password.IsNullOrWhiteSpace())
            {
                message = "Password required";
            }
            if (message.IsNullOrWhiteSpace() && user.Password.Length < 6)
            {
                message = "Password must be atleast 6 characters in length";
            }
            if (message.IsNullOrWhiteSpace() && user.ConfirmPassword.IsNullOrWhiteSpace())
            {
                message = "Confirm password required";
            }
            if (message.IsNullOrWhiteSpace() && user.ConfirmPassword != user.Password)
            {
                message = "Password and confirm password are not matched";
            }
            if (message.IsNullOrWhiteSpace())
            {
                try
                {
                    if (userService.FindUserByEmail(user.EmailAddress) == null)
                    {
                        userService.CreateUser(new ApplicationUser
                        {
                            GivenName = GivenName,
                            Surname = Surname,
                            PasswordHash = user.Password.ToPasswordHash(),
                            EmailAddress = user.EmailAddress
                        });
                        message = "Your registration is successful";
                        code = Result.Success;
                    }
                    else
                    {
                        code = Result.Warning;
                        message = "Email address already taken, please choose another one.";
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }

            var response = new ResponseMessage { Message = message, Code = code.ToString() };

            return Json(response);

        }

        [HttpGet]
        public JsonResult GetUserInformation(string userId, [FromServices] IUserService userService,
            [FromServices] IUserLoginHistoryService userLoginHistoryService)
        {
            string message = String.Empty;
            UserLoginHistoryViewModel userLoginHistoryViewModel = null;

            Result code = Result.Failure;
            if (userId == null)
            {
                message = "user Id not found";
            }

            if (message.IsNullOrWhiteSpace())
            {
                try
                {
                    Guid? guidUserId = userId.ToGuid();

                    if (guidUserId != null)
                    {
                        var user = userService.FindUser(guidUserId.Value);
                        if (user != null)
                        {
                            userLoginHistoryViewModel = new UserLoginHistoryViewModel
                            {
                                UserId = user.Id.ToString(),
                                EmailAddress = user.EmailAddress,
                                GivenName = user.GivenName,
                                Surname = user.Surname,
                                LastLogin = userLoginHistoryService.GetUserLastLoginInformation(user.Id) == null ? "NEVER" : userLoginHistoryService.GetUserLastLoginInformation(user.Id)?.LoginTime.ToDisplayDateFormat()
                            };
                            message = "Success";
                            code = Result.Success;
                        }
                        else
                        {
                            message = "user not found";
                        }
                    }
                    else
                    {
                        message = "Invalid user Id";
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }

            var response = new ResponseMessage { Message = message, Code = code.ToString(), Data = userLoginHistoryViewModel };

            return Json(response);

        }
    }
}
