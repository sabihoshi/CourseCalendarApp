namespace CourseCalendarApp.Models;

public class Course
{
    public virtual Advisor? Advisor { get; set; }

    public virtual Event Schedule { get; set; } = null!;

    public Guid Id { get; set; }

    public int Units { get; set; }

    public string CourseCode { get; set; } = null!;

    public string Section { get; set; } = null!;

    public string Title { get; set; } = null!;
}

public class OrganizationEvent
{
    public virtual Event Schedule { get; set; } = null!;

    public Guid Id { get; set; }

    public string Organization { get; set; } = null!;
}