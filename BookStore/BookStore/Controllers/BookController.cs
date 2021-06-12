using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _LanguageRepository = null;
        public BookController(BookRepository bookRepository , LanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _LanguageRepository = languageRepository;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            var data= await _bookRepository.GetAllBooks();

            return View(data);
        }

        public async Task<ViewResult> GetBook(int id)
        {
            var data=await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookname ,string author)
        {
            return _bookRepository.SearchBook(bookname, author);
        }

        public async Task<ViewResult> AddNewBook(bool isSuccess =false ,int bookId=0)
        {
            var model = new BookModel()
            {
                //Language = "2"
            };

            ViewBag.language = new SelectList(await _LanguageRepository.GetLanguages(), "Id", "Name");

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });

                }
            }

            ViewBag.language = new SelectList(await _LanguageRepository.GetLanguages() ,"Id","Name");

                
            return View();
        }






    }
}
