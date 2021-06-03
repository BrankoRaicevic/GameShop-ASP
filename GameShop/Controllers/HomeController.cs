using GameShop.Models;
using GameShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GameShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;

        public HomeController(IProductRepository productRepository) 
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = productRepository.GetAllProducts();
            return View(model);
        }
        [HttpGet]
        public IActionResult Games()
        {
            var model = productRepository.GetGames();
            return View(model);
        }
        [HttpGet]
        public IActionResult Consoles()
        {
            var model = productRepository.GetConsoles();
            return View(model);
        }
        [HttpGet]
        public IActionResult Periferals()
        {
            var model = productRepository.GetPeriferals();
            return View(model);
        }
        [HttpGet]
        public ViewResult Details(int? id)
        {
            Product product = productRepository.GetProduct(id.Value);

            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            Manufacturer manufacturer = productRepository.GetManufacturer(product.ManufacturerId);

            Publisher publisher = productRepository.GetPublisher(product.PublisherId);

            if (manufacturer == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            HomeDetailsViewModel hdvm = new HomeDetailsViewModel()
            {
                Product = product,
                ProductId = product.ProductId,
                Manufacturer = manufacturer.ManufacturerName,
                Publisher = publisher != null ? publisher.PublisherName : null,
                PageTitle = product.ProductName
            };
            return View(hdvm);
        }

        [HttpPost]
        public IActionResult Details(HomeDetailsViewModel model)
        {
            Product product = productRepository.GetProduct(model.ProductId);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", model.ProductId);
            }
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(model.ProductId.ToString())))
            {
                HttpContext.Session.SetInt32(model.ProductId.ToString(), 1);
            }
            else
            {
                int res = (int)HttpContext.Session.GetInt32(model.ProductId.ToString());
                HttpContext.Session.SetInt32(model.ProductId.ToString(), res + 1);
                
            }
            List<Product> products = new List<Product>();
            foreach (var key in HttpContext.Session.Keys)
            {
                if (key != "username")
                {
                    var product2 = productRepository.GetProduct(Int32.Parse(key));
                    if (product2 != null)
                    {
                        products.Add(product2);
                    }
                }
            }
            return RedirectToAction("Cart", products);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            List<Product> products = new List<Product>();
            foreach (var key in HttpContext.Session.Keys)
            {
                if (key != "username")
                {
                    var product2 = productRepository.GetProduct(Int32.Parse(key));
                    if (product2 != null)
                    {
                        products.Add(product2);
                    }
                }
            }
            return View(products);
        }

        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            HttpContext.Session.Remove(id.ToString());

            List<Product> products = new List<Product>();
            foreach (var key in HttpContext.Session.Keys)
            {
                if (key != "username")
                {
                    var product2 = productRepository.GetProduct(Int32.Parse(key));
                    if (product2 != null)
                    {
                        products.Add(product2);
                    }
                }
            }
            return View("Cart", products);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            if (HttpContext.Session.Keys.Count() < 2)
            {
                return View("Checkout");
            }

            SqlConnection cn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;database=GameShop;Trusted_Connection=true");

            cn.Open();
            var username = HttpContext.Session.GetString("username");
            foreach (var key in HttpContext.Session.Keys)
            {
                if(key != "username") 
                {
                    SqlCommand cmd = new SqlCommand("USP_CommitTransaction");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = cn;

                    cmd.Parameters.Add("@productid", SqlDbType.Int).Value = Int32.Parse(key);
                    cmd.Parameters.Add("@amount", SqlDbType.Int).Value = HttpContext.Session.GetInt32(key);
                    cmd.Parameters.Add("@user", SqlDbType.VarChar, 30).Value = username;

                    cmd.ExecuteNonQuery();

                    HttpContext.Session.Remove(key);
                }
            }

            cn.Close();

            return View("Checkout");
        }
    }
}
