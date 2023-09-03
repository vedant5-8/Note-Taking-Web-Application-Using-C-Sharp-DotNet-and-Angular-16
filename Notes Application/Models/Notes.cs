using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes_Application.Models
{
    public class Notes
    {
        [Key]
        public Guid NoteId { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string NoteHeading { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string NoteDetails { get; set; }
    }
}
