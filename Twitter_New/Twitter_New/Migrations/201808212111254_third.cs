namespace Twitter_New.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RepplyTweets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        TweetID = c.Int(nullable: false),
                        TweetUserName = c.String(),
                        ReplyTweetBody = c.String(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RepplyTweets");
        }
    }
}
