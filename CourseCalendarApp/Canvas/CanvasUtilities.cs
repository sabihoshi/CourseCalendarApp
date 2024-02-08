using CourseCalendarApp.Models;
using Microsoft.EntityFrameworkCore;
using ReverseMarkdown;
using UVACanvasAccess.ApiParts;
using static ReverseMarkdown.Config.UnknownTagsOption;

namespace CourseCalendarApp.Canvas;

public static class CanvasUtilities
{
    private static readonly Config Config = new()
    {
        UnknownTags       = Bypass,
        SmartHrefHandling = true
    };

    private static readonly Converter Converter = new(Config);

    public static Api CreateCanvasApi(string token) => new(token, "https://feu.instructure.com/api/v1/");
}