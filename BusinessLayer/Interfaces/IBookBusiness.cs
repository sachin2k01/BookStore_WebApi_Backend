using ModelLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBookBusiness
    {
        public BookModel AddBooks(BookModel book);
        public List<bookEntity> AllBook();
        public bookEntity GetBook(int bookId);
    }
}
