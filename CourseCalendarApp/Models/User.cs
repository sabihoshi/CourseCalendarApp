
namespace CourseCalendarApp.Models;

public class User
{
    public Guid Id { get; set; }

    public virtual ICollection<Course>? Courses { get; set; }

    public virtual Section? Section { get; set; }

    public virtual ICollection<Event>? Events { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string AccessType { get; set; } = "User";
}


public class IgnoredEvent
{
    public Guid Id { get; set; }

    public virtual ICollection<Event>? Events { get; set; }
}