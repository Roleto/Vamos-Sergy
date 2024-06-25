using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Vamos_Sergy.Helpers;

namespace Vamos_Sergy.Models.Items
{
    public class Equipment
    {
        private string getPrice { get => $"{Price} g {Mushroom} m"; }

        private string GetStatsHtml()
        {
            string output = $"{Item.Description}";
            if (Item.Type == EquipmentEnum.Weapon)
            {
                output += $"<br>Damage: {MinDamage} - {MaxDamage}";
            }
            if (Str > 0)
            {
                output += $"<br>Stenght : {Str}";
            }
            if (Dex > 0)
            {
                output += $"<br>Dexterity : {Dex}";
            }
            if (Inte > 0)
            {
                output += $"<br>Inteligence : {Inte}";
            }
            if (Vit > 0)
            {
                output += $"<br>Vitality: {Vit}";
            }
            if (Luck > 0)
            {
                output += $"<br>Luck: {Luck}";
            }
            if (Item.Type == EquipmentEnum.Shield)
            {
                output += $"<br>Block : {Block}%";
            }
            if (Price != 0)
            {
                output += $"<br>Price : {this.getPrice}<br>";
            }
            return output;
        }

        [Key]
        public string Id { get; set; }

        public string ItemId { get; set; }
        [NotMapped]
        public string Name { get; set; }


        public int InventorySlot { get; set; }


        [Required]
        public bool IsEqueped { get; set; }

        [Required]
        [ForeignKey(nameof(Hero))]
        public string OwherId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Hero Owner { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Item Item { get; set; }

        [Required]
        public string Stats { get; set; }

        private double price;
        [NotMapped]
        public double Price { get => price; }

        private int mushroom;

        [NotMapped]
        public int Mushroom { get => mushroom; }

        [NotMapped]
        public int Str { get; set; }
        [NotMapped]
        public int Dex { get; set; }
        [NotMapped]
        public int Inte { get; set; }
        [NotMapped]
        public int Vit { get; set; }
        [NotMapped]
        public int Luck { get; set; }

        [NotMapped]
        public int MinDamage { get; set; }
        [NotMapped]
        public int MaxDamage { get; set; }
        [NotMapped]

        public int Block { get; set; }

        [NotMapped]
        public string StatForDisplay { get; set; }

        public string Url { get; set; }

        [NotMapped]
        public string Type { get => Item.Type.ToString();  }


        protected Random _random;

        public Equipment()
        {
            _random = new Random();
        }
        public Equipment(Item item)
        {
            _random = new Random();
            Id = Guid.NewGuid().ToString();
            ItemId = item.Id;
            Name = item.Name;
            Url = item.Url;
            Item = item;
            IsEqueped = false;
            Stats = "";
            GenerateStat();
        }
        public Equipment(Item item, string stats)
        {
            _random = new Random();
            Id = Guid.NewGuid().ToString();
            Item = item;
            Name = item.Name;
            ItemId = item.Id;
            Url = item.Url;
            IsEqueped = false;
            Stats = stats;
            Url = item.Url;
            SetStat();
            StatForDisplay = GetStatsHtml();
        }

        public Equipment(Shop shopItem)
        {
            _random = new Random();
            Id = Guid.NewGuid().ToString();
            Item = shopItem.Item;
            ItemId = shopItem.ItemId;
            Url = shopItem.Url;
            IsEqueped = false;
            Stats = shopItem.Stats;
            SetStat();
            Name = shopItem.Item.Name;
            StatForDisplay = GetStatsHtml();

        }       
        public bool CanBuy(double price, int mushroom)
        {
            if (Price <= price && Mushroom <= mushroom)
            {
                price = 0;
                return true;
            }
            return false;
        }
        public void SetStat()
        {
            string[] statsArray = Stats.Split(';');
            foreach (string stat in statsArray)
            {
                string[] statArray = stat.Split(":");
                switch (statArray[0])
                {
                    case "int":
                        Inte = int.Parse(statArray[1]);
                        break;
                    case "str":
                        Str = int.Parse(statArray[1]);
                        break;
                    case "dex":
                        Dex = int.Parse(statArray[1]);
                        break;
                    case "vit":
                        Vit = int.Parse(statArray[1]);
                        break;
                    case "luck":
                        Luck = int.Parse(statArray[1]);
                        break;
                    case "min":
                        MinDamage = int.Parse(statArray[1]);
                        break;
                    case "max":
                        MaxDamage = int.Parse(statArray[1]);
                        break;
                    case "block":
                        Block = int.Parse(statArray[1]);
                        break;
                    case "dmg":
                        string[] damages = statArray[1].Split("-");
                        MinDamage = int.Parse(damages[0]);
                        MaxDamage = int.Parse(damages[1]);
                        break;
                    case "Price":
                        string rawprices = statArray[1].Remove(statArray[1].Length - 1, 1);
                        char[] delimiterChars = { ':', 'g' };
                        string[] prices = rawprices.Split(delimiterChars);
                        price = double.Parse(prices[0]);
                        if (prices.Length > 1)
                        {
                            mushroom = int.Parse(prices[1]);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        protected virtual void GenerateStat()
        {
            switch (Item.RequiredClass)
            {
                default:
                case ClassEnum.Mage:
                    Inte = _random.Next(5, 11);
                    Stats += "int:" + Inte.ToString();
                    Stats += ";";
                    GenerateRandomStat();
                    break;
                case ClassEnum.Warrior:
                    Str = _random.Next(5, 11);
                    Stats += "str:" + Str.ToString();
                    Stats += ";";
                    GenerateRandomStat();
                    break;
                case ClassEnum.Ranger:
                    Dex = _random.Next(5, 11);
                    Stats += "dex:" + Dex.ToString();
                    Stats += ";";
                    GenerateRandomStat();
                    break;
                case ClassEnum.All:
                    GenerateRandomStat();
                    GenerateRandomStat();
                    break;
            }

            if (Item.Type == EquipmentEnum.Weapon)
            {
                MinDamage = _random.Next(2, 5);
                MaxDamage = _random.Next(5, 10);
                Stats += $"dmg:{MinDamage}-{MaxDamage};";
            }
            else if (Item.Type == EquipmentEnum.Shield)
            {
                Block = _random.Next(26);
                Stats += "block:" + Block.ToString();
            }
            price = Math.Round((_random.NextDouble() + 0.1) * 10, 2);
            int mush = _random.Next(0, 101);
            if (mush <= 5)
                mushroom = 15;
            else if (mush <= 20)
                mushroom = 1;
            else if (mush <= 25)
                mushroom = 2;
            StatForDisplay = GetStatsHtml();
        }

        protected void GenerateRandomStat()
        {
            // 0 = int 1 = str 2 = dex
            int[] ramdumValues = new int[5];
            int maxRandom = 0;
            switch (Item.RequiredClass)
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
                    Inte = _random.Next(1, 5);
                    Stats += "int:" + Inte.ToString();
                    Stats += ";";
                    break;
                case 1:
                    Str = _random.Next(1, 5);
                    Stats += "str:" + Str.ToString();
                    Stats += ";";
                    break;
                case 2:
                    Dex = _random.Next(1, 5);
                    Stats += "dex:" + Dex.ToString();
                    Stats += ";";
                    break;
                case 3:
                    Vit = _random.Next(1, 5);
                    Stats += "vit:" + Vit.ToString();
                    Stats += ";";
                    break;
                case 4:
                    Luck = _random.Next(1, 5);
                    Stats += "luck:" + Luck.ToString();
                    Stats += ";";
                    break;
            }
        }
        public int Attack(ClassEnum kast)
        {
            if (Item.Type != EquipmentEnum.Weapon)
                return 0;

            int damage = 0;
            double crit = 0;

            switch (Item.RequiredClass)
            {
                default:
                case ClassEnum.Mage:
                    damage = _random.Next(MinDamage + (Inte * 4), MaxDamage + 1 + (Inte * 4));
                    crit = damage * 0.4;
                    break;
                case ClassEnum.Warrior:
                    damage = _random.Next(MinDamage + Str, MaxDamage + 1 + Str);
                    crit = damage * 0.3;
                    break;
                case ClassEnum.Ranger:
                    damage = _random.Next(MinDamage + (Dex * 2), MaxDamage + 1 + (Dex * 2));
                    crit = damage * 0.2;
                    break;
            }
            if (Owner.Crit > _random.Next(0, 101))
                damage += (int)crit;

            return damage;
        }
        public override string ToString()
        {

            return GetStatsHtml();
        }
    }
}
