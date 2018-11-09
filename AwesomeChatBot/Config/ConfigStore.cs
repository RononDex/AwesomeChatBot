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
        private List<ConfigFile> ConfigFiles { get; set; } = new List<ConfigFile>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configFolder">Path to the folder containing the config files</param>
        public ConfigStore(string configFolder)
        {
            this.ConfigFolder = configFolder;

            if (string.IsNullOrEmpty(this.ConfigFolder))
                this.ConfigFolder = "./config";

            this.LoadConfigFiles();
        }

        /// <summary>
        /// Gets a value from the config store. Returns default value if config value not defined
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetConfigValue<T>(string key, T defaultValue = default(T), params IConfigurationDependency[] dependencies) where T : IConvertible
        {
            var fileName = GetFileNameFromDependencies(dependencies);
            var configNotFound = false;
            var configFile = this.ConfigFiles.FirstOrDefault(x => x.Name.ToLower() == fileName.ToLower());

            // if config file does not exist or is empty
            // return default value for the requested type (means config value is not set)
            if (configFile == null || configFile.Sections == null || configFile.Sections.Count == 0)
                configNotFound = true;

            if (!configNotFound)
            {
                var dependenciesOrdered = dependencies.Where(x => x != null).OrderBy(x => x.ConfigOrder).ToList();
                var curSection = configFile.Sections.FirstOrDefault(x => x.Id == dependenciesOrdered[0].ConfigId);

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
            return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependencies"></param>
        public void SetConfigValue<T>(string key, T value, params IConfigurationDependency[] dependencies) where T : IConvertible
        {
            var fileName = GetFileNameFromDependencies(dependencies);

            // Get file, create it if it doesnt exist yet
            var configFile = this.ConfigFiles.FirstOrDefault(x => x.Name.ToLower() == fileName.ToLower());
            if (configFile == null)
            {
                configFile = new ConfigFile();
                configFile.Name = fileName.ToLower();
                configFile.Sections = new List<ConfigSection>();
                this.ConfigFiles.Add(configFile);
            }

            var dependenciesSorted = dependencies.Where(x => x != null).OrderBy(x => x.ConfigOrder);
            ConfigSection curParentSection = null;

            // Find the section
            foreach (var dependency in dependenciesSorted)
            {
                ConfigSection section = null;
                if (curParentSection == null)
                    section = configFile.Sections.FirstOrDefault(x => x.Id == dependency.ConfigId);
                else
                    section = curParentSection.SubSections.FirstOrDefault(x => x.Id == dependency.ConfigId);

                // Create section if it doesnt exist yet
                if (section == null)
                {
                    section = new ConfigSection();
                    section.Id = dependency.ConfigId;

                    // Add to parent
                    if (curParentSection == null)
                        configFile.Sections.Add(section);
                    else
                        curParentSection.SubSections.Add(section);
                }

                curParentSection = section;

                // Save the config value
                var configEntry = curParentSection.Config.FirstOrDefault(x => x.Key == key);
                if (configEntry == null)
                {
                    configEntry = new ConfigValue();
                    configEntry.Key = key;
                    configEntry.Value = value?.ToString();
                }
                else
                    configEntry.Value = value?.ToString();
            }

            // Save the changed config file
            SaveConfigFile(configFile);
        }

        /// <summary>
        /// Determines wether a command is active in the given context
        /// </summary>
        /// <param name="server"></param>
        /// <param name="channel"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool IsCommandActive(Commands.Command command, params IConfigurationDependency[] dependencies)
        {
            // get "enabled" setting for the command in the given context, using "false" by default
            var isActive = this.GetConfigValue<bool>("enabled", false, dependencies);    
            return isActive;
        }

        /// <summary>
        /// Enables the given command in the given context
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        public void EnableCommand(Commands.Command command, params IConfigurationDependency[] dependencies)
        {
            // Enables the command in the config file
            this.SetConfigValue<bool>("enabled", true, dependencies);
        }

        /// <summary>
        /// Disables the given command in the given context
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dependencies"></param>
        /// <returns></returns>
        public void DisableCommand(Commands.Command command, params IConfigurationDependency[] dependencies)
        {
            // Enables the command in the config file
            this.SetConfigValue<bool>("enabled", false, dependencies);
        }

        /// <summary>
        /// Loads the config files from the ConfigFolder folder
        /// </summary>
        private void LoadConfigFiles()
        {
            // Create directory if it does not exist
            if (!Directory.Exists(this.ConfigFolder))
                Directory.CreateDirectory(this.ConfigFolder);

            var jsonFiles = Directory.GetFiles(this.ConfigFolder, "*.json");

            foreach (var jsonFile in jsonFiles)
            {
                var parsedConfigFile = JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText(jsonFile));
                parsedConfigFile.Name = Path.GetFileNameWithoutExtension(jsonFile);
                this.ConfigFiles.Add(parsedConfigFile);
            }
        }

        /// <summary>
        /// Saves the given configuration file
        /// </summary>
        private void SaveConfigFile(ConfigFile file)
        {
            var fileName = file.Name + ".json";
            var json = JsonConvert.SerializeObject(file);
            var path = Path.Combine(this.ConfigFolder, fileName);
            File.WriteAllText(path, json);
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
