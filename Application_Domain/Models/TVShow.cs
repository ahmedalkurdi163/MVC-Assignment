using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Domain.Models
{
    public class TVShow
    {
        [Key]
        public int TVShowId { get; set; }
        public required string Title { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public required Rating Rating { get; set; }
        // رابط البرنامج وليس العنصر 
        public required string URL { get; set; }
        public bool IsActive { get; set; } = true;
        public List<TVShowLanguages> languages { get; set; } = new List<TVShowLanguages>();
        public Attachment Attachment_img { get; set; }
        public int AttachmentId { get; set; }
    }
    public enum Rating
    {
        G,
        PG,
        PG13,
        R,
        NC17
    }
}
