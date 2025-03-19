namespace Data.Entities
{
    public class AddressEntity
    {

        public int AddressId { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }

        public ICollection<MemberEntity> Members { get; set; } = null!;

    }
}
