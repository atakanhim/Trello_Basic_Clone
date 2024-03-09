using Microsoft.AspNetCore.Identity;


namespace trelloClone.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
        public List<Board> Boards { get; set; }

    }
}
