namespace Twitter_New.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Retweets", "TweetID", c => c.Int(nullable: false));
            DropColumn("dbo.Retweets", "newTweetID");
            DropColumn("dbo.Retweets", "reTweetID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Retweets", "reTweetID", c => c.Int(nullable: false));
            AddColumn("dbo.Retweets", "newTweetID", c => c.Int(nullable: false));
            DropColumn("dbo.Retweets", "TweetID");
        }
    }
}
