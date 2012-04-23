using System;
using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace BDT.Domain.Entities
{
    public class SessionDate : EntityBase
    {
        [Required]
        public DateTime Date { get; set; }
        public int SessionId { get; set; }
        public int InstructorId { get; set; }

        public virtual Session Session { get; set; }
        public virtual Instructor Instructor { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}