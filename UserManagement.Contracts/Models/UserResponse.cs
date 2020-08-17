﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Contracts.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsTrialUser { get; set; }
        public int? CustomerId { get; set; }
        public List<int> UserRoles { get; set; }
    }
}
