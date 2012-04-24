namespace BDT.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FirstDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                        State = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SessionDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SessionId = c.Int(nullable: false),
                        InstructorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Sessions", t => t.SessionId, cascadeDelete: true)
                .ForeignKey("Instructors", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.SessionId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seats = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Instructors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "LocationSessions",
                c => new
                    {
                        Location_Id = c.Int(nullable: false),
                        Session_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Location_Id, t.Session_Id })
                .ForeignKey("Locations", t => t.Location_Id, cascadeDelete: true)
                .ForeignKey("Sessions", t => t.Session_Id, cascadeDelete: true)
                .Index(t => t.Location_Id)
                .Index(t => t.Session_Id);
            
            CreateTable(
                "SessionDateStudents",
                c => new
                    {
                        SessionDate_Id = c.Int(nullable: false),
                        Student_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SessionDate_Id, t.Student_Id })
                .ForeignKey("SessionDates", t => t.SessionDate_Id, cascadeDelete: true)
                .ForeignKey("Students", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.SessionDate_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("SessionDateStudents", new[] { "Student_Id" });
            DropIndex("SessionDateStudents", new[] { "SessionDate_Id" });
            DropIndex("LocationSessions", new[] { "Session_Id" });
            DropIndex("LocationSessions", new[] { "Location_Id" });
            DropIndex("SessionDates", new[] { "InstructorId" });
            DropIndex("SessionDates", new[] { "SessionId" });
            DropForeignKey("SessionDateStudents", "Student_Id", "Students");
            DropForeignKey("SessionDateStudents", "SessionDate_Id", "SessionDates");
            DropForeignKey("LocationSessions", "Session_Id", "Sessions");
            DropForeignKey("LocationSessions", "Location_Id", "Locations");
            DropForeignKey("SessionDates", "InstructorId", "Instructors");
            DropForeignKey("SessionDates", "SessionId", "Sessions");
            DropTable("SessionDateStudents");
            DropTable("LocationSessions");
            DropTable("Instructors");
            DropTable("Locations");
            DropTable("Sessions");
            DropTable("SessionDates");
            DropTable("Students");
        }
    }
}
