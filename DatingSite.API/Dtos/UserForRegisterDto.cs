using System.ComponentModel.DataAnnotations;

namespace DatingSite.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}