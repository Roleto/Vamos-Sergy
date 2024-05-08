namespace Vamos_Sergy.Models.Items
{
    public class Weapon : Equipment
    {
        public Weapon() : base()
        {
            
        }

        public Weapon(Item item) : base(item)
        {
            
        }
        public int Attack(ClassEnum kast)
        {
            int damage = 0;
            double crit = 0;

            switch (RequiredClass)
            {
                default:
                case ClassEnum.Mage:
                    damage = _random.Next(MinDamage + (Inte * 4), MaxDamage + 1+ (Inte * 4));
                    crit = damage * 0.4;
                    break;
                case ClassEnum.Warrior:
                    damage = _random.Next(MinDamage + Str, MaxDamage + 1+ Str);
                    crit = damage * 0.3;
                    break;
                case ClassEnum.Ranger:
                    damage = _random.Next(MinDamage + (Dex * 2), MaxDamage + 1+ (Dex * 2));
                    crit = damage * 0.2;
                    break;
            }
            if (Owner.Crit > _random.Next(0, 101))
                damage += (int)crit;

            return damage;
        }
    }
}
