using Parse;
using System;

namespace VetMapp.Models
{
    public class PetModel
    {
        public string ObjectId { get; set; }

        public ParseUser User { get; set; }

        public string Name { get; set; }

        public string Kind { get; set; }

        public string Breed { get; set; }

        public DateTime BirthDate { get; set; }

        public ParseFile Picture { get; set; }

        public string Logo { get; set; } //client
    }
}