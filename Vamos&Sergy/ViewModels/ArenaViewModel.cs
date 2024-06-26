using Vamos_Sergy.Models;

namespace Vamos_Sergy.ViewModels
{
    public class ArenaViewModel
    {
        private List<Hero> HeroList;
        public Hero MyHero;
        public Hero Enemy;
        public Hero[] Heroes;
        public string Damages;
        public int FignhtNumber;
        public bool canFight;
        public bool HeroWin;
        public DateTime LastFight;
        Random _rnd;

        public ArenaViewModel(List<Hero> heroList, Hero myHero)
        {
            HeroList = heroList;
            MyHero = myHero;
            Heroes = new Hero[3];
            FignhtNumber = 0;
            _rnd = new Random();
            canFight = false;
            HeroWin = false;
            Damages = "";
            GenaretFighters(true);
        }

        public void GenaretFighters(bool first = false)
        {
            if (!first)
                canFight = false;

            LastFight = DateTime.Now;
            for (int i = 0; i < 3; i++)
            {
                Heroes[i] = HeroList.ElementAt(_rnd.Next(0, HeroList.Count));
            }
        }

        public void ChangeFight()
        {
            if (canFight)
                return;
            TimeSpan dif = DateTime.Now.Subtract(LastFight);
            if (dif.TotalMinutes > 10)
                canFight = true;
        }

        public void Fight()
        {
            List<int> list = new List<int>();
            string output = "";
            MyHero.CurrentHp = MyHero.Hp;

            Enemy.CurrentHp = Enemy.Hp;

            int damage = 0;
            while (true)
            {
                damage = MyHero.Damage;
                list.Add(damage);
                output += damage.ToString() + ';';
                Enemy.CurrentHp -= damage;
                if (Enemy.CurrentHp <= 0)
                {
                    HeroWin = true;
                    break;
                }
                damage = Enemy.Damage;
                list.Add(damage);
                output += damage.ToString() + ';';
                MyHero.CurrentHp -= damage;
                if (MyHero.CurrentHp <= 0)
                {
                    HeroWin = false;
                    break;
                }
            }

            Damages = output;
        }
    }
}
