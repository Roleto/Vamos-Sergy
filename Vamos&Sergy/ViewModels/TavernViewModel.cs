using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Models.Items;
using Vamos_Sergy.Models;
using Vamos_Sergy.Data.Interfaces;
using Newtonsoft.Json;

namespace Vamos_Sergy.ViewModels
{

    public class TavernViewModel : IViewModel
    {
        private Random _random;
        [JsonIgnore]
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
            if (Hero.Mushroom > 0 && Hero.BeerCount < 10)
            {
                Hero.Mushroom--;
                Hero.BeerCount++;
                return true;
            }
            return false;
        }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
