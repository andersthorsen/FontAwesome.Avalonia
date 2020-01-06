using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FontAwesome.Generate.Models
{
    public class IconEntry
    {
        public IconEntry()
        {
            Categories = new List<string>();
        }

        private static readonly Regex REG_PROP = new Regex(@"\([^)]*\)");

        [YamlMember(Alias = "unicode")]
        public string Unicode { get; set; }

        [YamlMember(Alias = "categories")]
        public List<string> Categories { get; set; }

        [YamlMember(Alias = "label")]
        public string Label { get; set; }

        [YamlMember(Alias = "styles")]
        public List<string> Styles { get; set; }
        
        public string Safe(string text)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;

            if (text.EndsWith("-o") || text.Contains("-o-"))
                text = text.Replace("-o", "-outline");

            var stringBuilder = new StringBuilder(textInfo.ToTitleCase(text.Replace("-", " ")));

            stringBuilder
                .Replace("-", string.Empty).Replace("/", "_")
                .Replace(" ", string.Empty).Replace(".", string.Empty)
                .Replace("'", string.Empty);

            var matches = REG_PROP.Matches(stringBuilder.ToString());
            stringBuilder = new StringBuilder(REG_PROP.Replace(stringBuilder.ToString(), string.Empty));
            var hasMatch = false;

            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                if (match.Value.IndexOf("Hand", StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    hasMatch = true;
                    break;
                }
            }

            if (hasMatch)
            {
                stringBuilder.Insert(0, "Hand");
            }

            if (char.IsDigit(stringBuilder[0]))
                stringBuilder.Insert(0, '_');

            return stringBuilder.ToString();
        }
    }
}