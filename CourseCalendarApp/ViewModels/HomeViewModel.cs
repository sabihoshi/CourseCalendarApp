using Stylet;

namespace CourseCalendarApp.ViewModels;

public class HomeViewModel(MainWindowViewModel main) : Screen
{
    public MainWindowViewModel Main { get; } = main;

    public void Navigate(Screen screen)
    {
        if (screen != Main.LoginPage
            && screen != Main.SettingsPage
            && screen != Main.EmployeeListPage
            && Main.IsLoggedOut)
            Main.NavigateToItem(Main.LoginPage);
        else
            Main.NavigateToItem(screen);
    }
}