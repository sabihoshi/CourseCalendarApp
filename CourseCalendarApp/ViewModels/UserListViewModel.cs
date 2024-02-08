using CourseCalendarApp.Models;
using Stylet;
using StyletIoC;

namespace CourseCalendarApp.ViewModels;

public class UserListViewModel(IContainer ioc, MainWindowViewModel main) : Screen
{
    private readonly MainWindowViewModel _main = main;

    public BindableCollection<string> UserNames => new(Users.Select(x => x.Name));

    public BindableCollection<User> Users { get; set; } = new();

    public BindableCollection<User> FilteredUsers =>
        string.IsNullOrWhiteSpace(FilterText)
            ? Users
            : new BindableCollection<User>(Users.Where(x
                => x.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase)));

    public bool IsEmpty => !Users.Any();

    public bool ShowDeleteButton { get; set; }

    public string FilterText { get; set; } = string.Empty;

    public User? SelectedUser { get; set; }

    public event EventHandler<User>? UserSelected;

    public async Task DeleteUser(User user)
    {
        await using var db = ioc.Get<DatabaseContext>();
        db.Users.Remove(user);
        await db.SaveChangesAsync();

        OnActivate();
    }

    public void Activate() => OnActivate();

    public void OnUserSelected() => UserSelected?.Invoke(this, SelectedUser!);

    protected override void OnActivate()
    {
        var db = ioc.Get<DatabaseContext>();
        Users = new BindableCollection<User>(db.Users);
        NotifyOfPropertyChange(() => FilteredUsers);
    }
}