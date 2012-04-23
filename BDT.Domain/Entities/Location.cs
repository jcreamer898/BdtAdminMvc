namespace BDT.Domain.Entities
{
    public class Location : EntityBase
    {
        public int Seats { get; set; }
        public string Name { get; set; }
    }

    public class Instructor : EntityBase
    {
        public string Name { get; set; }
    }
}