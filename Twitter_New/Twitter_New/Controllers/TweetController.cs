using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter_New.Models;
using Twitter_New.Models.Repository;
using Twitter_New.SessionSetting;

namespace Twitter_New.Controllers
{
    public class TweetController : Controller
    {
        BaseRepository<Tweet> _br = new BaseRepository<Tweet>();
        TwitterDbContext _db = new TwitterDbContext();

        // GET: Tweet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTweet()
        {
            var myID = SessionSet<User>.Get("Login").ID;
            List<TweetDto> list = new List<TweetDto>();
            list.Clear();
            var myTweets = _db.Tweets.Where(k => k.UserID == myID).ToList();
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
            var fList = (from u in _br.Query<FriendShip>()
                         join t in _br.Query<Tweet>()
                         on u.FriendID equals t.UserID
                         where u.UserID == myID
                         select new TweetDto()
                         {
                             ID = t.ID,
                             UserID = t.UserID,
                             TweetBody = t.TweetBody,
                             UserName = u.Friend.UserName,
                             CreatedDate = t.CreatedDate
                         }).Distinct().ToList();
            var concatList = fList.Concat(myList);
            list = concatList.OrderByDescending(k => k.CreatedDate).ToList();

            return View(list);
        }
        public ActionResult AddTweet()
        {
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public ActionResult AddTweet(Tweet tweet)
        {
            var user = SessionSet<User>.Get("Login");
            tweet.UserID = user.ID;
            tweet.ParentID = 0;
            tweet.CreatedDate = DateTime.Now;
            _br.AddModel(tweet);
            return RedirectToAction("Home", "Home");

        }
        [HttpGet]
        public ActionResult DeleteTweet(int id)
        {
            var user = SessionSet<User>.Get("Login").ID;
           

            int delTweetId = Convert.ToInt32(RouteData.Values["id"]);
            var tweet = _db.Tweets.FirstOrDefault(x => x.ID == delTweetId);
            tweet.ParentID = 0;
            if (tweet != null)
            {
                _br.DeleteModel(tweet.ID);
            }

            return RedirectToAction("Home","Home");
        
    }

        [HttpGet]
        public ActionResult ReTweet(int id)
        {
            var user = SessionSet<User>.Get("Login").ID;
            Tweet tweet = _br.Query<Tweet>().Where(k => k.ID == id).FirstOrDefault();
            var rtList = _db.Retweets.ToList();
            var result = _br.Query<Retweet>().Where(k => k.TweetID == id && k.UserID == user).Any();
            if (!result)
            {
                Retweet rt = new Retweet()
                {
                    UserID = user,
                    TweetID = id,
                    CreatedDate = DateTime.Now
                };
                _db.Retweets.Add(rt);
                _db.SaveChanges();
            }
            else
            {
                var retwDel = rtList.Where(k => k.TweetID == id && k.UserID == user).FirstOrDefault();
                _db.Retweets.Remove(retwDel);
                _db.SaveChanges();
            }
            

           
            
            return RedirectToAction("Home", "Home");

        }
        [HttpGet]
        public ActionResult TweetID(int id)//click tweet
        {
            Comment com = new Comment();
            com.tweetID = id;
            SessionSet<Comment>.Set(com, "get");
            return View();
        }

        [HttpPost]
        public ActionResult CommentTweet(Tweet tw)
        {
            var id = SessionSet<Comment>.Get("get").tweetID;
            var tID = _br.Query<Tweet>().FirstOrDefault(k => k.ID == id);
            var tweetUser = _br.Query<User>().Where(k => k.ID == tID.UserID).FirstOrDefault();
            RepplyTweet repplyTweet = new RepplyTweet()
            {
                UserID = SessionSet<User>.Get("Login").ID,
                TweetID = tID.ID,
                CreatedDate = DateTime.Now,
                TweetUserName = tweetUser.UserName,
                ReplyTweetBody = tw.TweetBody
            };
            _db.RepplyTweets.Add(repplyTweet);
            _db.SaveChanges();
            
            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult GetReply(int id)
        {
            var replyTweets = _br.Query<RepplyTweet>().Where(k => k.TweetID == id).ToList();

            return View(replyTweets);
        }

        [HttpGet]
        public ActionResult Liked(int id)
        {
            var userID = SessionSet<User>.Get("Login").ID;
            LikedTweet liked = new LikedTweet();
            var likeList = _db.LikedTweets.ToList();
            var result = likeList.Where(k => k.userID == userID&&k.tweetID==id).Any();
            if (!result)
            {
                liked.tweetID = id;
                liked.userID = userID;
                _db.LikedTweets.Add(liked);
                _db.SaveChanges();
            }
            else
            {
                var likeDel = likeList.Where(k => k.userID == userID && k.tweetID == id).FirstOrDefault();
                _db.LikedTweets.Remove(likeDel);
                _db.SaveChanges();
            }

            return RedirectToAction("Home", "Home"); 
        }

    }
}