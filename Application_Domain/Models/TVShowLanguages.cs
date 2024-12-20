using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Domain.Models
{
    // TVShow and Languages كلاس كسر العلاقة بين ال 
    public class TVShowLanguages
    {
        [Key]
        public int TVShowLanguageId { get; set; }
        public int LanguageId { get; set; }
        public Languages Language { get; set; }

        public int TVShowId { get; set; }
        public TVShow TVShow { get; set; }
        public string LanguageName { get; set; }
    }
}
