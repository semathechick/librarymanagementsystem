using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Book : Entity<Guid>
{
    public string Name { get; set; }
    public string ISBN { get; set; }
    public int Page { get; set; }
    public string Language { get; set; }
    public int UnitsInStock { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public Guid PublisherId { get; set; }
    public Guid AuthorId { get; set; }
    public virtual ICollection<AuthorBook>? AuthorBooks { get; set; } 
    public virtual ICollection<CategoryBook>? CategoryBooks { get; set; } 
  
    public virtual ICollection<BookPublisher>? BookPublishers { get; set; }
    public Book()
    {
        
    }
    public Book(Guid id,string name, string isbn) :base(id)
    {
        Name = name;
        ISBN = isbn;
    }
}
