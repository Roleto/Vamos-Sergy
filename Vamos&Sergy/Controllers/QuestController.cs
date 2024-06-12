using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models.Items;
using Vamos_Sergy.Models;
using Vamos_Sergy.Data.Classes;

namespace Vamos_Sergy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestController : ControllerBase
    {
        private readonly IRepository<Quest> _questRepository;

        public QuestController(IRepository<Quest> questRepository)
        {
            _questRepository = questRepository;
        }
        [HttpGet]
        public IEnumerable<Quest> Get()
        {
            var quests = _questRepository.Read().ToArray();
            List<Quest> result = new List<Quest>();
            List<int> helperList = new List<int>();
            Random r = new Random();
            for (int i = 0; i<3;i++)
            {
                int index = r.Next(0,quests.Count());
                if (helperList.Contains(index))
                {
                    i--;
                    continue;
                }
                Quest q = quests[index];
                q.Exp = r.Next(100,1001);
                double gold = (r.NextDouble() +.1) * 10;
                q.Gold = Math.Round(gold,2);
            }
            return result;
        }

        [HttpPost]
        public void Create([FromBody] Quest item)
        {
            _questRepository.Create(item);
        }
        [HttpPut]
        public void Update([FromBody] Quest item)
        {
            _questRepository.Update(item);
        }

        [HttpDelete("id")]
        public void Delete(string id)
        {
            _questRepository.Delete(id);
        }
    }
}
