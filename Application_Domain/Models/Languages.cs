using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Domain.Models
{
    public class Languages
    {
        [Key]
        public int LanguageId { get; set; }
        public required string Name { get; set; }
        public List<TVShowLanguages> TVShows { get; set; } = new List<TVShowLanguages>();
    }
}