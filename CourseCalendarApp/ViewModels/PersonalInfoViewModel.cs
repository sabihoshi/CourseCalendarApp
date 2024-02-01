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

    public BindableCollection<User> Users { get; set; } = new();

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
        _user = db.Users
            .FirstOrDefault(x => x.Id == User.Id);
        Users = new BindableCollection<User>(db.Users.Where(x => x.Id != User.Id));
    }
}