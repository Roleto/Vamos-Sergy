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
using Vamos_Sergy.Migrations;
using Vamos_Sergy.Models;
using Vamos_Sergy.Models.Items;
using Vamos_Sergy.ViewModels;

namespace Vamos_Sergy.Controllers
{
    public class MainController : Controller
    {
        private readonly IRepository<Hero> _heroRepo;
        private readonly IRepository<Item> _itemRepo;
        private readonly IRepository<Equipment> _equipmentRepo;
        private readonly UserManager<SiteUser> _userManager;
        private static ViewModel _viewModel;


        public MainController(IRepository<Hero> heroRepo, IRepository<Item> itemRepo, IRepository<Equipment> equipmentRepo, UserManager<SiteUser> userManager)
        {
            _heroRepo = heroRepo;
            _itemRepo = itemRepo;
            _equipmentRepo = equipmentRepo;
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
            var eq = _equipmentRepo.Read().Where(x => x.OwherId == hero.Id);
            List<Equipment> equipmentList = new List<Equipment>();
            foreach (var item in eq)
            {
                //itt kell megadni a többit
                Item i = _itemRepo.Read(item.ItemId);
                item.SetStat(i);

                equipmentList.Add(item);
            }
            hero.GetEqupments(equipmentList);
            _viewModel = new ViewModel(hero, _itemRepo.Read().ToList());
            ViewData["gold"] = 1;
            ViewData["mushroom"] = 0;
            return RedirectToAction(nameof(ViewHero));
        }



        [HttpGet]
        public IActionResult CreateHero()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Proba()
        {
            List<string> Messages = new List<string>()
        {
            "Turns out NASA can't even improve on duct tape... Duct tape is magic and should be worshipped.",
            "How come Aquaman can control whales? They're mammals! Makes no sense.",
            "As with most of life's problems, this one can be solved by a box of pure radiation."
        };
            return View(Messages);
        }

        [HttpGet]
        public async Task<IActionResult> WeaponShop()
        {
            ViewData["gold"] = 1;
            ViewData["mushroom"] = 0;
            return View(_viewModel);
        }

        [HttpGet]
        public IActionResult BuyWeapon(int index)
        {
            if (_viewModel.Hero.InvIndex < _viewModel.Hero.MaxInvetory)
            {
                Equipment e = _viewModel.Buy(index);
                e.OwherId = _viewModel.Hero.Id;
                e.InventorySlot = _viewModel.Hero.GetFirsNull;
                _equipmentRepo.Create(e);
                _viewModel.Hero.Inventory[e.InventorySlot] = e;
            }
            return RedirectToAction(nameof(WeaponShop));
        }

        [HttpGet]
        public IActionResult EquipItem(int index, string name)
        {
            if (index <= _viewModel.Hero.InvIndex)
            {
                Equipment e = _viewModel.Hero.Inventory[index];
                Item i = _itemRepo.Read(e.ItemId);
                if (e == null || i == null)
                    return RedirectToAction(name);
                e.IsEqueped = true;
                e.Name = i.Name;
                e.Description = i.Description;
                e.Type = i.Type;
                e.RequiredClass = i.RequiredClass;
                Equipment old = _viewModel.Hero.Equip(e);
                _equipmentRepo.Update(e);
                if (old != null)
                    _equipmentRepo.Update(old);
            }
            return RedirectToAction(name);
        }

        [HttpGet]
        public IActionResult UnEquipItem(EquipmentEnum equipment, string name)
        {
            _viewModel.Hero.UnEquip(equipment);
            return RedirectToAction(name);
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
            if (hero.Kast == ClassEnum.Warrior)
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            else
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            var bytes = ms.ToArray();
            hero.Data = bytes;
            hero.ContentType = "image/jpeg";
            _heroRepo.Create(hero);
            return RedirectToAction(nameof(ViewHero));
        }
        private byte[] imageToByteArray(System.Drawing.Image imageIn, ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, format);
            return ms.ToArray();
        }

        [HttpGet]
        public IActionResult ViewHero()
        {
            //var userId = _userManager.GetUserId(this.User);
            //Hero hero = _heroRepo.ReadFromOwner(userId);
            //f48378fc-0d28-4f9f-b37a-615a3cae98c8
            //var equipments = _equipmentRepo.Read().Where(x => x.OwherId == hero.Id);
            //if (equipments != null)
            //{
            //    hero.GetEqupments(equipments.ToList()) ;
            //}
            return View(_viewModel.Hero);
        }

        [HttpGet]
        public ContentResult HeroCard()
        {
            //< div class="container">
            //                    <div class="row">
            //                        <div class="col col-3 ">
            //                            <img class="profilPic" src="@Url.Action("GetProfilImage", "Main")" class="card-img-top" alt="...">
            //                        </div>
            //                        <div class="col col-9">
            //                            <div class="container text-center">
            //                                <div class="row">
            //                                    <div class="col">
            //                                        <small>Gold: 100</small>
            //                                        <small>Mushrom: 5</small>
            //                                    </div>
            //                                </div>
            //                            </div>
            //                        </div>
            //                    </div>
            //                </div>
            return new ContentResult
            {
                ContentType = "text/html",
                Content = "<div class=\"container\"><div class=\"row\"><div class=\"col col-3 \"><img class=\"profilPic\" src=\"@Url.Action(\"GetProfilImage\", \"Main\")\" class=\"card-img-top\" alt=\"...\"></div><div class=\"col col-9\"><div class=\"container text-center\"><div class=\"row\"><div class=\"col\">" +
                "<small>Gold: 100</small>" +
                "<small>Mushrom: 5</small></div></div></div></div></div></div>"
            };
        }

        //[HttpGet]
        //public IActionResult HeroCard()
        //{
        //    var userId = _userManager.GetUserId(this.User);
        //    Hero hero = _heroRepo.ReadFromOwner(userId);
        //    return View(hero);
        //}

        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateItem(Item newItem, IFormFile picturedata, IFormFile secondaryPicturedata)
        {
            newItem.Id = Guid.NewGuid().ToString();
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
            if (secondaryPicturedata != null)
            {
                using (var stream = secondaryPicturedata.OpenReadStream())
                {
                    byte[] buffer = new byte[secondaryPicturedata.Length];
                    stream.Read(buffer, 0, (int)secondaryPicturedata.Length);
                    newItem.SecondaryData = buffer;
                    newItem.SecondaryContentType = secondaryPicturedata.ContentType;
                }
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
                    return new FileContentResult(hero.Data, hero.ContentType);
                }
                else
                {
                    return BadRequest();
                }
            }

        }

        public async Task<IActionResult> GetProfilImage()
        {
            var principal = this.User;
            var user = await _userManager.GetUserAsync(principal);
            if (user != null && user.ContentType?.Length > 3)
            {
                return new FileContentResult(user.Data, user.ContentType);
            }
            else
            {
                Image image = Image.FromFile("D:\\Egyetem\\prog5\\FF\\Vamos&Sergy\\Vamos&Sergy\\wwwroot\\Images\\NullImages\\profil.png");
                return new FileContentResult(imageToByteArray(image, ImageFormat.Png), "image/png");
            }
        }

        public async Task<IActionResult> GetShopImage(string itemId)
        {
            var item = _itemRepo.Read(itemId);
            if (item.ContentType?.Length > 3)
            {
                return new FileContentResult(item.Data, item.ContentType);
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> GetInventoryImage(string id, bool secondary = false)
        {
            var item = _itemRepo.Read(id);
            if (secondary)
            {
                if (item != null && item.SecondaryContentType?.Length > 3)
                {
                    return new FileContentResult(item.SecondaryData, item.SecondaryContentType);
                }
                else
                {
                    return BadRequest();
                }
            }
            if (item != null && item.ContentType?.Length > 3)
            {
                return new FileContentResult(item.Data, item.ContentType);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
