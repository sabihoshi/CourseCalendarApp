namespace CourseCalendarApp.Models;

public class User
{
    public Guid Id { get; set; }

    public virtual ICollection<Course>? Courses { get; set; }

    public virtual ICollection<Event>? Events { get; set; }

    public virtual Section? Section { get; set; }

    public string AccessType { get; set; } = "User";

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;
}

public class IgnoredEvent
{
    public Guid Id { get; set; }

    public virtual ICollection<Event>? Events { get; set; }
}