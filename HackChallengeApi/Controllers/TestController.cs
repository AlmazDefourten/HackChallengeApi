using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackChallengeApi.AudioHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HackChallengeApi.Models;
using Microsoft.AspNetCore.Cors;

namespace HackChallengeApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class TestController(AppDbContext context) : ControllerBase
    {
        // GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestClass>>> GetTestClass()
        {
            return await context.TestClass.ToListAsync();
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestClass>> GetTestClass(int id)
        {
            var testClass = await context.TestClass.FindAsync(id);

            if (testClass == null)
            {
            }

            return new TestClass {Id = 1};
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestClass(int id, TestClass testClass)
        {
            if (id != testClass.Id)
            {
                return BadRequest();
            }

            context.Entry(testClass).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestClass>> PostTestClass(TestClass testClass)
        {
            context.TestClass.Add(testClass);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTestClass", new { id = testClass.Id }, testClass);
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestClass(int id)
        {
            var testClass = await context.TestClass.FindAsync(id);
            if (testClass == null)
            {
                return NotFound();
            }

            context.TestClass.Remove(testClass);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestClassExists(int id)
        {
            return context.TestClass.Any(e => e.Id == id);
        }
    }
}
