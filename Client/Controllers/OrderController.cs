using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        private HttpClient client;
        private string url = "";
        public OrderController()
        {
            client = new HttpClient();
            var contetType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contetType);
            url = "https://localhost:7200/api/Order";
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            HttpResponseMessage response = client.GetAsync($"{url}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(data);
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            HttpResponseMessage response = client.GetAsync($"{url}/GetOrdeDetailByOrderId/{id}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            OrderDetail order = JsonConvert.DeserializeObject<OrderDetail>(data);
            return View(order);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            try
            {
                string data = JsonConvert.SerializeObject(order);
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
                Order order = new Order();
                HttpResponseMessage response = client.GetAsync($"{url}/GetOrderById/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    order = JsonConvert.DeserializeObject<Order>(data);
                }
                return View(order);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Edit(Order order) 
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            try
            {
                string data = JsonConvert.SerializeObject(order);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync($"{url}/{order.OrderId}", content).Result;
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
                Order order = new Order();
                HttpResponseMessage response = client.GetAsync($"{url}/GetOrderById/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    order = JsonConvert.DeserializeObject<Order>(data);
                }
                return View(order);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost , ActionName("Delete")]
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
