using System.Text;
// test check out
namespace BasicLibrary
{
    internal class Program
    {
        static List<(string BName, string BAuthor, int ID, int Qun)> Books = new List<(string BName, string BAuthor, int ID,int Qun)>();
        static string filePath = "C:\\Users\\Codeline User\\Desktop\\Afra\\lib.txt";

        static void Main(string[] args)
        {// downloaded form ahmed device 
            bool ExitFlag = false;
            int choice;
            LoadBooksFromFile();

            do {

                Console.WriteLine("Welcome to Library");
                Console.WriteLine("\nLogin as..\n\nEnter the number of your choice: ");
                Console.WriteLine("\n 1- Admin ");
                Console.WriteLine("\n 2- User");
                Console.WriteLine("\n 3- Exit");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AdminMenu();
                        break;

                    case 2:
                        UserMenu();
                        break;

                    case 3:
                        SaveBooksToFile();
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();



            } while (ExitFlag != true);
         


        }





        static void AdminMenu()
        {
            bool ExitFlag = false;
            do
            {
                Console.WriteLine("Welcome Admin");
                Console.WriteLine("\n Enter the char of operation you need :");
                Console.WriteLine("\n A- Add New Book");
                Console.WriteLine("\n B- Display All Books");
                Console.WriteLine("\n C- Search for Book by Name");
                Console.WriteLine("\n D- Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "A":
                        AddnNewBook();
                        break;

                    case "B":
                        ViewAllBooks();
                        break;

                    case "C":
                        SearchForBook();
                        break;

                    case "D":
                        //SaveBooksToFile();
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);

        }

        static void UserMenu()
        {
            bool ExitFlag = false;
            do
            {
                Console.WriteLine("Welcome User");
                Console.WriteLine("\n Enter the char of operation you need :");
                Console.WriteLine("\n A- Search for Book by Name");
                Console.WriteLine("\n B- Borrow Book");
                Console.WriteLine("\n C- Return Book");
                Console.WriteLine("\n D- Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "A":
                        SearchForBook();
                        break;

                    case "B":
                        BorrowBook();
                        break;

                    case "C":
                        ReturnBook();
                        break;

                    case "D":
                        //SaveBooksToFile();
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;


                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);

        }
        static void AddnNewBook() 
        { 
            Console.WriteLine("Enter Book Name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Book Author");
            string author= Console.ReadLine();  

            Console.WriteLine("Enter Book ID");
            int ID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Book Quantity");
            int qun = int.Parse(Console.ReadLine());


            Books.Add(  ( name, author, ID ,qun)  );
            Console.WriteLine("Book Added Succefully");

        }

        static void ViewAllBooks()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {             
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append(" name : ").Append(Books[i].BName);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Books[i].ID);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Quantity : ").Append(Books[i].Qun);
                sb.AppendLine().AppendLine();
                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
        }

        static void SearchForBook()
        {
            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine();  
            bool flag=false;

            for(int i = 0; i< Books.Count;i++)
            {
                if (Books[i].BName == name)
                {
                    Console.WriteLine("Book Author is : " + Books[i].BAuthor);
                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found"); }
        }

        static void LoadBooksFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                Books.Add((parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3])));
                            }
                        }
                    }
                    Console.WriteLine("Books loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        static void SaveBooksToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var book in Books)
                    {
                        writer.WriteLine($"{book.BName}|{book.BAuthor}|{book.ID}|{book.Qun}");
                    }
                }
                Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        static void BorrowBook()
        {
           
            ViewAllBooks();
            Console.WriteLine("Enter Book ID");
            int ID = int.Parse(Console.ReadLine());
            for (int i = 0; i < Books.Count; i++)
            {
                if(Books[i].ID == ID)
                {
                    if (Books[i].Qun> 0)
                    {
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID,(Books[i].Qun-1));
                        Console.WriteLine(Books[i].BName +" availabe.\nPlease Return it withen 2 weeks..");
                    }
                    else
                    {
                        Console.WriteLine("Book not availabe");
                    }
                }
            }
               



        }
        
        static void ReturnBook()
        {
            ViewAllBooks();
            Console.WriteLine("Enter Book ID");
            int ID = int.Parse(Console.ReadLine());
            for (int i = 0; i < Books.Count; i++)
            {

                Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, (Books[i].Qun +1));
                Console.WriteLine(Books[i].BName + " returned to the library\n\nThank you.");
            }
        }
    }
}

