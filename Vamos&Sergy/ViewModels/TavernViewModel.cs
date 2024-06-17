using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Models.Items;
using Vamos_Sergy.Models;

namespace Vamos_Sergy.ViewModels
{

    public class TavernViewModel
    {
        private Random _random;
        public Hero Hero { get; set; }


        public TavernViewModel(Hero hero)
        {
            Hero = hero;
            _random = new Random();
        }
    }
}
