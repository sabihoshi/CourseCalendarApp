using System.Net.Http;
using CanvasApi.Client;
using CanvasApi.Client.Courses.Models;
using ReverseMarkdown;
using static ReverseMarkdown.Config.UnknownTagsOption;
using Calendar = Ical.Net.Calendar;

namespace CourseCalendarApp.Canvas;

public static class CanvasUtilities
{
    private static readonly Config Config = new()
    {
        UnknownTags = Bypass,
        SmartHrefHandling = true
    };

    private static readonly Converter Converter = new(Config);
    private static readonly HttpClient HttpClient = new();

    public static CanvasApiClient CreateClient(string token) =>
        new("https://feu.instructure.com/api/v1/", token, null);

    public static async Task<Calendar> GetCourseCalendarAsync(ICourse course)
        => Calendar.Load(await HttpClient.GetStringAsync(course.Calendar.Ics));
}