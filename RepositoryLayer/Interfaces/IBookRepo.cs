﻿using ModelLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRepo
    {
        public BookModel AddBooks(BookModel book);
        public List<bookEntity> AllBook();
        public bookEntity GetBook(int bookId);
    }
}
