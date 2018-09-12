using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter_New.Models
{
    public class TweetDto:BaseEntity
    {
        public int UserID{ get; set; }
        public string TweetBody { get; set; }
        public string UserName { get; set; }
    }
}