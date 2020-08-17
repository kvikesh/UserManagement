using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Users = new HashSet<Users>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
