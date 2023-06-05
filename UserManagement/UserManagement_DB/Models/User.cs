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
        [Required]
        public  string UserName { get; set; }
        [Required]
        public  string Email { get; set; }
        [Required]
        public  string Password { get; set; }
        [Required]
        public  IsActive IsActive { get; set; }
        public UserProfile Profile { get; set; }
    }
}
