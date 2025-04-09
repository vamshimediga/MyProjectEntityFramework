using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        // One-to-Many Relationship
        public ICollection<Photo> Photos { get; set; }
    }
}
