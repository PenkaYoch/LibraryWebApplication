using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryWebApplication.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [DisplayName] //("Genre name: ")
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Genre description cannot be longer than 100.")]
        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}