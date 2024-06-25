using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Category : Entity<Guid>
{
    public string CategoryName { get; set; }
    public virtual ICollection<CategoryBook>? CategoryBooks { get; set; } 
    public Category()
    {
        
    }
    public Category(Guid id,string name):base(id) 
    {
        CategoryName = name;
    }
}
