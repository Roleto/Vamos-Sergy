using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.ViewModels
{
    public class ShopViewModel
    {
        private readonly List<Item> items;
        public Hero Hero { get; set; }

        public string BackgroundUrl { get; set; }

        public ShopViewModel(Hero hero, List<Item> items, string name)
        {
            Hero = hero;
            this.items = items;
        }

        public Equipment GetShopItem(int i, string name)
        {
            var shopItem = Hero.Shops.Where(shop => shop.ShopType == name).FirstOrDefault(x => x.ShopSlot == i);
            var item = items.FirstOrDefault(x => x.Id == shopItem.ItemId);
            shopItem.Item = item;
            Equipment eq = new Equipment(shopItem);
            return eq;
        }
    }
}
