using Parse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetMapp.Helpers;
using VetMapp.Models;

namespace VetMapp.Core
{
    public class DataClient
    {

        public static DataClient Instance
        {
            get
            {
                return new DataClient();
            }
        }


        public async Task<UserModel> Register(RegisterModel model)
        {
            UserModel newUser;

            try
            {
                var user = new ParseUser()
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };
                user["nameSurname"] = model.nameSurname;
                user["phoneNumber"] = model.phoneNumber;
                user["isVet"] = model.isVet;
                user["withFacebook"] = model.withFacebook;

                await user.SignUpAsync();

                newUser = new UserModel()
                {
                    IsSuccess = true,
                    Message = "",
                    User = ParseUser.CurrentUser
                };
            }

            catch (Exception e)
            {
                string message = "";

                if (e.Message == "Account already exists for this username.") message = "kullanıcı adı";
                else if(e.Message == "Email address format is invalid.") message = "e -posta adresi";

                newUser = new UserModel()
                {
                    IsSuccess = false,
                    Message = message,
                    User = null
                };
            }

            return newUser;
        }

        public async Task<UserModel> Login(string username, string password)
        {
            UserModel model;

            try
            {
                var user = await ParseUser.LogInAsync(username, password);

                model = new UserModel()
                {
                    IsSuccess = true,
                    User = user,
                    Message = "Successful"
                };
            }

            catch (Exception e)
            {
                model = new UserModel()
                {
                    IsSuccess = false,
                    User = null,
                    Message = e.Message
                };
            }

            return model;
        }

        public async Task<UserModel> FacebookLogin()
        {
            var user = await ParseFacebookUtils.LogInAsync(new List<string>() { "email", "public_profile" });

            return null;
        }

        public async Task<List<PetModel>> Pets()
        {
            List<PetModel> list = new List<PetModel>();
            var data = await ParseObject.GetQuery("Pets").WhereEqualTo("user", ParseUser.CurrentUser).FindAsync();

            foreach (ParseObject obj in data)
            {
                var model = new PetModel()
                {
                    User = ParseUser.CurrentUser,
                    Name = obj.Get<string>("name"),
                    Kind = obj.Get<string>("kind"),
                    Breed = obj.Get<string>("breed"),
                    BirthDate = obj.Get<DateTime>("birthdate"),
                    Picture = (obj.ContainsKey("picture") == true && obj["picture"] != null) ? obj.Get<ParseFile>("picture") : null
                };

                model.ObjectId = obj.ObjectId;
                model.Logo = (model.Picture != null) ? model.Picture.Url.OriginalString : PetHelper.PetImage(model.Kind);
                
                list.Add(model);
            }

            return list;
        }

        public async Task AddPet(PetModel model)
        {
            ParseObject addedPet = new ParseObject("Pets");

            addedPet["name"] = model.Name;
            addedPet["kind"] = model.Kind;
            addedPet["breed"] = model.Breed;
            addedPet["birthdate"] = model.BirthDate;
            addedPet["picture"] = model.Picture;
            addedPet["user"] = ParseUser.CurrentUser;

            await addedPet.SaveAsync();
        }

        public async Task UpdatePet(PetModel model)
        {
            ParseObject uptadedPet = new ParseObject("Pets");

            uptadedPet.ObjectId = model.ObjectId;
            uptadedPet["name"] = model.Name;
            uptadedPet["kind"] = model.Kind;
            uptadedPet["breed"] = model.Breed;
            uptadedPet["birthdate"] = model.BirthDate;
            if (model.Picture != null) uptadedPet["picture"] = model.Picture;
            uptadedPet["user"] = ParseUser.CurrentUser;

            await uptadedPet.SaveAsync();
        }

        public async Task DeletePet(PetModel model)
        {
            ParseObject deletedPet = new ParseObject("Pets");

            deletedPet.ObjectId = model.ObjectId;
            deletedPet["name"] = model.Name;
            deletedPet["kind"] = model.Kind;
            deletedPet["breed"] = model.Breed;
            deletedPet["birthdate"] = model.BirthDate.Date;
            if(model.Picture != null) deletedPet["picture"] = model.Picture;
            deletedPet["user"] = ParseUser.CurrentUser;

            await deletedPet.DeleteAsync();
        }

        public async Task<List<VetModel>> Vets(ParseGeoPoint geo, int limit)
        {
            List<VetModel> list = new List<VetModel>();

            try
            {
                var query = ParseObject.GetQuery("UserVets").WhereNear("location", geo).Limit(limit);

                var final = await query.FindAsync();

                foreach (ParseObject obj in final)
                {
                    var model = new VetModel()
                    {
                        Address = (obj.ContainsKey("address") == true) ? obj.Get<string>("address") : null,
                        CellNumber = (obj.ContainsKey("cellNumber") == true) ? obj.Get<string>("cellNumber") : null,
                        City = (obj.ContainsKey("city") == true) ? obj.Get<string>("city") : null,
                        Town = (obj.ContainsKey("town") == true) ? obj.Get<string>("town") : null,
                        Email = (obj.ContainsKey("email") == true) ? obj.Get<string>("email") : null,
                        FacebookPage = (obj.ContainsKey("facebookPage") == true) ? obj.Get<string>("facebookPage") : null,
                        InstagramAccount = (obj.ContainsKey("instagramAccount") == true) ? obj.Get<string>("instagramAccount") : null,
                        TwitterAccount = (obj.ContainsKey("twitterAccount") == true) ? obj.Get<string>("twitterAccount") : null,
                        WebAddress = (obj.ContainsKey("webAddress") == true) ? obj.Get<string>("webAddress") : null,
                        webURLAddress = (obj.ContainsKey("webURLAddress") == true) ? obj.Get<string>("webURLAddress") : null,
                        IsActive = (obj.ContainsKey("isActive") == true) ? obj.Get<bool>("isActive") : false,
                        IsConfirmed = (obj.ContainsKey("isConfirmed") == true) ? obj.Get<bool>("isConfirmed") : false,
                        IsMember = (obj.ContainsKey("isMember") == true) ? obj.Get<bool>("isMember") : false,
                        IsNew = (obj.ContainsKey("isNew") == true) ? obj.Get<bool>("isNew") : false,
                        Location = obj.Get<ParseGeoPoint>("location"),
                        Name = (obj.ContainsKey("name") == true) ? obj.Get<string>("name") : null,
                        PhoneNumber = (obj.ContainsKey("phoneNumber") == true) ? obj.Get<string>("phoneNumber") : null,
                        Services = (obj.ContainsKey("services") == true) ? (IList<object>)obj.Get<object>("services") : null,
                        WorkingDaysAndHours = (obj.ContainsKey("workingDaysAndHours") == true) ? (IList<object>)obj.Get<object>("workingDaysAndHours") : null,
                        Logo = (obj.ContainsKey("logo") == true) ? obj.Get<ParseFile>("logo").Url.AbsoluteUri : "ms-appx:///Assets/Logos/flyoutLogo.png",
                        Images = (obj.ContainsKey("images") == true) ? (IList<object>)obj.Get<object>("images") : null
                    };

                    model.Color = VetHelper.setColor(model);
                    model.Status = VetHelper.setStatus(model);
                    model.MapPin = "ms-appx:///Assets/MapPins/" + model.Color + ".png";
                    model.Style = VetHelper.setStyle(model);

                    list.Add(model);
                }

                return list;
            }

            catch (Exception)
            {
                return list;
            }
        }

        public async Task<List<VetModel>> GetVetsInCity(string city)
        {
            List<VetModel> list = new List<VetModel>();

            try
            {
                var query = ParseObject.GetQuery("UserVets").WhereEqualTo("city", city);

                var final = await query.FindAsync();

                foreach (ParseObject obj in final)
                {
                    var model = new VetModel()
                    {
                        Address = (obj.ContainsKey("address") == true) ? obj.Get<string>("address") : null,
                        CellNumber = (obj.ContainsKey("cellNumber") == true) ? obj.Get<string>("cellNumber") : null,
                        City = (obj.ContainsKey("city") == true) ? obj.Get<string>("city") : null,
                        Town = (obj.ContainsKey("town") == true) ? obj.Get<string>("town") : null,
                        Email = (obj.ContainsKey("email") == true) ? obj.Get<string>("email") : null,
                        FacebookPage = (obj.ContainsKey("facebookPage") == true) ? obj.Get<string>("facebookPage") : null,
                        InstagramAccount = (obj.ContainsKey("instagramAccount") == true) ? obj.Get<string>("instagramAccount") : null,
                        TwitterAccount = (obj.ContainsKey("twitterAccount") == true) ? obj.Get<string>("twitterAccount") : null,
                        WebAddress = (obj.ContainsKey("webAddress") == true) ? obj.Get<string>("webAddress") : null,
                        webURLAddress = (obj.ContainsKey("webURLAddress") == true) ? obj.Get<string>("webURLAddress") : null,
                        IsActive = (obj.ContainsKey("isActive") == true) ? obj.Get<bool>("isActive") : false,
                        IsConfirmed = (obj.ContainsKey("isConfirmed") == true) ? obj.Get<bool>("isConfirmed") : false,
                        IsMember = (obj.ContainsKey("isMember") == true) ? obj.Get<bool>("isMember") : false,
                        IsNew = (obj.ContainsKey("isNew") == true) ? obj.Get<bool>("isNew") : false,
                        Location = obj.Get<ParseGeoPoint>("location"),
                        Name = (obj.ContainsKey("name") == true) ? obj.Get<string>("name") : null,
                        PhoneNumber = (obj.ContainsKey("phoneNumber") == true) ? obj.Get<string>("phoneNumber") : null,
                        Services = (obj.ContainsKey("services") == true) ? (IList<object>)obj.Get<object>("services") : null,
                        WorkingDaysAndHours = (obj.ContainsKey("workingDaysAndHours") == true) ? (IList<object>)obj.Get<object>("workingDaysAndHours") : null,
                        Logo = (obj.ContainsKey("logo") == true) ? obj.Get<ParseFile>("logo").Url.AbsoluteUri : "ms-appx:///Assets/Logos/flyoutLogo.png",
                        Images = (obj.ContainsKey("images") == true) ? (IList<object>)obj.Get<object>("images") : null
                    };

                    model.Color = VetHelper.setColor(model);
                    model.Status = VetHelper.setStatus(model);
                    model.MapPin = "ms-appx:///Assets/MapPins/" + model.Color + ".png";
                    model.Style = VetHelper.setStyle(model);

                    list.Add(model);
                }

                return list;
            }

            catch (Exception)
            {
                return list;
            }
        }

        public async Task<List<InformationModel>> Informations()
        {
            List<InformationModel> list = new List<InformationModel>();
            var data = await ParseObject.GetQuery("Information").FindAsync();

            foreach (ParseObject obj in data)
            {
                var model = new InformationModel()
                {
                    Position = obj.Get<int>("position"),
                    Name = obj.Get<string>("name"),
                    SubName = obj.Get<string>("subname"),
                    ForLovers = obj.Get<bool>("forLovers"),
                    ForVets = obj.Get<bool>("forVets"),
                    Titles = (IList<object>)obj.Get<object>("titles"),
                    HtmlUrls = (IList<object>)obj.Get<object>("htmlURLs"),
                    Logo = obj.Get<ParseFile>("logo").Url.AbsoluteUri
                };

                list.Add(model);
            }

            return list;
        }
    }
}