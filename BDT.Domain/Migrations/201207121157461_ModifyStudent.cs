namespace BDT.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ModifyStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("Students", "Phone", c => c.String());
            AddColumn("Students", "PhoneType", c => c.String());
            AddColumn("Students", "Phone2", c => c.String());
            AddColumn("Students", "PhoneType2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Students", "PhoneType2");
            DropColumn("Students", "Phone2");
            DropColumn("Students", "PhoneType");
            DropColumn("Students", "Phone");
        }
    }
}
