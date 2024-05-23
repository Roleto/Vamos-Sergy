using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Models.Items;
using Vamos_Sergy.Models;

namespace Vamos_Sergy.ViewModels
{

    public class TavernViewModel
    {
        private Random _random;
        public Hero Hero { get; set; }
        public int selectedQuest { get; set; }
        public TavernViewModel(Hero hero)
        {
            Hero = hero;
            _random = new Random();
        }
        public bool Gambling { get => _random.Next(0, 101) <= 33; }
        public bool BuyBeer()
        {
            if (Hero.Mushroom > 0)
            {
                Hero.Mushroom--;
                return true;
            }
            return false;
        }
    }
}
