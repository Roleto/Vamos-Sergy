using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Models.Items;
using Vamos_Sergy.Models;

namespace Vamos_Sergy.ViewModels
{

    public class TavernViewModel
    {
        private Random _random;
        public Hero Hero { get; set; }

        public bool HeroWin { get; set; }     

        public TavernViewModel(Hero hero)
        {
            Hero = hero;
            _random = new Random();
        }

        public List<int> Fight()
        {
            List<int> list = new List<int>();
            Hero.CurrentHp = Hero.Hp;
            Monster monster = Hero.Quest[Hero.SelectedQuest.Value].Enemy;
            monster.CurrentHp = monster.Hp;

            int damage = 0;
            while(true)
            {
                damage = Hero.Damage;
                list.Add(damage);
                monster.CurrentHp -= damage;
                if(monster.CurrentHp <= 0)
                {
                    HeroWin = true;
                    break;
                }
                damage = monster.Damage;
                list.Add(damage);
                Hero.CurrentHp -= damage;
                if(Hero.CurrentHp <= 0)
                {
                    HeroWin = false;
                    break;
                }
            }

            return list;
        }
    }
}
