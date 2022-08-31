using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Book
    {
        uint _numOfBooks;
        public Book( string title, string authorName, uint numOfBooks,double price)
        {
            Title = title;
            AuthorName = authorName;
            _numOfBooks = numOfBooks;
            Price = price;
            
        } 
        public Book SetData()
        {
            Console.Write("Enter Book Title:");
            Title = Console.ReadLine();
            Console.Write("Enter Author Name:");
            AuthorName = Console.ReadLine();
            Console.Write("Enter Number of Copies:");
            bool success = uint.TryParse(Console.ReadLine(),out _numOfBooks);
            while (!success)
            {
                Console.Write("Please Enter a 'Number'.\nEnter Number of Copies:");
                success = uint.TryParse(Console.ReadLine(),out _numOfBooks);
            }
            return this;
        }
        public uint NumOfBooks { get{return _numOfBooks;} set{_numOfBooks = value;
 }}
        public virtual string Type
        {
            get { return  "Book"; }
        }
        public void Borrow()
        {
            NumOfBooks--;
            if(NumOfBooks == 0)
                IsAvailable = false;

        }
        public void Return()
        {
            NumOfBooks++;
            IsAvailable = true;
        }
        public int ID {get; set;}
        public string Title{ get; set; }
        public string AuthorName { get; set; }
        public bool IsAvailable { get; set; }
        public double Price { get; set; }

    }
}
