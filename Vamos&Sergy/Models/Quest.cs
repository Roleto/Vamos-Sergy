using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Models
{
    public class Quest
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
        [NotMapped]
        public int Exp { get; set; }
        [NotMapped]
        public double Gold { get; set; }
        [NotMapped]
        public int Mushroom { get; set; }
        [NotMapped]
        public Equipment? Equipment { get; set; }

        [NotMapped]
        public double Time { get; set; }

        public Quest()
        {

        }
        public Quest(int id, string text, int exp, double gold, List<Item> itemList)
        {
            Id = id;
            Text = text;
            Exp = exp;
            Gold = gold;
            Random r = new Random();
            int random = r.Next(0, 101);
            if (random <= 5)
            {
                // Epic lesz majd
                Equipment = new Equipment(itemList.ElementAt(r.Next(itemList.Count)));
            }
            else if (random <= 15)
            {
                Mushroom = 1;
            }
            else if (random <= 20)
            {
                Equipment = new Equipment(itemList.ElementAt(r.Next(itemList.Count)));
            }
            else
            {
                Mushroom = 0;
                Equipment = null;
            }
            Array values = Enum.GetValues(typeof(QuestTimeEnum));
            switch ((QuestTimeEnum)values.GetValue(r.Next(values.Length)))
            {
                default:
                case QuestTimeEnum.Short:
                    Time = 300;
                    break;
                case QuestTimeEnum.Medium:
                    Time = 600;
                    break;
                case QuestTimeEnum.Long:
                    Time = 1200;
                    break;
            }
        }
        public string GetTimeString(MountEnum mount,double adventure)
        {
            if(adventure > Time / 60) {
                Time = adventure * 60;
            }
            double percentage = 1;
            switch (mount)
            {
                default:
                case MountEnum.None:
                    break;
                case MountEnum.Pig:
                    percentage = .9;
                    break;
                case MountEnum.Wolf:
                    percentage = .8;
                    break;
                case MountEnum.Raptor:
                    percentage = .7;
                    break;
                case MountEnum.Griff:
                    percentage = .5;
                    break;
            }
            int newTime = (int)(Time * percentage);
            int min = newTime / 60;
            int sec = newTime % 60;
            if (sec < 10)
                return $"{min}:0{sec}";
            return $"{min}:{sec}";
        }
        public double GetGoldValue(MountEnum mount)
        {
            if (mount == MountEnum.Griff) 
                return Math.Round(Gold * 1.5,2);
            return Gold;
        }
    }
}
