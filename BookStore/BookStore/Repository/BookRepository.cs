using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();

        }

        public List<BookModel> SearchBook(string title , string AuthorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(AuthorName)).ToList();

        }


        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel (){Id=1, Title="MVC", Author="Ahmed"},
                new BookModel (){Id=2, Title=".Net Core", Author="Ahmed"},
                new BookModel (){Id=3, Title="C#", Author="Mahmoud"},
                new BookModel (){Id=4, Title="Java", Author="mohamed"},
                new BookModel (){Id=5, Title="PHP", Author="mohamed"}

            };
        }


    }
}
