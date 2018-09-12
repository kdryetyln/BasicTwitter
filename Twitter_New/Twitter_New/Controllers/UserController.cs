using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter_New.Models;
using Twitter_New.Models.Repository;
using System.Web.Security;
using Twitter_New.SessionSetting;

namespace Twitter_New.Controllers
{
    public class UserController : Controller
    {
        BaseRepository<User> _br = new BaseRepository<User>();
        TwitterDbContext _db = new TwitterDbContext();

        public ActionResult Index()
        {
            return View();
        }

        // GET: User
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {           
            var userName=user.UserName;
            var mail = user.Email;
            user.CreatedDate = DateTime.Now;
            bool result = _br.Query<User>().Where(k => (k.UserName.Equals(userName)) && (k.Email.Equals(mail))).Any();
            if (!result)
            {
                _br.AddModel(user);
            }
            else {
                ModelState.AddModelError("", "Please try a different username and email.");
            }
            return RedirectToAction("Starting", "Home");

        }
        public ActionResult Login()
        {
            return RedirectToAction("Starting", "Home");
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var userName = user.UserName;
            var pass = user.Password;

            bool result = _br.Query<User>().Where(k => (k.UserName.Equals(userName)) && (k.Password.Equals(pass))).Any();
            if (result)
            {
                IQueryable<User> users = _br.Query<User>().Where(k => (k.UserName.Equals(userName)) && (k.Password.Equals(pass)));
                var myUser = users.FirstOrDefault();
                User _temp = new User();
                _temp.ID = myUser.ID;
                _temp.NameLastname = myUser.NameLastname;
                _temp.UserName = myUser.UserName;
                _temp.Email = myUser.Email;
                _temp.Password = myUser.Password;
                SessionSet<User>.Set(_temp, "Login");
                return RedirectToAction("Home", "Home");
            }
            else {
                ModelState.AddModelError("", "The username and password you entered did not match our records. Please double-check and try again.");
                return RedirectToAction("Starting", "Home");
            }
        }

        public ActionResult WhoToFollow()
        {
            var myId = SessionSet<User>.Get("Login").ID;
            var notfriends = _db.Database.SqlQuery<int>(
                                   "SELECT ID FROM Users where ID not IN(SELECT FriendID FROM FriendShips where UserID =   " + myId + ") and ID!=" + myId).ToList();
            List<User> list = new List<User>();
            foreach (int item in notfriends)
            {
                var model = _db.Users.FirstOrDefault(x => x.ID == item);
                list.Add(model);
            }
            return View(list);
        }
        [HttpGet]
        public ActionResult Follow()
        {
            FriendShip friend = new FriendShip();
            var FriendId = RouteData.Values["id"];
            var user = SessionSet<User>.Get("Login");
            friend.UserID = user.ID;
            friend.FriendID = Convert.ToInt32(FriendId);

            var result = _br.Query<FriendShip>().Where(k => (k.UserID.Equals(friend.UserID)) && (k.FriendID.Equals(friend.FriendID))).FirstOrDefault();
            if (result == null)
            {
                _db.FriendShips.Add(friend);
                _db.SaveChanges();
            }
            return RedirectToAction("Home", "Home");
        }

        public ActionResult UnFollow(int id)
        {
            var friend = _db.FriendShips.FirstOrDefault(k => k.FriendID == id);
            if (friend != null)
            {
                FriendShip model = _db.FriendShips.Where(k => k.FriendID == id).FirstOrDefault();
                _db.FriendShips.Remove(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Home", "Home");
        }

        public ActionResult MyFollowing()
        {
            var myId = SessionSet<User>.Get("Login").ID;
            var following = _db.Database.SqlQuery<string>("select NameLastname from Users Where ID in " +
                "(select f.FriendID  from FriendShips f inner join Users u on u.ID=f.UserID  where f.UserID=" + myId + ")").ToList();
            List<User> list = new List<User>();
            foreach (string item in following)
            {
                var model = _db.Users.FirstOrDefault(x => x.NameLastname == item);
                list.Add(model);
            }
            return View(list);
        }
        public ActionResult MyFollowers()
        {
            var myId = SessionSet<User>.Get("Login").ID;
            var following = _db.Database.SqlQuery<string>("select NameLastname from Users Where ID in " +
                "(select f.UserID  from FriendShips f inner join Users u on u.ID=f.UserID  where f.FriendID=" + myId + ")").ToList();
            List<User> list = new List<User>();
            foreach (string item in following)
            {
                var model = _db.Users.FirstOrDefault(x => x.NameLastname == item);
                list.Add(model);
            }
            return View(list);
        }



    }
}