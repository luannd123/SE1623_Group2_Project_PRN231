using Client.Models;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client;
        private string url = "";
        private string _url = "";

        public HomeController()
        {

            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

        }
        public async Task<IActionResult> Index(int? id)
        {
             url = "https://localhost:7200/api/Product";
            _url = "https://localhost:7200/api/Category";         
            HttpResponseMessage response = await client.GetAsync(url);
            HttpResponseMessage _response = await client.GetAsync(_url);
            string json = await response.Content.ReadAsStringAsync();
            string _json = await _response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Product>? products = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(json, options);
            List<Category>? categories = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(_json, options);
            ViewData["Categories"] = categories;
            return View(products);
        }

       
        public async Task<IActionResult> Category(int id)
        {         
            url = "https://localhost:7200/api/Category/GetProductByCategoryId";
            _url = "https://localhost:7200/api/Category";
            HttpResponseMessage response = client.GetAsync($"{url}/{id}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(data);

            HttpResponseMessage _response = client.GetAsync($"{_url}").Result;
            string _data = _response.Content.ReadAsStringAsync().Result;
            List<Category> category = JsonConvert.DeserializeObject<List<Category>>(_data);
            ViewData["Categories"] = category;
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            url = "https://localhost:7200/api/Product/GetProductById";
            Product product = new Product();
            HttpResponseMessage response = client.GetAsync($"{url}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(data);
            }
            return View(product);
        }

        public async Task<IActionResult> Profile()
        {
 
            string? getMember = HttpContext.Session.GetString("member");
            User? member = null ;
            if (getMember is not null)
            {
                member = System.Text.Json.JsonSerializer.Deserialize<User>(getMember);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            url = "https://localhost:7200/api/User";
            HttpResponseMessage response = client.GetAsync($"{url}/GetUserById/{member.UserId}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                member = JsonConvert.DeserializeObject<User>(data);
                return View(member);
            }
            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> Orders()
        {
            //get user
            string? getMember = HttpContext.Session.GetString("member");
            User? member = null;
            Order? order = null;
            if (getMember is not null)
            {
                member = System.Text.Json.JsonSerializer.Deserialize<User>(getMember);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            url = "https://localhost:7200/api/Order";
            var url1 = url + member.UserId;
            HttpResponseMessage response = await client.GetAsync($"{url}/GetOrderById/{order.OrderId}");
            string json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Order>? orders = System.Text.Json.JsonSerializer.Deserialize<List<Order>>(json, options);
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            //get user
            string? getMember = HttpContext.Session.GetString("member");
            User? member = null;
            if (getMember is not null)
            {
                member = System.Text.Json.JsonSerializer.Deserialize<User>(getMember);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            url = "https://localhost:7200/api/OrderDetail";
            HttpResponseMessage response = await client.GetAsync($"{url}/{id}");
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<OrderDetail>? orderDetailDTO = System.Text.Json.JsonSerializer.Deserialize<List<OrderDetail>>(json, options);
            return View(orderDetailDTO);
        }

        public async Task<ActionResult> UpdateUser(int id)
        {
            //get user
            string? getMember = HttpContext.Session.GetString("member");
            User? member = null;
            if (getMember is not null)
            {
                member = System.Text.Json.JsonSerializer.Deserialize<User>(getMember);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            url = "https://localhost:7200/api/User";
            HttpResponseMessage response = await client.GetAsync(url + "/" + member.UserId);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            User createUpdateMemberDTO = System.Text.Json.JsonSerializer.Deserialize<User>(json, options);
            return View(createUpdateMemberDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUser(User createUpdateMemberDTO)
        {
            if (HttpContext.Session.GetString("member") is null) return Redirect("~/Auth/Login");
            url = "https://localhost:7200/api/User";
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
            //get user
            string? getMember = HttpContext.Session.GetString("member");
            User? member = null;
            if (getMember is not null)
            {
                member = System.Text.Json.JsonSerializer.Deserialize<User>(getMember);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }

            //get cart
            string? jsoncart = HttpContext.Session.GetString("cart");
            if (jsoncart is null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<Cart>? carts = System.Text.Json.JsonSerializer.Deserialize<List<Cart>>(jsoncart);

            //add order
            Order orderDTO = new Order
            {
                UserId = member.UserId,
                OrderDate = DateTime.Now,
                ShippedDate = DateTime.Now.AddDays(1),
                RequireDate = DateTime.Now.AddDays(7),
                Freight = carts.Sum(x => x.Product.UnitPrice * x.Quantity).ToString(),
            };
            url = "https://localhost:7200/api/Order";
            JsonContent content = JsonContent.Create(orderDTO);
            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                int orderId = System.Text.Json.JsonSerializer.Deserialize<int>(json, options);
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
                url = "https://localhost:7200/api/OrderDetail";
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
                member = System.Text.Json.JsonSerializer.Deserialize<User>(getMember);
                url = "https://localhost:7200/api/User";
                HttpResponseMessage response = await client.GetAsync($"{url}/GetUserById/{member.UserId}");
                string json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                User? createUpdateMemberDTO = System.Text.Json.JsonSerializer.Deserialize<User>(json, options);
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
            List<Cart>? carts = System.Text.Json.JsonSerializer.Deserialize<List<Cart>>(jsoncart);
            ViewData["TotalPrice"] = carts.Sum(x => x.Quantity * x.Product.UnitPrice);
            return View(carts);
        }

        public IActionResult removeCart(int id)
        {
            string? getCart = HttpContext.Session.GetString("cart");
            List<Cart>? carts = System.Text.Json.JsonSerializer.Deserialize<List<Cart>>(getCart);
            int index = Exists(carts, id);
            carts.RemoveAt(index);
            if (carts.Count == 0)
            {
                HttpContext.Session.Remove("cart");
                return RedirectToAction("Cart", "Home");
            }
            string savesjoncart = System.Text.Json.JsonSerializer.Serialize(carts);
            HttpContext.Session.SetString("cart", savesjoncart);
            return RedirectToAction("Cart", "Home");
        }
        private async Task<bool> AddToCart(int id)
        {
            List<Cart>? carts = new List<Cart>();
            url = "https://localhost:7200/api/Product";
            HttpResponseMessage response = await client.GetAsync(url + "/GetProductById/" + id);
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Product? productOnOrder = System.Text.Json.JsonSerializer.Deserialize<Product>(json, options);
            string? getCart = HttpContext.Session.GetString("cart");
            if (getCart is not null)
            {
                carts = System.Text.Json.JsonSerializer.Deserialize<List<Cart>>(getCart);
            }
            if (carts is null)
            {
                carts = new List<Cart>();
                carts.Add(new Cart
                {
                    Product = productOnOrder,
                    Quantity = 1
                });
                string saveCart = System.Text.Json.JsonSerializer.Serialize(carts);
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
                string saveCart = System.Text.Json.JsonSerializer.Serialize(carts);
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
