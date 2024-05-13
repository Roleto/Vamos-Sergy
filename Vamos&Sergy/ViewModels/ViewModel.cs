using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.ViewModels
{
    public class ViewModel
    {
        public Hero Hero { get; set; }
        private List<Item> ItemList { get; set; }
        private Dictionary<string, Equipment> WeaponShop;
        private Dictionary<string, Equipment> MagicShop;
        public string WeaponBackgroundUrl { get; set; }
        public string MagicBackgroundUrl { get; set; }
        private Random _random;

        public ViewModel()
        {

        }
        public ViewModel(Hero hero, List<Item> itemRepo)
        {
            Hero = hero;
            ItemList = itemRepo;
            WeaponBackgroundUrl = "D:\\Egyetem\\prog5\\FF\\Vamos & Sergy\\Vamos & Sergy\\wwwroot\\Images\\Backgrounds\\weaponshop.jpg";
            WeaponShop = new Dictionary<string, Equipment>();
            MagicShop = new Dictionary<string, Equipment>();
            GenerateAll();
        }

        public Equipment GetShopItem(int n)
        {
            return WeaponShop.ElementAt(n).Value;
        }

        public void GenerateAll()
        {
            _random = new Random();
            for (int i = 0; i < 6; i++)
            {
                int n = _random.Next(0, ItemList.Count());
                Equipment e = new Equipment(ItemList.ElementAt(n));
                if ((e.RequiredClass == Hero.Kast || e.RequiredClass == ClassEnum.All) && !WeaponShop.ContainsKey(e.ItemId))
                    WeaponShop[e.ItemId] = e;
                else
                    i--;
            }
        }
        public Equipment? Buy(int index)
        {
            Equipment e = GetShopItem(index);
            if (e.CanBuy(Hero.Gold, Hero.Mushroom))
            {
                GenerateOne(e.ItemId);
                Hero.Gold -= e.Price;
                Hero.Mushroom -= e.Mushroom;
                return e;
            }
            return null;
        }
        void GenerateOne(string id)
        {
            WeaponShop.Remove(id);
            Equipment e = null;
            do
            {
                int n = _random.Next(0, ItemList.Count());
                e = new Equipment(ItemList.ElementAt(n));
            }
            while (WeaponShop.ContainsKey(e.ItemId));
            WeaponShop[e.ItemId] = e;
        }
    }
}
