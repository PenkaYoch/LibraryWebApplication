using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LibraryWebApplication.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [DisplayName] //("Title: ")
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Book title cannot be longer than 300.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        [DisplayName] //("Release date: ")
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [DisplayName] //("Writer: ")
        public int WriterId { get; set; }

        [DisplayName] //("Genre: ")
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DisplayName] //("Description: ")
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Book description cannot be longer than 500.")]
        public string Description { get; set; }

        public virtual Writer BookWriter { get; set; }
        public virtual Genre BookGenre { get; set; }

        //public string BookWriterFullName()
        //{
        //    return BookWriter.FirstName + " " + BookWriter.LastName;
        //}
    }
}