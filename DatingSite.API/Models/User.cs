using System;
using System.Collections.Generic;

namespace DatingSite.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Gender { get; set; }

        public string KnownAs { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; } 

        public DateTime BirthdayDate { get; set; }

        public DateTime Created { get; set; }

        public DateTime WasActive { get; set; }

        public string Intro { get; set; }

        public string LookingFor { get; set; }

        public string InterestedIn { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
