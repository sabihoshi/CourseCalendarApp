using System.Web.Helpers;
using CourseCalendarApp.Models;
using Microsoft.EntityFrameworkCore;
using Stylet;
using StyletIoC;
using Wpf.Ui.Mvvm.Contracts;

namespace CourseCalendarApp.ViewModels;

public class LoginViewModel(
    IEventAggregator events,
    IContainer ioc,
    ISnackbarService snackbar,
    MainWindowViewModel main)
    : Screen
{
    private readonly IEventAggregator _events = events;
    private readonly ISnackbarService _snackbar = snackbar;

    public bool LoginError { get; set; }

    public string Password { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public async Task Login()
    {
        LoginError = true;
        NotifyOfPropertyChange(() => LoginError);

        if (string.IsNullOrEmpty(Password)) return;
        if (string.IsNullOrEmpty(Username)) return;

        var db = ioc.Get<DatabaseContext>();

        var employee = await db.Users.FirstOrDefaultAsync(x => x.Username == Username);
        if (employee == null) return;

        if (Crypto.VerifyHashedPassword(employee.Password, Password))
        {
            LoginError = false;
            main.Login(employee);
        }
    }

    protected override async void OnActivate()
    {
        Username = string.Empty;
        Password = string.Empty;

        await main.Logout();
    }
}