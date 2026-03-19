using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Entites.Blogs
{
    public class BlogPost : BaseEntity<int>
    {

        public BlogPost()
        {
            
        }
        public BlogPost(string title, string content, BlogPostStatus status, int? categoryId, string authorId)
        {
            Title = title;
            Content = content;
            Status = status;
            CategoryId = categoryId;
            AuthorId = authorId;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public IdentityUser Author { get; set; }
        public string AuthorId { get; set; }
        public DateTime CrerateAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public BlogPostStatus Status { get; set; } = BlogPostStatus.Published;
        public List<Tag> Tags { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
