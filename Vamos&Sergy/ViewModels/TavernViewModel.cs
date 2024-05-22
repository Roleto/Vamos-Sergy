using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Models.Items;
using Vamos_Sergy.Models;

namespace Vamos_Sergy.ViewModels
{

    public class TavernViewModel
    {
        private Random _random;
        public Hero Hero { get; set; }
        //private int genGold()
        //{
        //    int random = _random.Next(1, 5);
        //    int gold_min = _random.Next(1, 5) * Hero.Level;
        //    int gold_max = _random.Next(5, 10) * Hero.Level;
        //    return _random.Next(gold_min, gold_max);
        //}
        //private int genExp()
        //{
        //    return _random.Next(Hero.MaxXp / 10, Hero.MaxXp / 4 + 1);
        //}

        //private int genMushrom()
        //{
        //    if (_random.Next(0, 101) <= 15)
        //    {
        //        return 1;
        //    }
        //    return 0;
        //}
        public TavernViewModel(Hero hero)
        {
            Hero = hero;
            _random = new Random();
            //for (int i = 0; i < Quests.Length; i++)
            //{
            //    int gold = _random.Next();
            //    int mushroom = 0;
            //    int exp = 0;
            //    Quests[i] = new Quest("------------Qeust text------------", genExp(), genGold(), genMushrom(), itemList);
            //}
        }

        public bool Gambling { get => _random.Next(0, 101) <= 33; }

        public bool BuyBeer()
        {
            if(Hero.Mushroom > 0)
            {
                Hero.Mushroom--;
                return true;
            }
            return false;
        }
    }
}
