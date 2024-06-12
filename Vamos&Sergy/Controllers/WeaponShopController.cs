using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Data.Classes;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponShopController : ControllerBase
    {
        private readonly IRepository<Item> _itemRepo;
        private static List<Item> items;
        public WeaponShopController(IRepository<Item> itemRepo)
        {
            _itemRepo = itemRepo;
            items = _itemRepo.Read().ToList();
        }

        [HttpGet]
        public Equipment Get()
        {
            Random r = new Random();
            if(items.Count == 0)
                items = _itemRepo.Read().ToList();
            int n = r.Next(0, items.Count);
            Equipment equipment = new Equipment(items.ElementAt(n));
            items.RemoveAt(n);
            return equipment;
        }


    }
}
