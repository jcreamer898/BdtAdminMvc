using System.Collections.Generic;
using BDT.Domain.Entities;

namespace BDT.Domain.Abstract
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
        Student SignUp(int id, int sessionId);
        Student Create(Student student);
        Student Get(int id);
        Student Update(Student student);
    }
}