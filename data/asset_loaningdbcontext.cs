using asset_loaning_api.models.domains;
using Microsoft.EntityFrameworkCore;
namespace asset_loaning_api.data

{
    public class asset_loaningdbcontext: DbContext
    {
       public asset_loaningdbcontext(DbContextOptions dbContextOptions) :base(dbContextOptions) 
        { 

        }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<AssetDetails> AssetDetails { get; set; }
        public DbSet<transactions> transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<transactions>()
                .HasOne(x => x.supervisor)
                .WithMany()
                .HasForeignKey(x => x.supervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<transactions>()
                .HasOne(x => x.student)
                .WithMany()
                .HasForeignKey(x => x.studentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<transactions>()
                .HasOne(x => x.returning_supervisor)
                .WithMany()
                .HasForeignKey(x => x.returning_supervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
    
}
