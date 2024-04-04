using CourseCalendarApp.Models;
using Stylet;
using StyletIoC;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

namespace CourseCalendarApp.ViewModels;

public class PersonalInfoViewModel(
    IContainer ioc,
    ISnackbarService snackBar,
    MainWindowViewModel main)
    : Screen
{
    private User? _user;

    public BindableCollection<string> Organizations { get; set; } = new();

    public BindableCollection<string> Sections { get; set; } = new();

    public BindableCollection<User> Users { get; set; } = new();

    public DateTime DateOfBirth
    {
        get => User.DateOfBirth?.DateTime ?? DateTime.Now;
        set => User.DateOfBirth = value;
    }

    public int Age => Math.Min(DateTime.Now.Year - DateOfBirth.Year - (DateOfBirth.DayOfYear < DateTime.Now.DayOfYear ? 0 : 1), 0);

    public User User
    {
        get => _user ?? main.LoggedInUser!;
        set => _user = value;
    }

    public async Task Save()
    {
        await using var db = ioc.Get<DatabaseContext>();

        db.Users.Update(User);
        await db.SaveChangesAsync();

        await snackBar.ShowAsync("Success", "Successfully saved changes to the database.",
            SymbolRegular.PeopleCheckmark20, ControlAppearance.Success);
    }

    public void Activate() => OnActivate();

    protected override void OnActivate()
    {
        var db = ioc.Get<DatabaseContext>();

        Users = new BindableCollection<User>(db.Users.Where(x => x.Id != User.Id));
        Sections = new BindableCollection<string>(db.Users
            .Select(x => x.Section)
            .Where(x => x != null)
            .ToList().Cast<string>().Distinct());
        Organizations = new BindableCollection<string>(db.Users
            .Select(x => x.Organization)
            .Where(x => x != null)
            .ToList().Cast<string>().Distinct());
    }
}