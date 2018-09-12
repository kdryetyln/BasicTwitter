using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter_New.Models
{
    public class Retweet:BaseEntity
    {
        public User User { get; set; }
        public int UserID { get; set; }
        public int TweetID { get; set; }//which tweet
    }
}