using System.Windows.Media;

namespace CourseCalendarApp.Models;

public class Event
{
    public bool AllDay { get; set; }

    public bool Locked { get; set; }

    public bool Private { get; set; }

    public DateTimeOffset End { get; set; }

    public DateTimeOffset Start { get; set; }

    public virtual EventColor? BackgroundColor { get; set; }

    public virtual EventColor? ForegroundColor { get; set; }

    public Guid Id { get; set; }

    public string? Description { get; set; }

    public string? RecurrenceRule { get; set; }

    public string? Title { get; set; }

    public virtual User? Creator { get; set; }
}

public class EventColor : IEquatable<EventColor>, IEquatable<Brush>
{
    public EventColor() { }

    public EventColor(Brush brush)
    {
        var color = (Color) brush.GetValue(SolidColorBrush.ColorProperty);

        Red   = color.R;
        Green = color.G;
        Blue  = color.B;
    }

    public byte Blue { get; set; }

    public byte Green { get; set; }

    public byte Red { get; set; }

    public Guid Id { get; set; }

    public bool Equals(Brush? other)
    {
        if (ReferenceEquals(null, other)) return false;
        var color = (Color) other.GetValue(SolidColorBrush.ColorProperty);
        return Red == color.R && Green == color.G && Blue == color.B;
    }

    public bool Equals(EventColor? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Red == other.Red && Green == other.Green && Blue == other.Blue && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((EventColor) obj);
    }

    public static bool operator ==(EventColor? left, EventColor? right) => Equals(left, right);

    public static bool operator !=(EventColor? left, EventColor? right) => !Equals(left, right);

    public Brush ToBrush() => new SolidColorBrush(Color.FromRgb(Red, Green, Blue));

    public override int GetHashCode() => HashCode.Combine(Red, Green, Blue, Id);
}