using EkoStatApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EkoStatApi.Data;

internal class EkoStatContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Entry> Entries => Set<Entry>();

    public EkoStatContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();

            entity.HasOne(e => e.User).WithMany(e => e.Tags)
                .HasForeignKey(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();

            entity.HasMany(e => e.Tags).WithMany(e => e.Articles);
            entity.HasOne(e => e.User).WithMany(e => e.Articles)
                .HasForeignKey(e => e.UserId).IsRequired();
        });

        modelBuilder.Entity<Entry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Comment).IsRequired(false);
            entity.Property(e => e.TimeStamp).IsRequired();
            entity.Property(e => e.Count).IsRequired();
            entity.Property(e => e.CostPerArticle).IsRequired();

            entity.HasOne(e => e.Article).WithMany(e => e.Entries)
                .HasForeignKey(e => e.ArticleId).IsRequired();
            entity.HasOne(e => e.Unit).WithMany(e => e.Entries)
                .HasForeignKey(e => e.UnitId).IsRequired();
            entity.HasOne(e => e.User).WithMany(e => e.Entries)
                .HasForeignKey(e => e.UserId).IsRequired();
        });
    }
}
