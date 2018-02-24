using FansPen.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansPen.Domain.Repository
{
    public class LikeRepository : BaseRepository<Like>
    {
        private DbSet<Like> _likeEntity;

        public LikeRepository(ApplicationContext context) : base(context)
        {
            _likeEntity = context.Set<Like>();
        }

        public int SetLike(string userId, int commentId)
        {
            _likeEntity.Add(new Like { ApplicationUserId = userId, CommentId = commentId });
            Save();
            return _likeEntity.Where(x => x.CommentId == commentId).Count();
        }

        public int RemoveLike(string userId, int commentId)
        {
            Like delLike = _likeEntity
                            .Where(x => x.ApplicationUserId == userId)
                            .Where(x => x.CommentId == commentId)
                            .FirstOrDefault();
            if (delLike != null)
            {
                _likeEntity.Remove(delLike);
                Save();
            }
            return _likeEntity.Where(x => x.CommentId == commentId).Count();
        }

        public void DeleteUserLikes(string idUser)
        {
            List<Like> likes = _likeEntity
                .Where(x => x.ApplicationUserId == idUser)
                .ToList();
            if (likes != null)
            {                
                _likeEntity.RemoveRange(likes);
                Save();
            }
        }
    }
}
