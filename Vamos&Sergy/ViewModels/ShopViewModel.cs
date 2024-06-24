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
        public Hero Hero { get; set; }

        public string BackgroundUrl { get; set; }

        public ShopViewModel(Hero hero,string name)
        {
            Hero = hero;
        }

        public Equipment GetShopItem(int i, string name)
        {
            var shopItem = Hero.Shops.Where(shop => shop.ShopType == name).FirstOrDefault(x => x.ShopSlot == i);
            Equipment eq = new Equipment(shopItem);
            return eq;
        }
    }
}
