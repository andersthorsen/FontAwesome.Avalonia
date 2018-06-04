using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace FontAwesome.Avalonia
{
    /// <summary>
    /// Provides a lightweight control for displaying a FontAwesome icon as text.
    /// </summary>
    public class FontAwesome
        : TextBlock
    {
        /// <summary>
        /// FontAwesome FontFamily.
        /// </summary>
        private static readonly FontFamily FontAwesomeFontFamily = Awesome.FontAwesomeFontFamily;
        /// <summary>

        /// Identifies the FontAwesome.Avalonia.FontAwesome.Icon dependency property.
        /// </summary>
        public static readonly StyledProperty<FontAwesomeIcon> IconProperty =
            AvaloniaProperty.Register<FontAwesome, FontAwesomeIcon>(nameof(Icon));

        /// <summary>
        /// Gets or sets the FontAwesome icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public FontAwesomeIcon Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        } 

        static FontAwesome()
        {
            IconProperty.Changed.Subscribe(OnIconPropertyChanged);
        }
 
        private static void OnIconPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if(!(e.Sender is FontAwesome)) return;
            var target = e.Sender as FontAwesome;
          //  this.SetValue(FontFamilyProperty, FontAwesomeFontFamily);
            target.SetValue(TextAlignmentProperty, TextAlignment.Center);
            target.SetValue(TextProperty, char.ConvertFromUtf32((int)e.NewValue));
        }
    }
}
