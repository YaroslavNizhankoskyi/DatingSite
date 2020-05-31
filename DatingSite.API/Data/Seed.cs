using System.Collections.Generic;
using System.Linq;
using DatingSite.API.Models;
using Newtonsoft.Json;

namespace DatingSite.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            
            if(!context.Users.Any()){
                var json = System.IO.File.ReadAllText("Data/SeedForUsers.json");
                var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);

                foreach(var user in users)
                {   
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }

                context.SaveChanges();
            }

           
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA1())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}