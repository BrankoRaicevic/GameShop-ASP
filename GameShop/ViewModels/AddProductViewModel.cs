using GameShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.ViewModels
{
    public class AddProductViewModel
    {
        public AddProductViewModel()
        {
            Categories = new List<Category>();
            Manufacturers = new List<Manufacturer>();
            Publishers = new List<Publisher>();
        }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Product Model")]
        public string ProductModel { get; set; }
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Publisher")]
        public int? PublisherId { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        [Remote(action: "IsValidPrice", controller: "Administration")]
        public string Cost { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
    }
}
