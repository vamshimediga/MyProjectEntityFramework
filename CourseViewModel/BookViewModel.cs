using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; } // Foreign Key to Author

        // Navigation property for the related author
        public AuthorViewModel Author { get; set; }
    }
}
