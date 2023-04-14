using Microsoft.AspNetCore.Mvc;
using Feedback360_Frontend.Models;
using Feedback360_Frontend.Services;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Feedback360_Frontend.Controllers
{
    public class PermissionController : Controller
    {
        public IConfiguration configuration;
        private readonly ILogger<PermissionController> _logger;
        Uri baseAddress;
        HttpClient client;
        public PermissionController(IConfiguration configuration,ILogger<PermissionController> logger)
        {
            baseAddress = new Uri(configuration.GetValue<string>("V2BaseUrl"));
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _logger = logger;
        }
       
        /// <summary>
        /// get permissions from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetPermissions(int? id)
        {
           
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Permission/GetAllPermissions").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var getAllPermissions = JsonConvert.DeserializeObject<ResponseVM>(data);
            Dictionary<string, int> permissionDict = new Dictionary<string, int>();

            foreach (var item in getAllPermissions.Data)
            {
                permissionDict.Add((string)item.permissionDescription, (int)item.permissionId);

            }


            ViewBag.PermissionList = permissionDict;
            RolePermissionVM rolePermissionVM = new RolePermissionVM();
           rolePermissionVM.RoleId = (int)id;

            return View(rolePermissionVM);

        }

        /// <summary>
        /// Set permissions for particular role
        /// </summary>
        /// <param name="rolePermissionsVM"></param>
        /// <param name="selectedPermissions"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetPermissions(RolePermissionVM rolePermissionsVM, List<int> selectedPermissions)
        {

            var adminSession = SessionHelper.GetObject<AuthResponseDto>(HttpContext.Session, "Admin");
            RolePermissionVM rolePermissionVM = new RolePermissionVM();
            rolePermissionVM.RoleId = rolePermissionsVM.RoleId;
            rolePermissionVM.PermissionId = selectedPermissions;
            string data = JsonConvert.SerializeObject(rolePermissionVM);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Permission/AddPermission", content).Result;
            return Json(new { Success = response.IsSuccessStatusCode, Message = "Permissions set Successfully!"});
            //return RedirectToAction("UserRoles", "Admin");


        }

        /// <summary>
        /// get permissions that already selected for particular roleid.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RemovePermissions(int? Id)
        {
            
            HttpResponseMessage singleResponse = client.GetAsync(client.BaseAddress + $"/Permission/GetAllPermissionsByRole?RoleId={Id}").Result;
            string newData = singleResponse.Content.ReadAsStringAsync().Result;
            var getAllPermissionsSingle = JsonConvert.DeserializeObject<ResponseVM>(newData);
            string check = JsonConvert.SerializeObject(getAllPermissionsSingle.Data);
            var getingData = JsonConvert.DeserializeObject<RemovepermisionVM>(check);
            List<int> plist = new List<int>();
            plist.AddRange(getingData.PermissionName);
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Permission/GetAllPermissions").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var getAllPermissions = JsonConvert.DeserializeObject<ResponseVM>(data);
            Dictionary<string, int> permissionDict1 = new Dictionary<string, int>();
            foreach (var item in getAllPermissions.Data)
            {
                foreach (var i in plist)
                {
                    if (i == (int)item.permissionId)
                    {
                        
                        permissionDict1.Add((string)item.permissionDescription, (int)item.permissionId);
                    }
                }

            }

            ViewBag.PermissionList1 = permissionDict1;
            RemovepermisionVM removepermisionVM = new RemovepermisionVM();
            removepermisionVM.RoleId = (int)Id;
            return View(removepermisionVM);

        }


        /// <summary>
        /// Remove permissions or permission list.
        /// </summary>
        /// <param name="removepermisionsVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemovePermissions(RemovepermisionVM removepermisionsVM)
        {
            RemovepermisionVM removepermisionVM = new RemovepermisionVM();
            removepermisionVM.RoleId = removepermisionsVM.RoleId;
            removepermisionVM.PermissionName = removepermisionsVM.PermissionName;
            string data = JsonConvert.SerializeObject(removepermisionVM);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Permission/UpdatePermissions", content).Result;
            return Json(new { Success = response.IsSuccessStatusCode, Message = "Permissions removed Successfully!"});
            //return RedirectToAction("UserRoles", "Admin");

        }



    
}
}
