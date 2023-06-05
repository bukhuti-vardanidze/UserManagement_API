using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement_DB.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public  string FirstName { get; set; }
        [Required]
        public  string LastName { get; set; }

        [Required, MinLength(11),MaxLength(11)]
        [RegularExpression(@"^[0-9]+$")]
        public  string PersonalNumber { get; set; }
        [Required]
        public  int UserId { get; set; }
       
        public User User { get; set; }
    }
}
