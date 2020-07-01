using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models
{
    public class GameLauncher
    {
        public int LauncherID { get; set; }
        [ForeignKey("LauncherID")]
        public Launcher Launcher { get; set; }
        public int GameID { get; set; }
        [ForeignKey("GameID")]
        public Game Game { get; set; }
    }
}
