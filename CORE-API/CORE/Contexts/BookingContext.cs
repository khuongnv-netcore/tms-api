using CORE_API.Tms.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CORE_API.CORE.Contexts
{
    public class BookingContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingContext(DbContextOptions<BookingContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            BookingEx.OnModelCreating(builder);

            BookingContainerEx.OnModelCreating(builder);

            BookingContainerDetailEx.OnModelCreating(builder);

            Driver.OnModelCreating(builder);

            FixedAsset.OnModelCreating(builder);
        }
    }
}
