using Client.Models;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string url = "";
        private string _url = "";

        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            url = "https://localhost:7200/api/Product/GetAllProduct";
            _url = "https://localhost:7200/api/Category/GetAllCategory";
            HttpResponseMessage response = await client.GetAsync(url);  
            HttpResponseMessage _response = await client.GetAsync(_url);
            string json = await response.Content.ReadAsStringAsync();
            string _json = await _response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Category>? categories = JsonSerializer.Deserialize<List<Category>>(_json,option);
            List<Product>? products = JsonSerializer.Deserialize<List<Product>>(json,option);
            ViewData["Categories"] = categories;
            return View(products);
        }

        public async Task<IActionResult> Detail(int id)
        {
            url = "https://localhost:7200/api/Product/GetProductById";
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            Product? product = JsonSerializer.Deserialize<Product>(json,option);
            return View(product);
        }

        public async Task<IActionResult> Profile()
        {
            string? getMember = HttpContext.Session.GetString("member");
            User? user = null;
            if (getMember is not null)
            {
                user = JsonSerializer.Deserialize<User>(getMember);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            url = "https://localhost:7200/api/User/GetUserById";
            HttpResponseMessage response = await client.GetAsync(url + "/" + user.UserId);
            string json = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            User? createUser = JsonSerializer.Deserialize<User>(json,option);
            return View(createUser);    
        }

        public async Task<ActionResult> UpdateUser(int id)
        {
            //get user
            string? getMember = HttpContext.Session.GetString("member");
            User? member = null;
            if (getMember is not null)
            {
                member = JsonSerializer.Deserialize<User>(getMember);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            url = "https://localhost:7200/api/User/GetUserById";
            HttpResponseMessage response = await client.GetAsync(url + "/" + member.UserId);
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
        public async Task<ActionResult> UpdateUser(User createUpdateMemberDTO)
        {
            if (HttpContext.Session.GetString("member") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/User/UpdateUser";
            JsonContent content = JsonContent.Create(createUpdateMemberDTO);
            HttpResponseMessage response = await client.PutAsync(url + "/" + createUpdateMemberDTO.UserId, content);
            try
            {
                return Redirect("~/home/profile");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> SaveOrder()
        {
            string? getMember = HttpContext.Session.GetString("member");
            User? user = null;
            if (getMember is not null)
            {
                user = JsonSerializer.Deserialize<User>(getMember);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            string? jsonCart = HttpContext.Session.GetString("cart");
            if (jsonCart is null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart>? carts = JsonSerializer.Deserialize<List<Cart>>(jsonCart);

            Order orderDtos = new Order
            {
                UserId = user.UserId,
                OrderDate = DateTime.Now,
                RequireDate = DateTime.Now.AddDays(7),
                ShippedDate = DateTime.Now.AddDays(1),
                Freight = carts.Sum(x => x.Product.UnitPrice * x.Quantity).ToString(),
            };
            url = "https://localhost:7200/api/Order/AddNewOrder";
            JsonContent content = JsonContent.Create(orderDtos);
            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                int orderId = JsonSerializer.Deserialize<int>(json, option);
                //add order detail
                List<OrderDetail> orderDetailDTOs = new List<OrderDetail>();
                foreach (var item in carts)
                {
                    OrderDetail orderDetailDTO = new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Product.UnitPrice,
                        Discount = 0
                    };
                    orderDetailDTOs.Add(orderDetailDTO);
                }
                url = "https://localhost:7200/api/OrderDetail/AddNewOrderDetail";
                JsonContent content1 = JsonContent.Create(orderDetailDTOs);
                HttpResponseMessage response1 = await client.PostAsync(url, content1);
                HttpContext.Session.Remove("cart");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Cart(int id)
        {
            //get user
            string? getMember = HttpContext.Session.GetString("member");
            User? member = null;
            if (getMember is not null)
            {
                member = JsonSerializer.Deserialize<User>(getMember);
                url = "https://localhost:7200/api/User/GetUserById";
                HttpResponseMessage response = await client.GetAsync(url + "/" + member.UserId);
                string json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                User? createUpdateMemberDTO = JsonSerializer.Deserialize<User>(json, options);
                ViewData["Member"] = createUpdateMemberDTO;
            }
            //add to cart
            if (id != 0)
            {
                await AddToCart(id);
            }
            //get cart
            string? jsoncart = HttpContext.Session.GetString("cart");
            if (jsoncart is null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<Cart>? carts = JsonSerializer.Deserialize<List<Cart>>(jsoncart);
            ViewData["TotalPrice"] = carts.Sum(x => x.Quantity * x.Product.UnitPrice);
            return View(carts);
        }

        private async Task<bool> AddToCart(int id)
        {
            List<Cart>? carts = new List<Cart>();
            url = "https://localhost:7200/api/Product/GetProductById";
            HttpResponseMessage response = await client.GetAsync(url + "/" + id);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Product? productOnOrder = JsonSerializer.Deserialize<Product>(json, options);
            string? getCart = HttpContext.Session.GetString("cart");
            if (getCart is not null)
            {
                carts = JsonSerializer.Deserialize<List<Cart>>(getCart);
            }
            if (carts is null)
            {
                carts = new List<Cart>();
                carts.Add(new Cart
                {
                    Product = productOnOrder,
                    Quantity = 1
                });
                string saveCart = JsonSerializer.Serialize(carts);
                HttpContext.Session.SetString("cart", saveCart);
                return true;
            }
            else
            {
                int cartIndex = Exists(carts, id);
                if (cartIndex == -1)
                {
                    carts.Add(new Cart
                    {
                        Product = productOnOrder,
                        Quantity = 1
                    });
                }
                else
                {
                    if (carts[cartIndex].Quantity < productOnOrder.Quantity)
                    {
                        carts[cartIndex].Quantity++;
                    }
                }
                string saveCart = JsonSerializer.Serialize(carts);
                HttpContext.Session.SetString("cart", saveCart);
                return true;

            }
        }
        private int Exists(List<Cart> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}