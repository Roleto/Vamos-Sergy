﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Vamos_Sergy.Models.Items;

namespace Vamos_Sergy.Data.Classes
{
    public class ShopItem
    {
        public string HeroId { get; set; }        
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Stats { get; set; }
        public int ShopSlot { get; set; }
        public int InvSlot { get; set; }

    }
}
