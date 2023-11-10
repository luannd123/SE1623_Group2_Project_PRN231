using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient client;
        private readonly IConfiguration configuration;
        private string url = "";

        public AuthController(IConfiguration configuration)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            this.configuration = configuration;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync(User createUser)
        {
            url = "https://localhost:7200/api/User";
            JsonContent content = JsonContent.Create(createUser);
            HttpResponseMessage response = await client.PostAsync(url, content);
            return Redirect("~/Auth/Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(User m)
        {
            var EmailSetting = configuration.GetValue<string>("MailCredentialsSettings:UserName");
            var PasswordSetting = configuration.GetValue<string>("MailCredentialsSettings:Password");

            if (m.Email == EmailSetting && m.Password == PasswordSetting)
            {
                m.Email = EmailSetting;
                HttpContext.Session.SetString("admin", JsonSerializer.Serialize(m));
                return Redirect("~/Product/Index");
            }
            url = "https://localhost:7200/api/User/email,password?";
            JsonContent content = JsonContent.Create(m);
            var _url = url + "email=" + m.Email.ToString() + "&password=" + m.Password.ToString();
            HttpResponseMessage response = await client.PostAsync(_url, content);
            string json = response.Content.ReadAsStringAsync().Result;
            ViewData["Email"] = m.Email;
            if (json.Length == 0) return Redirect("~/Auth/Login");
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            User? createUpdateMember = JsonSerializer.Deserialize<User?>(json, option);
            HttpContext.Session.SetString("member", JsonSerializer.Serialize(createUpdateMember));
            return Redirect("~/Home/Index/");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("admin");
            HttpContext.Session.Remove("member");
            return Redirect("~/Auth/Login");
        }
    }
}
