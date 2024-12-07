using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MedicalApp.Identity
{
    public class UserApp : IdentityUser
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string FName { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "NationaID is required")]
        public string NationaID { get; set; }
        public string File { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        public Gender gender { get; set; }
        public enum Gender
        {
            Male = 1,
            Female = 2

        }
    }
}
