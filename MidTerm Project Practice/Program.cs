using System;
using System.Linq;

public class Program
{
    public class Library
    {
        private List<Book> books = new List<Book>();

        public Book[] SeeAllBooks()
        {
            return books.ToArray();
        }
        public Book[] SeeBooksOfStatusX(bookStatus status)
        {
            List<Book> newBookList = new List<Book>();
            foreach(Book book in books)
            {
                if (book.status == status)
                {
                    newBookList.Add(book);
                }
            }
            return newBookList.ToArray();
        }
        public void WhichList()
        {
            string validNum = "";
            int listChoice = 0;
            bool validList = true;
            bookStatus status = bookStatus.Available;
            while (validList)
            {
                Console.WriteLine();
                Console.WriteLine("Which list would you like to see?");
                Console.WriteLine("Enter 1 to see all books.");
                Console.WriteLine("Enter 2 to see all books available to be checked out.");
                Console.WriteLine("Enter 3 to see all book that are out and need returned.");
                Console.WriteLine("Enter 4 to see all books that need repair.");
                Console.WriteLine("Enter 5 to see all new arrivals.");
                Console.WriteLine("Enter any other number to go back to main menu.");
                validNum = Console.ReadLine();
            
                try
                {
                    listChoice = int.Parse(validNum);
                    validList = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry, that is not valid.");
                    validList = true;
                }
            }
            if (validList == false)
            {
                if (listChoice == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Here is a list of all books:");
                    foreach(Book book in SeeAllBooks())
                    {
                        Console.WriteLine($"{book.bookId}. {book.title} by {book.author}. Status: {book.status}.");
                    }
                }
                else if (listChoice == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Here is all available books:");
                    status = bookStatus.Available;

                    foreach(Book book in SeeBooksOfStatusX(status))
                    {
                        Console.WriteLine($"{book.bookId}. {book.title} by {book.author}");
                    }
                }
                else if (listChoice == 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Here is all books that are out:");
                    status = bookStatus.Out;
                    
                    foreach(Book book in SeeBooksOfStatusX(status))
                    {
                        Console.WriteLine($"{book.bookId}. {book.title} by {book.author}");
                    }
                }
                else if (listChoice == 4)
                {
                    Console.WriteLine();
                    Console.WriteLine("Here is all books that need repair:");
                    status = bookStatus.needsRepair;

                    foreach(Book book in SeeBooksOfStatusX(status))
                    {
                        Console.WriteLine($"{book.bookId}. {book.title} by {book.author}");
                    }
                }
                else if (listChoice == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Here is all books that are coming soon: ");
                    status = bookStatus.notInYet;

                    foreach(Book book in SeeBooksOfStatusX(status))
                    {
                        Console.WriteLine($"{book.bookId}. {book.title} by {book.author}");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Returning to main menu.");
                }
            }
        }
        public Book[] AuthorSearch()
        {
            var booksByAuthor = new List<Book>();
            Console.WriteLine();
            Console.WriteLine("Who is the author you would like to search for?");
            string authorSearch = Console.ReadLine();
            foreach (Book book in books)
            {
                if (book.author.Contains(authorSearch,StringComparison.CurrentCultureIgnoreCase))
                {
                    booksByAuthor.Add(book);
                }
            }
            Console.WriteLine($"Here are the books found by {authorSearch}:");
            return booksByAuthor.ToArray();
        }
        public Book[] KeywordSearch()
        {
            var keyBooks = new List<Book>();
            Console.WriteLine();
            Console.WriteLine("What is the keyword you would like to search for?");
            string keySearch = Console.ReadLine();
            foreach(Book book in books)
            {
                if (book.title.Contains(keySearch,StringComparison.CurrentCultureIgnoreCase))
                {
                    keyBooks.Add(book);
                }
            }
            Console.WriteLine($"Here are the books found with the {keySearch} keyword:");
            return keyBooks.ToArray();
        }
        public void WhichSearch()
        {
            string validNum = "";
            int searchOption = 0;
            bool validSearch = true;
            while (validSearch)
            {
                Console.WriteLine();
                Console.WriteLine("How would you like to search?");
                Console.WriteLine("Enter 1 for search by author.");
                Console.WriteLine("Enter 2 for search by keyword.");
                Console.WriteLine("Enter any other number to return to main menu.");
                validNum = Console.ReadLine();           
            
                try
                {
                    searchOption = int.Parse(validNum);
                    validSearch = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry, that is not valid.");
                    validSearch = true;
                }
            }
            if (validSearch == false)
            {
                if (searchOption == 1)
                {
                    Console.WriteLine();
                    foreach (Book book in AuthorSearch())
                    {
                        Console.WriteLine($"{book.bookId}. {book.title} by {book.author}.  Status: {book.status}");
                    }
                }
                else if (searchOption == 2)
                {
                    Console.WriteLine();
                    foreach (Book book in KeywordSearch())
                    {
                        Console.WriteLine($"{book.bookId}. {book.title} by {book.author}.  Status: {book.status}");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Returning to main menu.");
                }
            }
        }
        public int ValidBook()
        {
            bool numOnList = true;
            int userChoice = 0;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Pick your book by user ID#: (1-15).");
                string isInt = Console.ReadLine();
                bool respIsInt = false;
                respIsInt = int.TryParse(isInt, out userChoice);
                if (respIsInt == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry, that is not a valid number.");
                }
                if (respIsInt)
                {
                    if (userChoice < 16 && userChoice > 0)
                    {
                        numOnList = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Sorry, that is not valid number.");
                        numOnList = true;
                    }
                }
            } while (numOnList);
            return userChoice;
        }

        public void ReturnBook()
        {
            Console.WriteLine();
            Console.WriteLine("Which book would you like to return?");
            int bookIn = ValidBook();

            foreach (Book book in books)
            {
                if (book.bookId == bookIn)
                {
                    if (book.status == bookStatus.Out)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Thank you for returning this book");
                        book.status = bookStatus.Available;
                        break;
                    }
                    else if (book.status != bookStatus.Out)
                    {
                        Console.WriteLine();
                        Console.WriteLine("It looks like that book doesn't currently need to be returned.");
                    }
                }
            }     
        }

        public Library( Book[] newBookInventory )
        {
            if (newBookInventory != null)
                books.AddRange(newBookInventory);
        }
       
        public void SecretMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Hello, welcome to the secret menu!");
            Console.WriteLine("As it turns out, we actually have every book your heart desires, hehe.");
            Console.WriteLine("Which book would you like to check out?");
            int bookOut = ValidBook();

            foreach (Book book in books)
            {
                if ( book.bookId == bookOut)
                {
                    Console.WriteLine();
                    DateTime dueDate = DateTime.Now.Date.AddDays(14);
                    Console.WriteLine($"Great you picked {book.title} by {book.author}, this is due back to us by: " + dueDate.ToString("d"));
                }
            }
        }
        public void CheckOutBook()
        {
            Console.WriteLine();
            Console.WriteLine("Which book would you like to check out?");
            int bookOut = ValidBook();

            foreach (Book book in books)
            {
                if(book.bookId == bookOut)
                {
                    if (book.status == bookStatus.Out)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Sorry, that book is out!");
                    }
                    else if (book.status == bookStatus.needsRepair)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Sorry, that book is being repaired at the moment.");
                    }
                    else if (book.status == bookStatus.notInYet)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Sorry, that has not arrived at our library yet.");
                    }
                    else
                    {
                        book.status = bookStatus.Out;
                        Console.WriteLine();
                        DateTime dueDate = DateTime.Now.Date.AddDays(14);
                        Console.WriteLine($"Great you picked {book.title} by {book.author}, this is due back to us by: " + dueDate.ToString("d"));
                    }
                }
            }
        }
    }
  
    public static void Main(string[] args)
    {
        
        
        List<Book> books = new List<Book>();
        Book one = new Book("C# For Dummies", "David Luxford", bookStatus.Out, 1);
        Book two = new Book("Guide to Mummification", "Doctor Cob", bookStatus.Available, 2);
        Book three = new Book("How Many Eggs is too Many?", "Christy Hartner", bookStatus.needsRepair, 3);
        Book four = new Book("Getting Rid of Stinky Elves!", "Santa Clause", bookStatus.notInYet, 4);
        Book five = new Book("200 Eggs is Definitely too Many", "Christy Hartner", bookStatus.Out, 5);
        Book six = new Book("My Cats are Cute", "Real John Smith", bookStatus.Available, 6);
        Book seven = new Book("Things I Learned in Prison", "Gee Wiz", bookStatus.needsRepair, 7);
        Book eight = new Book("The Healing Properties of Corn", "Doctor Cob", bookStatus.notInYet, 8);
        Book nine = new Book("Hold on, I'm thinking", "Socrates", bookStatus.Available, 9);
        Book ten = new Book("Seven Deadly Sins but I made Another", "Kaige Miller", bookStatus.needsRepair, 10);
        Book eleven = new Book("How to Avoid Being Kidnapped by Kaige Miller", "Kaige Miller", bookStatus.notInYet, 11);
        Book twelve = new Book("How to Name Your Children", "John Smith's Parents", bookStatus.Out, 12);
        Book thirteen = new Book("Why Anime is Overrated and Ruining Society", "ACODESKI", bookStatus.Available, 13);
        Book fourteen = new Book("Why Dragon Ball Z is the Superior Anime", "Dominion", bookStatus.Available, 14);
        Book fifteen = new Book("Oops, I wrote a Book", "Hussein Yassine", bookStatus.Available, 15);

        var bookList = new Book[] { one, two, three, four, five, six, seven, eight, nine, ten, eleven, twelve, thirteen, fourteen, fifteen };
        var avaBooks = new Book[] { two, six, nine, thirteen, fourteen };
        var nonAvaBooks = new Book[] { one, three, four, five, seven, eight, ten, eleven, twelve };
        var outBooks = new Book[] { one, five, twelve };
        Library allBooks = new Library(bookList);

       
        bool terminalLoop = true;
        int optSearch = 0;
        Console.WriteLine("Welcome to The Library Terminal!");
        while (terminalLoop)
        {
            bool searchVal = true;

            while (searchVal) 
            { 
                Console.WriteLine();
                Console.WriteLine("What would you like to do?  Enter the corresponding number.");
                Console.WriteLine("1. See list.");
                Console.WriteLine("2. Search.");
                Console.WriteLine("3. Return book.");
                Console.WriteLine("4. Check out book.");
                Console.WriteLine("5. Quit.");
                string searchOptions = Console.ReadLine();
                                       
                try
                {
                        optSearch = int.Parse(searchOptions);
                        searchVal = false;
                }
                catch (Exception ex)
                {
                        Console.WriteLine();
                        Console.WriteLine("Sorry, that is not valid.");
                        searchVal = true;
                }                
            }

            if (searchVal == false)
            {
                if (optSearch == 1)
                {
                    allBooks.WhichList();
                }
                else if (optSearch == 2)
                {
                    allBooks.WhichSearch();
                }
                else if (optSearch == 3)
                {
                    allBooks.ReturnBook();
                }
                else if (optSearch == 4)
                {
                    allBooks.CheckOutBook();
                }
                else if (optSearch == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Thanks and have a great day!");
                    terminalLoop = false;
                }
                else if (optSearch > 10)
                {
                    allBooks.SecretMenu();
                }    
                else 
                {
                    Console.WriteLine();
                    Console.WriteLine("Never gonna give you up");
                    Console.WriteLine("Never gonna let you down");
                    Console.WriteLine("Never gonna run around and desert you");
                    Console.WriteLine("Never gonna make you cry");
                    Console.WriteLine("Never gonna say goodbye");
                    Console.WriteLine("Never gonna tell a lie and hurt you");
                }
            }
        }
        
        
    }
}
public enum bookStatus
{
    Available,
    Out,
    needsRepair,
    notInYet
}
public class Book
{
    public string title;
    public string author;
    public bookStatus status;
    public int bookId;

    public Book (string newTitle, string newAuthor, bookStatus newStatus, int newBookId)
    {
        title = newTitle;
        author = newAuthor;
        status = newStatus; 
        bookId = newBookId;
    }
   
}
