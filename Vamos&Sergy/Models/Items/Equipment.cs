using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vamos_Sergy.Helpers;

namespace Vamos_Sergy.Models.Items
{
    public class Equipment
    {
        [Key]
        public string Id { get; set; }

        public string ItemId{ get; set; }     

        [NotMapped]
        [ShowTable]
        public string Name { get; set; }

        [NotMapped]
        [ShowTable]
        public string Description { get; set; }
        public int InventorySlot { get; set; }

        [NotMapped]
        public EquipmentEnum Type { get; set; }

        [NotMapped]
        public ClassEnum RequiredClass { get; set; }
        
        [Required]
        public bool IsEqueped { get; set; }

        [Required]
        public string OwherId { get; set; }

        [NotMapped]
        public virtual Hero Owner { get; set; }

        [Required]
        public string Stats { get; set; }


        [NotMapped]
        public double Price { get; }

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

        protected Random _random;

        public Equipment()
        {
            _random = new Random();
        }
        public Equipment(Item item)
        {
            _random = new Random();
            Id=Guid.NewGuid().ToString();
            ItemId = item.Id;
            Name = item.Name;
            Description = item.Description;
            Type = item.Type;
            RequiredClass = item.RequiredClass;
            IsEqueped = false;
            Price = (_random.NextDouble() + 0.1) * 10;
            Stats = "";
            GenerateStat();
        }
        public void SetStat()
        {
            string[] statsArray =  Stats.Split(';');
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
                    default:
                        break;
                }
            }
        }
        protected virtual void GenerateStat()
        {
            switch (RequiredClass)
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
    }
}
