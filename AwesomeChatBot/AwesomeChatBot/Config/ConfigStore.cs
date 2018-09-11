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
        /// Gets a value from the config store. Returns default value if config value not defined
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetConfigValue<T>(string key, params IConfigurationDependency[] dependencies) where T : IConvertible
        {
            var fileName = GetFileNameFromDependencies(dependencies);
            var configNotFound = false;
            var configFile = this.ConfigFiles.FirstOrDefault(x => x.Name.ToLower() == fileName.ToLower());

            // if config file does not exist or is empty
            // return default value for the requested type (means config value is not set)
            if (configFile == null || configFile.Sections == null || configFile.Sections.Count == 0)
                configNotFound = true;

            var dependenciesOrdered = dependencies.OrderBy(x => x.ConfigOrder).ToList();
            var curSection = configFile.Sections.FirstOrDefault(x => x.Id == dependenciesOrdered[0].ConfigId);

            if (!configNotFound)
            {
                // Find the config entry section
                foreach (var dependency in dependenciesOrdered)
                {
                    // If section was not found, return default value (config value not set)
                    if (curSection == null)
                    {
                        configNotFound = true;
                        break;
                    }

                    curSection = curSection.SubSections.FirstOrDefault(x => x.Id == dependency.ConfigId);
                }
            }

            // Get the configuration value
            if (!configNotFound)
            {
                var configEntry = curSection.Config.FirstOrDefault(x => x.Key == key);
                if (configEntry == null)
                    configNotFound = true;
                else
                {
                    return (T)Convert.ChangeType(configEntry.Value, typeof(T));
                }
            }

            // If section was not found, return default value (config value not set)
            if (configNotFound)
                return default(T);             
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
