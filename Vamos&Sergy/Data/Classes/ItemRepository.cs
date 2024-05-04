
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
            var itemName = context.Items.FirstOrDefault(h => h.Name == item.Name);

            if (itemName != null)
                throw new ArgumentException("Item with this name already exists");

            context.Items.Add(item);
            context.SaveChanges();
        }
        public virtual IQueryable<Item> Read()
        {
            return context.Items;
        }

        public virtual Item? Read(string id)
        {
            return context.Items.FirstOrDefault(t => t.Id == id);
        }

        public virtual Item? ReadFromOwner(string id)
        {
            throw new NotImplementedException("Items dont have owner id-s");
        }

        public virtual void Update(Item item)
        {
            var old = Read(item.Id);
            old.Name = item.Name;
            old.Description = item.Description;
            old.Type = item.Type;
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            var item = Read(id);
            context.Items.Remove(item);
            context.SaveChanges();
        }
    }
}
