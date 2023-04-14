using Feedback360_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Feedback360_Frontend.Controllers
{
    public class UserAuthorityController : Controller
    {
        public IConfiguration configuration;
        private readonly ILogger<UserAuthorityController> _logger;
        Uri baseAddress;
        HttpClient client;
        public UserAuthorityController(IConfiguration configuration,ILogger<UserAuthorityController> logger)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V2BaseUrl"));
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }

        public ActionResult UserAuthority()
        {
            string checkAdmin = HttpContext.Session.GetString("Admin");
            if(checkAdmin != null)
            {
                var banklist = client.GetAsync("https://localhost:5000/api/v1/User/api/getbanklist").Result;
                var res = banklist.Content.ReadAsStringAsync().Result;
                var resopnseData = JsonConvert.DeserializeObject<ResponseVM>(res);
                Console.WriteLine(res);
                Console.WriteLine(resopnseData);
                List<SelectListItem> banks = new List<SelectListItem>();
                foreach (var i in resopnseData.Data)
                {
                    banks.Add(new SelectListItem { Text = i.bankName.ToString(), Value = i.bankId.ToString() });
                }
                Console.WriteLine(banks.ToString());
                ViewBag.Banklist = banks;
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin","Admin");
            }

        }

        public ActionResult LoadUserAuthorityList(int bankId)
        {
            var userAuthorityList = client.GetAsync(client.BaseAddress + $"/UserAuthority/GetUsersByBankId?bankId={bankId}").Result;
            HttpContext.Session.SetInt32("BankId", bankId);
            var res = userAuthorityList.Content.ReadAsStringAsync().Result;
            List<GetUsersByBankIdVM> userAuthList = JsonConvert.DeserializeObject<List<GetUsersByBankIdVM>>(res);
            return PartialView("_LoadUserAuthorityList", userAuthList);
        }

        [HttpGet]
        public ActionResult SetUserAuthority(int id)
        {
            int bankId = (int)HttpContext.Session.GetInt32("BankId");
            //UserAuthorityList
            var userAuthorityList = client.GetAsync(client.BaseAddress + $"/UserAuthority/GetUsersByBankId?bankId={bankId}").Result;
            var res = userAuthorityList.Content.ReadAsStringAsync().Result;
            List<GetUsersByBankIdVM> userAuthList = JsonConvert.DeserializeObject<List<GetUsersByBankIdVM>>(res);
            GetUsersByBankIdVM user = userAuthList.Where(u => u.UserId == id).FirstOrDefault();

            //Reportee List
            var reportingAuthorityList = client.GetAsync(client.BaseAddress + $"/UserAuthority/GetReporteeByBankId?bankId={bankId}").Result;
            var result = reportingAuthorityList.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<List<GetReporteeByBankIdVM>>(result);
            List<SelectListItem> reporteeAuthorities = new List<SelectListItem>();
            bool selectedItem = false;
            foreach(var item in responseData)
            {                
                
                    if(item.Id == user.UserId)
                    {
                        selectedItem = true;
                    }

                    reporteeAuthorities.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.Id.ToString(), Selected = selectedItem });
                
            }
            ViewBag.ReportingAuthorityList = reporteeAuthorities;

            //Reveiewing Authority List
            
            var reviewingAuthorityList = client.GetAsync(client.BaseAddress + $"/UserAuthority/GetReviewingAuthorityByBankId?bankId={bankId}").Result;
            var result2 = reviewingAuthorityList.Content.ReadAsStringAsync().Result;
            var responseData2 = JsonConvert.DeserializeObject<List<GetReiewingAuthorityByBankIdVM>>(result2);
            List<SelectListItem> reviewingAuthorities = new List<SelectListItem>();
            bool selectedItem2 = false;
            foreach(var item in responseData2)
            {
                if(item.Id == user.UserId)
                {
                    selectedItem2 = true;
                }

                reviewingAuthorities.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName, Value = item.Id.ToString(), Selected = selectedItem2 });

            }
            ViewBag.ReviewingAuthorityList = reviewingAuthorities;
            return PartialView("_SetUserAuthority", user);
        }
        [HttpPost]
        public ActionResult SetUserAuthority(GetUsersByBankIdVM getUsersByBankIdVM)
        {
            SetUserAuthorityDTO userAuthorityDTO = new SetUserAuthorityDTO();
            userAuthorityDTO.UserId = getUsersByBankIdVM.UserId;
            if(getUsersByBankIdVM.ReportingAuthority == null)
            {
                userAuthorityDTO.ReportingAuthority = 0;
            }
            else
            {
                userAuthorityDTO.ReportingAuthority = getUsersByBankIdVM.ReportingAuthority;
            }
            if(getUsersByBankIdVM.ReviewingAuthority == null)
            {
                userAuthorityDTO.ReviewingAuthority = 0;
            }
            else
            {
                userAuthorityDTO.ReviewingAuthority = getUsersByBankIdVM.ReviewingAuthority;
            }
            string data = JsonConvert.SerializeObject(userAuthorityDTO);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/UserAuthority/AddUserAuthority", content).Result;
            if(response.IsSuccessStatusCode)
            {
                TempData["AuthorityAdded"] = "Authority Added successfully";
            }
            return Json(new { Success = response.IsSuccessStatusCode, Message = "Authority Set Successfully!" });

        }

    }
}
