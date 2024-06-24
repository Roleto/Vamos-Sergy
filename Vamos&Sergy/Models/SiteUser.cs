using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vamos_Sergy.Models
{
    public class SiteUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Hero> Heroes { get; set; }
    }
}
