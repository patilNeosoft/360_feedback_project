using Feedback360.Infrastructure.EncryptDecrypt;
using Feedback360_Frontend.Models;
using Feedback360_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Feedback360_Frontend.Controllers
{
    public class UserQueryController : Controller
    {
        public IConfiguration configuration;
        private readonly ILogger<BannerController> _logger;
        Uri baseAddress;
        HttpClient client;
        public UserQueryController(IConfiguration configuration,ILogger<BannerController> logger)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V1BaseUrl"));
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }

        /// <summary>
        /// - Sprint 3 - by Pooja Patil
        /// UserQueies is a function which returns all available queries for particular user
        /// </summary>
        /// <returns>queries details</returns>
        [HttpGet]
        public ActionResult UserQueries()
        {

            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {
                int UserId = dashboardSession.UserID;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/UserQuery/GetQueriesByUserId?UserId={UserId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic getAllQueries = JsonConvert.DeserializeObject(data);
                ViewBag.allQueries = getAllQueries.data;
                return View("GetUserQueries");
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }

        [HttpGet]
        public ActionResult LoadUserQueries()
        {

            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            int UserId = dashboardSession.UserID;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/UserQuery/GetQueriesByUserId?UserId={UserId}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            dynamic getAllQueries = JsonConvert.DeserializeObject(data);
            ViewBag.allQueries = getAllQueries.data;
            return PartialView("LoadUserQueries");
        }

    


    /// <summary>
    /// - Sprint 3 - by Pooja Patil
    /// form to create new user query
    /// </summary>
    /// <returns>return to userqueries page after submitting form</returns>
    [HttpGet]
    public ActionResult UserQueryForm()
    {
        var model = new UserQueryVm();
        var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
        model.UserId = dashboardSession.UserID;
        return PartialView("_UserQueryForm", model);
    }

    /// <summary>
    /// - Sprint 3 - by Pooja Patil
    /// post request form to create new query
    /// </summary>
    /// <param name="userQueryVm">query data to post</param>
    /// <returns>return to userqueries page after submitting form</returns>
    [HttpPost]
    public ActionResult UserQueryForm(UserQueryVm userQueryVm)
    {
        string data = JsonConvert.SerializeObject(userQueryVm);
        StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/UserQuery/AddUserQuery", content).Result;
        return Json(new { Success = response.IsSuccessStatusCode, Message = "User Query Added Successfully!" });
        }

        /// <summary>
        /// - Sprint 3 - by Pooja Patil
        /// shows all related data to single query
        /// </summary>
        /// <param name="id">query id</param>
        /// <returns>returns new view page with all details of single query
        /// and comment box to send comment</returns>
        [HttpGet]
    public ActionResult ViewQueryDetails(string id)
    {
        var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
        if (dashboardSession != null)
        {
            //int queryid = Int32.Parse(EncryptionDecryption.DecryptString(id));
            var base64Bytes = System.Convert.FromBase64String(id);
            int queryid = int.Parse(System.Text.Encoding.UTF8.GetString(base64Bytes)) / (-99);
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/UserQuery/GetQuery?queryId={queryid}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            dynamic getQuery = JsonConvert.DeserializeObject(data);
            ViewBag.QueryData = getQuery.data;
            HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + $"/UserQuery/GetCommentsByQueryId?queryId={queryid}").Result;
            string data1 = response1.Content.ReadAsStringAsync().Result;
            List<CommentVm> allComments = JsonConvert.DeserializeObject<List<CommentVm>>(data1);
            if (allComments.Count() == 0)
            {
                TempData["ViewQueryDetails"] = "No Comments Found !";
            }

            return View(allComments);
        }
        else
        {
            return RedirectToAction("UserLogin", "User");
        }
    }

    /// <summary>
    /// - Sprint 3 - by Pooja Patil
    /// post request form to send a comment
    /// </summary>
    /// <param name="commentText">text for enter comment</param>
    /// <param name="queryId">query id to post comment for particular query</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult SendUserComment(string commentText, int queryId)
    {
        var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
        string RoleName = dashboardSession.RoleName;
        CreateComment createComment = new CreateComment();
        createComment.CommentText = commentText;
        createComment.QueryId = queryId;
        createComment.RoleName = RoleName;
        string data = JsonConvert.SerializeObject(createComment);
        StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/UserQuery/AddUserComment", content).Result;
        if (response.IsSuccessStatusCode)
        {
            TempData["commentAdded"] = "new comment added successfully";
        }
        return RedirectToAction("UserQueries");
    }
    public JsonResult DeleteUserQuery(int queryId)
    {
        HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + $"/UserQuery/DeleteUserQuery?queryId={queryId}").Result;
        string data = response.Content.ReadAsStringAsync().Result;
        bool IsQueryDeleted = JsonConvert.DeserializeObject<bool>(data);
        if (IsQueryDeleted)
        {
            return Json(true);
        }
        return Json(false);
    }
}
}
