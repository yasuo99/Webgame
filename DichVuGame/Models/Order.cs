using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Display(Name = "Người mua")]
        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationID")]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime PurchasedDate { get; set; }
        [Display(Name = "Thành tiền")]
        public int Total { get; set; }
        public virtual ICollection<OrderDetail> Codes { get; set; }
    }
}
