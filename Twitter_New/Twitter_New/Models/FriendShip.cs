using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter_New.Models
{
    public class FriendShip:BaseEntity
    {
        public User User { get; set; }
        public int UserID { get; set; }
        public User Friend { get; set; }
        public int FriendID { get; set; }
    }
}