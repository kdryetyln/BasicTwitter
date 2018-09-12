using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Twitter_New.Models.Repository
{
    public class BaseRepository<T> where T:class
    {
        TwitterDbContext _db = new TwitterDbContext();
        private DbSet<T> Entity { get { return _db.Set<T>(); } }
        public void AddModel(T model)
        {
            Entity.Add(model);
            _db.SaveChanges();
        }
        public void DeleteModel(int id)
        {
            var modelId = GetModelID(id);
            Entity.Remove(modelId);
            _db.SaveChanges();
        }
        public void UpdateModel(T model)
        {
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public ICollection<T> GetAllModelList()
        {
            return Entity.ToList();
        }
        public T GetModelID(int id)
        {
            return Entity.Find(id);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return _db.Set<E>();
        }

        public void UnFollow(int id)
        {
            FriendShip unf = _db.FriendShips.Where(k => k.ID == id).FirstOrDefault();
            _db.FriendShips.Remove(unf);
            _db.SaveChanges();
        }

    }
    
}