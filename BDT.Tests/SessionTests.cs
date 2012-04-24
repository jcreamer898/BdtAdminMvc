using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BDT.Domain;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;
using BDT.Domain.Repositories;
using Moq;
using NUnit.Framework;

namespace BDT.Tests
{
    [TestFixture]
    class When_a_student_signs_up_for_dates_in_one_session : TestInitializer
    {
        private List<SessionDate> _sessionDates;
        private Session _session;

        [TestFixtureSetUp]
        public void SetupTestFixture(){
            var database = new BdtContext();
            var instructor = new Instructor();
            instructor.Name = "Joe";
            database.Instructors.Add(instructor);

            var location = new Location();
            location.Name = "Brentwood";
            location.Seats = 60;
            database.Locations.Add(location);

            database.SaveChanges();

            _session = new Session
            {
                Name = "Summer Session 1",
                Locations = new List<Location>{location},
            };
            database.Sessions.Add(_session);
            database.SaveChanges();

            _sessionDates = new List<SessionDate>();
            _sessionDates.Add(new SessionDate
            {
                Date = new DateTime(2012, 5, 12),
                SessionId = _session.Id,
                InstructorId = instructor.Id
            });
            _sessionDates.Add(new SessionDate
            {
                Date = new DateTime(2012, 5, 19),
                SessionId = _session.Id,
                InstructorId = instructor.Id
            });
            _sessionDates.Add(new SessionDate
            {
                Date = new DateTime(2012, 5, 26),
                SessionId = _session.Id,
                InstructorId = instructor.Id
            });
            _sessionDates.Add(new SessionDate
            {
                Date = new DateTime(2012, 6, 2),
                SessionId = _session.Id,
                InstructorId = instructor.Id
            });

            _sessionDates.ForEach(d => database.SessionDates.Add(d));
            database.SaveChanges();
        }

        [TestFixtureTearDown]
        public void TearDownTestFixture()
        {
            var database = new BdtContext();
            database.Instructors.ToList().ForEach(i => database.Instructors.Remove(i));
            database.Locations.ToList().ForEach(l => database.Locations.Remove(l));

            database.SaveChanges();   
        }

        [TearDown]
        public void TearDown()
        {
            var database = new BdtContext();
            var remove = database.Students.SingleOrDefault(s => s.FirstName == "JonathanTest");
            if(remove != null)
            {
                database.Students.Remove(remove);
                database.SaveChanges();
            }
            Assert.IsEmpty(database.Students);
        }

        [Test]
        public void A_student_gets_added()
        {
            var database = new BdtContext();

            var student = new Student
                {
                    FirstName = "JonathanTest",
                    SessionDates = new List<SessionDate>()
                };
            var sessionDates = database.SessionDates.Take(4).ToList();


            IStudentRepository studentRepository = new StudentRepository();
            var signedUpStudent = studentRepository.AddNew(student);

            Assert.That(database.Students.Count(), Is.EqualTo(1));
        }

        [Test]
        public void A_student_gets_added_and_get_all_students_returns_with_that_new_student()
        {
            var database = new BdtContext();

            var student = new Student
            {
                FirstName = "JonathanTest",
                SessionDates = new List<SessionDate>()
            };
            var sessionDates = database.SessionDates.Take(4).ToList();


            IStudentRepository studentRepository = new StudentRepository();
            var signedUpStudent = studentRepository.AddNew(student);

            Assert.That(database.Students.Count(), Is.EqualTo(1));

            var allStudents = studentRepository.GetAllStudents();

            Assert.That(allStudents.Count(), Is.EqualTo(1));
            Assert.That(allStudents.Any(st => st.Id == signedUpStudent.Id), Is.True);
        }


        [Test]
        public void A_student_gets_signed_up_for_session_dates_for_that_session()
        {
            var database = new BdtContext();
            
            var student = new Student
                {
                    FirstName = "JonathanTest",
                    SessionDates = new List<SessionDate>()
                };
            var sessionDates = database.SessionDates.Take(4).ToList();
            

            IStudentRepository studentRepository = new StudentRepository();
            var signedUpStudent = studentRepository.AddNew(student);

            Assert.That(database.Students.Count(), Is.EqualTo(1));

            signedUpStudent = studentRepository.SignUp(student.Id, sessionDates);

            Assert.That(database.Locations.Count(), Is.EqualTo(1));
            Assert.That(database.Sessions.Count(), Is.EqualTo(1));
            Assert.That(database.Instructors.Count(), Is.EqualTo(1));
            Assert.That(database.SessionDates.Count(), Is.EqualTo(4));
            Assert.That(signedUpStudent.SessionDates.Count(), Is.EqualTo(4));
        }
    }

    [TestFixture]
    class Adding_new_sessions : TestInitializer
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var database = new BdtContext();
            var instructor = new Instructor();
            instructor.Name = "Joe";
            database.Instructors.Add(instructor);
            database.SaveChanges();
        }

        [TestFixtureTearDown]
        public void TearDownTestFixture()
        {
            var database = new BdtContext();
            database.Instructors.ToList().ForEach(i => database.Instructors.Remove(i));

            database.SaveChanges();   
        }
        [SetUp]
        public void Setup()
        {
            var database = new BdtContext();
            var location = new Location();
            location.Name = "Brentwood";
            location.Seats = 60;
            database.Locations.Add(location);

            database.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            var bdtContext = new BdtContext();
            bdtContext.Locations.ToList().ForEach(l => bdtContext.Locations.Remove(l));
            bdtContext.SaveChanges();

            bdtContext.Sessions.ToList().ForEach(s => bdtContext.Sessions.Remove(s));
            bdtContext.SessionDates.ToList().ForEach(d => bdtContext.SessionDates.Remove(d));
            bdtContext.SaveChanges();
        }

        [Test]
        public void It_should_add_the_session()
        {
            var database = new BdtContext();
            var sessionRepository = new SessionRepository();
            var session = new Session
                {
                    Name = "Test Session",
                    Locations = new List<Location>(database.Locations)
                };
            session.SessionDates = new List<SessionDate>
                {
                    new SessionDate
                        {
                            Date = new DateTime(2012, 12, 5),
                            InstructorId = database.Instructors.Single().Id
                        }
                };
            var addedSession = sessionRepository.Add(session);
            Assert.That(addedSession.Id,Is.GreaterThan(0));
            Assert.That(database.SessionDates.Count(), Is.EqualTo(1));
        }

        [Test]
        public void It_should_add_the_session_and_dates_for_the_session()
        {
            var database = new BdtContext();
            var sessionRepository = new SessionRepository();
            var session = new Session
            {
                Name = "Test Session",
                Locations = new List<Location>{database.Locations.Single()}
            };
            var addedSession = sessionRepository.Add(session);
            Assert.That(addedSession.Id, Is.GreaterThan(0));

            var sessionDates = new List<SessionDate>
                {
                    new SessionDate
                        {
                            Date = new DateTime(2012,5,12),
                            InstructorId = database.Instructors.Single().Id
                        },
                    new SessionDate
                        {
                            Date = new DateTime(2012, 5, 19),
                            InstructorId = database.Instructors.Single().Id
                        }
                };

            sessionRepository.AddDatesToSession(session.Id, sessionDates);

            var addedSessionWithDates = sessionRepository.Get(session.Id);
            Assert.That(addedSessionWithDates.SessionDates.Count(), Is.EqualTo(2));
            Assert.That(database.SessionDates.Count(), Is.EqualTo(2));
        }
    }

    internal class TestInitializer
    {
        public TestInitializer()
        {
            Database.SetInitializer(new TestDataInitializer());
        }
    }

    internal class TestDataInitializer : DropCreateDatabaseAlways<BdtContext>
    {

    }
}
