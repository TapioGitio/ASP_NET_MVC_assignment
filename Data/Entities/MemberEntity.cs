using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class MemberEntity : IdentityUser
    {

        public string? ProfileImagePath { get; set; }

        [ProtectedPersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }

        [ProtectedPersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }

        [ProtectedPersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string? JobTitle { get; set; }

        [Required]
        public bool AcceptTerms { get; set; }



        public AddressEntity? Address { get; set; }
    }
}
