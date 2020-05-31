using System;
using System.Collections.Generic;
using DatingSite.API.Models;

namespace DatingSite.API.Dtos
{
    public class UserProfile
    {
        
        public int Id { get; set; }

        public string Username { get; set; }

        public string Gender { get; set; }

        public string KnownAs { get; set; }

        public int Age { get; set; }

        public DateTime Created { get; set; }

        public DateTime WasActive { get; set; }

        public string Intro { get; set; }

        public string LookingFor { get; set; }

        public string InterestedIn { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

        public string PhotoUrl { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}