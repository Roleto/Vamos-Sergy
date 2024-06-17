namespace Vamos_Sergy.Models
{
    public interface ICharacter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GuildId { get; set; }
        public string GuildName { get; set; }
        public int Level { get; set; }

        public ClassEnum Kast { get; set; }
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
