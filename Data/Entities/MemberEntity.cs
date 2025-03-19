using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class MemberEntity
    {

        public int MemberId { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = null!;

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "varchar(200)")]
        public string Email { get; set; } = null!;

        [Column(TypeName = "varchar(20)")]
        public string? PhoneNumber { get; set; }

        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;

    }
}
