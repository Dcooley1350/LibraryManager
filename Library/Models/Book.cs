using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<AuthorBook>();
            this.Copies = new HashSet<Copy>();
        }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<AuthorBook> Authors { get; set; }
        public virtual ICollection<Copy> Copies { get; set; }
    }
}