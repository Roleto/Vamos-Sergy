using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Data.Classes
{
    public class ItemRepository : IRepository<Item>
    {
        ApplicationDbContext context;

        public ItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Item item)
        {
            var itemName = context.Heroes.FirstOrDefault(h => h.Name == item.Name);

            if (itemName != null)
                throw new ArgumentException("Item with this name already exists");

            context.Items.Add(item);
            context.SaveChanges();
        }
        public IEnumerable<Item> Read()
        {
            return context.Items;
        }

        public Item? Read(string id)
        {
            return context.Items.FirstOrDefault(t => t.Id == id);
        }
        
        public Item? ReadFromName(string name)
        {
            return context.Items.FirstOrDefault(t => t.Name == name);
        }

        public Item? ReadFromOwner(string id)
        {
            throw new NotImplementedException("Items dont have owner id-s");
        }

        public void Update(Item item)
        {
            var old = Read(item.Id);
            old.Name = item.Name;
            old.Description = item.Description;
            old.Stats = item.Stats;
            old.Type = item.Type;
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            Item item = Read(id);
            context.Items.Remove(item);
            context.SaveChanges();
        }
    }
}
