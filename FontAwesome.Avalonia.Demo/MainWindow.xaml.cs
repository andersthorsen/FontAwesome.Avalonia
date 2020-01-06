using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FontAwesome.Avalonia.Demo
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            this.FindControl<TextBlock>("TestTextBlock").Text = char.ConvertFromUtf32(0xf0e0);
        }
    }
}
