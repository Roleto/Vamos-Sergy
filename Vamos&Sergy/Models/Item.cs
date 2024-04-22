using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vamos_Sergy.Helpers;

namespace Vamos_Sergy.Models
{
    public abstract class Item
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(24)]
        [ShowTable]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [ShowTable]
        public string Description { get; set; }


        [Required]
        [NotMapped]
        public string Stats { get; set; }

        [Required]
        public EquipmentEnum Type { get; set; }

        [Required]
        public RaceEnum RequiredClass{ get; set; }

        [NotMapped]
        public int Price { get; set; }
        
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
        public string ContentType { get; set; }

        [NotMapped]
        public byte[] Data { get; set; }

        public override string ToString()
        {
            string output = "";
            string[] stats = Stats.Split(';');
            return base.ToString();
        }
        public Item()
        {
            Id = Guid.NewGuid().ToString();
        }
        protected void GenerateStat(string line)
        {

        }

    }
}
