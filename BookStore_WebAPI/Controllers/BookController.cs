using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entities;

namespace BookStore_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _ibookBusiness;
        public BookController(IBookBusiness bookBusiness)
        {
            _ibookBusiness = bookBusiness;
        }

        [HttpPost]
        [Route("addBook")]
        public IActionResult AddBooks(BookModel book)
        {
            var books=_ibookBusiness.AddBooks(book);
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("allBooks")]
        public IActionResult AllBooksDetails()
        {
            List<bookEntity> books=_ibookBusiness.AllBook();
            if(books != null)
            {
                return Ok(books);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getBook")]
        public IActionResult getBookById(int bookId)
        {
            var book=_ibookBusiness.GetBook(bookId);
            if(book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
