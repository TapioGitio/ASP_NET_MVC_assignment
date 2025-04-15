using Domain.Models;

namespace AlphaWebApp.Models;

public class MemberViewModel
{
    public IEnumerable<Member> Members = [];
    public MemberUpdForm UpdateFormData { get; set; } = new();

}
