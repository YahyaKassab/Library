using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Borrow
    {
        public Borrow(int bookID, int memberID, DateTime borrowDate)
        {
            BookID = bookID;
            MemberID = memberID;
            BorrowDate = borrowDate;
        }

        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
    }
}
