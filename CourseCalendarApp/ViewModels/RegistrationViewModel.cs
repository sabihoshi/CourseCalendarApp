using System.Web.Helpers;
using CourseCalendarApp.Models;
using Stylet;
using StyletIoC;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

namespace CourseCalendarApp.ViewModels;

public class RegistrationViewModel(
    IContainer ioc,
    ISnackbarService snackBar,
    DatabaseContext db,
    MainWindowViewModel main)
    : Screen
{
    private readonly IContainer _ioc = ioc;

    public BindableCollection<User> Users => new(db.Users);

    public bool CanInteract { get; set; } = true;

    public User? SelectedManager { get; set; }

    public string AccessType { get; set; } = string.Empty;


    public string Name { get; set; } = string.Empty;

   
    public string Password { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public async Task Register()
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            await snackBar.ShowAsync("Error!", "Please provide a username.", SymbolRegular.Person12,
                ControlAppearance.Caution);
            return;
        }

        if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
        {
            await snackBar.ShowAsync("Error!", "Password is too weak.", SymbolRegular.Password20,
                ControlAppearance.Caution);
            return;
        }

        if (db.Users.Any(x => x.Username == Username))
        {
            await snackBar.ShowAsync("Error!", "Username already exists", SymbolRegular.ErrorCircle20,
                ControlAppearance.Danger);
            return;
        }

        var user = new User
        {
            Username     = Username,
            Password     = Crypto.HashPassword(Password),
            Name = Name
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        main.NavigateToItem(main.EmployeeListPage);
        await snackBar.ShowAsync("Success", "Successfully created user.", SymbolRegular.AddCircle20,
            ControlAppearance.Success);
    }
}