using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Vamos_Sergy.Helpers;

namespace Vamos_Sergy.Models.Items
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SimpleId { get; set; }

        [Required]
        [StringLength(24)]
        [ShowTable]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [ShowTable]
        public string Description { get; set; }

        [Required]
        public EquipmentEnum Type { get; set; }

        public string Url { get; set; }
        public Shop()
        {

        }
        public Shop(string shopName)
        {
            ShopType = shopName;
        }
        public string OwnerId { get; set; }

        public string ShopType { get; set; }
    }
}
