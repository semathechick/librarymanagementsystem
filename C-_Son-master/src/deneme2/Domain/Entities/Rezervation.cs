using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Rezervation:Entity<Guid>
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime RezervationDate { get; set; }//Rezervasyonun başlayacağı tarih
    public DateTime ExpirationDate { get; set; }//Rezervasyonun biteceği tarih

    public virtual Member Member { get; set; }
    public virtual Book Book { get; set; }
}
