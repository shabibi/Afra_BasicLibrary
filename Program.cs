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
        {
            bool ExitFlag = false;
            int choice;
            LoadBooksFromFile();
            //loop main menu..
            do {
                Console.WriteLine("------------------Welcome to Library----------------");
                Console.WriteLine("\nLogin as..\n\nEnter the number of your choice: ");
                Console.WriteLine("\n  1- Admin ");
                Console.WriteLine("\n  2- User");
                Console.WriteLine("\n  3- Exit");
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


        //Display Admin menu
        static void AdminMenu(string userName)
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("---------------Welcome "+ userName+ "---------------");
                Console.WriteLine("\n\n Enter the number of operation you need :");
                Console.WriteLine("\n   1- Add New Book");
                Console.WriteLine("\n   2- Display All Books");
                Console.WriteLine("\n   3- Remove Book");
                Console.WriteLine("\n   4- Edit Book");
                Console.WriteLine("\n   5- Search for Book by Name");
                Console.WriteLine("\n   6- Display Report");
                Console.WriteLine("\n   7- Exit");

                int choice = handelIntError(Console.ReadLine());
                
                switch (choice)
                {
                    case 1:
                        AddnNewBook();
                        break;

                    case 2:
                        Console.Clear();
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

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

            } while (ExitFlag != true);

        }

        //Display User menu
        static void UserMenu(string userName)
        {
            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("---------------Welcome "+userName+"---------------");
                Console.WriteLine("\n\n Enter the char of operation you need :");
                Console.WriteLine("\n   1- Search for Book by Name");
                Console.WriteLine("\n   2- Borrow Book");
                Console.WriteLine("\n   3- Return Book");
                Console.WriteLine("\n   4- Exit");

                int choice = handelIntError(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        SearchForBook();
                        break;

                    case 2:
                        BorrowBook();
                        break;

                    case 3:
                        ReturnBook();
                        break;

                    case 4:
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;


                }

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

                

            } while (ExitFlag != true);

        }

        //Add new book to the libarary
        static void AddnNewBook() 
        {
            Console.Clear();
            Console.WriteLine("------------------------Adding Book-------------------------\n");
            
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

        //Display All books available in the Library
        static void ViewAllBooks()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("\t\t Books Menu\n");
            Console.WriteLine("***********************************************************");
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;
            Console.WriteLine("ID\tTitle\tAuther\tQuantity ");
            Console.WriteLine("-----------------------------------------------------------");

            for (int i = 0; i < Books.Count; i++)
            {             
                BookNumber = i + 1;
                sb.Append(Books[i].ID).Append("\t").Append(Books[i].BName).Append("\t").Append(Books[i].BAuthor).Append("\t").Append(Books[i].Qun);
                sb.AppendLine();
              
                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
            Console.WriteLine("-----------------------------------------------------------\n");
        }

        //Remove books from the library by entering book's id
        static void RemoveBook()
        {
            Console.Clear();
            Console.WriteLine("------------------------Removing Book------------------------\n");

            Books.Clear();
            LoadBooksFromFile();
            ViewAllBooks();
            
            bool flge = false;
            string removedBook;

            Console.WriteLine("Enter Book ID");
            int ID = handelIntError(Console.ReadLine());

            for (int i = 0; i < Books.Count; i++) 
            {
                if (Books[i].ID == ID)
                {
                    removedBook = Books[i].BName;
                    Books.RemoveAt(i);
                    SaveBooksToFile();
                    flge = true;
                    Console.WriteLine(removedBook + " Removed from Library");
                }
            }
            if (flge != true)
            {
                Console.WriteLine("Book not availabe");
            }

        }
        //Search for book's Author be entering book's name
        static void SearchForBook()
        {
            Console.Clear();
            Console.WriteLine("-----------------------Search for Book-----------------------\n");
            
            ViewAllBooks();

            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine().ToUpper();  

            bool flag=false;

            for(int i = 0; i< Books.Count;i++)
            {
                if (Books[i].BName.ToUpper() == name)
                {
                    Console.WriteLine("\n"+Books[i].BName +" Author is : " + Books[i].BAuthor);
                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("\nbook not found"); }
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
                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        //save Books tuple in lib file 
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
       
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        //Display menu of Editing options
        static void EditBook()
        {
            Console.Clear();
            Console.WriteLine("------------------------Editing Book------------------------\n");
           
            Books.Clear();

            LoadBooksFromFile();
            ViewAllBooks();
            
            StringBuilder sb = new StringBuilder();
            int index = -1;
            Console.WriteLine("Enter Book ID");
            int ID = handelIntError(Console.ReadLine());

            Console.Clear();
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == ID)
                {
                    index = i;
                    Console.WriteLine("\nID\tTitle\tAuther\tQuantity ");
                    Console.WriteLine("******************************************************************");
                    Console.WriteLine(Books[i].ID + "\t" + Books[i].BName + "\t" + Books[index].BAuthor + "\t" + Books[index].Qun);
                    Console.WriteLine("******************************************************************\n");
                }
            }
            
            Console.WriteLine("Choose number to edit " + Books[index].BName +" :\n");
            Console.WriteLine("1.Book Title\n\n2.Book Auther\n\n3.Book Quantity\n");
        
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

        //Function for editing book title
        static void EditBookeTitle(int index)
        {
            Console.Clear();
            Console.WriteLine("----------------------Edit " + Books[index].BName + " Title----------------------\n");
          

            Console.WriteLine("Enter new title: ");
            string title = Console.ReadLine();

            Console.WriteLine("\nThe new book edite is\n");
            Console.WriteLine(Books[index].ID + "\t" + title + "\t" + Books[index].BAuthor + "\t" + Books[index].Qun);
            Console.WriteLine("\nPress 1 to confirm ");

            string confirm = Console.ReadLine();
            if (confirm == "1")
            {
                Books[index] = ((title, Books[index].BAuthor, Books[index].ID, Books[index].Qun));
                SaveBooksToFile();
                Console.WriteLine();
                Console.WriteLine("\nThe new name is saved..");
                
            }
            else
            {
                Console.WriteLine("\nThe change was not saved..");
            }
        }
        //Function for editing book Author
        static void EditBookeAuthor(int index)
        {
            Console.Clear();
            Console.WriteLine("----------------------Edit " + Books[index].BName + " Auther----------------------\n");
            
            Console.WriteLine("Enter new auther name: ");
            string Author = Console.ReadLine();

            Console.WriteLine("The new book edite is\n");
            Console.WriteLine(Books[index].ID + "\t" + Books[index].BName + "\t" + Author + "\t" + Books[index].Qun);
            Console.WriteLine("\nPress 1 to confirm ");

            string confirm = Console.ReadLine();
            if (confirm == "1")
            {
                Books[index] = ((Books[index].BName, Author, Books[index].ID, Books[index].Qun));
                Console.WriteLine();
                SaveBooksToFile();
                Console.WriteLine("\nThe new auther is saved..");
                
            }
            else
            {
                Console.WriteLine("\nThe change was not saved..");
            }
        }

        //Function for editing book Quantity
        static void EditBookeQuantity(int index)
        {
            Console.Clear();
            Console.WriteLine("---------------------Edit " + Books[index].BName + " Quantity---------------------\n");
            
            Console.WriteLine("\nEnter new Quantity : ");
            int qun = handelIntError(Console.ReadLine());

            Console.WriteLine("\nThe new book edite is");
            Console.WriteLine(Books[index].ID + "\t" + Books[index].BName + "\t" + Books[index].BAuthor + "\t" + qun);
            Console.WriteLine("\nPress 1 to confirm ");

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

        //Function for borroing books and update quantity
        static void BorrowBook()
        {
            Books.Clear();
            LoadBooksFromFile();

            borrowBook.Clear();
            LoadBorrowedBookFile();

            Console.Clear();
            Console.WriteLine("------------------------Borrow Book------------------------\n");
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
                        Console.WriteLine("\n"+Books[i].BName +" is availabe.\nPlease Return it withen 2 weeks..\n");
                        Console.WriteLine("********************************************************************");

                        //Add borrowing book in borrrow file and save changes in lib file
                        SaveBooksToFile();
                        borrowBook.Add((userID, ID));
                        BorrowedBookFile();

                        //Display suggestion list after borrowing
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

        //Function for return books and apdate quantity 
        static void ReturnBook()
        {
            Books.Clear();
            borrowBook.Clear();
            LoadBooksFromFile();
            LoadBorrowedBookFile() ;

            Console.Clear();
            Console.WriteLine("------------------------Return Book------------------------\n");

            bool flge = false;
            int index=-1;
         
            //Display list of borrowing books for user
            Console.WriteLine("\nBooks you have borrowed .. ");
            Console.WriteLine("*****************************************************************");
            for(int i=0; i< borrowBook.Count; i++)
            {
                if (borrowBook[i].userId == userID)
                {
                    Console.WriteLine(borrowBook[i].BookId + "\t" + Books[Books.FindIndex(book => book.ID == borrowBook[i].BookId)].BName + "\t");
                }
            }
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("\nEnter Book ID");
            int ID = handelIntError(Console.ReadLine());
            for (int i = 0; i < borrowBook.Count; i++)
            {
                
                if ((borrowBook[i].BookId == ID) && (borrowBook[i].userId ==userID))
                {
                    index = Books.FindIndex(book => book.ID == borrowBook[i].BookId);
                    Books[index] = (Books[index].BName, Books[index].BAuthor, Books[index].ID, (Books[index].Qun + 1));
                    Console.WriteLine("\n"+Books[index].BName + " returned to the library\n\nThank you.");

                    borrowBook.Remove((userID,ID));
                    BorrowedBookFile() ;
                    SaveBooksToFile();
                    flge = true;
                }
                
            }

            if (flge != true)
            {
                Console.WriteLine("\nBook not exist..");
            }

        }

        //Verify Admin access and add admin if not found
        static void CheckAdmin()
        {
            int Fixedpassword = 12345;
            int password;
            AdminUsers.Clear();
            AdminsFile();

            Console.WriteLine("\nEnter user name");
            string userName = Console.ReadLine();
           
            
            if (AdminUsers.Contains(userName))
            {
                Console.WriteLine("\nEnter Password..");
                password = handelIntError(Console.ReadLine());
                if (Fixedpassword == password)
                {
                    AdminMenu(userName);
                }
                else
                {
                    Console.WriteLine("Incorrect Password");
                    Console.WriteLine("\npress enter key to continue");
                    string cont = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("\nUser name not Registered befor..");
                Console.WriteLine("\nDo you want to add new admin?(enter 1 or 2)\n1.yes \n2.no");
               int choice = handelIntError(Console.ReadLine());
                
                if(choice == 1)
                {
                    Console.WriteLine("Enter Admin Password..");
                    password = handelIntError(Console.ReadLine());
                    if (Fixedpassword == password)
                    {
                        AdminUsers.Add(userName);
                        AddNewAdmin();
                        AdminMenu(userName);
                    }
                    else
                    {
                        Console.WriteLine("\nIncorrect Admin Password");
                        Console.WriteLine("\npress enter key to continue");
                        string cont = Console.ReadLine();
                    }
                }

            }
        }
       
       //Load admins from admin file   
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

        //Save new admin name in admin file
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
                Console.WriteLine("\nAdmin saved to file successfully.");
                Console.WriteLine("\npress enter key to continue");
                string cont = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        //Verify User access and add user if not found
        static void CheckUser()
        {
            Users.Clear();
            ReadUsersFormFile();
            
            int password;
            bool flag = false;

            Console.WriteLine("\nEnter user name");
            string userName = Console.ReadLine();

            Console.WriteLine("\nEnter Password..");
            password = handelIntError(Console.ReadLine());

            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserName == userName)
                {
                    userID = i;
                }
            }

            //check Incorrect Passward
            if (!(Users.Contains((userName, password))))
            {
                for (int i = 0;i<Users.Count;i++)
                {
                    if (Users[i].UserName == userName)
                    {
                        if (Users[i].password != password)
                        {
                            Console.WriteLine("Incorrect Passward");
                            Console.WriteLine("\npress enter key to continue");
                            string cont = Console.ReadLine();
                            
                            return;
                        }
                    }
                }
                Console.WriteLine("Not Registered befor..");
                Console.WriteLine("\nDo you want to Regester?(enter 1 or 2)\n1.yes \n2.no");
                int choice = handelIntError(Console.ReadLine());

                //Regester new user
                if (choice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("\nEnter user name");
                    userName = Console.ReadLine();
                    for (int i = 0; i < Users.Count; i++)
                    {
                        if (Users[i].UserName == userName)
                        {
                            Console.WriteLine("This User is registered");
                            Console.WriteLine("\npress enter key to continue");
                            string cont = Console.ReadLine();
                            flag = true;
                            return;
                        }

                    }
                    if(flag != true)
                    {
                        Console.WriteLine("\nEnter Password..");
                        password = handelIntError(Console.ReadLine());

                        //check if username regestered before


                        Users.Add((userName, password));
                        userID = Users.Count;
                        AddNewUser();
                        UserMenu(userName);

                    }
                    

                }
                else if (choice == 2)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("\npress enter key to continue");
                    string cont = Console.ReadLine();
                }

            }
            else
            {
                UserMenu(userName);
            }
        }

        //Save new User name in user file
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

                Console.WriteLine("\nUser saved to file successfully.");
                Console.WriteLine("\npress enter key to continue");
                string cont = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        //Load Users from user file 
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

        //Save new borrow books in borrow file
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

        //Load borrow books from borrow file
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

        //Desplay Report to admin
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

            Console.WriteLine("Number of Borroed Books is : " + borrowBook.Count);
            Console.WriteLine("Number of Books in Library is : " + booksInLibrary);
            Console.WriteLine("Most borrowed book : " );
            for (int i = 0;i < popularBook.Length; i++)
            {
                if (popularBook[i] == popularBook.Max())
                {
                    Console.WriteLine(Books[i].BName);
                }
            }

        }

        //Display Book Suggestion for user after borrowing book
        static void BookSuggestion(int bId)
        {
            
          List<int> SuggestedBookIds = new List<int> ();

            //add ids of user who borrow same book in list
            List<int> SimilarPeopleIds = new List<int>();

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
            //add ids of books for users in SimilarPeopleIds in list
            for (int j = 0; j < SimilarPeopleIds.Count; j++)
            {

                for (int i = 0; i < borrowBook.Count; i++)
                {
                    if (borrowBook[i].userId == SimilarPeopleIds[j] && borrowBook[i].BookId != bId)
                    {
                        SuggestedBookIds.Add(borrowBook[i].BookId);

                    }
                }
            }
            //Remove similar books in SuggestedBookIds list
            List<int> FinalSuggestedBookIds = SuggestedBookIds.Distinct().ToList();

            //Display Suggestion list
            if (FinalSuggestedBookIds.Count != 0)
            {
                Console.WriteLine("People who borrowed this book also borrowed with it");
                for (int i = 0; i < FinalSuggestedBookIds.Count; i++)
                {
                    Console.WriteLine(Books[FinalSuggestedBookIds[i]].BName);
                }
            }
        }

        //Handel integer input errors
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

