using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement_Repositories.Dtos
{
    public class RegisterUserProfileDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }


        [Required, MinLength(11), MaxLength(11)]
        [RegularExpression(@"^[0-9]+$")]
        public string PersonalNumber { get; set; }
        
    }
}
