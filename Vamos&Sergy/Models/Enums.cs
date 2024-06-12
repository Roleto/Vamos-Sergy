namespace Vamos_Sergy.Models
{
    public enum EquipmentEnum
    {
        Helmet = 0,
        Armor = 1,
        Hand = 2,
        Leg = 3,
        Necklace = 4,
        Belt = 5,
        Ring = 6,
        Misc = 7,
        Shield = 8,
        Weapon = 9,
    }
    public enum ClassEnum
    {
        Mage = 0,
        Warrior = 1,
        Ranger = 2,
        All = 3,
    }

    public enum RaceEnum
    {
        Human = 0,
        Elf = 1,
        Darkelf = 2,
        Dwarf= 3,
        Deamon= 4,
    }

    public enum HeroStateEnum
    {
        Free = 0,
        OnAdvanture = 1,
        Working = 2,
    }

    public enum QuestTimeEnum
    {
        Short = 0, // 5perc
        Medium = 1,//10 perc
        Long = 2,//20 perc
    }

    public enum MountEnum
    {
        None = 0,
        Pig = 1,
        Wolf = 2,
        Raptor = 3,
        Griff = 4,
    }
}
