using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Data.Classes;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroController
    {
        private readonly IRepository<Hero> _heroRepo;
        private readonly IRepository<Equipment> _equipmentRepo;


        public HeroController(IRepository<Hero> heroRepo, IRepository<Equipment> equipmentRepo)
        {
            _heroRepo = heroRepo;
            _equipmentRepo = equipmentRepo;
        }

        [HttpGet]
        public IEnumerable<Hero> Get()
        {
            return _heroRepo.Read();
        }

        [HttpGet("id")]
        public Hero Get(string id)
        {
            return _heroRepo.ReadFromOwner(id);
        }        

        [HttpPost]
        public void Create([FromBody] Hero item)
        {
            _heroRepo.Create(item);
        }
        [HttpPut]
        public void Update([FromBody] Hero item)
        {
            _heroRepo.Update(item);
        }

        [HttpDelete("id")]
        public void Delete(string id)
        {
            _heroRepo.Delete(id);
        }
    }
}
