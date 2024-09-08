using System;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;
// test check out
namespace BasicLibrary
{
    internal class Program
    {
        static List<(string BName, string BAuthor, int ID, int Qun)> Books = new List<(string BName, string BAuthor, int ID,int Qun)>();
        static List<string> AdminUsers = new List<string>();
        static List<(string UserName, int password)> Users = new List<(string UserName, int password)>();
        static List<(int userId, int BookId)>borrowBook = new List<(int userId, int BookId)>();
        static string filePath = "C:\\Users\\Codeline User\\Desktop\\Afra\\lib.txt";
        static string AdminFile = "C:\\Users\\Codeline User\\Desktop\\Afra\\Admin.txt";
        static string UsersFile = "C:\\Users\\Codeline User\\Desktop\\Afra\\Users.txt";
        static string BorrowFile = "C:\\Users\\Codeline User\\Desktop\\Afra\\Borrow.txt";
        static int userID = 0;

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
                        CheckAdmin();
                        break;

                    case 2:
                        CheckUser();
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
                Console.WriteLine("\n 1- Add New Book");
                Console.WriteLine("\n 2- Display All Books");
                Console.WriteLine("\n 3- Remove Book");
                Console.WriteLine("\n 4- Edit Book");
                Console.WriteLine("\n 5- Search for Book by Name");
                Console.WriteLine("\n 6- Display Report");
                Console.WriteLine("\n 7- Exit");

                int choice = handelIntError(Console.ReadLine());
                
                switch (choice)
                {
                    case 1:
                        AddnNewBook();
                        break;

                    case 2:
                        ViewAllBooks();
                        break;

                    case 3:
                        RemoveBook();
                        break;

                    case 4:
                        EditBook();
                        break;
                    case 5:
                        SearchForBook();
                        break;
                    case 6:
                        Report();
                        break;

                    case 7:
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
            Books.Clear();
            LoadBooksFromFile();
            ViewAllBooks();


                int  ID = Books.Count;
                Console.WriteLine("Enter Book Name");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Book Author");
                string author = Console.ReadLine();

                Console.WriteLine("Enter Book Quantity");
                int qun = handelIntError(Console.ReadLine());


                Books.Add((name, author, ID, qun));
                SaveBooksToFile();
                Console.WriteLine("Book Added Succefully");

            
             

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
            Books.Clear();
            LoadBooksFromFile();
            ViewAllBooks();

            Console.WriteLine("Enter Book ID");
            bool flge = false;

            int ID = handelIntError(Console.ReadLine());
            for (int i = 0; i < Books.Count; i++) 
            {
                if (Books[i].ID == ID)
                {
                    Books.RemoveAt(i);
                    SaveBooksToFile();
                    flge = true;
                }
            }
            if (flge != true)
            {
                Console.WriteLine("Book not availabe");
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

        static void EditBook()
        {
            Books.Clear();
            LoadBooksFromFile();
            ViewAllBooks();
            
            StringBuilder sb = new StringBuilder();
            int index = -1;
            Console.WriteLine("Enter Book ID");
            int ID = handelIntError(Console.ReadLine());

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == ID)
                {
                    index = i;
                    Console.WriteLine("\nID\tTitle\tAuther\tQuantity ");
                    Console.WriteLine(Books[i].ID + "\t" + Books[i].BName + "\t" + Books[index].BAuthor + "\t" + Books[index].Qun);
                }
            }
            
            Console.WriteLine("Choose number to edit:");
            Console.WriteLine("1.Book Title\n2.Book Auther\n3.Book Quantity");
        
            int choice = handelIntError(Console.ReadLine());

            if (index != -1)
            {
                switch (choice)
                {
                    case 1:
                        EditBookeTitle(index);
                        break;

                    case 2:
                        EditBookeAuthor(index);
                        break;

                    case 3:
                        EditBookeQuantity(index);
                        break;

                    default:
                        Console.WriteLine("Invaild input");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
        }

        static void EditBookeTitle(int index)
        {
            Console.WriteLine("\tEdit " + Books[index].BName + " Title\n");
            Console.WriteLine("Enter new title: ");
            string title = Console.ReadLine();

            Console.WriteLine("The new book edite is");
            Console.WriteLine(Books[index].ID + "\t" + title + "\t" + Books[index].BAuthor + "\t" + Books[index].Qun);
            Console.WriteLine("Press 1 to confirm ");

            string confirm = Console.ReadLine();
            if (confirm == "1")
            {
                Books[index] = ((title, Books[index].BAuthor, Books[index].ID, Books[index].Qun));
                SaveBooksToFile();
                Console.WriteLine();
                Console.WriteLine("The new name is saved..");
                
            }
            else
            {
                Console.WriteLine("The change was not saved..");
            }
        }

        static void EditBookeAuthor(int index)
        {
            Console.WriteLine("\tEdit " + Books[index].BName + " Auther\n");
            Console.WriteLine("Enter new auther name: ");
            string Author = Console.ReadLine();

            Console.WriteLine("The new book edite is");
            Console.WriteLine(Books[index].ID + "\t" + Books[index].BName + "\t" + Author + "\t" + Books[index].Qun);
            Console.WriteLine("Press 1 to confirm ");

            string confirm = Console.ReadLine();
            if (confirm == "1")
            {
                Books[index] = ((Books[index].BName, Author, Books[index].ID, Books[index].Qun));
                Console.WriteLine();
                SaveBooksToFile();
                Console.WriteLine("The new auther is saved..");
                
            }
            else
            {
                Console.WriteLine("The change was not saved..");
            }
        }

        static void EditBookeQuantity(int index)
        {
            Console.WriteLine("\tEdit " + Books[index].BName + " Quantity\n");
            Console.WriteLine("Enter new Quantity : ");
            int qun = handelIntError(Console.ReadLine());

            Console.WriteLine("The new book edite is");
            Console.WriteLine(Books[index].ID + "\t" + Books[index].BName + "\t" + Books[index].BAuthor + "\t" + qun);
            Console.WriteLine("Press 1 to confirm ");

            string confirm = Console.ReadLine();
            if (confirm == "1")
            {
                Books[index] = ((Books[index].BName, Books[index].BAuthor, Books[index].ID, qun));
                Console.WriteLine();
                SaveBooksToFile();
                Console.WriteLine("The new Quantity is saved..");

            }
            else
            {
                Console.WriteLine("The change was not saved..");
            }
        }

        static void BorrowBook()
        {
            Books.Clear();
            LoadBooksFromFile();

            borrowBook.Clear();
            LoadBorrowedBookFile();

            
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
                        SaveBooksToFile() ;

                        borrowBook.Add((userID, ID));
                        BorrowedBookFile();
                        BookSuggestion(ID);
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
            Books.Clear();
            borrowBook.Clear();
            LoadBooksFromFile();
            LoadBorrowedBookFile() ;

            
            bool flge = false;
            int index=-1;
         
            Console.WriteLine("\nBooks you have borrowed .. ");
            for(int i=0; i< borrowBook.Count; i++)
            {
                if (borrowBook[i].userId == userID)
                {
                    Console.WriteLine(borrowBook[i].BookId + "\t" + Books[Books.FindIndex(book => book.ID == borrowBook[i].BookId)].BName + "\t");
                }
            }

            Console.WriteLine("Enter Book ID");
            int ID = handelIntError(Console.ReadLine());
            for (int i = 0; i < borrowBook.Count; i++)
            {
                
                if ((borrowBook[i].BookId == ID) && (borrowBook[i].userId ==userID))
                {
                    index = Books.FindIndex(book => book.ID == borrowBook[i].BookId);
                    Books[index] = (Books[index].BName, Books[index].BAuthor, Books[index].ID, (Books[index].Qun + 1));
                    Console.WriteLine(Books[index].BName + " returned to the library\n\nThank you.");

                    borrowBook.Remove((userID,ID));
                    BorrowedBookFile() ;
                    SaveBooksToFile();
                    flge = true;
                }
                
            }

            if (flge != true)
            {
                Console.WriteLine("Book not exist..");
            }

        }

        static void CheckAdmin()
        {
            int Fixedpassword = 12345;
            int password;
            AdminUsers.Clear();
            AdminsFile();

            Console.WriteLine("Enter user name");
            string userName = Console.ReadLine();
           
            
            if (AdminUsers.Contains(userName))
            {
                Console.WriteLine("Enter Password..");
                password = handelIntError(Console.ReadLine());
                if (Fixedpassword == password)
                {
                    AdminMenu();
                }
                else
                {
                    Console.WriteLine("Incorrect Password");
                    Console.WriteLine("press any key to continue");
                    string cont = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("User name not Registered befor..");
                Console.WriteLine("Do you want to add new admin?(enter 1 or 2)\n1.yes \n2.no");
               int choice = handelIntError(Console.ReadLine());
                
                if(choice == 1)
                {
                    Console.WriteLine("Enter Admin Password..");
                    password = handelIntError(Console.ReadLine());
                    if (Fixedpassword == password)
                    {
                        AdminUsers.Add(userName);
                        AddNewAdmin();
                        AdminMenu();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Admin Password");
                        Console.WriteLine("press any key to continue");
                        string cont = Console.ReadLine();
                    }
                }

            }
        }
       
            
        static void AdminsFile()
        {
            try
            {
                if (File.Exists(AdminFile))
                {
                    using (StreamReader reader = new StreamReader(AdminFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                                AdminUsers.Add((line));
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        static void AddNewAdmin()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(AdminFile))
                {
                    foreach (string user in AdminUsers)
                    {
                        writer.WriteLine(user);
                    }
                }
                Console.WriteLine("Admin saved to file successfully.");
                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void CheckUser()
        {
            Users.Clear();
            ReadUsersFormFile();
            
            int password;
            Console.WriteLine("Enter user name");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter Password..");
            password = handelIntError(Console.ReadLine());
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserName == userName)
                {
                    userID = i;
                }
            }
            if (!(Users.Contains((userName, password))))
            {
                for (int i = 0;i<Users.Count;i++)
                {
                    if (Users[i].UserName == userName)
                    {
                        if (Users[i].password != password)
                        {
                            Console.WriteLine("Incorrect Passward");
                            Console.WriteLine("press any key to continue");
                            string cont = Console.ReadLine();
                            
                            return;
                        }
                    }
                }
                Console.WriteLine("Not Registered befor..");
                Console.WriteLine("Do you want to Regester?(enter 1 or 2)\n1.yes \n2.no");
                int choice = handelIntError(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Enter user name");
                    userName = Console.ReadLine();
                    Console.WriteLine("Enter Password..");
                    password = handelIntError(Console.ReadLine());

                    Users.Add((userName, password));
                    userID = Users.Count;
                    AddNewUser();
                    UserMenu();

                }
                else
                {
                    Console.WriteLine("Incorrect user Password");
                    Console.WriteLine("press any key to continue");
                    string cont = Console.ReadLine();
                }

            }
            else
            {
                UserMenu();
                
            }
            
        
        }

        static void AddNewUser()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(UsersFile))
                {
                    foreach (var user in Users)
                    {
                        writer.WriteLine($"{user.UserName}|{user.password}");
                    }
                }

                Console.WriteLine("User saved to file successfully.");
                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        static void ReadUsersFormFile()
        {
            try
            {
                if (File.Exists(UsersFile))
                {
                    using (StreamReader reader = new StreamReader(UsersFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 2)
                            {
                                Users.Add((parts[0],handelIntError( parts[1])));
                            }
                        }
                    }
              
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        static void BorrowedBookFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(BorrowFile))
                {
                    foreach (var book in borrowBook)
                    {

                        writer.WriteLine($"{book.userId}|{book.BookId}");

                    }
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }

        }
        static void LoadBorrowedBookFile()
        {

            try
            {
                if (File.Exists(BorrowFile))
                {
                    using (StreamReader reader = new StreamReader(BorrowFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 2)
                            {
                                borrowBook.Add((handelIntError( parts[0]), handelIntError(parts[1])));
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }

        }

        static void Report()
        {
            Books.Clear();
            
            LoadBorrowedBookFile();
            LoadBooksFromFile();
            int booksInLibrary = 0;
            
            for (int i = 0; i < Books.Count; i++)
            {
                booksInLibrary += Books[i].Qun;
            }

            int[] popularBook = new int[Books.Count];
            int popularBookId =-1; 
            for (int i = 0; i < borrowBook.Count; i++) 
            {
                for (int j = 0; j < popularBook.Length; j++)
                {
                    if(borrowBook[i].BookId == j)
                    {
                        popularBook[j]++;
                    }
                }
                
            }
            
            for(int i = 0;i < popularBook.Length; i++)
            {
                if (popularBook[i] == popularBook.Max())
                {
                    popularBookId = i;
                }
            }

            
            Console.WriteLine("Number of Borroed Books is : "+borrowBook.Count);
            Console.WriteLine("Number of Books in Library is : " + booksInLibrary);
            Console.WriteLine("Most borrowed book : " + Books[popularBookId].BName);

        }

        static void BookSuggestion(int bId)
        {
            List<int> SimilarPeopleIds= new List<int>  ();
          List<int> SuggestedBookIds = new List<int> ();    
            for (int i = 0; i < borrowBook.Count; i++)
            {
                if (bId == borrowBook[i].BookId)
                {
                    if (borrowBook[i].userId != userID)
                    {
                        SimilarPeopleIds.Add(borrowBook[i].userId);
                    }
                }
            }
            for (int j = 0; j < SimilarPeopleIds.Count; j++)
            {

                for (int i = 0; i < borrowBook.Count; i++)
                {
                    if (borrowBook[i].userId == SimilarPeopleIds[j] && borrowBook[i].BookId != bId)
                    {
                        SuggestedBookIds.Add(borrowBook[i].BookId);
                        //;
                    }
                }
            }
            List<int> FinalSuggestedBookIds = SuggestedBookIds.Distinct().ToList();
            Console.WriteLine("People who borrowed this book also borrowed with it");
            if (FinalSuggestedBookIds.Count != 0)
            {
                for (int i = 0; i < FinalSuggestedBookIds.Count; i++)
                {
                    Console.WriteLine(Books[FinalSuggestedBookIds[i]].BName);
                }
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

