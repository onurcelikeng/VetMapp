using System.Collections.Generic;

namespace VetMapp.Models
{
    public class InformationModel
    {
        public int Position { get; set; }

        public string Name { get; set; }

        public string SubName { get; set; }

        public bool ForLovers { get; set; }

        public bool ForVets { get; set; }

        public IList<object> Titles { get; set; }

        public IList<object> HtmlUrls { get; set; }

        public string Logo { get; set; }
    }
}