using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;

namespace Vamos_Sergy.Data.Classes
{
    public class QuestRepository : IRepository<Quest>
    {
        ApplicationDbContext context;

        public QuestRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Quest item)
        {
            context.Quests.Add(item);
            context.SaveChanges();
        }
        public IQueryable<Quest> Read()
        {
            return context.Quests;
        }

        public Quest? Read(string id)
        {
            var quests = Read().ToArray();
            Random r = new Random();
            var quest = context.Quests.FirstOrDefault(x => x.Id == int.Parse(id));

            if (quest != null)
            {
                int exp = r.Next(100, 1001);
                double gold = (r.NextDouble() + .1) * 10;
                gold = Math.Round(gold, 2);
                return new Quest(quest.Id,quest.Text,exp,gold,context.Items.ToList());
            }
            return null;
        }

        public Quest? ReadFromOwner(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Quest item)
        {
            var old = context.Quests.FirstOrDefault(x => x.Id == item.Id);
            if(old == null)
                throw new ArgumentException("Can't update quest is not exists");
            old.Text = item.Text;
            context.SaveChanges();

        }

        public void Delete(string id)
        {
            var old = Read(id);
            if( old == null ) 
                throw new ArgumentException("Can't delete quest is not exists");
            context.Quests.Remove(old);
            context.SaveChanges();
        }

    }
}
