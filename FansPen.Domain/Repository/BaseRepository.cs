using FansPen.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace FansPen.Domain.Repository
{
    public class BaseRepository<T> where T : class
    {
        public ApplicationContext Db;

        public BaseRepository(ApplicationContext context)
        {
            Db = context;
        }

        public List<T> GetList()
        {
            return Db.Set<T>().ToList();
        }

        public T GetItem(int id)
        {
            return Db.Set<T>().Find(id);
        }


        public void Create(T item)
        {
            Db.Set<T>().Add(item);
        }

        public T Update(int id, T item)
        {
            Db.Set<T>().Update(item);
            Save();
            return Db.Set<T>().Find(id);
        }

        public bool Delete(int id)
        {
            var item = Db.Set<T>().Find(id);
            if (item != null)
            {
                Db.Set<T>().Remove(item);
                Save();
            }
            return item != null;
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}