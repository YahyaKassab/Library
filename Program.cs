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
            string av;
          void printBook(Book item)
            {
                if (item.IsAvailable)
                {
                    av = "Available";
                }
                else
                {
                    av = "Not Available";
                }
                Console.Write($"{item.Title.PadRight(20,' ')} |{item.AuthorName.PadRight(20,' ')} |{av.PadRight(12,' ')}");
            }
          void printNovel(Novel item)
            {
                Console.Write($"{item.Title.PadRight(20,' ')} |{item.AuthorName.PadRight(20,' ')} |{av.PadRight(12,' ')} |{item.NumOfVolumes.ToString().PadRight(3,' ')}");
            }
          void printBooks(IEnumerable<Book> objects)
            {
                Console.WriteLine("Books".PadRight(50,' '));
                Console.WriteLine("==============================================================================");
                string av;
                foreach(var item in objects)
                {
                if(item.IsAvailable)av = "Available";
                else av = "Not Available";
                    Console.Write($"{item.Title.PadRight(20,' ')} |{item.AuthorName.PadRight(20,' ')} |{av.PadRight(12,' ')}");
                }
            }
          void printNovels(IEnumerable<Novel> objects)
            {
                string av;
                Console.WriteLine("Novels".PadRight(50,' '));
                Console.WriteLine("==============================================================================");
                foreach(var item in objects)
                {
                if(item.IsAvailable)av = "Available";
                else av = "Not Available";
                    Console.Write($"{item.Title.PadRight(20,' ')} |{item.AuthorName.PadRight(20,' ')} |{av.PadRight(12,' ')} |{item.NumOfVolumes.ToString().PadRight(3,' ')}");
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
          bool IsMember()
            {
                Console.Write("Enter Your Name:");
                string name = Console.ReadLine().ToLower();
                if(!members.Exists(m=>m.Name.ToLower() == name))
                {
                    Console.WriteLine("You Are Not A Member Let's Make You one");
                    return false;
                }
                return true;
            }
          Member GetMember()
            {
                Console.Write("Enter Your Name:");
                string name = Console.ReadLine().ToLower();
                Member member = members.First(members => members.Name.ToLower() == name.ToLower());
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
                        if(IsMember())  
                        member = GetMember();
                        printMember(member);
                        Console.WriteLine($"OK {member.Name}, Press Any Key To Continue");
                        Console.ReadKey();
                        break;
                    case 'n':
                        string name = AddMember();
                        Console.WriteLine($"OK {name}, Press Any Key To Continue");
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
                char c = 's';
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
                    
                     //   char c;

                        confirm = true;
                    if (existsTitleBook)
                    {
                        Book searchedB = SearchBorrowBook(confirm,Search.ToLower()).Item2;

                        switch (c)
                        {
                            case 'y':
                                EnjoyBook(member,searchedB);
                                break;
                            default :
                                continue;
                                break;
                        }
                    }
                    else if (existsTitleNovel)
                    {
                        Novel searchedN = SearchBorrowNovel(confirm,Search.ToLower()).Item2;
                        switch (c)
                        {
                            case 'y':
                                EnjoyNovel(member,searchedN);
                                break;
                            default :
                                continue;
                                break;
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
                int opt;
           // dfsfsd
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

                } while (opt > 8 || opt < 0);

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
                        
                        Console.ReadKey();
                        break;
                    case Options.DeleteMember:
                        DeleteMember();
                        Console.ReadKey();
                        break;
                    case Options.DeleteAll:
                        
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }

            }while (opt != 0);
    
        }
    }
}