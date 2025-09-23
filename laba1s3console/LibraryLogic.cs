using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1s3console
{
    public class LibraryLogic
    {
        private readonly IBookRepository _repo;

        public LibraryLogic(IBookRepository repo)
        {
            _repo = repo;
        }

        // CRUD
        public void CreateBook(Book book) => _repo.Add(book);

        public bool DeleteBook(Guid id) => _repo.Remove(id);

        public Book? ReadBook(Guid id) => _repo.Get(id);

        public IEnumerable<Book> ReadAllBooks() => _repo.GetAll();

        public bool UpdateBook(Book book) => _repo.Update(book);

        // --- 2 бизнес-функции, зависящие от сущности "Книга" ---
        // 1) Группировка книг по жанрам
        public IDictionary<string, List<Book>> GroupBooksByGenre()
        {
            return _repo.GetAll()
                        .GroupBy(b => string.IsNullOrWhiteSpace(b.Genre) ? "Unknown" : b.Genre)
                        .ToDictionary(g => g.Key, g => g.OrderBy(b => b.Title).ToList());
        }

        // 2) Поиск книг по автору (точный или частичный, без учета регистра)
        public IEnumerable<Book> FindBooksByAuthor(string authorPart)
        {
            if (string.IsNullOrWhiteSpace(authorPart)) return Enumerable.Empty<Book>();
            var q = authorPart.Trim().ToLowerInvariant();
            return _repo.GetAll().Where(b => b.Author.ToLowerInvariant().Contains(q));
        }

        // Дополнительная полезная функция: книги в указанном диапазоне лет
        public IEnumerable<Book> BooksPublishedBetween(int yearFrom, int yearTo)
        {
            return _repo.GetAll().Where(b => b.Year >= yearFrom && b.Year <= yearTo);
        }
    }
}
