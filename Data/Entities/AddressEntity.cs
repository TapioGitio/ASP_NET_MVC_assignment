using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class AddressEntity
    {
        [Key, ForeignKey("Member")]
        public string UserId { get; set; } = null!;

   
        [Column(TypeName = "nvarchar(100)")]
        public string Street { get; set; } = null!;


        [Column(TypeName = "varchar(20)")]
        public string PostalCode { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public string City { get; set; } = null!;

        public MemberEntity Member { get; set; } = null!;
    }
}
