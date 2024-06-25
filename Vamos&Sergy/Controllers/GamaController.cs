using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using Vamos_Sergy.Data.Classes;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GamaController : ControllerBase
    {
        private readonly IRepository<Hero> _heroRepo;
        private readonly IRepository<Shop> _shopRepo;
        private readonly IRepository<Item> _itemRepo;
        private readonly IRepository<Equipment> _equipmentRepo;
        private Random _rnd;

        public GamaController(IRepository<Hero> heroRepo, IRepository<Shop> shopRepo, IRepository<Item> itemRepo, IRepository<Equipment> equipmentRepo)
        {
            _heroRepo = heroRepo;
            _shopRepo = shopRepo;
            _itemRepo = itemRepo;
            _equipmentRepo = equipmentRepo;
            _rnd = new Random();
        }
        [HttpGet("{id}")]
        public IEnumerable<Equipment> EqupItem(string id)
        {
            //return PartialViewResul
            List<Equipment> output = new List<Equipment>();
            var newEq = _equipmentRepo.Read(id);
            var item = _itemRepo.Read(newEq.ItemId);
            output.Add(new Equipment(item, newEq.Stats));
            Equipment equipment = null;
            var hero = _heroRepo.Read(newEq.Owner.Id);
            var eqItems = hero.Equipments.Where(x => x.IsEqueped == true);
            //var oldEq = hero.Equipments.FirstOrDefault(x => x.IsEqueped == true && x.Item.Type == newEq.Item.Type);
            foreach (var eq in hero.Equipments)
            {
                if (eq.IsEqueped == true)
                {
                    item = _itemRepo.Read(eq.ItemId);
                    if (item.Type == output[0].Item.Type)
                    {
                        equipment = new Equipment(item, eq.Stats);
                        equipment.Id = eq.Id;
                        break;
                    }
                }

            }
            if (equipment != null)
            {
                equipment.IsEqueped = false;
                equipment.InventorySlot = newEq.InventorySlot;
                //_equipmentRepo.Update(equipment);
                output.Add(equipment);
            }
            newEq.IsEqueped = true;
            //_equipmentRepo.Update(newEq);

            return output;
        }

        [HttpPost]
        public Equipment BuyWeapon([FromBody] ShopItem item)
        {

            var oldItem = _itemRepo.Read(item.ItemId);
            string[] stats = item.Stats.Split(';');
            var hero = _heroRepo.Read(item.HeroId);
            Equipment equipment = new Equipment(oldItem, item.Stats);
            if (hero.CanBuy(equipment))
            {
                var items = _itemRepo.Read().ToArray();
                equipment.Owner = hero;
                equipment.OwherId = hero.Id;
                equipment.InventorySlot = item.InvSlot;
                hero.Inventory[equipment.InventorySlot] = equipment;
                _heroRepo.Update(hero);
                _equipmentRepo.Create(equipment);
                Item newItem = null;
                do
                {
                    var i = items[_rnd.Next(items.Length)];
                    if (i.RequiredClass == hero.Kast)
                    {
                        newItem = i;
                    }
                } while (newItem == null);
                Shop s = new Shop(newItem, hero, item.Name, item.ShopSlot);
                var updateShop = _shopRepo.Read().FirstOrDefault(x => x.ShopType == item.Name && x.ShopSlot == item.ShopSlot);
                s.Id = updateShop.Id;
                _shopRepo.Update(s);
                var eq = new Equipment(s);
                return eq;
            }
            return null;
            //if (hero.InvIndex > 0)
            //{
            //    if (hero.heroItem != null)
            //    {
            //        e.OwherId = hero.Id;
            //        e.InventorySlot = hero.GetFirsNull;
            //        hero.Inventory[e.InventorySlot] = e;
            //        _equipmentRepo.Create(e);
            //        _heroRepo.Update(hero);
            //        _hero = hero;
            //        this.RefreshHero(hero);
            //        RedirectToAction(nameof(WeaponShop));
            //    }
            //    ViewData["error"] = "Don't have enough money";
            //    return View("WeaponShop", _weaponshop);
            //}
            //ViewData["error"] = "Inventury is full";
            //return View("WeaponShop", _weaponshop);
        }

        [HttpPut]
        public void UpdateHero([FromBody] HeroItem heroItem)
        {
            var hero = _heroRepo.Read(heroItem.Id);
            if (hero != null)
            {
                hero.Gold += heroItem.Gold;
                hero.Mushroom += heroItem.Mushroom;
                hero.Adventure += heroItem.Adventure;
                hero.BeerCount += heroItem.BeerCount;
                _heroRepo.Update(hero);
            }
        }
        //Get


        protected Equipment GenerateOne(string id)
        {
            Random _random = new Random();
            var ItemList = _itemRepo.Read().ToArray();
            int n = _random.Next(0, ItemList.Length);
            return new Equipment(ItemList[n]);
        }
    }
}
