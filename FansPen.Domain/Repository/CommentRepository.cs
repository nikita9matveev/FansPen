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

        public Comment SendComment(string userId, int fanficId, string text)
        {
            DateTime now = DateTime.Now;
            _commentEntity.Add(new Comment
            {
                ApplicationUserId = userId,
                DataCreate = now,
                Text = text,
                FanficId = fanficId
            });
            Save();
            return _commentEntity
                .Include(x => x.ApplicationUser)
                .Include(x => x.Likes)
                .Where(x => x.ApplicationUserId == userId)
                .Where(x => x.DataCreate == now)
                .Where(x => x.Text == text)
                .Where(x => x.FanficId == fanficId)
                .First();
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

        public void DeleteAllUserComments(string idUser)
        {
            List<Comment> comments = _commentEntity
                .Where(x => x.ApplicationUserId == idUser)
                .ToList();
            if (comments != null)
            {
                _commentEntity.RemoveRange(comments);
                Save();
            }
        }
    }
}
