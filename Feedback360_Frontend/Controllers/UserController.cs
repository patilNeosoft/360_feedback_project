using Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard;
using Feedback360.Application.Responses;
using Feedback360_Frontend.IServices;
using Feedback360_Frontend.Models;
using Feedback360_Frontend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace Feedback360_Frontend.Controllers
{
    public class UserController : Controller
    {
        public IConfiguration configuration;
        private readonly Captcha _captcha;
        Uri baseAddress ;
        HttpClient client;
        IOtpService _otpService;
        ISendMailService _mailService;
        public UserController(IConfiguration configuration,Captcha captcha, IOtpService otpService, ISendMailService mailService)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V1BaseUrl"));
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _captcha = captcha;
            _otpService = otpService;
            _mailService = mailService;
        }

        /// <summary>
        /// This method returns captcha.
        /// </summary>
        /// <returns></returns>
        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }
        /// <summary>
        /// This is getview of userlogin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UserLogin()
        {
            try
            {
                var banklist = client.GetAsync(client.BaseAddress + "/User/api/getbanklist").Result;
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
            catch(AggregateException aggregateException)
            {
                Console.WriteLine(aggregateException);
                return RedirectToAction("InternalServerError", "Error");
            }
            
        }

        /// <summary>
        /// Post view of user login
        /// </summary>
        /// <param name="userLoginModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                AuthResponseDto authResponseDto = new AuthResponseDto();
                string data = JsonConvert.SerializeObject(userLoginModel);
                if (!Captcha.ValidateCaptchaCode(userLoginModel.CaptchaCode, HttpContext))
                {
                    TempData["InvalidCaptcha"] = "Please Enter Valid Captcha";
                    return RedirectToAction("UserLogin");
                }
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var response = client.PostAsync(client.BaseAddress + "/User/api/userlogin", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var authdata = JsonConvert.DeserializeObject<AuthResponseDto>(result);
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(authdata));
                    var otpCode = _otpService.GenerateOtp();
                    HttpContext.Session.SetInt32("OtpCode", otpCode);
                    //otp timer
                    DateTime removeOtpSession = DateTime.Now;
                    removeOtpSession = removeOtpSession.AddMinutes(5);
                    String otpExpiry = removeOtpSession.ToString();
                    Console.WriteLine("Session OTP time in resend OTP", removeOtpSession);
                    HttpContext.Session.SetString("OtpExpiry", otpExpiry);
                    //end of otp timer
                    var userSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
                    EmailEntity emailEntity = new EmailEntity()
                    {
                        Email = userSession.Email,
                        Subject = "OTP",
                        Body = $"OTP is only valid for next 5 minutes. <br> Your OTP for Feedback 360 is <b>{otpCode}</b>."
                    };
                    _mailService.SendMail(emailEntity);
                    return RedirectToAction("OtpView");
                }
            }
            TempData["ErrorMessage"] = "Invalid Creaditionals.Please Enter Valid Creaditionals.";
            return RedirectToAction("UserLogin");
        }
        /// <summary>
        /// Resend mail if User hasn't recieved OTP
        /// </summary>
        /// <returns>It will send a new OTP to the email</returns>
        [HttpPost]
        public JsonResult ReSendOtp()
        {
            var otpCode = _otpService.GenerateOtp();
            HttpContext.Session.SetInt32("OtpCode", otpCode);
            DateTime removeOtpSession = DateTime.Now;
            removeOtpSession = removeOtpSession.AddMinutes(5);
            String otpExpiry = removeOtpSession.ToString();
            Console.WriteLine("Session OTP time in resend OTP", removeOtpSession);
            HttpContext.Session.SetString("OtpExpiry", otpExpiry);
            var userSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            EmailEntity emailEntity = new EmailEntity()
            {
                Email = userSession.Email,
                Subject = "OTP",
                Body = $"Your OTP for Feedback 360 is <b>{otpCode}</b>"
            };
            _mailService.SendMail(emailEntity);
            return Json(true);
        }

        /// <summary>
        /// log out method for user
        /// </summary>
        /// <returns>returns to user login page</returns>
        public IActionResult LogoutUser()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("UserLogin");
        }
        /// <summary>
        /// This method return user dashboard 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UserDashboard()
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            if (dashboardSession != null)
            {
                int? bankId = dashboardSession.BankId;
                HttpResponseMessage response = client.GetAsync($"https://localhost:5000/api/v2/Banner/GetBannersByBankId?bankId={bankId}").Result;
                HttpResponseMessage res = client.GetAsync($"https://localhost:5000/api/v2/Announcements/GetDashboardAnnouncementById?bankId={bankId}").Result;
               
                string jsonString = res.Content.ReadAsStringAsync().Result;
                dynamic announcementResult = JsonConvert.DeserializeObject(jsonString);
                if (announcementResult != null){

                    ViewBag.Message = announcementResult.message;
                }
                string data = response.Content.ReadAsStringAsync().Result;
                List<CarouselVM> getAllPermissionsList = JsonConvert.DeserializeObject<List<CarouselVM>>(data);
                return View(getAllPermissionsList);
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        /// <summary>
        /// This is a view for OTP
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OtpView()
        {
            return View();
        }

        /// <summary>
        /// post method to check otp entered by user
        /// </summary>
        /// <param name="otpVM"></param>
        /// <returns>returns to userdashboard if otp is correct else returns to same view</returns>
        [HttpPost]
        public IActionResult OtpView(OtpVM otpVM)
        {
            if(ModelState.IsValid)
            {
                int? sessionOtpCode = HttpContext.Session.GetInt32("OtpCode");
                String otpSession = HttpContext.Session.GetString("OtpExpiry");
                DateTime otpExpiry = Convert.ToDateTime(otpSession);
                DateTime currentTime = DateTime.Now;
                Console.WriteLine(currentTime);
                int userOtpCode = otpVM.OtpCode;
                if (sessionOtpCode == userOtpCode && otpExpiry > currentTime)
                {
                    var userSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
                    //userSession.BankId = authdata.BankId;
                    TempData["Username"] = userSession.UserName;
                    TempData["UserRole"] = userSession.RoleName;
                    HttpContext.Session.Remove("OtpCode");
                    return RedirectToAction("UserDashboard");
                }
                else
                {
                    TempData["WrongOtp"] = "Please enter the correct OTP";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="forgotPasswordVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(forgotPasswordVM);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                var response = client.PostAsync(client.BaseAddress + "/User/ForgotPassword", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //HttpContext.Session.SetString("Email", forgotPasswordVM.Email);
                    string result = response.Content.ReadAsStringAsync().Result;
                    TempData["PasswordSent"] = "Password has been sent Successfully to your Email Id";
                   
                }
                else
                {
                    TempData["EmailExistError"] = "The Email does not Exist in our Database";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordVM resetPasswordVM)
        {
           
            if (ModelState.IsValid)
            {
                ResetPasswordDTO resetPasswordDTO = new ResetPasswordDTO();
                resetPasswordDTO.Email = resetPasswordVM.Email;
                resetPasswordDTO.NewPassword = resetPasswordVM.NewPassword;
                resetPasswordDTO.ConfirmPassword = resetPasswordVM.ConfirmPassword;
                string data = JsonConvert.SerializeObject(resetPasswordDTO);
                StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = client.PostAsync(client.BaseAddress + "/User/ResetPassword", stringContent).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["PasswordResetSuccessfully"] = "Your Password has been changed Successfuly";
                    return RedirectToAction("UserLogin");
                }
                else
                {
                    TempData["PasswordResetError"] = "Value Error";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                var userData = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
                ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO();
                changePasswordDTO.UserId = userData.UserID;
                changePasswordDTO.OldPassword = changePasswordVM.OldPassword;
                changePasswordDTO.NewPassword = changePasswordVM.NewPassword;
                changePasswordDTO.ConfirmPassword = changePasswordVM.ConfirmPassword;
                string data = JsonConvert.SerializeObject(changePasswordDTO);
                StringContent stringContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = client.PostAsync(client.BaseAddress + "/User/ChangePassword", stringContent).Result;
                if(httpResponse.IsSuccessStatusCode)
                {
                    TempData["PasswordChangedSuccessfully"] = "Your Password has been changed Successfuly";
                    return View();
                }
                else
                {
                    TempData["OldpasswordNotMatchesError"] = "Please Enter the correct password in old Password Field";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public JsonResult IsOldAndNewPasswordSame(string NewPassword,string OldPassword)
        {
            bool result = false;
            if(NewPassword == OldPassword)
            {
                result = true;
               
            }
            return Json(!result);
        }
    
        public ActionResult _360Feedback()
        {
            var dashboardSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "User");
            
            if (dashboardSession != null)
            {
                string UserRole = dashboardSession.RoleName;
                 
                if(UserRole == "Employee")
                {
                    ViewBag.UserRole = "Employee";
                }
                else if (UserRole == "Reportee")
                {
                    ViewBag.UserRole = "Reportee";
                }
                else if(UserRole == "Reviewing Authority")
                {
                    ViewBag.UserRole = "Reviewing Authority";
                }
                else if(UserRole == "Administrator")
                {
                    ViewBag.UserRole = "Administrator";
                }
                    return View();
            }
            return RedirectToAction("UserLogin");

        }
    }
}
