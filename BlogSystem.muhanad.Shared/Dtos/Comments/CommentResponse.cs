using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Shared.Dtos.Comments
{
    public class CommentResponse
    {
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public string PostTitle { get; set; }
        public string AuthorName { get; set; }
    }
}
