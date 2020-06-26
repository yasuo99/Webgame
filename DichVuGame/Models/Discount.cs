using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class Discount
    {
        public int ID { get; set; } 
        public string DiscountCode { get; set; }
        public double DiscountValue { get; set; }
        public int Amount { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<RentalHistory> RentalHistories { get; set; }
    }
}
