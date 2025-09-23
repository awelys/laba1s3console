using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1s3console
{
    public interface IBookRepository
    {
        void Add(Book book);
        bool Remove(Guid id);
        Book? Get(Guid id);
        IEnumerable<Book> GetAll();
        bool Update(Book book);
    }
}
