using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using BDT.Domain.Entities;

namespace BDT.Domain.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BdtContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BdtContext context)
        {
            Instructor phil = context.Instructors.Add(new Instructor
                {
                    Name = "Phil Townsend"
                });
            Instructor brandon = context.Instructors.Add(new Instructor
                {
                    Name = "Brandon Howard"
                });
            context.Instructors.Add(new Instructor
                {
                    Name = "Johnny Bologna"
                });
            context.SaveChanges();

            Location brentwood = context.Locations.Add(new Location
                {
                    Name = "Brentwood"
                });
            Location westEnd = context.Locations.Add(new Location
                {
                    Name = "West End"
                });
            context.SaveChanges();

            context.Sessions.Add(new Session
                {
                    Locations = new List<Location>{brentwood},
                    Name = "Summer Session 1",
                    SessionDates = new List<SessionDate>
                        {
                            new SessionDate
                                {
                                    Date = new DateTime(2012, 5, 12),
                                    InstructorId = brandon.Id
                                },
                            new SessionDate
                                {
                                    Date = new DateTime(2012, 5, 19),
                                    InstructorId = brandon.Id
                                },
                            new SessionDate
                                {
                                    Date = new DateTime(2012, 5, 26),
                                    InstructorId = brandon.Id
                                },
                            new SessionDate
                                {
                                    Date = new DateTime(2012, 6, 2),
                                    InstructorId = brandon.Id
                                }
                        }
                });
            context.Sessions.Add(new Session
                {
                    Locations = new List<Location>{westEnd},
                    Name = "Summer Session 2",
                    SessionDates = new List<SessionDate>
                        {
                            new SessionDate
                                {
                                    Date = new DateTime(2012, 5, 12),
                                    InstructorId = phil.Id
                                },
                            new SessionDate
                                {
                                    Date = new DateTime(2012, 5, 19),
                                    InstructorId = phil.Id
                                },
                            new SessionDate
                                {
                                    Date = new DateTime(2012, 5, 26),
                                    InstructorId = phil.Id
                                },
                            new SessionDate
                                {
                                    Date = new DateTime(2012, 6, 2),
                                    InstructorId = phil.Id
                                }
                        }
                });
            context.SaveChanges();

            var jonathan = new Student
                {
                    FirstName = "Jonathan",
                    LastName = "Creamer",
                    Address = "2757 Jutes Drive",
                    City = "Thompson Station",
                    Email = "matrixhasyou2k4@gmail.com",
                    State = "TN",
                    Zip = "37179"
                };
            var lynnsey = new Student
                {
                    FirstName = "Lynnsey",
                    LastName = "Creamer",
                    Address = "2757 Jutes Drive",
                    City = "Thompson Station",
                    Email = "matrixhasyou2k4@gmail.com",
                    State = "TN",
                    Zip = "37179"
                };
            context.Students.Add(jonathan);
            context.Students.Add(lynnsey);
            context.SaveChanges();
        }
    }
}