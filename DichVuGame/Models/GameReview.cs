using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class GameReview
    {
        public int GameID { get; set; }
        [ForeignKey("GameID")]
        public Game Game { get; set; }
        public int ReviewID { get; set; }
        [ForeignKey("ReviewID")]
        public Review Review { get; set; }
    }
}
