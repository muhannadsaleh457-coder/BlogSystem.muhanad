using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Entites.Blogs
{
    public class Comment : BaseEntity<int>
    {
        public string  Content { get; set; }
        public DateTime CreateAt { get; set; }
        public BlogPost Post { get; set; }
        public int PostId { get; set; }
        public IdentityUser Author { get; set; }
        public string AuthorId { get; set; }
    }
}
