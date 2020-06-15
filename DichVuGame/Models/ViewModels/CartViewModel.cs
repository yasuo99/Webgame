using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuGame.Models.ViewModels
{
    public class CartViewModel
    {
        public Game Game { get; set; }
        public List<Game> Games { get; set; }
        public GameAccount GameAccount { get; set; }
    }
}
