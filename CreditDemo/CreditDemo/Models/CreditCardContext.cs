using CreditDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCard.Models
{
    public class CreditCardContext : DbContext
    {
        public CreditCardContext(DbContextOptions<CreditCardContext> options) : base(options)
        {
        }

        public DbSet<CreditCardItem> CreditCardItems { get; set; }

    }
}
