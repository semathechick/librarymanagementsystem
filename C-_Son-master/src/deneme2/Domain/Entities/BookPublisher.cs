using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class BookPublisher :Entity<Guid>
{
    public Guid BookId { get; set; }
    public virtual Book? Book { get; set; }
    public Guid PublisherId { get; set; }
    public virtual Publisher? Publisher { get; set; }
    public BookPublisher()
    {
        
    }

    public BookPublisher(Guid bookId, Guid publisherId)
    {
        BookId = bookId;
        PublisherId = publisherId;
    }
}
