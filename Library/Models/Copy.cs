using System.Collections.Generic;

namespace Library.Models
{
    public class Copy
    {
        public Copy()
        {
            CheckedOut = false;
        }
        public Copy(int bookId, Book book)
        {
            CheckedOut= false;
            BookId = bookId;
            Book = book;
        }
        public int CopyId { get; set; }
        public int BookId { get; set; }
        public bool CheckedOut { get; set; }
        public virtual Book Book { get; set; }
        public virtual ApplicationUser User { get; set;}
    }
}