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
            return Db.Students.ToList();
        }

        /// <summary>
        /// Sign up a student for a given list of classes
        /// </summary>
        /// <param name="id">The Id of the student to add classes</param>
        /// <param name="sessionDates">The dates to sign up the student</param>
        /// <returns>The registered student.</returns>
        public Student SignUp(int id, int sessionId)
        {
            var student = Db.Students.Include("Sessions").SingleOrDefault(s => s.Id == id);

            if (student == null)
            {
                throw new Exception("No student found.");
            }

            if (student.Sessions.Any(s => s.Id == sessionId))
            {
                throw new Exception("Student is already registered for this date.");
            }

            var session = Db.Sessions.Include("Students").SingleOrDefault(s => s.Id == sessionId);
            
            if (session == null)
            {
                throw new Exception("No session found.");
            }

            var limit = session.Seats;
            
            if (session.Students.Count + 1 > limit)
            {
                throw new Exception("Session is full.");
            }


            // Attach the dates to the current context to ensure no new sessions
            // get added by accident.
//            var list = sessionDates.ToList();
//            if (!DateAvailable(sessionDates, session))
//            {
//                throw new Exception("This Session is already full.");
//            }
//            list.ForEach(s => Db.SessionDates.Attach(s));
//            student.SessionDates = sessionDates.ToList();

            student.Sessions.Add(session);
            
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
