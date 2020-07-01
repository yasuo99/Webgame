using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class Discount
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int DiscountValue { get; set; }
        public bool Available { get; set; }
    }
}
