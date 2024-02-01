using CourseCalendarApp.Models;
using Stylet;

namespace CourseCalendarApp.ViewModels;

public class UserViewModel : Screen
{
    public bool ShowDeleteButton { get; set; }

    public User User { get; set; }
}