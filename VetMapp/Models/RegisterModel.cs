namespace VetMapp.Models
{
    public class RegisterModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string nameSurname { get; set; }

        public string phoneNumber { get; set; }

        public bool isVet { get; set; }

        public bool withFacebook { get; set; }
    }
}