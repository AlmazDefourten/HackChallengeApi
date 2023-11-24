namespace HackChallengeApi.AudioHandler;

public class AudioHub : Hub
{
    public async Task SendAudioStream(byte[] audioData)
    {
        await Clients.All.SendAsync("ReceiveAudioStream", audioData);
    }
}