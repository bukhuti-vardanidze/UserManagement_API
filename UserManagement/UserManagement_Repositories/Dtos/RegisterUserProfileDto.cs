﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement_Repositories.Dtos
{
    public class RegisterUserProfileDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        public string PersonalNumber { get; set; }
        
    }
}
