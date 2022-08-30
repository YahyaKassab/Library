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
            books.Add(new Book("The Great Gatsby","F. Scott Fitzgerald", 3));
            books.Add(new Book("The Grapes of Wrath","John Steinbeck", 4));
            books.Add(new Book("Nineteen Eighty-Four","George Orwell", 5));
            books.Add(new Book("Ulysses","James Joyce", 1));
            novels.Add(new Novel("Lolita","Joseph Heller", 8, 14));
            novels.Add(new Novel("Catch-22","J. D. Salinger", 2, 22));
            novels.Add(new Novel("Beloved","William Faulkner", 3,12 ));
          void printBook(Book item)
            {
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
          void printMember(Member member)
            {
                string prem = member.IsPremuim ? "Premuim" : "Not Premuim"
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
                Member member = from m in members
                                where m.Name.ToLower() == name.ToLower()
                                select m;
                return member;
            }
          void BorrowBook()
            {
                Member member;
                bool mmbrerror = false;
                do
                {

                Console.Write("Are You A Member?(y/n)");
                char ans;
                char.TryParse(Console.ReadLine().ToLower(), out ans);
                switch (ans)
                {
                    case 'y':
                        if(IsMember())  member = GetMember();
                        printMember(member);
                        Console.WriteLine($"OK {member.Name}, Press Any Key To Continue");
                        Console.ReadKey();
                        break;
                    case 'n':
                        string name = members.Add();
                        Console.WriteLine($"OK {name}, Press Any Key To Continue");
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        mmbrerror = true;
                        break;
                }
                }while(mmbrerror)
                bool exists;
                do
                {
                bool cont;
                Console.WriteLine("Search For The Book");
                string Search = Console.ReadLine().ToLower();
                char c;
                bool existsTitleBook = books.ToList().Exists(n=>n.Title.ToLower() == Search);
                bool existsTitleNovel = novels.ToList().Exists(n=>n.Title.ToLower() == Search);
                bool existsAuthorBook = books.ToList().Exists(n=>n.AuthorName.ToLower() == Search);
                bool existsAuthorNovel = novels.ToList().Exists(n=>n.AuthorName.ToLower() == Search);
                exists = existsTitle && existsAuthor;
                if(!exists){
                        
                        Console.WriteLine("Sorry, We dont Have the book");
                        continue;
                    }
                cont = exists;
                    Book searchedB;
                    Novel searchedN;
                    if (existsTitleBook)
                    {
                         searchedB = books.ToList().Where(n=>n.Title.ToLower() == Search);
                        printBook(searchedB);
                    }
                    else if (existsTitleNovel)
                    {
                         searchedN = novels.ToList().Where(n=>n.Title.ToLower() == Search);
                        printNovel(searchedN);
                    } 
                    else if (existsAuthorBook)
                    {
                         searchedB = books.ToList().Where(n=>n.AuthorName.ToLower() == Search);
                        printBook(searchedB);
                    }
                    else if (existsAuthorNovel)
                    {
                         searchedN = novels.ToList().Where(n=>n.AuthorName.ToLower() == Search);
                        printNovel(searchedN);
                    }

                    Console.WriteLine("Do you want to borrow it?(y/n)");
                    char confirm = char.TryParse(Console.ReadLine().ToLower());

                if(existsAuthorBook || existsTitleBook)
                    {
                    switch (confirm)
                    {
                        case 'y':
                            Console.WriteLine("ENJOY!");
                            member.NumOfBorrows++;
                            borrows.Add(new Borrow(searchedB.ID,member.ID,DateTime.Now));
                        case 'n':
                            continue;
                            break;
                        default:
                            continue;
                            break;
                            
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
                books.Add(new Book(title,author,copies));
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
                novels.Add(new Novel(title,author,vols,copies));
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
                        
                        Console.ReadKey();
                        break;
                    case Options.ReturnBook:
                        
                        Console.ReadKey();
                        break;
                    case Options.PrintAll:
                        
                        Console.ReadKey();
                        break;
                    case Options.PrintBooks:
                        
                        Console.ReadKey();
                        break;
                    case Options.PrintNovels:
                        
                        Console.ReadKey();
                        break;
                    case Options.PrintBooksByAuthorName:
                        
                        Console.ReadKey();
                        break;
                    case Options.PrintMembers:
                        
                        Console.ReadKey();
                        break;
                    case Options.DeleteMember:

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