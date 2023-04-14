using Feedback360_Frontend.Models;
using Feedback360_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Feedback360_Frontend.Controllers
{
    public class DepartmentTeamController : Controller
    {
        public IConfiguration configuration;
        readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<DepartmentTeamController> _logger;
        Uri baseAddress;
        HttpClient client;
        public DepartmentTeamController(IConfiguration configuration,ILogger<DepartmentTeamController> logger, IWebHostEnvironment hostEnvironment)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V1BaseUrl"));
            this._hostEnvironment = hostEnvironment;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult DepartmentTeamHomePage()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetFinancialYear").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            FinancialYearVm financialYear = JsonConvert.DeserializeObject<FinancialYearVm>(data);
            return View(financialYear);
        }
        //get all employee list
        [HttpGet]
        public ActionResult GetAllAvailableMembersList()
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            List<GetEmployeeListByDepAndBankId> getEmployeeListByDepAndBankId = new List<GetEmployeeListByDepAndBankId>();
            if (dashboardSession != null)
            {
                int bankId = dashboardSession.BankId;
                int userId = dashboardSession.UserID;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetAllAvailableUsers?bankId={bankId}&userId={userId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                getEmployeeListByDepAndBankId = JsonConvert.DeserializeObject<List<GetEmployeeListByDepAndBankId>>(data);
                var userFound = getEmployeeListByDepAndBankId.Find(u=>u.UserId == userId);
                //TO CHECK if he is team leader
                HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetTeamMembers?userId={userId}").Result;
                string data1 = response1.Content.ReadAsStringAsync().Result;
                List<GetMyTeamVm> getMyTeamVmList = JsonConvert.DeserializeObject<List<GetMyTeamVm>>(data1);
                HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetTLDetails?teamLeadId={userId}").Result;
                string data2 = response2.Content.ReadAsStringAsync().Result;
                GetTeamLeaderDetailsVm getTeamLeaderDetailsVm = JsonConvert.DeserializeObject<GetTeamLeaderDetailsVm>(data2);
                if (getTeamLeaderDetailsVm.Id == userId || userFound != null)
                {
                    ViewBag.TeamLeadId = userId;
                    ViewBag.isTeamLead = "team lead!";
                }
            }
            return PartialView("GetAllAvailableMembersList", getEmployeeListByDepAndBankId);

        }

        //add member
        [HttpGet]
        public ActionResult InitAddMemberForm(int UserId, string UserName)
        {

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/DepartmentTeam/GetAllSecondaryRoles").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<SecondaryRoleListVm> secondaryRoleListVms = new List<SecondaryRoleListVm>();
            secondaryRoleListVms = JsonConvert.DeserializeObject<List<SecondaryRoleListVm>>(data);
            ViewBag.secondaryRoleListVms = secondaryRoleListVms;
            var model = new DepartmentTeamVm();
            ViewBag.UserName = UserName.Replace('-', ' ');
            model.UserId = UserId;
            return PartialView("InitAddMemberForm", model);
        }
        [HttpPost]
        public ActionResult AddMemberToGroup(DepartmentTeamVm departmentTeamVm)
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            departmentTeamVm.TeamLeadId = dashboardSession.UserID;
            departmentTeamVm.BankId = dashboardSession.BankId;
            departmentTeamVm.DeptId = 0;
            string data = JsonConvert.SerializeObject(departmentTeamVm);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/DepartmentTeam/AddMemberToGroup", content).Result;
            return Json(new { Success = response.IsSuccessStatusCode, Message = "New Member Added Successfully!" });
        }

        //get group members
        public ActionResult MyTeamHomePage()
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            GetTeamLeaderDetailsVm getTeamLeaderDetailsVm = new GetTeamLeaderDetailsVm();
            if (dashboardSession != null)
            {
                int userId = dashboardSession.UserID;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetTLDetails?teamLeadId={userId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                getTeamLeaderDetailsVm = JsonConvert.DeserializeObject<GetTeamLeaderDetailsVm>(data);
                ViewBag.getTeamLeaderDetailsVm = getTeamLeaderDetailsVm;
                //GET FINANCIAL YEAR
                HttpResponseMessage fResponse = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetFinancialYear").Result;
                string fData = fResponse.Content.ReadAsStringAsync().Result;
                FinancialYearVm financialYear = JsonConvert.DeserializeObject<FinancialYearVm>(fData);
                ViewBag.financialYear = financialYear;
                if(getTeamLeaderDetailsVm.Id == userId)
                {
                    ViewBag.TeamLeadId = userId;
                }
            }

            return View(getTeamLeaderDetailsVm);
        }
        public ActionResult loadMyTeam()
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            List<GetMyTeamVm> getMyTeamVmList = new List<GetMyTeamVm>();
            HttpResponseMessage fresponse = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetFinancialYear").Result;
            string fdata = fresponse.Content.ReadAsStringAsync().Result;
            FinancialYearVm financialYear = JsonConvert.DeserializeObject<FinancialYearVm>(fdata);
            ViewBag.financialYearVm = financialYear;
            if (dashboardSession != null)
            {
                int userId = dashboardSession.UserID;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetTeamMembers?userId={userId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                getMyTeamVmList = JsonConvert.DeserializeObject<List<GetMyTeamVm>>(data);
                var selfDetails = getMyTeamVmList.Find(u => u.UserId == dashboardSession.UserID);
                if (getMyTeamVmList.Count != 0)
                {
                    HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + $"/DepartmentTeam/GetTLDetails?teamLeadId={userId}").Result;
                    string data1 = response1.Content.ReadAsStringAsync().Result;
                    GetTeamLeaderDetailsVm getTeamLeaderDetailsVm = JsonConvert.DeserializeObject<GetTeamLeaderDetailsVm>(data1);
                    //if (getTeamLeaderDetailsVm.Id != userId)
                    //{
                    //    DateTime joiningDate = (DateTime)selfDetails.TeamJoiningDate;
                    //    ViewBag.joiningdate = joiningDate;
                    //    ViewBag.isTeamLead = "not a team lead!";
                    //}
                    //DateTime joiningDateOfTl = getTeamLeaderDetailsVm.TeamJoiningDate;
                    //ViewBag.joiningdate = joiningDateOfTl;
                    DateTime joiningDate = (DateTime)selfDetails.TeamJoiningDate;
                     ViewBag.joiningdate = joiningDate;
                    if (getTeamLeaderDetailsVm.Id == userId)
                    {
                        ViewBag.TeamLeadId = userId;
                        ViewBag.isTeamLead = "team lead!";
                    }
                }
                ViewBag.NoTeamAssigned = "no team assigned";
            }
            return PartialView("loadMyTeam", getMyTeamVmList);
        }
        //remove member

        public JsonResult DeleteGroupMember(int DepTeamId)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + $"/DepartmentTeam/RemoveTeamMember?depTeamId={DepTeamId}").Result;
            if (response.IsSuccessStatusCode)
            {
                return Json("true");

            }
            return Json("false");
        }

        //drop team
        public JsonResult DropGroup(int leaderId)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + $"/DepartmentTeam/DropGroup?teamLeadId={leaderId}").Result;
            if (response.IsSuccessStatusCode)
            {
                return Json("true");

            }
            return Json("false");
        }
    }
}
