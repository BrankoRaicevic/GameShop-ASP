using GameShop.Models;
using GameShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameShop.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IProductRepository productRepository;

        public AdministrationController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            AddProductViewModel apvm = new AddProductViewModel()
            {
                Manufacturers = productRepository.GetManufacturers(),
                Categories = productRepository.GetCategories(),
                Publishers = productRepository.GetPublishers()
            };
            return View(apvm);
        }
        [HttpPost]
        public IActionResult AddProduct(AddProductViewModel model)
        {
            double cost;
            if (ModelState.IsValid && Double.TryParse(model.Cost, out cost))
            {
                Product product = new Product()
                {
                    ProductName = model.ProductName,
                    ProductModel = model.ProductModel,
                    ProductDescription = model.ProductDescription,
                    CategoryId = model.CategoryId,
                    ManufacturerId = model.ManufacturerId,
                    PublisherId = model.PublisherId,
                    Amount = model.Amount,
                    Cost = cost
                };

                var result = productRepository.AddProduct(product);

                if (result != 0)
                {
                    return RedirectToAction("AddProduct");
                }
            }
            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsValidPrice([Bind(Prefix = "Cost")] string price)
        {
            double cost;
            if (!string.IsNullOrEmpty(price) && Double.TryParse(price, out cost) && !price.Contains('.'))
            {
                return Json(true);
            }
            else
            {
                return Json($"Invalid number");
            }
        }

        [HttpGet]
        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCompany(AddCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsSelected == 1)
                {
                    Manufacturer man = new Manufacturer()
                    {
                        ManufacturerName = model.Name
                    };
                    var result = productRepository.AddManufacturer(man);

                    if (result != 0)
                    {
                        return RedirectToAction("AddCompany");
                    }
                }
                else
                {
                    Publisher pub = new Publisher()
                    {
                        PublisherName = model.Name
                    };
                    var result = productRepository.AddPublisher(pub);

                    if (result != 0)
                    {
                        return RedirectToAction("AddCompany");
                    }
                }
            }
            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CompanyAlreadyExists(string Name, int IsSelected)
        {
            var user = productRepository.CheckCompany(Name, IsSelected);

            if (user == false)
            {
                return Json(true);
            }
            else
            {
                return Json($"Company {Name} is already in the database.");
            }
        }
    }
}
