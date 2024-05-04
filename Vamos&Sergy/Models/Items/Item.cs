using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Vamos_Sergy.Helpers;

namespace Vamos_Sergy.Models.Items
{
    public class Item
    {
        [Key]
        public string Id { get; set; }

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

        [Required]
        public ClassEnum RequiredClass { get; set; }
        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [AllowNull]
        public string? SecondaryContentType { get; set; }

        [AllowNull]
        public byte[]? SecondaryData { get; set; }

        public Item()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
