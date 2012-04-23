using System.Collections.Generic;
using BDT.Domain.Entities;

namespace BDT.Domain.Abstract
{
    public interface IInstructorRepostiory
    {
        Instructor Get(int id);
        IEnumerable<Instructor> GetAllInstructors();
        Instructor Add(Instructor instructor);
        Instructor Update(Instructor instructor);
        void Delete(int instructor);
    }
}