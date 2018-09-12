namespace Twitter_New.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikedTweets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        tweetID = c.Int(nullable: false),
                        userID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tweets", t => t.tweetID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.userID, cascadeDelete: false)
                .Index(t => t.tweetID)
                .Index(t => t.userID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikedTweets", "userID", "dbo.Users");
            DropForeignKey("dbo.LikedTweets", "tweetID", "dbo.Tweets");
            DropIndex("dbo.LikedTweets", new[] { "userID" });
            DropIndex("dbo.LikedTweets", new[] { "tweetID" });
            DropTable("dbo.LikedTweets");
        }
    }
}
