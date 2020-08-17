using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserManagement.Contracts.Models
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsTrialUser { get; set; }
        public int? CustomerId { get; set; }

        public List<int> Roles { get; set; }
    }
}
