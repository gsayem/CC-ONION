using CallCentre.Interfaces.Services;
using CallCentre.Model;
using CallCentreWeb.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreWeb.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            if (HttpContext != null)
            {
                ViewBag.CurrentUserName = HttpContext.Session.Get(SessionKey.CurrentUserName);
            }
        }

        public void DbConnectionTest(IUserService userService)
        {
            bool IsDbConnectSuccess = false;
            try
            {
                userService.GetUserList();
                IsDbConnectSuccess = true;
            }
            catch (Exception)
            {

            }
            HttpContext.Session.Set(SessionKey.DbConnectionStatus, IsDbConnectSuccess.ToString());
            if (IsDbConnectSuccess)
            {
                HttpContext.Session.Set(SessionKey.DbConnectionStatusText, "Database Connection Success");
            }
            else
            {
                HttpContext.Session.Set(SessionKey.DbConnectionStatusText, "Database Connection failed, please check the connection string");
            }
            ViewBag.IsDbConnectSuccess = IsDbConnectSuccess;
        }

        public void InsertCostingData(ICostingTimeSlotService costService)
        {
            if (costService.GetAllCostingTimeSlot().Count == 0)
            {
                costService.InsertCostingTimeSlot(new CostingTimeSlot
                {
                    Id = Guid.NewGuid(),
                    StartHour = 6,
                    StartMinute = 1,
                    EndHour = 12,
                    EndMinute = 0,
                    CostPerMinute = 5f
                });
                costService.InsertCostingTimeSlot(new CostingTimeSlot
                {
                    Id = Guid.NewGuid(),
                    StartHour = 12,
                    StartMinute = 1,
                    EndHour = 18,
                    EndMinute = 0,
                    CostPerMinute = 5.5f
                });
                costService.InsertCostingTimeSlot(new CostingTimeSlot
                {
                    Id = Guid.NewGuid(),
                    StartHour = 18,
                    StartMinute = 1,
                    EndHour = 24,
                    EndMinute = 0,
                    CostPerMinute = 6f
                });
                costService.InsertCostingTimeSlot(new CostingTimeSlot
                {
                    Id = Guid.NewGuid(),
                    StartHour = 0,
                    StartMinute = 1,
                    EndHour = 6,
                    EndMinute = 0,
                    CostPerMinute = 7f
                });
            }
        }
    }
}
