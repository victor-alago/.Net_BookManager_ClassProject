namespace ClassProject;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
    


    public class User : IdentityUser
    {
        [PersonalData]
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        
        [PersonalData]
        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        
        [PersonalData]
        [StringLength(250)]
        public string? Address { get; set; }

 
        [PersonalData]
        public DateOnly? DateOfBirth { get; set; }

        
        [StringLength(500)]
        public string? ProfilePictureUrl { get; set; }


        public string FullName => $"{FirstName} {LastName}";
    }

