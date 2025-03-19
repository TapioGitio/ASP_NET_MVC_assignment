namespace Data.Entities
{
    public class MemberEntity
    {

        public int MemberId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;

    }
}
