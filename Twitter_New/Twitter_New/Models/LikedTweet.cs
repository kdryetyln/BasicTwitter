using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter_New.Models
{
    public class LikedTweet
    {
        public int ID { get; set; }
        public Tweet Tweet { get; set; }
        public int tweetID { get; set; }
        public User User { get; set; }
        public int userID { get; set; }
    }
}