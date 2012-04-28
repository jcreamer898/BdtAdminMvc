using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BDT.Domain;
using BDT.Domain.Abstract;
using BDT.Domain.Entities;
using BDT.Domain.Repositories;
using NUnit.Framework;

namespace BDT.Tests
{
    [TestFixture]
    internal class When_a_student_signs_up_for_dates_in_one_session : TestInitializer
    {
        #region Setup/Teardown

        [TearDown]
        public void TearDown()
        {
            var database = new BdtContext();
            Student remove = database.Students.SingleOrDefault(s => s.FirstName == "JonathanTest");
            if (remove != null)
            {
                database.Students.Remove(remove);
                database.SaveChanges();
            }
            Assert.IsEmpty(database.Students);
        }

        #endregion

        private List<SessionDate> _sessionDates;
        private Session _session;

        [TestFixtureSetUp]
        public void SetupTestFixture()
        {
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
                    Locations = new List<Location> {location},
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

        [Test]
        public void A_student_gets_added()
        {
            var database = new BdtContext();

            var student = new Student
                {
                    FirstName = "JonathanTest",
                    SessionDates = new List<SessionDate>()
                };
            List<SessionDate> sessionDates = database.SessionDates.Take(4).ToList();


            IStudentRepository studentRepository = new StudentRepository();
            Student signedUpStudent = studentRepository.Create(student);

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
            List<SessionDate> sessionDates = database.SessionDates.Take(4).ToList();


            IStudentRepository studentRepository = new StudentRepository();
            Student signedUpStudent = studentRepository.Create(student);

            Assert.That(database.Students.Count(), Is.EqualTo(1));

            IEnumerable<Student> allStudents = studentRepository.GetAllStudents();

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
            List<SessionDate> sessionDates = database.SessionDates.Take(4).ToList();


            IStudentRepository studentRepository = new StudentRepository();
            Student signedUpStudent = studentRepository.Create(student);

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
    internal class Adding_new_sessions : TestInitializer
    {
        #region Setup/Teardown

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

        #endregion

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

        [TestFixture]
        public class When_a_student_signs_up_for_a_class
        {
            [SetUp]
            public void SetUp()
            {
                var database = new BdtContext();
                var instructor = new Instructor();
                instructor.Name = "Joe";
                database.Instructors.Add(instructor);

                var location = new Location();
                location.Name = "Brentwood";
                location.Seats = 60;
                database.Locations.Add(location);

                database.SaveChanges();

                var sessionRepository = new SessionRepository();
                var session = new Session
                {
                    Name = "Test Session",
                    Locations = new List<Location>
                        {
                           location
                        },
                    SessionDates = new List<SessionDate>
                        {
                            new SessionDate
                            {
                                Date = new DateTime(2012, 5, 19),
                                InstructorId = instructor.Id,
                                Duration = 8
                            },
                            new SessionDate
                            {
                                Date = new DateTime(2012,5,12),
                                InstructorId = instructor.Id,
                                Duration = 8
                            }
                        }
                };
                var addedSession = sessionRepository.Add(session);
            }

            [TearDown]
            public void TearDown()
            {
                var bdtContext = new BdtContext();
                bdtContext.Locations.ToList().ForEach(l => bdtContext.Locations.Remove(l));
                bdtContext.SaveChanges();

                bdtContext.Students.ToList().ForEach(s => bdtContext.Students.Remove(s));
                bdtContext.Sessions.ToList().ForEach(s => bdtContext.Sessions.Remove(s));
                bdtContext.SessionDates.ToList().ForEach(d => bdtContext.SessionDates.Remove(d));
                bdtContext.SaveChanges();
            }

            [Test]
            public void Students_are_added_and_can_be_retrieved()
            {
                var student = new Student
                {
                    FirstName = "Jonathan",
                    LastName = "Creamer",
                    Address = "2757 Jutes Dr",
                    City = "Thompsons Station",
                    State = "TN",
                    Email = "matrixhasyou2k4@gmail.com"
                };
                IStudentRepository studentRepository = new StudentRepository();
                studentRepository.Create(student);

                var studentRetrieved = studentRepository.Get(student.Id);
                Assert.That(studentRetrieved.Id, Is.EqualTo(student.Id));
            }

            [Test]
            public void The_student_gets_registered()
            {
                var student = new Student
                    {
                        FirstName = "Jonathan",
                        LastName = "Creamer",
                        Address = "2757 Jutes Dr",
                        City = "Thompsons Station",
                        State = "TN",
                        Email = "matrixhasyou2k4@gmail.com"
                    };
                var studentRepository = new StudentRepository();
                studentRepository.Create(student);

                var db = new BdtContext();
                var dates = db.SessionDates;
                studentRepository.SignUp(student.Id, dates);

                var students = studentRepository.GetAllStudents();
                Assert.That(students.ToList().ElementAt(0).SessionDates != null);
                Assert.That(students.ToList().ElementAt(0).SessionDates.ElementAt(0).Date.Year, Is.EqualTo(2012));
            }
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
                            InstructorId = database.Instructors.Single().Id,
                            Duration = 8
                        }
                };
            Session addedSession = sessionRepository.Add(session);
            Assert.That(addedSession.Id, Is.GreaterThan(0));
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
                    Locations = new List<Location> {database.Locations.Single()}
                };
            Session addedSession = sessionRepository.Add(session);
            Assert.That(addedSession.Id, Is.GreaterThan(0));

            var sessionDates = new List<SessionDate>
                {
                    new SessionDate
                        {
                            Date = new DateTime(2012, 5, 12),
                            InstructorId = database.Instructors.Single().Id,
                            Duration = 8
                        },
                    new SessionDate
                        {
                            Date = new DateTime(2012, 5, 19),
                            InstructorId = database.Instructors.Single().Id,
                            Duration = 8
                        }
                };

            sessionRepository.AddDatesToSession(session.Id, sessionDates);

            Session addedSessionWithDates = sessionRepository.Get(session.Id);
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
