using Libary.UI.Models;
using Library.Domain.Entities;
using Library.Library.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Libary.UI.Controllers
{
    public class LibraryController1 : Controller
    {
        private readonly IdentityDbContext _identityDbContext;
        public LibraryController1(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
            
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            var Book = new Book()
            {
                Id = Guid.NewGuid(),
                Name = book.Name,
                Author = book.Author,
                Price = book.Price,

            };
            await _identityDbContext.Books.AddAsync(book);
           await _identityDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _identityDbContext.Books.ToListAsync();
            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var books=await _identityDbContext.Books.FirstOrDefaultAsync(x=>x.Id == id);
            if (books != null)
            {
                var viewModel = new Book()
                {

                    Name = books.Name,
                    Author = books.Author,
                    Price = books.Price,

                };
                return await Task.Run(()=>View("View",viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(Book book)
        {
            var books= await _identityDbContext.Books.FindAsync(book.Id);
            if(books != null)
            {
                var viewModel = new Book()
                {
                    Name = books.Name,
                    Author = books.Author,
                    Price = books.Price,
                    Id = book.Id
                };
                await _identityDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
                
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var book=await _identityDbContext.Books.FindAsync(Id);
            if (book != null)
            {
                _identityDbContext.Books.Remove(book);
                _identityDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
       
    }
}
