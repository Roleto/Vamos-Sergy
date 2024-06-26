using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vamos_Sergy.Models
{
    public class Monster : ICharacter
    {
        private Random _random;

        public Monster()
        {
            _random = new Random();
        }

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
        public int Defence => Str / 2;

        public int Dex { get; set; }

        [NotMapped]
        public int Evasion => Dex / 2;

        public int Inte { get; set; }

        [NotMapped]
        public int Resistance => Inte / 2;

        public int Vit { get; set; }

        [NotMapped]
        public int Hp => Vit * 2 * (this.Level + 1);
        
        [NotMapped]
        public int CurrentHp { get; set; }


        public double Luck { get; set; }


        [NotMapped]
        public int Damage
        {
            get
            {
                int minDamage = 0;
                int maxDamage = 0;
                switch (Kast)
                {
                    default:
                    case ClassEnum.Mage:
                        minDamage = (Level * 50) * (1 + Inte / 10);
                        maxDamage = (Level * 100) * (1 + Inte / 10);
                        return _random.Next(minDamage, maxDamage); 
                    case ClassEnum.Warrior:
                        minDamage = (Level * 50) * (1 + Str / 10);
                        maxDamage = (Level * 100) * (1 + Str / 10);
                        return _random.Next(minDamage, maxDamage);
                    case ClassEnum.Ranger:
                        minDamage = (Level * 50) * (1 + Dex / 10);
                        maxDamage = (Level * 100) * (1 + Dex / 10);
                        return _random.Next(minDamage, maxDamage);
                }
            }
        }

        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public int FightCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
