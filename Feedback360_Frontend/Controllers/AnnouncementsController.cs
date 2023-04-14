using Feedback360.Application.Features.Banners.Queries.GetAllBanners;
using Feedback360_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Feedback360_Frontend.Controllers
{
    public class AnnouncementsController : Controller
    {
        public IConfiguration configuration;
        private readonly ILogger<AnnouncementsController> _logger;
        Uri baseAddress ;
        HttpClient client;

        public AnnouncementsController(IConfiguration configuration,ILogger<AnnouncementsController> logger)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V2BaseUrl"));
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// - Sprint 2 - by Ramya Guduru
        /// Announcements Action method lists all available announcements with Announcement, Bank name, Status.
        /// </summary>
        /// <returns>list of Announcements</returns>
        [HttpGet]
        public ActionResult GetAllAnnouncements()
        {
            string check = HttpContext.Session.GetString("Admin");
            if (check != null)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Announcements/AllAnnouncements").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<GetAllAnnouncements> getAllAnnouncements = JsonConvert.DeserializeObject<List<GetAllAnnouncements>>(data);
                if (getAllAnnouncements.Count != 0)
                {
                    return View("GetAllAnnouncements", getAllAnnouncements);
                }
                else
                {
                    TempData["NoAnnouncementsFound"] = "No Announcements Are Found !";
                    return View("GetAllAnnouncements", getAllAnnouncements);
                }
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

        }

        [HttpGet]
        public ActionResult LoadAllAnnouncements()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Announcements/AllAnnouncements").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<GetAllAnnouncements> getAllAnnouncements = JsonConvert.DeserializeObject<List<GetAllAnnouncements>>(data);
            if (getAllAnnouncements.Count != 0)
            {
                return PartialView("LoadAllAnnouncements", getAllAnnouncements);
            }
            else
            {
                TempData["NoAnnouncementsFound"] = "No Announcements Are Found !";
                return PartialView("LoadAllAnnouncements", getAllAnnouncements);
            }
            
        }

        /// <summary>
        /// - Sprint 2 - by Ramya Guduru
        /// Create announcement method create new announcement and stores in db
        /// </summary>
        /// <param name="AnnouncementsVm">it is announcement object which is taken from user.</param>
        /// <returns>this method returns to getallannouncement page where newly added announcement will get listed.</returns>

        [HttpPost]
        public ActionResult CreateAnnouncement(AnnouncementsVm announcement)
        {
            string data = JsonConvert.SerializeObject(announcement);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Announcements/CreateAnnouncement", content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["announcementAdded"] = "new announcement added successfully";
            }
            return Json(new { Success = response.IsSuccessStatusCode, Message = "Announcement added Successfully!" });

        }

        /// <summary>
        /// - Sprint 2 - by Ramya Guduru
        /// delete Announcement method deletes an announcement from db.
        /// </summary>
        /// <param name="id">id is announcement id.</param>
        /// <returns>return to GetAllAnnouncements view</returns>
        [HttpPost]
        public JsonResult DeleteAnnouncement(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + $"/Announcements/DeleteAnnouncement?announcementId={id}").Result;
            return Json(true);
        }

        /// <summary>
        /// - Sprint 2 - by Ramya Guduru
        /// updates announcement information
        /// </summary>
        /// <param name="AnnouncementsVm">it is announcement object which retrive from db with announcement id</param>
        /// <returns>return to getallannouncement view with updated announcement </returns>

        [HttpPost]
        public JsonResult UpdateAnnouncement(AnnouncementsVm announcement)
        {
            
                string data = JsonConvert.SerializeObject(announcement);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Announcements/updateAnnouncement", content).Result;
                return Json(new { Success = response.IsSuccessStatusCode, Message = "Announcement Updated Successfully!" });

        }


        /// <summary>
        /// - Sprint 2 - by Ramya Guduru
        /// common get method for Edit and Create announcement.It will display input fields based on id.
        /// if id exists then it will display input fields for Edit functionality and will redirect to Post method of Edit.
        /// else will perform Create  announcement.
        /// </summary>
        /// <param name="id">id is announcement id which require for finding appropriate object from Db.</param>
        /// <returns>it returns announcement object if id is provided.</returns>
        public ActionResult InitAnnouncementForm(int? id)
        {
            var banklist = client.GetAsync(client.BaseAddress + $"/Banner/GetBankList").Result;
            var res = banklist.Content.ReadAsStringAsync().Result;
            var resopnseData = JsonConvert.DeserializeObject<ResponseVM>(res);
            List<SelectListItem> banks = new List<SelectListItem>();
            foreach (var i in resopnseData.Data)
            {
                banks.Add(new SelectListItem { Text = i.bankName.ToString(), Value = i.bankId.ToString() });
            }

            ViewBag.Banklist = banks;
            var model = new AnnouncementsVm();

            if (id.HasValue)
            {

                StringContent content = new StringContent(id.Value.ToString(), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/Announcements/GetAnnouncementById?announcementId={id.Value}").Result;
                string announcementData = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<AnnouncementsVm>(announcementData);
                model.AnnouncementId = id.Value;
                TempData["edit"] = "Edit";
                return PartialView("_AnnouncementForm", model);

            }

            return PartialView("_AnnouncementForm", model);
        }

    }
}
