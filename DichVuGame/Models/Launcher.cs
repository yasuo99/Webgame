using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class Launcher
    {
        public int ID { get; set; }
        [Display(Name = "Nhà phát ")]
        public string Launchername { get; set; }
        [Display(Name = "Link tải xuống")]
        public string Downloadlink { get; set; }
        public virtual List<Game> Games { get; set; }
    }
}
