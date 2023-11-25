namespace HackChallengeApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackChallengeApi.Models;

[Route("api/[controller]")]
[ApiController]
public class AudioFilesController : ControllerBase
{
    private readonly AppDbContext _context;

    public AudioFilesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/AudioFiles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AudioFile>>> GetAudioFiles()
    {
        return await _context.AudioFiles.ToListAsync();
    }

    // GET: api/AudioFiles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AudioFile>> GetAudioFile(int id)
    {
        var audioFile = await _context.AudioFiles.FindAsync(id);

        if (audioFile == null)
        {
            return NotFound();
        }

        return audioFile;
    }

    // POST: api/AudioFiles
    [HttpPost]
    public async Task<ActionResult<AudioFile>> PostAudioFile(AudioFile audioFile)
    {
        _context.AudioFiles.Add(audioFile);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAudioFile), new { id = audioFile.Id }, audioFile);
    }

    // DELETE: api/AudioFiles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAudioFile(int id)
    {
        var audioFile = await _context.AudioFiles.FindAsync(id);
        if (audioFile == null)
        {
            return NotFound();
        }

        _context.AudioFiles.Remove(audioFile);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}