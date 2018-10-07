using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace FontAwesome.Generate.Models
{
    public class IconContainer
    {
        [YamlMember(Alias = "icons")]
        public List<IconEntry> Icons { get; set; }
    }
}