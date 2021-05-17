using CallCentre.Common.Extension;
using CallCentre.Interfaces.Services;
using CallCentreWeb.Extensions;
using CallCentreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreWeb.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] IUserService userService)
        {
            DbConnectionTest(userService);
            HttpContext.Session.Remove(SessionKey.CurrentUserName);
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginViewModel login, [FromServices] IUserService userService, [FromServices] IUserLoginHistoryService userLoginHistoryServic)
        {
            string message = String.Empty;

            Result code = Result.Failure;
            if (login == null)
            {
                message = "Login data not found";
            }
            if (message.IsNullOrWhiteSpace() && login.EmailAddress.IsNullOrWhiteSpace())
            {
                message = "User email address required";
            }
            if (message.IsNullOrWhiteSpace() && !login.EmailAddress.IsValidEmailAddress())
            {
                message = "Invalid email address format";
            }
            if (message.IsNullOrWhiteSpace() && login.Password.IsNullOrWhiteSpace())
            {
                message = "User password required";
            }
            if (message.IsNullOrWhiteSpace())
            {
                try
                {
                    var user = userService.FindUserForLogin(login.EmailAddress, login.Password.ToPasswordHash());
                    if (user != null)
                    {
                        userLoginHistoryServic.CreateUserLoginHistory(new CallCentre.Model.UserLoginHistory { LoginTime = DateTime.Now, User_Id = user.Id });
                        message = "Login success";
                        code = Result.Success;
                        HttpContext.Session.Set(SessionKey.CurrentUserName, user.GetFullName());
                    }
                    else
                    {
                        message = "Invalid user";
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
    }
}
