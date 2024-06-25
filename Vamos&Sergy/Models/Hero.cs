using Vamos_Sergy.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Vamos_Sergy.Models.Items;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vamos_Sergy.Migrations;

namespace Vamos_Sergy.Models
{
    public class Hero : ICharacter
    {
        #region Base props

        [Key]
        public string Id { get; set; }

        [StringLength(24)]
        [ShowTable]
        public string Name { get; set; }
        public string GuildId { get; set; }
        public string GuildName { get; set; }

        [ShowTable]
        public int Exp { get; set; }

        [ShowTable]

        public int Level { get; set; }

        [ShowTable]
        [Range(0,100)]
        public double Adventure{ get; set; }

        public HeroStateEnum HeroState { get; set; }

        [ShowTable]
        public double Gold { get; set; }
        [ShowTable]
        public int Mushroom { get; set; }
        public int BeerCount { get; set; }

        [NotMapped]
        [JsonIgnore]
        public double GetCurrentXpPercentage { get => (Exp / (double)MaxXp); }

        [NotMapped]
        [JsonIgnore]
        public int MaxXp { get => (Level * 15) + (Level * 100); }

        [Required]
        [ShowTable]
        [JsonIgnore]
        public ClassEnum Kast { get; set; }

        [ShowTable]
        [JsonIgnore]
        public RaceEnum Race { get; set; }

        [ShowTable]
        [JsonIgnore]
        public MountEnum Mount { get; set; }

        [JsonIgnore]
        public DateTime? MountEndDate { get; set; }

        [JsonIgnore]
        public int Honor { get; set; }

        public string WeaponShop { get; set; }
        public string MagicShop { get; set; }
        public string QuestIds{ get; set; }

        [AllowNull]
        [NotMapped]
        [JsonIgnore]
        public List<Quest> Quest { get; set; }

        [AllowNull]
        public int? SelectedQuest { get; set; }

        [JsonIgnore]
        [NotMapped]
        public Quest? GetSelectedQuest { get => (Quest != null && SelectedQuest != null) ? Quest[SelectedQuest.Value] : null; }


        [AllowNull]
        public DateTime? QuestStarted { get; set; }

        #endregion
        #region stat props

        private int str;

        public int Str
        {
            get
            {
                int realStr = this.str;
                foreach (var item in Equipment)
                {
                    if (item.Value != null)
                        realStr += item.Value.Str;
                }
                return realStr;
            }
            set { str = value; }
        }
        [NotMapped]
        public int Defence { get => Str / 2; }

        private int dex;

        public int Dex
        {
            get
            {
                int realDex = this.dex;
                foreach (var item in Equipment)
                {
                    if (item.Value != null)
                        realDex += item.Value.Dex;
                }
                return realDex;
            }
            set { dex = value; }
        }
        [NotMapped]
        public int Evasion { get => Dex / 2; }

        private int inte;

        public int Inte
        {
            get
            {
                int realInt = this.inte;
                foreach (var item in Equipment)
                {
                    if (item.Value != null)
                        realInt += item.Value.Inte;
                }
                return realInt;
            }
            set { inte = value; }
        }
        [NotMapped]
        public int Resistance { get => Inte / 2; }

        private int vit;

        public int Vit
        {
            get
            {
                int realVit = this.vit;
                foreach (var item in Equipment)
                {
                    if (item.Value != null)
                        realVit += item.Value.Vit;
                }
                return realVit;
            }
            set { vit = value; }
        }

        [NotMapped]
        public int Hp { get => Vit * 2 * (this.Level + 1); }

        [NotMapped]
        public int CurrentHp { get; set; }


        private double luck;

        public double Luck
        {
            get
            {
                double realLuck = this.luck;
                foreach (var item in Equipment)
                {
                    if (item.Value != null)
                        realLuck += item.Value.Luck;
                }
                return realLuck;
            }
            set { luck = value; }
        }

        [NotMapped]
        public int Damage
        {
            get
            {
                switch (Kast)
                {
                    default:
                    case ClassEnum.Mage:
                        return weaponDmg() * (1 + Inte / 10);
                    case ClassEnum.Warrior:
                        return weaponDmg() * (1 + Str / 10);
                    case ClassEnum.Ranger:
                        return weaponDmg() * (1 + Dex / 10);
                }
            }
        }

        private int weaponDmg()
        {
            if (Equipment.ContainsKey(EquipmentEnum.Weapon))
            {
                Weapon weapon = Equipment[EquipmentEnum.Weapon] as Weapon;
                if (weapon != null)
                    return (weapon.MinDamage + weapon.MaxDamage) / 2;
            }
            return 1;
        }

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
                return Math.Round(Crit, 2);
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
        [JsonIgnore]
        public virtual Dictionary<int, Equipment> Inventory { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Dictionary<EquipmentEnum, Equipment> Equipment { get; set; }

        [Range(0, 25)]
        public int MaxInvetory { get; set; }


        #endregion

        public string OwnerId { get; set; }

        [NotMapped]
        [AllowNull]
        [JsonIgnore]
        public virtual SiteUser Owner { get; set; }

        [AllowNull]
        [JsonIgnore]
        public string ContentType { get; set; }
        [AllowNull]
        [JsonIgnore]
        public byte[] Data { get; set; }

        [NotMapped]
        public virtual ICollection<Equipment> Equipments { get; set; }
        [NotMapped]
        public virtual ICollection<Shop> Shops{ get; set; }

        [NotMapped]
        [JsonIgnore]
        public int InvIndex
        {
            get
            {
                int output = MaxInvetory;

                for (int i = 0; i < MaxInvetory; i++)
                {
                    if (Inventory.ContainsKey(i))
                        output--;
                }

                return output;
            }
        }
        [JsonIgnore]
        public int GetFirsNull
        {
            get
            {
                int i = 0;
                for (i = 0; i < Inventory.Count; i++)
                {
                    if (!Inventory.ContainsKey(i) || Inventory[i] == null)
                    {
                        break;
                    }
                }
                return i;
            }
        }


        public Hero()
        {
            Id = Guid.NewGuid().ToString();
            Inventory = new Dictionary<int, Equipment>();
            Equipment = new Dictionary<EquipmentEnum, Equipment>();
            Adventure = 100;
            Honor= 100;
            HeroState = HeroStateEnum.Free;
            Shops = new HashSet<Shop>();
            Equipments = new HashSet<Equipment>();

        }
        public void GenerateStats(RaceEnum race)
        {
            Mount = MountEnum.None;
            MaxInvetory = 5;
            SetStats(race);
        }
        public void SetEquipment()
        {
            foreach (Equipment item in Equipments.ToList())
            {
                Equipment eq = new Equipment(item.Item, item.Stats);
                eq.Id = item.Id;
                eq.IsEqueped = item.IsEqueped;
                eq.InventorySlot = item.InventorySlot;
                if (eq.IsEqueped)
                {
                    Equipment[eq.Item.Type] = eq;
                }
                else
                {
                    Inventory[eq.InventorySlot] = eq;
                }
            }
        }

        public void SetQuest(List<Quest> quests)
        {
            Quest = quests;
            string quest = "";
            foreach (var item in quests)
            {
                quest += $"{item.Id};";
            }
            QuestIds = quest.Remove(quest.Length - 1,1);
            ;
        }

        public bool CanBuy(Equipment e)
        {
            if(e.Price <= Gold && e.Mushroom <= Mushroom)
            {
                Gold -= e.Price;
                Mushroom -= e.Mushroom;
                return true;
            }
            return false;
        }
        public Equipment Equip(Equipment item)
        {
            if (Equipment.ContainsKey(item.Item.Type) && Equipment[item.Item.Type] != null)
            {
                var old = Equipment[item.Item.Type];
                Equipment[item.Item.Type] = item;
                old.IsEqueped = false;
                old.InventorySlot = item.InventorySlot;
                Inventory[old.InventorySlot] = old;
                return old;
            }
            else
            {
                Equipment[item.Item.Type] = item;
                Inventory[item.InventorySlot] = null;
                return null;
            }
        }
        public Equipment? UnEquip(EquipmentEnum equipment)
        {
            if (Equipment.ContainsKey(equipment) && InvIndex <= MaxInvetory)
            {
                var e = Equipment[equipment];
                e.InventorySlot = GetFirsNull;
                e.IsEqueped = false;
                Inventory[e.InventorySlot] = e;
                Equipment[equipment] = null;
                return e;
            }
            return null;
        }

       

        public double EnverimentalBonus()
        {
            return (15 * Level) * .25;
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


    }
}
