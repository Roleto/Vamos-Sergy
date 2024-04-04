using Microsoft.AspNetCore.Mvc;
using PracticeModul.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Vamos_Sergy.Models;

namespace PracticeModul.Models
{
    public class Hero
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(24)]
        [ShowTable]
        public string Name { get; set; }

        [ShowTable]
        public int Exp { get; set; }

        [Required]
        [ShowTable]
        public int Level { get; set; }

        [NotMapped]
        public double GetCurrentXpPercentage { get => (Exp/ ((double)Level * 256)) * 100; }

        [Required]
        [ShowTable]
        public ClassEnum Kast { get; set; }

        [ShowTable]
        public bool HasMount { get; set; }


        private int str;

        //[NotNull]
        public int Str
        {
            get { return str; }
            set { str = value; }
        }

        private int dex;

        //[NotNull]
        public int Dex
        {
            get { return dex; }
            set { dex = value; }
        }        

        private int inte;

        //[NotNull]
        public int Inte
        {
            get { return inte; }
            set { inte = value; }
        }

        private int vit;

        //[NotNull]
        public int Vit
        {
            get { return vit; }
            set { vit = value; }
        }

        [NotMapped]
        public int Hp { get => vit * 2 *(this.Level + 1); }

        private double luck;

        //[NotNull]
        public double Luck
        {
            get { return luck; }
            set { luck = value; }
        }
        [NotMapped]
        [Range(0,50)]
        //Enemy level-re kell majd ki venni
        public double Crit { get => (luck * 5) / ((double)this.Level * 2); }

        public string OwnerId { get; set; }

        [NotMapped]
        public virtual SiteUser Owner { get; set; }

        public Hero()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Hero(string? id)
        {
            if(id == null)
                Id = Guid.NewGuid().ToString();
            else 
                Id = id;
        }

    }
}
