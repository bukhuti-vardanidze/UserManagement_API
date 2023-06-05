using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserManagement_DB.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum IsActive
    {
        Active = 1,
        NotActive = 0
    }
    public class User
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //public string Id { get; set; }
        public  string UserName { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        public  IsActive IsActive { get; set; }

        //[ForeignKey("Id")]
        //public virtual IdentityUser IdentityUser { get; set; }
        public UserProfile Profile { get; set; }
    }
}
