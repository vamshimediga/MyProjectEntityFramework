using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        // Navigation property for related books
        public ICollection<Book> Books { get; set; }
    }
}
