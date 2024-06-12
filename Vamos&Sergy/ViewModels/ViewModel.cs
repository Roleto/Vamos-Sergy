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

        public ViewModel(Hero hero)
        {
            Hero = hero;
            _random = new Random();
        }

        public Equipment GetEquipment(int n = 0)
        {
            return EquipmentList.ElementAt(n).Value;
        }
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
