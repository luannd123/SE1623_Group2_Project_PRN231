using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client = null;
        private string url = "";
        public UserController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Authen/Login");
            url = "https://localhost:7200/api/User/GetUser";
            HttpResponseMessage response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<User>? users = JsonSerializer.Deserialize<List<User>>(json, options);
            return View(users);
        }
        //https://localhost:7200/api/User/GetUser

        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User UserUpdateMemberDTO)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Authen/Login");
            url = "https://localhost:7200/api/User/AddNewUser";
            JsonContent content = JsonContent.Create(UserUpdateMemberDTO);
            HttpResponseMessage response = await client.PostAsync(url, content);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Update(int id)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7020/api/User/UpdateUser";
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            User? createUpdateMemberDTO = JsonSerializer.Deserialize<User>(json, options);
            return View(createUpdateMemberDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAsync(User createUpdateMemberDTO)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7020/api/User/UpdateUser";
            JsonContent content = JsonContent.Create(createUpdateMemberDTO);
            HttpResponseMessage response = await client.PutAsync(url + "/" + createUpdateMemberDTO.UserId, content);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Authen/Login");
            url = "https://localhost:7200/api/User/DeleteUser";
            HttpResponseMessage response = await client.DeleteAsync(url + "/" + id);
            return RedirectToAction(nameof(Index));
        }
    }
}
