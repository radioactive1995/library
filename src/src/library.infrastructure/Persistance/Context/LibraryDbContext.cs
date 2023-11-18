using library.domain.Books;
using library.domain.Books.ValueObjects;
using library.domain.Users;
using library.domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace library.infrastructure.Persistance.Context;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                to => to.Value,
                from => BookId.CreateBookId(from).Value);

            entity.OwnsOne(e => e.ISBN, navigationBuilder =>
            {
                navigationBuilder.Property(isbn => isbn.Value).HasColumnName("ISBN");
            });
            entity.OwnsOne(e => e.Status, navigationBuilder =>
            {
                navigationBuilder.Property(status => status.StatusValue).HasColumnName("Status");
                navigationBuilder.Property(status => status.BorrowedUserId).HasColumnName("BorrowedUserId");
            });
            entity.Property(e => e.Author);
            entity.Property(e => e.Title);
            entity.Property(e => e.PublishedDate).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                to => to.Value,
                from => UserId.CreateIUserId(from).Value);

            entity.OwnsOne(e => e.Name, navigationBuilder =>
            {
                navigationBuilder.Property(name => name.UserName).HasColumnName("UserName");
                navigationBuilder.Property(name => name.FirstName).HasColumnName("FirstName");
                navigationBuilder.Property(name => name.LastName).HasColumnName("LastName");
                navigationBuilder.Ignore(name => name.FullName);
            });

            entity.OwnsOne(e => e.Password, navigationBuilder =>
            {
                navigationBuilder.Property(password => password.Value).HasColumnName("Password");
            });

            entity.OwnsOne(e => e.Email, navigationBuilder =>
            {
                navigationBuilder.Property(email => email.Value).HasColumnName("Email");
                navigationBuilder.Property(email => email.IsEmailConfirmed).HasColumnName("IsEmailConfirmed");
            });
        });
    }
}
