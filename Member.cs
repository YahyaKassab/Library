using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Member
    {
        public Member(string name, string city,uint numOfBorrows = 0)
        {
            Name = name;
            City = city;
            NumOfBorrows = 0;
        }

        public int ID { get; private set;}
        public string Name { get; set; }
        public string City { get; set; }
        public uint NumOfBorrows { get; set; }
        public bool IsPremuim { get; private set; }
        //when a member borrows 3 books and return them in time he becomes premuim 
        //he gets to buy any book with 50% discount 
    }
}
