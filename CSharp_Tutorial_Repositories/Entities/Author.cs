using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Tutorial_Repositories.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
