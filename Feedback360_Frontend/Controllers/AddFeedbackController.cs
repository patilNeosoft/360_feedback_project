using Feedback360_Frontend.Models;
using Feedback360_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Feedback360_Frontend.Controllers
{
    public class AddFeedbackController : Controller
    {
        public IConfiguration configuration;
        private readonly ILogger<AddFeedbackController> _logger;
        Uri baseAddress;
        HttpClient client;
        public AddFeedbackController(IConfiguration configuration, ILogger<AddFeedbackController> logger)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V1BaseUrl"));
            _logger = logger;
            client = new HttpClient();
            client.BaseAddress = baseAddress;


        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetFeedbackForm()
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {
                int bankId=  dashboardSession.BankId;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/FeedbackForm/GetQuestionsByBankId?bankid={bankId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<QuestionsVM> getAllQuestionsList = JsonConvert.DeserializeObject<List<QuestionsVM>>(data);
                List<QuestionListVM> questionListVMs = new List<QuestionListVM>();
                foreach(var item in getAllQuestionsList)
                {
                    QuestionListVM questionListVMObj = new QuestionListVM();
                    questionListVMObj.QuestionId=item.QuestionId;
                    questionListVMObj.Question = item.Question;
                    questionListVMs.Add(questionListVMObj);
                }
                return View(questionListVMs);
            }
            return RedirectToAction("UserLogin", "User");
          

        }

        [HttpPost]
        public ActionResult GetFeedbackForm(List<QuestionListVM> questionListVMs)
        {

           var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            FeedbackUserMappingVM feedbackUserMappingVM = new FeedbackUserMappingVM();
            feedbackUserMappingVM.UserId = dashboardSession.UserID;
            feedbackUserMappingVM.BankId = dashboardSession.BankId;
            feedbackUserMappingVM.FYId = 1;
            string mappingdata = JsonConvert.SerializeObject(feedbackUserMappingVM);
            StringContent contentdata = new StringContent(mappingdata, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage responseid = client.PostAsync(client.BaseAddress + "/FeedbackForm/GetFeedbackId", contentdata).Result;
            string feedbackusermapid = responseid.Content.ReadAsStringAsync().Result;
            
            foreach (var item in questionListVMs)
           {
               PostSelfFeedbackFormVM postSelfFeedbackFormVM = new PostSelfFeedbackFormVM();
                postSelfFeedbackFormVM.FeedbackId = Convert.ToInt32(feedbackusermapid);
               postSelfFeedbackFormVM.QuestionId = item.QuestionId;
                postSelfFeedbackFormVM.UserId = dashboardSession.UserID;
                postSelfFeedbackFormVM.BankId = dashboardSession.BankId;
                postSelfFeedbackFormVM.SubjectDescription = item.SubjectDescription;
                postSelfFeedbackFormVM.SelfScore = item.SelfScore;
                postSelfFeedbackFormVM.SelfComment = item.SelfComment;
                postSelfFeedbackFormVM.SelfCommentDate = DateTime.Now;
                postSelfFeedbackFormVM.FeedbackStatus = false;
                postSelfFeedbackFormVM.ApprovedStatus = 1;
                string data = JsonConvert.SerializeObject(postSelfFeedbackFormVM);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/FeedbackForm/AddFeedback", content).Result;
            }
            return RedirectToAction("UserDashboard","User");
        }

        [HttpGet]
        public ActionResult GetUserOfRepa()
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {
                int userId = dashboardSession.UserID;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/FeedbackForm/GetRepaUsers?userId={userId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<GetRepaUserListVM> getRepaUserListVMs = JsonConvert.DeserializeObject<List<GetRepaUserListVM>>(data);
                return View(getRepaUserListVMs);

            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }

        [HttpGet]
        public ActionResult ViewRepaFeedback(string id)
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");

           
            var base64Bytes = System.Convert.FromBase64String(id);
            int feedbackid = int.Parse(System.Text.Encoding.UTF8.GetString(base64Bytes)) / (-99);
            if (dashboardSession != null)
            {
              
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/FeedbackForm/GetRepaFeedbacksByFeedbackId?FeedbackId={feedbackid}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<FeedbackAnswerFormUIVM> getRepaUserFeedbackListVMs = JsonConvert.DeserializeObject<List<FeedbackAnswerFormUIVM>>(data);
                return View(getRepaUserFeedbackListVMs);

            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }

        }

        [HttpPost]
        public ActionResult ViewRepaFeedback(List<FeedbackAnswerFormUIVM> feedbackAnswerFormUIVMList)
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {


                foreach (var item in feedbackAnswerFormUIVMList)
                {
                    FeedbackAnswerFormUIVM postRepaFeedback = new FeedbackAnswerFormUIVM();
                    postRepaFeedback.FeedbackId = item.FeedbackId;
                    postRepaFeedback.QuestionId = item.QuestionId;
                    postRepaFeedback.UserId = item.UserId;
                    postRepaFeedback.BankId = item.BankId;
                    postRepaFeedback.SubjectDescription = item.SubjectDescription;
                    postRepaFeedback.SelfScore = item.SelfScore;
                    postRepaFeedback.SelfComment = item.SelfComment;
                    postRepaFeedback.SelfCommentDate = item.SelfCommentDate;
                    postRepaFeedback.RepaScore = item.RepaScore;
                    postRepaFeedback.RepaComment = item.RepaComment;
                    postRepaFeedback.RepaCommentDate = DateTime.Now;
                    postRepaFeedback.FeedbackStatus = false;
                    postRepaFeedback.ApprovedStatus = 2;
                    string data = JsonConvert.SerializeObject(postRepaFeedback);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/FeedbackForm/EditRepaFeedbackList", content).Result;
                }
                return RedirectToAction("GetUserOfRepa");
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }

        }


        [HttpGet]
        public ActionResult GetUserOfReva()
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {
                int userId = dashboardSession.UserID;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/FeedbackForm/GetRevaUsers?userId={userId}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<GetRepaUserListVM> getRevaUserListVMs = JsonConvert.DeserializeObject<List<GetRepaUserListVM>>(data);
                return View(getRevaUserListVMs);

            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }
        }

        [HttpGet]
        public ActionResult ViewRevaFeedback(string id)
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            var base64Bytes = System.Convert.FromBase64String(id);
            int feedbackid = int.Parse(System.Text.Encoding.UTF8.GetString(base64Bytes)) / (-99);
            if (dashboardSession != null)
            {

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/FeedbackForm/GetRevaFeedbacksByFeedbackId?FeedbackId={feedbackid}").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<RevaFeedbackUserFormUIVM> getRevaUserFeedbackListVMs = JsonConvert.DeserializeObject<List<RevaFeedbackUserFormUIVM>>(data);
                return View(getRevaUserFeedbackListVMs);

            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }

        }

        [HttpPost]
        public ActionResult ViewRevaFeedback(List<RevaFeedbackUserFormUIVM> revaFeedbackUserFormUIVM)
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {


                foreach (var item in revaFeedbackUserFormUIVM)
                {
                    RevaFeedbackUserFormUIVM postRevaFeedback = new RevaFeedbackUserFormUIVM();
                    postRevaFeedback.FeedbackId = item.FeedbackId;
                    postRevaFeedback.QuestionId = item.QuestionId;
                    postRevaFeedback.UserId = item.UserId;
                    postRevaFeedback.BankId = item.BankId;
                    postRevaFeedback.SubjectDescription = item.SubjectDescription;
                    postRevaFeedback.SelfScore = item.SelfScore;
                    postRevaFeedback.SelfComment = item.SelfComment;
                    postRevaFeedback.SelfCommentDate = item.SelfCommentDate;
                    postRevaFeedback.RepaScore = item.RepaScore;
                    postRevaFeedback.RepaComment = item.RepaComment;
                    postRevaFeedback.RepaCommentDate = item.RepaCommentDate;
                    postRevaFeedback.RevaScore = item.RevaScore;
                    postRevaFeedback.RevaComment = item.RevaComment;
                    postRevaFeedback.RevaCommentDate = DateTime.Now;
                    postRevaFeedback.FeedbackStatus = true;
                    postRevaFeedback.ApprovedStatus = 3;
                    string data = JsonConvert.SerializeObject(postRevaFeedback);
                    StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/FeedbackForm/EditRevaFeedbackList", content).Result;
                }
                return RedirectToAction("GetUserOfReva");
            }
            else
            {
                return RedirectToAction("UserLogin", "User");
            }

        }


    }
}
