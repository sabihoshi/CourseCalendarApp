using Microsoft.EntityFrameworkCore;

namespace CourseCalendarApp.Models;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Event> Events { get; set; } = null!;
}