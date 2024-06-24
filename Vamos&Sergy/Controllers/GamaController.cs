using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public Equipment? BuyWeapon([FromBody] ShopItem item)
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
                    if(i.RequiredClass == hero.Kast)
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
        [HttpGet("{id}")]
        public Equipment EqupItem(string id)
        {
            //return PartialViewResul
            return  _equipmentRepo.Read(id);
        }

        protected Equipment GenerateOne(string id)
        {
            Random _random = new Random();
            var ItemList = _itemRepo.Read().ToArray();
            int n = _random.Next(0, ItemList.Length);
            return new Equipment(ItemList[n]);
        }
    }
}
