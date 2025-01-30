using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class PostViewModel
    {
        public int PostId { get; set; }

        public int UserId { get; set; } // Foreign Key

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } // Nullable

        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public UserViewModel User { get; set; }
    }
}
