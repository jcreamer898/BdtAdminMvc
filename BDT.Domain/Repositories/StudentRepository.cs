using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;

namespace BDT.Domain.Repositories
{
    /// <summary>
    /// Repository for managing students
    /// </summary>
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        /// <summary>
        /// Get a list of all students
        /// </summary>
        /// <returns>A list of students</returns>
        public IEnumerable<Student> GetAllStudents()
        {
            return Db.Students.Include("SessionDates").ToList();
        }

        /// <summary>
        /// Sign up a student for a given list of classes
        /// </summary>
        /// <param name="id">The Id of the student to add classes</param>
        /// <param name="sessionDates">The dates to sign up the student</param>
        /// <returns>The registered student.</returns>
        public Student SignUp(int id, IEnumerable<SessionDate> sessionDates)
        {
            var student = Db.Students.SingleOrDefault(s => s.Id == id);
            if (sessionDates != null)
            {
                // Attach the dates to the current context to ensure no new sessions
                // get added by accident.
                var list = sessionDates.ToList();
                list.ForEach(s => Db.SessionDates.Attach(s));
                student.SessionDates = sessionDates.ToList();
            }
            Db.Entry(student).State = EntityState.Modified;
            Db.SaveChanges();
            return student;
        }

        /// <summary>
        /// Add a new student
        /// </summary>
        /// <param name="student">The student to add</param>
        /// <returns>The added student</returns>
        public Student Create(Student student)
        {
            Db.Students.Add(student);
            Db.SaveChanges();
            return student;
        }

        /// <summary>
        /// Update a student
        /// </summary>
        /// <param name="student">The student to add</param>
        /// <returns>The added student</returns>
        public Student Update(Student student)
        {
            Db.Entry(student).State = EntityState.Modified;
            Db.SaveChanges();
            return student;
        }

        /// <summary>
        /// Get a student by an Id
        /// </summary>
        /// <param name="id">The id of the student to retrieve</param>
        /// <returns>A student</returns>
        public Student Get(int id)
        {
            return Db.Students.SingleOrDefault(st => st.Id == id);
        }
    }

    public abstract class BaseRepository
    {
        /// <summary>
        /// The database connection
        /// </summary>
        protected BdtContext Db;

        protected BaseRepository()
        {
            Db = new BdtContext();
        }

        protected BaseRepository(BdtContext db)
        {
            Db = db;
        }
    }
}
