using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BDT.Domain.Entities
{
    public class Session : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public int Seats { get; set; }
        public string Notes { get; set; }

        public int LocationId { get; set; }

        [DisplayName("Session Dates")]
        public virtual ICollection<SessionDate> SessionDates { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public virtual Location Location { get; set; }
    }

    public class Seats
    {
        public int Number { get; set; }
        public int SessionId { get; set; }
        public int LocationId { get; set; }

        public virtual Session Session { get; set; }
        public virtual Location Location { get; set; }
    }
}