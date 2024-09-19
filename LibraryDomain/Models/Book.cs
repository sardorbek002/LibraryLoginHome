using Library.Domain.Entities;

namespace Libary.UI.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public long Price { get; set; }


    }
}
