using Application_Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class TVShowCreateModel
    {
        public required string Title { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public required Rating Rating { get; set; }
        public required string URL { get; set; }
        /*        public int AttachmentId { get; set; }*/
        public List<int> languages { get; set; } = new List<int>();

        // إضافة الخاصية لاستقبال ملف الصورة
        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }

}