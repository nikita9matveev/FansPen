using FansPen.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Web.Tools
{
    public class FanficComparer : IEqualityComparer<FanficPreViewModel>
    {
        public bool Equals(FanficPreViewModel x, FanficPreViewModel y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(FanficPreViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
