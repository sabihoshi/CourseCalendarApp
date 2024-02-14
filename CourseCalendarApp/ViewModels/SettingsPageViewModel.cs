using System.Reflection;
using CourseCalendarApp.Models;
using CourseCalendarApp.ModernWPF;
using ModernWpf;
using Stylet;
using StyletIoC;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;
using Animation_Transition = CourseCalendarApp.ModernWPF.Animation.Transition;
using TransitionCollection = CourseCalendarApp.ModernWPF.Animation.Transitions.TransitionCollection;

namespace CourseCalendarApp.ViewModels;

public class SettingsPageViewModel(IContainer ioc, MainWindowViewModel main) : Screen
{
    private readonly IEventAggregator _events = ioc.Get<IEventAggregator>();
    private readonly IThemeService _theme = ioc.Get<IThemeService>();

    public bool CanChangeCredentials => main.IsLoggedIn;

    public static CaptionedObject<Animation_Transition>? Transition { get; set; } =
        TransitionCollection.Transitions[0];

    public int AmountToGenerate { get; set; }

    public string Password { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string CanvasKey { get; set; } = string.Empty;

    public static Version ProgramVersion => Assembly.GetExecutingAssembly().GetName().Version!;

    public async Task GenerateFakeData()
    {
        var db = ioc.Get<DatabaseContext>();
        db.Users.AddRange(ExampleData.GenerateUsers(AmountToGenerate));
        await db.SaveChangesAsync();

        main.CalendarPage.GenerateRandomAppointments();

        var service = ioc.Get<ISnackbarService>();
        await service.ShowAsync("Success", "Successfully generated fake data.",
            SymbolRegular.PersonAdd20, ControlAppearance.Success);
    }

    public async Task Save()
    {
        if (main.LoggedInUser is null) return;

        await using var db = ioc.Get<DatabaseContext>();
        var snackBar = ioc.Get<ISnackbarService>();

        if (string.IsNullOrWhiteSpace(Username))
        {
            await snackBar.ShowAsync("Error!", "Please provide a username.");
            return;
        }

        if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
        {
            await snackBar.ShowAsync("Error!", "Password is too weak.");
            return;
        }

        if (db.Users.Any(x => x.Username == Username))
        {
            await snackBar.ShowAsync("Error!", "Username already exists");
            return;
        }

        main.LoggedInUser.Username = Username;
        main.LoggedInUser.Password = Password;

        db.Update(main.LoggedInUser);
        await db.SaveChangesAsync();

        await snackBar.ShowAsync("Success", "Successfully saved changes to the database.",
            SymbolRegular.PeopleCheckmark20, ControlAppearance.Success);
    }

    public void Clear()
    {
        Username = string.Empty;
        Password = string.Empty;
    }

    public void OnThemeChanged() => _theme.SetTheme(ThemeManager.Current.ApplicationTheme switch
    {
        ApplicationTheme.Light => ThemeType.Light,
        ApplicationTheme.Dark  => ThemeType.Dark,
        _                      => _theme.GetSystemTheme()
    });

    protected override void OnActivate() => Username = main.LoggedInUser?.Username ?? string.Empty;
}