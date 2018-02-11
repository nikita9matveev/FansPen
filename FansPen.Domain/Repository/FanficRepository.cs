using FansPen.Domain.Models;

namespace FansPen.Domain.Repository
{
    public class FanficRepository : BaseRepository<Fanfic>
    { 
        public FanficRepository(ApplicationContext context) : base(context) { }
    }
}