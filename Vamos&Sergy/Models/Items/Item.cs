﻿using System.ComponentModel.DataAnnotations;
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

      

        [NotMapped]
        public string ContentType { get; set; }

        [NotMapped]
        public byte[] Data { get; set; }

        public Item()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
