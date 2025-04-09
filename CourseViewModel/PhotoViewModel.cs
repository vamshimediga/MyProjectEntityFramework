using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    public class PhotoViewModel
    {
        public int PhotoId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public DateTime UploadedDate { get; set; }

        // Foreign Key
        public int UserProfileId { get; set; }

        // Navigation Property
        public UserProfileViewModel UserProfile { get; set; }
    }
}
