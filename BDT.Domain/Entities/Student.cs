using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Build.Framework;

namespace BDT.Domain.Entities
{
    public class Student : EntityBase
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string State { get; set; }
        
        
        public string Email { get; set; }

        public virtual ICollection<SessionDate> SessionDates { get; set; }
    }
}