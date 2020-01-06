using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using FontAwesome.Generate.Models;
using MoreLinq.Extensions;

namespace FontAwesome.Generate
{
    public class FAConfigYamlParser
    {
        public FAConfigYamlParser(string iconPath, string categoryPath)
        {

            var deserializer = new DeserializerBuilder()
                                        .IgnoreUnmatchedProperties()
                                        .Build();

            Categories = deserializer.Deserialize<Dictionary<string, CategoryEntry>>(new StreamReader(categoryPath));

            Items = deserializer.Deserialize<Dictionary<string, IconEntry>>(new StreamReader(iconPath));

            // Map categories onto the icons
            Categories.ForEach(c =>
                c.Value.Icons.ForEach(i => Items[i].Categories.Add(c.Key)));
        }

        public Dictionary<string, CategoryEntry> Categories { get; set; }

        public Dictionary<string, IconEntry> Items { get; }
    }
}