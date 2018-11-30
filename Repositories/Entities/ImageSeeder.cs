using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Entities
{
    public class ImageSeeder
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
        public string Location { get; set; }
    }
}
