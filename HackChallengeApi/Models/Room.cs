namespace HackChallengeApi.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Users { get; set; } = new List<string>();
    public int? CurrentTrackId { get; set; }
    public AudioFile? CurrentTrack { get; set; }
}