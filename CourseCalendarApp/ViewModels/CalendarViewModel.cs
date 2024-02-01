using System.Collections.ObjectModel;
using System.Windows.Media;
using CourseCalendarApp.Models;
using Stylet;
using StyletIoC;
using Syncfusion.UI.Xaml.Scheduler;
using Wpf.Ui.Mvvm.Contracts;

namespace CourseCalendarApp.ViewModels;

public class CalendarViewModel(
    IEventAggregator events,
    IContainer ioc,
    ISnackbarService snackbar,
    MainWindowViewModel main)
    : Screen
{
    private readonly IEventAggregator _events = events;
    private readonly IContainer _ioc = ioc;
    private readonly ISnackbarService _snackbar = snackbar;
    private readonly EventColor _backgroundColor = new() { Red = 45, Green = 153, Blue = 255 };
    private readonly DatabaseContext _db = ioc.Get<DatabaseContext>();
    private readonly EventColor _foregroundColor = new() { Red = 255, Green = 255, Blue = 255 };
    private readonly MainWindowViewModel _main = main;
    private CalendarViewModel _view = null!;

    public ScheduleAppointmentCollection Events { get; set; } = new();

    public ScheduleAppointmentCollection GetUserSchedule(User user)
    {
        var events = new ScheduleAppointmentCollection();

        foreach (var course in user.Courses) { }

        return events;
    }

    public void AddEvents(IEnumerable<Event> events)
    {
        foreach (var e in events)
        {
            Events.Add(new ScheduleAppointment
            {
                StartTime             = e.Start.LocalDateTime,
                EndTime               = e.End.LocalDateTime,
                Subject               = e.Title,
                Notes                 = e.Description,
                Id                    = e.Id.ToString(),
                IsAllDay              = e.AllDay,
                RecurrenceRule        = e.RecurrenceRule,
                Foreground            = e.ForegroundColor?.ToBrush() ?? _foregroundColor.ToBrush(),
                AppointmentBackground = e.BackgroundColor?.ToBrush() ?? _backgroundColor.ToBrush()
            });
        }
    }

    public void OnEditorClosed(SfScheduler? sender, AppointmentEditorClosingEventArgs e)
    {
        switch (e.Action)
        {
            case AppointmentEditorAction.Add:
            {
                var appointment = e.Appointment;

                var fg = _db.Set<EventColor>().FirstOrDefault(x => x.Equals(new EventColor(appointment.Foreground)));
                var bg = _db.Set<EventColor>()
                    .FirstOrDefault(x => x.Equals(new EventColor(appointment.AppointmentBackground)));

                var newEvent = new Event
                {
                    AllDay          = appointment.IsAllDay,
                    Description     = appointment.Notes,
                    End             = appointment.EndTime,
                    Start           = appointment.StartTime,
                    RecurrenceRule  = appointment.RecurrenceRule,
                    Title           = appointment.Subject,
                    Private         = false,
                    Locked          = false,
                    Creator         = _main.LoggedInUser,
                    ForegroundColor = new EventColor(appointment.Foreground),
                    BackgroundColor = new EventColor(appointment.AppointmentBackground)
                };

                _db.Events.Add(newEvent);
                _db.SaveChanges();

                appointment.Id                    = newEvent.Id;
                appointment.AppointmentBackground = newEvent.BackgroundColor.ToBrush();
                break;
            }
            case AppointmentEditorAction.Edit:
            {
                var appointment = e.Appointment;
                var newEvent = _db.Events.Find((Guid) appointment.Id);

                if (newEvent == null) return;

                newEvent.AllDay         = appointment.IsAllDay;
                newEvent.Description    = appointment.Notes;
                newEvent.End            = appointment.EndTime;
                newEvent.Start          = appointment.StartTime;
                newEvent.RecurrenceRule = appointment.RecurrenceRule;
                newEvent.Title          = appointment.Subject;

                _db.Events.Update(newEvent);
                _db.SaveChanges();
                break;
            }
            case AppointmentEditorAction.Delete:
            {
                var appointment = e.Appointment;
                var newEvent = _db.Events.Find((Guid) appointment.Id);

                if (newEvent == null) return;

                _db.Events.Remove(newEvent);
                _db.SaveChanges();

                break;
            }
            case AppointmentEditorAction.Cancel:
                return;
            default:
                return;
        }
    }

    protected override void OnViewLoaded()
    {
        _view  = this;
        Events = GenerateRandomAppointments();
    }

    private Brush GetAppointmentForeground(Brush backgroundColor)
    {
        if (backgroundColor.ToString().Equals("#FF8551F2") || backgroundColor.ToString().Equals("#FF5363FA") ||
            backgroundColor.ToString().Equals("#FF2D99FF"))
            return Brushes.White;
        return new SolidColorBrush(Color.FromRgb(51, 51, 51));
    }

    private ScheduleAppointmentCollection GenerateRandomAppointments()
    {
        var WorkWeekDays = new ObservableCollection<DateTime>();
        var WorkWeekSubjects = new ObservableCollection<string>
        {
            "GoToMeeting", "Business Meeting", "Conference", "Project Status Discussion",
            "Auditing", "Client Meeting", "Generate Report", "Target Meeting", "General Meeting"
        };

        var NonWorkingDays = new ObservableCollection<DateTime>();
        var NonWorkingSubjects = new ObservableCollection<string>
        {
            "Go to party", "Order Pizza", "Buy Gift",
            "Vacation"
        };
        var YearlyOccurrance = new ObservableCollection<DateTime>();
        var YearlyOccurranceSubjects = new ObservableCollection<string>
            { "Wedding Anniversary", "Sam's Birthday", "Jenny's Birthday" };
        var MonthlyOccurrance = new ObservableCollection<DateTime>();
        var MonthlyOccurranceSubjects = new ObservableCollection<string>
            { "Pay House Rent", "Car Service", "Medical Check Up" };
        var WeekEndOccurrance = new ObservableCollection<DateTime>();
        var WeekEndOccurranceSubjects = new ObservableCollection<string> { "FootBall Match", "TV Show" };

        var brush = new ObservableCollection<SolidColorBrush>();
        brush.Add(new SolidColorBrush(Color.FromRgb(133, 81, 242)));
        brush.Add(new SolidColorBrush(Color.FromRgb(140, 245, 219)));
        brush.Add(new SolidColorBrush(Color.FromRgb(83, 99, 250)));
        brush.Add(new SolidColorBrush(Color.FromRgb(255, 222, 133)));
        brush.Add(new SolidColorBrush(Color.FromRgb(45, 153, 255)));
        brush.Add(new SolidColorBrush(Color.FromRgb(253, 183, 165)));
        brush.Add(new SolidColorBrush(Color.FromRgb(198, 237, 115)));
        brush.Add(new SolidColorBrush(Color.FromRgb(253, 185, 222)));
        brush.Add(new SolidColorBrush(Color.FromRgb(255, 222, 133)));

        var ran = new Random();
        var today = DateTime.Now;
        if (today.Month == 12)
            today                        = today.AddMonths(-1);
        else if (today.Month == 1) today = today.AddMonths(1);

        var startMonth = new DateTime(today.Year, today.Month - 1, 1, 0, 0, 0);
        var endMonth = new DateTime(today.Year, today.Month + 1, 30, 0, 0, 0);
        var dt = new DateTime(today.Year, today.Month, 15, 0, 0, 0);
        var day = (int) startMonth.DayOfWeek;
        var CurrentStart = startMonth.AddDays(-day);

        var appointments = new ScheduleAppointmentCollection();
        for (var i = 0; i < 120; i++)
        {
            if (i % 7 == 0 || i % 7 == 6)
            {
                //add weekend appointments
                NonWorkingDays.Add(CurrentStart.AddDays(i));
            }
            else
            {
                //Add Workweek appointment
                WorkWeekDays.Add(CurrentStart.AddDays(i));
            }
        }

        for (var i = 0; i < 120; i++)
        {
            var date = WorkWeekDays[ran.Next(0, WorkWeekDays.Count)].AddHours(ran.Next(9, 17));
            appointments.Add(new ScheduleAppointment
            {
                StartTime             = date,
                EndTime               = date.AddHours(1),
                AppointmentBackground = brush[i % brush.Count],
                Foreground            = GetAppointmentForeground(brush[i % brush.Count]),
                Subject               = WorkWeekSubjects[i % WorkWeekSubjects.Count]
            });
        }
        var j = 0;
        var k = 0;
        var l = 0;

        while (j < YearlyOccurranceSubjects.Count)
        {
            var date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)];
            appointments.Add(new ScheduleAppointment
            {
                StartTime             = date,
                EndTime               = date.AddHours(1),
                AppointmentBackground = brush[1],
                Foreground            = GetAppointmentForeground(brush[1]),
                Subject               = YearlyOccurranceSubjects[j]
            });
            j++;
        }
        while (k < MonthlyOccurranceSubjects.Count)
        {
            var date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(9, 23));
            appointments.Add(new ScheduleAppointment
            {
                StartTime             = date,
                EndTime               = date.AddHours(1),
                AppointmentBackground = brush[k],
                Foreground            = GetAppointmentForeground(brush[k]),
                Subject               = MonthlyOccurranceSubjects[k]
            });
            k++;
        }
        while (l < WeekEndOccurranceSubjects.Count)
        {
            var date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(0, 23));
            appointments.Add(new ScheduleAppointment
            {
                StartTime             = date,
                EndTime               = date.AddHours(1),
                AppointmentBackground = brush[l],
                Foreground            = GetAppointmentForeground(brush[1]),
                Subject               = WeekEndOccurranceSubjects[l]
            });
            l++;
        }

        return appointments;
    }
}