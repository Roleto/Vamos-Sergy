using System;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.ViewModels
{
    public abstract class ViewModel 
    {
        public Hero Hero { get; set; }
        protected List<Item> ItemList { get; set; }
        protected Dictionary<string, Equipment> EquipmentList;
        protected Random _random;

        public ViewModel()
        {
        }
        public ViewModel(Hero hero)
        {
            Hero = hero;
            _random = new Random();
            //_random = new Random();
            //WeaponBackgroundUrl = "D:\\Egyetem\\prog5\\FF\\Vamos & Sergy\\Vamos & Sergy\\wwwroot\\Images\\Backgrounds\\weaponshop.jpg";

        }

        //public Equipment GetShopItem(int n)
        public Equipment GetEquipment(int n = 0)
        {
            return EquipmentList.ElementAt(n).Value;
        }

        //public void GenerateAll()
        //{
        //    for (int i = 0; i < 6; i++)
        //    {
        //        int n = _random.Next(0, ItemList.Count());
        //        Equipment e = new Equipment(ItemList.ElementAt(n));
        //        if ((e.RequiredClass == Hero.Kast || e.RequiredClass == ClassEnum.All) && !EquipmentList.ContainsKey(e.ItemId))
        //            EquipmentList[e.ItemId] = e;
        //        else
        //            i--;
        //    }
        //}
        //public Equipment? Buy(int index)
        //{
        //    Equipment e = GetEquipment(index);
        //    if (e.CanBuy(Hero.Gold, Hero.Mushroom))
        //    {
        //        GenerateOne(e.ItemId);
        //        Hero.Gold -= e.Price;
        //        Hero.Mushroom -= e.Mushroom;
        //        return e;
        //    }
        //    return null;
        //}
        protected void GenerateOne(string id)
        {
            EquipmentList.Remove(id);
            Equipment e = null;
            do
            {
                int n = _random.Next(0, ItemList.Count());
                e = new Equipment(ItemList.ElementAt(n));
            }
            while (EquipmentList.ContainsKey(e.ItemId));
            EquipmentList[e.ItemId] = e;
        }
    }
}
