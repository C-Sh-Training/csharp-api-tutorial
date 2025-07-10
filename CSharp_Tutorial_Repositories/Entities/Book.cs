using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Repositories.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public virtual Author? Author { get; set; }
    }
}
