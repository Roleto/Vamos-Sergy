using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Vamos_Sergy.Data.Classes;
using Vamos_Sergy.Data.Interfaces;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Controllers
{
    public class MainController : Controller
    {
        private readonly IRepository<Hero> _heroRepo;
        private readonly IRepository<Item> _itemRepo;
        private readonly UserManager<SiteUser> _userManager;



        public MainController(IRepository<Hero> heroRepo, IRepository<Item> itemRepo, UserManager<SiteUser> userManager)
        {
            _heroRepo = heroRepo;
            _itemRepo = itemRepo;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var principal = this.User;
            var user = await _userManager.GetUserAsync(principal);
            var hero = _heroRepo.ReadFromOwner(user.Id);
            if (hero == null)
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
        public IActionResult CreateHero(Hero hero, string picturedata)
        {
            hero.OwnerId = _userManager.GetUserId(this.User);
            hero.GenerateStats(hero.Race);
            string name = picturedata.Split('\\')[2];
            IFormFile file;
            // wwwroot\\Images\\ClassImages\\warrior.jpg
            string url = "wwwroot\\Images\\ClassImages\\" + name;
            Image image = Image.FromFile(url);
            var ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var bytes = ms.ToArray();
            hero.Data = bytes;
            hero.ContentType = "image/jpeg";
            _heroRepo.Create(hero);
            return RedirectToAction(nameof(ViewHero));
        }
        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        [HttpGet]
        public IActionResult ViewHero()
        {
            var userId = _userManager.GetUserId(this.User);
            Hero hero = _heroRepo.ReadFromOwner(userId);
            return View(hero);
        }

        [HttpGet]
        public IActionResult HeroCard()
        {
            var userId = _userManager.GetUserId(this.User);
            Hero hero = _heroRepo.ReadFromOwner(userId);
            return View(hero);
        }

        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateItem(Item newItem, IFormFile picturedata)
        {
            using (var stream = picturedata.OpenReadStream())
            {
                byte[] buffer = new byte[picturedata.Length];
                stream.Read(buffer, 0, (int)picturedata.Length);
                //fájl módszer
                //string filename = newItem.Id + "." + picturedata.FileName.Split('.')[1];
                //newItem.ImageFileName = filename;
                //System.IO.File.WriteAllBytes(Path.Combine("wwwroot", "images", filename), buffer);

                newItem.Data = buffer;
                newItem.ContentType = picturedata.ContentType;
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            _itemRepo.Create(newItem);
            return RedirectToAction(nameof(ViewHero));
        }

        public async Task<IActionResult> GetImage(bool isUserImage = true)
        {
            var principal = this.User;
            var user = await _userManager.GetUserAsync(principal);
            if (isUserImage)
            {
                if (user.ContentType?.Length > 3)
                {
                    return new FileContentResult(user.Data, user.ContentType);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                var hero = _heroRepo.ReadFromOwner(user.Id);
                if (hero.ContentType?.Length > 3)
                {
                    var valami = new FileContentResult(hero.Data, hero.ContentType);
                    return new FileContentResult(hero.Data, hero.ContentType);
                }
                else
                {
                    return BadRequest();
                }
            }

        }

    }
}
