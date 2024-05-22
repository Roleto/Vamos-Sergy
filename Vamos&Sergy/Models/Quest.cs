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
        [StringLength(24)]
        public string Text { get; set; }
        [NotMapped]
        public int Exp { get; set; }
        [NotMapped]
        public double Gold { get; set; }
        [NotMapped]
        public int Mushroom { get; set; }
        [NotMapped]
        public Equipment? Equipment { get; set; }

        public Quest()
        {
            
        }
        public Quest(int id,string text, int exp, double gold, List<Item> itemList)
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
            else if(random <= 20)
            {
                Equipment = new Equipment(itemList.ElementAt(r.Next(itemList.Count)));
            }
            else
            {
                Mushroom = 0;
                Equipment = null;
            }
        }
    }
}
