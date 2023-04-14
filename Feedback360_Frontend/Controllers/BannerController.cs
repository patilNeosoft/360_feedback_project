using Feedback360_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Json;
using Newtonsoft.Json;

namespace Feedback360_Frontend.Controllers
{
    public class BannerController : Controller
    {
        public IConfiguration configuration;
        readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<BannerController> _logger;
        Uri baseAddress ;
        HttpClient client;
        public BannerController(IConfiguration configuration,ILogger<BannerController> logger, IWebHostEnvironment hostEnvironment)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V2BaseUrl"));
            this._hostEnvironment = hostEnvironment;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }


        
        /// <summary>
        /// - Sprint 2 - by Pooja Patil
        /// common get method for Edit and Create banner.It will display input fields based on id.
        /// if id exists then it will display input fields for Edit functionality and will redirect to Post method of Edit.
        /// else will perform Create banner method.
        /// </summary>
        /// <param name="id">id is bannner id which require for finding appropriate object from Db.</param>
        /// <returns>it returns Banner object if id is provided.</returns>
        public ActionResult InitBannerForm(int? id)
        {
            var banklist = client.GetAsync(client.BaseAddress + "/Banner/GetBankList").Result;
            var res = banklist.Content.ReadAsStringAsync().Result;
            var resopnseData = JsonConvert.DeserializeObject<ResponseVM>(res);
            List<SelectListItem> banks = new List<SelectListItem>();
            foreach (var i in resopnseData.Data)
            {
                banks.Add(new SelectListItem { Text = i.bankName.ToString(), Value = i.bankId.ToString() });
            }
           
            ViewBag.Banklist = banks;
            var model = new BannerVm();
            
            if (id.HasValue)
            {
                
                StringContent content = new StringContent(id.ToString(), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/Banner/GetBannerById?bannerId={id}").Result;
                string bannerData = response.Content.ReadAsStringAsync().Result;
                var banner = JsonConvert.DeserializeObject<BannerVm>(bannerData);
                banner.BannerId = id.Value;
                TempData["editBanner"] = "Edit";
                return PartialView("_BannerForm", banner);

            }
            
            return PartialView("_BannerForm", model);
        }

        /// <summary>
        /// - Sprint 2 - by Pooja Patil
        /// Create banner method create new banner and stores image url to db.image will be stored 
        /// to wwwroot/BannerImages folder.
        /// </summary>
        /// <param name="bannerVm">it is banner object which is taken from user.</param>
        /// <returns>this method returns to getallbanners page where newly added banner will get listed.</returns>
        [HttpPost]
        public ActionResult CreateBanner(BannerVm bannerVm)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string bannerImageName = Path.GetFileNameWithoutExtension(bannerVm.BannerImage.FileName);
            string extension = Path.GetExtension(bannerVm.BannerImage.FileName);
            bannerVm.BannerImageUrl = bannerImageName = bannerImageName + extension;
            string path = Path.Combine(wwwRootPath + "/BannerImages/" + bannerImageName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                bannerVm.BannerImage.CopyTo(fileStream);
            }
            string data = JsonConvert.SerializeObject(bannerVm);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + $"/Banner/AddBanner", content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["bannerAdded"] = "new banner added successfully";
            }
            return RedirectToAction("Banners");
        }

        /// <summary>
        /// - Sprint 2 - by Pooja Patil
        /// Banners method lists all available banners with Bank name,banner title and banner image.
        /// </summary>
        /// <returns>list of banners</returns>
        [HttpGet]
        public ActionResult Banners()
        {
            string check = HttpContext.Session.GetString("Admin");
            if (check != null)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Banner/GetAllBanners").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<GetAllBannerVm> getAllBanners = JsonConvert.DeserializeObject<List<GetAllBannerVm>>(data);
                if (getAllBanners.Count != 0)
                {
                    return View("GetBannersList", getAllBanners);
                }
                else
                {
                    TempData["NoBannersFound"] = "No Banners Found !";
                    return View("GetBannersList", getAllBanners);
                }
            }
            else
            {
                return RedirectToAction("AdminLogin","Admin");
            }
        }

        [HttpGet]
        public ActionResult LoadAllBanners()
        {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Banner/GetAllBanners").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                List<GetAllBannerVm> getAllBanners = JsonConvert.DeserializeObject<List<GetAllBannerVm>>(data);
                if (getAllBanners.Count != 0)
                {
                return PartialView("LoadAllBanners", getAllBanners);

                }
                else
                {
                TempData["NoBannersFound"] = "No Banners Found !";
                return PartialView("LoadAllBanners", getAllBanners);
            }

        }





            /// <summary>
            /// - Sprint 2 - by Pooja Patil
            /// delete banner method deletes banner from db.soft delete method which sets IsDeleted field as true.
            /// </summary>
            /// <param name="id">id is banner id.</param>
            /// <returns>return to getallbanners view</returns>
            public JsonResult DeleteBanner(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + $"/Banner/DeleteBanner?bannerId={id}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            BannerVm Banner = JsonConvert.DeserializeObject<BannerVm>(data);
            string wwwRootPath = _hostEnvironment.WebRootPath;  
            string fullPath = Path.Combine(wwwRootPath + "/BannerImages/" + Banner.BannerImageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                
            }
            return Json(true);
        }

        /// <summary>
        /// - Sprint 2 -by Pooja Patil
        /// updates banner information
        /// </summary>
        /// <param name="bannerVm">it is banner object which retrive from db with banner id</param>
        /// <returns>return to getallbanners view with updated banner </returns>
        [HttpPost]
        public ActionResult UpdateBanner(BannerVm bannerVm)
        {
            if (bannerVm.BannerImage != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fullPath = Path.Combine(wwwRootPath + "/BannerImages/" + bannerVm.BannerImageUrl);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);

                }

                string bannerImageName = Path.GetFileNameWithoutExtension(bannerVm.BannerImage.FileName);
                string extension = Path.GetExtension(bannerVm.BannerImage.FileName);
                bannerVm.BannerImageUrl = bannerImageName = bannerImageName + extension;
                string path = Path.Combine(wwwRootPath + "/BannerImages/" + bannerImageName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    bannerVm.BannerImage.CopyTo(fileStream);
                }
            }
            string data = JsonConvert.SerializeObject(bannerVm);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Banner/UpdateBanner", content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["bannerUpdated"] = "banner updated successfully";
            }
            return RedirectToAction("Banners");
        }

        

    }

}
