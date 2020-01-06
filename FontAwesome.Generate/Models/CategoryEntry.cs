using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace FontAwesome.Generate.Models
{
    public class CategoryEntry
    {
        [YamlMember(Alias = "icons")]
        public List<string> Icons { get; set; }

        [YamlMember(Alias = "label")]
        public string Label { get; set; }
    }
}
