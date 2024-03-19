using Microsoft.EntityFrameworkCore;

namespace SignalRChat.DataAccess;

public class ChatContext(DbContextOptions<ChatContext> options)  : DbContext(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .Property(a => a.UserName)
            .HasMaxLength(25)
            .IsRequired();

        modelBuilder.Entity<ApplicationUser>()
            .Property(a => a.Password)
            .HasMaxLength(16)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}