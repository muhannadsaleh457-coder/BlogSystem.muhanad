using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Entites.Blogs
{
    public class Tag : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<BlogPost> Posts { get; set; }
    }
}
