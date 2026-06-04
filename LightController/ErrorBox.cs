using System.Diagnostics;
using System.Windows;

namespace LightController;

public static class ErrorBox
{
    public static void Show(string msg, bool kill = true)
    {
        MessageBox.Show(msg, "Light Controller", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
        if(kill)
        {
            Log.Error("Application closed: '" + msg + "'");
            Process.GetCurrentProcess().Kill();
        }
    }

    /// <summary>
    /// Closes the application if the user presses cancel
    /// </summary>
    public static void AskRetryFatal(string msg)
    {
        var result = MessageBox.Show(msg, "Light Controller", MessageBoxButton.RetryCancel, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
        if (result != MessageBoxResult.Retry)
        {
            Log.Error("Application closed after user prompt: '" + msg + "'");
            Process.GetCurrentProcess().Kill();
        }
    }

    public static bool AskRetry(string msg)
    {
        var result = MessageBox.Show(msg, "Light Controller", MessageBoxButton.RetryCancel, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
        return result == MessageBoxResult.Retry;
    }
}
