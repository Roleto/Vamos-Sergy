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
        private readonly IRepository<Item> _itemRepo;

        public GamaController(IRepository<Hero> heroRepo, IRepository<Equipment> equipmentRepo, IRepository<Item> itemRepo)
        {
            _heroRepo = heroRepo;
            _itemRepo = itemRepo;
        }

        [HttpPost]
        public Equipment? BuyWeapon([FromBody] ShopItem item)
        {

            var i = _itemRepo.Read(item.ItemId);
            //"f48378fc-0d28-4f9f-b37a-615a3cae98c8"
            //"d0370360-bc48-47b5-8409-4e932ce30eba"
            string[] stats = item.Stats.Split(';');
            string rawprices = stats[stats.Length - 2];
            rawprices = rawprices.Remove(rawprices.Length - 1, 1);
            char[] delimiterChars = { ':', 'g' };
            string[] prices = rawprices.Split(delimiterChars);
            //rawprices = rawprices.Remove(6, rawprices.Length - 6);
            double gold = double.Parse(prices[1]);
            int mushroom = int.Parse(prices[2]);
            var hero = _heroRepo.Read(item.HeroId);
            if (hero.Gold >= gold && hero.Mushroom >= mushroom)
            {
                hero.Gold -= gold;
                hero.Mushroom -= mushroom;
                //_heroRepo.Update(hero);
                return new Equipment(i, item.Stats);
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
        public void RefreshItem()
        {
            //return PartialViewResul
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
