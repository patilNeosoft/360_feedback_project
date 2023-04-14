using Feedback360_Frontend.Models;
using Feedback360_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Feedback360_Frontend.Controllers
{
    public class SelfFeedbackController : Controller
    {
        public IConfiguration configuration;
        private readonly ILogger<SelfFeedbackController> _logger;
        Uri baseAddress;
        HttpClient client;
        public SelfFeedbackController(IConfiguration configuration,ILogger<SelfFeedbackController> logger)
        {

            baseAddress = new Uri(configuration.GetValue<string>("V1BaseUrl"));
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult SelfFeedbackHomePage()
        {

            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {
                int UserId = dashboardSession.UserID;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/SelfFeedback/GetUserAuthorityData?UserId={UserId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                UserAuthorityMappingVM getAuthorityData = JsonConvert.DeserializeObject<UserAuthorityMappingVM>(data);

                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + $"/SelfFeedback/GetUserFeedbackDetails?UserId={UserId}").Result;
                string data1 = response1.Content.ReadAsStringAsync().Result;
                List<GetFeedBackByUserIdVM> getSelfFeedbackData = JsonConvert.DeserializeObject<List<GetFeedBackByUserIdVM>>(data1);
                foreach (var item in getSelfFeedbackData)
                {
                    ViewBag.rolename = item.RoleName;
                    ViewBag.username = item.UserName;
                    break;
                }
                ViewBag.SelfFeedbackData = getSelfFeedbackData;
                return View(getAuthorityData);
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }

        public ActionResult GetFeedbackByFinancialYear(int startYear, int endYear)
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {
                int UserId = dashboardSession.UserID;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/SelfFeedback/GetUserFeedbackDetailsByFinancialYear?startYear={startYear}&endYear={endYear}&UserId={UserId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<GetFeedBackByUserIdVM> getSelfFeedbackData = JsonConvert.DeserializeObject<List<GetFeedBackByUserIdVM>>(data);
                foreach (var item in getSelfFeedbackData)
                {
                    ViewBag.rolename = item.RoleName;
                    ViewBag.username = item.UserName;
                    break;
                }
                return View(getSelfFeedbackData);
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }
        public ActionResult GetFeedbackSummary(int? financialYear)
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {
                int UserId = dashboardSession.UserID;
                if (financialYear == null)
                {
                    financialYear = 0;
                }
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/SelfFeedback/GetSelfFeedbackSummary?UserId={UserId}&financialYear={financialYear}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<SelfFeedbackSummaryVM> getSelfFeedbackSummary = JsonConvert.DeserializeObject<List<SelfFeedbackSummaryVM>>(data);
                
                ViewBag.FinancialYearList = getSelfFeedbackSummary;
                
                return View(getSelfFeedbackSummary);
            }
            else
            {
                return RedirectToAction("UserLogin", "User");

            }
        }

        public ActionResult LoadFeedbackSummary(int? financialYear)
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            int UserId = dashboardSession.UserID;
            if (financialYear == null)
            {
                financialYear = 0;
            }
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/SelfFeedback/GetSelfFeedbackSummary?UserId={UserId}&financialYear={financialYear}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<SelfFeedbackSummaryVM> getSelfFeedbackSummary = JsonConvert.DeserializeObject<List<SelfFeedbackSummaryVM>>(data);

            ViewBag.FinancialYearList = getSelfFeedbackSummary;

            return PartialView("_GetFeedbackSummary", getSelfFeedbackSummary);


        }
        }
    }
