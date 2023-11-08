using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

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
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Product/GetAllProduct";
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            string json = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product>? products = JsonSerializer.Deserialize<List<Product>>(json);
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string? name)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Product/GetProductByName";
            HttpResponseMessage response = await client.GetAsync(url + "/" + "name");
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product>? products = JsonSerializer.Deserialize<List<Product>>(json, options);
            ViewData["name"] = name;
            return View(products);
        }

        public async Task<ActionResult> Create()
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            _url = "https://localhost:7200/api/Category/GetAllCategory";
            HttpResponseMessage _response = await client.GetAsync(_url);
            string _json = await _response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            List<Category>? categories = JsonSerializer.Deserialize<List<Category>>(_json, options);
            ViewBag.Category = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product product)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Product/AddNewProduct";
            JsonContent content = JsonContent.Create(product);
            HttpResponseMessage response = await client.PostAsync(url, content);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> Update(int id)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Product/GetProductById/?";
            _url = "https://localhost:7200/api/Category/GetAllCategory";
            HttpResponseMessage responseMessage = await client.GetAsync(url + "id=" + id);
            HttpResponseMessage response = await client.GetAsync(_url);
            string json = await response.Content.ReadAsStringAsync();
            string _json = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Category>? categories = JsonSerializer.Deserialize<List<Category>>(json, options);
            Product? product = JsonSerializer.Deserialize<Product>(json, options);
            ViewBag.Categories = categories;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAsync(Product product)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Product/UpdateProduct/id?";
            JsonContent content = JsonContent.Create(product);
            HttpResponseMessage response = await client.PutAsync(url + "id=" + product.ProductId, content);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        public async Task<ActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("admin") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/Product/DeleteProduct/id?";
            HttpResponseMessage response = await client.DeleteAsync(url + "id=" + id);
            return RedirectToAction(nameof(Index));
        }
    }
}
