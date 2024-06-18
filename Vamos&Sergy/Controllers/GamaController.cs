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
        private readonly IRepository<Equipment> _equipmentRepo;


        public GamaController(IRepository<Hero> heroRepo)
        {
            _heroRepo = heroRepo;
        }

        //post
        public void BuyItem([FromBody] ShopItem item,HeroItem heroItem)
        {

            var hero = _heroRepo.Read(heroItem.Id);

            //if (hero.InvIndex > 0)
            //{
            //    Equipment? e = _weaponshop.Buy(index, hero);
            //    if (e != null)
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
            if (hero != null) {
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

        }
    }
}
