using CSharp_Tutorial_Repositories.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Services.BusinessObjects.BookModels
{
    public class AddBookModel
    {
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Author ID is required!")]
        public int AuthorId { get; set; }
        [MaxLength(500, ErrorMessage = "Exceeded max length of description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Publish date can not be null")]
        public DateTime PublishedDate { get; set; }
        [MaxLength(100, ErrorMessage = "Exceeded max length of ISBN")]
        public string ISBN { get; set; } = string.Empty;
        [MaxLength(50, ErrorMessage = "Exceed max length of publisher")]
        public string Publisher { get; set; } = string.Empty;
    }
}
