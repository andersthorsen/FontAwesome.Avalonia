using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Logging.Serilog;

namespace FontAwesome.Avalonia.Demo
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            var assembly = Assembly.GetAssembly(typeof(IRotatable));

            foreach (var resourceName in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine(resourceName);

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug(LogEventLevel.Verbose);
    }
}
