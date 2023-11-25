using Microsoft.AspNetCore.SignalR;

namespace HackChallengeApi.AudioHandler;

public class AudioHub(MinioRepository.MinioRepository minioRepo) : Hub
{
    public async Task JoinRoom(int roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.Group(roomId.ToString()).SendAsync("JoinRoom", roomId);
    }

    public async Task LeaveRoom(int roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.Group(roomId.ToString()).SendAsync("LeaveRoom", roomId);
    }

    public async Task SendAudioStreamRooms(int roomId, byte[] audioData)
    {
        await Clients.Group(roomId.ToString()).SendAsync("ReceiveAudioStream", audioData);
    }
    public async Task SendTestAudio()
    {
        var audioBytes = await minioRepo.GetObject();
        await Clients.All.SendAsync("SendTestAudio", audioBytes);
    }
}