using Feedback360.Domain.Entities;
using Feedback360_Frontend.Models;
using Feedback360_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Feedback360_Frontend.Controllers
{
    public class AdminQueryController : Controller
    {
        public IConfiguration configuration;
        private readonly ILogger<AdminQueryController> _logger;
        Uri baseAddress;
        HttpClient client;
        public AdminQueryController(IConfiguration configuration,ILogger<AdminQueryController> logger)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V2BaseUrl"));
            _logger = logger;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method returns all queries of users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAllQueriesAdmin()
        {
            string check = HttpContext.Session.GetString("Admin");
            if (check != null)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Query/GetAllAdminQueries").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic getAllQueryAdminList = JsonConvert.DeserializeObject(data);
                ViewBag.allAdminQueries = getAllQueryAdminList.data;
                if (ViewBag.allAdminQueries.Count != 0)
                {
                    return View();
                }
                else
                {
                    TempData["NoQuerisFound"] = "No Queries Found !";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
        }

        
        [HttpGet]
        public ActionResult AdminComment(int queryid)
        {
            AdminCommentVM adminCommentVM = new AdminCommentVM();
            adminCommentVM.QueryId = queryid;
            return PartialView("AdminComment", adminCommentVM);
        }

        [HttpPost]
        public ActionResult AdminComment(AdminCommentVM adminCommentVM)
        {
            string data = JsonConvert.SerializeObject(adminCommentVM);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Query/ResolveQuery", content).Result;
            return RedirectToAction("GetAllQueriesAdmin");
        }

       /// <summary>
       /// Delete particular query
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public JsonResult DeleteQuery(int id)
        {
            if (id != null)
            {   List<int> query = new List<int>();
                query.Add(id);
                string data = JsonConvert.SerializeObject(query);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + $"/Query/DeleteQuery",content).Result;
            }
            return Json(true);
        }

        public ActionResult Load()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Query/GetAllAdminQueries").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            dynamic getAllQueryAdminList = JsonConvert.DeserializeObject(data);
            ViewBag.allAdminQueries = getAllQueryAdminList.data;
            return PartialView("_AdminQueryGrid", ViewBag.allAdminQueries);
        }
        /// <summary>
        /// Delete All Queries
        /// </summary>
        /// <param name="bulkDeleteVM"></param>
        /// <returns></returns>
        public JsonResult DeleteAllQuery(BulkDeleteVM bulkDeleteVM)
        {
            string data= JsonConvert.SerializeObject(bulkDeleteVM.idList);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + $"/Query/DeleteQuery", content).Result;
            return Json(true);
        }

        /// <summary>
        /// Get Comment and query details of particular query.
        /// </summary>
        /// <param name="queryid"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult GetQueryAndCommentDetails(int queryid)
        {
            string check = HttpContext.Session.GetString("Admin");
            if (check != null)
            {

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/Query/GetQueryById?queryId={queryid}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic getQuery = JsonConvert.DeserializeObject(data);
                ViewBag.QueryData = getQuery.data;

                HttpResponseMessage responseComment = client.GetAsync(client.BaseAddress + $"/Query/GetAllCommentsById?queryid={queryid}").Result;
                string dataComment = responseComment.Content.ReadAsStringAsync().Result;
                List<AdminCommentVM> adminCommentVM = JsonConvert.DeserializeObject<List<AdminCommentVM>>(dataComment);
                if (adminCommentVM.Count() == 0)
                {
                    TempData["ViewQueryDetails"] = "No Comments Found !";
                }
                return View(adminCommentVM);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
        }

        /// <summary>
        /// Add comment on query and also set status of query resolve or not.
        /// </summary>
        /// <param name="commentText"></param>
        /// <param name="queryId"></param>
        /// <param name="querystatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendAdminComment(string commentText, int queryId,string querystatus)
        {
            if (querystatus == "True") {
                SetStatusVM setStatusVM = new SetStatusVM();
                setStatusVM.QueryId = queryId;
                string idData = JsonConvert.SerializeObject(setStatusVM);
                StringContent contentData = new StringContent(idData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseComment = client.PostAsync(client.BaseAddress + "/Query/SetStatus", contentData).Result;
            }
            var adminDashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "Admin");
            string RoleName = adminDashboardSession.RoleName;
            AdminCommentAnotherVM createAdminComment = new AdminCommentAnotherVM();
            createAdminComment.QueryId = queryId;
            createAdminComment.CommentDescription = commentText;
            createAdminComment.RoleName = RoleName;
            string data = JsonConvert.SerializeObject(createAdminComment);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress+"/Query/ResolveQuery",content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["commentAdded"] = "new comment added successfully";
            }
            return RedirectToAction("GetAllQueriesAdmin");
        }

    }
}
