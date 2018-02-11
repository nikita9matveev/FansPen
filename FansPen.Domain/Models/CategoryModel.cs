using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FansPen.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Fanfic> Fanfics { get; set; }
        public Category()
        {
            Fanfics = new List<Fanfic>();
        }
    }
}
