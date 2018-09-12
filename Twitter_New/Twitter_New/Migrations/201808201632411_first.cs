namespace Twitter_New.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendShips",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        FriendID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.FriendID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.UserID)
                .Index(t => t.FriendID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameLastname = c.String(),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CreatedDate = c.DateTime(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TweetBody = c.String(nullable: false, maxLength: 300),
                        UserID = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Retweets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        newTweetID = c.Int(nullable: false),
                        reTweetID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Retweets", "UserID", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "UserID", "dbo.Users");
            DropForeignKey("dbo.FriendShips", "FriendID", "dbo.Users");
            DropForeignKey("dbo.Tweets", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "User_ID", "dbo.Users");
            DropIndex("dbo.Retweets", new[] { "UserID" });
            DropIndex("dbo.Tweets", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "User_ID" });
            DropIndex("dbo.FriendShips", new[] { "FriendID" });
            DropIndex("dbo.FriendShips", new[] { "UserID" });
            DropTable("dbo.Retweets");
            DropTable("dbo.Tweets");
            DropTable("dbo.Users");
            DropTable("dbo.FriendShips");
        }
    }
}
