using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Shared.Dtos.Posts
{
    public class PostResponse
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public DateTime CrerateAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public string Status { get; set; } 
        public List<string> Tags { get; set; }
        public string CategoryName { get; set; }

    }
}
