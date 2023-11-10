using DataAccess.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client;
        private string url = "";
        private string _url = "";

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            url = "https://localhost:7200/api/Product";
            _url = "https://localhost:7200/api/Category";
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");

            HttpResponseMessage response = client.GetAsync($"{url}").Result;
            string data = response.Content.ReadAsStringAsync().Result;  
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(data);
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            HttpResponseMessage response = client.GetAsync($"{_url}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<Category> category = JsonConvert.DeserializeObject<List<Category>>(data);
            ViewBag.Category = category;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product) 
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            try
            {
                string data = JsonConvert.SerializeObject(product);
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
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            try
            {
                HttpResponseMessage _response = client.GetAsync($"{_url}").Result;
                string _data = _response.Content.ReadAsStringAsync().Result;
                List<Category> category = JsonConvert.DeserializeObject<List<Category>>(_data);
                ViewData["Category"] = category;

                Product product = new Product();
                HttpResponseMessage response = client.GetAsync($"{url}/GetProductById/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(data);
                }
                return View(product);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            try
            {
                HttpResponseMessage _response = client.GetAsync($"{_url}").Result;
                string _data = _response.Content.ReadAsStringAsync().Result;
                List<Category> category = JsonConvert.DeserializeObject<List<Category>>(_data);
                ViewData["Category"] = category;

                string data = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync($"{url}/{product.ProductId}", content).Result;
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
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            try
            {
                Product product = new Product();
                HttpResponseMessage response = client.GetAsync($"{url}/GetProductById/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(data);
                }
                return View(product);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
