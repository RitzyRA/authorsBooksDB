using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AuthorsBooksDB
{
    class Program
    {
        private static AuthorsBooksDBAppDbContext _context = new AuthorsBooksDBAppDbContext();
        static void Main()
        {
            //GetAuthors();
            //GetBooks();
            GetAllAuthorsWBooks();
            AddAuthorWBook(new Author("Rūta", "Šepetys"), new Book("Tarp pilkų debesų"));
            Console.WriteLine("---------------------------------------------------------");
            GetAllAuthorsWBooks();
        }
        static void GetAuthors()
        {
            var al = _context.Authors.ToListAsync();
            foreach (var author in al.Result)
                Console.WriteLine(author);
        }
        static void GetBooks()
        {
            var bl = _context.Books.ToListAsync();
            foreach (var book in bl.Result)
                Console.WriteLine(book);
        }
        static void GetAllAuthorsWBooks()
        {
            var authors = _context.Authors.Include("Books");
            foreach (var author in authors)
            {
                foreach (var book in author.Books)
                {
                    Console.WriteLine($"{ author} - {book}");
                }
            }
        }
        static void AddAuthorWBook(Author author, Book book)
        {
            Author firstAuthor = null;
            try
            {
                firstAuthor = _context.Authors.Include("Books").Where(a => a.Name == author.Name && a.Surname == author.Surname).First();
                
            }
            catch (InvalidOperationException e) when (e.Message.Contains("Sequence contains no elements"))
            {
                _context.Authors.Add(author);
                _context.SaveChanges();
                firstAuthor = _context.Authors.Include("Books").Where(a => a.Name == author.Name && a.Surname == author.Surname).First();
            }
            finally
            {
                firstAuthor.Books.Add(book);
                _context.SaveChanges();
            }
        }
    }
    class AuthorsBooksDBAppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=.; database=AuthorsBooksDB; integrated security=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author(1, "Jonas", "Biliūnas"),
                new Author(2, "Balys", "Sruoga"),
                new Author() { Id = 3, Name = "Žemaitė" }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book(1, "Marti"),
                new Book(2, "Dievų miškas"),
                new Book(3, "Kliudžiau"),
                new Book(4, "Liūdna pasaka")
            );
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<AuthorBook>(
                    ab => ab.HasOne<Author>().WithMany(),
                    ab => ab.HasOne<Book>().WithMany()
                );

            modelBuilder.Entity<AuthorBook>().HasData(
                new AuthorBook() { AuthorId = 1, BookId = 3 },
                new AuthorBook() { AuthorId = 1, BookId = 4 },
                new AuthorBook() { AuthorId = 2, BookId = 2 },
                new AuthorBook() { AuthorId = 3, BookId = 1 }
            );

        }
    }
    class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Book> Books { get; set; }
        public Author(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
        public Author(string name)
        {
            Name = name;
        }
        public Author(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public Author() { }
        public override string ToString()
        {
            return $"{{ Id: {Id} : Name: { Name } { Surname } }}";
        }
    }
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; }

        public Book(int id, string title)
        {
            Id = id;
            Title = title;
        }
        public Book(string title)
        {
            Title = title;
        }
        public override string ToString()
        {
            return $"{{ Id: {Id} : Title: { Title } }}";
        }
    }
    class AuthorBook
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
    }
}
