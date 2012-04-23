using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;

namespace BDT.Domain.Repositories
{
    public class InstructorRepostiory : BaseRepository, IInstructorRepostiory
    {
        public Instructor Get(int id)
        {
            return Db.Instructors.SingleOrDefault(l => l.Id == id);
        }

        public IEnumerable<Instructor> GetAllInstructors()
        {
            return Db.Instructors;
        }

        public Instructor Add(Instructor instructor)
        {
            Db.Instructors.Add(instructor);
            Db.SaveChanges();
            return instructor;
        }

        public Instructor Update(Instructor instructor)
        {
            Db.Entry(instructor).State = EntityState.Modified;
            Db.SaveChanges();
            return instructor;
        }

        public void Delete(int instructor)
        {
            Db.Instructors.Remove(Get(instructor));
            Db.SaveChanges();
        }}
}
