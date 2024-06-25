using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
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
        private readonly IRepository<Item> _itemRepo;
        private readonly IRepository<Equipment> _equipmentRepo;
        private readonly IRepository<Hero> _heroRepo;
        private readonly IRepository<Shop> _shopRepo;
        private readonly IRepository<Quest> _questRepo;
        private readonly IRepository<Monster> _monsterRepo;

        private readonly UserManager<SiteUser> _userManager;
        //private static ShopViewModel _weaponshop;
        private static ArenaViewModel _arena;
        private static TavernViewModel _tavern;
        private static Hero _hero;
        private static string baseUri = "https://localhost:7284";
        private Hero SetHero(Hero hero)
        {
            setEqupment(hero);
            var quests = _questRepo.Read().ToArray();
            List<Quest> questList = new List<Quest>();
            string[] valami = hero.QuestIds.Split(';');
            foreach (var item in hero.QuestIds.Split(';'))
            {
                var q = _questRepo.Read(item);
                if (q != null)
                    questList.Add(q);
            }
            hero.SetQuest(questList);
            return hero;
        }
        private void RefreshHero(Hero hero)
        {

            ViewData["gold"] = hero.Gold;
            ViewData["mushroom"] = hero.Mushroom;
        }
        private byte[] imageToByteArray(System.Drawing.Image imageIn, ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, format);
            return ms.ToArray();
        }

        private void SetShop(Hero hero)
        {
            Random r = new Random();
            var items = _itemRepo.Read().ToArray();
            int weaponCounter = 0;
            int magicCounter = 0;
            do
            {
                var item = items[r.Next(items.Length)];
                if (magicCounter < 6 && (item.Type == EquipmentEnum.Necklace || item.Type == EquipmentEnum.Ring || item.Type == EquipmentEnum.Misc))
                {
                    Shop shop = new Shop(item, hero, "magic", magicCounter);
                    //var e = hero.Shops.FirstOrDefault(x => x.Name == shop.Name);
                    //if (e == null)
                    //{
                    //}
                    hero.Shops.Add(shop);
                    _shopRepo.Create(shop);
                    magicCounter++;
                }
                else if (weaponCounter < 6 && item.RequiredClass == hero.Kast)
                {
                    Shop shop = new Shop(item, hero, "weapon", weaponCounter);
                    //var e = hero.Shops.FirstOrDefault(x => x.Name == shop.Name);
                    //if (e == null)
                    //{
                    //}
                    hero.Shops.Add(shop);
                    _shopRepo.Create(shop);
                    weaponCounter++;

                }
            } while (weaponCounter + magicCounter < 12);
        }
        private void setEqupment(Hero hero)
        {
            foreach (Equipment item in hero.Equipments)
            {
                if(item.Item == null)
                {
                    item.Item = _itemRepo.Read(item.ItemId);
                }
                Equipment eq = new Equipment(item.Item, item.Stats);
                eq.Id = item.Id;
                eq.IsEqueped = item.IsEqueped;
                eq.InventorySlot = item.InventorySlot;
                if (eq.IsEqueped)
                {
                    hero.Equipment[eq.Item.Type] = eq;
                }
                else
                {
                    hero.Inventory[eq.InventorySlot] = eq;
                }
            }
        }
        public MainController(IRepository<Item> itemRepo, IRepository<Equipment> equipmentRepo, IRepository<Hero> heroRepo, IRepository<Shop> shopRepo, IRepository<Quest> questRepo, IRepository<Monster> monsterRepo, UserManager<SiteUser> userManager)
        {
            _itemRepo = itemRepo;
            _equipmentRepo = equipmentRepo;
            _heroRepo = heroRepo;
            _shopRepo = shopRepo;
            _questRepo = questRepo;
            _monsterRepo = monsterRepo;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var principal = this.User;
            var _siteUser = await _userManager.GetUserAsync(principal);
            _hero = _heroRepo.ReadFromOwner(_siteUser.Id);
            if (_hero == null)
                return RedirectToAction(nameof(CreateHero));
            if (_hero.Shops.Count == 0)
            {
                SetShop(_hero);
            }
            //_weaponshop = new ShopViewModel(_hero, "D:\\Egyetem\\prog5\\FF\\Vamos&Sergy\\Vamos&Sergy\\wwwroot\\Images\\Backgrounds\\weaponshop.jpg");
            setEqupment(_hero);
            _arena = new ArenaViewModel(_heroRepo.Read().ToList(), _hero);
            _tavern = new TavernViewModel(_hero);
            this.RefreshHero(_hero);
            return RedirectToAction(nameof(ViewHero));
        }
        #region ApiCalls

        [HttpGet]
        public async Task<IActionResult> ViewHero()
        {
            var hero = _heroRepo.Read(_hero.Id);
            this.RefreshHero(hero);
            setEqupment(hero);
            return View(hero);
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
            var quests = _questRepo.Read().ToArray();
            List<Quest> questList = new List<Quest>();
            Random r = new Random();
            do
            {
                int random = r.Next(quests.Length);
                if (questList.FirstOrDefault(x => x.Id == quests[random].Id) == null)
                {
                    questList.Add(quests[random]);
                }
            } while (questList.Count != 3);
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
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateItem()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CreateItem(Item newItem, IFormFile picturedata, IFormFile secondaryPicturedata)
        {
            //nedd updtate
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

        [HttpGet]
        public IActionResult EquipItem(int index, string name)
        {
            Equipment e = _hero.Inventory[index];
            if (e == null)
                return RedirectToAction(name);
            //Item i = _itemRepo.Read(e.ItemId);
            //if (i == null)
            //    return RedirectToAction(name);
            e.IsEqueped = true;
            //e.Name = i.Name;
            //e.Description = i.Description;
            //e.Type = i.Type;
            //e.RequiredClass = i.RequiredClass;
            Equipment old = _hero.Equip(e);
            _equipmentRepo.Update(e);
            if (old != null)
                _equipmentRepo.Update(old);

            if (_hero.Inventory[e.InventorySlot] != null && old == null)
            {
                _hero.Inventory[e.InventorySlot] = null;
            }
            return RedirectToAction(name);
        }

        [HttpGet]
        public IActionResult UnEquipItem(EquipmentEnum equipment, string name)
        {

            var e = _hero.UnEquip(equipment);
            if(e != null)
            {
                _equipmentRepo.Update(e);
            }
            return RedirectToAction(name);
        }

        [HttpGet]
        public IActionResult CreateMonster()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMonster(Monster monster, IFormFile picturedata)
        {
            using (var stream = picturedata.OpenReadStream())
            {
                byte[] buffer = new byte[picturedata.Length];
                stream.Read(buffer, 0, (int)picturedata.Length);
                monster.Data = buffer;
                monster.ContentType = picturedata.ContentType;
            }
            _monsterRepo.Create(monster);
            return RedirectToAction(nameof(CreateMonster));
        }

        #endregion
        [HttpGet]
        public IActionResult Tavern()
        {
            var hero = _heroRepo.Read(_hero.Id);
            this.RefreshHero(hero);
            _tavern.Hero = hero;
            return View(_tavern);
        }

        [HttpPost]
        public IActionResult Tavern(int selectedQuest, bool questEnded, int beerCount, int goldInput, int mushroomInput)
        {
            if (questEnded)
            {

            }
            else
            {
                Random r = new Random();
                var m = _monsterRepo.Read().ToArray();
                Quest q = _hero.Quest[selectedQuest];
                _hero.Gold = goldInput;
                _hero.Mushroom = mushroomInput;
                _hero.SelectedQuest = selectedQuest;
                _hero.Quest[selectedQuest].Enemy = m[r.Next(0, m.Length)];
                _hero.QuestStarted = DateTime.Now;
                _hero.HeroState = HeroStateEnum.OnAdvanture;
                _hero.BeerCount = beerCount;
                _hero.Adventure -= q.Time / 60;
            }
            _heroRepo.Update(_hero);
            // kell egy void majd a viewmodelleket is frissiteni
            return RedirectToAction(nameof(Tavern));
        }

        [HttpGet]
        public IActionResult Arena()
        {
            var hero = _heroRepo.Read(_hero.Id);
            this.RefreshHero(hero);
            _arena.ChangeFight();
            return View(_arena);
        }

        [HttpPost]
        public IActionResult Arena(bool win)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Stable()
        {
            var hero = _heroRepo.Read(_hero.Id);
            this.RefreshHero(hero);
            return View(hero);
        }

        [HttpPost]
        public IActionResult Stable(MountEnum mount)
        {
            ;
            return RedirectToAction(nameof(Stable));
        }

        [HttpGet]
        public async Task<IActionResult> WeaponShop()
        {
            var hero = SetHero(_heroRepo.Read(_hero.Id));
            this.RefreshHero(hero);
            ShopViewModel shop_vm = new ShopViewModel(hero,_itemRepo.Read().ToList(), "weapon");
            return View(shop_vm);
        }

        [HttpGet]
        public async Task<IActionResult> BuyWeapon(int index)
        {
            var hero = _heroRepo.Read(_hero.Id);

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
            return View("WeaponShop");

        }

        public async Task<IActionResult> GetImage(bool isUserImage = true, string id = "")
        {
            if (id != "")
            {
                var hero = _heroRepo.Read(id);
                if (hero == null)
                {
                    //var monster =_mosterRepo.Read(id);
                    //if (monster.ContentType?.Length > 3)
                    //{
                    //    return new FileContentResult(monster.Data, monster.ContentType);
                    //}
                    //else
                    //{
                    //    return BadRequest();
                    //}
                }
                if (hero.ContentType?.Length > 3)
                {
                    return new FileContentResult(hero.Data, hero.ContentType);
                }
                else
                {
                    return BadRequest();
                }
            }
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

        public async Task<IActionResult> GetCharacterImg(bool isUserImage = true)
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
