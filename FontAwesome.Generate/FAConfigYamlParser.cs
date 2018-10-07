using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using FontAwesome.Generate.Models;

namespace FontAwesome.Generate
{
    public class FAConfigYamlParser
    {
        private readonly ConfigContainer _config;
        private readonly IconContainer _iconContainer;

        public FAConfigYamlParser(string configYaml)
        {

            var deserializer = new DeserializerBuilder()
                                        .IgnoreUnmatchedProperties()
                                        .Build();

            _config = deserializer.Deserialize<ConfigContainer>(new StreamReader(configYaml));

            if (string.IsNullOrEmpty(_config.IconMeta)) throw new Exception("Missing Icon metadata on config.yml");

            var iconPath = Path.Combine(Path.GetDirectoryName(configYaml), _config.IconMeta);

            if (!File.Exists(iconPath))
                throw new FileNotFoundException("icon.yaml file specified in _config.yaml could not be found", iconPath);

            _iconContainer = deserializer.Deserialize<IconContainer>(new StreamReader(iconPath));
        }

        public IEnumerable<IconEntry> Items
        {
            get { return _iconContainer.Icons; }
        }

        public FontAwesomeConfig Config
        {
            get { return _config.FontAwesome; }
        }

        public ConfigContainer Container
        {
            get { return _config; }
        }

    }
}