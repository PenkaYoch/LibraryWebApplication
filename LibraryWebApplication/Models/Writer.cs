using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LibraryWebApplication.Models
{
    public class Writer
    {
        public int WriterId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [DisplayName] //("First name: ")
        [StringLength(200, MinimumLength = 1, ErrorMessage = "First name cannot be longer than 200 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [DisplayName] //("Last name: ")
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Last name cannot be longer than 200 characters.")]
        public string LastName { get; set; }
        [DisplayName] //("User name: ")
        [StringLength(100, MinimumLength = 1, ErrorMessage = "User name cannot be longer than 100 characters.")]
        public string UserName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public string BookWriterFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}