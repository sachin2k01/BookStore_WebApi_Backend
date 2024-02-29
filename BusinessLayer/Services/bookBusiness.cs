using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class bookBusiness:IBookBusiness
    {
        private readonly IBookRepo _ibookRepo;
        public bookBusiness(IBookRepo bookRepo)
        {
            _ibookRepo = bookRepo;
        }
        public BookModel AddBooks(BookModel book)
        {
            return _ibookRepo.AddBooks(book);
        }
        public List<bookEntity> AllBook()
        {
            return _ibookRepo.AllBook();
        }

        public bookEntity GetBook(int bookId)
        {
            return _ibookRepo.GetBook(bookId);
        }
    }
}
