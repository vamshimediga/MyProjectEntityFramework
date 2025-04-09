using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public DateTime UploadedDate { get; set; }

        // Foreign Key
        public int UserProfileId { get; set; }

        // Navigation Property
        public UserProfile UserProfile { get; set; }
    }
}
