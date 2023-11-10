using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        private HttpClient client;
        private string url = "";

        public UserController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            url = "https://localhost:7200/api/User";
        }
        [HttpGet]
        public IActionResult Index()
        {
            HttpResponseMessage response = client.GetAsync($"{url}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<User> users = JsonConvert.DeserializeObject<List<User>>(data);
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync($"{url}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                User user = new User();
                HttpResponseMessage response = client.GetAsync($"{url}/GetUserById/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(data);
                }
                return View(user);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id , User user) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync($"{url}/{user.UserId}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                User user = new User();
                HttpResponseMessage response = client.GetAsync($"{url}/GetUserById/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(data);
                }
                return View(user);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost , ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
