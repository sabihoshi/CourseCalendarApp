using CourseCalendarApp.Models;
using CourseCalendarApp.Views;
using ModernWpf;
using Stylet;
using StyletIoC;
using Syncfusion.SfSkinManager;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Theme = Syncfusion.SfSkinManager.Theme;

namespace CourseCalendarApp.ViewModels;

public class MainWindowViewModel : Conductor<IScreen>
{
    public static MainWindowView MainView = null!;
    public static NavigationStore Navigation = null!;
    private readonly IDialogService _dialog;
    private readonly IEventAggregator _events;
    private readonly IContainer _ioc;
    private readonly ISnackbarService _snackbar;
    private readonly IThemeService _theme;

    public MainWindowViewModel(IStyletIoCBuilder builder)
    {
        Title = $"FEU Course Calendar System {SettingsPageViewModel.ProgramVersion}";

        builder.Bind<MainWindowViewModel>().ToInstance(this);
        _ioc      = builder.BuildContainer();
        _events   = _ioc.Get<IEventAggregator>();
        _theme    = _ioc.Get<IThemeService>();
        _snackbar = _ioc.Get<ISnackbarService>();
        _dialog   = _ioc.Get<IDialogService>();

        HomePage         = _ioc.Get<HomeViewModel>();
        SettingsPage     = _ioc.Get<SettingsPageViewModel>();
        LoginPage        = _ioc.Get<LoginViewModel>();
        AdminPage        = _ioc.Get<AdminViewModel>();
        PersonalInfoPage = _ioc.Get<PersonalInfoViewModel>();
        UserListPage     = _ioc.Get<UserListViewModel>();
        RegistrationPage = _ioc.Get<RegistrationViewModel>();
        CalendarPage     = _ioc.Get<CalendarViewModel>();
    }

    public AdminViewModel AdminPage { get; }

    public bool IsAdmin => LoggedInUser is { AccessType: "Admin" };

    public bool IsLoggedIn => LoggedInUser is not null;

    public bool IsLoggedOut => LoggedInUser is null;

    public CalendarViewModel CalendarPage { get; set; }

    public HomeViewModel HomePage { get; }

    public LoginViewModel LoginPage { get; }

    public PersonalInfoViewModel PersonalInfoPage { get; }

    public Screen ContentDialogContent { get; set; }

    public RegistrationViewModel RegistrationPage { get; }

    public Screen FirstPage => HomePage;

    public SettingsPageViewModel SettingsPage { get; }

    public string LogText => IsLoggedIn ? "Logout" : "Login";

    public string Title { get; set; }

    public SymbolRegular LogIcon => IsLoggedIn ? SymbolRegular.DoorArrowLeft20 : SymbolRegular.Person12;

    public User? LoggedInUser { get; set; }

    public UserListViewModel UserListPage { get; }

    public async Task LoginAsync(User employee)
    {
        LoggedInUser = employee; 

        NavigateToItem(HomePage);
        await _snackbar.ShowAsync("Welcome!", $"You have successfully logged in as {LoggedInUser.Name}.",
            SymbolRegular.WeatherSunny20, ControlAppearance.Primary);
    }

    public async Task Logout()
    {
        if (LoggedInUser is null) return;

        await using var db = _ioc.Get<DatabaseContext>();
        await _snackbar.ShowAsync("Goodbye",
            "You have logged out. Goodbye.",
            SymbolRegular.WeatherMoon20, ControlAppearance.Secondary);

        LoggedInUser = null;
    }

    public void Navigate(INavigation sender, RoutedNavigationEventArgs args)
    {
        if (args.CurrentPage is NavigationItem { Tag: IScreen viewModel })
            ActivateItem(viewModel);
    }

    public void NavigateToItem(IScreen view)
    {
        if (view == SettingsPage)
        {
            NavigateToSettings();
            return;
        }

        var navigationItem = Navigation.Items.OfType<NavigationItem>()
            .Select((item, index) => new { Item = item, Index = index })
            .First(x => x.Item.Tag == view);
        Navigation.SelectedPageIndex = navigationItem.Index;
        Navigation.Navigate(navigationItem.Item.PageType);
    }

    public void NavigateToSettings()
    {
        ActivateItem(SettingsPage);
        Navigation.Navigate(typeof(SettingsPageView));
    }

    public void ToggleTheme()
    {
        ThemeManager.Current.ApplicationTheme = _theme.GetTheme() switch
        {
            ThemeType.Unknown      => ApplicationTheme.Dark,
            ThemeType.Dark         => ApplicationTheme.Light,
            ThemeType.Light        => ApplicationTheme.Dark,
            ThemeType.HighContrast => ApplicationTheme.Dark,
            _                      => ApplicationTheme.Dark
        };

        CalendarPage.CalendarTheme = _theme.GetTheme() switch
        {
            ThemeType.Unknown      => "Windows11Dark",
            ThemeType.Dark         => "Windows11Light",
            ThemeType.Light        => "Windows11Dark",
            ThemeType.HighContrast => "Windows11Dark",
            _                      => "Windows11Dark"
        };

        if (CalendarPage.CalendarView is not null)
            SfSkinManager.SetTheme(CalendarPage.CalendarView.Calendar, new Theme() { ThemeName = CalendarPage.CalendarTheme });

        SettingsPage.OnThemeChanged();
    }

    protected override async void OnViewLoaded()
    {
        MainView   = (MainWindowView) View;
        Navigation = MainView.RootNavigation;
        SettingsPage.OnThemeChanged();

        NavigateToItem(FirstPage);

        MainView.RootSnackBar.Timeout = (int) TimeSpan.FromSeconds(3).TotalMilliseconds;

        _snackbar.SetSnackbarControl(MainView.RootSnackBar);
        _dialog.SetDialogControl(MainView.RootContentDialog);

        await _snackbar.ShowAsync("Welcome!", "Welcome to FEU Course Calendar",
            SymbolRegular.RibbonStar24, ControlAppearance.Primary);
    }
}