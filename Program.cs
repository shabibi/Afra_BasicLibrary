using System;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;
// test check out
namespace BasicLibrary
{
    internal class Program
    {
        static List<(int ID, string BName, string BAuthor, int copies,int BorrowedCopies,double Price,string Category,int BorrowPeriod)> Books
            = new List<(int ID, string BName, string BAuthor,  int copies, int BorrowedCopies, double Price, string Category, int BorrowPeriod)>();

        static List<(int AID,string Aname, string email,int password)>Admin = new List<(int AID, string Aname, string email, int password)>();

        static List<(int UID,string UserName,string email, string password)> Users = new List<(int UID, string UserName, string email, string password)>();

        static List<(int UId, int BId,DateTime Bdate, DateTime Rdate, DateTime? ActualRD,int? Rating,bool isReturn)>borrowBook
            = new List<(int UId, int BId, DateTime Bdate, DateTime Rdate, DateTime? ActualRD, int? Rating, bool isReturn)>();

        static List<(int CID, string CName, int NOFBooks)> Categories = new List<(int CID, string CName, int NOFBooks)>();

        static string filePath = "C:\\Users\\Codeline User\\Desktop\\Afra\\lib.txt";
        static string AdminFile = "C:\\Users\\Codeline User\\Desktop\\Afra\\Admin.txt";
        static string UsersFile = "C:\\Users\\Codeline User\\Desktop\\Afra\\Users.txt";
        static string BorrowFile = "C:\\Users\\Codeline User\\Desktop\\Afra\\Borrow.txt";
        static string CategoriesFile = "C:\\Users\\Codeline User\\Desktop\\Afra\\CategoriesFile.txt";

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
            Overdue();
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

            Categories.Clear();
            LoadFromCategoryFile();

            ViewAllBooks();

            int ID = 0;
            string choice;
            
            for (int i = 0; i < Books.Count; i++)
            {
                if (i == Books.Count - 1)
                {
                    ID = Books[i].ID + 1;
                }

            }

            Console.WriteLine("Enter Book Name");
            string name = Console.ReadLine();

            for (int i = 0; i < Books.Count; i++) 
            {
                //if book is already in the library
                if (Books[i].BName == name)
                {
                    Console.WriteLine("\nThis book is already added to the Library.. ");
                    Console.WriteLine("\nDo you want to add more copies?\n(press 1 to confirm)");
                     choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        Console.WriteLine("\nEnter number of copies to add..");
                        int copy = handelIntError(Console.ReadLine());
                        copy += Books[i].copies;

                        //display new updates of exicting book
                        Console.WriteLine("\nNew update for " + Books[i].BName + " is \n");
                        
                        Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,5} {4,20} {5,10} {6,15} {7,15}",
                                      "ID", "Title", "Author", "Copies", "Borrowed Copies", "Price", "Category", "Borrow Period");
                        Console.WriteLine(new string('-', 140));

                        Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,5} {4,20} {5,10:F2} {6,15} {7,15}", Books[i].ID, Books[i].BName, Books[i].BAuthor ,
                            copy , Books[i].BorrowedCopies , Books[i].Price,
                            Books[i].Category ,Books[i].BorrowPeriod);
                        Console.WriteLine(new string('-', 140));
                        

                        Console.WriteLine("\nTo Confirm changes press 1");
                        choice = Console.ReadLine();

                        //confirm updates in number of copies
                        if (choice == "1")
                        {
                            Books[i]=((Books[i].ID, Books[i].BName, Books[i].BAuthor, 
                             copy, Books[i].BorrowedCopies, Books[i].Price,
                            Books[i].Category, Books[i].BorrowPeriod));
                            SaveBooksToFile();
                            Console.WriteLine("The new Copies is saved..");
                        }
                        else
                        {
                            Console.WriteLine("The change not saved..");
                        }

                    }
                    
                    return;
                }
            }
            //enter data of new book to add
            Console.WriteLine("\nEnter Book Author");
            string author = Console.ReadLine();

            Console.WriteLine("\nEnter Book Copies");
            int qun = handelIntError(Console.ReadLine());

            Console.WriteLine("\nEnter Book Price");
            double price = double.Parse(Console.ReadLine());

            //Display Categories list
            Console.WriteLine("\nEnter ID Book Category");
            Console.WriteLine(new string('-', 115));
            for (int i = 0; i < Categories.Count; i++)
            {
                Console.WriteLine(Categories[i].CID + "\t" + Categories[i].CName);
            }
            Console.WriteLine(new string('-', 115));
            //check category existing
            string category = null;
            int catg = handelIntError(Console.ReadLine());
            for (int i = 0; i < Categories.Count; i++)
            {
                if (catg == Categories[i].CID)
                {
                    category = Categories[i].CName;

                    Console.WriteLine("\nEnter Book Borrow Period");
                    int BorrowPeriod = handelIntError(Console.ReadLine());

                    //desplay datat of new book befor confirm to add
                    Console.WriteLine("New Book data is \n");
                    Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,5} {4,20} {5,10} {6,15} {7,15}",
                                  "ID", "Title", "Author", "Copies", "Borrowed Copies", "Price", "Category", "Borrow Period");
                    Console.WriteLine(new string('-', 140));

                    // Data row
                    Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,5} {4,20} {5,10:F2} {6,15} {7,15}",
                                      ID, name, author, qun, "0", price, category, BorrowPeriod);
                    Console.WriteLine(new string('-', 140));

                    Console.WriteLine("To Add this book press 1");
                    choice = Console.ReadLine();

                    //confirm adding new book
                    if (choice == "1")
                    {
                        Books.Add((ID, name, author, qun, 0, price, category, BorrowPeriod));
                        int catgNOFB = Categories[i].NOFBooks +1 ;
                        Categories[i] = (Categories[i].CID, Categories[i].CName, catgNOFB);
                        Console.WriteLine("n of catg"+ catgNOFB);
                        SaveCategoriesToFile();
                        SaveBooksToFile();
                        Console.WriteLine("Book Added Succefully");
                    }
                    else
                    {
                        Console.WriteLine("The change was saved..");
                    }
                    return;
                }
            }
            if (category == null)
            {
                Console.WriteLine("This category not existing in Library ");
            }
           
        }

        //Display All books available in the Library
        static void ViewAllBooks()
        {
            Books.Clear();
            LoadBooksFromFile();
            Console.WriteLine(new string('*', 140));
            Console.WriteLine("\t\t\t\t\t\t Books Menu\n");
            Console.WriteLine(new string('*', 140));
            
            Console.WriteLine("\n{0,-10} {1,-30} {2,-25} {3,5} {4,20} {5,10} {6,15} {7,15}",
                              "ID", "Title", "Author", "Copies", "Borrowed Copies", "Price", "Category", "Borrow Period");
            Console.WriteLine(new string('-', 140));


            for (int i = 0; i < Books.Count; i++)
            {
                Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,5} {4,20} {5,10:F2} {6,15} {7,15}", Books[i].ID, Books[i].BName, Books[i].BAuthor,
                    Books[i].copies, Books[i].BorrowedCopies, Books[i].Price,
                    Books[i].Category, Books[i].BorrowPeriod);
            }
            Console.WriteLine(new string('-', 140));

        }

        //Remove books from the library by entering book's id
        static void RemoveBook()
        {
            Console.Clear();
            Console.WriteLine(new string('*', 140));
            Console.WriteLine("\t\t\t\t\t\t Removing Book\n");
            Console.WriteLine(new string('*', 140));

            Books.Clear();
            LoadBooksFromFile();

            borrowBook.Clear();
            LoadBorrowedBookFile();
            ViewAllBooks();

            bool flge = false;
            string removedBook;

            Console.WriteLine("Enter Book ID");
            int ID = handelIntError(Console.ReadLine());
            int index = -1;
            for (int i = 0; i < Books.Count; i++)
            {
                //index = borrowBook.FindIndex(book => book.BookId == Books[i].ID);
                if (Books[i].ID == ID)
                {
                    if (Books[i].BorrowedCopies > 0)
                    {
                        Console.WriteLine("This Book was Borrowed you cannot Remove it..");
                        return;
                    }
               
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
            Console.WriteLine(new string('*', 140));
            Console.WriteLine("\t\t\t\t\t\t Search for Book\n");
            Console.WriteLine(new string('*', 140));

            ViewAllBooks();

            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine().ToUpper();  

            bool flag=false;
            
            //display all the books that have the same word in its name
            for (int i = 0; i< Books.Count;i++)
            {
                if (Books[i].BName.ToUpper().Contains(name))
                {
                    Console.WriteLine($"\nBook ID: {Books[i].ID}");
                    Console.WriteLine($"Book Name: {Books[i].BName}");
                    Console.WriteLine($"Author: {Books[i].BAuthor}");
                    Console.WriteLine($"Copies: {Books[i].copies}");
                    Console.WriteLine($"Borrowed Copies: {Books[i].BorrowedCopies}");
                    Console.WriteLine();

                    flag = true;
                }
            
            }
            if (flag != true)
            { 
                Console.WriteLine("\nbook not found"); 
                return ;
            }

            //check if user want to borrow
            if (userID != 0)
            {
                Console.WriteLine("Press 1 if you want to Borrow Book ?!");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    BorrowBook();
                }
            }
            

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
                            if (parts.Length == 8)
                            {
                                Books.Add((int.Parse(parts[0]), parts[1].Trim(), parts[2].Trim(),  int.Parse(parts[3]),
                                    int.Parse(parts[4]), double.Parse(parts[5]), parts[6].Trim(), int.Parse(parts[7])));
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

                        writer.WriteLine($"{book.ID}|{book.BName}|{book.BAuthor}|{book.copies}|" +
                            $"{book.BorrowedCopies}|{book.Price}|{book.Category}|{book.BorrowPeriod}");
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
            Console.WriteLine(new string('*', 140));
            Console.WriteLine("\t\t\t\t\t\t Editing Book\n");
            Books.Clear();

            LoadBooksFromFile();
            ViewAllBooks();

            StringBuilder sb = new StringBuilder();
            int index = -1;
            Console.WriteLine("Enter Book ID");
            int ID = handelIntError(Console.ReadLine());

            //Console.Clear();
            Console.WriteLine("{0,-5} {1,-30} {2,-25} {3,8}", "ID", "Title", "Author", "Quantity");
            Console.WriteLine(new string('*', 68)); 
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == ID)
                {
                    index = i;
                    // Display the book list in a formatted view
                   
                        Console.WriteLine("{0,-5} {1,-30} {2,-25} {3,8}",
                            Books[i].ID,
                            Books[i].BName,
                            Books[i].BAuthor,
                            Books[i].copies);
                   
                }
            }
            Console.WriteLine(new string('*', 68)); // Divider line at the end


            Console.WriteLine("Choose number to edit " + Books[index].BName + " :\n");
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
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName == title)
                {
                    Console.WriteLine("The Title of this book is in the Library ");
                    return;
                }
            }

            Console.WriteLine("\nThe new book edite is\n");
            Console.WriteLine(Books[index].ID + "\t" + title + "\t" + Books[index].BAuthor + "\t" + Books[index].copies);
            Console.WriteLine("\nPress 1 to confirm ");

            string confirm = Console.ReadLine();
            if (confirm == "1")
            {
                Books[index] = ((Books[index].ID, title, Books[index].BAuthor, Books[index].copies, Books[index].BorrowedCopies, 
                    Books[index].Price, Books[index].Category, Books[index].BorrowPeriod));
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
            Console.WriteLine(Books[index].ID + "\t" + Books[index].BName + "\t" + Author + "\t" + Books[index].copies);
            Console.WriteLine("\nPress 1 to confirm ");

            string confirm = Console.ReadLine();
            if (confirm == "1")
            {
                Books[index] = ((Books[index].ID, Books[index].BName, Author, Books[index].copies, Books[index].BorrowedCopies,
                    Books[index].Price, Books[index].Category, Books[index].BorrowPeriod));
               
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

            if (qun < 0)
            {
                Console.WriteLine("Not allowed to add negative copies..");
                return;
            }
            Console.WriteLine("\nThe new book edite is");
            Console.WriteLine(Books[index].ID + "\t" + Books[index].BName + "\t" + Books[index].BAuthor + "\t" + qun);
            Console.WriteLine("\nPress 1 to confirm ");

            string confirm = Console.ReadLine();
            if (confirm == "1")
            {
                Books[index] = ((Books[index].ID, Books[index].BName, Books[index].BAuthor, qun, Books[index].BorrowedCopies,
                    Books[index].Price, Books[index].Category, Books[index].BorrowPeriod));
               
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
            Console.WriteLine(new string('*', 140));
            Console.WriteLine("\t\t\t\t\t\t Borrow Book\n");


            Console.WriteLine(new string('*', 140));
            Console.WriteLine("\t\t\t\t\t\t Books Menu\n");
            Console.WriteLine(new string('*', 140));

            Console.WriteLine("\n{0,-10} {1,-30} {2,-25} {3,5} {4,20} {5,10} {6,15} {7,15}",
                              "ID", "Title", "Author", "Copies", "Borrowed Copies", "Price", "Category", "Borrow Period");
            Console.WriteLine(new string('-', 140));

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].copies > 0)
                {
                    Console.WriteLine("{0,-10} {1,-30} {2,-25} {3,5} {4,20} {5,10:F2} {6,15} {7,15}", Books[i].ID, Books[i].BName, Books[i].BAuthor,
                    Books[i].copies, Books[i].BorrowedCopies, Books[i].Price,
                    Books[i].Category, Books[i].BorrowPeriod);
                }
            }
            bool flge = false;
            string choice;
            Console.WriteLine("\nEnter Book ID");
            int ID = handelIntError(Console.ReadLine());

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == ID)
                {
                    if (Books[i].copies != Books[i].BorrowedCopies)
                    {
                        for (int j = 0; j < borrowBook.Count; j++)
                        {
                            if ((borrowBook[j].UId == userID) && (borrowBook[j].BId == ID))
                            {

                                if (borrowBook[j].isReturn == false)
                                {
                                    Console.WriteLine("You are still borrowed this book ..");
                                    Console.WriteLine("Do you want to borrow another book\n 1.yes\n 2.No");
                                    choice = Console.ReadLine();
                                    if (choice == "1")
                                    {
                                        BorrowBook();
                                        return;
                                    }
                                    else if (choice == "2")
                                    {
                                        UserMenu(Users[userID].UserName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sorry your choice was wrong");
                                        return;
                                    }

                                }
                                else
                                {
                                    Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, (Books[i].copies ), (Books[i].BorrowedCopies +1 )
                                         , Books[i].Price, Books[i].Category, Books[i].BorrowPeriod);
                                    Console.WriteLine("\n" + Books[i].BName + " is availabe.");
                                    Console.WriteLine("********************************************************************");

                                    //confirm borrowing..
                                    Console.WriteLine("To confirm book borrowing press 1 ..");
                                    choice = Console.ReadLine();

                                    if (choice == "1")
                                    {
                                        DateTime today = DateTime.Today;

                                        DateTime returnD = today.AddDays(Books[i].BorrowPeriod);
                                        DateTime? Adate = null;
                                        int? rate = null;
                                        //Add borrowing book in borrrow file and save changes in lib file
                                        SaveBooksToFile();

                                        borrowBook[j] = ((userID, ID, today, returnD, Adate, rate, false));
                                        BorrowedBookFile();
                                        Console.WriteLine("Thank you..\nPlease Return it withen " + Books[i].BorrowPeriod + " days..\n");

                                        //Display suggestion list after borrowing
                                        BookSuggestion(ID);
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\tThank You..");
                                        return;
                                    }

                                }
                            }
                        }
                        Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, (Books[i].copies), (Books[i].BorrowedCopies + 1)
                           , Books[i].Price, Books[i].Category, Books[i].BorrowPeriod);
                        Console.WriteLine("\n" + Books[i].BName + " is availabe.");
                        Console.WriteLine("********************************************************************");

                        //confirm borrowing..
                        Console.WriteLine("To confirm book borrowing press 1 ..");
                        choice = Console.ReadLine();

                        if (choice == "1")
                        {
                            DateTime today = DateTime.Today;

                            DateTime returnD = today.AddDays(Books[i].BorrowPeriod);
                            DateTime? Adate = null;
                            int? rate = null;
                            //Add borrowing book in borrrow file and save changes in lib file
                            SaveBooksToFile();

                            borrowBook.Add((userID, ID, today, returnD, Adate, rate, false));
                            BorrowedBookFile();
                            Console.WriteLine("Thank you..\nPlease Return it withen " + Books[i].BorrowPeriod + " days..\n");

                            //Display suggestion list after borrowing
                            BookSuggestion(ID);
                        }
                        else
                        {
                            Console.WriteLine("\tThank You..");
                            return;
                        }
                        flge = true;
                    }
                    else 
                    {
                        Console.WriteLine("Book not availabe for Borroing.");
                        flge = true;
                    }
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
            LoadBorrowedBookFile();

            Console.Clear();
            Console.WriteLine(new string('*', 120));
            Console.WriteLine("\t\t\t\t\t\t Return Book\n");
            Console.WriteLine(new string('*', 120));

            bool flge = false;
            int index = -1;

            //Display list of borrowing books for user
            Console.WriteLine("\nBooks you have borrowed .. ");
            Console.WriteLine(new string('*', 120));
            Console.WriteLine("{0,-10} {1,-30} {2,-12} {3,-12}", "Book ID", "Book Name", "Borrow Date", "Return Date");
            for (int i = 0; i < borrowBook.Count; i++)
            {
                if ((borrowBook[i].UId == userID) && (!borrowBook[i].isReturn))
                {
                    index = Books.FindIndex(book => book.ID == borrowBook[i].BId);
                    
                    Console.WriteLine("{0,-10} {1,-30} {2,-12} {3,-12}" ,borrowBook[i].BId, Books[index].BName , 
                        borrowBook[i].Bdate.ToString("yyyy-MM-dd"), borrowBook[i].Rdate.ToString("yyyy-MM-dd"));
                }
            }
            Console.WriteLine(new string('*', 120));
            Console.WriteLine("\nEnter Book ID");
            int ID = handelIntError(Console.ReadLine());
            for (int i = 0; i < borrowBook.Count; i++)
            {

                if ((borrowBook[i].BId == ID) && (borrowBook[i].UId == userID) && (!borrowBook[i].isReturn))
                {
                    Console.WriteLine("\n" + Books[index].BName + " returned to the library\n\nThank you.");
                    Console.WriteLine("How would you rate the book out of 10 ?");
                    int rate = handelIntError(Console.ReadLine());

                    //update books list
                    index = Books.FindIndex(book => book.ID == borrowBook[i].BId);
                    Books[index] = (Books[index].ID, Books[index].BName, Books[index].BAuthor,  (Books[index].copies),
                        (Books[index].BorrowedCopies - 1), Books[index].Price, Books[index].Category, Books[index].BorrowPeriod);

                    //update borrow books list
                    borrowBook[i] = (borrowBook[i].UId, borrowBook[i].BId,borrowBook[i].Bdate, borrowBook[i].Rdate, DateTime.Now.Date, rate,true);
                    
                    //save changes to files.
                    BorrowedBookFile();
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
            userID = 0;
            int Fixedpassword = 12345;
            int password;
            Admin.Clear();
            AdminsFile();

            Console.WriteLine("\nEnter your email");
            string email = Console.ReadLine();
            if (IsEmailValid(email))
            {
                for (int i = 0; i < Admin.Count; i++)
                {

                    if (Admin[i].email.Contains(email))
                    {
                        Console.WriteLine("\nEnter Admin's Password..");
                        password = handelIntError(Console.ReadLine());
                        if (Fixedpassword == password)
                        {
                            AdminMenu(Admin[i].Aname);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect Admin's Password");
                            Console.WriteLine("\npress enter key to continue");
                            string cont = Console.ReadLine();
                        }

                        return;
                    }
                }
                Console.WriteLine("\nEmail not Registered befor..");
                Console.WriteLine("\nDo you want to add new admin?(enter 1 if yes)");
                int choice = handelIntError(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("Enter Admin Password..");
                    password = handelIntError(Console.ReadLine());
                    if (Fixedpassword == password)
                    {
                        Console.WriteLine("\nEnter your Name..");
                        string name = Console.ReadLine();
                        Admin.Add((Admin.Count, name, email, password));
                        AddNewAdmin();
                        AdminMenu(name);
                    }
                    else
                    {
                        Console.WriteLine("\nIncorrect Admin Password");
                        Console.WriteLine("\npress enter key to continue");
                        string cont = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("\npress enter key to return to main menu..");
                    string cont = Console.ReadLine();
                }
                
            }
            else
            {
                Console.WriteLine("Invalid Email ..");
                Console.WriteLine("\npress enter key to continue");
                string cont = Console.ReadLine();
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
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                Admin.Add((handelIntError(parts[0]), parts[1], parts[2], handelIntError(parts[3])));
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

        //Save new admin name in admin file
        static void AddNewAdmin()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(AdminFile))
                {
                    foreach (var admin in Admin)
                    {
                        writer.WriteLine($"{admin.AID}|{admin.Aname}|{admin.email}|{admin.password}");
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
            
            string password;
            bool flag = false;
            Console.WriteLine("\nEnter your email");
            string email = Console.ReadLine();
            if (IsEmailValid(email))
            {
                for (int i = 0; i < Users.Count; i++)
                {

                    if (Users[i].email.Contains(email))
                    {
                        Console.WriteLine("\nEnter Password..");
                        password = Console.ReadLine();
                        if (Users[i].password != password)
                        {
                            Console.WriteLine("Incorrect Passward");
                            Console.WriteLine("\npress enter key to continue");
                            string cont = Console.ReadLine();
                        }
                        else
                        {
                            userID = Users[i].UID;
                            UserMenu(Users[i].UserName);
                            
                        }
                        return;
                    }
                }

                Console.WriteLine("\nEmail not Registered befor..");
                Console.WriteLine("\nDo you want to add new admin?(enter 1 if yes)");
                int choice = handelIntError(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("Enter Admin Password..");
                    password = Console.ReadLine();

                    Console.WriteLine("\nEnter your Name..");
                    string name = Console.ReadLine();
                    userID = Users.Count + 1;
                    Users.Add((userID, name, email, password));
                    AddNewUser();
                    UserMenu(name);
                    
                }
                
                else
                {
                    Console.WriteLine("\npress enter key to return to main menu..");
                    string cont = Console.ReadLine();
                }

            }
            else
            {
                Console.WriteLine("Invalid Email ..");
                Console.WriteLine("\npress enter key to continue");
                string cont = Console.ReadLine();
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
                        writer.WriteLine($"{user.UID}|{user.UserName}|{user.email}|{user.password}");
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
                            if (parts.Length == 4)
                            {
                                Users.Add((int.Parse(parts[0]), parts[1], parts[2],parts[3]));
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

                        string actualRD = book.ActualRD.HasValue ? book.ActualRD.Value.ToString("yyyy-MM-dd") : "N/A";
                        string rating = book.Rating.HasValue ? book.Rating.Value.ToString() : "N/A";

                        writer.WriteLine($"{book.UId}|{book.BId}|{book.Bdate.ToString("yyyy-MM-dd")}" +
                            $"|{book.Rdate.ToString("yyyy-MM-dd")}|{actualRD}|{rating}|{book.isReturn}");

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
                            if (parts.Length == 7)
                            {
                                // Handle each part with default values if parsing fails
                                
                                int userId = int.Parse(parts[0]);
                                int bId = int.Parse(parts[1]);
                                DateTime borrowDate = DateTime.Parse(parts[2]);
                                DateTime returnDate = DateTime.Parse(parts[3]);
                                DateTime? dueDate = ParseDate(parts[4]);
                                int? fine = ParseInt(parts[5]);
                                bool isReturned = bool.Parse(parts[6]);

                                borrowBook.Add((userId, bId,  borrowDate, returnDate, dueDate, fine, isReturned));
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
            Console.Clear();
            Console.WriteLine(new string('*', 120));
            Console.WriteLine("\t\t\t\t\t\t Library Statistics\n");
            Console.WriteLine(new string('*', 120));

            Books.Clear();
            LoadBorrowedBookFile();
            LoadBooksFromFile();

            Categories.Clear();
            LoadFromCategoryFile();

            borrowBook.Clear();
            LoadBorrowedBookFile();

            int booksInLibrary = Books.Count;
            
            int totalOFBorrowedB = 0;
            for (int i = 0; i < Books.Count; i++)
            {
                booksInLibrary += Books[i].copies;
                totalOFBorrowedB += Books[i].BorrowedCopies;
            }
            int Returned = 0;
            for(int i = 0; i < borrowBook.Count; i++)
            {
                if (borrowBook[i].isReturn == false)
                {
                    Returned++;
                }
            }
            
            Console.WriteLine("{0,-30} {1,10}", "Number of Books in Library:", Books.Count);
            Console.WriteLine("{0,-30} {1,10}", "Number of Copies in Library:", booksInLibrary);
            Console.WriteLine("{0,-30} {1,10}", "Number of Borrowed Books:", totalOFBorrowedB);
            Console.WriteLine("{0,-30} {1,10}", "Number of Returned Books:", Returned);
            Console.WriteLine("{0,-30} {1,10}", "Number of Categories:", Categories.Count);

            Console.WriteLine(new string('-', 115));
            Console.WriteLine("{0,-20} ,{1,10}","Category name"," NO.Book\n");
            for (int i = 0; i < Categories.Count; i++)
            {
                Console.WriteLine("{0,-20} ,{1,10}", Categories[i].CName , Categories[i].NOFBooks);
            }
            Console.WriteLine(new string('-', 115));


            int[] popularBook = new int[Books.Count];
            for (int i = 0; i < borrowBook.Count; i++)
            {
                for (int j = 1; j < popularBook.Length; j++)
                {
                    if (borrowBook[i].BId == j)
                    {
                        popularBook[j]++;
                    }
                }

            }
            Console.WriteLine(new string('-', 115));
            Console.WriteLine("Most borrowed book : \n");
            for (int i = 0; i < popularBook.Length; i++)
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

            List<int> SuggestedBookIds = new List<int>();

            //add ids of user who borrow same book in list
            List<int> SimilarPeopleIds = new List<int>();

            for (int i = 0; i < borrowBook.Count; i++)
            {
                if (bId == borrowBook[i].BId)
                {
                    if (borrowBook[i].UId != userID)
                    {
                        SimilarPeopleIds.Add(borrowBook[i].UId);
                    }
                }
            }
            //add ids of books for users in SimilarPeopleIds in list
            for (int j = 0; j < SimilarPeopleIds.Count; j++)
            {

                for (int i = 0; i < borrowBook.Count; i++)
                {
                    if (borrowBook[i].UId == SimilarPeopleIds[j] && borrowBook[i].BId != bId)
                    {
                        SuggestedBookIds.Add(borrowBook[i].BId);

                    }
                }
            }
            //Remove similar books in SuggestedBookIds list
            List<int> FinalSuggestedBookIds = SuggestedBookIds.Distinct().ToList();
            Console.WriteLine("*******************************************************");
            //Display Suggestion list
            if (FinalSuggestedBookIds.Count != 0)
            {
                Console.WriteLine("People who borrowed this book also borrowed with it\n");
                for (int i = 0; i < FinalSuggestedBookIds.Count; i++)
                {
                    Console.WriteLine(Books[FinalSuggestedBookIds[i]].ID+"\t" +Books[FinalSuggestedBookIds[i]].BName);
                }
            }
            Console.WriteLine();
            Console.WriteLine("\nPress 1 if you want to borrow another book?\n");
            string choise = Console.ReadLine();
            if (choise == "1")
            {
                BorrowBook();
                return;
            }
            else
                return;

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

        //check e_mail validation
        static bool IsEmailValid(string email)
        {
            var regexEmail = new Regex(@"^[^@\s]+@[^@\s]+\.(com)$");
            return (regexEmail.IsMatch(email));
        }

        //Read Category file
        static void LoadFromCategoryFile()
        {
            try
            {
                if (File.Exists(CategoriesFile))
                {
                    using (StreamReader reader = new StreamReader(CategoriesFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 3)
                            {
                                Categories.Add((int.Parse(parts[0]), parts[1].Trim(),int.Parse( parts[2])));
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

        //write to Categories file
        static void SaveCategoriesToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(CategoriesFile))
                {
                    foreach (var catg in Categories)
                    {

                        writer.WriteLine($"{catg.CID}|{catg.CName}|{catg.NOFBooks}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        static void Overdue()
        {
            borrowBook.Clear();
            LoadBorrowedBookFile();
            for (int i = 0; i < borrowBook.Count; i++)
            {

                if ((borrowBook[i].UId == userID) && (!borrowBook[i].isReturn))
                {
                    if (borrowBook[i].Rdate < DateTime.Today)
                    {
                        Console.WriteLine("\nYou have a book that is Overdue..\n");
                        Console.WriteLine("Press enter to Return it ..");
                        Console.ReadLine();

                            ReturnBook();
               
                    }
                }
            }
        }
        static int? ParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : (int?)null;
        }

        static DateTime? ParseDate(string value)
        {
            return DateTime.TryParse(value, out var result) ? result : (DateTime?)null;
        }

       


    }
}

