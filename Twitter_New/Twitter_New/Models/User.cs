using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter_New.Models
{
    public class User : BaseEntity
    {
        public string NameLastname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Tweet> Tweets {get;set;}
        public ICollection<User> Friends { get; set; }
        public ICollection<LikedTweet> LikedTweets { get; set; }


    }
}