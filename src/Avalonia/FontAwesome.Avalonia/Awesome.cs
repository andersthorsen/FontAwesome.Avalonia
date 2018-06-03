using System;
using System.Windows;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace FontAwesome.Avalonia
{
    /// <summary>
    /// Provides attached properties to set FontAwesome icons on controls.
    /// </summary>
    public class Awesome
    {

        /// <summary>
        /// FontAwesome FontFamily.
        /// </summary>
        public static readonly FontFamily FontAwesomeFontFamily = new FontFamily("resm:FontAwesome.Avalonia.FontAwesome.otf#FontAwesome");

        /// <summary>
        /// Identifies the FontAwesome.Avalonia.Awesome.Content attached dependency property.
        /// </summary> 
        public static readonly AttachedProperty<FontAwesomeIcon> ContentProperty =
            AvaloniaProperty.RegisterAttached<Awesome, ContentControl, FontAwesomeIcon>("Column");

        /// <summary>
        /// Gets the content of a ContentControl, expressed as a FontAwesome icon.
        /// </summary>
        /// <param name="target">The ContentControl subject of the query</param>
        /// <returns>FontAwesome icon found as content</returns>
        public static FontAwesomeIcon GetContent(ContentControl target)
        {
            return (FontAwesomeIcon)target.GetValue(ContentProperty);
        }

        /// <summary>
        /// Sets the content of a ContentControl expressed as a FontAwesome icon. This will cause the content to be redrawn.
        /// </summary>
        /// <param name="target">The ContentControl where to set the content</param>
        /// <param name="value">FontAwesome icon to set as content</param>
        public static void SetContent(ContentControl target, FontAwesomeIcon value)
        {
            target.SetValue(ContentProperty, value);
        }

        private static void ContentChanged(ContentControl target, AvaloniaPropertyChangedEventArgs evt)
        {
            // If value is not a FontAwesomeIcon just ignore: Awesome.Content property can only be set to a FontAwesomeIcon value
            if (!(evt.NewValue is FontAwesomeIcon)) return;

            FontAwesomeIcon symbolIcon = (FontAwesomeIcon)evt.NewValue;
            int symbolCode = (int)symbolIcon;
            char symbolChar = (char)symbolCode;

            target.FontFamily = FontAwesomeFontFamily;
            target.Content = symbolChar;
        }

        private const FontAwesomeIcon DEFAULT_CONTENT = FontAwesomeIcon.None;
    }
}
