using Application_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class TVShowUpdateModel
    {
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Rating Rating { get; set; }
        public string? URL { get; set; }
        public bool IsActive { get; set; }
        public List<TVShowLanguages> languages { get; set; } = new List<TVShowLanguages>();
        /* public Attachment? Attachment_img { get; set; }
         public int AttachmentId { get; set; }*/
        /*[Display(Name = "Upload Image")]*/
        /*  public IFormFile ImageFile { get; set; }*/
    }
}