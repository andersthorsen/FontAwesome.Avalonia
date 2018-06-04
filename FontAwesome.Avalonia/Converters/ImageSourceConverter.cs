// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Linq;
// using System.Text;
// using Avalonia.Data;
// using Avalonia.Markup;
// using Avalonia.Media;

// namespace FontAwesome.Avalonia.Converters
// {
//     /// <summary>
//     /// Converts a FontAwesomIcon to an ImageSource. Use the ConverterParameter to pass the Brush.
//     /// </summary>
//     public class ImageSourceConverter
//         : MarkupExtension, IValueConverter
//     {
//         public override object ProvideValue(IServiceProvider serviceProvider)
//         {
//             return this;
//         }

//         public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//         {
//             if (!(value is FontAwesomeIcon)) return null;

//             var brush = parameter as Brush;

//             if (brush == null)
//                 brush = Brushes.Black;
            
//             return ImageAwesome.CreateImageSource((FontAwesomeIcon)value, brush);
//         }

//         public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }
