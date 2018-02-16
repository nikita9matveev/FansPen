using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansPen.Domain.Repository
{
    public class CommentRepository : BaseRepository<Comment>
    {
        private DbSet<Comment> _commentEntity;

        public CommentRepository(ApplicationContext context) : base(context)
        {
            _commentEntity = context.Set<Comment>();
        }

        public List<Comment> GetCommentsByIdFanfic(int id, int package)
        {
            return _commentEntity
                .OrderByDescending(x => x.DataCreate)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Likes)
                .Where(x => x.FanficId == id)
                .Skip(package).Take(10).ToList();
        }

        public int SendComment(string userId, int fanficId, string text)
        {
            _commentEntity.Add(new Comment
            {
                ApplicationUserId = userId,
                DataCreate = DateTime.Now,
                Text = text,
                FanficId = fanficId
            });
            Save();
            return _commentEntity.Where(x => x.FanficId == fanficId).Count();
        }

        public List<Comment> GetNewComments(int id)
        {
            return _commentEntity
                .OrderByDescending(x => x.DataCreate)
                .Include(x => x.ApplicationUser)
                .Include(x => x.Likes)
                .Where(x => x.DataCreate > DateTime.Now.AddSeconds(-2))
                .Where(x => x.FanficId == id).ToList();
        }

        public int DeleteComment(int idComment, int idFanfic)
        {
            Comment comment = _commentEntity.Where(x => x.Id == idComment).FirstOrDefault();
            if(_commentEntity != null)
            {
                _commentEntity.Remove(comment);
                Save();
            }
            return _commentEntity.Where(x => x.FanficId == idFanfic).Count();
        }

        public int GetNewCount(int idFanfic)
        {
            return _commentEntity.Where(x => x.FanficId == idFanfic).Count();
        }
    }
}
