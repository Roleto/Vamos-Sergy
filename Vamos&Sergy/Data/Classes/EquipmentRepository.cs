
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Data.Classes
{
    public class EquipmentRepository : IRepository<Equipment>
    {
        protected ApplicationDbContext context;

        public EquipmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Equipment item)
        {
            var itemName = context.Equipments.FirstOrDefault(h => h.Name == item.Name);

            if (itemName != null)
                throw new ArgumentException("Item with this name already exists");

            context.Equipments.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<Equipment> Read()
        {
            return context.Equipments;
        }

        public Equipment? Read(string id)
        {
            return context.Equipments.FirstOrDefault(t => t.Id == id);
        }       

        public Equipment? ReadFromOwner(string id)
        {
            return context.Equipments.FirstOrDefault(t => t.Id == id);
        }

        public void Update(Equipment item)
        {
            var old = Read(item.Id);
            old.Name = item.Name;
            old.Description = item.Description;
            old.Type = item.Type;
            old.IsEqueped = item.IsEqueped;
            old.OwherId = item.OwherId;
            old.Stats = item.Stats;
            context.SaveChanges();
        }
        public void Delete(string id)
        {
            var item = Read(id);
            context.Equipments.Remove(item);
            context.SaveChanges();
        }
    }
}
