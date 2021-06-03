using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int? PublisherId { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public string ProductDescription { get; set; }
        public int Amount { get; set; }
        public double Cost { get; set; }
    }
}
