using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.ViewModels
{
    public class AddCompanyViewModel
    {
        [Required]
        [Remote(action: "CompanyAlreadyExists", controller: "Administration", AdditionalFields = "IsSelected")]
        public string Name { get; set; }
        [Required]
        public int IsSelected { get; set; }
    }
}
