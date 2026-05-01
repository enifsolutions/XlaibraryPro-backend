using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XlibraryPro.Domain.Models
{
    public class BookAuthorModel
    {
        public long AuthorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? Role { get; set; }
    }

    public class BookGenreModel
    {
        public long GenreFormId { get; set; }
        public string GenreFormName { get; set; } = string.Empty;
    }
}
