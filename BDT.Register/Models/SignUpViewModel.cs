using BDT.Domain.Entities;
namespace BDT.Domain
{
    public class SignUpViewModel
    {
        public Session Session { get; set; }
        public Location Location { get; set; }
        public Student Student { get; set; }
    }
}