using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Vamos_Sergy.Helpers;
using System;
using System.Text.Json.Serialization;

namespace Vamos_Sergy.Models.Items
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SimpleId { get; set; }

        [ForeignKey(nameof(Hero))]
        public string OwnerId { get; set; }
        public string ItemId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public Item Item { get; set; }
        [NotMapped]
        public virtual Hero Owner { get; set; }

        public string ShopType { get; set; }

        [Required]
        [StringLength(24)]
        [ShowTable]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [ShowTable]
        public string Description { get; set; }

        public EquipmentEnum Type { get; set; }
        public ClassEnum RequiredClass { get; set; }

        public string Url { get; set; }

        public string Stats { get; set; }

        public int ShopSlot { get; set; }

        private Random _random;
        public Shop()
        {
            _random = new Random();
        }
        public Shop(Item item,Hero hero, string shopName,int shopSlot)
        {
            _random = new Random();
            Id = Guid.NewGuid().ToString();
            Owner = hero;
            OwnerId = hero.Id;
            Item = item;
            ItemId = item.Id;
            Name = item.Name;
            Description = item.Description;
            Type = item.Type;
            ShopType = shopName;
            Url = item.Url;
            RequiredClass = item.RequiredClass;
            Stats = "";
            ShopSlot = shopSlot;
            GenerateStat();
        }

        protected virtual void GenerateStat()
        {
            switch (RequiredClass)
            {
                default:
                case ClassEnum.Mage:
                    Stats += "int:" + _random.Next(5, 11).ToString();
                    Stats += ";";
                    GenerateRandomStat();
                    break;
                case ClassEnum.Warrior:
                    Stats += "str:" + _random.Next(5, 11).ToString();
                    Stats += ";";
                    GenerateRandomStat();
                    break;
                case ClassEnum.Ranger:
                    Stats += "dex:" + _random.Next(5, 11).ToString();
                    Stats += ";";
                    GenerateRandomStat();
                    break;
                case ClassEnum.All:
                    GenerateRandomStat();
                    GenerateRandomStat();
                    break;
            }

            if (Type == EquipmentEnum.Weapon)
            {
                int MinDamage = _random.Next(2, 5);
                int MaxDamage = _random.Next(5, 10);
                Stats += $"dmg:{MinDamage}-{MaxDamage};";
            }
            else if (Type == EquipmentEnum.Shield)
            {
                int Block = _random.Next(26);
                Stats += "block:" + Block.ToString() + ";";
            }
            double price = Math.Round((_random.NextDouble() + 0.1) * 10, 2);
            Stats += $"Price:{price.ToString()}g";
            int mush = _random.Next(0, 101);
            if (mush <= 5)
                Stats += "15m";
            else if (mush <= 20)
                Stats += "1m";
            else if (mush <= 25)
                Stats += "2m";
        }

        protected void GenerateRandomStat()
        {
            // 0 = int 1 = str 2 = dex
            int[] ramdumValues = new int[5];
            int maxRandom = 0;
            switch (RequiredClass)
            {
                default:
                case ClassEnum.Mage:
                    ramdumValues[0] = 1;
                    ramdumValues[1] = 2;
                    ramdumValues[2] = 3;
                    ramdumValues[3] = 4;
                    ramdumValues[4] = 5;
                    maxRandom = 4;
                    break;
                case ClassEnum.Warrior:
                    ramdumValues[0] = 0;
                    ramdumValues[1] = 2;
                    ramdumValues[2] = 3;
                    ramdumValues[3] = 4;
                    ramdumValues[4] = 5;
                    maxRandom = 4;
                    break;
                case ClassEnum.Ranger:
                    ramdumValues[0] = 1;
                    ramdumValues[1] = 0;
                    ramdumValues[2] = 3;
                    ramdumValues[3] = 4;
                    ramdumValues[4] = 5;
                    maxRandom = 4;
                    break;
                case ClassEnum.All:
                    ramdumValues[0] = 0;
                    ramdumValues[1] = 1;
                    ramdumValues[2] = 2;
                    ramdumValues[3] = 3;
                    ramdumValues[4] = 4;
                    maxRandom = 5;
                    break;
            }
            int n = _random.Next(0, maxRandom);
            switch (ramdumValues[n])
            {
                default:
                case 0:
                    Stats += "int:" + _random.Next(1, 5).ToString();
                    Stats += ";";
                    break;
                case 1:
                    Stats += "str:" + _random.Next(1, 5).ToString();
                    Stats += ";";
                    break;
                case 2:
                    Stats += "dex:" + _random.Next(1, 5).ToString();
                    Stats += ";";
                    break;
                case 3:
                    Stats += "vit:" + _random.Next(1, 5).ToString();
                    Stats += ";";
                    break;
                case 4:
                    Stats += "luck:" + _random.Next(1, 5).ToString();
                    Stats += ";";
                    break;
            }
        }
    }
}
