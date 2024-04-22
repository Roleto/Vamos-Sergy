using Vamos_Sergy.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Vamos_Sergy.Models
{
    public class Hero
    {
        #region Base props

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(24)]
        [ShowTable]
        public string Name { get; set; }

        [ShowTable]
        public int Exp { get; set; }

        [Required]
        [ShowTable]

        public int Level { get; set; }

        [ShowTable]
        public int Gold { get; set; }
        [ShowTable]
        public int Mushroom { get; set; }

        [NotMapped]
        public double GetCurrentXpPercentage { get => (Exp / ((double)Level * 256)) * 100; }

        [Required]
        [ShowTable]
        public ClassEnum Kast { get; set; }

        [Required]
        [ShowTable]
        public RaceEnum Race { get; set; }

        [ShowTable]
        public bool HasMount { get; set; }

        #endregion
        #region stat props

        private int str;

        //[NotNull]
        public int Str
        {
            get { return str; }
            set { str = value; }
        }
        [NotMapped]
        public int Defence { get => str / 2; }

        private int dex;

        //[NotNull]
        public int Dex
        {
            get { return dex; }
            set { dex = value; }
        }
        [NotMapped]
        public int Evasion { get => dex / 2; }

        private int inte;

        //[NotNull]
        public int Inte
        {
            get { return inte; }
            set { inte = value; }
        }
        [NotMapped]
        public int Resistance { get => inte / 2; }

        private int vit;

        //[NotNull]
        public int Vit
        {
            get { return vit; }
            set { vit = value; }
        }

        [NotMapped]
        public int Hp { get => vit * 2 * (this.Level + 1); }



        private double luck;

        //[NotNull]
        public double Luck
        {
            get { return luck; }
            set { luck = value; }
        }

        [NotMapped]
        public int Damage { get => 1000; }


        [NotMapped]
        [Range(0, 50)]
        //Enemy level-re kell majd ki venni
        public double Crit
        {
            get
            {
                double Crit = (luck * 5) / ((double)this.Level * 2);
                if (Crit > 50)
                    return 50;
                return Math.Round(Crit,2);
            }
        }

        [NotMapped]
        public int Armor { get => 120; }
        private int MaxArmor
        {
            get
            {
                switch (Kast)
                {
                    case ClassEnum.Mage:
                        return 10;
                    case ClassEnum.Warrior:
                        return 50;
                    case ClassEnum.Ranger:
                        return 25;
                }
                return 10;
            }
        }

        [NotMapped]
        public int DamageReduction
        {
            get
            {
                int armor = Armor / Level;
                if (armor > MaxArmor)
                    return MaxArmor;
                return armor;
            }
        }

        #endregion
        #region inventory props

        [NotMapped]
        public List<Item> Inventory { get; set; }
        [NotMapped]
        public int MaxInvetory { get; set; }

        [AllowNull]
        public virtual Item Helmet { get; set; }
        [AllowNull]
        public virtual Item BodyArmor { get; set; }
        [AllowNull]
        public virtual Item Gloves { get; set; }
        [AllowNull]
        public virtual Item Boots { get; set; }
        [AllowNull]
        public virtual Weapon Weapon { get; set; }
        [AllowNull]
        public virtual Item Shield { get; set; }
        [AllowNull]
        public virtual Item Necklace { get; set; }
        [AllowNull]
        public virtual Item Belt{ get; set; }
        [AllowNull]
        public virtual Item Ring { get; set; }
        [AllowNull]
        public virtual Item Misc { get; set; }

        #endregion

        public string OwnerId { get; set; }

        [NotMapped]
        public virtual SiteUser Owner { get; set; }

        [NotMapped]
        public string ContentType { get; set; }

        [NotMapped]
        public byte[] Data { get; set; }
        public Hero()
        {
            Id = Guid.NewGuid().ToString();
            MaxInvetory = 5;
        }
        public void GenerateStats(RaceEnum race)
        {
            HasMount = false;
            SetStats(race);
            Inventory = new List<Item>();
            MaxInvetory = 5;
        }

        private void SetStats(RaceEnum race)
        {
            Str = 5;
            Dex = 5;
            Inte = 5;
            Vit = 5;
            Luck = 5;
            switch (race)
            {
                default:
                case RaceEnum.Human:
                    break;
                case RaceEnum.Elf:
                    Inte += 1;
                    Vit -= 1;
                    break;
                case RaceEnum.Darkelf:
                    Inte += 2;
                    Str -= 1;
                    break;
                case RaceEnum.Dwarf:
                    Str += 1;
                    Inte -= 1;
                    break;
                case RaceEnum.Deamon:
                    Inte += 1;
                    Vit += 1;
                    break;
            }
        }
        //public Hero(string? id)
        //{
        //    if(id == null)
        //        Id = Guid.NewGuid().ToString();
        //    else 
        //        Id = id;
        //}

    }
}
