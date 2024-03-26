namespace CourseCalendarApp.ModernWPF.Animation.Transitions;

public static class TransitionCollection
{
    public static List<CaptionedObject<Transition>> Transitions =
    [
        new CaptionedObject<Transition>(new EntranceTransition(), "Entrance"),
        new CaptionedObject<Transition>(new DrillInTransition(), "Drill in"),
        new CaptionedObject<Transition>(new SlideTransition(Direction.FromLeft), "Slide from Left"),
        new CaptionedObject<Transition>(new SlideTransition(Direction.FromRight), "Slide from Right"),
        new CaptionedObject<Transition>(new SlideTransition(Direction.FromBottom), "Slide from Bottom"),
        new CaptionedObject<Transition>(new SuppressTransition(), "Suppress")
    ];
}