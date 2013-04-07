namespace HotelEden.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoomTypeURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("RoomTypes", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("RoomTypes", "Url");
        }
    }
}
