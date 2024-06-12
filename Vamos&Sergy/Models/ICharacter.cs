using System.ComponentModel.DataAnnotations.Schema;

namespace Vamos_Sergy.Models
{
    public interface ICharacter
    {
        public int Str { get; set; }
        public int Defence { get; }
        public int Dex { get; set; }
        public int Evasion { get; }
        public int Inte { get; set; }
        public int Resistance { get; }
        public int Vit { get; set; }
        public int Hp { get; }
        public double Luck { get; set; }
        public int Damage { get; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
