using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1s3console
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        public void Add(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            _books.Add(book);
        }

        public bool Remove(Guid id)
        {
            var b = _books.FirstOrDefault(x => x.Id == id);
            if (b == null) return false;
            _books.Remove(b);
            return true;
        }

        public Book? Get(Guid id) => _books.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Book> GetAll() => _books.ToList();

        public bool Update(Book book)
        {
            var idx = _books.FindIndex(x => x.Id == book.Id);
            if (idx == -1) return false;
            _books[idx] = book;
            return true;
        }
    }
}
