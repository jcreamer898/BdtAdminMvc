using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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
            Seeder.Seed(context);
        }
    }

    internal static class Seeder
    {
        internal static void Seed (BdtContext context )
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