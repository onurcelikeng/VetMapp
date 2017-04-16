namespace VetMapp.Helpers
{
    public class PetHelper
    {

        public static string PetImage(string kind)
        {
            string path;

            if (kind == "Kedi") path = "ms-appx:///Assets/Pets/cat.png";
            else if (kind == "Köpek") path = "ms-appx:///Assets/Pets/dog.png";
            else if (kind == "Kuş") path = "ms-appx:///Assets/Pets/bird.png";
            else path = "ms-appx:///Assets/Pets/other.png";

            return path;
        }

        public static string[] AnimalKinds()
        {
            string[] animals = { "Kedi", "Köpek", "Kuş", "Diğer" };
            return animals;
        }

        public static string[] catBreeds()
        {
            string[] array =
            {
                    "Abyssinian",
                    "American Bobtail",
                    "American Curl",
                    "American Keuda",
                    "American Shorthair",
                    "American Wirehair",
                    "Ankara Kedisi",
                    "Australian Mist",
                    "Balinese",
                    "Bengal",
                    "Birman",
                    "Bombay",
                    "Brazilian Shorthair",
                    "British Shorthair",
                    "Burmese",
                    "Burmilla",
                    "California Spangled",
                    "Chartreux",
                    "Chinchilla",
                    "Colorpoint Shorthair",
                    "Cornish Rex",
                    "Cymric",
                    "Devon Rex",
                    "Egyptian Mau",
                    "European Burmese",
                    "European Shorthair",
                    "Exotic Shorthair",
                    "Havana Brown",
                    "Himalayan",
                    "Honey Bear",
                    "Japanese Bobtail",
                    "Javanese",
                    "Kashmir",
                    "Korat",
                    "Laperm",
                    "Maine Coon",
                    "Manx",
                    "Mojave Spotted",
                    "Munchkin",
                    "Nebelung",
                    "Norwegian Forest",
                    "Ocicat",
                    "Oriental Longhair",
                    "Oriental Shorthair",
                    "Persian",
                    "Pixie Bob",
                    "Ragamuffin",
                    "Ragdoll",
                    "Russian Blue",
                    "Savannah",
                    "Scottish Fold Longhair",
                    "Scottish Fold Shorthair",
                    "Selkirk Rex",
                    "Sokak Kedisi",
                    "Siamese",
                    "Siberian",
                    "Singapura",
                    "Siyam",
                    "Snowshoe",
                    "Sokoke",
                    "Somali",
                    "Sphynx",
                    "Tiffanie",
                    "Tekir",
                    "Tonkinese",
                    "Van Kedis",
                    "York Chocolate"
            };
            return array;
        }

        public static string[] dogBreeds()
        {
            string[] array =
            {
                "Afgan Tazısı",
                "Airedale Teriyeri",
                "Akita",
                "Alaska Malamut",
                "Alman Tel Tüylü Pointer",
                "Alman Kurdu",
                "Amerikan Cocker Spaniel",
                "Avcı Basset",
                "Avustralya Çobanı",
                "Avustralya Sürü Köpeği",
                "Avustralya Teriyeri",
                "Avustralyalı İpek Teriyer",
                "Avustralyalı Kelpie",
                "Avustralyalı Kısa Tüylü Sürü Köpeği",
                "Basenji",
                "Beagle",
                "Bedlington Teriyeri",
                "Belçika Çoban Köpeği",
                "Bernese Dağ Köpeği",
                "Bichon Frise",
                "Bloodhound",
                "Border Koli",
                "Border Teriyeri",
                "Bordo Köpeği",
                "Borzoi",
                "Boston Teriyeri",
                "Bouvier Des Flandres",
                "Boxer",
                "Briard",
                "Britanya Bulldogu",
                "Britanyalı Basset Fauve",
                "Brittany",
                "Brüksel Griffon",
                "Bull Teriyer",
                "Bullmastiff",
                "Cairn Teriyeri",
                "Cavalier King Charles Spaniel",
                "Chesapeake Bay Retriever",
                "Chihuahua",
                "Clumber Spaniel",
                "Cocker Spaniel",
                "Curly Coated Retriever",
                "Çinli Crested",
                "Çov Çov",
                "Dachshund",
                "Dalmaçyalı",
                "Dandie Dinmont Teriyeri",
                "Deerhound",
                "Doberman",
                "Düz Tüylü Retriever",
                "Eski İngiliz Sürü Köpeği",
                "Field Spaniel",
                "Finnish Spitz",
                "Fox Teriyer",
                "Foxhound",
                "French Bulldog",
                "Golden Retriever",
                "Gordon Setter",
                "Great Dane",
                "Greyhound",
                "Hungarian Vizsla",
                "Irish Setter",
                "Irish Su Spanieli",
                "Irish Teriyer",
                "Irish Wolfhound",
                "İngiliz Oyuncak Teriyeri",
                "İngiliz Setter",
                "İngiliz Springer Spaniel",
                "İskoç Teriyeri",
                "İsveçli Vallhund",
                "İtalyan Greyhound",
                "Jack Russell",
                "Japon Chin",
                "Japon Spitz",
                "Keeshond",
                "Kısa Tüylü Alman Pointer",
                "King Charles Spaniel",
                "Koli",
                "Labrador",
                "Lakeland Teriyeri",
                "Lhasa Apso",
                "Lowchen",
                "Maltese",
                "Maremma Sürü Köpeği",
                "Mastiff",
                "Minyatür Pinscher",
                "Munsterlander",
                "Newfoundland",
                "Norfolk Teriyeri",
                "Norwich Teriyeri",
                "Nova Scotia Duck Tolling Retriever",
                "Papillon",
                "Pekingese",
                "Petit Basset Griffon Vendéen",
                "Pharaoh (Firavun) Hound",
                "Pirene Dağ Köpeği",
                "Pointer",
                "Pomeranian",
                "Poodle",
                "Portekiz Su Köpeği",
                "Pug",
                "Puli",
                "Rodoslu Ridgeback",
                "Rottweiler",
                "Saint Bernard",
                "Sakallı Koli",
                "Saluki",
                "Samoyed",
                "Schipperke",
                "Schnauzer",
                "Sealyham Teriyeri",
                "Shar-Pei",
                "Sokak Köpeği",
                "Shetland Sürü Köpeği",
                "Shih Tzu",
                "Sibiryaly Husky",
                "Skye Teriyer",
                "Staffordshire Bull Teriyer",
                "Sussex Spaniel",
                "Tibetli Spaniel",
                "Tibetli Teriyer",
                "Weimaraner",
                "Welsh Corgi",
                "Welsh Springer Spaniel",
                "West Highland Beyaz Teriyer",
                "Whippet",
                "Yorkshire Terrier",
                "Yumuşak Tüylü Wheaten Teriyer"
            };
            return array;
        }

        public static string[] birdBreeds()
        {
            string[] array =
            {
                 "Ara (Macaw) Papağanı",
                 "Cennet Papağanı",
                 "Gri Papağan",
                 "Güvercin",
                 "Kakadu Papağanı",
                 "Kakariki",
                 "Kırmızı Arkalı Papağan",
                 "Kumru",
                 "Lori",
                 "Muhabbet Kuşu",
                 "Rosella",
                 "Sarı Alınlı Papağan",
                 "Şemsiye Kakadu",
                 "Senegal Papağanı",
                 "Sultan Papağanı",
                 "Turuncu Kanatlı Papağan"
            };

            return array;
        }

        public static string[] otherBreeds()
        {
            string[] array = { "Hamster", "Örümcek", "Yılan", "Kaplumbağa", "Tavşan" };

            return array;
        }
    }
}