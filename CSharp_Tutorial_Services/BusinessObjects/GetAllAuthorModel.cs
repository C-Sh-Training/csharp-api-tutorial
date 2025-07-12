using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.BusinessObjects
{
    public class GetAllAuthorModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Biography cannot exceed 500 characters.")]
        public string? Biography { get; set; }

        [Required(ErrorMessage = "DOB is required")]
        public DateTime DateOfBirth { get; set; }
    }
}
