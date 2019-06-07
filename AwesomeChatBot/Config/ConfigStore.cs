using Microsoft.Extensions.Logging;
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

        private ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="configFolder">Path to the folder containing the config files</param>
        public ConfigStore(string configFolder, ILoggerFactory loggerFactory)
        {
            ConfigFolder = configFolder;
            LoggerFactory = loggerFactory;

            if (string.IsNullOrEmpty(ConfigFolder))
            {
                loggerFactory.CreateLogger(GetType().FullName).LogWarning("No ConfigFolderPath provided, will be using the default './config' directory!");
                ConfigFolder = "config";
            }

            LoadConfigFiles();
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
            var configFile = ConfigFiles.FirstOrDefault(x => x.Name.ToLower() == fileName.ToLower());

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
                    // Ignore null entries
                    if (dependency == null)
                        continue;

                    // If section was not found, return default value (config value not set)
                    if (curSection == null)
                    {
                        configNotFound = true;
                        return defaultValue;
                    }

                    curSection = curSection.SubSections.FirstOrDefault(x => x.Id == dependency.ConfigId);
                }

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
        /// Gets a list of all currently set config values for given dependencies
        /// </summary>
        /// <param name="dependencies"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IReadOnlyList<ConfigValue> GetAllConfigValues(params IConfigurationDependency[] dependencies)
        {
            var fileName = GetFileNameFromDependencies(dependencies);

            var configFile = ConfigFiles.FirstOrDefault(x => x.Name.ToLower() == fileName.ToLower());

            // if config file does not exist or is empty
            // return default value for the requested type (means config value is not set)
            if (configFile == null || configFile.Sections == null || configFile.Sections.Count == 0)
                return new List<ConfigValue>();

            var dependenciesOrdered = dependencies.Where(x => x != null).OrderBy(x => x.ConfigOrder).ToList();
            var curSection = configFile.Sections.FirstOrDefault(x => x.Id == dependenciesOrdered[0].ConfigId);

            // Find the config entry section
            foreach (var dependency in dependenciesOrdered)
            {
                // Ignore null entries
                if (dependency == null)
                    continue;

                // If section was not found, return default value (config value not set)
                if (curSection == null)
                {
                    return new List<ConfigValue>();
                }

                curSection = curSection.SubSections.FirstOrDefault(x => x.Id == dependency.ConfigId);
            }

            var configEntries = curSection.Config;

            return configEntries;
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
            var configFile = ConfigFiles.FirstOrDefault(x => x.Name.ToLower() == fileName.ToLower());
            if (configFile == null)
            {
                configFile = new ConfigFile
                {
                    Name = fileName.ToLower(),
                    Sections = new List<ConfigSection>()
                };
                ConfigFiles.Add(configFile);
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
                    section = new ConfigSection
                    {
                        Id = dependency.ConfigId
                    };

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
                    configEntry = new ConfigValue
                    {
                        Key = key,
                        Value = value?.ToString()
                    };
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
        public bool IsCommandActive(Commands.Command command, bool enabledByDefault, params IConfigurationDependency[] dependencies)
        {
            // get "enabled" setting for the command in the given context, using "false" by default
            var isActive = GetConfigValue($"Command-{command.Name}-Enabled", enabledByDefault, dependencies);
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
            SetConfigValue($"Command-{command.Name}-Enabled", true, dependencies);
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
            SetConfigValue($"Command-{command.Name}-Enabled", false, dependencies);
        }

        /// <summary>
        /// Loads the config files from the ConfigFolder folder
        /// </summary>
        private void LoadConfigFiles()
        {
            // Create directory if it does not exist
            if (!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);

            var jsonFiles = Directory.GetFiles(ConfigFolder, "*.json");

            foreach (var jsonFile in jsonFiles)
            {
                var parsedConfigFile = JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText(jsonFile));
                parsedConfigFile.Name = Path.GetFileNameWithoutExtension(jsonFile);
                ConfigFiles.Add(parsedConfigFile);
            }
        }

        /// <summary>
        /// Saves the given configuration file
        /// </summary>
        private void SaveConfigFile(ConfigFile file)
        {
            var fileName = file.Name;
            var json = JsonConvert.SerializeObject(file);
            File.WriteAllText(fileName, json);
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
                fileName = string.Join("-", dependencies.Where(x => x != null).OrderBy(x => x.ConfigOrder).Select(x => x.GetType().Name.ToLower()));
                fileName += ".json";
            }

            return Path.Combine(ConfigFolder, fileName);
        }
    }
}
