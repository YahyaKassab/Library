using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Borrow
    {
        public Borrow(int bookID, int memberID, DateOnly borrowDate)
        {
            BookID = bookID;
            MemberID = memberID;
            BorrowDate = borrowDate;
        }

        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateOnly BorrowDate { get; set; }
        public Nullable<DateOnly> ReturnDate { get; set; }
    }
}
