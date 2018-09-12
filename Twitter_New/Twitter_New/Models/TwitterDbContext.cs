using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Twitter_New.Models
{
    public class TwitterDbContext:DbContext
    {
        public TwitterDbContext():base("TwitterConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TwitterDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<FriendShip> FriendShips { get; set; }
        public DbSet<Retweet> Retweets { get; set; }
        public DbSet<LikedTweet> LikedTweets { get; set; }
        public DbSet<RepplyTweet> RepplyTweets { get; set; }
    }
}