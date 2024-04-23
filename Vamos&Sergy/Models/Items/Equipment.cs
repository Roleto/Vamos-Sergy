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
        public int Price { get => 1; }

        [NotMapped]
        [Range(0, 24)]
        public int ItemSlot { get; set; }

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

        protected virtual void GenerateStat()
        {
            switch (RequiredClass)
            {
                default:
                case ClassEnum.Mage:
                    Inte = 10;
                    break;
                case ClassEnum.Warrior:
                    Str = 10;
                    break;
                case ClassEnum.Ranger:
                    Dex = 10;
                    break;
            }
        }
    }
}
