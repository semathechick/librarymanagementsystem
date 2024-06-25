using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class AuthorBook : Entity<Guid>
{
    public virtual Author? Author { get; set; }
    public Guid AuthorId { get; set; }
    public virtual Book? Book { get; set; }
    public Guid BookId { get; set; }
    public AuthorBook()
    {
        
    }

    public AuthorBook(Guid id,Guid authorId, Guid bookId):base(id)
    {
        AuthorId = authorId;
        BookId = bookId;
    }
}
