using System.ComponentModel.DataAnnotations.Schema;

namespace Vamos_Sergy.Models
{
    public class Weapon : Item
    {
        [NotMapped]
        public int MinDamage { get; set; }
        [NotMapped]
        public int MaxDamage { get; set; }

    }
}
