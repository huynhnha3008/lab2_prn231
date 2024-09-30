using ODataBookStore.Models;

namespace ODataBookStore.Data
{
    public static class DataSource
    {
        private static IList<Book> listBooks { get; set; }
        public static IList<Book> GetBooks()
        {
            if (listBooks != null)
            {
                return listBooks;
            }
            listBooks = new List<Book>();

            Book book = new Book()
            {
                Id = 1,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#5.0",
                Author = "Mark Micharlis",
                Price = 59.99m,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, THu Duc District"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new Book()
            {
                Id = 2,
                ISBN = "063-6-920-02371-5",
                Title = "Enterprise Games",
                Author = "Michael Hugos",
                Price = 49.99m,
                Location = new Address
                {
                    City = "Bellevue",
                    Street = "Main St."
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "O'Reilly",
                    Category = Category.EBook
                }
            };
            listBooks.Add(book);

            book = new Book()
            {
                Id = 3,
                ISBN = "063-6-920-02371-9",
                Title = "Unity Game Programming",
                Author = "Michael Hugos",
                Price = 49.99m,
                Location = new Address
                {
                    City = "Bellevue",
                    Street = "Main St."
                },
                Press = new Press
                {
                    Id = 3,
                    Name = "Apress",
                    Category = Category.EBook
                }
            };
            listBooks.Add(book);

            
            book = new Book()
            {
                Id = 4,
                ISBN = "978-0-596-52768-5",
                Title = "Learning Python",
                Author = "Mark Lutz",
                Price = 39.99m,
                Location = new Address
                {
                    City = "Bellevue",
                    Street = "Massachusetts Ave"
                },
                Press = new Press
                {
                    Id = 4,
                    Name = "Apress",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new Book()
            {
                Id = 5,
                ISBN = "978-1-59327-950-9",
                Title = "Automate the Boring Stuff with Python",
                Author = "Al Sweigart",
                Price = 29.99m,
                Location = new Address
                {
                    City = "San Francisco",
                    Street = "Market St."
                },
                Press = new Press
                {
                    Id = 5,
                    Name = "No Starch Press",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new Book()
            {
                Id = 6,
                ISBN = "978-0-201-61588-3",
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                Price = 49.99m,
                Location = new Address
                {
                    City = "Boston",
                    Street = "Boylston St."
                },
                Press = new Press
                {
                    Id = 6,
                    Name = "Addison-Wesley",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new Book()
            {
                Id = 7,
                ISBN = "978-1-4919-1889-0",
                Title = "Fluent Python",
                Author = "Luciano Ramalho",
                Price = 54.99m,
                Location = new Address
                {
                    City = "Sao Paulo",
                    Street = "Paulista Ave"
                },
                Press = new Press
                {
                    Id = 7,
                    Name = "O'Reilly",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            book = new Book()
            {
                Id = 8,
                ISBN = "978-0-123-45678-9",
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Price = 44.99m,
                Location = new Address
                {
                    City = "Chicago",
                    Street = "State St."
                },
                Press = new Press
                {
                    Id = 8,
                    Name = "Prentice Hall",
                    Category = Category.Book
                }
            };
            listBooks.Add(book);

            return listBooks;
        }
    }
}
