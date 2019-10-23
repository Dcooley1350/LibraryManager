using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.Models
{
    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<AuthorBook>();
        }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<AuthorBook> Books { get; }

    }
}