using DatingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Data
{
    public class DataContext : DbContext
    {
        // `: base` "chains" ctor of this class to that of the parent class
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        // need to tell EF about our entities, require properties (DbSet)
        // name becomes the sql table name
        public DbSet<Value> Values { get; set; }
    }
}