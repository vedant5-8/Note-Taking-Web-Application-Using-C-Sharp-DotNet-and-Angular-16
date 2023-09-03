using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes_Application.Data;
using Notes_Application.Models;

namespace Notes_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly NotesDbContext notesDbContext;
        public NotesController(NotesDbContext notesDbContext)
        {
            this.notesDbContext = notesDbContext;
        }

        // Get All Notes
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await notesDbContext.Notes.ToListAsync();
            return Ok(notes);
        }

        // Get single Note
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetNote")]
        public async Task<IActionResult> GetNote([FromRoute] Guid id)
        {
            var note = await notesDbContext.Notes.FirstOrDefaultAsync(x => x.NoteId == id);
            if (note != null)
            {
                return Ok(note);
            }
            return NotFound("Note not found.");
        }

        // Add New Note
        [HttpPost]
        public async Task<IActionResult> AddNewNote([FromBody] Notes notes)
        {
            notes.NoteId = Guid.NewGuid();
            await notesDbContext.Notes.AddAsync(notes);
            await notesDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNote), new { id = notes.NoteId }, notes);
        }

        // Edit A Existing Note
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditNote([FromRoute] Guid id, [FromBody] Notes notes)
        {
            var existingNote = await notesDbContext.Notes.FirstOrDefaultAsync(x => x.NoteId == id);
            if (existingNote != null)
            {
                existingNote.NoteHeading = notes.NoteHeading;
                existingNote.NoteDetails = notes.NoteDetails;
                await notesDbContext.SaveChangesAsync();
                return Ok(existingNote);
            }
            return NotFound("Note not found.");
        }

        // Delete A Existing Note
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var existingNote = await notesDbContext.Notes.FirstOrDefaultAsync(x => x.NoteId == id);
            if (existingNote != null)
            {
                notesDbContext.Remove(existingNote);
                await notesDbContext.SaveChangesAsync();
                return Ok(existingNote);
            }
            return NotFound("Note not found.");
        }

    }
}
