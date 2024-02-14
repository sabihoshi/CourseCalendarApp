using System.ComponentModel.DataAnnotations;

namespace CourseCalendarApp.Models;

public class User
{
    public DateTimeOffset? DateOfBirth { get; set; }

    public Guid Id { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public int Term { get; set; }

    public int Year { get; set; }

    public string AccessType { get; set; } = "User";

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? CanvasToken { get; set; }

    public string? Email { get; set; }

    public string? Organization { get; set; }

    public string? Section { get; set; }
}

public class IgnoredEvent
{
    public Guid Id { get; set; }

    public virtual ICollection<Event>? Events { get; set; }
}