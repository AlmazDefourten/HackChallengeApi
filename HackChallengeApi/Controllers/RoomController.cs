using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
namespace HackChallengeApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using HackChallengeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using HackChallengeApi.AudioHandler;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHubContext<AudioHub> _audioHubContext;
    
    public RoomController(AppDbContext context, UserManager<AppUser> userManager, IHubContext<AudioHub> audioHubContext)
    {
        _context = context;
        _userManager = userManager;
        _audioHubContext = audioHubContext;
    }
    
    // GET: api/room
    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _context.Rooms.ToListAsync();
        return Ok(rooms);
    }

    // GET: api/room/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    // POST: api/room
    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    // DELETE: api/room/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // POST: api/room/{id}/join
    [HttpPost("{id}/join")]
    public async Task<IActionResult> JoinRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User); // Получаем текущего пользователя из контекста аутентификации

        // Проверка наличия пользователя в комнате
        if (room.Users.Contains(user.Username))
        {
            return BadRequest("User already in the room");
        }

        // Обновление информации о пользователе и комнате в базе данных
        user.CurrentRoomId = id;
        room.Users.Add(user.Username);

        _context.Entry(user).State = EntityState.Modified;
        _context.Entry(room).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        
        // Присоединение пользователя к комнате в SignalR Hub
        await _audioHubContext.Clients.Client(HttpContext.Connection.Id).SendAsync("JoinRoom", id);
        
        return Ok("Joined the room");
    }

    // POST: api/room/leave
    [HttpPost("leave")]
    public async Task<IActionResult> LeaveRoom()
    {
        var user = await _userManager.GetUserAsync(User); // Получаем текущего пользователя из контекста аутентификации

        // Проверка наличия пользователя в комнате
        if (user.CurrentRoomId == null)
        {
            return BadRequest("User is not in a room");
        }
        
        var roomId = user.CurrentRoomId.Value;
        var room = await _context.Rooms.FindAsync(roomId);
        if (room == null)
        {
            return BadRequest("User is not in a valid room");
        }

        // Обновление информации о пользователе и комнате в базе данных
        user.CurrentRoomId = null;
        room.Users.Remove(user.Username);

        _context.Entry(user).State = EntityState.Modified;
        _context.Entry(room).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
        
        // Отсоединение пользователя от комнаты в SignalR Hub
        await _audioHubContext.Clients.Client(HttpContext.Connection.Id).SendAsync("LeaveRoom", roomId);
        
        return Ok("Left the room");
    }
    
    // POST: api/room/changeTrack
    [HttpPost("{id}/changeTrack/{audioId}")]
    public async Task<IActionResult> ChangeTrack(int id, int audioId)
    {
        var room = await _context.Rooms.Include(r => r.CurrentTrack).FirstOrDefaultAsync(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        // Поиск аудиофайла по его идентификатору
        var newTrack = await _context.AudioFiles.FindAsync(audioId);
        if (newTrack == null)
        {
            return NotFound("Audio not found");
        }

        // Обновление информации о текущем треке в комнате в базе данных
        room.CurrentTrack = newTrack;
        room.CurrentTrackId = newTrack.Id;
        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        // Отправка события в SignalR
        await _audioHubContext.Clients.Group(id.ToString()).SendAsync("TrackChanged", newTrack);
        
        string filePath = "test.mp3";
        byte[] audioData = await System.IO.File.ReadAllBytesAsync(filePath);
        
        await _audioHubContext.Clients.Group(id.ToString()).SendAsync("ReceiveAudioStream", audioData);
        
        return Ok("Track changed");
    }
    
    // POST: api/room/removeTrack
    [HttpPost("{id}/removeTrack")]
    public async Task<IActionResult> RemoveTrack(int id)
    {
        var room = await _context.Rooms.Include(r => r.CurrentTrack).FirstOrDefaultAsync(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        // Удаление информации о текущем треке в комнате в базе данных
        room.CurrentTrack = null;
        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        // Отправка события в SignalR
        await _audioHubContext.Clients.Group(id.ToString()).SendAsync("TrackRemoved");
        
        return Ok("Track removed");
    }
}