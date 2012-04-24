using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Build.Framework;

namespace BDT.Domain.Entities
{
    public class Session : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        [DisplayName("Session Dates")]
        public virtual ICollection<SessionDate> SessionDates { get; set; }
    }
}