using Parse;
using System;
using VetMapp.Models;
using Windows.ApplicationModel.Email;

namespace VetMapp.Helpers
{
    public sealed class EmailHelper
    {

        public EmailHelper()
        {

        }


        public async void SendEmail(PetModel pet, string vetName, string vetEmail)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Address = vetEmail
            };

            EmailMessage mail = new EmailMessage();

            mail.Subject = "VetMapp - " + ParseUser.CurrentUser["nameSurname"] + " size bir soru sordu !";

            if (pet == null)
            {
                mail.Body = "Merhaba " + vetName + " !" + "\n\n" +
                            "Size aşağıdaki soruyu sormak istiyorum." + "\n\n" +
                            "Sorunuz : " + "\n\n" +
                            "Bu e-posta VetMapp uygulaması aracılığı ile iletilmiştir." + "\n\n";
            }

            else
            {
                mail.Body = "Merhaba " + vetName + " !" + "\n\n" +
                            "Size aşağıdaki soruyu sormak istiyorum." + "\n\n" +
                            "Sorunuz : " + "\n\n" +
                            "Küçük dostumun bilgileri:" + "\n" +
                            "Adı:" + pet.Name + "\n" +
                            "Doğum Tarihi:" + pet.BirthDate.Date.ToString("dd/MM/yyyy") + "\n" +
                            "Irkı:" + pet.Kind + "\n" +
                            "Cinsi:" + pet.Breed + "\n\n" +
                            "Bu e-posta VetMapp uygulaması aracılığı ile iletilmiştir." + "\n\n";
            }

            mail.To.Add(sendTo);
            await EmailManager.ShowComposeNewEmailAsync(mail);

        }

    }
}