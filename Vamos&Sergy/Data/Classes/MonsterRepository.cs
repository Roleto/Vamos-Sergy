using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Data.Classes
{
    public class MonsterRepository : IRepository<Monster>
    {
        ApplicationDbContext context;

        public MonsterRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Monster item)
        {
            var monster = context.Monsters.FirstOrDefault(m => m.Id == item.Id);

            if (monster != null)
                throw new ArgumentException("Hero with this name already exists");

            context.Monsters.Add(item);
            context.SaveChanges();
        }
        public IQueryable<Monster> Read()
        {
           return context.Monsters;
        }

        public Monster? Read(string id)
        {
            return context.Monsters.FirstOrDefault(m => m.Id == id);
        }

        public Monster? ReadFromOwner(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Monster item)
        {
            var old = Read(item.Id);
            if (old == null)
                return;
            old.Name = item.Name;
            old.Level = item.Level;
            old.Kast = item.Kast;
            old.Str = item.Str;
            old.Dex = item.Dex;
            old.Inte = item.Inte;
            old.Vit = item.Vit;
            old.Luck = item.Luck;
            if(item.ContentType.Length > 3)
            old.ContentType = item.ContentType;
            if(item.Data != null)
                old.Data = item.Data;
            context.SaveChanges();
        }
        public void Delete(string id)
        {
            var monster = context.Monsters.FirstOrDefault(m => m.Id == id);

            if (monster != null)
                throw new ArgumentException("Hero with this name already exists");

            context.Monsters.Remove(monster);
            context.SaveChanges();
        }
    }
}
