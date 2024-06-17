using Vamos_Sergy.Models;

namespace Vamos_Sergy.ViewModels
{
    public class ArenaViewModel
    {
        private List<Hero> HeroList;
        public Hero MyHero;
        public Hero[] Heroes;
        public int FignhtNumber;
        public bool canFight;
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
    }
}
