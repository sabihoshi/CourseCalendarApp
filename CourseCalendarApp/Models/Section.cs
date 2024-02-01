namespace CourseCalendarApp.Models;

public class Section
{
    public Guid Id { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public int Term { get; set; }

    public int Year { get; set; }

    public string Name { get; set; } = null!;
}