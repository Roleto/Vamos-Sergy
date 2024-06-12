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

        public ShopViewModel()
        {
            BackgroundUrl = "D:\\Egyetem\\prog5\\FF\\Vamos & Sergy\\Vamos & Sergy\\wwwroot\\Images\\Backgrounds\\weaponshop.jpg";
        }

        public ShopViewModel(Hero hero, List<Item> itemList,string url) : base(hero)
        {
            ItemList = itemList;
            BackgroundUrl = url;
            EquipmentList = new Dictionary<string, Equipment>();
            GenerateAll();
        }

        public virtual Equipment? Buy(int index)
        {
            Equipment e = GetEquipment(index);
            if (e.CanBuy(Hero.Gold, Hero.Mushroom))
            {
                GenerateOne(e.ItemId);
                Hero.Gold -= e.Price;
                Hero.Mushroom -= e.Mushroom;
                return e;
            }
            return null;
        }


        public void GenerateAll()
        {
            for (int i = 0; i < 6; i++)
            {
                int n = _random.Next(0, ItemList.Count());
                Equipment e = new Equipment(ItemList.ElementAt(n));
                if ((e.RequiredClass == Hero.Kast || e.RequiredClass == ClassEnum.All) && !EquipmentList.ContainsKey(e.ItemId))
                    EquipmentList[e.ItemId] = e;
                else
                    i--;
            }
        }


    }
}
