using System.Collections.Generic;
using BDT.Domain.Entities;

namespace BDT.Domain.Abstract
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
        Student SignUp(int id, IEnumerable<SessionDate> sessionDates);
        Student AddNew(Student student);
    }
}