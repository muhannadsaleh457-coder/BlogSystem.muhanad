using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Shared.Dtos.Comments
{
    public class CreateCommentRequest
    {
        public string Content { get; set; }
        public int PostId { get; set; }

    }
}
