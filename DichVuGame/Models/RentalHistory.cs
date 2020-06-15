using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class RentalHistory
    {
        public int ID { get; set; }
        [Display(Name = "Id khách thuê")]
        public string ApplicationUserID { get; set;}
        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int GameAccountID { get; set; }
        [ForeignKey("GameAccountID")]
        public virtual GameAccount GameAccount { get; set; }
        [Display(Name = "Thời gian bắt đầu")]
        public DateTime StartRenting { get; set; }
        [Display(Name = "Thời gian kết thúc")]
        public DateTime EndRenting { get; set; }
        [Display(Name = "Thanh toán")]
        public int Total { get; set; }
        public bool OnGoing { get; set; }
    }
}
