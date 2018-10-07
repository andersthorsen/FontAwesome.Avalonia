using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace FontAwesome.Generate.Models
{
    public class Github
    {
        [YamlMember(Alias = "url")]
        public string Url { get; set; }
    }
}