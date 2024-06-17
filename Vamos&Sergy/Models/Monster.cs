using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vamos_Sergy.Models
{
    public class Monster : ICharacter
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public string GuildId { get; set; }
        [NotMapped]
        public string GuildName { get; set; }
        public int Level { get; set; }

        public ClassEnum Kast { get; set; }
        public int Str { get; set; }

        [NotMapped]
        public int Defence => throw new NotImplementedException();

        public int Dex { get; set; }

        [NotMapped]
        public int Evasion => throw new NotImplementedException();

        public int Inte { get; set; }

        [NotMapped]
        public int Resistance => throw new NotImplementedException();

        public int Vit { get; set; }

        [NotMapped]
        public int Hp => throw new NotImplementedException();

        public double Luck { get; set; }


        [NotMapped]
        public int Damage => throw new NotImplementedException();

        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
