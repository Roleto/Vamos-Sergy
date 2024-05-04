using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Data;
using System.Xml.Linq;
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
            old.Name = item.Name;
            old.Kast = item.Kast;
            old.Level = item.Level;
            old.HasMount = item.HasMount;
            old.Exp = item.Exp;
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
