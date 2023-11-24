namespace HackChallengeApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using HackChallengeApi.Models;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly List<Room> _rooms = new List<Room>();
    private int _nextRoomId = 1;
    
    // GET: api/room
    [HttpGet]
    public IActionResult GetRooms()
    {
        return Ok(_rooms);
    }

    // GET: api/room/{id}
    [HttpGet("{id}")]
    public IActionResult GetRoom(int id)
    {
        var room = _rooms.Find(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    // POST: api/room
    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room room)
    {
        room.Id = _nextRoomId++;
        _rooms.Add(room);

        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    // DELETE: api/room/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = _rooms.Find(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        _rooms.Remove(room);

        return NoContent();
    }
}