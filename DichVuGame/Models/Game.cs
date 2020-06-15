using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class Game
    {
        public int ID { get; set; }
        [Display(Name = "Tên game")]
        public string Gamename { get; set; }
        [Display(Name = "Poster game")]
        public string GamePoster { get; set; }
        [Display(Name ="Năm phát hành")]
        public DateTime Release { get; set; }
        public int StudioID { get; set; }
        [ForeignKey("StudioID")]
        public virtual Studio Studio { get; set; }
        [Display(Name = "Giá")]
        public int Price { get; set; }
        [Display(Name = "Sẵn có")]
        public int Available { get; set; }      
        public string Alias { get; set; }
        public virtual SystemRequirement SystemRequirement { get; set; }
        public virtual ICollection<Code> Codes { get; set; }
        public virtual ICollection<GameTag> GameTags { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<GameAccount> GameAccounts { get; set; }
        public virtual ICollection<GameDemo> GameDemos { get; set; }
    }
}
