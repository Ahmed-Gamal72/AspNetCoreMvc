﻿using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;

        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };

            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });

            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }


        public async Task<List<BookModel>> GetAllBooks()
        {
            //var books = new List<BookModel>();
            //var allbooks = await _context.Books.ToListAsync();

            //if(allbooks?.Any() == true )
            //{
            //    foreach (var book in allbooks)
            //    {
            //        books.Add(new BookModel()
            //        {   
            //            Author=book.Author,
            //            Category=book.Category,
            //            Id=book.Id,
            //            LanguageId=book.LanguageId,
            //            Language=book.Language.Name,
            //            Title=book.Title,
            //            TotalPages=book.TotalPages
            //        });

            //    }

            //}
            //return books;

            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Id = book.Id,
                Description = book.Description,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl

            }).ToListAsync();

        }


        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {

            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Id = book.Id,
                Description = book.Description,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl

            }).Take(count).ToListAsync();

        }




        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()

                {
                    Author = book.Author,
                    Category = book.Category,
                    Id = book.Id,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }
                    ).ToList(),
                    BookPdfUrl = book.BookPdfUrl

                }).FirstOrDefaultAsync();

        }

        public List<BookModel> SearchBook(string title, string AuthorName)
        {
            return null;

        }

        public string GetAppName()
        {
            return "Book Store Application ";

        }




    }
}
