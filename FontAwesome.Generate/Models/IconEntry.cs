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
        private static readonly Regex REG_PROP = new Regex(@"\([^)]*\)");

        [YamlMember(Alias = "name")]
        public string Name { get; set; }
        [YamlMember(Alias = "id")]
        public string Id { get; set; }
        [YamlMember(Alias = "unicode")]
        public string Unicode { get; set; }
        [YamlMember(Alias = "created")]
        public string Created { get; set; }

        [YamlMember(Alias = "aliases")]
        public List<string> Aliases { get; set; }

        [YamlMember(Alias = "categories")]
        public List<string> Categories { get; set; }

        private string _safeName = null;

        [YamlIgnore]
        public string SafeName
        {
            get
            {
                if (string.IsNullOrEmpty(_safeName))
                {
                    _safeName = Safe(Id);
                }
                return _safeName;
            }
        }

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