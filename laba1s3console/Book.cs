using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1s3console
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public string? ISBN { get; set; }

        public override string ToString()
        {
            return $"[{Id}] \"{Title}\" — {Author} ({Year}), Genre: {Genre}, ISBN: {ISBN ?? "—"}";
        }
    }
}
