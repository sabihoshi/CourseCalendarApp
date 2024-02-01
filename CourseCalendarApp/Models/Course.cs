namespace CourseCalendarApp.Models;

public class Course
{
    public virtual Advisor? Advisor { get; set; }

    public Guid Id { get; set; }

    public virtual Section Section { get; set; } = null!;

    public string? CourseCode { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }
}