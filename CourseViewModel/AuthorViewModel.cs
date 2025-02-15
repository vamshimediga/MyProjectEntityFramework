using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        // Navigation property for related books
         public ICollection<BookViewModel> Books { get; set; }
    }
}
