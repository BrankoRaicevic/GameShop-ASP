using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public float ProductCost { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
