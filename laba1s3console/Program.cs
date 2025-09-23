namespace laba1s3console
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new InMemoryBookRepository();
            var logic = new LibraryLogic(repo);

            // Добавим немного стартовых данных
            seedSampleData(logic);

            Console.WriteLine("=== Library Console App ===");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1: Показать все книги");
                Console.WriteLine("2: Добавить книгу");
                Console.WriteLine("3: Удалить книгу по Id");
                Console.WriteLine("4: Изменить книгу по Id");
                Console.WriteLine("5: Поиск по автору");
                Console.WriteLine("6: Группировка по жанрам");
                Console.WriteLine("0: Выход");
                Console.Write("Ввод: ");
                var k = Console.ReadLine();
                Console.WriteLine();
                switch (k)
                {
                    case "1":
                        foreach (var b in logic.ReadAllBooks()) Console.WriteLine(b);
                        break;
                    case "2":
                        var newBook = readBookFromConsole();
                        logic.CreateBook(newBook);
                        Console.WriteLine("Книга добавлена: " + newBook.Id);
                        break;
                    case "3":
                        Console.Write("Введите Id книги для удаления: ");
                        if (Guid.TryParse(Console.ReadLine(), out var delId))
                        {
                            var ok = logic.DeleteBook(delId);
                            Console.WriteLine(ok ? "Удалено" : "Книга не найдена");
                        }
                        else Console.WriteLine("Неверный формат Id");
                        break;
                    case "4":
                        Console.Write("Введите Id книги для изменения: ");
                        if (Guid.TryParse(Console.ReadLine(), out var upId))
                        {
                            var exist = logic.ReadBook(upId);
                            if (exist == null) { Console.WriteLine("Книга не найдена"); break; }
                            Console.WriteLine("Текущие данные: " + exist);
                            var updated = readBookFromConsole();
                            updated.Id = exist.Id;
                            var res = logic.UpdateBook(updated);
                            Console.WriteLine(res ? "Обновлено" : "Ошибка при обновлении");
                        }
                        else Console.WriteLine("Неверный формат Id");
                        break;
                    case "5":
                        Console.Write("Введите часть имени автора: ");
                        var part = Console.ReadLine() ?? string.Empty;
                        var found = logic.FindBooksByAuthor(part).ToList();
                        if (!found.Any()) Console.WriteLine("Ничего не найдено");
                        else found.ForEach(b => Console.WriteLine(b));
                        break;
                    case "6":
                        var groups = logic.GroupBooksByGenre();
                        foreach (var g in groups)
                        {
                            Console.WriteLine($"Genre: {g.Key} ({g.Value.Count})");
                            foreach (var b in g.Value) Console.WriteLine("  " + b);
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }

        static Book readBookFromConsole()
        {
            Console.Write("Title: "); var title = Console.ReadLine() ?? string.Empty;
            Console.Write("Author: "); var author = Console.ReadLine() ?? string.Empty;
            Console.Write("Genre: "); var genre = Console.ReadLine() ?? string.Empty;
            Console.Write("Year: "); int.TryParse(Console.ReadLine(), out var year);
            Console.Write("ISBN (optional): "); var isbn = Console.ReadLine();
            return new Book { Title = title, Author = author, Genre = genre, Year = year, ISBN = string.IsNullOrWhiteSpace(isbn) ? null : isbn };
        }

        static void seedSampleData(LibraryLogic logic)
        {
            logic.CreateBook(new Book { Title = "Мастер и Маргарита", Author = "М. Булгаков", Genre = "Роман", Year = 1967, ISBN = "978-5-17-0" });
            logic.CreateBook(new Book { Title = "Война и мир", Author = "Л. Толстой", Genre = "Роман", Year = 1869, ISBN = "978-5-02-0" });
            logic.CreateBook(new Book { Title = "Метро 2033", Author = "Д. Глуховский", Genre = "Научная фантастика", Year = 2005 });
            logic.CreateBook(new Book { Title = "Clean Code", Author = "Robert C. Martin", Genre = "Programming", Year = 2008 });
        }
    }
}