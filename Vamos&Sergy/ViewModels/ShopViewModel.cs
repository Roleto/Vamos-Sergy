using System;
using System.Collections.Generic;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.ViewModels
{
    public class ShopViewModel : ViewModel
    {
        public string BackgroundUrl { get; set; }

        public ShopViewModel(Hero hero, List<Item> itemList,string url) : base(hero)
        {
            ItemList = itemList;
            BackgroundUrl = url;
            EquipmentList = new Dictionary<string, Equipment>();
            GenerateAll(hero);
        }

        public virtual Equipment? Buy(int index,Hero hero)
        {
            Equipment e = GetEquipment(index);
            if (e.CanBuy(hero.Gold, hero.Mushroom))
            {
                GenerateOne(e.ItemId);
                hero.Gold -= e.Price;
                hero.Mushroom -= e.Mushroom;
                return e;
            }
            return null;
        }


        public void GenerateAll(Hero hero)
        {
            for (int i = 0; i < 6; i++)
            {
                int n = _random.Next(0, ItemList.Count());
                Equipment e = new Equipment(ItemList.ElementAt(n));
                if ((e.RequiredClass == hero.Kast || e.RequiredClass == ClassEnum.All) && !EquipmentList.ContainsKey(e.ItemId))
                    EquipmentList[e.ItemId] = e;
                else
                    i--;
            }
        }


    }
}
