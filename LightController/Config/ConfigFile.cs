﻿using LightController.Config.Bacnet;
using LightController.Config.Dmx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace LightController.Config
{
    public class ConfigFile
    {
        [YamlMember(Description = "Name of the MIDI device, or blank to pick the first one")]
        public string MidiDevice { get; set; }

        [YamlMember(Description = "Scene to load on startup")]
        public string DefaultScene { get; set; }

        [YamlMember(Alias = "Pro", Description = "ProPresenter settings")]
        public ProPresenterConfig ProPresenter { get; set; } = new ProPresenterConfig();

        [YamlMember(Description = "DMX settings")]
        public DmxConfig Dmx { get; set; } = new DmxConfig();

        [YamlMember(Description = "List of lighting scenes")]
        public List<Scene> Scenes { get; set; } = new List<Scene>();

        [YamlMember(Description = "Transition time in seconds for switching scenes")]
        public double DefaultTransitionTime { get; set; } = 1;

        [YamlMember(Description = "BACnet settings")]
        public BacnetConfig Bacnet { get; set; } = new BacnetConfig();

        private string fileLocation;

        public ConfigFile() { }

        public static ConfigFile Load(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                ConfigFile existingFile;
                using (StreamReader reader = File.OpenText(fileLocation))
                {
                    DeserializerBuilder deserializer = new DeserializerBuilder()
                        .IgnoreFields()
                        .IgnoreUnmatchedProperties()
                        .WithNamingConvention(UnderscoredNamingConvention.Instance);
                    foreach (var tag in GetYamlTags())
                        deserializer.WithTagMapping(tag.Item1, tag.Item2);

                    existingFile = deserializer.Build().Deserialize<ConfigFile>(reader);
                }
                if(existingFile != null)
                {
                    existingFile.fileLocation = fileLocation;
                    return existingFile;
                }
            }
            ConfigFile newFile = new ConfigFile();
            newFile.fileLocation = fileLocation;
            newFile.Save();
            return newFile;
        }

        public void Save(string fileLocation = null)
        {
            if (fileLocation == null)
                fileLocation = this.fileLocation;
            using(StreamWriter writer = File.CreateText(fileLocation))
            {
                SerializerBuilder serializer = new SerializerBuilder()
                    .IgnoreFields()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance);
                foreach (var tag in GetYamlTags())
                    serializer.WithTagMapping(tag.Item1, tag.Item2);

                serializer.Build().Serialize(writer, this);
            }
        }

        private static IEnumerable<Tuple<TagName, Type>> GetYamlTags()
        {
            foreach(Type t in typeof(MainWindow).Assembly.GetTypes())
            {
                if(t.IsDefined(typeof(YamlTagAttribute), false))
                {
                    YamlTagAttribute attr = t.GetCustomAttribute<YamlTagAttribute>();
                    yield return new Tuple<TagName, Type>(attr.Tag, t);
                }
            }
        }

        public void Open()
        {
            using Process fileopener = new Process();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + fileLocation + "\"";
            fileopener.Start();
        }
    }
}
