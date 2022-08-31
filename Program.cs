using System;
using System.Collections;
namespace Library
{
    class program
    {
        enum Options
            {
                Exit = 0,
                AddDoc,
                AddMember,
                BorrowBook,
                ReturnBook,
                PrintAll,
                PrintBooks,
                PrintNovels,
                PrintBooksByAuthorName,
                PrintMembers,
                DeleteMember,
                DeleteAll,

            }
        //work with print members selects

        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            List<Novel> novels = new List<Novel>();
            List<Borrow> borrows = new List<Borrow>();
            List<Member> members = new List<Member>();
            books.Add(new Book("The Great Gatsby","F. Scott Fitzgerald", 3, 80));
            books.Add(new Book("The Grapes of Wrath","John Steinbeck", 4, 45));
            books.Add(new Book("Nineteen Eighty-Four","George Orwell", 5 , 120));
            books.Add(new Book("Ulysses","James Joyce", 1, 89));
            novels.Add(new Novel("Lolita","Joseph Heller", 8, 14,90));
            novels.Add(new Novel("Catch-22","J. D. Salinger", 2, 22,110));
            novels.Add(new Novel("Beloved","William Faulkner", 3,12,105));
            void printBook(Book item)
            {
                string av = "Available";
                if (!item.IsAvailable)
                {
                    av = "Not Available";
                }
                Console.WriteLine($"{item.Title.PadRight(20,' ')} |{item.AuthorName.PadRight(20,' ')} |{av.PadRight(15,' ')}|${item.Price.ToString().PadRight(3,' ')}");
            }
            void printNovel(Novel item)
            {
                string av = "Available";
                if (!item.IsAvailable)
                {
                    av = "Not Available";
                }
                Console.WriteLine($"{item.Title.PadRight(20,' ')} |{item.AuthorName.PadRight(20,' ')} |{av.PadRight(15,' ')} |${item.Price.ToString().PadRight(6,' ')} |{item.NumOfVolumes.ToString().PadRight(6,' ')}");
            }
            void printBooks(IEnumerable<Book> Books)
            {
                var MemberBook = from borr in borrows
                                 join book in Books on borr.BookID equals book.ID
                                 join m in members on borr.MemberID equals m.ID
                                 select new
                                 {
                                     BookName = book.Title, AuthorName = book.AuthorName, Price = book.Price, MemberName = m.Name, IsAvailable = book.IsAvailable
                                 };
                Console.WriteLine("Books".PadRight(50,' '));
                Console.WriteLine("==============================================================================");
                Console.WriteLine($"{"Title".PadRight(20,' ')} |{"AuthorName".PadRight(20,' ')} |{"State".PadRight(15,' ')} |{"Price".PadRight(6,' ')}|{"Borrowers".PadRight(15, ' ')} ");

                string av;
                foreach(var item in MemberBook)
                {
                if(item.IsAvailable)av = "Available";
                else av = "Not Available";
                    Console.WriteLine($"{item.BookName.PadRight(20,' ')} |{item.AuthorName.PadRight(20,' ')} |{av.PadRight(15,' ')} |${item.Price.ToString().PadRight(3,' ')}|{item.MemberName.PadRight(15, ' ')} ");
                }
            }
            void printNovels(IEnumerable<Novel> Novels)
            {
                string av;
                var MemberBook = from borr in borrows
                                 join nov in Novels on borr.BookID equals nov.ID
                                 join m in members on borr.MemberID equals m.ID
                                 select new
                                 {
                                     BookName = nov.Title, AuthorName = nov.AuthorName, Price = nov.Price, Vols = nov.NumOfVolumes, MemberName = m.Name, IsAvailable = nov.IsAvailable
                                 };
                    
                Console.WriteLine("Novels".PadRight(50,' '));
                Console.WriteLine("==============================================================================");
                Console.WriteLine($"{"Title".PadRight(20,' ')} |{"AuthorName".PadRight(20,' ')} |{"State".PadRight(15,' ')}|{"Price".PadRight(6,' ')} |{"Borrowers".PadRight(15, ' ')} |{"Vols".PadRight(6,' ')}");
                foreach(var item in MemberBook)
                {
                if(item.IsAvailable)av = "Available";
                else av = "Not Available";
                    Console.WriteLine($"{item.BookName.PadRight(20,' ')} |{item.AuthorName.PadRight(20,' ')} |{av.PadRight(15,' ')}|${item.Price.ToString().PadRight(6,' ')} |{item.MemberName.PadRight(15, ' ')} |{item.Vols.ToString().PadRight(6,' ')}");
                }
            }
            void PrintAll()
            {
                printBooks(books);
                printNovels(novels);
            }
            void printMember(Member member)
            {
                string prem = member.IsPremuim ? "Premuim" : "Not Premuim";
                Console.WriteLine($"{member.Name.PadRight(15,' ')} |{member.City.PadRight(15,' ')} |{prem.PadRight(15,' ')}");
            }
            bool IsMember(string name)
            {
                if(!members.Exists(m=>m.Name.ToLower() == name))
                {
                    Console.WriteLine("You Are Not A Member Let's Make You one");
                    Console.ReadKey();
                    return false;
                }
                return true;
            }
            Member GetMember(string name)
            {
                Member member = members.Find(members => members.Name.ToLower() == name.ToLower());
                return member;
            }
            int EnjoyBook(Member member, Book book)
            {
                if(book.NumOfBooks == 0)
                {
                    Console.WriteLine("Novel is Not Available Right Now");
                    return -1;
                }
                Console.WriteLine("ENJOY!");
                member.NumOfBorrows++;
                book.NumOfBooks--;
                if(book.NumOfBooks == 0)
                {
                    book.IsAvailable = false;
                }
                borrows.Add(new Borrow(book.ID,member.ID,DateTime.Now));
                return 0;
            }
            int EnjoyNovel(Member member, Novel novel)
            {
                if(novel.NumOfBooks == 0)
                {
                    Console.WriteLine("Novel is Not Available Right Now");
                    return -1;
                }
                Console.WriteLine("ENJOY!");
                member.NumOfBorrows++;
                novel.NumOfBooks--;
                if(novel.NumOfBooks == 0)
                {
                    novel.IsAvailable = false;
                }
                borrows.Add(new Borrow(novel.ID,member.ID,DateTime.Now));
                return 0;
            }
            (bool,Novel) SearchBorrowNovel( bool confirm, string Search)
            {
                Novel novel = novels.Find(n=>n.Title.ToLower() == Search);
                printNovel(novel);
                Console.WriteLine("Do you want to borrow it?(y/n)");
                char c;
                confirm = char.TryParse(Console.ReadLine().ToLower(), out c);
                switch (c)
                {
                    case 'y':
                        return (true,novel);
                    default:
                        return (false,novel);
                }
            }
            (bool, Book) SearchBorrowBook(bool confirm, string Search)
            {
                Book book = books.Find(n=>n.Title.ToLower() == Search);
                printBook(book);
                Console.WriteLine("Do you want to borrow it?(y/n)");
                char c;
                confirm = char.TryParse(Console.ReadLine().ToLower(), out c);
                switch (c)
                {
                    case 'y':
                        return (true,book);
                    default:
                        return (false,book);
                }
            }
            void BorrowBook()
            {
                Member member = new Member("Yahya","ismailia");
                bool confirm;
                bool mmbrerror = false;
                do
                {

                Console.Write("Are You A Member?(y/n)");
                char ans;
                char.TryParse(Console.ReadLine().ToLower(), out ans);
                switch (ans)
                {
                    case 'y':
                            Console.Write("Enter Your Name:");
                            string name = Console.ReadLine();
                            if (IsMember(name))
                            {
                              member = GetMember(name);
                              printMember(member);
                            }else
                            {
                                string Membername = AddMember();
                            }
                        Console.WriteLine($"OK {member.Name}, Press Any Key To Continue");
                        Console.ReadKey();
                        break;
                    case 'n':
                        string Mname = AddMember();
                        Console.WriteLine($"OK {Mname}, Press Any Key To Continue");
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        mmbrerror = true;
                        break;
                }
                }while(mmbrerror);
                bool exists;
                do
                {
                bool cont;
                Console.WriteLine("Search For The Book");
                string Search = Console.ReadLine().ToLower();
                char c = 'y';
                bool existsTitleBook = books.ToList().Exists(n=>n.Title.ToLower() == Search);
                bool existsTitleNovel = novels.ToList().Exists(n=>n.Title.ToLower() == Search);
                bool existsAuthorBook = books.ToList().Exists(n=>n.AuthorName.ToLower() == Search);
                bool existsAuthorNovel = novels.ToList().Exists(n=>n.AuthorName.ToLower() == Search);
                exists = existsTitleBook || existsAuthorBook || existsTitleNovel || existsAuthorNovel ;
                if(!exists){
                       
                        Console.WriteLine("Sorry, We dont Have the book");
                        continue;
                    }
                    cont = exists;
                    
                      //  char c;

                        confirm = true;
                    if (existsTitleBook)
                    {
                        var searchedB = SearchBorrowBook(confirm,Search.ToLower());
                        //Console.WriteLine(SearchBorrowBook(confirm,Search.ToLower()).Item1);

                        if (searchedB.Item1)
                        {

                                EnjoyBook(member,searchedB.Item2);

                        }
                    }
                    else if (existsTitleNovel)
                    {
                        var searchedN = SearchBorrowNovel(confirm,Search.ToLower());

                        if (searchedN.Item1)
                        {
                                EnjoyNovel(member,searchedN.Item2);
                        }

                    } 
                    else if (existsAuthorBook)
                    {
                         var searchedBList = books.ToList().Where(n=>n.AuthorName.ToLower() == Search);
                         printBooks(searchedBList);
                         if(searchedBList.Count().Equals(1))
                        {
                            Console.WriteLine("Do you want to borrow it?(y/n)");
                            bool conf = char.TryParse(Console.ReadLine().ToLower(), out c);
                            switch (c)
                        {
                            case 'y':
                                    EnjoyBook(member, searchedBList.ToArray()[0]);
                                break;
                            default :
                                continue;
                                break;
                        }


                        }
                        else
                        {
                            bool done = false;
                            Console.WriteLine("Which one Do you want?(By Name)");
                            string bookName = Console.ReadLine().ToLower();
                            foreach(Book book in searchedBList)
                            {
                                if(book.Title.ToLower() == bookName.ToLower())
                                {
                                    EnjoyBook(member, book);
                                    done = true;
                                }
                                break;
                            }
                            if (!done)
                            {
                                Console.WriteLine("Wrong Input");
                                continue;
                            }

                        }
                    }
                    else if (existsAuthorNovel)
                    {
                         var searchedNList = novels.ToList<Novel>().Where(n=>n.AuthorName.ToLower() == Search);
                         printBooks(searchedNList);
                         if(searchedNList.Count().Equals(1))
                        {
                            Console.WriteLine("Do you want to borrow it?(y/n)");
                            confirm = char.TryParse(Console.ReadLine().ToLower(), out c);
                            switch (c)
                        {
                            case 'y':
                                    EnjoyNovel(member, searchedNList.First());
                                break;
                            default :
                                continue;
                                break;
                        }


                        }
                        else
                        {
                            bool done = false;
                            Console.WriteLine("Which one Do you want?(By Name)");
                            string novelName = Console.ReadLine().ToLower();
                            foreach(Novel novel in searchedNList)
                            {
                                if(novel.Title.ToLower() == novelName.ToLower())
                                {
                                    EnjoyBook(member, novel);
                                    done = true;
                                }
                                break;
                            }
                            if (!done)
                            {
                                Console.WriteLine("Wrong Input");
                                continue;
                            }

                        }
                    }
                
                }while(!exists);


            }
            void AddBook()
            {
                Console.Write("Enter Book Title:");
                string title = Console.ReadLine();
                Console.Write("Enter Author Name:");
                string author = Console.ReadLine();
                Console.Write("Enter Number of Copies:");
                uint copies;
                bool success = uint.TryParse(Console.ReadLine(),out copies);
                while (!success)
                {
                    Console.Write("Please Enter a 'Number'.\nEnter Number of Copies:");
                    success = uint.TryParse(Console.ReadLine(),out copies);
                }
                int price;
                Console.Write("Enter The Price:");
                bool success2 = int.TryParse(Console.ReadLine(),out price);
                while (!success2)
                {
                    Console.Write("Please Enter A 'Number'\nEnter The Price:");
                    success2 = int.TryParse(Console.ReadLine(),out price);
                }
                books.Add(new Book(title,author,copies,price));
                Console.WriteLine("Book Added Successfully");
            }
            string AddMember()
            {
                Console.Clear();
                Console.Write("Enter Member's Name:");
                string name = Console.ReadLine(); 
                Console.Write("Enter Member's City:");
                string city = Console.ReadLine();
                members.Add(new Member(name,city));
                Console.WriteLine("Member Added Successfully");
                return name;

            }
            void AddNovel()
            {
                Console.Write("Enter Novel Title:");
                string title = Console.ReadLine();
                Console.Write("Enter Author Name:");
                string author = Console.ReadLine();
                Console.Write("Enter Number of Copies:");
                uint copies;
                bool success = uint.TryParse(Console.ReadLine(),out copies);
                while (!success)
                {
                    Console.Write("Please Enter a 'Number'.\nEnter Number of Copies:");
                    success = uint.TryParse(Console.ReadLine(),out copies);
                }
                Console.Write("Enter Number of Volumes:");
                uint vols;
                bool success2 = uint.TryParse(Console.ReadLine(),out vols);
                while (!success2)
                {
                    Console.Write("Please Enter a 'Number'.\nEnter Number of Volumes:");
                    success2 = uint.TryParse(Console.ReadLine(),out copies);
                }
                int price;
                Console.WriteLine("Enter The Price:");
                bool success3 = int.TryParse(Console.ReadLine(),out price);
                while (!success3)
                {
                    Console.Write("Please Enter a 'Number'.\nEnter The Price:");
                    success3 = uint.TryParse(Console.ReadLine(),out copies);
                }
                novels.Add(new Novel(title,author,vols,copies, price));
                Console.WriteLine("Novel Added Successfully");

            }
            void AddDoc()
        {
            bool success;
            do
            {
                Console.WriteLine("Novel or Book?(n/b)");
                char Answer;
                success = char.TryParse(Console.ReadLine().ToLower(), out Answer);
                switch (Answer)
                {
                    case 'n':
                        AddNovel();
                        break;
                    case 'b' :
                        AddBook();
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        success = false;
                        break;
                }
            }while(!success);
        }
            Member SearchMember()
            {
                Console.Write("Enter The Name Of The Member:");
                string memberName = Console.ReadLine().ToLower();
                Member member = members.Find(m=>m.Name.ToLower() == memberName);
                return member;

            }
            void PrintMembers()
            {
                var MembersBooks = from bo in borrows
                                   join book in books on bo.BookID equals book.ID
                                   join m in members on bo.MemberID equals m.ID
                                   select new
                                   {
                                       MemberName = m.Name, City = m.City, Status = m.IsPremuim, Books = book.Title
                                   };
                Console.WriteLine("Members".PadRight(50,' '));
                Console.WriteLine($"{"Name".PadRight(15,' ')} |{"City".PadRight(12,' ')} |{"Status".PadRight(25,' ')} |{"Books".PadRight(20,' ')}");
                foreach(var m in MembersBooks)
                {
                string status = m.Status == true? "Premuim" : "Not Premuim";
                Console.WriteLine($"{m.MemberName.PadRight(15,' ')} |{m.City.PadRight(12,' ')} |{status.PadRight(25,' ')} |{m.Books.PadRight(20,' ')}");
                }
            }
            void DeleteMember()
            {
                Member member = SearchMember();
                members.Remove(member);
            }
            void DeleteAll()
            {
                members.Clear();
                books.Clear();
                novels.Clear();
                borrows.Clear();
                
            }
            void ReturnBook()
            {
                Console.Write("Enter Your Name:");
                string name = Console.ReadLine();
                Member member = GetMember(name);
                
            }
                   int opt;
            do
            {
                Console.Clear();
                Console.WriteLine("0-Exit");
                Console.WriteLine("1-Add Doc");
                Console.WriteLine("2-Add Member");
                Console.WriteLine("3-Borrow Book");
                Console.WriteLine("4-Return Book");
                Console.WriteLine("5-Print All");
                Console.WriteLine("6-Print Books");
                Console.WriteLine("7-Print Novels");
                Console.WriteLine("8-Print Books By Author Name");
                Console.WriteLine("9-Print Members");
                Console.WriteLine("10-Delete Member");
                Console.WriteLine("11-Delete All");
                do
                {
                    Console.WriteLine("Please Select an option");
                    string selectionString = Console.ReadLine();
                    if (!int.TryParse(selectionString, out opt))
                        Console.WriteLine("Invalid Selection ");

                } while (opt > 11 || opt < 0);

                Console.Clear();
                Options option = (Options)opt;
                switch (option)
                {
                    case Options.Exit:
                        break;
                    case Options.AddDoc:
                        AddDoc();
                        Console.ReadKey();
                        break;
                    case Options.AddMember:
                        AddMember();
                        Console.ReadKey();
                        break;
                    case Options.BorrowBook:
                        BorrowBook();
                        Console.ReadKey();
                        break;
                    case Options.ReturnBook:
                        
                        Console.ReadKey();
                        break;
                    case Options.PrintAll:
                        PrintAll();
                        Console.ReadKey();
                        break;
                    case Options.PrintBooks:
                        printBooks(books);
                        Console.ReadKey();
                        break;
                    case Options.PrintNovels:
                        printNovels(novels);
                        Console.ReadKey();
                        break;
                    case Options.PrintBooksByAuthorName:
                        
                        Console.ReadKey();
                        break;
                    case Options.PrintMembers:
                        PrintMembers();
                        Console.ReadKey();
                        break;
                    case Options.DeleteMember:
                        DeleteMember();
                        Console.ReadKey();
                        break;
                    case Options.DeleteAll:
                        DeleteAll();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }

            }while (opt != 0);
    
        }
    }
}