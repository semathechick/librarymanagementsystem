using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Author : Entity<Guid>
{ 
    public string Name { get; set; }
    public string IdentityNumber { get; set; }
    public virtual ICollection<AuthorBook>? AuthorBooks { get; set; }
    public Author()
    {
        
    }
    public Author(Guid id,string name, string ıdentityNumber):base(id)
    {
        Name = name;
        IdentityNumber = ıdentityNumber;
    }
}
