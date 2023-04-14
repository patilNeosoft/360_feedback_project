using Feedback360_Frontend.Models;
using Feedback360_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Feedback360_Frontend.Controllers
{
    public class AdminController : Controller
    {
        public IConfiguration configuration;
        private readonly ILogger<AdminController> _logger;
        Uri baseAddress;
        HttpClient client;
        public AdminController(IConfiguration configuration,ILogger<AdminController> logger)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V2BaseUrl"));
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }

        /// <summary>
        /// admin login page view
        /// </summary>
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        /// <summary>
        /// returns to admin dashboard if admin login credentials are valid.
        /// </summary>
        /// <param name="adminLogin"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginVM adminLogin)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(adminLogin);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Admin/AdminLogin", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var authData = JsonConvert.DeserializeObject<AuthResponseDto>(result);
                    HttpContext.Session.SetString("Admin", JsonConvert.SerializeObject(authData));
                    string check = HttpContext.Session.GetString("Admin");
                    return RedirectToAction("AdminDashboard");
                }
                else
                {
                    TempData["WrongCredentials"] = "Please Enter a valid Username or Password";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            string checkAdmin = HttpContext.Session.GetString("Admin");
            string checkUser = HttpContext.Session.GetString("User");
            if (checkAdmin != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("AdminLogin");
            }
            else if (checkUser != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("User/UserLogin");
            }
            else
            {
                return StatusCode(401, "Unauthorized Action");
            }
        }

        /// <summary>
        ///home page for admin with all available tiles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AdminDashboard()
        {
            string check = HttpContext.Session.GetString("Admin");
            if (check != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        /// <summary>
        /// log out method for admin
        /// </summary>
        /// <returns></returns>
        public IActionResult LogOutAdmin()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("AdminLogin");

        }

        /// <summary>
        /// returns all available user roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserRoles()
        {
            string check = HttpContext.Session.GetString("Admin");
            if (check != null)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Admin/GetAllUserRoles").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic getAllRoles = JsonConvert.DeserializeObject(data);
                ViewBag.allRoles = getAllRoles.data;
                return View("GetAllUserRoles");
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }


        public ActionResult LoadUserRoles()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Admin/GetAllUserRoles").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            dynamic getAllRoles = JsonConvert.DeserializeObject(data);
            ViewBag.allRoles = getAllRoles.data;
            return PartialView("LoadUserRoles");
        }

        /// <summary>
        /// - Sprint 1 - by Pooja Patil
        /// common get method for create and update user roles
        /// </summary>
        /// <param name="id">id is role id</param>
        /// <returns>returns to post method of edit user role if id is provided else will returns to post method of create user role</returns>
        public ActionResult InitUserRoleForm(int? id)
        {

            var model = new UserRoleVm();
            if (id.HasValue)
            {
                StringContent content = new StringContent(id.Value.ToString(), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/Admin/GetUserRoleById?roleId={id.Value}").Result;
                string roleData = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserRoleVm>(roleData);
                TempData["edit"] = "Edit";
                return PartialView("_UserRoleForm", model);

            }

            return PartialView("_UserRoleForm", model);
        }


        /// <summary>
        /// - Sprint 1 - by Pooja Patil
        /// creates new user role.checks for user role name is already exists.
        /// </summary>
        /// <param name="userRoleVm">User role object</param>
        /// <returns>return to get all user roles page with newly added user role</returns>
        [HttpPost]
        public ActionResult CreateUserRole(UserRoleVm userRoleVm)
        {
            userRoleVm.IsDeleted = false;
            string data = JsonConvert.SerializeObject(userRoleVm);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Admin/AddUserRole", content).Result;
            return Json(new { Success = response.IsSuccessStatusCode, Message = "UserRole Added Successfully!" });
        }

        /// <summary>
        /// - Sprint 1 - by Pooja Patil
        /// remote validation method to check if user role name already exists.
        /// </summary>
        /// <param name="RoleName">user role name entered by admin</param>
        /// <returns>returns error message if role name already exists.</returns>
        [HttpPost]
        public JsonResult IsRoleNameAvailable(string RoleName)
        {
            bool isUserRoleExists = false;
            if (RoleName != null)
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Admin/GetAllUserRoles").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic getAllRoles = JsonConvert.DeserializeObject(data);
                ViewBag.allRoles = getAllRoles.data;
                foreach (var item in ViewBag.allRoles)
                {
                    string role = item.roleName;
                    if (role.ToUpper() == RoleName.ToUpper())
                    {
                        isUserRoleExists = true;
                        break;
                    }
                }
            }
            return Json(!isUserRoleExists);
        }

        /// <summary>
        ///  post method to delete user role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteUserRole(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + $"/Admin/DeleteUserRole?roleId={id}").Result;
            return Json("true");
        }

        /// <summary>
        ///  - Sprint 1 - by Pooja Patil
        ///  updates data for user role
        /// </summary>
        /// <param name="userRoleVm">it is object data which to be stored</param>
        /// <returns>retrun to get all user roles page with updated user role information.</returns>
        [HttpPost]
        public JsonResult UpdateUserRole(UserRoleVm userRoleVm)
        {
            string data = JsonConvert.SerializeObject(userRoleVm);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Admin/UpdateUserRole", content).Result;
            return Json(new { Success = response.IsSuccessStatusCode, Message = "UserRole Updated Successfully!" });
        }
        public ActionResult Banners()
        {
            return RedirectToAction("Banners", "Banner");
        }



    }
}
