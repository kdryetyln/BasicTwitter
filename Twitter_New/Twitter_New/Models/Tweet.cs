using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitter_New.Models
{
    public class Tweet:BaseEntity
    {
        [StringLength(300),Required]
        public string TweetBody { get; set; }
        public User User;
        public int UserID { get; set; }
        public int ParentID { get; set; }
        public ICollection<LikedTweet> LikedTweets { get; set; }
    }
}