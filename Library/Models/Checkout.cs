using System.Collections.Generic;
using System;

namespace Library.Models
{
    public class Checkout
    {
        public Checkout()
        {
            Returned = false;
        }
        public int CheckoutId { get; set; }
        public int PatronId { get; set; }
        public int CopyId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; }
        public Patron Patron { get; set; }
        public Copy Copy { get; set; }
    }
}