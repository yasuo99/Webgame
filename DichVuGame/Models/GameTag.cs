using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class GameTag
    {
        public int GameID { get; set; }
        [ForeignKey("GameID")]
        public Game Game { get; set; }
        public int TagID { get; set; }
        [ForeignKey("TagID")]
        public Tag Tag { get; set; }
    }
}
