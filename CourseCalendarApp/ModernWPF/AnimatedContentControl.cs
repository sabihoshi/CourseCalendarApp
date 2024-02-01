using System.Windows.Controls;
using CourseCalendarApp.ModernWPF.Animation;
using CourseCalendarApp.ViewModels;

namespace CourseCalendarApp.ModernWPF;

public class AnimatedContentControl : ContentControl
{
    private static Transition Transition => SettingsPageViewModel.Transition!.Object;

    protected override void OnContentChanged(object? oldContent, object? newContent)
    {
        if (oldContent != null)
        {
            var exit = Transition.GetExitAnimation(oldContent, false);
            exit?.Begin();
        }

        if (newContent != null)
        {
            var enter = Transition.GetEnterAnimation(newContent, false);
            enter?.Begin();
        }

        base.OnContentChanged(oldContent, newContent);
    }
}