using System.ComponentModel.DataAnnotations;

namespace MSEducation.AuthenticationManager.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string RepeatedPassword { get; set; }

        public bool Validate()
        {
            var isValid = true;

            if (Password != RepeatedPassword)
                isValid = false;

            return isValid;
        }
    }
}
