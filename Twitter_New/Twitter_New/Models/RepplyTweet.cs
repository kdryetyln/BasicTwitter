using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter_New.Models
{
    public class RepplyTweet:BaseEntity
    {
        public int UserID { get; set; }
        public int TweetID { get; set; }
        public string TweetUserName { get; set; }
        public string ReplyTweetBody { get; set; }
    }
}