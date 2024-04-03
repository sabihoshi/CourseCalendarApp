using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using CanvasApi.Client.Courses.Models;
using CanvasApi.Client.Enrollments.Enums;
using CourseCalendarApp.Canvas;
using CourseCalendarApp.Models;
using CourseCalendarApp.Views;
using CsvHelper;
using CsvHelper.Configuration;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.EntityFrameworkCore;
using Stylet;
using StyletIoC;
using Syncfusion.UI.Xaml.Scheduler;
using Wpf.Ui.Common;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Calendar = Ical.Net.Calendar;

namespace CourseCalendarApp.ViewModels;

public partial class CalendarViewModel(
    IEventAggregator events,
    IContainer ioc,
    ISnackbarService snackbar,
    IDialogService dialog,
    MainWindowViewModel main)
    : Screen
{
    private readonly IDialogService _dialog = dialog;
    private readonly IEventAggregator _events = events;
    private readonly IContainer _ioc = ioc;
    private readonly ISnackbarService _snackbar = snackbar;
    private readonly EventColor _backgroundColor = new() { Red = 45, Green = 153, Blue = 255 };
    private readonly DatabaseContext _db = ioc.Get<DatabaseContext>();
    private readonly EventColor _foregroundColor = new() { Red = 255, Green = 255, Blue = 255 };

    public bool IsPublicEvent { get; set; }

    public bool ReplaceCourseSchedule { get; set; } = true;

    public CalendarView CalendarView { get; set; }

    public DateTime DisplayDate { get; } = DateTime.Now.Date.AddHours(6);

    public MainWindowViewModel Main { get; } = main;

    public ObservableCollection<CourseFilterViewModel> CanvasCourses { get; set; } = new();

    public ScheduleAppointmentCollection Events { get; set; } = [];

    public string CalendarTheme { get; set; } = "Windows11Dark";

    public string ImportCanvasScheduleInput { get; set; }

    public string ImportCORScheduleInput { get; set; } = string.Empty;

    public string SelectedEventType { get; set; } = "Event";

    public bool IsDuplicate(Event e) => Events.Any(x => x.Id == e.Id.ToString());

    public IEnumerable<CORScheduleItem> ParseEvents(string text)
    {
        using var reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(text)));
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter         = "\t",
            HasHeaderRecord   = CORScheduleHeaderRegex().IsMatch(text),
            HeaderValidated   = null,
            MissingFieldFound = null
        });

        return csv.GetRecords<CORScheduleItem>().ToList();
    }

    public IEnumerable<Event> GetEventsByOrganization(string organization)
        => _db.OrganizationEvents
           .Where(x => x.Organization == organization)
           .Select(x => x.Schedule);

    public IEnumerable<Event> GetEventsBySection(string section)
        => _db.Courses
           .Where(x => x.Section == section)
           .Select(x => x.Schedule);

    public ScheduleAppointmentCollection GenerateRandomAppointments()
    {
        var WorkWeekDays = new ObservableCollection<DateTime>();
        var WorkWeekSubjects = new ObservableCollection<string>
        {
            "Class Meeting", "Homework", "Project", "Project Status Discussion",
            "Org Event", "Group Meeting", "Generate Report", "School Event", "General Meeting"
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
        var WeekEndOccurrance         = new ObservableCollection<DateTime>();
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

        var ran   = new Random();
        var today = DateTime.Now;
        if (today.Month == 12)
            today                        = today.AddMonths(-1);
        else if (today.Month == 1) today = today.AddMonths(1);

        var startMonth   = new DateTime(today.Year, today.Month - 1, 1, 0, 0, 0);
        var endMonth     = new DateTime(today.Year, today.Month + 1, 30, 0, 0, 0);
        var dt           = new DateTime(today.Year, today.Month, 15, 0, 0, 0);
        var day          = (int) startMonth.DayOfWeek;
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

        foreach (var appointment in appointments)
        {
            var newEvent = ToEvent(appointment);
            _db.Events.Add(newEvent);
        }

        _db.SaveChanges();

        return appointments;
    }

    public async Task ImportCanvasSchedule()
    {
        var pressed = await CalendarView.ImportCanvasScheduleDialog.ShowAndWaitAsync(true);

        switch (pressed)
        {
            case IDialogControl.ButtonPressed.None:
                break;
            case IDialogControl.ButtonPressed.Left:
                if (!string.IsNullOrWhiteSpace(ImportCanvasScheduleInput))
                {
                    var courses = CanvasCourses.Where(x => x.IsSelected);
                    var calendars = await courses
                       .ToAsyncEnumerable()
                       .SelectAwait(async x => await CanvasUtilities.GetCourseCalendarAsync(x.Course))
                       .ToListAsync();
                    var events = calendars.SelectMany(x => x.Events).Select(ToEvent).ToList();

                    _db.Events.AddRange(events);
                    
                    AddEvents(events);
                }

                CalendarView.ImportCanvasScheduleDialog.Hide();
                break;
            case IDialogControl.ButtonPressed.Right:
                CalendarView.ImportCanvasScheduleDialog.Hide();
                ImportCanvasScheduleInput = string.Empty;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // public async void ImportCanvas()
    // {
    //     if (_main.LoggedInUser is null) return;
    //     if (string.IsNullOrWhiteSpace(_main.LoggedInUser.CanvasToken))
    //     {
    //         await _snackbar.ShowAsync("Error", "Canvas token is not set.", SymbolRegular.ErrorCircle20, ControlAppearance.Caution);
    //         return;
    //     }

    //     var api = CanvasUtilities.CreateCanvasApi(
    //         "9822~8U1rMaW7lQN24wcaAL8aRgyDfgSsjn19Z5mbm7JhC9qq6YMRSOP1zmurpSr49hVh");

    //     var activities = await api.GetActivityStream(true);

    //     foreach (var activity in activities)
    //     {
    //         Console.WriteLine(activity.Title);
    //     }
    // }

    public async Task ImportCORSchedule()
    {
        var pressed = await CalendarView.ImportCORScheduleDialog.ShowAndWaitAsync(true);

        switch (pressed)
        {
            case IDialogControl.ButtonPressed.None:
                break;
            case IDialogControl.ButtonPressed.Left:

                if (!string.IsNullOrWhiteSpace(ImportCORScheduleInput))
                {
                    var cor     = ParseEvents(ImportCORScheduleInput);
                    var courses = cor.SelectMany(AddCORSchedule).ToList();
                    var events  = courses.Select(x => x.Schedule).ToList();
                    
                    AddEvents(events);
                }

                CalendarView.ImportCORScheduleDialog.Hide();
                break;
            case IDialogControl.ButtonPressed.Right:
                CalendarView.ImportCORScheduleDialog.Hide();
                ImportCORScheduleInput = string.Empty;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return;

        IEnumerable<Course> AddCORSchedule(CORScheduleItem cor)
        {
            var days  = cor.Days.Split('/', StringSplitOptions.TrimEntries);
            var times = cor.Time.Split('/', StringSplitOptions.TrimEntries);
            var rooms = cor.Room.Split('/', StringSplitOptions.TrimEntries);

            if (days.Length != times.Length || days.Length != rooms.Length)
            {
                throw new InvalidDataException(
                    $"Invalid schedule format. The amount schedule for {cor.Courses} is not consistent.");
            }

            var added     = new List<Guid>();
            var schedules = days.Zip(times, rooms);

            if (ReplaceCourseSchedule)
                _db.RemoveRange(_db.Courses.Where(x => x.CourseCode == cor.Courses && x.Section == cor.Section));
            else
            {
                var course = _db.Set<Course>()
                   .Where(x => !added.Contains(x.Id))
                   .Where(x => !x.Schedule.Private
                            || (x.Schedule.Creator != null && Main.LoggedInUser != null &&
                                x.Schedule.Creator.Id == Main.LoggedInUser.Id))
                   .FirstOrDefault(x
                        => x.CourseCode == cor.Courses
                        && x.Section == cor.Section);

                _snackbar.Show("Course Schedule Conflict",
                    $"""
                     The course {cor.Courses} {cor.Title} has a conflicting schedule.
                     If you want to change this schedule, check "Force replace existing schedules."
                     """,
                    SymbolRegular.ErrorCircle20, ControlAppearance.Caution);

                yield break;
            }

            foreach (var (day, timeRange, room) in schedules)
            {
                var byDay = day switch
                {
                    "M"  => "MO",
                    "T"  => "TU",
                    "W"  => "WE",
                    "TH" => "TH",
                    "F"  => "FR",
                    "S"  => "SA",
                    "SU" => "SU",
                    _    => throw new InvalidDataException("Invalid day of the week.")
                };

                var time  = timeRange.Split('-', StringSplitOptions.TrimEntries);
                var start = DateTimeOffset.Parse(time[0]);
                var end   = DateTimeOffset.Parse(time[1]);

                var schedule = new Event
                {
                    AllDay          = false,
                    Locked          = true,
                    Private         = false,
                    Start           = start,
                    End             = end,
                    BackgroundColor = new EventColor { Red = 45, Green  = 153, Blue = 255 },
                    ForegroundColor = new EventColor { Red = 255, Green = 255, Blue = 255 },
                    Id              = Guid.NewGuid(),
                    Description     = $"{cor.Title} - {cor.Section} - {room}",
                    RecurrenceRule  = $"FREQ=WEEKLY;BYDAY={byDay}",
                    Title           = $"{cor.Courses} {cor.Title}",
                    Reminders       = [new EventReminder { ReminderTimeInterval = TimeSpan.FromMinutes(15) }]
                };

                var course = _db.Add(new Course
                {
                    CourseCode = cor.Courses,
                    Title      = cor.Title,
                    Units      = cor.Units,
                    Section    = cor.Section,
                    Schedule   = schedule
                }).Entity;

                added.Add(course.Id);

                if (Main.LoggedInUser is not null && string.IsNullOrWhiteSpace(Main.LoggedInUser.Section))
                    Main.LoggedInUser.Section = cor.Section;

                _db.SaveChanges();

                yield return course;
            }
        }
    }

    public void AddEvents(IEnumerable<Event> events)
    {
        foreach (var e in events)
        {
            if (Events.Any(x => new Guid((string) x.Id) == e.Id))
                continue;

            var appointment = new ScheduleAppointment
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
            };

            var reminders = e.Reminders.Select(x => x.ToSchedulerReminder(appointment));
            appointment.Reminders = [new SchedulerReminder { ReminderTimeInterval = TimeSpan.FromMinutes(15) }];

            Events.Add(appointment);
        }
    }

    public void ExportCalendar()
    {
        var calendar = new Calendar();
        var events = Events.Select(x =>
            new CalendarEvent
            {
                //Uid             = (string) x.Id,
                Name            = x.Subject,
                Summary         = x.Notes,
                Location        = x.Location,
                IsAllDay        = x.IsAllDay,
                Start           = new CalDateTime(x.StartTime),
                End             = new CalDateTime(x.EndTime),
                RecurrenceRules = ToRecurrencePattern(x.RecurrenceRule)
            }).ToList();

        foreach (var e in events)
        {
            calendar.Events.Add(e);
        }

        var serializer = new CalendarSerializer();
        var icalString = serializer.SerializeToString(calendar);

        File.WriteAllText("calendar.ics", icalString);

        _snackbar.ShowAsync("Calendar Export", "Calendar exported successfully", SymbolRegular.Calendar16,
            ControlAppearance.Success);

        RecurrencePattern[] ToRecurrencePattern(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                return [];

            var regex = new Regex("BYDAY=(?<day>[^;]+)");
            var match = regex.Match(pattern);

            if (!match.Success)
                return [];

            return
            [
                new RecurrencePattern
                {
                    Frequency = FrequencyType.Weekly,
                    ByDay = match.Groups["day"].Value.Split(",")
                       .Select(x => new WeekDay(x switch
                        {
                            "SU" => DayOfWeek.Sunday,
                            "MO" => DayOfWeek.Monday,
                            "TU" => DayOfWeek.Tuesday,
                            "WE" => DayOfWeek.Wednesday,
                            "TH" => DayOfWeek.Thursday,
                            "FR" => DayOfWeek.Friday,
                            "SA" => DayOfWeek.Saturday,
                            _    => throw new ArgumentOutOfRangeException(nameof(x), x, null)
                        })).ToList()
                }
            ];
        }
    }

    public void OnEditorClosed(SfScheduler? sender, AppointmentEditorClosingEventArgs e)
    {
        switch (e.Action)
        {
            case AppointmentEditorAction.Add:
            {
                var appointment = e.Appointment;
                var newEvent    = ToEvent(appointment);

                if (main.LoggedInUser is not null)
                {
                    main.LoggedInUser.Events.Add(newEvent);
                    _db.Users.Update(main.LoggedInUser);
                }
                else
                    _db.Events.Add(newEvent);

                _db.SaveChanges();

                appointment.Id                    = newEvent.Id;
                appointment.AppointmentBackground = newEvent.BackgroundColor.ToBrush();
                break;
            }
            case AppointmentEditorAction.Edit:
            {
                var appointment = e.Appointment;
                var newEvent = _db.Events
                   .Include(x => x.Reminders)
                   .FirstOrDefault(x => x.Id == (Guid) appointment.Id);

                if (newEvent == null) return;

                _db.RemoveRange(newEvent.Reminders);

                newEvent.AllDay = appointment.IsAllDay;
                newEvent.Description = appointment.Notes;
                newEvent.End = appointment.EndTime;
                newEvent.Start = appointment.StartTime;
                newEvent.RecurrenceRule = appointment.RecurrenceRule;
                newEvent.Title = appointment.Subject;
                newEvent.Reminders = appointment.Reminders.Select(EventReminderExtensions.ToEventReminder).ToList();

                _db.Events.Update(newEvent);
                _db.SaveChanges();
                break;
            }
            case AppointmentEditorAction.Delete:
            {
                var appointment = e.Appointment;
                var newEvent    = _db.Events.Find((Guid) appointment.Id);

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

    public async void OnImportCanvasScheduleInputChanged()
    {
        if (string.IsNullOrWhiteSpace(ImportCanvasScheduleInput)) return;

        using var client = CanvasUtilities.CreateClient(ImportCanvasScheduleInput);

        try
        {
            var courses = await client.Courses
               .List(x => x.EnrollmentState = EnrollmentState.Active)
               .ConfigureAwait(false);
            var models = courses.Select(x => new CourseFilterViewModel(x));
            CanvasCourses = new ObservableCollection<CourseFilterViewModel>(models);
        }
        catch (Exception e)
        {
            _ = Application.Current.Dispatcher.Invoke(async () =>
            {
                await _snackbar.ShowAsync("Invalid API Key", e.Message, SymbolRegular.ErrorCircle20,
                    ControlAppearance.Caution);
            });
        }
    }

    protected override void OnViewLoaded()
    {
        CalendarView = (CalendarView) View;

        Events.Clear();

        if (Main.LoggedInUser is not null)
        {
            if (!string.IsNullOrWhiteSpace(Main.LoggedInUser.Section))
                AddEvents(GetEventsBySection(Main.LoggedInUser.Section));

            if (!string.IsNullOrWhiteSpace(Main.LoggedInUser.Organization))
                AddEvents(GetEventsByOrganization(Main.LoggedInUser.Organization));

            AddEvents(Main.LoggedInUser.Events);
        }

        AddEvents(_db.Events.Where(x => !x.Private));
    }

    private bool IsEqual(ScheduleAppointment x, Event y)
    {
        if (x.Id is null) return false;
        if (new Guid((string) x.Id) == y.Id) return true;

        return x.Subject == y.Title
            && x.Notes == y.Description
            && x.StartTime == y.Start
            && x.EndTime == y.End
            && x.IsAllDay == y.AllDay
            && x.RecurrenceRule == y.RecurrenceRule;
    }

    private bool IsEqual(CalendarEvent x, Event y) => x.Uid == y.CanvasId;

    private Brush GetAppointmentForeground(Brush backgroundColor)
    {
        if (backgroundColor.ToString().Equals("#FF8551F2") || backgroundColor.ToString().Equals("#FF5363FA") ||
            backgroundColor.ToString().Equals("#FF2D99FF"))
            return Brushes.White;
        return new SolidColorBrush(Color.FromRgb(51, 51, 51));
    }

    private Event ToEvent(CalendarEvent calendarEvent) => new()
    {
        AllDay          = calendarEvent.IsAllDay,
        Description     = calendarEvent.Url.ToString(),
        Start           = calendarEvent.Start.Value,
        End             = calendarEvent.End?.Value ?? calendarEvent.Start.Value,
        RecurrenceRule  = calendarEvent.RecurrenceRules?.FirstOrDefault()?.ToString(),
        Title           = calendarEvent.Summary,
        Private         = true,
        Locked          = true,
        Creator         = Main.LoggedInUser,
        ForegroundColor = new EventColor { Red = 255, Green = 255, Blue = 255 },
        BackgroundColor = new EventColor { Red = 45, Green  = 153, Blue = 255 },
        Reminders       = [new EventReminder { ReminderTimeInterval = TimeSpan.FromMinutes(15) }]
    };

    private Event ToEvent(ScheduleAppointment appointment)
    {
        var fg = _db.Set<EventColor>()
           .FirstOrDefault(x => x.Equals(new EventColor(appointment.Foreground)));
        var bg = _db.Set<EventColor>()
           .FirstOrDefault(x => x.Equals(new EventColor(appointment.AppointmentBackground)));

        return new Event
        {
            AllDay          = appointment.IsAllDay,
            Description     = appointment.Notes,
            Start           = appointment.StartTime,
            End             = appointment.EndTime,
            RecurrenceRule  = appointment.RecurrenceRule,
            Title           = appointment.Subject,
            Private         = Main.IsLoggedIn && !IsPublicEvent,
            Locked          = false,
            Creator         = Main.LoggedInUser,
            ForegroundColor = new EventColor(appointment.Foreground),
            BackgroundColor = new EventColor(appointment.AppointmentBackground),
            Reminders       = appointment.Reminders.Select(EventReminderExtensions.ToEventReminder).ToList()
        };
    }

    [GeneratedRegex("Courses\tTitle\tSection\tUnits\tDays\tTime\tRoom")]
    private static partial Regex CORScheduleHeaderRegex();

    public class CourseFilterViewModel(ICourse course)
    {
        public bool IsSelected { get; init; } = true;

        public ICourse Course { get; init; } = course;

        // Let's generate the acronym for the course name
        public string CourseNameAcronym => string
           .Join("", course.Name.Split(' ')
               .Select(x => x.FirstOrDefault(char.IsLetter)))
           .ToUpper();
    }

    public record CORScheduleItem(
        string Courses,
        string Title,
        string Section,
        int Units,
        string Days,
        string Time,
        string Room);
}