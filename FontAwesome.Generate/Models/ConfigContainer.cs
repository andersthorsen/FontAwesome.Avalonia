using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace FontAwesome.Generate.Models
{
    public class ConfigContainer
    {
        [YamlMember(Alias = "icon_meta")]
        public string IconMeta { get; set; }

        [YamlMember(Alias = "icon_destination")]
        public string IconDestination { get; set; }

        [YamlMember(Alias = "fontawesome")]
        public FontAwesomeConfig FontAwesome { get; set; }
    }
}