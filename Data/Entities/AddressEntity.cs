using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class AddressEntity
    {

        public int AddressId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Street { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? ZipCode { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? City { get; set; }

        public ICollection<MemberEntity> Members { get; set; } = null!;

    }
}
