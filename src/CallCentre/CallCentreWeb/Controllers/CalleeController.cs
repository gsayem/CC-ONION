using CallCentre.Common.Extension;
using CallCentre.Interfaces.Services;
using CallCentre.Model;
using CallCentreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreWeb.Controllers
{
    public class CalleeController : BaseController
    {
        private readonly ICostingTimeSlotService _costService;
        public CalleeController([FromServices] ICostingTimeSlotService costService)
        {
            _costService = costService;
            InsertCostingData(costService);
        }
        public IActionResult CalleeList([FromServices] ICalleeService calleeService)
        {
            List<CallCentreRecordViewModel> callCentreRecordList = new List<CallCentreRecordViewModel>();
            calleeService.GetCalleeList().ToList().ForEach(f =>
            {
                callCentreRecordList.Add(new CallCentreRecordViewModel
                {
                    Id = f.Id.ToString(),
                    FirstName = f.FirstName,
                    Lastname = f.Lastname,
                    Phone = f.Phone,
                    LastCallDateTime = f.CalleeCallDurations.Count() == 0 ? "NEVER" : f.CalleeCallDurations.OrderByDescending(s => s.CallDateTime).FirstOrDefault()?.CallDateTime.ToDisplayDateFormat(),
                    TotalCallDuration = f.CalleeCallDurations.Count() == 0 ? "NEVER" : TimeSpan.FromSeconds(f.CalleeCallDurations.Sum(s => s.CallDurationInSecond)).ToString(),
                    TotalCharge = f.CalleeCallDurations.Count() == 0 ? "NEVER" : f.CalleeCallDurations.Sum(s => s.Charge).ToString()
                });
            });
            return View(callCentreRecordList);
        }

        public IActionResult Create(string id, [FromServices] ICalleeService calleeService)
        {
            var model = new CallCentreRecordViewModel();
            if (!id.IsNullOrEmpty())
            {
                Guid? guidId = id.ToGuid();
                if (guidId != null)
                {
                    var callee = calleeService.FindCallee(guidId.Value);
                    model.Id = callee.Id.ToString();
                    model.FirstName = callee.FirstName;
                    model.Lastname = callee.Lastname;
                    model.Address = callee.Address;
                    model.Phone = callee.Phone;
                }

            }
            return View(model);
        }

        public IActionResult CalleeDuration(string calleeId, string durationId, [FromServices] ICalleeService calleeService, [FromServices] ICalleeCallDurationService calleeCallDurationService)
        {
            var model = new CallDurationViewModel();
            model.CalleeId = calleeId;

            if (!calleeId.IsNullOrEmpty())
            {
                Guid? guidId = calleeId.ToGuid();
                if (guidId != null)
                {
                    var callee = calleeService.FindCallee(guidId.Value);
                    callee.CalleeCallDurations.ToList().ForEach(s =>
                    {
                        model.CallDurationList.Add(new CallDurationViewModel
                        {
                            Id = s.Id.ToString(),
                            CalleeId = calleeId,
                            CallDateTime = s.CallDateTime.ToDisplayDateFormat(),
                            CallDuration = TimeSpan.FromSeconds(s.CallDurationInSecond).ToString(),
                            Charge = s.Charge.ToString()

                        });
                    });
                    if (!durationId.IsNullOrWhiteSpace())
                    {
                        var temp = callee.CalleeCallDurations.ToList().Where(s => s.Id.ToString() == durationId).FirstOrDefault();
                        if (temp != null)
                        {
                            model.Id = temp.Id.ToString();
                            model.CalleeId = calleeId;
                            model.CallDateTime = temp.CallDateTime.ToDisplayDateFormat();
                            model.CallDuration = temp.CallDurationInSecond.ToString();
                            model.Charge = temp.Charge.ToString();
                        }
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult SaveCalleeDuration(CallDurationViewModel callDurationViewModel, [FromServices] ICalleeService calleeService, [FromServices] ICalleeCallDurationService calleeCallDurationService)
        {
            string message = String.Empty;
            Result code = Result.Failure;
            CalleeCallDuration calleeCallDuration = new CalleeCallDuration();
            if (callDurationViewModel == null)
            {
                message = "Callee data not found";
            }
            if (message.IsNullOrWhiteSpace() && callDurationViewModel.CallDateTime.IsNullOrWhiteSpace())
            {
                message = "Call DateTime required";
            }
            if (message.IsNullOrWhiteSpace() && callDurationViewModel.CallDuration.IsNullOrWhiteSpace())
            {
                message = "Call Duration required";
            }


            if (message.IsNullOrWhiteSpace())
            {
                try
                {
                    if (callDurationViewModel.Id.IsNullOrEmpty())
                    {
                        calleeCallDuration = calleeCallDurationService.CreateCalleeCallDuration(new CalleeCallDuration
                        {
                            CallDateTime = Convert.ToDateTime(callDurationViewModel.CallDateTime),
                            CallDurationInSecond = Convert.ToInt64(callDurationViewModel.CallDuration),
                            Charge = CalculateCost(Convert.ToDateTime(callDurationViewModel.CallDateTime), Convert.ToInt64(callDurationViewModel.CallDuration)),
                            Callee_Id = callDurationViewModel.CalleeId.ToGuid().Value
                        });
                        message = "Duration added successful";
                        code = Result.Success;
                    }
                    else
                    {
                        Guid? guidId = callDurationViewModel.Id.ToGuid();
                        if (guidId != null)
                        {
                            calleeCallDuration = calleeCallDurationService.FindCalleeCallDuration(guidId.Value);
                            calleeCallDuration.CallDateTime = Convert.ToDateTime(callDurationViewModel.CallDateTime);
                            calleeCallDuration.CallDurationInSecond = Convert.ToInt64(callDurationViewModel.CallDuration);
                            calleeCallDuration.Charge = CalculateCost(Convert.ToDateTime(callDurationViewModel.CallDateTime), Convert.ToInt64(callDurationViewModel.CallDuration));
                            calleeCallDuration.Callee_Id = callDurationViewModel.CalleeId.ToGuid().Value;

                            calleeCallDurationService.UpdatCalleeCallDuration(calleeCallDuration);
                            message = "Duration update successful";
                            code = Result.Success;
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
            callDurationViewModel = new CallDurationViewModel
            {
                Id = calleeCallDuration.Id.ToString(),
                CalleeId = calleeCallDuration.Callee_Id.ToString(),
                CallDateTime = calleeCallDuration.CallDateTime.ToString(),
                CallDuration = TimeSpan.FromSeconds(calleeCallDuration.CallDurationInSecond).ToString(),
                Charge = calleeCallDuration.Charge.ToString()
            };


            var response = new ResponseMessage { Message = message, Code = code.ToString(), Data = callDurationViewModel };

            return Json(response);
        }

        [HttpPost]
        public JsonResult CalleeRegistration(CallCentreRecordViewModel callee, [FromServices] ICalleeService calleeService)
        {
            string message = String.Empty;
            Result code = Result.Failure;
            if (callee == null)
            {
                message = "Callee data not found";
            }

            if (message.IsNullOrWhiteSpace() && callee.FirstName.IsNullOrWhiteSpace())
            {
                message = "First name required";
            }
            if (message.IsNullOrWhiteSpace() && callee.Lastname.IsNullOrWhiteSpace())
            {
                message = "Last name required";
            }
            if (message.IsNullOrWhiteSpace() && callee.Phone.IsNullOrWhiteSpace())
            {
                message = "Phone number required";
            }

            if (message.IsNullOrWhiteSpace())
            {
                try
                {
                    if (callee.Id.IsNullOrEmpty())
                    {
                        if (calleeService.FindCalleeByPhoneNumber(callee.Phone) == null)
                        {
                            calleeService.CreateCallee(new CallCentre.Model.Callee
                            {
                                FirstName = callee.FirstName,
                                Lastname = callee.Lastname,
                                Phone = callee.Phone,
                                Address = callee.Address
                            });
                            message = "Callee added successful";
                            code = Result.Success;
                        }
                        else
                        {
                            code = Result.Warning;
                            message = "Phone number already taken, please choose another one.";
                        }
                    }
                    else
                    {
                        Guid? guidId = callee.Id.ToGuid();
                        if (guidId != null)
                        {
                            var dbCallee = calleeService.FindCallee(guidId.Value);
                            dbCallee.FirstName = callee.FirstName;
                            dbCallee.Lastname = callee.Lastname;
                            dbCallee.Phone = callee.Phone;
                            dbCallee.Address = callee.Address;
                            calleeService.UpdateCallee(dbCallee);
                            message = "Callee update successful";
                            code = Result.Success;
                        }
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

        public IActionResult CalleeDelete(string id, [FromServices] ICalleeService calleeService, [FromServices] ICalleeCallDurationService calleeCallDurationService)
        {
            if (!id.IsNullOrEmpty())
            {
                Guid? guidId = id.ToGuid();
                if (guidId != null)
                {
                    var callee = calleeService.FindCallee(guidId.Value);
                    calleeService.DeleteCallee(callee);
                }
            }

            return RedirectToAction("CalleeList");
        }

        private decimal CalculateCost(DateTime startTime, long callDurationInSeconds)
        {

            var rules = _costService.GetAllCostingTimeSlot();
            var endTime = startTime.AddSeconds(callDurationInSeconds);
            decimal TotaCost = 0;
            CostingTimeSlot temp = null;
            for (DateTime s = startTime; s <= endTime; s = s.AddMinutes(1))
            {
                var startMilisecond = s.Hour.HourToMilisecond() + s.Minute.MinuteToMilisecond();
                temp = rules.FirstOrDefault(d => startMilisecond >= (d.StartHour.HourToMilisecond() + s.Minute.MinuteToMilisecond())
                && startMilisecond <= (d.EndHour.HourToMilisecond() + s.Minute.MinuteToMilisecond()));

                if (temp != null)
                {
                    TotaCost = TotaCost + Convert.ToDecimal(temp.CostPerMinute);
                }
            }

            return TotaCost;


        }
    }
}
