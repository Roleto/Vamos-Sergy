using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Data;
using System.Xml.Linq;
using Vamos_Sergy.Models.Items;
namespace Vamos_Sergy.Data.Classes
{
    public class HeroRepository : IRepository<Hero>
    {
        ApplicationDbContext context;

        public HeroRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Hero item)
        {
            var heroName = context.Heroes.FirstOrDefault(h => h.Name == item.Name);

            if (heroName != null)
                throw new ArgumentException("Hero with this name already exists");

            context.Heroes.Add(item);
            context.SaveChanges();
        }
        public IQueryable<Hero> Read()
        {
            return context.Heroes;
        }

        public Hero? Read(string id)
        {
            return context.Heroes.FirstOrDefault(t => t.Id == id);
        }

        public Hero? ReadFromName(string name)
        {
            return context.Heroes.FirstOrDefault(t => t.Name == name);
        }

        public Hero? ReadFromOwner(string id)
        {
            return context.Heroes.FirstOrDefault(t => t.OwnerId == id);
        }

        public void Update(Hero item)
        {
            var old = Read(item.Id);
            if (old == null)
                return;
            old.Name = item.Name;
            old.GuildId = item.GuildId;
            old.GuildName = item.GuildName;
            old.Exp = item.Exp;
            old.Level = item.Level;
            old.Adventure = item.Adventure;
            old.HeroState = item.HeroState;
            old.Gold = Math.Round(item.Gold,2);
            old.FightCount = item.FightCount;
            old.Mushroom = item.Mushroom;
            old.BeerCount = item.BeerCount;
            old.Mount = item.Mount;
            old.MountEndDate = item.MountEndDate;
            old.Honor = item.Honor;
            old.WeaponShop = item.WeaponShop;
            old.MagicShop = item.MagicShop;
            old.QuestIds = item.QuestIds;
            old.SelectedQuest = item.SelectedQuest;
            old.QuestStarted = item.QuestStarted;
            old.Str = item.Str;
            old.Dex = item.Dex;
            old.Inte = item.Inte;
            old.Vit = item.Vit;
            old.Luck = item.Luck;
            context.SaveChanges();
        }
        public void Delete(string id)
        {
            Hero hero = Read(id);
            context.Heroes.Remove(hero);
            context.SaveChanges();
        }
    }
}
