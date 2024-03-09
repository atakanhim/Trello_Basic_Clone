using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using trelloClone.Domain.Entities;
using trelloClone.Domain.Entities.Common;
using trelloClone.Domain.Entities.Identity;

namespace trelloClone.Persistence.Context
{
    public class TrelloCloneDbContext: IdentityDbContext<AppUser, AppRole,string>
    {
        public TrelloCloneDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Board>()
                .HasMany(b => b.Lists)
                .WithOne(l => l.Board)
                .HasForeignKey(l => l.BoardId);

            modelBuilder.Entity<AppUser>()
              .HasMany(b => b.Boards)
              .WithOne(l => l.AppUser)
              .HasForeignKey(l => l.AppUserId);

            modelBuilder.Entity<List>()
                .HasMany(l => l.Cards)
                .WithOne(c => c.List)
                .HasForeignKey(c => c.ListId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var entity in datas)
            {

                _ = entity.State switch
                {
                    EntityState.Added => entity.Entity.CreatedTime = DateTime.UtcNow,
                    EntityState.Modified => entity.Entity.UpdatedTime = DateTime.UtcNow,
                    _ => DateTime.UtcNow

                };


            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
