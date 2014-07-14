using System.Data.Entity.ModelConfiguration;
using Lxs.Core.Domain.Customers;


namespace Lxs.Data.Mapping.Customers
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("Customer");
            this.HasKey(c => c.Id);
            this.Property(u => u.Username).HasMaxLength(100);
            this.Property(u => u.Email).HasMaxLength(100);

        }
    }
}
