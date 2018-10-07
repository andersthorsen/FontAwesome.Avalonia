using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace FontAwesome.Generate.Models
{
    public class FontAwesomeConfig
    {
        [YamlMember(Alias = "doc_blob")]
        public string Version { get; set; }

        [YamlMember(Alias = "url")]
        public string Url { get; set; }

        [YamlMember(Alias = "tagline")]
        public string Tagline { get; set; }
        [YamlMember(Alias = "author")]
        public Author Author { get; set; }
        [YamlMember(Alias = "github")]
        public Github Github { get; set; }
    }
}