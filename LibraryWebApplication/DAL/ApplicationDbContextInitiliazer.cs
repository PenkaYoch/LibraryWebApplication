using LibraryWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWebApplication.DAL
{
    public class ApplicationDbContextInitiliazer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var writers = new List<Writer>
            {
            new Writer{WriterId=1000, FirstName="Carson",LastName="Alexander",UserName="username"},
            new Writer{WriterId=1001, FirstName="Meredith",LastName="Alonso", UserName="username"},
            new Writer{WriterId=1002, FirstName="Arturo",LastName="Anand",    UserName="username"},
            new Writer{WriterId=1003, FirstName="Gytis",LastName="Barzdukas", UserName="username"},
            new Writer{WriterId=1004, FirstName="Yan",LastName="Li",          UserName="username"}
            };

            writers.ForEach(s => context.Writers.Add(s));
            context.SaveChanges();
            var genres = new List<Genre>
            {
            new Genre{GenreId=1050,GenreName="poem",    },
            new Genre{GenreId=4022,GenreName="novel"},
            new Genre{GenreId=4041,GenreName="Macroeconomics"},
            new Genre{GenreId=1045,GenreName="Calculus"     }
            };
            genres.ForEach(s => context.Genres.Add(s));
            context.SaveChanges();
            var books = new List<Book>
            {
            new Book{Title="Title", ReleaseDate=DateTime.Now.Date, WriterId=1000, GenreId=1050, Description= "descr"},
            new Book{Title="Title", ReleaseDate=DateTime.Now.Date, WriterId=1000, GenreId=1050, Description= "descr"},
            new Book{Title="Title", ReleaseDate=DateTime.Now.Date, WriterId=1000, GenreId=1050, Description= "descr"},
            new Book{Title="Title", ReleaseDate=DateTime.Now.Date, WriterId=1000, GenreId=1050, Description= "descr"},
            new Book{Title="Title", ReleaseDate=DateTime.Now.Date, WriterId=1000, GenreId=1050, Description= "descr"}
            };
            books.ForEach(s => context.Books.Add(s));
            context.SaveChanges();
        }
    }
}