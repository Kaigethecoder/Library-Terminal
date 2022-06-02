using System;
using System.Linq;

public class Program
{
    public class Library
    {
        private List<Book> books = new List<Book>();
        public void ReturnBook()
        {//how do I display library to do list of all non available books?
            int bookReturnChoice = 0;
            bool whichBook = true;
            bool outOrNot = true;
            while (whichBook)
            {
                Console.WriteLine("Which book would you like to return? Please pick a book that is out by book ID");
                string bookToReturn = Console.ReadLine();
                try
                {
                    bookReturnChoice = Convert.ToInt32(bookToReturn);
                    whichBook = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sorry, that is not valid.");
                    whichBook = true;
                }
                if (whichBook == false)
                {
                    //when I got to this point I realized it may be smarter to use method with returnbook(Book[] books) 
                    //this way is better I think because we can call that SPECIFIC array/list
                    /*foreach (Book book in outBooks)
                     * 
                     * if (book.bookId == bookReturnChoice)
                     *      if(book.status == bookStatus.Out)
                     *      {Console.WriteLine("Thank you for returning the book.");
                     *      outOrNot = false;
                    else                   
                    Console.WriteLine("That book is not currently out.");
                    outOrNot = true;
                    } */
                }
            }
        }

        public Library( Book[] newBookInventory )
        {
            // you could just move the book creation code here
            // That works
            // But imagine, what if you had another library someplace else with different books?
            if (newBookInventory != null)
                books.AddRange(newBookInventory);
        }
       
        public void CheckOutBook()
        {

            string isABook = "";
            int bookOut = 0;
            bool whichBook = true;

            while (whichBook)
            {
                Console.WriteLine("Which book would you like to check out?  Pick a number 1-14.");
                isABook = Console.ReadLine();
                try
                {

                    bookOut = int.Parse(isABook);
                    if (bookOut < 15 && bookOut > 0)
                    {
                        whichBook = false;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that is not valid.");
                        whichBook = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sorry, that is not valid.");
                    whichBook = true;
                }
            }
            foreach (Book book in books)
            {
                if(book.bookId == bookOut)
                {
                    if (book.status == bookStatus.Out)
                    {
                        Console.WriteLine("Sorry, that book is out!");
                    }
                    else if (book.status == bookStatus.needsRepair)
                    {
                        Console.WriteLine("Sorry, that book is being repaired at the moment.");
                    }
                    else if (book.status == bookStatus.notInYet)
                    {
                        Console.WriteLine("Sorry, that has not arrived at our library yet.");
                    }
                    else
                    {
                        DateTime dueDate = DateTime.Now.Date.AddDays(14);
                        Console.WriteLine($"Great you picked {book.title} by {book.author}, this is due back to us by: " + dueDate.ToString("d"));
                    }
                }
            }
        }
    }
    public static void Main(string[] args)
    {
        //create book class with title author and status(check in or out or waiting for new arrival)
        //store books in list of 12 min
        //allow user to display entire list
        // search book by title
        //select a book from the list to check out
        //if checked out, let user know
        //if available, assign due date 2 weeks from check out date
        //allow user to return a book
        
        List<Book> books = new List<Book>();
        Book one = new Book("C# For Dummies", "David Luxford", bookStatus.Out, 1);
        Book two = new Book("Guide to Mummification", "Doctor Cob", bookStatus.Available, 2);
        Book three = new Book("How Many Eggs is too Many?", "Christy Hartner", bookStatus.needsRepair, 3);
        Book four = new Book("Getting Rid of Stinky Elves!", "Santa Clause", bookStatus.notInYet, 4);
        Book five = new Book("200 Eggs is Definitely too Many", "Christy Hartner", bookStatus.Out, 5);
        Book six = new Book("My Cats are Cute", "Real John Smith", bookStatus.Available, 6);
        Book seven = new Book("Things I Learned in Prison", "Gee Wiz", bookStatus.needsRepair, 7);
        Book eight = new Book("The Healing Properties of Corn", "Doctor Cob", bookStatus.notInYet, 8);
        Book nine = new Book("Oops, I wrote a Book", "Socrates", bookStatus.Available, 9);
        Book ten = new Book("Seven Deadly Sins but I made Another", "Kaige Miller", bookStatus.needsRepair, 10);
        Book eleven = new Book("How to Avoid Being Kidnapped by Kaige Miller", "Kaige Miller", bookStatus.notInYet, 11);
        Book twelve = new Book("How to Name Your Children", "John Smith's Parents", bookStatus.Out, 12);
        Book thirteen = new Book("Why Anime is Overrated and Ruining Society", "ACODESKI", bookStatus.Available, 13);
        Book fourteen = new Book("Why Dragon Ball Z is the Superior Anime", "Dominion", bookStatus.Available, 14);

        var bookList = new Book[] { one, two, three, four, five, six, seven, eight, nine, ten, eleven, twelve, thirteen, fourteen };
        var avaBooks = new Book[] { two, six, nine, thirteen, fourteen };
        var nonAvaBooks = new Book[] { one, three, four, five, seven, eight, ten, eleven, twelve };
        var outBooks = new Book[] { one, five, twelve };
        Library allBooks = new Library(bookList);
        // if I make these a library? will it mess up my method?
        //Library allAvaBooks = new Library(avaBooks);
       // Library allnonAva = new Library(nonAvaBooks);

        // So lets say we use an instance of a Library class to represent the library we are working with
        // If there are two branches of the library, then we would create two instances
        // So each instance has all the same functionality, but different books available
        // I need to tell each branch what books it has in it's inventory when I create that object.
        // So therefore the Library constructor takes a Book[].  The main method passes that to the constructor,
        // and then I have a working library.


        Console.WriteLine("Hello, welcome to the library!");
        Console.WriteLine("Would you like to see the book options? (y/n?)");
        string seeListofAll = Console.ReadLine();
        if (seeListofAll.ToUpper() == "Y")
        {
            SeeList(bookList);
        }

        //AFTER YOU WRITE THE DIFFERENT OPTIONS YOU CAN SIMPLIFY TO HOW CAN I HELP TODAY?
        //THEN A LIST OF OPTIONS TO SEARCH BY AUTHOR,KEYWORD, ETC. SEE LIST OF EACH KIND, GET OR RETURN BOOK.
        bool searchVal = true;
        bool searchNumVal = true;
        int optSearch = 0;

        while (searchNumVal)
        {

            Console.WriteLine("Push 1 to search for author.");
            Console.WriteLine("Push 2 to search by title keyword.");
            Console.WriteLine("OR push 3 if you wish not to search.");
            string searchOptions = Console.ReadLine();
            try
            {
                optSearch = int.Parse(searchOptions);
                searchVal = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry, that is not valid.");
                searchVal = true;
            }
            if (searchVal == false)
            {
                if (optSearch == 1)
                {
                    //method for search by author
                    searchNumVal = false;
                }
                else if (optSearch == 2)
                {
                    //method for title keyword search
                    searchNumVal = false;
                }
                else if (optSearch == 3)
                {
                    Console.WriteLine("Understood.");
                    searchNumVal = false;
                }
                else
                {
                    Console.WriteLine("Sorry, that is not valid.");
                    searchNumVal = true;
                }
            }//this loop has not been tested completely and probably needs to be broken down into methods.
        }
        Console.WriteLine("Would you like to return a book? (y/n?)");
        string retBook = Console.ReadLine();
        if (retBook.ToUpper() == "Y")
        {
            Console.WriteLine("Here is the list of books that are out in order by Book ID#");
            foreach (Book book in outBooks)
            {
                Console.WriteLine($"{book.bookId}. {book.title} by {book.author}.");
            }
            //method to return book
        }
    }

    public static void ReturnBook()
    {
        Console.WriteLine("Which book would you like to return?");
        int bookReturn = int.Parse(Console.ReadLine());
        

    }

    public static void SeeList(Book[] listOptions)
    {
        foreach (Book book in listOptions)
        {
            Console.WriteLine($"{book.bookId}. '{book.title}' by {book.author}");
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
        status = newStatus; // might need to do subclass for checked out books for due date
        bookId = newBookId;
    }
    
}