﻿using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Data.Classes
{
    public class ShopRepository : IRepository<Shop>
    {
        ApplicationDbContext context;

        public ShopRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Shop item)
        {

            context.Shop.Add(item);
            context.SaveChanges();
        }
     

        public IQueryable<Shop> Read()
        {
            return context.Shop;
        }

        public Shop? Read(string id)
        {
            return context.Shop.FirstOrDefault(t => t.Id == id);
        }

        public Shop? ReadFromOwner(string id)
        {
            return context.Shop.FirstOrDefault(t => t.OwnerId == id);
        }

        public void Update(Shop item)
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
