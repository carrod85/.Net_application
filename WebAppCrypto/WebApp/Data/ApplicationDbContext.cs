using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.domain.Identity;
using WebApp.domain;

namespace WebApp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WebApp.domain.Cesar>? Cesar { get; set; }

    public DbSet<WebApp.domain.DHellman>? DHellman { get; set; }

    public DbSet<WebApp.domain.Vigenere>? Vigenere { get; set; }

    public DbSet<WebApp.domain.Rsa>? Rsa { get; set; }
}