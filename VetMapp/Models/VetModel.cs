using Parse;
using System.Collections.Generic;

namespace VetMapp.Models
{
    public class VetModel
    {
        public string Address { get; set; }

        public string CellNumber { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string FacebookPage { get; set; }

        public string InstagramAccount { get; set; }

        public bool IsActive { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsMember { get; set; }

        public bool IsNew { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Status { get; set; }

        public string Town { get; set; }

        public string TwitterAccount { get; set; }

        public string WebAddress { get; set; }

        public string webURLAddress { get; set; }

        public ParseGeoPoint Location { get; set; }

        public IList<object> Services { get; set; }

        public IList<object> WorkingDaysAndHours { get; set; }

        public string Logo { get; set; }

        public IList<object> Images { get; set; }

        public string Color { get; set; }

        public string MapPin { get; set; }

        public StyleModel Style { get; set; }
    }
}