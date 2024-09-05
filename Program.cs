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
                choice = handelIntError(Console.ReadLine());
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

                Console.Clear();


            } while (ExitFlag != true);
         
        }





        static void AdminMenu()
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome Admin");
                Console.WriteLine("\n Enter the char of operation you need :");
                Console.WriteLine("\n A- Add New Book");
                Console.WriteLine("\n B- Display All Books");
                Console.WriteLine("\n C- Remove Book");
                Console.WriteLine("\n D- Search for Book by Name");
                Console.WriteLine("\n E- Exit");

                string choice = Console.ReadLine().ToUpper();
                
                switch (choice)
                {
                    case "A":
                        AddnNewBook();
                        break;

                    case "B":
                        ViewAllBooks();
                        break;

                    case "D":
                        SearchForBook();
                        break;

                    case "C":
                        RemoveBook();
                        break;

                    case "E":
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;


                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

            } while (ExitFlag != true);

        }

        static void UserMenu()
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome User");
                Console.WriteLine("\n Enter the char of operation you need :");
                Console.WriteLine("\n A- Search for Book by Name");
                Console.WriteLine("\n B- Borrow Book");
                Console.WriteLine("\n C- Return Book");
                Console.WriteLine("\n D- Exit");

                string choice = Console.ReadLine().ToUpper();

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

                

            } while (ExitFlag != true);

        }
        static void AddnNewBook() 
        {
            Console.Clear();
            Console.WriteLine("\t\tAdding Book\n");
            ViewAllBooks();
            bool flag = false;
            Console.WriteLine("Enter Book ID");
            int ID = handelIntError(Console.ReadLine());
            foreach (var book in Books)
            {
                if (book.ID == ID)
                {
                    Console.WriteLine("The id is exist..");
                    flag = true;
                    break;

                }
            }
            if( flag !=true)
            {
                Console.WriteLine("Enter Book Name");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Book Author");
                string author = Console.ReadLine();

                Console.WriteLine("Enter Book Quantity");
                int qun = handelIntError(Console.ReadLine());


                Books.Add((name, author, ID, qun));
                Console.WriteLine("Book Added Succefully");

            }
             

        }

        static void ViewAllBooks()
        {
            Console.Clear();
            Console.WriteLine("\t\t Books \n");
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;
            Console.WriteLine("ID\tTitle\tAuther\tQuantity ");

            for (int i = 0; i < Books.Count; i++)
            {             
                BookNumber = i + 1;
                sb.Append(Books[i].ID).Append("\t").Append(Books[i].BName).Append("\t").Append(Books[i].BAuthor).Append("\t").Append(Books[i].Qun);
                sb.AppendLine();
              
                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
        }

        static void RemoveBook()
        {
            ViewAllBooks();
            Console.WriteLine("Enter Book ID");

            int ID = handelIntError(Console.ReadLine());
            for (int i = 0; i < Books.Count; i++) 
            {
                if (Books[i].ID == ID)
                {
                    Books.RemoveAt(i);
                }
            }

        }

        static void SearchForBook()
        {
            ViewAllBooks();
            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine().ToUpper();  
            bool flag=false;

            for(int i = 0; i< Books.Count;i++)
            {
                if (Books[i].BName.ToUpper() == name)
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
            bool flge = false;  
            Console.WriteLine("Enter Book ID");
           
            int ID = handelIntError(Console.ReadLine());
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
                    flge = true;
                }

            }
            if (flge != true)
            {
                Console.WriteLine("Book not availabe");
            }

              
        }
        
        static void ReturnBook()
        {
            ViewAllBooks();
            bool flge = false;
            Console.WriteLine("Enter Book ID");
            int ID = handelIntError(Console.ReadLine());
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == ID)
                {
                    Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, (Books[i].Qun + 1));
                    Console.WriteLine(Books[i].BName + " returned to the library\n\nThank you.");
                    flge = true;
                }
                
            }

            if (flge != true)
            {
                Console.WriteLine("Book not exist..");
            }

        }

        static int handelIntError(string input)
        {
            int num;
            bool flag = true;
            do
            {
                if (int.TryParse(input, out num))
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("re-enter input");
                    input = Console.ReadLine();
                    flag = true;
                }
            } while (flag == true);
            return num;
        }
    }
}

