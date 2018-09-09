using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AwesomeChatBot.Config
{
    /// <summary>
    /// Responsible for loading and storing configuration values
    /// </summary>
    public class ConfigStore
    {
        /// <summary>
        /// Path to the folder containing all the config files
        /// </summary>
        public string ConfigFolder { get; set; }

        /// <summary>
        /// Internal list of all config files
        /// </summary>
        private List<ConfigFile> ConfigFiles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configFolder">Path to the folder containing the config files</param>
        public ConfigStore(string configFolder)
        {
            this.ConfigFolder = configFolder;
        }

        /// <summary>
        /// Gets a value from the config store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetConfigValue<T>(params IConfigurationDependency[] dependencies) where T : IConvertible
        {

        }

        /// <summary>
        /// Loads the config files from the ConfigFolder folder
        /// </summary>
        private void LoadConfigFiles()
        {
            var jsonFiles = Directory.GetFiles(this.ConfigFolder, "*.json");
            var serializer = new JsonSerializer();

            foreach (var jsonFile in jsonFiles)
            {
                var parsedConfigFile = JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText(jsonFile));
                parsedConfigFile.Name = Path.GetFileNameWithoutExtension(jsonFile);
                this.ConfigFiles.Add(parsedConfigFile);
            }
        }

        /// <summary>
        /// Gets the file name where the configuration for the given dependencies is stored in
        /// </summary>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        private string GetFileNameFromDependencies(params IConfigurationDependency[] dependencies)
        {
            var fileName = "";
            if (dependencies == null || dependencies.Length == 0)
                fileName = "global.json";
            else
            {
                fileName = string.Join('-', dependencies.OrderBy(x => x.ConfigOrder).Select(x => x.GetType().Name.ToLower()));
                fileName += ".json";
            }

            return Path.Combine(ConfigFolder, fileName);
        }
    }
}
