using System.Web.Helpers;
using AutoBogus;

namespace CourseCalendarApp.Models;

public class ExampleData
{
    public static List<User> GenerateUsers(int count)
    {
        AutoFaker.Configure(builder =>
        {
            builder
                .WithSkip<Guid>()
                .WithSkip<Guid?>();
        });

        var users = new List<User>();

        for (var i = 0; i < count; i++)
        {
            var employee = AutoFaker.Generate<User>();

            employee.Password   = Crypto.HashPassword("password");
            employee.AccessType = Random.Shared.Next(0, 2) == 0 ? "User" : "Admin";

            users.Add(employee);
        }

        return users;
    }
}