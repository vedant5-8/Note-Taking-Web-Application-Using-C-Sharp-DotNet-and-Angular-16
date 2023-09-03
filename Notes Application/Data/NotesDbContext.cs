using Microsoft.EntityFrameworkCore;
using Notes_Application.Models;

namespace Notes_Application.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions options) : base(options)
        {

        }

        // DbSet
        public DbSet<Notes> Notes { get; set; }
    }
}
