using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vamos_Sergy.Data.Classes;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;

namespace Vamos_Sergy.Controllers
{
    public class MainController : Controller
    {
        private readonly IRepository<Hero> _repo;
        private readonly UserManager<SiteUser> _userManager;

        public MainController(IRepository<Hero> heroRepository, UserManager<SiteUser> userManager)
        {
            this._repo = heroRepository;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var principal = this.User;
            var user = await _userManager.GetUserAsync(principal);
            var hero = _repo.ReadFromOwner(user.Id);
            if(hero == null)
                return RedirectToAction(nameof(CreateHero));
            //return RedirectToAction(nameof(HeroCard));
            return RedirectToAction(nameof(ViewHero));
            //return View();
        }

        [HttpGet]
        public IActionResult CreateHero()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateHero(Hero hero)
        {
            hero.OwnerId = _userManager.GetUserId(this.User);
            _repo.Create(hero);
            return RedirectToAction(nameof(ViewHero));
        }

        [HttpGet]
        public IActionResult ViewHero()
        {
            var userId = _userManager.GetUserId(this.User);
            Hero hero = _repo.ReadFromOwner(userId);
            return View(hero);
        }
       
        [HttpGet]
        public IActionResult HeroCard()
        {
            var userId = _userManager.GetUserId(this.User);
            Hero hero = _repo.ReadFromOwner(userId);
            return View(hero);
        }

        public async Task<IActionResult> GetImage()
        {
            var principal = this.User;
            var user = await _userManager.GetUserAsync(principal);
            if (user.ContentType?.Length > 3)
            {
                return new FileContentResult(user.Data, user.ContentType);
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
