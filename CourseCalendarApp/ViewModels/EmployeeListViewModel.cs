using CourseCalendarApp.Models;
using Stylet;
using StyletIoC;

namespace CourseCalendarApp.ViewModels;

public class EmployeeListViewModel(IContainer ioc, MainWindowViewModel main) : Screen
{
    private readonly MainWindowViewModel _main = main;

    public BindableCollection<string> EmployeeNames => new(Employees.Select(x => x.Name));

    public BindableCollection<User> Employees { get; set; } = new();

    public BindableCollection<User> FilteredEmployees =>
        string.IsNullOrWhiteSpace(FilterText)
            ? Employees
            : new BindableCollection<User>(Employees.Where(x
                => x.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase)));

    public bool IsEmpty => !Employees.Any();

    public bool ShowDeleteButton { get; set; }

    public string FilterText { get; set; } = string.Empty;

    public User? SelectedEmployee { get; set; }

    public event EventHandler<User>? EmployeeSelected;

    public async Task DeleteEmployee(User employee)
    {
        await using var db = ioc.Get<DatabaseContext>();
        db.Users.Remove(employee);
        await db.SaveChangesAsync();

        OnActivate();
    }

    public void Activate() => OnActivate();

    public void OnEmployeeSelected() => EmployeeSelected?.Invoke(this, SelectedEmployee!);

    protected override void OnActivate()
    {
        var db = ioc.Get<DatabaseContext>();
        Employees = new BindableCollection<User>(db.Users);
        NotifyOfPropertyChange(() => FilteredEmployees);
    }
}