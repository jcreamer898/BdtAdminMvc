using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Build.Framework;

namespace BDT.Domain.Entities
{
    public class SessionDate : EntityBase
    {
        [Required]
        public DateTime Date { get; set; }

        [DefaultValue(8)]
        public int Duration { get; set; }

        public int SessionId { get; set; }
        
        public virtual Session Session { get; set; }
    }
}