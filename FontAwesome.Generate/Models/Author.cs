using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace FontAwesome.Generate.Models
{
    public class Author
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }
        [YamlMember(Alias = "github")]
        public string Github { get; set; }
    }
}