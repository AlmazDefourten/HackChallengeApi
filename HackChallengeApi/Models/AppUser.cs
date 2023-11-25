using Microsoft.AspNetCore.Identity;

namespace HackChallengeApi.Models;

public class AppUser : IdentityUser
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public int? CurrentRoomId { get; set; }
}