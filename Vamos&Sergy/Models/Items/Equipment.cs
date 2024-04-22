using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vamos_Sergy.Models.Items
{
    public class Equipment : Item
    {
        [Required]
        public bool IsEqueped { get; set; }

        [Required]
        public string OwherId { get; set; }

        [Required]
        public string Stats { get; set; }


        [NotMapped]
        public int Price { get; set; }

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

        protected override void GenerateStat()
        {
            base.GenerateStat();
        }
    }
}
