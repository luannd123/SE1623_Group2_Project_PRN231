using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient client;
        private string url = "";
        public OrderController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<ActionResult> IndexAsync()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Order/GetOrders";
            HttpResponseMessage response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Order>? orderDTO = JsonSerializer.Deserialize<List<Order>>(json, options);
            return View(orderDTO);
        }
        public async Task<ActionResult> DetailsAsync(int id)
        {
            url = "https://localhost:7200/api/OrderDetail/GetOrderDetails";
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<OrderDetail>? orderDetailDTO = JsonSerializer.Deserialize<List<OrderDetail>>(json, options);
            return View(orderDetailDTO);
        }

        public async Task<ActionResult> Update(int id)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Order/UpdateOrder";
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Order? orderDTO = JsonSerializer.Deserialize<Order>(json, options);
            return View(orderDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAsync(Order orderDTO)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Authen/Login");
            url = "https://localhost:7200/api/Order/UpdateOrder";
            JsonContent content = JsonContent.Create(orderDTO);
            HttpResponseMessage response = await client.PutAsync(url + "/" + orderDTO.OrderId, content);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Order";
            HttpResponseMessage response = await client.DeleteAsync(url + "/" + id);
            return RedirectToAction(nameof(Index));
        }
    }
}
