using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models.ViewModels
{
    public class HomeViewModel
    {
        public Game Game { get; set; }
        public GameAccount GameAccount { get; set; }
        public List<Game> Games { get; set; }
        public List<GameAccount> GameAccounts { get; set; } 
    }
}
