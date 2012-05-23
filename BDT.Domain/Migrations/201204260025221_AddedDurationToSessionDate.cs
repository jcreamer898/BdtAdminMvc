namespace BDT.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedDurationToSessionDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("SessionDates", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("SessionDates", "Duration");
        }
    }
}
