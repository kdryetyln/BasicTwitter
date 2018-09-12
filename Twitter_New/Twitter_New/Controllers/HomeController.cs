using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Twitter_New.Models;
using Twitter_New.Models.Repository;
using Twitter_New.SessionSetting;

namespace Twitter_New.Controllers
{
    public class HomeController : Controller
    {
        TwitterDbContext _db = new TwitterDbContext();
        BaseRepository<Tweet> _br = new BaseRepository<Tweet>();
        public ActionResult Starting()
        {
            return View();
        }
        public ActionResult Home()
        {
            var user = SessionSet<User>.Get("Login");
            if (user.ID != null)
                return View();
            else
                return RedirectToAction("Login", "User");

        }
        [HttpPost]
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();

            Session.Clear();
            if (Session["ID"] == null && Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
        }
        public ActionResult MyProfile()
        {
            var myID = SessionSet<User>.Get("Login").ID;
            var myTweets =_db.Tweets.Where(k => k.UserID == myID).ToList();
            List<TweetDto> myList = new List<TweetDto>();
            foreach (var item in myTweets)
            {
                var user = _db.Users.Where(k => k.ID == item.UserID).First();
                TweetDto yeni = new TweetDto()
                {
                    ID = item.ID,
                    UserID = item.UserID,
                    CreatedDate = item.CreatedDate,
                    TweetBody = item.TweetBody,
                    UserName = user.UserName
                };
                myList.Add(yeni);
            }
            return View(myList.OrderByDescending(k=>k.CreatedDate).ToList());
        }
        [HttpGet]
        public ActionResult FriendProfile(int id)
        {
            var friendTweet = _db.Tweets.Where(k => k.UserID == id).ToList();
            List<TweetDto> fList = new List<TweetDto>();
            foreach (var item in friendTweet)
            {
                var user = _db.Users.Where(k => k.ID == item.UserID).First();
                TweetDto yeni = new TweetDto()
                {
                    ID = item.ID,
                    UserID = item.UserID,
                    CreatedDate = item.CreatedDate,
                    TweetBody = item.TweetBody,
                    UserName = user.UserName
                };
                fList.Add(yeni);
            }
            Session["TweetUserId"] = id;
            return View(fList.OrderByDescending(k=>k.CreatedDate).ToList());
        }
    }
}