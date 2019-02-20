namespace AwesomeChatBot.Commands
{
    /// <summary>
    /// A command represents an entity that reacts to defined messages
    /// </summary>
    public abstract class Command : Config.IConfigurationDependency
    {
        /// <summary>
        /// Unique name of the command
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The unique named used to identify this item in configuration files
        /// </summary>
        public string ConfigId => this.Name;

        /// <summary>
        /// The hierarchical order inside the config. Command level configuration values are usually server or channel dependent
        /// </summary>
        public int ConfigOrder => 99999999;
    }
}
