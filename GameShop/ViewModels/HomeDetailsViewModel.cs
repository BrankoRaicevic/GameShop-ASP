using GameShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string Manufacturer { get; set; }
        public string Publisher { get; set; }
        public string PageTitle { get; set; }
    }
}
