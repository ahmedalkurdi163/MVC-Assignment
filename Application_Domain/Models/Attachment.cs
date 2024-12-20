using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Domain.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public required string Title { get; set; }
        public required string Path { get; set; }
        public required FileType FileType { get; set; }
    }
    public enum FileType
    {
        Jpg,
        Png,
        Gif,
        Bmp,
        Tiff
    }
}