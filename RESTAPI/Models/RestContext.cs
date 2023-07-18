using Microsoft.EntityFrameworkCore;

namespace RESTAPI.Models
{
    /// <summary>
    ///     Set Database Context
    /// </summary>
    public class RestContext : DbContext
    {
        public RestContext(DbContextOptions<RestContext> options)
            : base(options)
        {
        }

        public DbSet<CafeModel> CafeItems { get; set; } = null!;
    }
}
